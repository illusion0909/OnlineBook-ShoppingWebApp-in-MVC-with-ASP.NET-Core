var dataTable;

$(document).ready(function () {
    loadDataTable();
})
function loadDataTable() {
    dataTable = $('#tblData').dataTable({
        "ajax": {
            "url": "/Admin/Product/GetAll"
        },
        "columns": [
            { "data": "title", "width": "20%" },
            { "data": "description", "width": "20%" },
            { "data": "author", "width": "20%" },
            { "data": "isbn", "width": "20%" },
            { "data": "price", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                            <a href="/Admin/Product/Upsert/${data}" class="btn btn-success">
                            <i class="fas fa-edit"></i>
                            </a>

                            <a onclick=Delete("/Admin/Product/Delete/${data}") class="btn btn-danger">
                            <i class="fas fa-trash-alt"></i>
                            </a>
                            </div>

                           `;
                }
            }
        ]
    })
}
function Delete(url) {
    swal({
        title: "Want to delete data?",
        text: "Delete Information!!",
        buttons: true,
        icon: "warning",
        dangerModel: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })

        }
    })
}