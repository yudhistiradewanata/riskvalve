const urlassetdatatable = getInputVal("#urlassetdatatable");

const buttonexport = $("#btnExcelAsset");
buttonexport.click(function () {
  $.ajax({
    url: urlassetdatatable,
    headers: {
      __RequestVerificationToken: requestVerificationToken,
    },
    type: "POST",
    data: {
      length: -1, // This tells the server to return all records
      searchValue: $("#assetTable").DataTable().search(),
      sortColumn: $("#assetTable").DataTable().order()[0][0],
      sortColumnDirection: $("#assetTable").DataTable().order()[0][1],
    },
    success: function (data) {
      var exportData = data.data;
      var table = $("#assetTable").DataTable();

      // Clear the table and add the full dataset for exporting
      table.clear();
      table.rows.add(exportData);
      table.draw();

      // Trigger the export button
      table.button(".buttons-excel").trigger();

      // Revert the table to the original data (if needed)
      table.ajax.reload();
    },
  });
});

$(document).ready(function () {
  var searchColumnValue = {};
  var searchColumn = {};
  var table = $("#assetTable").DataTable({
    buttons: [
      {
        extend: "excel",
        title: "Asset Data",
        exportOptions: {
            columns: ':not(.notexport)',
            format: {
                body: function (data, row, column, node) {
                    if ($(node).hasClass("export-html")) {
                        data = $(node).attr("real-val");
                    }
                    if ($(node).hasClass("export-numeric")) {
                        return data.replace(/([.])/g, '').replace(/([,])/g, '.');
                    }
                    return data;
                }
            }
        }
      },
    ],
    processing: true,
    serverSide: true,
    order: [1],
    ajax: {
      url: urlassetdatatable,
      type: "POST",
      datatype: "json",
      data: function (d) {
        d.searchValue = $("#assetTable").DataTable().search();
        d.sortColumn = $("#assetTable").DataTable().order()[0][0];
        d.sortColumnDirection = $("#assetTable").DataTable().order()[0][1];
        d.searchColumnValues = JSON.stringify(searchColumnValue);
        d.searchColumns = JSON.stringify(searchColumn);
      },
      headers: {
        __RequestVerificationToken: requestVerificationToken,
      },
    },
    columns: [
      {
        data: null,
        orderable: false,
        render: function (data, type, row, meta) {
          return meta.row + 1;
        },
      },
      { data: "tagNo" },
      { data: "businessArea" },
      { data: "platform" },
      { data: "valveType" },
      { data: "size" },
      { data: "assetName" },
      { data: "status" },
      { data: "actuation" },
      { data: "classRating" },
      { data: "parentEquipmentNo" },
      { data: "parentEquipmentDescription" },
      { data: "installationDate" },
      { data: "pidNo" },
      { data: "costOfReplacementAndRepair" },
      { data: "usageType" },
      { data: "manufacturer" },
      { data: "bodyModel" },
      { data: "bodyMaterial" },
      { data: "endConnection" },
      { data: "serialNo" },
      { data: "manualOverride" },
      { data: "actuatorMfg" },
      { data: "actuatorSerialNo" },
      { data: "actuatorTypeModel" },
      { data: "actuatorPower" },
      { data: "operatingTemperature" },
      { data: "operatingPressure" },
      { data: "flowRate" },
      { data: "serviceFluid" },
      { data: "fluidPhase" },
      { data: "toxicOrFlamableFluid" },
      {
        data: "id",
        orderable: false,
        render: function (data, type, row) {
          return (
            '<button onclick="editAction.apply(this)" class="btn btn-custom-size btn-primary invert btn-with-icon btn-asset-action" attr-mode="view" attr-itemid="' +
            data +
            '"><i class="fa-solid fa-pencil"></i>Edit</button>'
          );
        },
      },
    ],
    columnDefs: [
      {
        targets: "noshow",
        visible: false,
      },
    ],
    createdRow: function (row, data, dataIndex) {
      var info = $(this).DataTable().page.info();
      var pageNumber = info.page;
      var entriesPerPage = info.length;
      var cell = $("td", row).eq(0);
      cell.html(pageNumber * entriesPerPage + dataIndex + 1);
    },
    layout: {
      topStart: null,
      topEnd: "search",
      bottomStart: ["paging", "pageLength"],
      bottomEnd: "info",
    },
    scrollX: true,
    initComplete: function () {
      const api = this.api();
      const headerRow = $(api.table().header()).find('tr').first();
      const headerCells = headerRow.find('th');
      const searchRow = $('<tr/>').appendTo(headerRow.parent());
      headerCells.each(function (index) {
        const header = $(this);
        const title = header.text();
        const searchCell = $('<th/>').appendTo(searchRow);
        if (header.css('display') === 'none' || index === 0 || index === headerCells.length - 1) {
            return;
        }
        $('<input type="text" placeholder="Search ' + title + '" />')
            .appendTo(searchCell)
            .on('keyup change clear', debounce(function () {
              const curridx = header.attr('data-dt-column');
              const currname = header.attr('data-column-name');
              const searchValue = this.value;
              searchColumnValue[curridx] = searchValue;
              searchColumn[curridx] = currname;
              table.ajax.reload();
            }, 500));
    });
    api.draw();
      // this.api().columns(':not(:first-child):not(:last-child)').every(function () {
      //   var column = this;
      //   var input = $('<input type="text" placeholder="Search...">')
      //     .appendTo($(column.header()))
      //     .on('keyup change clear', debounce(function () {
      //       // if (column.search() !== this.value) {
      //         searchColumnValue[column.index()] = this.value;
      //         searchColumn[column.index()] = $(column.header()).attr('data-column-name');
      //         table.ajax.reload();
      //       // }
      //     },500)
      //   );
      // });
      // this.api().draw();
    },
  });
});
