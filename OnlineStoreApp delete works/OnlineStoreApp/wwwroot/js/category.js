// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var dataTable;

$('#tblData').DataTable().destroy();

$(document).ready(function () {
    loadDataTable();
});



function loadDataTable() {
    dataTable = $('#tblData').dataTable({
        "ajax": {
            "url": "/Admin/Category/GetAll"
        },
        "columns": [
            { "data": "name", "width": "60%" },
            {
                "data": "id", "render": function (data) {
                    return `<div class="text-center">
                    <a href="/Admin/Category/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                    <i class="fa-solid fa-pen"></i></a>

                    <a  onclick=Delete("/Admin/Category/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                        <i class="fa-solid fa-trash-can"></i></a>
                   </div>   
                          `;
                }, "width": "40%"
            },
        ]
    });
}


function Delete(url) {
    swal({
        title: "Are you sure?",
        text: "You will not be able to restore the data!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) { // Corrected syntax here
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}




