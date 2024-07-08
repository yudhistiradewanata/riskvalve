const urlddlbusinessarea = getInputVal('#urlddlbusinessarea')
const urlbtnfilter = getInputVal('#urlbtnfilter')

const dataTableOptions = {
    layout: {
        topStart: {
            buttons: ['excel']
        },
        topEnd: 'search',
        bottomStart: null,
        bottomEnd: null
    }
}

initDatatable(dataTableOptions)

$(document).ready(function () {
    $('#ddlBusinessArea').on('change', function () {
        var areaId = $(this).val();
        $.ajax({
            url: urlddlbusinessarea,
            type: 'GET',
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
            success: function (data) {
                $(".datatable").DataTable().destroy();
                $('#table-detail').html('');
                $.each(data, function (i, item) {
                    var html = `
                        <tr>
                            <td>${i + 1}</td>
                            <td>${item.asset.tagNo}</td>
                            <td>${item.asset.businessArea}</td>
                            <td>${item.asset.platform}</td>
                            <td>${item.asset.parentEquipmentNo}</td>
                            <td>${item.asset.valveType}</td>
                            <td>${item.asset.parentEquipmentDescription}</td>
                            <td>${item.asset.installationDate}</td>
                            <td>${item.asset.pidNo}</td>
                            <td>${item.asset.valveType}</td>
                            <td>${item.asset.size}</td>
                            <td>${item.asset.classRating}</td>
                            <td>${item.assessmentDate}</td>
                            <td>${item.lastInspectionDate}</td>
                            <td>${item.lastMaintenanceDate}</td>
                            <td>${item.tp1A}</td>
                            <td>${item.tp2A}</td>
                            <td>${item.tp3A}</td>
                            <td>${item.tp1B}</td>
                            <td>${item.tp2B}</td>
                            <td>${item.tp3B}</td>
                            <td>${item.tp1C}</td>
                            <td>${item.tp2C}</td>
                            <td>${item.tp3C}</td>
                            <td>${item.tp1Risk}</td>
                            <td>${item.tp2Risk}</td>
                            <td>${item.tp3Risk}</td>
                            <td>${item.recommendationAction}</td>
                            <td>${item.tpTimeToActionRisk}</td>
                            <td>${item.leakageToAtmosphere}</td>
                            <td>${item.failureOfFunction}</td>
                            <td>${item.passingAccrosValve}</td>
                            <td>${item.leakageToAtmosphereTP1}</td>
                            <td>${item.leakageToAtmosphereTP2}</td>
                            <td>${item.leakageToAtmosphereTP3}</td>
                            <td>${item.failureOfFunctionTP1}</td>
                            <td>${item.failureOfFunctionTP2}</td>
                            <td>${item.failureOfFunctionTP3}</td>
                            <td>${item.passingAccrosValveTP1}</td>
                            <td>${item.passingAccrosValveTP2}</td>
                            <td>${item.passingAccrosValveTP3}</td>
                            <td>${item.inspectionEffectiveness}</td>
                            <td>${item.impactOfInternalFluidImpurities}</td>
                            <td>${item.impactOfOperatingEnvelopes}</td>
                            <td>${item.usedWithinOEMSpecification}</td>
                            <td>${item.repaired}</td>
                            <td>${item.productLossDefinition}</td>
                            <td>${item.hsseDefinision}</td>
                            <td>${item.loFScoreLeakageToAtmophereTP1}</td>
                            <td>${item.loFScoreFailureOfFunctionTP1}</td>
                            <td>${item.loFScorePassingAccrosValveTP1}</td>
                            <td>${item.loFScoreLeakageToAtmophereTP2}</td>
                            <td>${item.loFScoreFailureOfFunctionTP2}</td>
                            <td>${item.loFScorePassingAccrosValveTP2}</td>
                            <td>${item.loFScoreLeakageToAtmophereTP3}</td>
                            <td>${item.loFScoreFailureOfFunctionTP3}</td>
                            <td>${item.loFScorePassingAccrosValveTP3}</td>
                            <td>${item.coFScore}</td>
                        </tr>`
                    $('#table-detail').append(html);
                });
                initDatatable(dataTableOptions)
            }
        });
    });
});

    $('#btnExcel').on('click', () => {
        $('.dt-button.buttons-excel').trigger('click')
    })
