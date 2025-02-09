
function ShowSearchModal() {

    $("#invoiceSearchModal").modal('show');
}

function AddEditInvoice(invoiceId) {
    window.location.href = '/AR/Invoice/InvoiceAddEdit/' + invoiceId;
}

$(document).ready(function () {
    alert("document ready : ar_invoicelist.js");

    let requestUrl = (window.location.origin + "/AR/Invoice/GetInvoicesList");

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
            //// Your column definitions
            //// Example: { data: 'title', name: 'Title', className: 'col-2' },
            //{
            //    data: 'invoiceno', name: 'InvoiceNo', className: 'col-2', render: function (data, type, row, meta) {
            //        var invoiceId = row.id;
            //        return '<a href="/AR/Invoice/Detail/' + invoiceId + '">' + data + '</a>';
            //        //return '<a  href="/AR/Invoice/Detail/' + data + '">Edit</a>';
            //    }
            //},
            //{ data: 'invoiceid', name: 'InvoiceID', className: 'col-1 d-none' },
            { data: 'invoiceno', name: 'InvoiceNo', className: 'col-1' },
            { data: 'invoicedate', name: 'InvoiceDate', className: 'col-1' },
            { data: 'challanno', name: 'ChallanNo', className: 'col-1' },
            { data: 'challandate', name: 'ChallanDate', className: 'col-1' },
            { data: 'customername', name: 'CustomerName', className: 'col-2' },
            { data: 'netamount', name: 'NetAmount', className: 'col-1 text-end' },
            { data: 'cgstamount', name: 'CGSTAmount', className: 'col-1 text-end' },
            { data: 'sgstamount', name: 'SGSTAmount', className: 'col-1 text-end' },
            { data: 'igstamount', name: 'IGSTAmount', className: 'col-1 text-end' },
            { data: 'gstamount', name: 'GSTAmount', className: 'col-1 text-end' },
            { data: 'totalamount', name: 'TotalAmount', className: 'col-2 text-end' }
            ,
            {
                data: 'invoiceid', className: 'col-1 text-center', render: function (data, type, row, meta) {
                    //var invoiceId = row.id;
                    //return '<a  href="/AR/Invoice/InvoiceAddEdit/' + data + '">Edit</a>';
                    return '<a href="/AR/Invoice/InvoiceAddEdit/' + data + '"><button class="btn btn-primary-light btn-icon ms-1 btn-sm invoice-edit-btn" ><i class="ri-edit-line"></i></button></a>';
                }
            },
            {
                data: 'invoiceid', className: 'text-center', render: function (data, type, row, meta) {
                    //return '<a  href="/AR/Invoice/Delete/' + data + '">Delete</a>';
                    return '<a href="/AR/Invoice/Delete/' + data + '"><button class="btn btn-danger-light btn-icon ms-1 btn-sm invoice-delete-btn"><i class="ri-delete-bin-5-line"></i></button></a>';

                }
            }

        ]
    });

});