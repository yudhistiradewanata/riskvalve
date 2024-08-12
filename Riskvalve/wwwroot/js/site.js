// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function initDatepicker() {
  $(".datepicker")
    .datepicker({
      format: "dd-mm-yyyy",
      autoclose: true,
      todayHighlight: true,
    })
    .on("changeDate", function (e) {
      $(this).removeClass("error");
      $(this).trigger("change");
    });
}

$(document).ready(function () {
  $(".btn-inspection").click(function () {
    console.log("clicked");
  });

  initDatepicker();
});

/*
function initDefaultDatatable () {
  $(".datatable").DataTable({
    scrollX: true,
    scrollCollapse: true,
    ordering: false,
    // lengthChange: false,
  });
}
*/

function dataTableSearch(value) {
  $("input[type=search].dt-input").val(value).trigger("input");
}

function refreshDatatableColumn() {
  DataTable.tables({ visible: true, api: true }).columns.adjust();
}

function initDatatable(options = {}) {
  const defaultLayout = {
    topStart: null,
    topEnd: "search",
    bottomStart: ["paging", "pageLength"],
    bottomEnd: "info",
  };
  const {
    selector = ".datatable",
    scrollable = true,
    layout = defaultLayout,
    paging = true,
    ordering = false,
    columnDefs = []
  } = options || {};
  const scrollableConfig = options.scrollable
    ? {
        scrollCollapse: true,
        scrollY: "60vh",
      }
    : {};
  const initCompleteConfig = options.initComplete ? {
    initComplete: options.initComplete
  } : {}
  const table = $(selector).DataTable({
    /*
      columnDefs: [
        { "orderable": false, "targets": 0 } // Make the sequence column non-orderable
      ],
      */
    scrollX: scrollable,
    scrollCollapse: scrollable,
    layout,
    paging,
    ordering,
    search: {
      smart: false,
    },
    columnDefs: [
      {
        targets: "noshow",
        visible: false,
      },
      ...columnDefs
    ],
    ...scrollableConfig,
    ...initCompleteConfig
  });

  /*
  table.on('order.dt search.dt', function () {
      table.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
          cell.innerHTML = i + 1;
      });
  }).draw();
  */

  return table;
}

function imagesPreview(input, placeToInsertImagePreview) {
  const galleryElement =
    placeToInsertImagePreview ||
    $(input).closest(".gallery-input").find(".preview-image-gallery");
  if (input.files) {
    var filesAmount = input.files.length;
    for (i = 0; i < filesAmount; i++) {
      const randomIdentifier = Date.now() + i;
      var reader = new FileReader();
      reader.onload = function (event) {
        const imageElement = $($.parseHTML("<img>")).attr({
          src: event.target.result,
        });
        const imageHtmlString = $("<div>").append(imageElement.clone()).html();

        const htmlToAppend = `
                <div class="single-image" data-identifier="${randomIdentifier}">
                  ${imageHtmlString}
                  <div class="delete-image-btn" onclick="deleteGalleryImage(${randomIdentifier})">
                    <i class="fa-solid fa-xmark"></i>
                  </div>
                </div>
              `;

        $(galleryElement).append(htmlToAppend);
      };
      reader.readAsDataURL(input.files[i]);

      //create single input file element so that images can be deleted seperately
      const singleInput = document.createElement("input");
      singleInput.type = "file";
      singleInput.name = `inspection_image[${randomIdentifier}]`;
      singleInput.required = true; // optional - add required attribute if needed
      singleInput.classList.add("singlefile");
      singleInput.setAttribute("identifier", randomIdentifier);

      // Create a DataTransfer object and add the file to it
      const dataTransfer = new DataTransfer();
      dataTransfer.items.add(input.files[i]);

      // Set the files property of the single file input to the created DataTransferItemList
      singleInput.files = dataTransfer.files;

      $(galleryElement).append(singleInput);
    }
  }
}

function deleteGalleryImage(key) {
  if (confirm("are you sure you want to delete this image?")) {
    $(`.gallery-input div[data-identifier=${key}]`).remove();
    $(`input[identifier=${key}]`).remove();
    $(`input[identifier=delete-${key}]`).val("true");
  }
}

function formatDate(date) {
  var dd = String(date.getDate()).padStart(2, "0");
  var mm = String(date.getMonth() + 1).padStart(2, "0"); //January is 0!
  var yyyy = date.getFullYear();

  return dd + "-" + mm + "-" + yyyy;
}

$.validator.setDefaults({
  errorClass: "error",
  errorPlacement: function (error, element) {
    return true; // Suppress error messages
  },
  ignore: "",
});

function debounce(func, wait) {
  let timeout;

  return function(...args) {
    const context = this;
    
    clearTimeout(timeout);
    
    timeout = setTimeout(() => {
      func.apply(context, args);
    }, wait);
  };
}
var urllogout = $('#urllogout').val();
var requestVerificationToken = $('#verification-token').val();

$(document).ready(function () {
    $("#btn-signout, #btn-signout-icon").click(function () {
        if (confirm('Are you sure you want to sign out?')) {
            $.ajax({
                url: urllogout,
                type: 'POST',
                headers: {
                    '__RequestVerificationToken': requestVerificationToken
                },
                success: function (apiresult) {
                    if (apiresult.isSuccess) {
                        var data = apiresult.data;
                        location.reload();
                    }
                },
            });
        }
    });
});

function getInputVal(id) {
  return $(id).val()
}

function checkPasswordValidation(password) {
  const passwordRegex = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_]).{10,}$/;
  return passwordRegex.test(password);
}