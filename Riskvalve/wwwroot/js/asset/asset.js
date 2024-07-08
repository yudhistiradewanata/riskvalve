const urlassetdatatable = getInputVal('#urlassetdatatable')

$(document).ready(function () {
  $('#assetTable').DataTable({
      "processing": true,
      "serverSide": true,
      "order": [1],
      "ajax": {
          "url": urlassetdatatable,
          "type": "POST",
          "datatype": "json",
          "data": function (d) {
              d.searchValue = $('#assetTable').DataTable().search();
              d.sortColumn = $('#assetTable').DataTable().order()[0][0];
              d.sortColumnDirection = $('#assetTable').DataTable().order()[0][1];
          },
          "headers": {
              '__RequestVerificationToken': requestVerificationToken
          },
      },
      "columns": [
          {
              "data": null, 
              "orderable": false, 
              "render": function (data, type, row, meta) { 
                  return meta.row + 1;
              }
          },
          { "data": "tagNo" },
          { "data": "businessArea" },
          { "data": "platform" },
          { "data": "valveType" },
          { "data": "size" },
          { "data": "classRating" },
          { "data": "pidNo" },
          { "data": "parentEquipmentNo" },
          { "data": "parentEquipmentDescription" },
          { "data": "serviceFluid" },
          {
              "data": "id",
              "orderable": false,
              "render": function (data, type, row) {
                  return '<button onclick="editAction.apply(this)" class="btn btn-custom-size btn-primary invert btn-with-icon btn-asset-action" attr-mode="view" attr-itemid="' + data + '"><i class="fa-solid fa-pencil"></i>Edit</button>';
              }
          }
      ],
      "createdRow": function (row, data, dataIndex) {
          var info = $(this).DataTable().page.info();
          var pageNumber = info.page;
          var entriesPerPage = info.length;
          var cell = $('td', row).eq(0);
          cell.html(((pageNumber) * entriesPerPage) + dataIndex + 1);
      },
      "layout": {
          topStart: null,
          topEnd: "search",
          bottomStart: ["paging", "pageLength"],
          bottomEnd: "info",
      },
  });
});