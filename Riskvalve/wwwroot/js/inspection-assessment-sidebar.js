const urldeleteasset = getInputVal('#urldeleteasset')
const urlgetassetsidebar = getInputVal('#urlgetassetsidebar')
const pageType = getInputVal('#pageType')
const urllevel4 = getInputVal('#urllevel4')
const urlassetsearch = getInputVal('#urlassetsearch')


function showLoader () {
    $('.loader').addClass('show')
}
function hideLoader () {
    $('.loader').removeClass('show')
}
function deleteAsset(idx, name) {
    var id = idx;
    if (confirm('Are you sure you want to delete this asset  ' + name + '?')) {
        $.ajax({
            url: urldeleteasset,
            type: 'POST',
            data: { id: id },
            headers: {
                '__RequestVerificationToken': requestVerificationToken
            },
            success: function (data) {
                if (data.result == 400) {
                    alert(data.message);
                } else {
                    location.reload();
                }
            }
        });
    }
}

function close_inspection(ctr, force) {
    if (force || confirm('Are you sure you want to close this tab?')) {
        $('#inspectionheader' + ctr).remove();
        $('#inspection' + ctr).remove();
        moveToLastTab()
    }
}

function moveToLastTab() {
    var lastTabEl = document.querySelector('#inspectiontabheader li:last-child a');
    const lastTab = new bootstrap.Tab(lastTabEl)
    lastTab.show()
}

function fold_all_sidebar_menu() {
    for (var i = 2; i <= 4; i++) {
        $('.sidebar-menu-inspection-assessment').find('a[attr-sidebar-level="' + i + '"]').each(function () {
            var parent = $(this).parent();
            parent.hide();

        });
    }
}

function sidebarClick () {
    if($(this).hasClass('child-open')) {
        $(this).removeClass('child-open')
        $(this).closest('.treeview').find('.treeview,.child-data-entry').css('display', 'none')
        $(this).closest('.treeview').find('.child-open').removeClass('child-open')
        return
    }
    $('.sidebartreea').removeClass('selected')
    $(this).addClass('selected')
    var level = $(this).attr('attr-sidebar-level');
    var level_1 = $(this).attr('attr-find-inspection');
    var level_2 = $(this).attr('attr-find-platform');
    var level_3 = $(this).attr('attr-find-equipment');
    let selectedEl = $(this)
    selectedEl.toggleClass('child-open')
    const isChildOpening = selectedEl.hasClass('child-open')
    if (!isChildOpening) {
        selectedEl.closest('.treeview').find('a').removeClass('child-open')
    } else {
        selectedEl.closest('.treeview').siblings().each(function () {
            $(this).find('a').removeClass('child-open')
        })
    }

    if (level == 3) {
        let ul_el = selectedEl.closest('.treeview').find('ul')
        ul_el.find('.child-data-entry').remove()
        showLoader()
        $.ajax({
            url: urllevel4,
            type: 'GET',
            data: {
                assetid: $(this).attr('attr-find-equipmentid')
            },
            headers: {
                '__RequestVerificationToken': requestVerificationToken
            },
            success: function (data) {
                let newLiElement = ''
                data.forEach(function (item) {
                    newLiElement += `<li class="child-data-entry">
                                            <a class="btn-add-new-inspection" history-button 
                                                attr-id="${item.AssetID}"
                                                attr-sidebar-level="4"
                                                attr-inspection-id="${item.Id}"
                                                attr-name="${item.Asset} ${item.Name}" 
                                                attr-find-inspection="${item.Area}" 
                                                attr-find-platform="${item.Platform}"
                                                attr-find-equipment="${item.Asset}"
                                                attr-find-imr="NEW"
                                                onclick="addTab.apply(this)"
                                            >${item.Name}</a>
                                        </li>` 
                })
                ul_el.append(newLiElement)
            },
            complete: hideLoader
        })
        $('.sidebar-menu-inspection-assessment').find('a[attr-sidebar-level="2"]').each(function () {
            var parent = $(this).parent();
            if ($(this).attr('attr-find-inspection') != level_1) {
                parent.hide();
            }
            else {
                parent.show();
            }
        });
        $('.sidebar-menu-inspection-assessment').find('a[attr-sidebar-level="3"]').each(function () {
            var parent = $(this).parent();
            if ($(this).attr('attr-find-platform') != level_2) {
                parent.hide();
            }
            else {
                parent.show();
            }
        });
        $('.sidebar-menu-inspection-assessment').find('a[attr-sidebar-level="4"]').each(function () {
            // alert('level 4')
            var parent = $(this).parent();
            if ($(this).attr('attr-find-equipment') != level_3 || !isChildOpening) {
                // alert('level 3')
                parent.hide();
            }
            else {
                parent.show();
            }
        });
    } else if (level == 2) {
        const ul_el = selectedEl.closest('.treeview').find('ul')
        ul_el.empty()
        showLoader()
        $.ajax({
            url: urlgetassetsidebar,
            type: 'GET',
            data: {
                platformid: $(this).attr('attr-find-platformid')
            },
            headers: {
                '__RequestVerificationToken': requestVerificationToken
            },
            success: function (apiresult) {
                if (apiresult.isSuccess) {
                    const data = apiresult.data;
                    let newLiElement = ''
                    let counter = 0
                    data.forEach(function (item) {
                        counter++
                        if(counter > 10 && window.allowSkip) return
                        const newLi = '<li class="treeview">'
                            + '<a class="sidebartreea" onclick="sidebarClick.apply(this)" attr-sidebar-level="3" attr-find-inspection="' + item.businessArea + '" attr-find-platform="' + item.platform + '" attr-find-equipmentid="'+ item.id +'" attr-find-equipment="' + item.tagNo + '">' + item.tagNo + '</a>'
                            + `<ul class="treeview-menu" equipment-id="${item.id}">
                                    <li style="display: none" class="treeview">
                                        <a class="btn-add-new-inspection" attr-id="${item.id}"
                                            attr-name="${item.tagNo}" attr-sidebar-level="4"
                                            attr-find-inspection="${item.businessArea}" attr-find-platform="${item.platform}"
                                            attr-find-equipment="${item.tagNo}" attr-find-imr="NEW" onclick="addTab.apply(this)">[Add New ${pageType}
                                            ]</a>
                                    </li>
                                </ul>`
                        +'</li>'
                        newLiElement += newLi
                    });
                    ul_el.html(newLiElement)
                } else {
                    alert(apiresult.message);
                }
            },
            complete: hideLoader
        })
        $('.sidebar-menu-inspection-assessment').find('a[attr-sidebar-level="2"]').each(function () {
            var parent = $(this).parent();
            if ($(this).attr('attr-find-inspection') != level_1) {
                parent.hide();
            }
            else {
                parent.show();
            }
        });
        $('.sidebar-menu-inspection-assessment').find('a[attr-sidebar-level="3"]').each(function () {
            var parent = $(this).parent();
            if ($(this).attr('attr-find-platform') != level_2 || !isChildOpening) {
                parent.hide();
            }
            else {
                parent.show();
            }
        });
        $('.sidebar-menu-inspection-assessment').find('a[attr-sidebar-level="4"]').each(function () {
            var parent = $(this).parent();
            parent.hide();
        });
    } else if (level == 1) {
        $('.sidebar-menu-inspection-assessment').find('a[attr-sidebar-level="2"]').each(function () {
            var parent = $(this).parent();
            if ($(this).attr('attr-find-inspection') != level_1 || !isChildOpening) {
                parent.hide();
            }
            else {
                parent.show();
            }
        });
        $('.sidebar-menu-inspection-assessment').find('a[attr-sidebar-level="3"]').each(function () {
            var parent = $(this).parent();
            parent.hide();
        });
        $('.sidebar-menu-inspection-assessment').find('a[attr-sidebar-level="4"]').each(function () {
            var parent = $(this).parent();
            parent.hide();
        });
    }
}

