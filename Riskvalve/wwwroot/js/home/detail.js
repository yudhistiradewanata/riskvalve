const urlddlbusinessarea = getInputVal('#urlddlbusinessarea')
const urlbtnfilter = getInputVal('#urlbtnfilter')
const urlinspectiondetail = getInputVal('#urlinspectiondetail')
const urlmaintenancedetail = getInputVal('#urlmaintenancedetail')
const urlassessmentdetail = getInputVal('#urlassessmentdetail')

const dataTableOptions = {
    layout: {
        topStart: {
            buttons: [{
                extend: 'excel',
                text: 'Excel',
                className: 'btn btn-default',
                exportOptions: {
                    columns: ':not(.notexport)',
                    format: {
                        body: function (data, row, column, node) {
                            if ($(node).hasClass("export-html")) {
                                data = $(node).attr("real-val");
                            }
                            if ($(node).hasClass("export-numeric")) {
                                return data.replace(/([.])/g, '').replace(/([,])/g, '.');
                            }
                            return data;
                        }
                    }
                }
            }]
        },
        topEnd: 'search',
        bottomStart: ["paging", "pageLength"],
        bottomEnd: "info",
    },
    ordering: true,
}
$(document).ready(function () {
    $('.havebgcolor').each(function () {
        $(this).css('background-color', getHeatColor($(this).text(), true));
    });

    $('#ddlBusinessArea').on('change', function () {
        var areaId = $(this).val();
        $.ajax({
            url: urlddlbusinessarea,
            type: 'GET',
            headers: {
                '__RequestVerificationToken': requestVerificationToken
            },
            data: { areaId: areaId },
            success: function (data) {
                $('#ddlPlatform').empty();
                $('#ddlPlatform').append('<option value="">All Platform</option>');
                $.each(data, function (i, item) {
                    $('#ddlPlatform').append('<option value="' + item.id + '">' + item.platform + '</option>');
                });
            }
        });
    });

    $('#btnFilter').on('click', function (e) {
        var areaId = $('#ddlBusinessArea').val();
        var platformId = $('#ddlPlatform').val();
        $.ajax({
            url: urlbtnfilter,
            type: 'GET',
            data: { areaId: areaId, platformId: platformId },
            headers: {
                '__RequestVerificationToken': requestVerificationToken
            },
            success: function (data) {
                $(".datatable").DataTable().destroy();
                $('#table-detail').html('');
                $.each(data, function (i, item) {
                    item = sanitizeNull(item);
                    var installationDate = item.asset.installationDate.length >= 10 ?
                        item.asset.installationDate.substring(6, 4) + "-" +
                        item.asset.installationDate.substring(3, 2) + "-" +
                        item.asset.installationDate.substring(0, 2) : item.asset.installationDate;
                    var assessmentDate = item.assessmentDate.length >= 10 ?
                        item.assessmentDate.substring(6, 4) + "-" + item.assessmentDate.substring(3, 2) + "-" +
                        item.assessmentDate.substring(0, 2) : item.assessmentDate;
                    var lastInspectionDate = item.lastInspectionDate.length >= 10 ?
                        item.lastInspectionDate.substring(6, 4) + "-" + item.lastInspectionDate.substring(3, 2) + "-" +
                        item.lastInspectionDate.substring(0, 2) : item.lastInspectionDate;
                    var lastMaintenanceDate = item.lastInspectionDate.length >= 10 ?
                        item.lastMaintenanceDate.substring(6, 4) + "-" + item.lastMaintenanceDate.substring(3, 2) + "-" +
                        item.lastMaintenanceDate.substring(0, 2) : item.lastMaintenanceDate;
                    var html = `
                            <tr>
                                <td>${i + 1}</td>
                                <td>${item.asset.businessArea}</td>
                                <td>${item.asset.platform}</td>
                                <td>${item.asset.tagNo}</td>
                                <td>${item.asset.assetName}</td>
                                <td class="noshow">${item.asset.parentEquipmentNo}</td>
                                <td class="noshow">${item.asset.parentEquipmentDescription}</td>
                                <td>${item.asset.pidNo}</td>
                                <td>${item.asset.valveType}</td>
                                <td>${item.asset.size}</td>
                                <td>${item.asset.classRating}</td>
                                <td class="export-html" real-val="${item.asset.installationDate}"><span style="display: none;">${installationDate}</span>${item.asset.installationDate}</td>
                                <td class="export-html" real-val="${item.assessmentDate}"><span style="display: none;">${assessmentDate}</span>${item.assessmentDate}</td>
                                <td class="export-html" real-val="${item.lastInspectionDate}"><span style="display: none;">${lastInspectionDate}</span>${item.lastInspectionDate}</td>
                                <td class="export-html" real-val="${item.lastMaintenanceDate}"><span style="display: none;">${lastMaintenanceDate}</span>${item.lastMaintenanceDate}</td>
                                <td>${item.timePeriode}</td>
                                <td class="noshow">${item.tP1A}</td>
                                <td class="noshow">${item.tP2A}</td>
                                <td class="noshow">${item.tP3A}</td>
                                <td class="noshow">${item.tP1B}</td>
                                <td class="noshow">${item.tP2B}</td>
                                <td class="noshow">${item.tP3B}</td>
                                <td class="noshow">${item.tP1C}</td>
                                <td class="noshow">${item.tP2C}</td>
                                <td class="noshow">${item.tP3C}</td>
                                <td class="havebgcolor">${item.tP1Risk}</td>
                                <td class="havebgcolor">${item.tP2Risk}</td>
                                <td class="havebgcolor">${item.tP3Risk}</td>
                                <td>${item.integrityStatus}</td>
                                <td>${item.recommendationAction}</td>
                                <td class="noshow">${item.tpTimeToActionRisk}</td>
                                <td class="noshow">${item.leakageToAtmosphere}</td>
                                <td class="noshow">${item.failureOfFunction}</td>
                                <td class="noshow">${item.passingAccrosValve}</td>
                                <td class="noshow">${item.leakageToAtmosphereTP1}</td>
                                <td class="noshow">${item.leakageToAtmosphereTP2}</td>
                                <td class="noshow">${item.leakageToAtmosphereTP3}</td>
                                <td class="noshow">${item.failureOfFunctionTP1}</td>
                                <td class="noshow">${item.failureOfFunctionTP2}</td>
                                <td class="noshow">${item.failureOfFunctionTP3}</td>
                                <td class="noshow">${item.passingAccrosValveTP1}</td>
                                <td class="noshow">${item.passingAccrosValveTP2}</td>
                                <td class="noshow">${item.passingAccrosValveTP3}</td>
                                <td class="noshow">${item.inspectionEffectiveness}</td>
                                <td class="noshow">${item.impactOfInternalFluidImpurities}</td>
                                <td class="noshow">${item.impactOfOperatingEnvelopes}</td>
                                <td class="noshow">${item.usedWithinOEMSpecification}</td>
                                <td class="noshow">${item.repaired}</td>
                                <td class="noshow">${item.productLossDefinition}</td>
                                <td class="noshow">${item.hsseDefinision}</td>
                                <td class="noshow">${item.loFScoreLeakageToAtmophereTP1}</td>
                                <td class="noshow">${item.loFScoreFailureOfFunctionTP1}</td>
                                <td class="noshow">${item.loFScorePassingAccrosValveTP1}</td>
                                <td class="noshow">${item.loFScoreLeakageToAtmophereTP2}</td>
                                <td class="noshow">${item.loFScoreFailureOfFunctionTP2}</td>
                                <td class="noshow">${item.loFScorePassingAccrosValveTP2}</td>
                                <td class="noshow">${item.loFScoreLeakageToAtmophereTP3}</td>
                                <td class="noshow">${item.loFScoreFailureOfFunctionTP3}</td>
                                <td class="noshow">${item.loFScorePassingAccrosValveTP3}</td>
                                <td class="noshow">${item.coFScore}</td>
                                <td class="nowrap">
                                    <button class="btn btn-light btn-sm" onclick="detailAssessment(${item.id})">Assessment</button>
                                    <button class="btn btn-light btn-sm" onclick="detailInspection(${item.lastInspectionId})">Inspection</button>
                                    <button class="btn btn-light btn-sm" onclick="detailMaintenance(${item.lastMaintenanceId})">Maintenance</button>
                                </td>
                            </tr>`
                    $('#table-detail').append(html);
                });
                initDatatable(dataTableOptions)
                $('.havebgcolor').each(function () {
                    $(this).css('background-color', getHeatColor($(this).text(), true));
                });
            }
        });
    });
    initDatatable(dataTableOptions)
});

