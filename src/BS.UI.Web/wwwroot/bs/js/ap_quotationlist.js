
function ShowSearchModal() {

    $("#quotationSearchModal").modal('show');
}


$(document).ready(function () {
    let requestUrl = (window.location.origin + "/AP/Quotation/GetQuotationsList");

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
            { data: 'billno', name: 'Quotation No', className: 'col-1' },
            { data: 'billdate', name: 'Quotation Date', className: 'col-1' },
            { data: 'vendorname', name: 'Vendor Name', className: 'col-2' },
            { data: 'netamount', name: 'Net Amt', className: 'col-1' },
            { data: 'cgstamount', name: 'CGST', className: 'col-1' },
            { data: 'sgstamount', name: 'SGST', className: 'col-1' },
            { data: 'igstamount', name: 'IGST', className: 'col-1' },
            { data: 'gstamount', name: 'GST', className: 'col-1' },
            { data: 'totalamount', name: 'Total', className: 'col-1 text-end' },
            {
                data: 'quotationid', className: 'col-1 text-center', render: function (data, type, row, meta) {
                    
                    return '<a href="/AP/Quotation/QuotationAddEdit/' + data + '"><button class="btn btn-primary-light btn-icon ms-1 btn-sm invoice-edit-btn" ><i class="ri-edit-line"></i></button></a>';
                }
            },
            {
                data: 'quotationid', className: 'text-center', render: function (data, type, row, meta) {

                    return '<a href="/AP/Quotation/Delete/' + data + '"><button class="btn btn-danger-light btn-icon ms-1 btn-sm invoice-delete-btn"><i class="ri-delete-bin-5-line"></i></button></a>';

                }
            }

        ]
    });

});