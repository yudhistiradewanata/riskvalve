const urlgetuser = getInputVal('#urlgetuser')
const urladduser = getInputVal('#urladduser')
const urlupdateuser = getInputVal('#urlupdateuser')
const urldeleteuser = getInputVal('#urldeleteuser')

function editAction() {
    $('#field-user-id').val('');
    $('#field-username').val('');
    $('#field-password').val('');
    $('#field-role').val('');
    $('#field-isadmin').prop('checked', false);
    $('#field-isengineer').prop('checked', false);
    $('#field-isviewer').prop('checked', false);
    var mode = $(this).attr('attr-mode');
    if (mode == 'create') {
        $('#field-user-mode').val('create');
        $('#btn-user-modal-delete').hide();
        $('#field-password').attr('required', 'true')
        $('#field-password').attr('placeholder', 'Password');
        $('#addUserModal').modal('show');
    } else if (mode == 'view') {
        var id = $(this).attr('attr-itemid');
        $('#field-password').removeAttr('required')
        $.ajax({
            url: urlgetuser,
            type: 'GET',
            data: { id: id },
            headers: {
                '__RequestVerificationToken': requestVerificationToken
            },
            success: function (apiresult) {
                if (apiresult.isSuccess) {
                    var data = apiresult.data;
                    $('#field-user-id').val(data.id);
                    $('#field-username').val(data.username);
                    $('#field-password').attr('placeholder', 'Leave blank if you don\'t want to change the password');
                    $('#field-role').val(data.role);
                    $('#field-isadmin').prop('checked', data.isAdmin);
                    $('#field-isengineer').prop('checked', data.isEngineer);
                    $('#field-isviewer').prop('checked', data.isViewer);
                    $('#field-user-mode').val('update');
                    $('#btn-user-modal-delete').show();
                    $('#addUserModal').modal('show');
                } else {
                    alert(apiresult.message);
                }
            }
        });
    }
}

$(document).ready(function () {
    $("input#field-username").on({
        keydown: function (e) {
            if (e.which === 32)
                return false;
        },
        change: function () {
            this.value = this.value.replace(/\s/g, "");
        }
    });
    $('#btn-user-modal-save').click(function () {
        if ($('.user-form').valid()) {
            var mode = $('#field-user-mode').val();
            var id = $('#field-user-id').val();
            var username = $('#field-username').val();
            var password = $('#field-password').val();
            var role = $('#field-role').val();
            var isAdmin = $('#field-isadmin').prop('checked');
            var isEngineer = $('#field-isengineer').prop('checked');
            var isViewer = $('#field-isviewer').prop('checked');
            if (mode == 'create') {
                if(checkPasswordValidation(password)) {
                    $.ajax({
                        url: urladduser,
                        type: 'POST',
                        headers: {
                            '__RequestVerificationToken': requestVerificationToken
                        },
                        data: { username: username, password: password, role: role, isAdmin: isAdmin, isEngineer: isEngineer, isViewer: isViewer },
                        success: function (apiresult) {
                            if (apiresult.isSuccess) {
                                var data = apiresult.data;
                                alert(apiresult.message);
                                location.reload();
                            } else {
                                alert(apiresult.message);
                            }
                        }
                    });
                } else {
                    alert('Password must contain at least 8 characters, 1 uppercase letter, 1 lowercase letter, 1 number, and 1 special character');
                }
            } else if (mode == 'update') {
                if(checkPasswordValidation(password) || password == '') {
                    $.ajax({
                        url: urlupdateuser,
                        type: 'POST',
                        headers: {
                            '__RequestVerificationToken': requestVerificationToken
                        },
                        data: { id: id, username: username, password: password, role: role, isAdmin: isAdmin, isEngineer: isEngineer, isViewer: isViewer },
                        success: function (apiresult) {
                            if (apiresult.isSuccess) {
                                var data = apiresult.data;
                                alert(apiresult.message);
                                location.reload();
                            } else {
                                alert(apiresult.message);
                            }
                        }
                    });
                } else {
                    alert('Password must contain at least 8 characters, 1 uppercase letter, 1 lowercase letter, 1 number, and 1 special character');
                }
            }
        }
    });
    $('#btn-user-modal-delete').click(function () {
        if (confirm('Are you sure you want to delete this user?')) {
            var id = $('#field-user-id').val();
            $.ajax({
                url: urldeleteuser,
                type: 'POST',
                data: { id: id },
                headers: {
                    '__RequestVerificationToken': requestVerificationToken
                },
                success: function (apiresult) {
                    if (apiresult.isSuccess) {
                        var data = apiresult.data;
                        alert(apiresult.message);
                        location.reload();
                    } else {
                        alert(apiresult.message);
                    }
                }
            });
        }
    });
    $('#btn-user-modal-cancel').click(function () {
        $('#addUserModal').modal('hide');
    });
    initDatatable({
        ordering: true,
    })
})
