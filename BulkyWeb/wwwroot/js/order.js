var dataTable;

$(document).ready(function () {
    var params = new URLSearchParams(window.location.search);
    var status = "";

    if (params.has('status')) {
        status = params.get('status');
    }

    loadDataTable(status);
});

function loadDataTable(status) {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            url: '/Admin/Order/getall?status=' + status
        },
        "columns": [
            { data: 'id' },
            { data: 'name' },
            { data: 'phoneNumber' },
            { data: 'applicationUser.email' },
            { data: 'orderStatus' },
            { data: 'orderTotal' },
            {
                data: 'id',
                "render": function (data) {
                    return `<div classe="w-75 btn-group" role="group">
                        <a href="/admin/order/details?orderId=${data}" class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i> Details</a>
                    </div>`
                }
            }
        ]
    });
}