$('#btnExcel').on('click', () => {
    $('.dt-button.buttons-excel').trigger('click')
})

function sanitizeNull(obj) {
    for (var key in obj) {
        if (obj[key] === null) {
            obj[key] = "";
        } else if (typeof obj[key] === 'object') {
            sanitizeNull(obj[key]);
        }
    }
    return obj;
}

function detailInspection(id) {
    $('#detailInspectionModal').find('input,textarea,select').val('')
    $('#detailInspectionModal .preview-image-gallery').find('input[type=file], .single-image').remove()
    fetchModalDetailData(id, 'inspection')
}

function detailMaintenance(id) {
    $('#detailMaintenanceModal').find('input,textarea,select').val('')
    $('#detailMaintenanceModal .preview-image-gallery').find('input[type=file], .single-image').remove()
    fetchModalDetailData(id, 'maintenance')
}

function detailAssessment(id) {
    $('#detailAssessmentModal').find('input,textarea,select').val('')
    $('#detailAssessmentModal .preview-image-gallery').find('input[type=file], .single-image').remove()
    fetchModalDetailData(id, 'assessment')
}

function getHeatColor(pos, hex = false) {
    if(['1A', '2A', '1B'].includes(pos)){
        if(hex){
            return '#81B014';
        }
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

function calculateYPos(value) {
    var ypos = 0;
    switch(value){
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
    return ypos;
}

function fetchModalDetailData (id, type) {
    // type = maintenance or inspection
    let endPoint = urlinspectiondetail
    let modalSelector = '#detailInspectionModal'
    if (type === 'maintenance') {
        modalSelector = '#detailMaintenanceModal'
        endPoint = urlmaintenancedetail
    }
    if (type === 'assessment') {
        modalSelector = '#detailAssessmentModal'
        endPoint = urlassessmentdetail
    }
    $.ajax({
        url: endPoint,
        type: 'GET',
        data: { id },
        headers: {
            '__RequestVerificationToken': requestVerificationToken
        },
        success: function (apiresult) {
            if(apiresult.isSuccess === false) {
                alert(apiresult.message)
                return false;
            } else {
                const data = apiresult.data
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
                    $(modalSelector + ' #field-maintenance-repaired').val(data.isValveRepaired)
                    $(modalSelector + ' #field-maintenance-description').val(data.maintenanceDescription)
                } else if(type === 'assessment') {
                    $(modalSelector + ' #time-to-limit-a').val(data.timeToLimitStateLeakageToAtmosphere)
                    $(modalSelector + ' #time-to-limit-b').val(data.timeToLimitStateFailureOfFunction)
                    $(modalSelector + ' #time-to-limit-c').val(data.timeToLimitStatePassingAccrosValve)
                    $(modalSelector + ' #assessment-no').val(data.assessmentNo)
                    $(modalSelector + ' #time-period').val(data.timePeriode)
                    $(modalSelector + ' #assessment-date').val(data.assessmentDate)

                    $(modalSelector + ' #leakage-to-atmosphere').val(data.leakageToAtmosphere)
                    $(modalSelector + ' #failure-of-function').val(data.failureOfFunction)
                    $(modalSelector + ' #passing-accross-valve').val(data.passingAccrosValve)
                    $(modalSelector + ' #leakage-to-atmosphere-tp1').val(data.leakageToAtmosphereTP1)
                    $(modalSelector + ' #failure-of-function-tp1').val(data.failureOfFunctionTP1)
                    $(modalSelector + ' #passing-accross-valve-tp1').val(data.passingAccrosValveTP1)
                    $(modalSelector + ' #leakage-to-atmosphere-tp2').val(data.leakageToAtmosphereTP2)
                    $(modalSelector + ' #failure-of-function-tp2').val(data.failureOfFunctionTP2)
                    $(modalSelector + ' #passing-accross-valve-tp2').val(data.passingAccrosValveTP2)
                    $(modalSelector + ' #leakage-to-atmosphere-tp3').val(data.leakageToAtmosphereTP3)
                    $(modalSelector + ' #failure-of-function-tp3').val(data.failureOfFunctionTP3)
                    $(modalSelector + ' #passing-accross-valve-tp3').val(data.passingAccrosValveTP3)
                    $(modalSelector + ' #inspection-effectiveness').val(data.inspectionEffectiveness)
                    $(modalSelector + ' #impact-of-internal-fluid').val(data.impactOfInternalFluidImpurities)
                    $(modalSelector + ' #impact-of-operating-envelopes').val(data.impactOfOperatingEnvelopes)
                    $(modalSelector + ' #used-within-oem-specification').val(data.usedWithinOEMSpecification)
                    $(modalSelector + ' #repaired').val(data.repaired)

                    $(modalSelector + ' #product-loss-definition').val(data.productLossDefinition)
                    $(modalSelector + ' #hsse-definition').val(data.hsseDefinision)

                    //integrity-plan
                    $(modalSelector + ' #integrity-status').val(data.integrityStatus)
                    $(modalSelector + ' #time-to-action').val(data.tpTimeToActionRisk)
                    $(modalSelector + ' #recommended-action').val(data.recommendationAction)
                    $(modalSelector + ' #summary').text(data.summary || 'asdasdasd')
                    $(modalSelector + ' #detailed-recommendation').text(data.detailedRecommendation || 'adsassad')

                    //fill inspection table
                    let inspectionHtml = ''
                    data.inspectionHistory.forEach((inspection, index) => {
                        var val = inspection.inspection
                        inspectionHtml += `<tr>
                            <td>${index + 1}</td>
                            <td>${val.inspectionDate}</td>
                            <td>${val.inspectionMethod}</td>
                            <td>${val.inspectionDescription}</td>
                            <td>${val.functionCondition}</td>
                            <td>${val.testPressureIfAny}</td>
                        </tr>`
                    })
                    $(modalSelector + ' #inspection-history-content').html(inspectionHtml)
                    
                    //fill maintenance table
                    let maintenanceHtml = ''
                    data.maintenanceHistory.forEach((maintenance, index) => {
                        var val = maintenance.maintenance
                        maintenanceHtml += `<tr>
                            <td>${index + 1}</td>
                            <td>${val.maintenanceDate}</td>
                            <td>${val.isValveRepaired}</td>
                            <td>${val.maintenanceDescription}</td>
                        </tr>`
                    })

                    const riskTableApiMapping = [
                        'tP1A', 'tP1B', 'tP1C', 'tP1Risk',
                        'tP2A', 'tP2B', 'tP2C', 'tP2Risk',
                        'tP3A', 'tP3B', 'tP3C', 'tP3Risk',
                        'tpTimeToActionA', 'tpTimeToActionB', 'tpTimeToActionC', 'tpTimeToActionRisk'
                    ]

                    riskTableApiMapping.forEach(i => {
                        const statusBox = $(`#${i.toUpperCase()}-colorlabel`)
                        const statusLabel = $(`#${i.toUpperCase()}-label`)
                        const risk = data[i]
                        statusBox.removeClass()
                        statusBox.addClass('status-box')
                        statusBox.addClass(getHeatColor(risk))
                        statusLabel.text(risk)
                    })

                    const riskHeatmapMapping = {
                        TP1A: 'loFScoreLeakageToAtmophereTP1',
                        TP1B: 'loFScoreFailureOfFunctionTP1',
                        TP1C: 'loFScorePassingAccrosValveTP1',
                        TP2A: 'loFScoreLeakageToAtmophereTP2',
                        TP2B: 'loFScoreFailureOfFunctionTP2',
                        TP2C: 'loFScorePassingAccrosValveTP2',
                        TP3A: 'loFScoreLeakageToAtmophereTP3',
                        TP3B: 'loFScoreFailureOfFunctionTP3',
                        TP3C: 'loFScorePassingAccrosValveTP3',
                    }

                    Object.keys(riskHeatmapMapping).forEach(key => {
                        const resKey = riskHeatmapMapping[key]
                        const val = data[resKey]
                        console.log(resKey, 'resKey')
                        console.log(val, 'value')
                        const xPos = calculateXPos(val)
                        const yPos = calculateYPos(data.consequenceOfFailure)
                        $(`#${key}`).css({
                            left: xPos + '%',
                            top: yPos + '%'
                        })
                    })

                    $(modalSelector + ' #maintenance-history-content').html(maintenanceHtml)
                } else {
                    $(modalSelector + ' #field-inspection-date').val(data.inspectionDate)
                    $(modalSelector + ' #field-inspection-inspection-method').val(data.inspectionMethod)
                    $(modalSelector + ' #field-inspection-current-condition-limit-state-a').val(data.currentConditionLeakeageToAtmosphere)
                    $(modalSelector + ' #field-inspection-current-condition-limit-state-b').val(data.currentConditionFailureOfFunction)
                    $(modalSelector + ' #field-inspection-current-condition-limit-state-c').val(data.currentConditionPassingAcrossValve)
                    $(modalSelector + ' #field-inspection-effectiveness').val(data.inspectionEffectiveness)
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
                } else if (type === 'inspection') {
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
                if(type === 'maintenance') {
                    $('#detailMaintenanceModal').attr('data-type', 'view')
                    $('#detailMaintenanceModal').modal('show')
                } else if (type === 'inspection') {
                    $('#detailInspectionModal').attr('data-type', 'view')
                    $('#detailInspectionModal').modal('show')
                } else if (type === 'assessment') {
                    $('#detailAssessmentModal').attr('data-type', 'view')
                    $('#detailAssessmentModal').modal('show')
                }
            }
        },
        error: function (error) {
            alert('History not found');
        }
    });
}