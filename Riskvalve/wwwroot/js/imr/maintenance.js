const urlupdatemainenance = getInputVal('#urlupdatemainenance')
const urladdmainenance = getInputVal('#urladdmainenance')
const urldeletemainenance = getInputVal('#urldeletemainenance')
const urlgetmaintenancedetail = getInputVal('#urlgetmaintenancedetail')
const urlmaintenanceprint = getInputVal('#urlmaintenanceprint')
const urlgetassetdetail = getInputVal('#urlgetassetdetail')


const urlParams = new URLSearchParams(window.location.search);
const idParam = Number(urlParams.get('maintenanceid'));
const assetIdParam = Number(urlParams.get('assetid'));
$(document).ready(function () {
    $('.btn-add-new-inspection').click(addTab);
    if (idParam) {
        $('.btn-add-new-inspection[attr-inspection-id=' + idParam + ']').trigger('click')
    } else if (assetIdParam) {
        $('.btn-add-new-inspection[attr-id=' + assetIdParam + ']').first().trigger('click')
    }
});

$(document).on('click', '.btn-close-inspection', function () {
    const ctr = $(this).closest('a').attr('tab-num')
    close_inspection(ctr);
})

function submitForm(el) {
    const form = $(el).closest('form.inspection-form')
    const equipmentId = $(el).closest('.tab-pane').attr('equipment-id')
    const ctr = $(el).closest('.tab-pane').attr('tab-num')
    const nativeForm = form[0]
    const formData = new FormData(nativeForm);
    const submitbutton = $(el);
    const buttontext = submitbutton.text();
    if (form.valid()) {
        submitbutton.prop("disabled", true).text("Loading...");
        if (form.hasClass('history-form')) {
            // update
            $.ajax({
                url: urlupdatemainenance,
                type: 'POST',
                data: formData,
                headers: {
                    '__RequestVerificationToken': requestVerificationToken
                },
                contentType: false,
                processData: false,
                success: function (apiresult) {
                    if(apiresult.isSuccess){
                        alert(apiresult.message)
                        var response = apiresult.data;
                        form.addClass('history-form')
                        // Handle success response
                        $('.btn-add-new-inspection[attr-inspection-id=' + response.id + ']').html(response.maintenanceDate)
                        form.find('.preview-image-gallery .single-image').remove()
                        form.find('.preview-image-gallery input[type=file]').remove()
                        fetchHistoryData({
                            id: response.id,
                            counter: ctr
                        })
                    } else {
                        alert(apiresult.message)
                    }
                    submitbutton.removeAttr("disabled").text(buttontext);
                },
                error: function (xhr, status, error) {
                    alert('Maintenance date already exist')
                    submitbutton.removeAttr("disabled").text(buttontext);
                }
            });
        } else {
            // add new
            $.ajax({
                url: urladdmainenance,
                type: 'POST',
                data: formData,
                headers: {
                    '__RequestVerificationToken': requestVerificationToken
                },
                contentType: false,
                processData: false,
                success: function (apiresult) {
                    if(apiresult.isSuccess){
                        alert(apiresult.message)
                        var res = apiresult.data;
                        form.addClass('history-form')
                        $('ul[equipment-id="' + equipmentId + '"] li').eq(0).after(`
                            <li class="treeview">
                                <a class="btn-add-new-inspection" history-button="" 
                                attr-id="${res.asset.id}" attr-inspection-id="${res.id}" 
                                attr-name="${res.asset.tagNo} ${res.maintenanceDate}" 
                                attr-sidebar-level="4" 
                                attr-find-inspection="${res.asset.businessArea}" 
                                attr-find-platform="${res.asset.platform}" 
                                attr-find-equipment="${res.asset.tagNo}" 
                                attr-find-imr="NEW"
                                onclick="addTab.apply(this)"
                                >
                                    ${res.maintenanceDate} (NEW)
                                </a>
                            </li>
                        `)

                        form.find('.preview-image-gallery .single-image').remove()
                        form.find('.preview-image-gallery input[type=file]').remove()
                        fetchHistoryData({
                            id: res.id,
                            counter: ctr
                        })
                    } else {
                        alert(apiresult.message)
                    }
                    submitbutton.removeAttr("disabled").text(buttontext);
                },
                error: function (xhr, status, error) {
                    // Handle error
                    alert('error submitting the form')
                    submitbutton.removeAttr("disabled").text(buttontext);
                }
            });
        }
    }
}

function deleteForm(el) {
    const form = $(el).closest('form.inspection-form')

    const id = form.find('#data-id').val()
    const tabId = $(el).closest('.tab-pane').attr('id')
    const ctr = $(el).closest('.tab-pane').attr('tab-num')

    if (confirm('Are you sure you want to delete this data?')) {
        $.ajax({
            url: urldeletemainenance,
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
                    close_inspection(ctr, true)
                    $('.btn-add-new-inspection[attr-inspection-id=' + id + ']').closest('li').remove()
                } else {
                    alert(apiresponse.message)
                }
            },
            error: function (xhr, status, error) {
                alert('Error deleting data')
            }
        });
    }
}

