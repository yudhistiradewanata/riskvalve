const urlimportexcel = getInputVal('#urlimportexcel')

$(document).ready(function () {
    $("#inspection-form").submit(function (e) {
        e.preventDefault();
        var formData = new FormData();
        formData.append("file", $("#fileUploadInspection")[0].files[0]);
        formData.append("mode", $("#modeInspection").val());
        $.ajax({
            url: urlimportexcel,
            type: "POST",
            data: formData,
            headers: {
                '__RequestVerificationToken': requestVerificationToken
            },
            contentType: false,
            processData: false,
            success: function (data) {
                bootbox.alert(data.message, function () {
                    $("#fileUploadInspection").val('');
                    location.reload();
                });
            },
            error: function (xhr, status, error) {
                console.log(xhr.responseText)
                alert('Error submitting the form')
            }
        });
    });
    $("#maintenance-form").submit(function (e) {
        e.preventDefault();
        var formData = new FormData();
        formData.append("file", $("#fileUploadMaintenance")[0].files[0]);
        formData.append("mode", $("#modeMaintenance").val());
        $.ajax({
            url: urlimportexcel,
            type: "POST",
            data: formData,
            headers: {
                '__RequestVerificationToken': requestVerificationToken
            },
            contentType: false,
            processData: false,
            success: function (data) {
                bootbox.alert(data.message, function () {
                    $("#fileUploadMaintenance").val('');
                    location.reload();
                });
            },
            error: function (xhr, status, error) {
                console.log(xhr.responseText)
                alert('Error submitting the form')
            }
        });
    });
});
