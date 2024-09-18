const urlgetplatformdetail = getInputVal('#urlgetplatformdetail')
const urladdplatform = getInputVal('#urladdplatform')
const urlupdateplatform = getInputVal('#urlupdateplatform')
const urldeleteplatform = getInputVal('#urldeleteplatform')


function editAction () {
  $('.form-control').removeClass('error');
  $('#field-platform-area').val('');
  $('#field-platform-name').val('');
  $('#field-platform-code').val('');
  var mode = $(this).attr('attr-mode');
  if (mode == 'create') {
      $('#field-platform-mode').val('create');
      $('#btn-platform-modal-delete').hide();
      $('.modalinfo').hide();
      $('#addPlatformModal').modal('show');
  } else if (mode == 'view') {
      var id = $(this).attr('attr-itemid');
      $.ajax({
          url: urlgetplatformdetail,
          type: 'GET',
          data: { id: id },
          headers: {
              '__RequestVerificationToken': requestVerificationToken
          },
          success: function (apiresult) {
              if (apiresult.isSuccess) {
                  var data = apiresult.data;
                  $('#field-platform-mode').val('update');
                  $('#field-platform-id').val(data.id);
                  $('#field-platform-area').val(data.areaID);
                  $('#field-platform-name').val(data.platform);
                  $('#field-platform-code').val(data.code);
                  $('#btn-platform-modal-delete').show();
                  // Example data
                  const createdBy = data.createdByUser;
                  const createdOn = data.createdAt;
                  const lastUpdatedBy = data.updatedByUser;
                  const lastUpdatedOn = data.updatedAt;
                  // Update the text
                  var recordMetaText = `Created by ${createdBy} on ${createdOn}`;
                  if(lastUpdatedBy != null && lastUpdatedBy != '') {
                    recordMetaText = `${recordMetaText}\nLast updated by ${lastUpdatedBy} on ${lastUpdatedOn}`;
                  }
                  $('.modalinfo').show();
                  $('#record-meta').text(recordMetaText);
                  $('#addPlatformModal').modal('show');
              } else {
                  alert(apiresult.message);
              }
          }
      });
  }
}

$(document).ready(function () {
  $('#btn-platform-modal-save').click(function () {
      const submitButton = $(this);
      const submitButtonText = submitButton.text();
      if($('#platform-form').valid()) {
          submitButton.text('Loading...');
          submitButton.attr('disabled', true); 
          var mode = $('#field-platform-mode').val();
          if (mode == 'create') {
              $.ajax({
                  url: urladdplatform,
                  type: 'POST',
                  data: {
                      areaID: $('#field-platform-area').val(),
                      platform: $('#field-platform-name').val(),
                      code: $('#field-platform-code').val()
                  },
                  headers: {
                      '__RequestVerificationToken': requestVerificationToken
                  },
                  success: function (apiresult) {
                      if (apiresult.isSuccess) {
                          alert(apiresult.message);
                          location.reload();
                      } else {
                          alert(apiresult.message);
                      }
                      submitButton.removeAttr('disabled').text(submitButtonText);
                  },
                  error: function (apiresult) {
                    submitButton.removeAttr('disabled').text(submitButtonText);
                  }
              });
          } else if (mode == 'update') {
              var id = $('#field-platform-id').val();
              $.ajax({
                  url: urlupdateplatform,
                  type: 'POST',
                  data: {
                      id: id,
                      areaID: $('#field-platform-area').val(),
                      platform: $('#field-platform-name').val(),
                      code: $('#field-platform-code').val()
                  },
                  headers: {
                      '__RequestVerificationToken': requestVerificationToken
                  },
                  success: function (apiresult) {
                      if (apiresult.isSuccess) {
                          alert(apiresult.message);
                          location.reload();
                      } else {
                          alert(apiresult.message);
                      }
                  },
                  error: function (apiresult) {
                    submitButton.removeAttr('disabled').text(submitButtonText);
                  }
              });
          }
      }
  });
  $('#btn-platform-modal-delete').click(function () {
      var id = $('#field-platform-id').val();
      if(confirm('are you sure you want to delete this data?')) {
          $.ajax({
              url: urldeleteplatform,
              type: 'POST',
              data: { id: id },
              headers: {
                  '__RequestVerificationToken': requestVerificationToken
              },
              success: function (apiresult) {
                  if (apiresult.isSuccess) {
                      alert(apiresult.message);
                      location.reload();
                  } else {
                      alert(apiresult.message);
                  }
              }
          });
      }
  });

  initDatatable({
      ordering: true,
  })
});