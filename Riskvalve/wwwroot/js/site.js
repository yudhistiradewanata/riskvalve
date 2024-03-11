// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
  $(".datatable").DataTable({
    scrollX: true,
    scrollCollapse: true,
    ordering: false,
    // lengthChange: false,
  });

  $(".btn-inspection").click(function () {
    console.log("clicked");
  });

  $(".datepicker").datepicker({
    format: "yyyy-mm-dd",
    autoclose: true,
    todayHighlight: true,
  });
});
