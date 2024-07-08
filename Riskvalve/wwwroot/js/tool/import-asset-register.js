const urlimportexcel = getInputVal('#urlimportexcel')

$(document).ready(function () {
  $("form").submit(function (e) {
      e.preventDefault();
      var formData = new FormData();
      formData.append("file", $("#fileUpload")[0].files[0]);
      formData.append("mode", $("#mode").val());
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
                  $("#fileUpload").val('');
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