
function ShowSearchModal() {

    $("#itemSearchModal").modal('show');
}


$(document).ready(function () {

    let requestUrl = (window.location.origin + "/AR/Item/GetItemsList");

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
            //{ data: 'itemcode', name: 'ItemCode', className: 'col-1' },
            { data: 'hsnno', name: 'HSNNo', className: 'col-1' },
            { data: 'itemname', name: 'ItemName', className: 'col-3' },
            { data: 'rate', name: 'Rate', className: 'col-1' },
            { data: 'cgst', name: 'CGST', className: 'col-1' },
            { data: 'sgst', name: 'SGST', className: 'col-1' },
            { data: 'igst', name: 'IGST', className: 'col-1' },
            { data: 'uom', name: 'UOM', className: 'col-1 ' },
            { data: 'category', name: 'Category', className: 'col-1 text-end' },
            {
                data: 'itemid', className: 'col-1 text-center', render: function (data, type, row, meta) {
                  
                    return '<a href="/AR/Item/ItemAddEdit/' + data + '"><button class="btn btn-primary-light btn-icon ms-1 btn-sm invoice-edit-btn" ><i class="ri-edit-line"></i></button></a>';
                }
            },
            {
                data: 'itemid', className: 'text-center', render: function (data, type, row, meta) {
                    return '<a href="/AR/Item/Delete/' + data + '"><button class="btn btn-danger-light btn-icon ms-1 btn-sm invoice-delete-btn"><i class="ri-delete-bin-5-line"></i></button></a>';

                }
            }

        ]
    });

});