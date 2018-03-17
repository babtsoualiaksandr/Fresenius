//// Write your JavaScript code.
$(document).ready(function () {
    $('#IndexTable').DataTable();
});

//$(document).ready(function () {
//    var table = $('#indexTableExp').DataTable();

//    $('#indexTableExp tbody').on('click', 'tr', function () {
//        var data = table.row(this).data();
//        alert('Потробности  ' + data[0] + data[1] + data[2] +'\'s row');
//    });
//});

//$(document).ready(function () {
//    var table = $('#indexTable').DataTable({
//        "scrollY": "200px",
//        "paging": false
//    });

//    $('a.toggle-vis').on('click', function (e) {
//        e.preventDefault();

//        // Get the column API object
//        var column = table.column($(this).attr('data-column'));

//        // Toggle the visibility
//        column.visible(!column.visible());
//    });
//});

//$(document).ready(function () {
//    $('#indexTable').DataTable({
//        responsive: {
//            details: {
//                display: $.fn.dataTable.Responsive.display.modal({
//                    header: function (row) {
//                        var data = row.data();
//                        return 'Details for ' + data[0] + ' ' + data[1];
//                    }
//                }),
//                renderer: $.fn.dataTable.Responsive.renderer.tableAll({
//                    tableClass: 'table'
//                })
//            }
//        }
//    });
//});

