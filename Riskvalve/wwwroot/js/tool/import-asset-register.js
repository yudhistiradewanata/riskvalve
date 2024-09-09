const urlimportexcel = getInputVal('#urlimportexcel')

$(document).ready(function () {
  $("form").submit(function (e) {
      e.preventDefault();
      // Disable the submit button to prevent double-clicking
      const submitButton = $(this).find('button[type="submit"]');
      const submitText = submitButton.text();
      submitButton.prop('disabled', true);
      submitButton.text('Loading...');
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
              submitButton.text(submitText);
              submitButton.prop('disabled', false);
          },
          error: function (xhr, status, error) {
              console.log(xhr.responseText)
              alert('Error submitting the form')
              submitButton.text(submitText);
              submitButton.prop('disabled', false);
          }
      });
  });
});