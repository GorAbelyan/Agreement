var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Agreement/GetAll"
        },
        "columns": [
            { "data": "user.userName", "width": "60px" },
            { "data": "productGroup.groupCode", "width": "60px" },
            { "data": "product.productNumber", "width": "60px" },
            {
                'data': 'effectiveDate',
                'render': function (jsonDate) {
                    var date = new Date(parseInt(jsonDate.substr(6)));
                    var month = ("0" + (date.getMonth() + 1)).slice(-2);
                    return ("0" + date.getDate()).slice(-2) + '-' + month + '-' + date.getFullYear();
                }
            },
            {
                'data': 'expirationDate',
                'render': function (jsonDate) {
                    var date = new Date(parseInt(jsonDate.substr(6)));
                    var month = ("0" + (date.getMonth() + 1)).slice(-2);
                    return ("0" + date.getDate()).slice(-2) + '-' + month + '-' + date.getFullYear();
                }
            },
            {
                "data": "productPrice", "width": "60px",
                'render': function (price) {
                    return '$' + price;
                }
            },
            {
                "data": "newPrice", "width": "60px",
                'render': function (price) {
                    return '$' + price;
                }
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                     <a href="/Agreement/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                       <i class="fas fa-edit"></i>
                         </a>
                     <a onclick=Delete("/Agreement/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                    <i class="fas fa-trash-alt"></i>
                     </a>
                       </div> `;
                }, "width": "40px"
            }
        ]

    });
}
function Delete(url) {
    swal({
        title: "Are you sure you want to Delete?",
        text: "You will not be able to restore the data!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });

        }
    });
}
