
function ShowSearchModal() {

    $("#challanSearchModal").modal('show');
}

function AddEditChallan(challanId) {
    window.location.href = '/AR/Challan/ChallanAddEdit/' + challanId;
}

function Delete(challanId) {
    window.location.href = '/AR/Challan/Delete/' + challanId;
}

function handleClick(action) {
    if (action === 'edit') {
        // Handle the edit action
        console.log('Edit button clicked');
        localStorage.setItem('actionMode', 'edit');
        sessionStorage.setItem('actionMode', 'edit');
        // Perform further actions like opening the edit form or setting values, etc.
    } else if (action === 'delete') {
        // Handle the delete action
        console.log('Delete button clicked');
        localStorage.setItem('actionMode', 'delete');
        sessionStorage.setItem('actionMode', 'delete');
        // Perform further actions like showing a confirmation dialog, deleting an item, etc.
        //alert('Are you sure you want to delete this Challan?');
    }
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
            //// Your column definitions
            //// Example: { data: 'title', name: 'Title', className: 'col-2' },
            //{
            //    data: 'challanno', name: 'ChallanNo', className: 'col-2', render: function (data, type, row, meta) {
            //        var challanId = row.id;
            //        return '<a href="/AR/Challan/Detail/' + challanId + '">' + data + '</a>';
            //        //return '<a  href="/AR/Challan/Detail/' + data + '">Edit</a>';
            //    }
            //},
            //{ data: 'challanid', name: 'ChallanID', className: 'col-1 d-none' },
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
                data: 'challanid', className: 'col-1 text-center', render: function (data, type, row, meta) {
                    //var challanId = row.id;
                    //return '<a  href="/AR/Challan/ChallanAddEdit/' + data + '">Edit</a>';
                    return '<a href="/AR/Challan/ChallanAddEdit/' + data + '" onclick="handleClick(\'edit\')"><button class="btn btn-primary-light btn-icon ms-1 btn-sm invoice-edit-btn" ><i class="ri-edit-line"></i></button></a>';
                }
            },
            {
                data: 'challanid', className: 'text-center', render: function (data, type, row, meta) {
                    //return '<a  href="/AR/Challan/Delete/' + data + '">Delete</a>';
                    return '<a href="/AR/Challan/ChallanAddEdit/' + data + '" onclick="handleClick(\'delete\')"><button class="btn btn-danger-light btn-icon ms-1 btn-sm invoice-delete-btn"><i class="ri-delete-bin-5-line"></i></button></a>';

                }
            }

        ]
    });

});

/*
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

*/