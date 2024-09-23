      const urlassessmentupdate = getInputVal('#urlassessmentupdate')
      const urlassessmentadd = getInputVal('#urlassessmentadd')
      const urlassessmentdelete = getInputVal('#urlassessmentdelete')
      const urlassetdetail = getInputVal('#urlassetdetail')
      const urlassessmentdetail = getInputVal('#urlassessmentdetail')
      const urlassessmentprint = getInputVal('#urlassessmentprint')
      const urlassessmentinspection = getInputVal('#urlassessmentinspection')
      const urlassessmentmaintenance = getInputVal('#urlassessmentmaintenance')
      const urlinspectiondetail = getInputVal('#urlinspectiondetail')
      const urlmaintenancedetail = getInputVal('#urlmaintenancedetail')

      
      const urlParams = new URLSearchParams(window.location.search);
      const idParam = Number(urlParams.get('assessmentid'));
      const assetIdParam = Number(urlParams.get('assetid'));
      $(document).ready(function () {
          $('.btn-add-new-inspection').click(addTab);
          if(idParam) {
              $('.btn-add-new-inspection[attr-inspection-id='+idParam+']').trigger('click')
          } else if(assetIdParam) {
              $('.btn-add-new-inspection[attr-id=' + assetIdParam + ']').first().trigger('click')
          }
      });

      const assessmentCalculatedField = [
          'consequenceOfFailure',
          'tP1A',
          'tP2A',
          'tP3A',
          'tP1B',
          'tP2B',
          'tP3B',
          'tP1C',
          'tP2C',
          'tP3C',
          'tpTimeToActionA',
          'tpTimeToActionB',
          'tpTimeToActionC',
      ]

      var gLastInspectionDate = null;

      $(document).on('click', '.btn-close-inspection', function () {
          const ctr = $(this).closest('a').attr('tab-num')
          close_inspection(ctr);
      })

      function submitForm(el) {
          const form = $(el).closest('form.assessment-form')
          const equipmentId = $(el).closest('.tab-pane').attr('equipment-id')
          const ctr = $(el).closest('.tab-pane').attr('tab-num')
          const nativeForm = form[0]
          const formData = new FormData(nativeForm);
          const selectedInspectionCount = form.find('input[name=selectedInspectionId]').length
          var alertmessage = '';
          if(!selectedInspectionCount){
              alertmessage += 'minimum 1 Inspection, ';
          }
          if(alertmessage.length > 0){
              alert('Please select ' + alertmessage.slice(0, -2) + ' to proceed');
              return;
          }
          const submitbutton = $(el);
          const buttontext = submitbutton.text();
          if(form.valid()) {
            submitbutton.prop("disabled", true).text("Loading...");
              if(form.hasClass('history-form')) {
                  // update
                  $.ajax({
                      url: urlassessmentupdate,
                      type: 'POST', // or 'GET' depending on your form method
                      data: formData,
                      headers: {
                          '__RequestVerificationToken': requestVerificationToken
                      },
                      contentType: false,
                      processData: false,
                      success: function(apiresult) {
                          if(apiresult.isSuccess){
                              alert(apiresult.message)
                              var res = apiresult.data;
                              // Handle success response
                              $('.btn-add-new-inspection[attr-inspection-id='+res.id+']').html(res.assessmentDate)
                              fetchHistoryData({
                                  id: res.id,
                                  counter: ctr
                              })
                          } else {
                              alert(apiresult.message)
                          }
                          submitbutton.removeAttr("disabled").text(buttontext);
                      },
                      error: function(xhr, status, error) {
                          alert('Error submitting the form')
                          submitbutton.removeAttr("disabled").text(buttontext);
                      }
                  });
              } else {
                  // add new
                  $.ajax({
                      url: urlassessmentadd,
                      type: 'POST', // or 'GET' depending on your form method
                      data: formData,
                      headers: {
                          '__RequestVerificationToken': requestVerificationToken
                      },
                      contentType: false,
                      processData: false,
                      success: function(apiresult) {
                          if(apiresult.isSuccess){
                              alert(apiresult.message)
                              var res = apiresult.data;
                              form.addClass('history-form')
                              // add menu to tree
                              $('ul[equipment-id="' + equipmentId + '"] li').eq(0).after(`
                                  <li class="treeview">
                                      <a class="btn-add-new-inspection" history-button="" 
                                      attr-id="${res.asset.id}" attr-inspection-id="${res.id}" 
                                      attr-name="${res.asset.tagNo} ${res.assessmentDate}" 
                                      attr-sidebar-level="4" 
                                      attr-find-inspection="${res.asset.businessArea}" 
                                      attr-find-platform="${res.asset.platform}" 
                                      attr-find-equipment="${res.asset.tagNo}" 
                                      attr-find-imr="NEW"
                                      onclick="addTab.apply(this)"
                                      >
                                          ${res.assessmentDate} (NEW)
                                      </a>
                                  </li>
                              `)
                              $('.nav-tab-menus').find('a[tab-num='+ctr+'] div[tab-title]').text(res.asset.tagNo + ' ' + res.assessmentDate)

                              fetchHistoryData({
                                  id: res.id,
                                  counter: ctr
                              })
                          } else {
                              alert(apiresult.message)
                          }
                          submitbutton.removeAttr("disabled").text(buttontext);
                      },
                      error: function(xhr, status, error) {
                          // Handle error
                          alert('An assessment with the same asset and date already exists')
                          submitbutton.removeAttr("disabled").text(buttontext);
                      }
                  });
              }
          } else {
              alert('please check the required fields')
          }
      }

      function deleteForm(el) {
          const form = $(el).closest('form.assessment-form')

          const id = form.find('#data-id').val()
          const tabId = $(el).closest('.tab-pane').attr('id')
          const ctr = $(el).closest('.tab-pane').attr('tab-num')

          if(confirm('Are you sure you want to delete this data?')) {
              $.ajax({
                  url: urlassessmentdelete,
                  type: 'POST', // or 'GET' depending on your form method
                  data: {
                      Id: id
                  },
                  headers: {
                      '__RequestVerificationToken': requestVerificationToken
                  },
                  success: function (apiresponse) {
                      if(apiresponse.isSuccess){
                          alert(apiresponse.message)
                          var response = apiresponse.data;
                          close_inspection(ctr, true)
                          $('.btn-add-new-inspection[attr-inspection-id='+id+']').closest('li').remove()
                          // Handle success response
                      } else {
                          alert(apiresponse.message)
                      }
                  },
                  error: function(xhr, status, error) {
                      alert('Error deleting data')
                  }
              });
          }
      }

      function deleteFromTab(id, name) {
          if (confirm('Are you sure you want to delete ' + name + '?')) {
              $.ajax({
                  url: urlassessmentdelete,
                  type: 'POST', // or 'GET' depending on your form method
                  data: {
                      Id: id
                  },
                  headers: {
                      '__RequestVerificationToken': requestVerificationToken
                  },
                  success: function (response) {
                      alert('Data successfully deleted')
                      $('.btn-add-new-inspection[attr-inspection-id=' + id + ']').closest('li').remove()
                  },
                  error: function (xhr, status, error) {
                      alert('Error deleting data')
                  }
              });
          }
      }

      function fetchAssetData ({id, counter}) {
          $.ajax({
              url: urlassetdetail,
              type: 'GET',
              data: { id },
              headers: {
                  '__RequestVerificationToken': requestVerificationToken
              },
              success: function (apiresult) {
                  if (apiresult.isSuccess) {
                      var data = apiresult.data;
                      fillAssetInfo ({counter, data})
                  } else {
                      alert(apiresult.message);
                      $('#inspectionheader' + counter).remove();
                      $('#inspection' + counter).remove();
                      moveToLastTab()
                  }
              },
              error: function (error) {
                  alert('Valve Tag not found');
                  $('#inspectionheader' + counter).remove();
                  $('#inspection' + counter).remove();
                  moveToLastTab()
              }
          });
      }

      function fetchHistoryData ({id, counter}) {
          $.ajax({
              url: urlassessmentdetail,
              type: 'GET',
              data: { id },
              headers: {
                  '__RequestVerificationToken': requestVerificationToken
              },
              success: function (apiresult) {
                  if(apiresult.isSuccess)
                  {
                      var data = apiresult.data;
                      var tempinspection = [];
                      data.inspectionHistory.forEach(function(inspection) {
                          tempinspection.push(inspection.inspection)
                      });
                      data.inspectionHistory = tempinspection;
                      var tempmaintenance = [];
                      data.maintenanceHistory.forEach(function(maintenance) {
                          tempmaintenance.push(maintenance.maintenance)
                      });
                      data.maintenanceHistory = tempmaintenance;
                      fillAssetInfo({
                          counter, data: data.asset
                      })

                      if(data.inspectionHistory.length > 0) {
                          fillInspectionTable(counter, data.inspectionHistory)
                      }
                      if(data.maintenanceHistory.length > 0) {
                          fillMaintenanceTable(counter, data.maintenanceHistory)
                      }

                      // check if field editable
                      const editable = assessmentCalculatedField.some(field => data[field] === null)
                      if(!editable) {
                          // $('#inspection'+counter).find('input:not([type="hidden"]), textarea, select').attr('disabled', true)
                          $('#inspection'+counter).find('.print-button').attr('href', urlassessmentprint+'/'+id)
                          // $('#inspection'+counter+' .assessment-form').addClass('not-editable')
                          // $('.only-add').hide()
                          $('#inspection'+counter+' .print-button').show()
                      }

                      // fill history data
                      $('#field-inspection-date'+counter).val(data.inspectionDate)
                      $('#field-inspection-inspection-method'+counter).val(data.inspectionMethodID)
                      $('#field-inspection-current-condition-limit-state-a'+counter).val(data.currentConditionLeakeageToAtmosphereID)
                      $('#field-inspection-current-condition-limit-state-b'+counter).val(data.currentConditionFailureOfFunctionID)
                      $('#field-inspection-current-condition-limit-state-c'+counter).val(data.currentConditionPassingAcrossValveID)
                      $('#field-inspection-effectiveness'+counter).val(data.inspectionEffectivenessID)
                      $('#field-inspection-function-condition'+counter).val(data.functionCondition)
                      $('#field-inspection-description'+counter).val(data.inspectionDescription)
                      $('#field-inspection-test-pressure-if-any'+counter).val(data.testPressureIfAny)
                      $('#maintenanceDataTemp'+counter).val(JSON.stringify(data.maintenanceHistory))
                      $('#inspectionDataTemp'+counter).val(JSON.stringify(data.inspectionHistory))

                      //fill assessment data
                      $('#assessment-no'+counter).val(data.assessmentNo)
                      $('#assessment-date'+counter).val(data.assessmentDate)
                      $('#time-period'+counter).val(data.timePeriode)
                      $('#time-to-limit-a'+counter).val(data.timeToLimitStateLeakageToAtmosphere)
                      $('#time-to-limit-b'+counter).val(data.timeToLimitStateFailureOfFunction)
                      $('#time-to-limit-c'+counter).val(data.timeToLimitStatePassingAccrosValve)
                      $('#inspection-effectiveness'+counter).val(data.inspectionEffectivenessID)
                      $('#impact-of-internal-fluid'+counter).val(data.impactOfInternalFluidImpuritiesID)
                      $('#impact-of-operating-envelopes'+counter).val(data.impactOfOperatingEnvelopesID)
                      $('#used-within-oem-specification'+counter).val(data.usedWithinOEMSpecificationID)
                      $('#repaired'+counter).val(data.repairedID)
                      $('#product-loss-definition'+counter).val(data.productLossDefinition)
                      $('#hsse-definition'+counter).val(data.hsseDefinisionID)
                      $('#assessment-summary'+counter).val(data.summary)
                      if(data.integrityStatus != null && data.integrityStatus != ''){
                        $('#integrity-status'+counter).val(data.integrityStatus)
                        $('#integrity-status'+counter).attr('x-fromdb', 'true')
                      }
                      if(data.timeToAction != null && data.timeToAction != ''){
                        $('#time-to-action'+counter).val(data.timeToAction)
                        $('#time-to-action'+counter).attr('x-fromdb', 'true')
                      }
                      if(data.recommendationActionID != null){
                        $('#recommended-action'+counter).val(data.recommendationActionID)
                      }
                      $('#detailed-recommendation'+counter).val(data.detailedRecommendation)

                      //LF1 & LF2
                      console.log('data', data)
                      $('#lastInspectionDate'+counter).val(data.inspectionDate)
                      $('#leakage-to-atmosphere'+counter).val(data.leakageToAtmosphereID)
                      $('#failure-of-function'+counter).val(data.failureOfFunctionID)
                      $('#passing-accross-valve'+counter).val(data.passingAccrosValveID)

                      $('#leakage-tp1'+counter).val(data.leakageToAtmosphereTP1ID)
                      $('#leakage-tp2'+counter).val(data.leakageToAtmosphereTP2ID)
                      $('#leakage-tp3'+counter).val(data.leakageToAtmosphereTP3ID)

                      $('#failure-tp1'+counter).val(data.failureOfFunctionTP1ID)
                      $('#failure-tp2'+counter).val(data.failureOfFunctionTP2ID)
                      $('#failure-tp3'+counter).val(data.failureOfFunctionTP3ID)

                      $('#passing-tp1'+counter).val(data.passingAccrosValveTP1ID)
                      $('#passing-tp2'+counter).val(data.passingAccrosValveTP2ID)
                      $('#passing-tp3'+counter).val(data.passingAccrosValveTP3ID)

                      //fill ID
                      $('#inspection'+counter).find('#data-id').val(id)
                      $('#inspection'+counter).find('form').addClass('history-form')
                      $('#inspection'+counter).find('.calculate-assessment').first().trigger('change')

                    //   let recordmeta = $('#record-meta' + counter);
                    //   console.log(recordmeta)
                    //   $('#record-meta' + counter).html(`Created by ${data.createdByUser} on ${data.createdAt}`);
                    //   if(data.updatedByUser){
                    //       $('#record-meta' + counter).html($('#record-meta' + counter).html() + `, Last updated by ${data.updatedByUser} on ${data.updatedAt}`);
                    //   }
                    //   $('#record-info' + counter).show();
                       $('#createdby' + counter).val(data.createdByUser);
                       $('#createdat' + counter).val(data.createdAt);
                       $('#updatedby' + counter).val(data.updatedByUser);
                       $('#updatedat' + counter).val(data.updatedAt);
                  } else {
                      alert(apiresult.message);
                      $('#inspectionheader' + counter).remove();
                      $('#inspection' + counter).remove();
                      moveToLastTab()
                  }
              },
              error: function (error) {
                  alert('History not found');
                  $('#inspectionheader' + counter).remove();
                  $('#inspection' + counter).remove();
                  moveToLastTab()
              }
          });
      }

      function fillAssetInfo ({counter, data}) {
          console.log(data)
          $('#field-assessment-assetid' + counter).val(data.id)
          $('#field-assessment-valvetype' + counter).val(data.valveType)
          $('#field-assessment-tagno' + counter).val(data.tagNo)
          $('#field-assessment-manufacturer' + counter).val(data.manufacturer)
          $('#field-assessment-equipmentname' + counter).val(data.assetName)
          $('#field-assessment-bodymaterial' + counter).val(data.bodyMaterial)
          $('#field-assessment-businessarea' + counter).val(data.businessArea)
          $('#field-assessment-bodymodel' + counter).val(data.bodyModel)
          $('#field-assessment-platform' + counter).val(data.platform)
          $('#field-assessment-endconnection' + counter).val(data.endConnection)
          $('#field-assessment-parentequipmentno' + counter).val(data.parentEquipmentNo)
          $('#field-assessment-serialno' + counter).val(data.serialNo)
          $('#field-assessment-parentequipmentdesc' + counter).val(data.parentEquipmentDescription)
          $('#field-assessment-usagetype' + counter).val(data.usageType)
          $('#field-assessment-installationdate' + counter).val(data.installationDate)
          $('#field-assessment-size' + counter).val(data.size)
          $('#field-assessment-pidno' + counter).val(data.pidNo)
          $('#field-assessment-classrating' + counter).val(data.classRating)
          $('#field-assessment-servicefluid' + counter).val(data.serviceFluid)
          $('#field-assessment-fluidphase' + counter).val(data.fluidPhase)
          $('#field-assessment-flowrate' + counter).val(data.flowRate)
          $('#field-assessment-operatingtemperature' + counter).val(data.operatingTemperature)
          $('#field-assessment-operatingpressure' + counter).val(data.operatingPressure)
          $('#field-assessment-toxicorflamableliquid' + counter).val(data.toxicOrFlamableFluid)
          $('#field-assessment-costofreplacement' + counter).val(data.costOfReplacementAndRepair)
          $('#field-assessment-actuatormfg' + counter).val(data.actuatorMfg)
          $('#field-assessment-actuatorserialno' + counter).val(data.actuatorSerialNo)
          $('#field-assessment-actuatortypemodel' + counter).val(data.actuatorTypeModel)
          $('#field-assessment-actuatorpower' + counter).val(data.actuatorPower)
          $('#field-assessment-manualoverride' + counter).val(data.manualOverride)
          $('#field-assessment-status' + counter).val(data.status)
          $('#field-assessment-actuation' + counter).val(data.actuation)
      }

      function calculateXPos(value) {
          let gapx = 20;
          let cat1min = 16;
          let cat1max = 49;
          let cat2min = cat1max + 1;
          let cat2max = 210;
          let cat3min = cat2max + 1;
          let cat3max = 891;
          let cat4min = cat3max + 1;
          let cat4max = 3769;
          let cat5min = cat4max + 1;
          let cat5max = 16050;

          value = value*1;
          xpos = 0;
          if(value <= cat1min){
              xpos = 0;
          } else if(value<=cat1max) {
              xpos = ((value-cat1min)/(cat1max-cat1min) * 20) + (gapx*0);
          } else if(value<=cat2max) {
              xpos = ((value-cat2min)/(cat2max-cat2min) * 20) + (gapx*1);
          } else if(value<=cat3max) {
              xpos = ((value-cat3min)/(cat3max-cat3min) * 20) + (gapx*2);
          } else if(value<=cat4max) {
              xpos = ((value-cat4min)/(cat4max-cat4min) * 20) + (gapx*3);
          } else if(value<=cat5max) {
              xpos = ((value-cat5min)/(cat5max-cat5min) * 20) + (gapx*4);
          } else {
              xpos = 100;
          }
          return xpos;
      }

      function getHeatColor(pos) {
          if(['1A', '2A', '1B'].includes(pos)){
              return 'olive';
          } else if(['3A', '2B', '1C'].includes(pos)){
              return 'green';
          } else if(['4A', '3B', '3C', '2C','1D'].includes(pos)){
              return 'yellow';
          } else if(['5A', '5B', '4B', '4C', '3D', '2D', '2E', '1E'].includes(pos)){
              return 'orange';
          } else {
              return 'red';
          }
      }

      function getIntegrityStatus(color) {
          if(color == 'olive'){
              return 'Very Low Priority';
          } else if(color == 'green'){
              return 'Low Priority';
          } else if(color == 'yellow'){
              return 'Medium Priority';
          } else if(color == 'orange'){
              return 'High Priority';
          } else {
              return 'Very High Priority';
          }
      }

      function calculateAssessment(ctr = -1) {
          if(gLastInspectionDate == null){
              return;
          }
          if(ctr == -1){
              var ctr = $(this).closest('.tab-pane').attr('tab-num');
          }
          if($('#leakage-to-atmosphere' + ctr).find('option:selected').length == 0) {
              return
          }
          var timePeriod = $('#time-period' + ctr).val();
          var assessmentDate = $('#assessment-date' + ctr).val();
          /* var lastInspectionDate = $('#lastInspectionDate' + ctr).val(); */
          var lastInspectionDate = $('.tab-pane[tab-num="'+ctr+'"]').find('#lastInspectionDate').val();
          var timeToLimitA = $('#time-to-limit-a' + ctr).val();
          var timeToLimitB = $('#time-to-limit-b' + ctr).val();
          var timeToLimitC = $('#time-to-limit-c' + ctr).val();
          var idImprobable = 1;
          var idDoubtful = 2;
          var idExpected = 3;
          var tp_limit_1 = timePeriod;
          var tp_limit_2 = timePeriod;
          var tp_limit_3 = timePeriod;
          var timeToLimitAint = timeToLimitA*1;
          var timeToLimitBint = timeToLimitB*1;
          var timeToLimitCint = timeToLimitC*1;
          // LF2 Leakage to Atmosphere Calculation
          var calculated_lf2LeakageToAtmosphereTP1 = idImprobable;
          var calculated_lf2LeakageToAtmosphereTP2 = idImprobable;
          var calculated_lf2LeakageToAtmosphereTP3 = idImprobable;
          // TP1
          if (timeToLimitAint >= 2 * tp_limit_1) {
              calculated_lf2LeakageToAtmosphereTP1 = idImprobable;
          } else if (timeToLimitAint > tp_limit_1 && timeToLimitAint < 2 * tp_limit_1) {
              calculated_lf2LeakageToAtmosphereTP1 = idDoubtful;
          } else {
              calculated_lf2LeakageToAtmosphereTP1 = idExpected;
          }
          // TP2
          var timeToLimitAintTP2 = timeToLimitAint - tp_limit_2;
          if (timeToLimitAintTP2 >= 2 * tp_limit_2) {
              calculated_lf2LeakageToAtmosphereTP2 = idImprobable;
          } else if (timeToLimitAintTP2 > tp_limit_2 && timeToLimitAintTP2 < 2 * tp_limit_2) {
              calculated_lf2LeakageToAtmosphereTP2 = idDoubtful;
          } else {
              calculated_lf2LeakageToAtmosphereTP2 = idExpected;
          }
          // TP3
          var timeToLimitAintTP3 = timeToLimitAintTP2 - tp_limit_3;
          if (timeToLimitAintTP3 >= 2 * tp_limit_3) {
              calculated_lf2LeakageToAtmosphereTP3 = idImprobable;
          } else if (timeToLimitAintTP3 > tp_limit_3 && timeToLimitAintTP3 < 2 * tp_limit_3) {
              calculated_lf2LeakageToAtmosphereTP3 = idDoubtful;
          } else {
              calculated_lf2LeakageToAtmosphereTP3 = idExpected;
          }
          // LF2 Failure of Function Calculation
          var calculated_lf2FailureOfFunctionTP1 = idImprobable;
          var calculated_lf2FailureOfFunctionTP2 = idImprobable;
          var calculated_lf2FailureOfFunctionTP3 = idImprobable;
          // TP1
          if (timeToLimitBint >= 2 * tp_limit_1) {
              calculated_lf2FailureOfFunctionTP1 = idImprobable;
          } else if (timeToLimitBint > tp_limit_1 && timeToLimitBint < 2 * tp_limit_1) {
              calculated_lf2FailureOfFunctionTP1 = idDoubtful;
          } else {
              calculated_lf2FailureOfFunctionTP1 = idExpected;
          }
          // TP2
          var timeToLimitBintTP2 = timeToLimitBint - tp_limit_2;
          if (timeToLimitBintTP2 >= 2 * tp_limit_2) {
              calculated_lf2FailureOfFunctionTP2 = idImprobable;
          } else if (timeToLimitBintTP2 > tp_limit_2 && timeToLimitBintTP2 < 2 * tp_limit_2) {
              calculated_lf2FailureOfFunctionTP2 = idDoubtful;
          } else {
              calculated_lf2FailureOfFunctionTP2 = idExpected;
          }
          // TP3
          var timeToLimitBintTP3 = timeToLimitBintTP2 - tp_limit_3;
          if (timeToLimitBintTP3 >= 2 * tp_limit_3) {
              calculated_lf2FailureOfFunctionTP3 = idImprobable;
          } else if (timeToLimitBintTP3 > tp_limit_3 && timeToLimitBintTP3 < 2 * tp_limit_3) {
              calculated_lf2FailureOfFunctionTP3 = idDoubtful;
          } else {
              calculated_lf2FailureOfFunctionTP3 = idExpected;
          }
          // LF2 Passing Accross Valve Calculation
          var calculated_lf2PassingAccrossValveTP1 = idImprobable;
          var calculated_lf2PassingAccrossValveTP2 = idImprobable;
          var calculated_lf2PassingAccrossValveTP3 = idImprobable;
          // TP1
          if (timeToLimitCint >= 2 * tp_limit_1) {
              calculated_lf2PassingAccrossValveTP1 = idImprobable;
          } else if (timeToLimitCint > tp_limit_1 && timeToLimitCint < 2 * tp_limit_1) {
              calculated_lf2PassingAccrossValveTP1 = idDoubtful;
          } else {
              calculated_lf2PassingAccrossValveTP1 = idExpected;
          }
          // TP2
          var timeToLimitCintTP2 = timeToLimitCint - tp_limit_2;
          if (timeToLimitCintTP2 >= 2 * tp_limit_2) {
              calculated_lf2PassingAccrossValveTP2 = idImprobable;
          } else if (timeToLimitCintTP2 > tp_limit_2 && timeToLimitCintTP2 < 2 * tp_limit_2) {
              calculated_lf2PassingAccrossValveTP2 = idDoubtful;
          } else {
              calculated_lf2PassingAccrossValveTP2 = idExpected;
          }
          // TP3
          var timeToLimitCintTP3 = timeToLimitCintTP2 - tp_limit_3;
          if (timeToLimitCintTP3 >= 2 * tp_limit_3) {
              calculated_lf2PassingAccrossValveTP3 = idImprobable;
          } else if (timeToLimitCintTP3 > tp_limit_3 && timeToLimitCintTP3 < 2 * tp_limit_3) {
              calculated_lf2PassingAccrossValveTP3 = idDoubtful;
          } else {
              calculated_lf2PassingAccrossValveTP3 = idExpected;
          }
          $('#leakage-tp1' + ctr).val(calculated_lf2LeakageToAtmosphereTP1);
          $('#leakage-tp2' + ctr).val(calculated_lf2LeakageToAtmosphereTP2);
          $('#leakage-tp3' + ctr).val(calculated_lf2LeakageToAtmosphereTP3);
          $('#failure-tp1' + ctr).val(calculated_lf2FailureOfFunctionTP1);
          $('#failure-tp2' + ctr).val(calculated_lf2FailureOfFunctionTP2);
          $('#failure-tp3' + ctr).val(calculated_lf2FailureOfFunctionTP3);
          $('#passing-tp1' + ctr).val(calculated_lf2PassingAccrossValveTP1);
          $('#passing-tp2' + ctr).val(calculated_lf2PassingAccrossValveTP2);
          $('#passing-tp3' + ctr).val(calculated_lf2PassingAccrossValveTP3);
          console.log('calculated', {
              calculated_lf2LeakageToAtmosphereTP1,
              calculated_lf2LeakageToAtmosphereTP2,
              calculated_lf2LeakageToAtmosphereTP3,
              calculated_lf2FailureOfFunctionTP1,
              calculated_lf2FailureOfFunctionTP2,
              calculated_lf2FailureOfFunctionTP3,
              calculated_lf2PassingAccrossValveTP1,
              calculated_lf2PassingAccrossValveTP2,
              calculated_lf2PassingAccrossValveTP3
          })
          if($('#leakage-to-atmosphere' + ctr).find('option:selected') == null) {
            return;
          }
          //LF1
          var lf1LeakageToAtmosphere = $('#leakage-to-atmosphere' + ctr).find('option:selected').attr('attr-value').replace(',','.');
          var lf1LeakageToAtmosphereWeighting = $('#leakage-to-atmosphere' + ctr).find('option:selected').attr('attr-weighting').replace(',','.');
          var lf1FailureOfFunction = $('#failure-of-function' + ctr).find('option:selected').attr('attr-value').replace(',','.');
          var lf1FailureOfFunctionWeighting = $('#failure-of-function' + ctr).find('option:selected').attr('attr-weighting').replace(',','.');
          var lf1PassingAccrossValve = $('#passing-accross-valve' + ctr).find('option:selected').attr('attr-value').replace(',','.');
          var lf1PassingAccrossValveWeighting = $('#passing-accross-valve' + ctr).find('option:selected').attr('attr-weighting').replace(',','.');
          //LF2
          var lf2LeakageToAtmosphereTP1 = $('#leakage-tp1' + ctr).find('option:selected').attr('attr-value').replace(',','.');
          var lf2LeakageToAtmosphereTP1Weighting = $('#leakage-tp1' + ctr).find('option:selected').attr('attr-weighting').replace(',','.');
          var lf2LeakageToAtmosphereTP1Value = $('#leakage-tp1' + ctr).find('option:selected').val();
          var lf2LeakageToAtmosphereTP2 = $('#leakage-tp2' + ctr).find('option:selected').attr('attr-value').replace(',','.');
          var lf2LeakageToAtmosphereTP2Weighting = $('#leakage-tp2' + ctr).find('option:selected').attr('attr-weighting').replace(',','.');
          var lf2LeakageToAtmosphereTP2Value = $('#leakage-tp2' + ctr).find('option:selected').val();
          var lf2LeakageToAtmosphereTP3 = $('#leakage-tp3' + ctr).find('option:selected').attr('attr-value').replace(',','.');
          var lf2LeakageToAtmosphereTP3Weighting = $('#leakage-tp3' + ctr).find('option:selected').attr('attr-weighting').replace(',','.');
          var lf2LeakageToAtmosphereTP3Value = $('#leakage-tp3' + ctr).find('option:selected').val();
          var lf2FailureOfFunctionTP1 = $('#failure-tp1' + ctr).find('option:selected').attr('attr-value').replace(',','.');
          var lf2FailureOfFunctionTP1Weighting = $('#failure-tp1' + ctr).find('option:selected').attr('attr-weighting').replace(',','.');
          var lf2FailureOfFunctionTP1Value = $('#failure-tp1' + ctr).find('option:selected').val();
          var lf2FailureOfFunctionTP2 = $('#failure-tp2' + ctr).find('option:selected').attr('attr-value').replace(',','.');
          var lf2FailureOfFunctionTP2Weighting = $('#failure-tp2' + ctr).find('option:selected').attr('attr-weighting').replace(',','.');
          var lf2FailureOfFunctionTP2Value = $('#failure-tp2' + ctr).find('option:selected').val();
          var lf2FailureOfFunctionTP3 = $('#failure-tp3' + ctr).find('option:selected').attr('attr-value').replace(',','.');
          var lf2FailureOfFunctionTP3Weighting = $('#failure-tp3' + ctr).find('option:selected').attr('attr-weighting').replace(',','.');
          var lf2FailureOfFunctionTP3Value = $('#failure-tp3' + ctr).find('option:selected').val();
          var lf2PassingAccrossValveTP1 = $('#passing-tp1' + ctr).find('option:selected').attr('attr-value').replace(',','.');
          var lf2PassingAccrossValveTP1Weighting = $('#passing-tp1' + ctr).find('option:selected').attr('attr-weighting').replace(',','.');
          var lf2PassingAccrossValveTP1Value = $('#passing-tp1' + ctr).find('option:selected').val();
          var lf2PassingAccrossValveTP2 = $('#passing-tp2' + ctr).find('option:selected').attr('attr-value').replace(',','.');
          var lf2PassingAccrossValveTP2Weighting = $('#passing-tp2' + ctr).find('option:selected').attr('attr-weighting').replace(',','.');
          var lf2PassingAccrossValveTP2Value = $('#passing-tp2' + ctr).find('option:selected').val();
          var lf2PassingAccrossValveTP3 = $('#passing-tp3' + ctr).find('option:selected').attr('attr-value').replace(',','.');
          var lf2PassingAccrossValveTP3Weighting = $('#passing-tp3' + ctr).find('option:selected').attr('attr-weighting').replace(',','.');
          var lf2PassingAccrossValveTP3Value = $('#passing-tp3' + ctr).find('option:selected').val();
          //LF3
          if($('#inspection-effectiveness' + ctr).find('option:selected').length == 0 || $('#inspection-effectiveness' + ctr).find('option:selected').val() == '') {
            return
          }
          var lf3InspectionEffectiveness = $('#inspection-effectiveness' + ctr).find('option:selected').attr('attr-value').replace(',','.');
          var lf3InspectionEffectivenessWeighting = $('#inspection-effectiveness' + ctr).find('option:selected').attr('attr-weighting').replace(',','.');
          //LF4
          if($('#impact-of-internal-fluid' + ctr).find('option:selected').length == 0 || $('#impact-of-internal-fluid' + ctr).find('option:selected').val() == '') {
            return
          }
          var lf4ImpactOfInternalFluid = $('#impact-of-internal-fluid' + ctr).find('option:selected').attr('attr-value').replace(',','.');
          var lf4ImpactOfInternalFluidWeighting = $('#impact-of-internal-fluid' + ctr).find('option:selected').attr('attr-weighting').replace(',','.');
          //LF5
          if($('#impact-of-operating-envelopes' + ctr).find('option:selected').length == 0 || $('#impact-of-operating-envelopes' + ctr).find('option:selected').val() == '') {
            return
          }
          var lf5ImpactOfOperatingEnvelopes = $('#impact-of-operating-envelopes' + ctr).find('option:selected').attr('attr-value').replace(',','.');
          var lf5ImpactOfOperatingEnvelopesWeighting = $('#impact-of-operating-envelopes' + ctr).find('option:selected').attr('attr-weighting').replace(',','.');
          //LF6
          if($('#used-within-oem-specification' + ctr).find('option:selected').length == 0 || $('#used-within-oem-specification' + ctr).find('option:selected').val() == '') {
            return
          }
          var lf6UsedWithinOEMSpecification = $('#used-within-oem-specification' + ctr).find('option:selected').attr('attr-value').replace(',','.');
          var lf6UsedWithinOEMSpecificationWeighting = $('#used-within-oem-specification' + ctr).find('option:selected').attr('attr-weighting').replace(',','.');
          //LF7
          if($('#repaired' + ctr).find('option:selected').length == 0 || $('#repaired' + ctr).find('option:selected').val() == '') {
            return
          }
          var lf7Repaired = $('#repaired' + ctr).find('option:selected').attr('attr-value').replace(',','.');
          var lf7RepairedWeighting = $('#repaired' + ctr).find('option:selected').attr('attr-weighting').replace(',','.');
          //CALCULATE
          // TP1A
          var lofLeakageToAtmosphereA = (lf2LeakageToAtmosphereTP1 * lf2LeakageToAtmosphereTP1Weighting) *
              (
                  (lf1LeakageToAtmosphere * lf1LeakageToAtmosphereWeighting) +
                  (lf3InspectionEffectiveness * lf3InspectionEffectivenessWeighting) +
                  (lf4ImpactOfInternalFluid * lf4ImpactOfInternalFluidWeighting) +
                  (lf5ImpactOfOperatingEnvelopes * lf5ImpactOfOperatingEnvelopesWeighting) +
                  (lf6UsedWithinOEMSpecification * lf6UsedWithinOEMSpecificationWeighting) +
                  (lf7Repaired * lf7RepairedWeighting)
              );
          console.log("=== START TP1A CALCULARTOR ===");
          console.log("lf2LeakageToAtmosphereTP1", lf2LeakageToAtmosphereTP1);
          console.log("lf2LeakageToAtmosphereTP1Weighting", lf2LeakageToAtmosphereTP1Weighting);
          console.log("LF2", lf2LeakageToAtmosphereTP1 * lf2LeakageToAtmosphereTP1Weighting);
          console.log("lf1LeakageToAtmosphere", lf1LeakageToAtmosphere);
          console.log("lf1LeakageToAtmosphereWeighting", lf1LeakageToAtmosphereWeighting);
          console.log("LF1", lf1LeakageToAtmosphere * lf1LeakageToAtmosphereWeighting);
          console.log("lf3InspectionEffectiveness", lf3InspectionEffectiveness);
          console.log("lf3InspectionEffectivenessWeighting", lf3InspectionEffectivenessWeighting);
          console.log("LF3", lf3InspectionEffectiveness * lf3InspectionEffectivenessWeighting);
          console.log("lf4ImpactOfInternalFluid", lf4ImpactOfInternalFluid);
          console.log("lf4ImpactOfInternalFluidWeighting", lf4ImpactOfInternalFluidWeighting);
          console.log("LF4", lf4ImpactOfInternalFluid * lf4ImpactOfInternalFluidWeighting);
          console.log("lf5ImpactOfOperatingEnvelopes", lf5ImpactOfOperatingEnvelopes);
          console.log("lf5ImpactOfOperatingEnvelopesWeighting", lf5ImpactOfOperatingEnvelopesWeighting);
          console.log("LF5", lf5ImpactOfOperatingEnvelopes * lf5ImpactOfOperatingEnvelopesWeighting);
          console.log("lf6UsedWithinOEMSpecification", lf6UsedWithinOEMSpecification);
          console.log("lf6UsedWithinOEMSpecificationWeighting", lf6UsedWithinOEMSpecificationWeighting);
          console.log("LF6", lf6UsedWithinOEMSpecification * lf6UsedWithinOEMSpecificationWeighting);
          console.log("lf7Repaired", lf7Repaired);
          console.log("lf7RepairedWeighting", lf7RepairedWeighting);
          console.log("LF7", lf7Repaired * lf7RepairedWeighting);
          console.log("calculator lf2 * (lf1 + lf3 + lf4 + lf5 + lf6 + lf7)", lofLeakageToAtmosphereA);
          console.log("=== END TP1A CALCULARTOR ===");
          var lofLeakageToAtmosphereAPos = calculateXPos(lofLeakageToAtmosphereA);
          // TP1B
          var lofLeakageToAtmosphereB = (lf2LeakageToAtmosphereTP2 * lf2LeakageToAtmosphereTP2Weighting) *
              (
                  (lf1LeakageToAtmosphere * lf1LeakageToAtmosphereWeighting) +
                  (lf3InspectionEffectiveness * lf3InspectionEffectivenessWeighting) +
                  (lf4ImpactOfInternalFluid * lf4ImpactOfInternalFluidWeighting) +
                  (lf5ImpactOfOperatingEnvelopes * lf5ImpactOfOperatingEnvelopesWeighting) +
                  (lf6UsedWithinOEMSpecification * lf6UsedWithinOEMSpecificationWeighting) +
                  (lf7Repaired * lf7RepairedWeighting)
              );
          var lofLeakageToAtmosphereBPos = calculateXPos(lofLeakageToAtmosphereB);
          // TP1C
          var lofLeakageToAtmosphereC = (lf2LeakageToAtmosphereTP3 * lf2LeakageToAtmosphereTP3Weighting) *
              (
                  (lf1LeakageToAtmosphere * lf1LeakageToAtmosphereWeighting) +
                  (lf3InspectionEffectiveness * lf3InspectionEffectivenessWeighting) +
                  (lf4ImpactOfInternalFluid * lf4ImpactOfInternalFluidWeighting) +
                  (lf5ImpactOfOperatingEnvelopes * lf5ImpactOfOperatingEnvelopesWeighting) +
                  (lf6UsedWithinOEMSpecification * lf6UsedWithinOEMSpecificationWeighting) +
                  (lf7Repaired * lf7RepairedWeighting)
              );
          var lofLeakageToAtmosphereCPos = calculateXPos(lofLeakageToAtmosphereC);
          // TP2A
          var lofFailureOfFunctionA = (lf2FailureOfFunctionTP1 * lf2FailureOfFunctionTP1Weighting) *
              (
                  (lf1FailureOfFunction * lf1FailureOfFunctionWeighting) +
                  (lf3InspectionEffectiveness * lf3InspectionEffectivenessWeighting) +
                  (lf4ImpactOfInternalFluid * lf4ImpactOfInternalFluidWeighting) +
                  (lf5ImpactOfOperatingEnvelopes * lf5ImpactOfOperatingEnvelopesWeighting) +
                  (lf6UsedWithinOEMSpecification * lf6UsedWithinOEMSpecificationWeighting) +
                  (lf7Repaired * lf7RepairedWeighting)
              );
          var lofFailureOfFunctionAPos = calculateXPos(lofFailureOfFunctionA);
          // TP2B
          var lofFailureOfFunctionB = (lf2FailureOfFunctionTP2 * lf2FailureOfFunctionTP2Weighting) *
              (
                  (lf1FailureOfFunction * lf1FailureOfFunctionWeighting) +
                  (lf3InspectionEffectiveness * lf3InspectionEffectivenessWeighting) +
                  (lf4ImpactOfInternalFluid * lf4ImpactOfInternalFluidWeighting) +
                  (lf5ImpactOfOperatingEnvelopes * lf5ImpactOfOperatingEnvelopesWeighting) +
                  (lf6UsedWithinOEMSpecification * lf6UsedWithinOEMSpecificationWeighting) +
                  (lf7Repaired * lf7RepairedWeighting)
              );
          var lofFailureOfFunctionBPos = calculateXPos(lofFailureOfFunctionB);
          // TP2C
          var lofFailureOfFunctionC = (lf2FailureOfFunctionTP3 * lf2FailureOfFunctionTP3Weighting) *
              (
                  (lf1FailureOfFunction * lf1FailureOfFunctionWeighting) +
                  (lf3InspectionEffectiveness * lf3InspectionEffectivenessWeighting) +
                  (lf4ImpactOfInternalFluid * lf4ImpactOfInternalFluidWeighting) +
                  (lf5ImpactOfOperatingEnvelopes * lf5ImpactOfOperatingEnvelopesWeighting) +
                  (lf6UsedWithinOEMSpecification * lf6UsedWithinOEMSpecificationWeighting) +
                  (lf7Repaired * lf7RepairedWeighting)
              );
          var lofFailureOfFunctionCPos = calculateXPos(lofFailureOfFunctionC);
          // TP3A
          var lofPassingAccrossValveA = (lf2PassingAccrossValveTP1 * lf2PassingAccrossValveTP1Weighting) *
              (
                  (lf1PassingAccrossValve * lf1PassingAccrossValveWeighting) +
                  (lf3InspectionEffectiveness * lf3InspectionEffectivenessWeighting) +
                  (lf4ImpactOfInternalFluid * lf4ImpactOfInternalFluidWeighting) +
                  (lf5ImpactOfOperatingEnvelopes * lf5ImpactOfOperatingEnvelopesWeighting) +
                  (lf6UsedWithinOEMSpecification * lf6UsedWithinOEMSpecificationWeighting) +
                  (lf7Repaired * lf7RepairedWeighting)
              );
          var lofPassingAccrossValveAPos = calculateXPos(lofPassingAccrossValveA);
          // TP3B
          var lofPassingAccrossValveB = (lf2PassingAccrossValveTP2 * lf2PassingAccrossValveTP2Weighting) *
              (
                  (lf1PassingAccrossValve * lf1PassingAccrossValveWeighting) +
                  (lf3InspectionEffectiveness * lf3InspectionEffectivenessWeighting) +
                  (lf4ImpactOfInternalFluid * lf4ImpactOfInternalFluidWeighting) +
                  (lf5ImpactOfOperatingEnvelopes * lf5ImpactOfOperatingEnvelopesWeighting) +
                  (lf6UsedWithinOEMSpecification * lf6UsedWithinOEMSpecificationWeighting) +
                  (lf7Repaired * lf7RepairedWeighting)
              );
          var lofPassingAccrossValveBPos = calculateXPos(lofPassingAccrossValveB);
          // TP3C
          var lofPassingAccrossValveC = (lf2PassingAccrossValveTP3 * lf2PassingAccrossValveTP3Weighting) *
              (
                  (lf1PassingAccrossValve * lf1PassingAccrossValveWeighting) +
                  (lf3InspectionEffectiveness * lf3InspectionEffectivenessWeighting) +
                  (lf4ImpactOfInternalFluid * lf4ImpactOfInternalFluidWeighting) +
                  (lf5ImpactOfOperatingEnvelopes * lf5ImpactOfOperatingEnvelopesWeighting) +
                  (lf6UsedWithinOEMSpecification * lf6UsedWithinOEMSpecificationWeighting) +
                  (lf7Repaired * lf7RepairedWeighting)
              );
          var lofPassingAccrossValveCPos = calculateXPos(lofPassingAccrossValveC);
          //HSSE Value
          var pld = $('#product-loss-definition' + ctr);
          if(pld.val() < 0 || pld.val() == ''){
              pld.val(0);
          }
          var bbls = Number(pld.val());
          var hsseSelect = $('#hsse-definition-bypld' + ctr);
          var hsseOptions = hsseSelect.find('option');
          var selectedHsse = '';
          hsseOptions.each(function(){
              var hsseValue = $(this).attr('attr-minvalue')*1;
              if(bbls >= hsseValue){
                  selectedHsse = $(this).val();
              }
          });
          hsseSelect.val(selectedHsse);
          var hsseSelectReal = $('#hsse-definition' + ctr);
          if(hsseSelectReal.find('option:selected').length == 0 || hsseSelectReal.find('option:selected').val() == '') {
              return
          }
          var cof1 = hsseSelectReal.find('option:selected').attr('attr-cofcategory');
          var cof2 = hsseSelect.find('option:selected').attr('attr-cofcategory');
          var cof = String.fromCharCode(Math.max(cof1.charCodeAt(0), cof2.charCodeAt(0)));
          var cofval = Math.max(hsseSelectReal.find('option:selected').attr('attr-score'), hsseSelect.find('option:selected').attr('attr-score'));
          var cof_rac = [];
          cof_rac['A'] = 3769
          cof_rac['B'] = 891
          cof_rac['C'] = 891
          cof_rac['D'] = 49
          cof_rac['E'] = 49
          var rac = cof_rac[cof];
          $('#consequenceOfFailure' + ctr).val(cof);
          var ypos = 0;
          switch(cof){
              case 'E':
                  ypos = 10;
                  break;
              case 'D':
                  ypos = 30;
                  break;
              case 'C':
                  ypos = 50;
                  break;
              case 'B':
                  ypos = 70;
                  break;
              case 'A':
                  ypos = 90;
                  break;
          }
          var tpos1a_lof = Math.floor((lofLeakageToAtmosphereAPos/20)+ 1);
          if(tpos1a_lof > 5){
              tpos1a_lof = 5;
          }
          var tpos1a = tpos1a_lof + cof;
          $('#tP1A' + ctr).val(tpos1a);
          var tpos1b_lof = Math.floor((lofLeakageToAtmosphereBPos/20)+ 1);
          if(tpos1b_lof > 5){
              tpos1b_lof = 5;
          }
          var tpos1b = tpos1b_lof + cof;
          $('#tP1B' + ctr).val(tpos1b);
          var tpos1c_lof = Math.floor((lofLeakageToAtmosphereCPos/20)+ 1);
          if(tpos1c_lof > 5){
              tpos1c_lof = 5;
          }
          var tpos1c = tpos1c_lof + cof;
          $('#tP1C' + ctr).val(tpos1c);
          var tpos2a_lof = Math.floor((lofFailureOfFunctionAPos/20)+ 1);
          if(tpos2a_lof > 5){
              tpos2a_lof = 5;
          }
          var tpos2a = tpos2a_lof + cof;
          $('#tP2A' + ctr).val(tpos2a);
          var tpos2b_lof = Math.floor((lofFailureOfFunctionBPos/20)+ 1);
          if(tpos2b_lof > 5){
              tpos2b_lof = 5;
          }
          var tpos2b = tpos2b_lof + cof;
          $('#tP2B' + ctr).val(tpos2b);
          var tpos2c_lof = Math.floor((lofFailureOfFunctionCPos/20)+ 1);
          if(tpos2c_lof > 5){
              tpos2c_lof = 5;
          }
          var tpos2c = tpos2c_lof + cof;
          $('#tP2C' + ctr).val(tpos2c);
          var tpos3a_lof = Math.floor((lofPassingAccrossValveAPos/20)+ 1);
          if(tpos3a_lof > 5){
              tpos3a_lof = 5;
          }
          var tpos3a = tpos3a_lof + cof;
          $('#tP3A' + ctr).val(tpos3a);
          var tpos3b_lof = Math.floor((lofPassingAccrossValveBPos/20)+ 1);
          if(tpos3b_lof > 5){
              tpos3b_lof = 5;
          }
          var tpos3b = tpos3b_lof + cof;
          $('#tP3B' + ctr).val(tpos3b);
          var tpos3c_lof = Math.floor((lofPassingAccrossValveCPos/20)+ 1);
          if(tpos3c_lof > 5){
              tpos3c_lof = 5;
          }
          var tpos3c = tpos3c_lof + cof;
          $('#tP3C' + ctr).val(tpos3c);
          // TOTAL
          var map_result = {
              'TP1A': {
                  'lof': Number(lofLeakageToAtmosphereA),
                  'cof': cof,
                  'xpos': Number(lofLeakageToAtmosphereAPos),
                  'ypos': Number(ypos),
                  'label': tpos1a,
                  'color': getHeatColor(tpos1a)
              },
              'TP2A': {
                  'lof': Number(lofLeakageToAtmosphereB),
                  'cof': cof,
                  'xpos': Number(lofLeakageToAtmosphereBPos),
                  'ypos': Number(ypos),
                  'label': tpos1b,
                  'color': getHeatColor(tpos1b)
              },
              'TP3A': {
                  'lof': Number(lofLeakageToAtmosphereC),
                  'cof': cof,
                  'xpos': Number(lofLeakageToAtmosphereCPos),
                  'ypos': Number(ypos),
                  'label': tpos1c,
                  'color': getHeatColor(tpos1c)
              },
              'TP1B': {
                  'lof': Number(lofFailureOfFunctionA),
                  'cof': cof,
                  'xpos': Number(lofFailureOfFunctionAPos),
                  'ypos': Number(ypos),
                  'label': tpos2a,
                  'color': getHeatColor(tpos2a)
              },
              'TP2B': {
                  'lof': Number(lofFailureOfFunctionB),
                  'cof': cof,
                  'xpos': Number(lofFailureOfFunctionBPos),
                  'ypos': Number(ypos),
                  'label': tpos2b,
                  'color': getHeatColor(tpos2b)
              },
              'TP3B': {
                  'lof': Number(lofFailureOfFunctionC),
                  'cof': cof,
                  'xpos': Number(lofFailureOfFunctionCPos),
                  'ypos': Number(ypos),
                  'label': tpos2c,
                  'color': getHeatColor(tpos2c)
              },
              'TP1C': {
                  'lof': Number(lofPassingAccrossValveA),
                  'cof': cof,
                  'xpos': Number(lofPassingAccrossValveAPos),
                  'ypos': Number(ypos),
                  'label': tpos3a,
                  'color': getHeatColor(tpos3a)
              },
              'TP2C': {
                  'lof': Number(lofPassingAccrossValveB),
                  'cof': cof,
                  'xpos': Number(lofPassingAccrossValveBPos),
                  'ypos': Number(ypos),
                  'label': tpos3b,
                  'color': getHeatColor(tpos3b)
              },
              'TP3C': {
                  'lof': Number(lofPassingAccrossValveC),
                  'cof': cof,
                  'xpos': Number(lofPassingAccrossValveCPos),
                  'ypos': Number(ypos),
                  'label': tpos3c,
                  'color': getHeatColor(tpos3c)
              }
          }
          console.log(map_result);

          //apply to matrix
          for (const [key, item] of Object.entries(map_result)) {
              const dot = $('.tab-pane[tab-num='+ctr+']').find('#'+key)
              dot.css({
                  top: item.ypos+'%',
                  left: item.xpos+'%'
              })

              const dotLabel = $('.tab-pane[tab-num='+ctr+']').find('['+key+'-label]')
              dotLabel.text(item.label)

              const dotColorLabel = $('.tab-pane[tab-num='+ctr+']').find('['+key+'-colorlabel]')
              dotColorLabel.removeClass()
              dotColorLabel.addClass('status-box')
              dotColorLabel.addClass(item.color)

              $('.tab-pane[tab-num='+ctr+']').find('#'+key+'-field').val(item.label)
              $('.tab-pane[tab-num='+ctr+']').find('#'+key+'-value').val(item.lof)
              $('.tab-pane[tab-num='+ctr+']').find('#COF-value').val(cofval)
          }
          // set cof
          $('.tab-pane[tab-num='+ctr+']').find('#consequenceOfFailure').val(cof)

          // Risk
          var tp1risk = Math.max(tpos1a_lof, tpos2a_lof, tpos3a_lof) + cof;
          var tp2risk = Math.max(tpos1b_lof, tpos2b_lof, tpos3b_lof) + cof;
          var tp3risk = Math.max(tpos1c_lof, tpos2c_lof, tpos3c_lof) + cof;
          var map_risk = {
              'TP1RISK': {
                  'label' : tp1risk,
                  'color' : getHeatColor(tp1risk)
              },
              'TP2RISK': {
                  'label' : tp2risk,
                  'color' : getHeatColor(tp2risk)
              },
              'TP3RISK': {
                  'label' : tp3risk,
                  'color' : getHeatColor(tp3risk)
              }
          }
          console.log(map_risk, 'map_risk');

          var tp1color = '';
          // apply to matrix map_risk
          for (const [key, item] of Object.entries(map_risk)) {
              const dotLabel = $('.tab-pane[tab-num='+ctr+']').find('['+key+'-label]')
              dotLabel.text(item.label)

              const dotColorLabel = $('.tab-pane[tab-num='+ctr+']').find('['+key+'-colorlabel]')
              dotColorLabel.removeClass()
              dotColorLabel.addClass('status-box')
              dotColorLabel.addClass(item.color)
              if(key == 'TP1RISK'){
                  tp1color = item.color;
              }

              $('.tab-pane[tab-num='+ctr+']').find('#'+key+'-field').val(item.label)
          }
          var currentIntegrityStatus = $('.tab-pane[tab-num='+ctr+']').find('#integrity-status'+ctr);
          console.log("AYAYA DEBUG", $(currentIntegrityStatus).attr('x-fromdb'));
          if(currentIntegrityStatus.attr('x-fromdb') != 'true'){
            $('.tab-pane[tab-num='+ctr+']').find('#integrity-status'+ctr).val(getIntegrityStatus(tp1color));
          }

          //TTL NEW
          var timelimittp1 = timePeriod*1;
          var timelimittp2 = timePeriod*2;
          var timelimittp3 = timePeriod*3;
          var loftp1a = lofLeakageToAtmosphereA;
          var loftp1b = lofFailureOfFunctionA;
          var loftp1c = lofPassingAccrossValveA;
          var loftp2a = lofLeakageToAtmosphereB;
          var loftp2b = lofFailureOfFunctionB;
          var loftp2c = lofPassingAccrossValveB;
          var loftp3a = lofLeakageToAtmosphereC;
          var loftp3b = lofFailureOfFunctionC;
          var loftp3c = lofPassingAccrossValveC;
          var loftp1aval = lf2LeakageToAtmosphereTP1Value;
          var loftp1bval = lf2FailureOfFunctionTP1Value;
          var loftp1cval = lf2PassingAccrossValveTP1Value;
          var loftp2aval = lf2LeakageToAtmosphereTP2Value;
          var loftp2bval = lf2FailureOfFunctionTP2Value;
          var loftp2cval = lf2PassingAccrossValveTP2Value;
          var loftp3aval = lf2LeakageToAtmosphereTP3Value;
          var loftp3bval = lf2FailureOfFunctionTP3Value;
          var loftp3cval = lf2PassingAccrossValveTP3Value;
          /* var tta_year = decideTTARAC(loftp1a, loftp2a, loftp3a, timelimittp1, timelimittp2, timelimittp3, rac, loftp1aval, loftp2aval, loftp3aval); */
          var tta_month = decideTTARAC(loftp1a, loftp2a, loftp3a, timelimittp1, timelimittp2, timelimittp3, rac, loftp1aval, loftp2aval, loftp3aval);
          /* var tta_days = tta_year * 365.25; */
          /* var ttb_year = decideTTARAC(loftp1b, loftp2b, loftp3b, timelimittp1, timelimittp2, timelimittp3, rac, loftp1bval, loftp2bval, loftp3bval); */
          var ttb_month = decideTTARAC(loftp1b, loftp2b, loftp3b, timelimittp1, timelimittp2, timelimittp3, rac, loftp1bval, loftp2bval, loftp3bval);
          /* var ttb_days = ttb_year * 365.25; */
          /* var ttc_year = decideTTARAC(loftp1c, loftp2c, loftp3c, timelimittp1, timelimittp2, timelimittp3, rac, loftp1cval, loftp2cval, loftp3cval); */
          var ttc_month = decideTTARAC(loftp1c, loftp2c, loftp3c, timelimittp1, timelimittp2, timelimittp3, rac, loftp1cval, loftp2cval, loftp3cval);
          /* var ttc_days = ttc_year * 365.25; */
          var tta_date = new Date(createDateFromString(gLastInspectionDate));
          tta_date.setMonth(tta_date.getMonth() + tta_month);
          var ttb_date = new Date(createDateFromString(gLastInspectionDate));
          ttb_date.setMonth(ttb_date.getMonth() + ttb_month);
          var ttc_date = new Date(createDateFromString(gLastInspectionDate));
          ttc_date.setMonth(ttc_date.getMonth() + ttc_month);
          var map_ttl = {
              'TPA-TTA': formatDate(tta_date),
              'TPB-TTA': formatDate(ttb_date),
              'TPC-TTA': formatDate(ttc_date),
              'TPRISK-TTA': formatDate(new Date(Math.min(tta_date.getTime(), ttb_date.getTime(), ttc_date.getTime()))),
          };
          console.log(map_ttl, 'map_tt');
          // apply
          for (const [key, item] of Object.entries(map_ttl)) {
              const dotLabel = $('.tab-pane[tab-num='+ctr+']').find('['+key+']')
              dotLabel.text(item)
              $('.tab-pane[tab-num='+ctr+']').find('#'+key+'-field').val(item)
          }
          var currentTTA = $('.tab-pane[tab-num='+ctr+']').find('#time-to-action'+ctr);
          console.log("AYAYA DEBUG X", $(currentTTA).attr('x-fromdb'));
          if(currentTTA.attr('x-fromdb') != 'true'){
            $('.tab-pane[tab-num='+ctr+']').find('#time-to-action'+ctr).val(map_ttl['TPRISK-TTA']);
          }
      }

      function decideTTARAC(loftp1, loftp2, loftp3, timelimittp1, timelimittp2, timelimittp3, rac, loftp1val, loftp2val, loftp3val){
          console.log("==START DECIDE TTL==");
          console.log("loftp1: "+loftp1);
          console.log("loftp2: "+loftp2);
          console.log("loftp3: "+loftp3);
          console.log("loftp1val: "+loftp1val);
          console.log("loftp2val: "+loftp2val);
          console.log("loftp3val: "+loftp3val);
          console.log("timelimittp1: "+timelimittp1);
          console.log("timelimittp2: "+timelimittp2);
          console.log("timelimittp3: "+timelimittp3);
          console.log("rac: "+rac);
          console.log("==END DECIDE TTL==");
          var calculate = 0;
          if(loftp1 > rac) {
              calculate = 0;
          }
          else {
              if (loftp3 > rac && loftp2 <= rac){
                  /* calculate = 0;((((timelimittp3-timelimittp2)*(rac-loftp2))/(loftp3-loftp2))/12) + (timelimittp2/12); */
                  /* 54-36 * 49-29 /290-29 + */
                  calculate = (timelimittp3-timelimittp2)/12 * (rac-loftp2) / (loftp3-loftp2) + (timelimittp2/12);
                  calculate = Math.floor(calculate*12)
              }
              else if(loftp2 > rac){
                  console.log("loftp2 > rac");
                  console.log("timelimittp2: "+timelimittp2);
                  console.log("timelimittp1: "+timelimittp1);
                  console.log("rac: "+rac);
                  console.log("loftp1: "+loftp1);
                  console.log("loftp2: "+loftp2);
                  calculate = (timelimittp2-timelimittp1)/12 * (rac-loftp1) / (loftp2-loftp1) + (timelimittp1/12);
                  calculate = Math.floor(calculate*12)
                  /* calculate = ((((timelimittp2-timelimittp1)*(rac-loftp1))/(loftp2val-loftp1val))/12) + (timelimittp1/12); */
              }
              else{
                  // LF3 && LF2 
                  calculate = decideTTL(loftp1val, loftp2val, loftp3val, timelimittp1, timelimittp2, timelimittp3);
              }
          }
          console.log("calculate: "+calculate);
          console.log("==END DECIDE TTL==");
          return calculate;
      }

      function decideTTL(tp1, tp2, tp3, tp1val, tp2val, tp3val){
          if(tp1 == 1 && tp2 == 1 && tp3 == 1) {
              return tp3val;
          } else if(tp1 == 1 && tp2 == 1 && tp3 == 2) {
              return tp3val;
          } else if(tp1 == 1 && tp2 == 2 && tp3 == 2) {
              return tp2val;
          } else if(tp1 == 2 && tp2 == 2 && tp3 == 2) {
              return (tp2val + tp3val)/2;
          } else if(tp1 == 2 && tp2 == 2 && tp3 == 3) {
              return (tp1val + tp2val)/2;
          } else if(tp1 == 2 && tp2 == 3 && tp3 == 3) {
              return tp1val;
          } else {
              return 0;
          }
      }

      function createDateFromString(dateString) {
          var parts = dateString.split("-");
          return new Date(parts[2], parts[1] - 1, parts[0]);
      }

      let template_inspection = $('#template_inspection');
      let template_inspection_html = template_inspection.html();
      template_inspection.remove();
      let template_inspection_header = $('#template_inspection_header');
      let template_inspection_header_html = template_inspection_header.html();
      template_inspection_header.remove();

      let idx = 0;
      function addTab() {
          let ctr = idx;

          let id = $(this).attr('attr-id');
          const equipmentId = id
          let isHistory = false
          let name = $(this).attr('attr-name');
          let tabInspectionId = 0
          if($(this).attr('attr-inspection-id')) {
              isHistory = true
              id = $(this).attr('attr-inspection-id')
              tabInspectionId = id
          } else {
              name = `New Assessment ${name}`
          }
          if (tabInspectionId > 0) {
              if ($('.tab-pane[tab-inspection-id="' + tabInspectionId + '"]').length > 0) {
                  alert('This assessment already exist in the tab');
                  return;
              }
          }

          let tabheadercontainer = $('#inspectiontabheader');
          let tabcontentcontainer = $('#tabcontentcontainer');
          tabheadercontainer.append('<li id="inspectionheader' + ctr + '"><a href="#inspection' + ctr + '" data-bs-toggle="tab" class="nav-link" data-bs-target="#inspection'+ctr+'" type="button" role="tab" tab-num="'+ctr+'"><div tab-title>' + name + '</div><button type="button" class="btn-close-inspection"><i class="fa-solid fa-xmark"></i></button></a></li>');
          tabcontentcontainer.append('<div class="tab-pane" tab-inspection-id="'+ tabInspectionId +'" equipment-id="'+equipmentId+'" id="inspection' + ctr + '" tab-num="'+ctr+'">' + template_inspection_html + '</div>');
          let tabcontent = tabcontentcontainer.find('.form-control,.form-select');
          tabcontent.each(function () {
              let name = $(this).attr('id') + ctr;
              $(this).attr('id', name);
          });
          //find button submit then add ctr on id
          let submitbtn = tabcontentcontainer.find('.form-submit');
          submitbtn.each(function () {
              let name = $(this).attr('id') + ctr;
              $(this).attr('id', name);
          })

          //update the label also
          let tabcontentlabel = tabcontentcontainer.find('label');
          tabcontentlabel.each(function () {
              let label = $(this).attr('for') + ctr;
              $(this).attr('for', label);
          });

          //other
          let recordinfo = tabcontentcontainer.find('.recordinfo');
          recordinfo.each(function () {
              let name = $(this).attr('id') + ctr;
              $(this).attr('id', name);
          });
          let recordmeta = tabcontentcontainer.find('.recordmeta');
          recordmeta.each(function () {
              let name = $(this).attr('id') + ctr;
              $(this).attr('id', name);
          });

          $('#inspectionheader' + ctr).find('a').click();
          initDatatable({
              scrollable: false,
              selector: `#inspection${ctr} .datatable`
          })
          isHistory ? fetchHistoryData({
              id,
              counter: ctr
          }) : fetchAssetData({
              id,
              counter: ctr
          })

          $('.addInspectionData').click(function(){
              var disabled = $(this).attr('disabled');
              if(!disabled){
                  $(this).attr('disabled', true)
                  addInspection($(this))
              }
          })

          $('.addMaintenanceData').click(function(){
              var disabled = $(this).attr('disabled');
              if(!disabled){
                  $(this).attr('disabled', true)
                  addMaintenance($(this))
              }
          })

          $('.integrity-status').on('focusout', function(){
              $(this).attr('x-fromdb', 'true')
          })

          $('.time-to-action').on('focusout', function(){
            $(this).attr('x-fromdb', 'true');
          });

          initDatepicker()
          moveToLastTab()
          idx++;
      }

      function fetchInspectionHistory(assetId, tabNum, selectedId = []) {
          return new Promise((res, rej) => {
              const modalId = '#addInspectionModal'
              const tableSelector = modalId+' .datatable'
              $(tableSelector).DataTable().destroy()
              const table = $(tableSelector+ ' tbody')
              table.html('')
              $.ajax({
                  url: urlassessmentinspection,
                  data: {
                      AssetID: assetId
                  },
                  headers: {
                      '__RequestVerificationToken': requestVerificationToken
                  },
                  type: 'GET',
                  success: function (apiresult) {
                      console.log(apiresult);
                      if(apiresult.isSuccess == true){
                          var data = apiresult.data;
                          let html = ''
                          data.forEach((item, idx) => {
                              html += `
                              <tr>
                                  <td>${idx+1}</td>
                                  <td>${item.inspectionDate}</td>
                                  <td>${item.inspectionMethod}</td>
                                  <td>${item.inspectionDescription}</td>
                                  <td>${item.functionCondition}</td>
                                  <td>${item.testPressureIfAny}</td>
                                  <td>
                                      <div class="inspection-maintenance-select">
                                          <input id="addInspectionRow${item.id}" ${selectedId.includes(String(item.id)) ? 'checked' : ''} type="checkbox" name="selectedId" value="${item.id}">
                                          <!-- <label for="addInspectionRow${item.id}" class="btn btn-light btn-sm">Select</label> -->
                                      </div>
                                  </td>
                              </tr>
                              `
                          })
                          $(modalId+' input[name=tempData]').val(JSON.stringify(data))
                          $(modalId+' input[name=tabNum]').val(tabNum)
                          table.append(html)
                          res(true)
                      } else {
                          alert(apiresult.message)
                          rej()
                      }
                  },
                  error: function (error) {
                      rej()
                  }
              });
          })
      }

      function fetchMaintenanceHistory(assetId, tabNum, selectedId = []) {
          return new Promise((res, ref) => {
              const modalId = '#addMaintenanceModal'
              const tableSelector = modalId+' .datatable'
              $(tableSelector).DataTable().destroy()
              const table = $(tableSelector+ ' tbody')
              table.html('')
              $.ajax({
                  url: urlassessmentmaintenance,
                  data: {
                      AssetID: assetId
                  },
                  headers: {
                      '__RequestVerificationToken': requestVerificationToken
                  },
                  type: 'GET',
                  success: function (apiresult) {
                      console.log(apiresult);
                      if(apiresult.isSuccess == true){
                          var data = apiresult.data;
                          let html = ''
                          data.forEach((item, idx) => {
                              html += `
                              <tr>
                                  <td>${idx+1}</td>
                                  <td>${item.maintenanceDate}</td>
                                  <td>${item.isValveRepaired}</td>
                                  <td>${item.maintenanceDescription}</td>
                                  <td>
                                      <div class="inspection-maintenance-select">
                                          <input id="addInspectionRow${item.id}" ${selectedId.includes(String(item.id)) ? 'checked' : ''} type="checkbox" name="selectedId" value="${item.id}">
                                          <!-- <label for="addInspectionRow${item.id}" class="btn btn-light btn-sm">Select</label> -->
                                      </div>
                                  </td>
                              </tr>
                              `
                          })
                          $(modalId+' input[name=tempData]').val(JSON.stringify(data))
                          $(modalId+' input[name=tabNum]').val(tabNum)
                          table.append(html)
                          res(true)
                      } else {
                          alert(apiresult.message)
                          ref()
                      }
                  },
                  error: function (error) {
                      ref()
                  }
              })
          });
      }
  
  $( "#addInspectionModal" ).on('shown.bs.modal', function(){
      // refreshDatatableColumn()
  });
  
  const changeContentCard = (key, el) => {
      const form = $(el).closest('form')
      form.find('.assessment-content-card').removeClass('active');
      form.find('.assessment-content-card.card-group-'+key).addClass('active')
      $(el).closest('.assessment-tab-content-submenu').find('.submenu-item').removeClass('active')
      $(el).addClass('active')
      var ctr = $(el).closest('.tab-pane').attr('tab-num');
      calculateAssessment(ctr)
  }


      /* MODAL CODE */
      function submitInspectionForm(el) {
          const form = $(el).closest('form')
          // const equipmentId = $(el).closest('.tab-pane').attr('equipment-id')
          const ctr = $(el).closest('.tab-pane').attr('tab-num')
          const equipmentId = form.find('#field-inspection-assetid').val()
          
      }

      function submitMaintenanceForm(el) {
          const form = $(el).closest('form')
          // const equipmentId = $(el).closest('.tab-pane').attr('equipment-id')
          const ctr = $(el).closest('.tab-pane').attr('tab-num')
          const equipmentId = form.find('#field-inspection-assetid').val()
          
      }

      function addInspection(el) {
          const form = $(el).closest('form')
          const nativeForm = form[0]
          const formData = new FormData(nativeForm)
          const selectedId = formData.getAll('selectedInspectionId')
          const equipmentId = $(el).closest('.tab-pane').attr('equipment-id')
          const ctr = $(el).closest('.tab-pane').attr('tab-num')

          fetchInspectionHistory(equipmentId,ctr,selectedId).then(() => {
              initDatatable({
                  scrollable: false,
                  paging: false,
                  selector: `#addInspectionModal .datatable`,
                  scrollable: true
              })
              $('#addInspectionModal').modal('show')
              setTimeout(() => {
                  refreshDatatableColumn()
                  $(el).removeAttr('disabled')
              }, 180);
          })
      }

      function confirmInspection(el) {
          const form = $(el).closest('form')
          const nativeForm = form[0]
          const formData = new FormData(nativeForm)
          const selectedId = formData.getAll('selectedId')
          const tempData = JSON.parse(formData.get('tempData'))

          const tabNum = formData.get('tabNum')
          const selectedRows = tempData.filter ( ({id}) => {
              return selectedId.includes(String(id))
          })
          $('.tab-pane[tab-num='+tabNum+'] .inspection-data-temp').val(JSON.stringify(selectedRows))

          fillInspectionTable(tabNum, selectedRows)
          $('#addInspectionModal').modal('hide')
          refreshDatatableColumn()
      }

      function deleteInspection(el, id) {
          if(confirm('remove inspection?')) {
              const tabNum = $(el).closest('.tab-pane').attr('tab-num')
              const tempDataEl = $(el).closest('.card-content').find('.inspection-data-temp')
              const tempData = JSON.parse(tempDataEl.val()) || []
              const newData = tempData.filter(item => Number(item.id) !== id)
              tempDataEl.val(JSON.stringify(newData))
              fillInspectionTable(tabNum, newData)
          }
      }

      function fillInspectionTable(tabNum, data) {
          const tableSelector = '.tab-pane[tab-num='+tabNum+'] .inspection-table'
          $(tableSelector).DataTable().destroy()
          const table = $(tableSelector+ ' tbody')
          table.html('')

          let html = ''
          data.forEach((item, idx) => {
              html += `
              <tr>
                  <td>${idx+1}</td>
                  <td>${item.inspectionDate}</td>
                  <td>${item.inspectionMethod}</td>
                  <td>${item.inspectionDescription}</td>
                  <td>${item.functionCondition}</td>
                  <td>${item.testPressureIfAny}</td>
                  <td>
                      <input type="hidden" name="selectedInspectionId" value="${item.id}" />
                      <a class="btn btn-light btn-sm" onclick="detailInspection(${item.id})">Detail</a>
                      <a class="btn btn-light btn-sm only-add" onclick="deleteInspection(this, ${item.id})">Delete</a>
                  </td>
              </tr>
              `
          })
          table.append(html)

          // fill LF1 and LF3
          const latestData = data.sort((a, b) => {
              const dateA = a.inspectionDate.split('-').reverse().join('-');
              const dateB = b.inspectionDate.split('-').reverse().join('-');
              return dateB.localeCompare(dateA);
          })[0] || {};
          console.log(latestData, 'latestData');
          gLastInspectionDate = latestData.inspectionDate
          $('#leakage-to-atmosphere'+tabNum).val(latestData.currentConditionLeakeageToAtmosphereID)
          $('#failure-of-function'+tabNum).val(latestData.currentConditionFailureOfFunctionID)
          $('#passing-accross-valve'+tabNum).val(latestData.currentConditionPassingAcrossValveID)
          $('#inspection-effectiveness'+tabNum).val(latestData.inspectionEffectivenessID)
          initDatatable({
              scrollable: false,
              selector: tableSelector
          })

          calculateAssessment(tabNum)
      }

      function addMaintenance(el) {
          const form = $(el).closest('form')
          const nativeForm = form[0]
          const formData = new FormData(nativeForm)
          const selectedId = formData.getAll('selectedMaintenanceId')
          const equipmentId = $(el).closest('.tab-pane').attr('equipment-id')
          const ctr = $(el).closest('.tab-pane').attr('tab-num')

          fetchMaintenanceHistory(equipmentId,ctr,selectedId).then(() => {
              initDatatable({
                  scrollable: false,
                  paging: false,
                  selector: `#addMaintenanceModal .datatable`,
                  scrollable: true
              })
              $('#addMaintenanceModal').modal('show')
              setTimeout(() => {
                  refreshDatatableColumn()
                  $(el).removeAttr('disabled')
              }, 180);
          })
      }

      function deleteMaintenance(el, id) {
          if(confirm('remove maintenance?')) {
              const tabNum = $(el).closest('.tab-pane').attr('tab-num')
              const tempDataEl = $(el).closest('.card-content').find('.maintenance-data-temp')
              const tempData = JSON.parse(tempDataEl.val()) || []
              const newData = tempData.filter(item => Number(item.id) !== id)
              tempDataEl.val(JSON.stringify(newData))
              fillMaintenanceTable(tabNum, newData)
          }
      }

      function confirmMaintenance(el) {
          const form = $(el).closest('form')
          const nativeForm = form[0]
          const formData = new FormData(nativeForm)
          const selectedId = formData.getAll('selectedId')
          const tempData = JSON.parse(formData.get('tempData'))
          const tabNum = formData.get('tabNum')
          const selectedRows = tempData.filter ( ({id}) => {
              return selectedId.includes(String(id))
          })
          $('.tab-pane[tab-num='+tabNum+'] .maintenance-data-temp').val(JSON.stringify(selectedRows))
          fillMaintenanceTable(tabNum, selectedRows)
          $('#addMaintenanceModal').modal('hide')
          refreshDatatableColumn()
      }

      function fillMaintenanceTable(tabNum, data) {
          const tableSelector = '.tab-pane[tab-num='+tabNum+'] .maintenance-table'
          $(tableSelector).DataTable().destroy()
          const table = $(tableSelector+ ' tbody')
          table.html('')

          let html = ''
          data.forEach((item, idx) => {
              html += `
              <tr>
                  <td>${idx+1}</td>
                  <td>${item.maintenanceDate}</td>
                  <td>${item.isValveRepaired}</td>
                  <td>${item.maintenanceDescription}</td>
                  <td>
                      <input type="hidden" name="selectedMaintenanceId" value="${item.id}" />
                      <a class="btn btn-light btn-sm" onclick="detailMaintenance(${item.id})">Detail</a>
                      <a class="btn btn-light btn-sm only-add" onclick="deleteMaintenance(this, ${item.id})">Delete</a>
                  </td>
              </tr>
              `
          })
          table.append(html)
          initDatatable({
              scrollable: false,
              selector: tableSelector
          })
      }

      function detailInspection(id) {
          $('#detailInspectionModal').find('input,textarea,select').val('')
          $('#detailInspectionModal .preview-image-gallery').find('input[type=file], .single-image').remove()
          fetchModalDetailData(id, 'inspection')

          $('#detailInspectionModal').attr('data-type', 'view')
          $('#detailInspectionModal').modal('show')
      }

      function detailMaintenance(id) {
          $('#detailMaintenanceModal').find('input,textarea,select').val('')
          $('#detailMaintenanceModal .preview-image-gallery').find('input[type=file], .single-image').remove()
          fetchModalDetailData(id, 'maintenance')

          $('#detailMaintenanceModal').attr('data-type', 'view')
          $('#detailMaintenanceModal').modal('show')
      }

      function fetchModalDetailData (id, type) {
      // type = maintenance or inspection
      let endPoint = urlinspectiondetail
      let modalSelector = '#detailInspectionModal'
      if (type === 'maintenance') {
          modalSelector = '#detailMaintenanceModal'
          endPoint = urlmaintenancedetail
      }
      $.ajax({
          url: endPoint,
          type: 'GET',
          data: { id },
          headers: {
              '__RequestVerificationToken': requestVerificationToken
          },
          success: function (apiresult) {
              if(apiresult.isSuccess){
                  var data = apiresult.data;
                  // $('#field-inspection-valvetagno' + counter).val(data.tagNo);
                  const dataAsset = data.asset
                  $(modalSelector+' #field-inspection-assetid').val(dataAsset.id)
                  $(modalSelector+' #field-inspection-valvetype').val(dataAsset.valveType)
                  $(modalSelector+' #field-inspection-tagno').val(dataAsset.tagNo)
                  $(modalSelector+' #field-inspection-valvetagno').val(dataAsset.tagNo)
                  $(modalSelector+' #field-inspection-manufacturer').val(dataAsset.manufacturer)
                  $(modalSelector+' #field-inspection-equipmentname').val('')
                  $(modalSelector+' #field-inspection-bodymaterial').val(dataAsset.bodyMaterial)
                  $(modalSelector+' #businessarea').val(dataAsset.businessArea)
                  $(modalSelector+' #bodymodel').val(dataAsset.bodyModel)
                  $(modalSelector+' #platform').val(dataAsset.platform)
                  $(modalSelector+' #endconnection').val(dataAsset.endConnection)
                  $(modalSelector+' #parentequipmentno').val(dataAsset.parentEquipmentNo)
                  $(modalSelector+' #serialno').val(dataAsset.serialNo)
                  $(modalSelector+' #parentequipmentdescription').val(dataAsset.parentEquipmentDescription)
                  $(modalSelector+' #usagetype').val('')
                  $(modalSelector+' #installationdate').val(dataAsset.installationDate)
                  $(modalSelector+' #size').val(dataAsset.size)
                  $(modalSelector+' #pidno').val(dataAsset.pidNo)
                  $(modalSelector+' #classrating').val(dataAsset.classRating)

                  // fill history data
                  if(type === 'maintenance') {
                      $(modalSelector + ' #field-maintenance-date').val(data.maintenanceDate)
                      $(modalSelector + ' #field-maintenance-repaired').val(data.isValveRepairedID)
                      $(modalSelector + ' #field-maintenance-description').val(data.maintenanceDescription)
                  } else {
                      $(modalSelector + ' #field-inspection-date').val(data.inspectionDate)
                      $(modalSelector + ' #field-inspection-inspection-method').val(data.inspectionMethodID)
                      $(modalSelector + ' #field-inspection-current-condition-limit-state-a').val(data.currentConditionLeakeageToAtmosphereID)
                      $(modalSelector + ' #field-inspection-current-condition-limit-state-b').val(data.currentConditionFailureOfFunctionID)
                      $(modalSelector + ' #field-inspection-current-condition-limit-state-c').val(data.currentConditionPassingAcrossValveID)
                      $(modalSelector + ' #field-inspection-effectiveness').val(data.inspectionEffectivenessID)
                      $(modalSelector + ' #field-inspection-function-condition').val(data.functionCondition)
                      $(modalSelector + ' #field-inspection-description').val(data.inspectionDescription)
                      $(modalSelector + ' #field-inspection-test-pressure-if-any').val(data.testPressureIfAny)
                  }

                  let galleryHtml = ''
                  if(type === 'maintenance') {
                      data.maintenanceFiles.forEach(i => {
                          galleryHtml += `<input type="hidden" identifier="delete-gallery-${i.id}" name="deletedImageIDs[${i.id}]" value="0">
                          <div class="single-image" data-identifier="gallery-${i.id}">
                              <img src="/${i.filePath}">
                              <div class="delete-image-btn" onclick="deleteGalleryImage('gallery-${i.id}')" style="display: none">
                                  <i class="fa-solid fa-xmark"></i>
                              </div>
                          </div>
                          `
                      })
                  } else {
                  data.inspectionFiles.forEach(i => {
                          galleryHtml += `<input type="hidden" identifier="delete-gallery-${i.id}" name="deletedImageIDs[${i.id}]" value="0">
                          <div class="single-image" data-identifier="gallery-${i.id}">
                              <img src="/${i.filePath}">
                              <div class="delete-image-btn" onclick="deleteGalleryImage('gallery-${i.id}')" style="display: none">
                                  <i class="fa-solid fa-xmark"></i>
                              </div>
                          </div>
                          `
                      })
                  }

                  //fill image
                  $(modalSelector).find('.preview-image-gallery').append(galleryHtml)
              } else {
                  alert(apiresult.message)
              }
          },
          error: function (error) {
              alert('History not found');
          }
      });
  }