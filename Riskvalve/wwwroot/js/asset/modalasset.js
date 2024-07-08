const urlgetassetdetail = getInputVal('#urlgetassetdetail')
const urladdasset = getInputVal('#urladdasset')
const urlupdateasset = getInputVal('#urlupdateasset')
const urldeleteasset = getInputVal('#urldeleteasset')

function editAction() {
    $('.form-control').val('');
    $('.form-control').removeClass('error');
    var mode = $(this).attr('attr-mode');
    if (mode == 'create') {
        $('#field-asset-mode').val('create');
        $('#btn-asset-modal-delete').hide();
        $('#addAssetModal').modal('show');
    } else if (mode == 'view') {
        var id = $(this).attr('attr-itemid');
        $.ajax({
            url: urlgetassetdetail,
            type: 'GET',
            data: { id: id },
            headers: {
                '__RequestVerificationToken': requestVerificationToken
            },
            success: function (apiresult) {
                if (apiresult.isSuccess) {
                    var data = apiresult.data;
                    console.log(data);
                    $('#field-asset-mode').val('update');
                    $('#field-asset-id').val(data.id);
                    $('#field-asset-id-show').val(data.id);
                    $('#field-asset-classrating').val(data.classRating);
                    $('#field-asset-tagno').val(data.tagNo);
                    $('#field-asset-parentequipmentno').val(data.parentEquipmentNo);
                    $('#field-asset-platform').val(data.platformID);
                    $('#field-asset-parentequipmentdescription').val(data.parentEquipmentDescription);
                    $('#field-asset-valvetype').val(data.valveTypeID);
                    $('#field-asset-installationdate').val(data.installationDate);
                    $('#field-asset-size').val(data.size);
                    $('#field-asset-pidno').val(data.pidNo);
                    $('#field-asset-manufacturer').val(data.manufacturer);
                    $('#field-asset-manualoverride').val(data.manualOverrideID);
                    $('#field-asset-bodymodel').val(data.bodyModel);
                    $('#field-asset-actuatormfg').val(data.actuatorMfg);
                    $('#field-asset-bodymaterial').val(data.bodyMaterial);
                    $('#field-asset-serialno').val(data.serialNo);
                    $('#field-asset-endconnection').val(data.endConnection);
                    $('#field-asset-actuatortypemodel').val(data.actuatorTypeModel);
                    $('#field-asset-actuatorserialno').val(data.actuatorSerialNo);
                    $('#field-asset-actuatorpower').val(data.actuatorPower);
                    $('#field-asset-operatingtemperature').val(data.operatingTemperature);
                    $('#field-asset-servicefluid').val(data.serviceFluid);
                    $('#field-asset-operatingpressure').val(data.operatingPressure);
                    $('#field-asset-fluidphase').val(data.fluidPhaseID);
                    $('#field-asset-flowrate').val(data.flowRate);
                    $('#field-asset-toxicorflamablefluid').val(data.toxicOrFlamableFluidID);
                    $('#field-asset-equipmentname').val(data.assetName);
                    $('#field-asset-replacementcost').val(data.costOfReplacementAndRepair);
                    $('#field-asset-status').val(data.status);
                    $('#field-asset-usagetype').val(data.usageType);
                    $('#field-asset-actuation').val(data.actuation);
                    $('#btn-asset-modal-delete').show();
                    $('#addAssetModal').modal('show');
                } else {
                    alert(apiresult.message);
                }
            }
        });
    }
}


