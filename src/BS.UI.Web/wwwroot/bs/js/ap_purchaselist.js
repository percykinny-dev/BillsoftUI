
function ShowSearchModal() {

    $("#purchaseSearchModal").modal('show');
}


$(document).ready(function () {
    let requestUrl = (window.location.origin + "/AP/Purchase/GetPurchasesList");

    //$('#responsiveDataTable').DataTable({
    new DataTable('#responsiveDataTable', {
        processing: true,
        serverSide: true,
        ajax: {
            //url: '@Url.Action("GetData", "Data")',
            url: requestUrl,
            type: 'POST'

        },
        columns: [
            //billno, billdate, vendorname, cgstamount, sgstamount, igstamount, totalamount
            //{ data: 'billid', name: 'billid', className: 'col-1' },
            { data: 'billno', name: 'Bill No', className: 'col-1' },
            { data: 'billdate', name: 'Bill Date', className: 'col-1' },
            { data: 'vendorname', name: 'Vendor Name', className: 'col-2' },
            { data: 'netamount', name: 'Net Amt', className: 'col-1' },
            { data: 'cgstamount', name: 'CGST', className: 'col-1' },
            { data: 'sgstamount', name: 'SGST', className: 'col-1' },
            { data: 'igstamount', name: 'IGST', className: 'col-1' },
            { data: 'gstamount', name: 'GST', className: 'col-1' },
            { data: 'totalamount', name: 'Total', className: 'col-1 text-end' },
            {
                data: 'purchaseid', className: 'col-1 text-center', render: function (data, type, row, meta) {
                    //var challanId = row.id;
                    //return '<a  href="/AR/Challan/ChallanAddEdit/' + data + '">Edit</a>';
                    return '<a href="/AP/Purchase/PurchaseAddEdit/' + data + '"><button class="btn btn-primary-light btn-icon ms-1 btn-sm invoice-edit-btn" ><i class="ri-edit-line"></i></button></a>';
                }
            },
            {
                data: 'purchaseid', className: 'text-center', render: function (data, type, row, meta) {
                    //return '<a  href="/AP/Challan/Delete/' + data + '">Delete</a>';
                    return '<a href="/AP/Purchase/Delete/' + data + '"><button class="btn btn-danger-light btn-icon ms-1 btn-sm invoice-delete-btn"><i class="ri-delete-bin-5-line"></i></button></a>';

                }
            }

        ]
    });

});