
$(document).ready(function () {

    //let table = new DataTable('#responsiveDataTable');

    //$('#responsiveDataTable').datatable();
    let requestUrl = (window.location.origin + "/AR/Customer/GetCustomersData");

    //$('#responsiveDataTable').DataTable({
    new DataTable('#responsiveDataTable', {
        processing: true,
        serverSide: true,
        ajax: {
            //url: '@Url.Action("GetCustomersData", "Customer")',
            url: requestUrl,
            type: 'POST'
        },
        columns: [
            // Your column definitions
            // Example: { data: 'title', name: 'Title', className: 'col-2' },
            {
                data: 'title', name: 'Title', className: 'col-2', render: function (data, type, row, meta) {
                    var customerId = row.id;
                    return '<a href="/AR/Customer/Detail/' + customerId + '">' + data + '</a>';
                }
            },
            { data: 'code', name: 'Code', className: 'col-1' },
            { data: 'gstno', name: 'GSTNo', className: 'col-1' },
            { data: 'billingaddress', name: 'BillingAddress', className: 'col-2 text-wrap text-break' },
            { data: 'emailaddress', name: 'EmailAddress', className: 'col-1' }

            //{ data: 'title'},
            //{ data: 'code'},
            //{ data: 'gstno' },
            //{ data: 'billingaddress' },
            //{ data: 'emailaddress' }
        ]
    });

});