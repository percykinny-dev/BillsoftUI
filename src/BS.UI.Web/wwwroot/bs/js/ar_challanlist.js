
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

$(document).on('click', '#searchchallan', function (e) {
    //alert('search challan');
    $.ajax({
        type: "POST",
        url: "/AR/Challan/GetChallansList", // replace with the actual URL of your controller
        data: {
            draw: 1,
            start: 0,
            length: 10,
            ChallanNo: document.getElementById("ChallanNo").value
            //CustomerName: $('#CustomerName').val(),
            //ProductName: $('#ProductName').val(),
        },
        success: function (response) {
            if ($.fn.dataTable) {
                // Check if the DataTable is already initialized
                if ($.fn.dataTable.isDataTable('#responsiveDataTable')) {
                    // If DataTable is initialized, just clear the data and add new data
                    table = $('#responsiveDataTable').DataTable();
                    table.clear();
                    table.rows.add(response.data);  // Assuming response.data contains the new array of rows
                    table.draw();
                } else {
                    // If DataTable is not initialized, initialize it
                    table = $('#responsiveDataTable').DataTable({
                        processing: true,
                        serverSide: true,
                        ajax: {
                            data: response.data, // Assuming response.data contains the array of rows
                            type: 'POST',
                            dataSrc: function (json) {
                                return json.data; // Adjust this as per your response structure
                            }
                        },
                        columns: [
                            // Define your columns here as before
                            {
                                data: 'challanno',
                                name: 'ChallanNo',
                                className: 'col-1',
                                //render: function (data, type, row, meta) {
                                //    return '<a href="/AR/Challan/Detail/' + row.challanid + '">' + data + '</a>';
                                //}
                            },
                            {
                                data: 'challandate',
                                name: 'ChallanDate',
                                className: 'col-1'
                            },
                            {
                                data: 'customername',
                                name: 'CustomerName',
                                className: 'col-2'
                            },
                            {
                                data: 'netamount',
                                name: 'NetAmount',
                                className: 'col-1 text-end'
                            },
                            {
                                data: 'cgstamount',
                                name: 'CGSTAmount',
                                className: 'col-1 text-end'
                            },
                            {
                                data: 'sgstamount',
                                name: 'SGSTAmount',
                                className: 'col-1 text-end'
                            },
                            {
                                data: 'igstamount',
                                name: 'IGSTAmount',
                                className: 'col-1 text-end'
                            },
                            {
                                data: 'gstamount',
                                name: 'GSTAmount',
                                className: 'col-1 text-end'
                            },
                            {
                                data: 'totalamount',
                                name: 'TotalAmount',
                                className: 'col-2 text-end'
                            },
                            {
                                data: 'challanid',
                                className: 'col-1 text-center',
                                render: function (data, type, row, meta) {
                                    return '<a href="/AR/Challan/ChallanAddEdit/' + data + '" onclick="handleClick(\'edit\')"><button class="btn btn-primary-light btn-icon ms-1 btn-sm invoice-edit-btn" ><i class="ri-edit-line"></i></button></a>';
                                }
                            },
                            {
                                data: 'challanid',
                                className: 'text-center',
                                render: function (data, type, row, meta) {
                                    return '<a href="/AR/Challan/ChallanAddEdit/' + data + '" onclick="handleClick(\'delete\')"><button class="btn btn-danger-light btn-icon ms-1 btn-sm invoice-delete-btn"><i class="ri-delete-bin-5-line"></i></button></a>';
                                }
                            }
                        ]
                    });
                }
            }
            else {
                console.error("DataTables library is not loaded.");
            }
           
        },
        error: function (xhr, status, error) {
            console.error("Error occurred while fetching data:", error);
        }
    });

});

$(document).ready(function () {
    // Date range
    flatpickr("#Challanfromdate", {});

    // Due range
    flatpickr("#Challantodate", {});

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