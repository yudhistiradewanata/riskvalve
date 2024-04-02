﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function initDatepicker () {
  $(".datepicker").datepicker({
    format: "dd-mm-yyyy",
    autoclose: true,
    todayHighlight: true,
  });
}

$(document).ready(function () {
  $(".btn-inspection").click(function () {
    console.log("clicked");
  });

  initDatepicker()  
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

function dataTableSearch (value) {
  $('input[type=search].dt-input').val(value).trigger('input')
}

function initDatatable (options) {
  const defaultLayout = {
    topStart: null,
    topEnd: 'search',
    bottomStart: ['paging', 'pageLength'],
    bottomEnd: 'info'
}
  const {
    selector = '.datatable',
    scrollable = true,
    layout = defaultLayout
  } = options || {}
  const table = $(selector).DataTable({
      scrollX: scrollable,
      scrollCollapse: scrollable,
      ordering: false,
      layout
  });

  return table
}

function imagesPreview (input, placeToInsertImagePreview) {
  const galleryElement = placeToInsertImagePreview || $(input).closest('.gallery-input').find('.preview-image-gallery')
  if (input.files) {
      var filesAmount = input.files.length;
      for (i = 0; i < filesAmount; i++) {
          const randomIdentifier = Date.now() + i
          var reader = new FileReader();
          reader.onload = function(event) {
              const imageElement = $($.parseHTML('<img>')).attr({
                  src: event.target.result
              })
              const imageHtmlString = $('<div>').append(imageElement.clone()).html();
              
              const htmlToAppend = `
                <div class="single-image" data-identifier="${randomIdentifier}">
                  ${imageHtmlString}
                  <div class="delete-image-btn" onclick="deleteGalleryImage(${randomIdentifier})">
                    <i class="fa-solid fa-xmark"></i>
                  </div>
                </div>
              `

              $(galleryElement).append(htmlToAppend)
          }
          reader.readAsDataURL(input.files[i]);

          //create single input file element so that images can be deleted seperately
          const singleInput = document.createElement('input');
          singleInput.type = 'file';
          singleInput.name = `inspection_image[${randomIdentifier}]`;
          singleInput.required = true; // optional - add required attribute if needed
          singleInput.classList.add('singlefile');
          singleInput.setAttribute('identifier', randomIdentifier)
          
          // Create a DataTransfer object and add the file to it
          const dataTransfer = new DataTransfer();
          dataTransfer.items.add(input.files[i]);

          // Set the files property of the single file input to the created DataTransferItemList
          singleInput.files = dataTransfer.files;

          $(galleryElement).append(singleInput)
      }
  }
};

function deleteGalleryImage(key) {
  if(confirm('are you sure you want to delete this image?')) {
    $(`.gallery-input div[data-identifier=${key}]`).remove()
    $(`input[identifier=${key}]`).remove()
    $(`input[identifier=delete-${key}]`).val('true')
  }
}