$(document).ready(function () {
    $('#btn-asset-modal-save').click(function () {
        if ($('#asset-form').valid()) {
            var mode = $('#field-asset-mode').val();
            var classrating = $('#field-asset-classrating').val();
            var tagno = $('#field-asset-tagno').val();
            var parentequipmentno = $('#field-asset-parentequipmentno').val();
            var platform = $('#field-asset-platform').val();
            var parentequipmentdescription = $('#field-asset-parentequipmentdescription').val();
            var valvetype = $('#field-asset-valvetype').val();
            var installationdate = $('#field-asset-installationdate').val();
            var size = $('#field-asset-size').val();
            var pidno = $('#field-asset-pidno').val();
            var manufacturer = $('#field-asset-manufacturer').val();
            var manualoverride = $('#field-asset-manualoverride').val();
            var bodymodel = $('#field-asset-bodymodel').val();
            var actuatormfg = $('#field-asset-actuatormfg').val();
            var bodymaterial = $('#field-asset-bodymaterial').val();
            var serialno = $('#field-asset-serialno').val();
            var endconnection = $('#field-asset-endconnection').val();
            var actuatortypemodel = $('#field-asset-actuatortypemodel').val();
            var actuatorserialno = $('#field-asset-actuatorserialno').val();
            var actuatorpower = $('#field-asset-actuatorpower').val();
            var operatingtemperature = $('#field-asset-operatingtemperature').val();
            var servicefluid = $('#field-asset-servicefluid').val();
            var operatingpressure = $('#field-asset-operatingpressure').val();
            var fluidphase = $('#field-asset-fluidphase').val();
            var flowrate = $('#field-asset-flowrate').val();
            var toxicorflamablefluid = $('#field-asset-toxicorflamablefluid').val();
            var assetname = $('#field-asset-equipmentname').val();
            var replacementcost = $('#field-asset-replacementcost').val();
            var status = $('#field-asset-status').val();
            var usagetype = $('#field-asset-usagetype').val();
            var actuation = $('#field-asset-actuation').val();
            if (mode == 'create') {
                $.ajax({
                    url: urladdasset,
                    type: 'POST',
                    data: {
                        tagno: tagno,
                        platformid: platform,
                        valvetypeid: valvetype,
                        size: size,
                        classrating: classrating,
                        parentequipmentno: parentequipmentno,
                        parentequipmentdescription: parentequipmentdescription,
                        installationdate: installationdate,
                        pidno: pidno,
                        manufacturer: manufacturer,
                        bodymodel: bodymodel,
                        bodymaterial: bodymaterial,
                        endconnection: endconnection,
                        serialno: serialno,
                        manualoverrideid: manualoverride,
                        actuatormfg: actuatormfg,
                        actuatorserialno: actuatorserialno,
                        actuatortypemodel: actuatortypemodel,
                        actuatorpower: actuatorpower,
                        operatingtemperature: operatingtemperature,
                        operatingpressure: operatingpressure,
                        flowrate: flowrate,
                        servicefluid: servicefluid,
                        fluidphaseid: fluidphase,
                        toxicorflamablefluidid: toxicorflamablefluid,
                        assetname: assetname,
                        costofreplacementandrepair: replacementcost,
                        status: status,
                        usagetype: usagetype,
                        actuation: actuation
                    },
                    headers: {
                        '__RequestVerificationToken': requestVerificationToken
                    },
                    success: function (apiresult) {
                        if (apiresult.isSuccess) {
                            var data = apiresult.data;
                            alert(apiresult.message);
                            console.log(data);
                            location.reload();
                        } else {
                            alert(apiresult.message);
                        }
                    }
                });
            } else if (mode == 'update') {
                var id = $('#field-asset-id').val();
                $.ajax({
                    url: urlupdateasset,
                    type: 'POST',
                    data: {
                        id: id,
                        tagno: tagno,
                        platformid: platform,
                        valvetypeid: valvetype,
                        size: size,
                        classrating: classrating,
                        parentequipmentno: parentequipmentno,
                        parentequipmentdescription: parentequipmentdescription,
                        installationdate: installationdate,
                        pidno: pidno,
                        manufacturer: manufacturer,
                        bodymodel: bodymodel,
                        bodymaterial: bodymaterial,
                        endconnection: endconnection,
                        serialno: serialno,
                        manualoverrideid: manualoverride,
                        actuatormfg: actuatormfg,
                        actuatorserialno: actuatorserialno,
                        actuatortypemodel: actuatortypemodel,
                        actuatorpower: actuatorpower,
                        operatingtemperature: operatingtemperature,
                        operatingpressure: operatingpressure,
                        flowrate: flowrate,
                        servicefluid: servicefluid,
                        fluidphaseid: fluidphase,
                        toxicorflamablefluidid: toxicorflamablefluid,
                        assetname: assetname,
                        costofreplacementandrepair: replacementcost,
                        status: status,
                        usagetype: usagetype,
                        actuation: actuation
                    },
                    headers: {
                        '__RequestVerificationToken': requestVerificationToken
                    },
                    success: function (apiresult) {
                        if (apiresult.isSuccess) {
                            var data = apiresult.data;
                            alert(apiresult.message);
                            console.log(data);
                            location.reload();
                        } else {
                            alert(apiresult.message);
                        }
                    }
                });
            }
        }
    });
    $('#btn-asset-modal-delete').click(function () {
        var id = $('#field-asset-id').val();
        if (confirm('are you sure you want to delete this data?')) {
            $.ajax({
                url: urldeleteasset,
                type: 'POST',
                data: { id: id },
                headers: {
                    '__RequestVerificationToken': requestVerificationToken
                },
                success: function (apiresult) {
                    if (apiresult.isSuccess) {
                        var data = apiresult.data;
                        alert(apiresult.message);
                        console.log(data);
                        location.reload();
                    } else {
                        alert(apiresult.message);
                    }
                }
            });
        }
    });
});