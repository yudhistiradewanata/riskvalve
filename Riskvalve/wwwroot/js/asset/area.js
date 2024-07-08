const urlgetareadetail = getInputVal('#urlgetareadetail')
const urladdarea = getInputVal('#urladdarea')
const urlupdatearea = getInputVal('#urlupdatearea')
const urldeletearea = getInputVal('#urldeletearea')

function editAction() {
  $('#field-business-area-name').val('');
  var mode = $(this).attr('attr-mode');
  if (mode == 'create') {
      $('#field-business-area-mode').val('create');
      $('#btn-area-modal-delete').hide();
      $('#addAreaModal').modal('show');
  } else if (mode == 'view') {
      var id = $(this).attr('attr-itemid');
      $.ajax({
          url: urlgetareadetail,
          type: 'GET',
          data: { id: id },
          headers: {
              '__RequestVerificationToken': requestVerificationToken
          },
          success: function (apiresult) {
              if (apiresult.isSuccess) {
                  var data = apiresult.data;
                  $('#field-business-area-mode').val('update');
                  $('#field-business-area-id').val(data.id);
                  $('#field-business-area-name').val(data.businessArea);
                  $('#btn-area-modal-delete').show();
                  $('#addAreaModal').modal('show');
              } else {
                  alert(apiresult.message);
              }
          }
      });
  }
}

$(document).ready(function () {
  $('#btn-area-modal-save').click(function () {
      if ($('.asset-form').valid()) {
          var mode = $('#field-business-area-mode').val();
          var name = $('#field-business-area-name').val();
          if (name == '') {
              alert('Business Area Name is required');
              return;
          } else {
              if (mode == 'create') {
                  $.ajax({
                      url: urladdarea,
                      type: 'POST',
                      data: { businessArea: name },
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
              } else if (mode == 'update') {
                  var id = $('#field-business-area-id').val();
                  $.ajax({
                      url: urlupdatearea,
                      type: 'POST',
                      data: { id: id, businessArea: name },
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
          }
      }
  });
  $('#btn-area-modal-delete').click(function () {
      var id = $('#field-business-area-id').val();
      if (confirm('are you sure you want to delete this data?')) {
          $.ajax({
              url: urldeletearea,
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