function deleteFromTab(id, name) {
    if (confirm('Are you sure you want to delete ' + name + '?')) {
        $.ajax({
            url: urldeletemainenance,
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

function printForm(el) {
    const form = $(el).closest('form.inspection-form')
    const id = form.find('#data-id').val()
    window.open(urlmaintenanceprint+'/' + id, '_blank');
}

function fetchAssetData({ id, counter }) {
    $.ajax({
        url: urlgetassetdetail,
        type: 'GET',
        data: { id },
        headers: {
            '__RequestVerificationToken': requestVerificationToken
        },
        success: function (apiresult) {
            if (apiresult.isSuccess) {
                var data = apiresult.data;
                fillAssetInfo({ counter, data })
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

function fetchHistoryData({ id, counter }) {
    $.ajax({
        url: urlgetmaintenancedetail,
        type: 'GET',
        data: { id },
        headers: {
            '__RequestVerificationToken': requestVerificationToken
        },
        success: function (apiresult) {
            if (apiresult.isSuccess) {
                var data = apiresult.data;
                // $('#field-inspection-valvetagno' + counter).val(data.tagNo);
                fillAssetInfo({
                    counter, data: data.asset
                })

                // fill history data
                $('#field-maintenance-date' + counter).val(data.maintenanceDate)
                $('#field-maintenance-repaired' + counter).val(data.isValveRepairedID)
                $('#field-maintenance-description' + counter).val(data.maintenanceDescription)

                let galleryHtml = ''
                data.maintenanceFiles.forEach(i => {
                    galleryHtml += `<input type="hidden" identifier="delete-gallery-${i.id}" name="deletedImageIDs[${i.id}]" value="0">
                                    <div class="single-image" data-identifier="gallery-${i.id}">
                                        <img src="/${i.filePath}"">
                                        <div class="delete-image-btn" onclick="deleteGalleryImage('gallery-${i.id}')">
                                            <i class="fa-solid fa-xmark"></i>
                                        </div>
                                    </div>
                                    `
                })

                //fill image
                $('#inspection' + counter).find('.preview-image-gallery').append(galleryHtml)
                $('#inspection' + counter).find('form').addClass('history-form')

                //fill ID
                $('#inspection' + counter).find('#data-id').val(id)
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

function fillAssetInfo({ counter, data }) {
    $('#field-inspection-assetid' + counter).val(data.id)
    $('#field-inspection-valvetype' + counter).val(data.valveType)
    $('#field-inspection-tagno' + counter).val(data.tagNo)
    $('#field-inspection-valvetagno' + counter).val(data.tagNo)
    $('#field-inspection-manufacturer' + counter).val(data.manufacturer)
    $('#field-inspection-equipmentname' + counter).val('')
    $('#field-inspection-bodymaterial' + counter).val(data.bodyMaterial)
    $('#businessarea' + counter).val(data.businessArea)
    $('#bodymodel' + counter).val(data.bodyModel)
    $('#platform' + counter).val(data.platform)
    $('#endconnection' + counter).val(data.endConnection)
    $('#parentequipmentno' + counter).val(data.parentEquipmentNo)
    $('#serialno' + counter).val(data.serialNo)
    $('#parentequipmentdescription' + counter).val(data.parentEquipmentDescription)
    $('#usagetype' + counter).val('')
    $('#installationdate' + counter).val(data.installationDate)
    $('#size' + counter).val(data.size)
    $('#pidno' + counter).val(data.pidNo)
    $('#classrating' + counter).val(data.classRating)
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
    if ($(this).attr('attr-inspection-id')) {
        isHistory = true
        id = $(this).attr('attr-inspection-id')
        tabInspectionId = id
    } else {
        name = `New Maintenance ${name}`
    }
    if (tabInspectionId > 0) {
        if ($('.tab-pane[tab-inspection-id="' + tabInspectionId + '"]').length > 0) {
            alert('This maintenance already exist in the tab');
            return;
        }
    }

    let tabheadercontainer = $('#inspectiontabheader');
    let tabcontentcontainer = $('#tabcontentcontainer');
    tabheadercontainer.append('<li id="inspectionheader' + ctr + '"><a href="#inspection' + ctr + '" data-bs-toggle="tab" class="nav-link" data-bs-target="#inspection' + ctr + '" type="button" role="tab" tab-num="' + ctr + '">' + name + ' <button type="button" class="btn-close-inspection"><i class="fa-solid fa-xmark"></i></button></a></li>');
    tabcontentcontainer.append('<div class="tab-pane" tab-inspection-id="' + tabInspectionId + '" equipment-id="' + equipmentId + '" id="inspection' + ctr + '" tab-num="' + ctr + '">' + template_inspection_html + '</div>');
    let tabcontent = tabcontentcontainer.find('.form-control');
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

    $('#inspectionheader' + ctr).find('a').click();

    isHistory ? fetchHistoryData({
        id,
        counter: ctr
    }) : fetchAssetData({
        id,
        counter: ctr
    })

    initDatepicker()

    moveToLastTab()

    idx++;

    $('.image-gallery-upload').on('change', function () {
        imagesPreview(this);
    });
}