$(document).ready(function () {
    fold_all_sidebar_menu();
    $("#searchForm").on("keyup", debounce(function () {
        if($(this).val().length > 0){
            showLoader()
            $.ajax({
                url: urlassetsearch,
                type: 'GET',
                data: {
                    search: $(this).val()
                },
                headers: {
                    '__RequestVerificationToken': requestVerificationToken
                },
                success: function (apiresult) {
                    if (apiresult.isSuccess) {
                        var data = apiresult.data;
                        for(var i = 0; i < Object.keys(data).length; i++){
                            var key = Object.keys(data)[i];
                            var value = data[key];
                            var ul_el = $('ul[attr-find-forsearch="'+key+'"]');
                            ul_el.empty()
                            value.forEach(function (item) {
                                let li_el = $('<li class="treeview"></li>')
                                let a_el = $('<a class="sidebartreea" onclick="sidebarClick.apply(this)" attr-sidebar-level="3" attr-find-inspection="' + item.businessArea + '" attr-find-platform="' + item.platform + '" attr-find-equipmentid="'+ item.id +'" attr-find-equipment="' + item.tagNo + '">' + item.tagNo + '</a>')
                                let ul_el2 = $(`<ul class="treeview-menu" equipment-id="${item.id}">
                                    <li style="display: none" class="treeview">
                                        <a class="btn-add-new-inspection" attr-id="${item.id}"
                                            attr-name="${item.tagNo}" attr-sidebar-level="4"
                                            attr-find-inspection="${item.businessArea}" attr-find-platform="${item.platform}"
                                            attr-find-equipment="${item.tagNo}" attr-find-imr="NEW" onclick="addTab.apply(this)">[Add New
                                            ${pageType}]</a>
                                    </li>
                                </ul>`)
                                li_el.append(a_el)
                                li_el.append(ul_el2)
                                ul_el.append(li_el)
                            })
                        }
                    } else {
                        alert(apiresult.message);
                    }
                },
                complete: hideLoader
            })
        }
        var value = $(this).val().toLowerCase();
        if (value == '') {
            $(".sidebar-menu-inspection-assessment li").filter(function () {
                $(this).show();
            });
            fold_all_sidebar_menu();
        } else {
            $(".sidebar-menu-inspection-assessment li").filter(function () {
                $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
            });
        }
    },500));
});