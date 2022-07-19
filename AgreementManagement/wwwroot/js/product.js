var dataTable;

$(document).ready(function () {
    loadDataTable();
});

var formatter = new Intl.NumberFormat('en-US', {
    style: 'currency',
    currency: 'USD',

    // These options are needed to round to whole numbers if that's what you want.
    //minimumFractionDigits: 0, // (this suffices for whole numbers, but will print 2500.10 as $2,500.1)
    //maximumFractionDigits: 0, // (causes 2500.99 to be printed as $2,501)
});

formatter.format(2500); /* $2,500.00 */

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
             "url": "/Product/GetAll",
            "data": function (d) {
                console.log(d);
            }
        },
        "columns": [
            { "data": "prodGroup.groupCode", "width": "60px" },
            { "data": "productDescription", "width": "60px" },
            { "data": "productNumber", "width": "60px" },
            {
                "data": "price", "width": "60px",
                'render': function (price) {
                    return '$' + price;
                }
            },
            { "data": "active", "width": "60px" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                     <a href="/Product/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                       <i class="fas fa-edit"></i>
                         </a>
                     <a onclick=Delete("/Product/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
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
