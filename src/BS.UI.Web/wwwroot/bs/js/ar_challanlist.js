
function ShowSearchModal() {

    $("#challanSearchModal").modal('show');
}

function AddEditChallan(challanId) {
    window.location.href = '/AR/Challan/ChallanAddEdit/' + challanId;
}
$(document).ready(function () {

   
    let requestUrl = (window.location.origin + "/AR/Challan/GetChallansList");

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
                data: 'challanno', name: 'ChallanNo', className: 'col-2', render: function (data, type, row, meta) {
                    var challanId = row.id;
                    //return '<a href="/AR/Challan/ChallanAddEdit/' + challanId + '">' + data + '</a>';
                    return '<a href="#" onclick="AddEditChallan(' + challanId + ')">' + data + '</a>';

                }
            },
            { data: 'challandate', name: 'ChallanDate', className: 'col-1' },
            { data: 'customername', name: 'CustomerName', className: 'col-2' },
            { data: 'netamount', name: 'NetAmount', className: 'col-1 text-end' },
            { data: 'cgstamount', name: 'CGSTAmount', className: 'col-1 text-end' },
            { data: 'sgstamount', name: 'SGSTAmount', className: 'col-1 text-end' },
            { data: 'igstamount', name: 'IGSTAmount', className: 'col-1 text-end' },
            { data: 'gstamount', name: 'GSTAmount', className: 'col-1 text-end' },
            { data: 'totalamount', name: 'TotalAmount', className: 'col-1 text-end' }
   
        ]
    });

});