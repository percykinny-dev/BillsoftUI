function ShowSearchModal() {

    $("#challanSearchModal").modal('show');
}

function ShowHideAddress() {
    let customerId = document.getElementById("customerID");
    let addressRow = document.getElementById('addressRow');

    var isCustomerSelected = (customerId.value !== '');

    // Toggle the 'hidden-element' class based on the selection
    if (isCustomerSelected) {
        clearAddressFields();
        document.getElementById('selCustomerId').value = customerId.value;

        getCustomerDefaultAddress(customerId.value)
            .then(function (defaultAddressList) {
                if (defaultAddressList !== null) {
                    defaultAddressList.forEach(function (address) {
                        updateAddressFields(address);
                    });
                }
            })
            .catch(function (error) {
                console.error(error);
            });

        addressRow.classList.remove('hidden-element');
    } else {
        addressRow.classList.add('hidden-element');
        document.getElementById('selCustomerId').value = '';
    }
}

function getCustomerDefaultAddress(customerId) {
    return new Promise(function (resolve, reject) {
        let requestBody = { "customerId": customerId };
        $('#loader').show();
        $.ajax({
            type: "POST",
            dataType: "json",
            url: (window.location.origin + "/AR/Customer/GetCustomerDefaultAddresses"),
            data: requestBody,
            success: function (response) {
                $('#loader').hide();
                if (response.success) {
                    let customerAddress = response.data;
                    resolve(customerAddress);
                } else {
                    reject(new Error("Failed to retrieve customer addresses."));
                }
            },
            error: function (er) {
                $('#loader').hide();
                reject(new Error("Failed to retrieve customer addresses."));
            }
        });
    });
}


// Call the function to populate the address list when the modal is shown
//$('#addressModal').on('shown.bs.modal', function () {
//    populateAddressList();
//});

// Function to dynamically populate and format the address list

function getAddressList(addressType) {
    //addressListContainer.innerHTML = '';
    console.log("addressType: " + addressType.toString());

    var customerId = document.getElementById('selCustomerId').value;
    let addresses = null;
    return new Promise(function (resolve, reject) {
        let requestBody = { "customerId": customerId, "addressType": addressType };


        $('#loader').show();
        $.ajax({
            type: "POST",
            dataType: "json",
            url: (window.location.origin + "/AR/Customer/GetCustomerAddressList"),
            data: requestBody,
            success: function (response) {
                $('#loader').hide();
                if (response.success) {
                    addresses = response.data;
                    resolve(addresses);
                }
            },
            error: function (er) {
                $('#loader').hide();
                reject(new Error("Failed to retrieve customer addresses."));
            }
        });
    });
}

function populateAddressList(addresses) {
    //var addressListContainer = document.getElementById("addressListContainer");

    // Clear existing content
   
    // Sample JSON array of addresses
    //var addresses = [
    //    {
    //        Address1: "123 Main St",
    //        Address2: "Apt 4",
    //        City: "Cityville",
    //        State: "CA",
    //        Zip: "12345",
    //        Phone: "555-1234"
    //    },
    //    {
    //        Address1: "Krishna Nagar",
    //        Address2: "Saki Naka",
    //        City: "Mumbai",
    //        State: "MH",
    //        Zip: "400027",
    //        Phone: "9819179555"
    //    },
    //    {
    //        Address1: "23/6-7, 2nd Floor,",
    //        Address2: " EAST PATEL NAGAR",
    //        City: "Delhi",
    //        State: "Delhi",
    //        Zip: "110008",
    //        Phone: "7789179555"
    //    },
    //    {
    //        Address1: "4022 4TH FLR ONE AEROCITY,CORPORATE PARK",
    //        Address2: "SAFED POOL ANDHERI KURLA RD SAKINAKA",
    //        City: "Mumabi",
    //        State: "MH",
    //        Zip: "400072",
    //        Phone: "7208968391"
    //    },
    //    // Add more addresses as needed
    //];


    // Get the address list container
    var addressList = document.querySelector('.list-group');

    // Clear existing list items
    addressList.innerHTML = '';

    // Loop through addresses and create list items
    addresses.forEach(function (address, index) {
        console.log("Address index", index);
        var listItem = document.createElement("label");
        listItem.classList.add('list-group-item');

        var radioInput = document.createElement("input");
        radioInput.type = "radio";
        radioInput.className = "form-check-input me-1 pr-2 float-start";
        radioInput.value = index;
        radioInput.name = "list-radio";
        listItem.setAttribute("data-index", index.toString());

        if (index === 0) {
            radioInput.checked = true; // Check the first radio button by default
            listItem.classList.add('bg-primary-transparent');
        }

        // Create a div for address details
        var addressDetails = document.createElement('div');
        //addressDetails.classList.add('float-end');
        addressDetails.style = "padding-left: 3rem";

        // Add address details to the div
        addressDetails.innerHTML = `
            <div>${address.address1}</div>
            <div>${address.address2}</div>
            <div>${address.city}, ${address.state} ${address.zipcode}</div>
            <div>${address.phone}</div>
        `;

        listItem.appendChild(radioInput);

        // Append the radio input and address details to the list item
        listItem.appendChild(radioInput);
        listItem.appendChild(addressDetails);

        // Append the list item to the address list container
        addressList.appendChild(listItem);

        // Add a click event listener to each list item
        listItem.addEventListener('click', function () {
            // Remove 'selected' class from all items
            document.querySelectorAll('.list-group-item').forEach(item => {
                item.classList.remove('bg-primary-transparent');
            });

            // Add 'selected' class to the clicked item
            listItem.classList.add('bg-primary-transparent');

            let selectedIndex = Number(listItem.getAttribute("data-index"));

            var btnOk = document.getElementById('selectAddressYes');

            btnOk.addEventListener('click', function () {
                updateAddressFields(addresses[selectedIndex]);
                // Close the modal
                $('#addressModal').modal('hide');
            });
        });

    });
}

function ShowAddressList(addressType) {

    //populateAddressList(addressType);
    getAddressList(addressType)
        .then(function (customerAddressList) {
            if (customerAddressList !== null) {
                populateAddressList(customerAddressList);
            }
        })
        .catch(function (error) {
            console.error(error);
        });


    $("#addressModal").modal('show');
}

function updateAddressFields(selectedAddress) {
    //clearAddressFields();

    if (selectedAddress.addressType == 1) {
        document.getElementById('billAddress').value = selectedAddress.address1 + ' ' + selectedAddress.address2 + ',' + selectedAddress.city + ',' + selectedAddress.state;
        document.getElementById('billZipcode').value = selectedAddress.zipcode;
        document.getElementById('billContact').value = selectedAddress.contactName;
        document.getElementById('billEmailAddress').value = selectedAddress.emailAddress;
        document.getElementById('billPhone').value = selectedAddress.workPhone;
    }
    else {
        document.getElementById('shipAddress').value = selectedAddress.address1 + ' ' + selectedAddress.address2 + ',' + selectedAddress.city + ',' + selectedAddress.state;
        document.getElementById('shipZipcode').value = selectedAddress.zipcode;
        document.getElementById('shipContact').value = selectedAddress.contactName;
        document.getElementById('shipEmailAddress').value = selectedAddress.emailAddress;
        document.getElementById('shipPhone').value = selectedAddress.workPhone;
    }
   
}

function clearAddressFields() {
    document.getElementById('billAddress').value = '';
    document.getElementById('billZipcode').value = '';
    document.getElementById('billContact').value = '';
    document.getElementById('billEmailAddress').value = '';
    document.getElementById('billPhone').value = '';

    document.getElementById('shipAddress').value = '';
    document.getElementById('shipZipcode').value = '';
    document.getElementById('shipContact').value = '';
    document.getElementById('shipEmailAddress').value = '';
    document.getElementById('shipPhone').value = '';

}

function addNewRow() {

    if (!validateProductDetails()) {
        setTimeout(function () {
            $('#solid-dangerToast').show();

        },5);
        /*$('#solid-dangerToast').show();*/
        return;
    }

    //// Get the values from the parent table row
    //var productName = $('#itemId1 option:selected').text();
    //var productNameVal = $('#itemId1 option:selected').val();
    //var hsnNo = $('#hsnNo').val();
    //var uom = $('#uom option:selected').text();
    //var uomVal = $('#uom option:selected').val();
    //var quantity = $('#qty').val();
    //var rate = $('#rate').val();

    //var pattern = /\[(\d+)%\]/;
    //var tax = extractNumericValue($('#taxType option:selected').text(), pattern) || 0;
    //var taxString = $('#taxType option:selected').text();

    //var taxVal = $('#taxType option:selected').val();
    //var total = $('#rowTotal').val();

    //var rowCount = $('#childTableBody tr').length;

    var rowValues = getRowValues();
    
    // Construct the HTML for the new row in the child table
    var newRowHtml = '<tr>';

    newRowHtml += '<td><input type="checkbox" id="row' + rowValues.rowCount + '" class="form-check-input"></td>'; // Checkbox column
    newRowHtml += '<td data-productname="' + rowValues.productNameVal + '" ><a href="#" onclick="selectRow(' + rowValues.rowCount + ');">' + rowValues.productName + '</a></td>'; // Product Name
    newRowHtml += '<td data-hsnno="' + rowValues.hsnNo + '">' + rowValues.hsnNo + '</td>'; // HSN No
    newRowHtml += '<td data-uom="' + rowValues.uomVal + '">' + rowValues.uom + '</td>'; // UOM
    newRowHtml += '<td data-qty="' + rowValues.quantity + '">' + rowValues.quantity + '</td>'; // Quantity
    newRowHtml += '<td data-rate="' + rowValues.rate + '">' + rowValues.rate + '</td>'; // Rate
    newRowHtml += '<td data-tax="' + rowValues.taxVal + '" data-gst="' + rowValues.tax + '">' + rowValues.taxString + '</td>'; // Tax
    newRowHtml += '<td data-rowTotal="' + rowValues.total + '">' + rowValues.total + '</td>'; // Total
    newRowHtml += '</tr>';

    // Append the new row to the child table
    $('#childTableBody').append(newRowHtml);

    recalculateTotals();
}

function getRowValues() {
    var productName = $('#itemId1 option:selected').text();
    var productNameVal = $('#itemId1 option:selected').val();
    var hsnNo = $('#hsnNo').val();
    var uom = $('#uom option:selected').text();
    var uomVal = $('#uom option:selected').val();
    var quantity = $('#qty').val();
    var rate = $('#rate').val();

    var pattern = /\[(\d+)%\]/;
    var tax = extractNumericValue($('#taxType option:selected').text(), pattern) || 0;
    var taxString = $('#taxType option:selected').text();

    var taxVal = $('#taxType option:selected').val();
    var total = $('#rowTotal').val();

    var rowCount = $('#childTableBody tr').length;

    return {
        productName: productName,
        productNameVal: productNameVal,
        hsnNo: hsnNo,
        uom: uom,
        uomVal: uomVal,
        quantity: quantity,
        rate: rate,
        tax: tax,
        taxString: taxString,
        taxVal: taxVal,
        total: total,
        rowCount: rowCount
    };
}

// Function to extract numeric value from GST [%] value
function extractNumericValue(inputString, pattern) {
    var match = inputString.match(pattern);
    if (match && match.length > 1) {
        return parseInt(match[1]);
    } else {
        return null; // Return null if no match found
    }
}

function closeErrorDiv() {
    $('#solid-dangerToast').hide();
}

$('body').on('click', function (e) {
    // Check if the click event occurred outside the toast container
    if (!$(e.target).closest('.toast-container').length) {
        // If so, hide the toast
        $('#solid-dangerToast').hide();
    }
});

function selectRow(rowIndex) {

    $('#selRowIndex').val(rowIndex);

    var row = $('#childTableBody tr').eq(rowIndex);
    var rowData = {
        itemName: row.find('[data-productname]').data('productname'),
        hsnNo: row.find('[data-hsnno]').data('hsnno'),
        uom: row.find('[data-uom]').data('uom')+'',
        qty: row.find('[data-qty]').data('qty'),
        rate: row.find('[data-rate]').data('rate'),
        tax: row.find('[data-tax]').data('tax')+'',
        rowTotal: row.find('[data-rowtotal]').data('rowtotal'),
        // Add other fields here
    };
    console.log(rowData);
    console.log(rowData.itemName);

    //var itemNameTxt = row.find('td:nth-child(1)').textContent;

    //$('#itemId1').val(rowData.itemName);
    $('#hsnNo').val(rowData.hsnNo);
    //$('#uom option:selected').val(rowData.uom);
    $('#qty').val(rowData.qty);
    $('#rate').val(rowData.rate);
    //$('#taxType option:selected').val(rowData.tax);
    $('#rowTotal').val(rowData.rowTotal);

    itemDropdown.setChoiceByValue(rowData.itemName);
    uomDropdown.setChoiceByValue(rowData.uom);
    taxDropdown.setChoiceByValue(rowData.tax);
 
    //console.log(productName, hsnNo, uom, quantity, rate, tax, total);
}

function editSelectedRow() {

    var rowIndex = parseInt(document.getElementById('selRowIndex').value);
    if (isNaN(rowIndex)) { return };

    var row = $('#childTableBody tr').eq(rowIndex);

    var rowValues = getRowValues();

    // Update cell values in the row
    var productHtml = '<a href="#" onclick="selectRow(' + rowIndex + ');">' + rowValues.productName + '</a>'
    row.find('td[data-productname]').innerHTML = productHtml;
    row.find('td[data-hsnno]').text(rowValues.hsnNo);
    row.find('td[data-uom]').text(rowValues.uom);
    row.find('td[data-qty]').text(rowValues.quantity);
    row.find('td[data-rate]').text(rowValues.rate);
    row.find('td[data-tax]').text(rowValues.taxString);
    row.find('td[data-rowtotal]').text(rowValues.total);

    row.find('td[data-productname]').data("productname", rowValues.productNameVal);
    row.find('td[data-hsnno]').data("hsnno", rowValues.hsnNo);
    row.find('td[data-uom]').data("uom", rowValues.uomVal);
    row.find('td[data-qty]').data("qty", rowValues.quantity);
    row.find('td[data-rate]').data("rate", rowValues.rate);
    row.find('td[data-tax]').data("tax", rowValues.taxVal);
    row.find('td[data-gst]').data("gst", rowValues.tax);
    row.find('[data-rowtotal]').data("rowtotal", rowValues.total);

    recalculateTotals();

}



function recalculateTotals() {

    let grandTotal = 0;
    let subTotal = 0;
    let cgstTotal = 0;
    let sgstTotal = 0;
    var rowTotal = 0;
    var gstRate = 0;
    var discount = 0;

    $('#childTableBody tr').each(function (rowIndex, row) {
        // Initialize an object to store the column values of the current row
        var rowData = {};

        rowTotal = parseFloat($(row).find('[data-rowtotal]').data('rowtotal'));
        subTotal = parseFloat(subTotal) + rowTotal;
        gstRate = parseFloat($(row).find('[data-gst]').data('gst'));
        cgstTotal = cgstTotal + (rowTotal * (gstRate * 0.5)/100);
        sgstTotal = sgstTotal + (rowTotal * (gstRate * 0.5)/100);
    });

    if ($('#discount').val() !='' && !isNaN(($('#discount').val()))){
        discount = parseFloat($('#discount').val()).toFixed(2);
    }

    grandTotal = subTotal + cgstTotal + sgstTotal - discount;

    $('#subTotal').val(subTotal.toFixed(2));
    $('#cgst').val(cgstTotal.toFixed(2));
    $('#sgst').val(sgstTotal.toFixed(2));
    $('#grandTotal').val(grandTotal.toFixed(2));

}



function addBlankRow_deprecated() {
    // Create a new row
    var newRow = document.createElement('tr');
    var newRowId = "itemRow" + (document.querySelectorAll("[id^='itemRow']").length + 1);
    newRow.setAttribute("id", newRowId);

    // Create cells for the new row
    var cell1 = document.createElement('td');
    var cell2 = document.createElement('td');
    var cell3 = document.createElement('td');
    var cell4 = document.createElement('td');
    var cell5 = document.createElement('td');
    var cell6 = document.createElement('td');
    // Add more cells as needed...

    // Create a select dropdown for the first cell
    var dropdown = document.createElement('select');
    dropdown.className = 'form-control';
    dropdown.setAttribute('data-trigger', '');
    dropdown.id = 'itemId' + (document.querySelectorAll('[id^="itemId"]').length + 1);

    // Add options to the dropdown
    var option1 = document.createElement('option');
    option1.value = '';
    option1.textContent = 'Select Product';

    var option2 = document.createElement('option');
    option2.value = '48';
    option2.textContent = 'Box Printing - 4911';

    var option3 = document.createElement('option');
    option3.value = '51';
    option3.textContent = 'A/5 PAMPHLET PRINT- 8443';


    // Add more options as needed...

    // Append options to the dropdown
    dropdown.appendChild(option1);
    dropdown.appendChild(option2);
    dropdown.appendChild(option3);
    // Append more options as needed...

    // Append the dropdown to the first cell
    cell1.appendChild(dropdown);

    // Create other elements for the remaining cells and append them

    // Create a container div for quantity
    var quantityContainer = document.createElement('div');
    quantityContainer.className = 'input-group border rounded flex-nowrap';

    // Create quantity input
    var quantityInput = document.createElement('input');
    quantityInput.type = 'text';
    quantityInput.className = 'form-control form-control-sm border-0 text-center w-100';
    quantityInput.ariaLabel = 'quantity';
    quantityInput.id = 'qty' + (document.querySelectorAll('[id^="qty"]').length + 1);
    quantityInput.name = 'qty' + (document.querySelectorAll('[name^="qty"]').length + 1);
    quantityInput.value = '1';

    // Create buttons for quantity control
    var quantityMinusButton = document.createElement('button');
    quantityMinusButton.type = 'button';
    quantityMinusButton.className = 'btn btn-icon btn-primary input-group-text flex-fill product-quantity-minus';
    quantityMinusButton.id = 'minusButton' + (document.querySelectorAll('[id^="qty"]').length + 1);
    quantityMinusButton.innerHTML = '<i class="ri-subtract-line"></i>';

    // Attach the click event handler to the button
    quantityMinusButton.addEventListener("click", function () {
        changeQty(quantityMinusButton, 0);
    });

    var quantityPlusButton = document.createElement('button');
    quantityPlusButton.type = 'button';
    quantityPlusButton.className = 'btn btn-icon btn-primary input-group-text flex-fill product-quantity-plus';
    quantityPlusButton.innerHTML = '<i class="ri-add-line"></i>';
    quantityPlusButton.id = 'plusButton' + (document.querySelectorAll('[id^="qty"]').length + 1);
    // Attach the click event handler to the button
    quantityPlusButton.addEventListener("click", function () {
        changeQty(quantityPlusButton, 1);
    });

    // Append buttons and input to the quantity container
    quantityContainer.appendChild(quantityMinusButton);
    quantityContainer.appendChild(quantityInput);
    quantityContainer.appendChild(quantityPlusButton);
    cell2.appendChild(quantityContainer);

    var priceInput = document.createElement('input');
    priceInput.type = 'text';
    priceInput.className = 'form-control form-control-light';
    priceInput.id = 'price' + (document.querySelectorAll('[id^="price"]').length + 1);
    priceInput.value = '₹60.00';

    cell3.appendChild(priceInput);

    var taxInput = document.createElement('input');
    taxInput.type = 'text';
    taxInput.className = 'form-control form-control-light';
    taxInput.id = 'tax' + (document.querySelectorAll('[id^="tax"]').length + 1);
    taxInput.value = '18%';

    cell4.appendChild(taxInput);

    var totalInput = document.createElement('input');
    totalInput.type = 'text';
    totalInput.className = 'form-control form-control-light';
    totalInput.id = 'total' + (document.querySelectorAll('[id^="total"]').length + 1);
    totalInput.value = '₹120.00';

    cell5.appendChild(totalInput);

    // Create delete button
    var deleteButton = document.createElement('button');
    deleteButton.type = 'button';
    deleteButton.className = 'btn btn-sm btn-icon btn-danger-light';
    deleteButton.innerHTML = '<i class="ri-delete-bin-5-line"></i>';

    // Attach event listener to the delete button
    deleteButton.addEventListener('click', function () {
        // Add logic to handle row deletion
        removeRow(newRow.id);
    });

    cell6.appendChild(deleteButton);


    // Append cells to the new row
    newRow.appendChild(cell1);
    newRow.appendChild(cell2);
    newRow.appendChild(cell3);
    newRow.appendChild(cell4);
    newRow.appendChild(cell5);
    newRow.appendChild(cell6);
    // Append more cells as needed...

    // Append the new row to the table body
    document.getElementById('challanTableBody').appendChild(newRow);
    initializeDropdown(dropdown);
}

function initializeDropdown(newDropdown) {
    //var element = genericExamples[i];
    new Choices(newDropdown, {
        allowHTML: true,
        placeholderValue: "This is a placeholder set in the config",
        searchPlaceholderValue: "Search",
    });

}

function changeQty(actionbutton, actionType) {
    //console.log("button clicked: ", actionbutton)
    var value = 1,
        minValue = 0,
        maxValue = 30;

    value = Number(actionbutton.parentElement.childNodes[1].value)
    if (value > minValue && actionType == 0) {
        value = value - 1;
        actionbutton.parentElement.childNodes[1].value = value;
    }

    if (value < maxValue && actionType == 1) {
        value = value + 1;
        actionbutton.parentElement.childNodes[1].value = value;
    }
}

function CalculateRowTotal() {

    var qtyValue = $('#qty').val();
    var rateValue = $('#rate').val();

    if (isNaN(qtyValue) || isNaN(rateValue)) {
         return; 
    }

    qtyValue = parseFloat(qtyValue);
    rateValue = parseFloat(rateValue);

    var total = qtyValue * rateValue;
    $('#rowTotal').val(total);

}

$(document).on('click', '#saveChallan', function (e) {

    hideTabValidationError();
    if (!$("#frmChallan").valid()) {
        console.log("invalid form challan");
        return false;
    }

    return;

    let challanId = 0;

    //let customerId = parseInt(document.getElementById("customerId").value);
    let customerId = document.getElementById("customerID").value;
    let billAddressId = document.getElementById("billAddressId").value;
    let shipAddressId = document.getElementById("shipAddressId").value;
    let challanNo = document.getElementById("challanNo").value;
    let currency = document.getElementById("currency").value;
    let issuedDate = document.getElementById("issuedDate").value;
    let purchaseOrder = document.getElementById("purchaseOrder").value;
    let poDate = document.getElementById("poDate").value;
    let note = document.getElementById("note").value;
    
    let requestBody = {
        "ChallanID": challanId,
        "CustomerID": customerId,
        "BillAddressID": billAddressId,
        "ShipAddressID": shipAddressId,
        "ChallanNo": challanNo,
        "ChallanDate": issuedDate,
        "PurchaseOrderNo": purchaseOrder,
        "PurchaseOrderDate": poDate,
        "Description": note
    };

    console.log(requestBody);
    //return false;

    $('#loader').show();
    $.ajax({
        type: "POST",
        dataType: "json",
        url: (window.location.origin + "/AR/Customer/SaveCustomer"),
        data: requestBody,
        success: function (response) {
            $('#loader').hide();
            if (response.success) {
                if (customerId === 0) {
                    customerId = parseInt(response.customerId);
                    document.getElementById("customerId").value = customerId;
                    let redirectUrl = window.location.origin + "/AR/Customer/Detail/" + customerId;
                    setTimeout(function () {
                        window.location.href = redirectUrl;
                    }, 2000);
                }
            }
        },
        error: function (er) {
            $('#loader').hide();
        }
    });
});


function SetupFormValidation() {


    jQuery.validator.addMethod("multiEmails", function (value, element) {

        if (value == null || value == undefined || value == '')
            return true;

        // Split the input value into individual email addresses using a comma as the separator
        var emails = value.split(',');
        //var emailRegex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
        var emailRegex = /^[a-zA-Z0-9.!#$%&'*+\/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/;

        var invalidEmails = [];

        // Validate each email address
        for (var i = 0; i < emails.length; i++) {
            var email = emails[i].trim(); // Remove leading/trailing spaces 
            if (!emailRegex.test(email)) {
                invalidEmails.push(email); // Store invalid email addresses
            }
        }

        // Return the invalid email addresses as a string (comma-separated)
        //return invalidEmails.length === 0 ? true : invalidEmails.join(', ');
        return invalidEmails.length === 0;

    }, '');

    //}, function (params, element) {
    //    // Display the error message with the list of invalid emails
    //    return "Invalid email addresses: " + params;
    //});


    $("#frmChallan").validate({
        errorClass: "invalid-form-control",
        ignore: "",
        invalidHandler: function (event, validator) {
            setTimeout(function () {
                showTabValidationError();
            });
        },
        rules: {
            customerID: {
                required: true
            },
            billAddressId: {
                required: true
            },
            shipAddressId: {
                required: true
            },
            challanNo: {
                required: true,
                maxlength: 20
            },
            issuedDate: {
                required: true
            },
            currency: {
                required: true
            }
        },
        messages: {
            customerID: {
                required: "Select a customer"
            },
            billAddressId: {
                required: "Select billing address"
            },
            shipAddressId: {
                required: "Select shipping address"
            },
            challanNo: {
                required: "Enter challan",
                maxlength: "challan length should not exceed 20 characters"
            },
            issuedDate: {
                required: "select challan issued date"
            },
            currency: {
                required: "select currency"
            }
        }
    });

   

};



// Document Ready function

let itemDropdown;
let uomDropdown;
let taxDropdown;

$(document).ready(function () {

    //SetupDataTables();

    SetupFormValidation();

    document.getElementById("customerID").addEventListener('change', ShowHideAddress);
    document.getElementById("discount").addEventListener('change', recalculateTotals);

    // Date issued 
    flatpickr("#issuedDate", {});

    // Due date 
    flatpickr("#poDate", {});

    //Disable Billing and Shipping controls
    var myRow = document.getElementById("addressRow");
    var inputElements = myRow.querySelectorAll("input");

    inputElements.forEach(function (input) {
        input.disabled = true;
    });

    //* QTY spinner START */
    var value = 1,
        minValue = 0,
        maxValue = 30;

    let productMinusBtn = document.querySelectorAll(".product-quantity-minus")
    let productPlusBtn = document.querySelectorAll(".product-quantity-plus")
    productMinusBtn.forEach((element) => {
        element.onclick = () => {
            value = Number(element.parentElement.childNodes[3].value)
            if (value > minValue) {
                value = Number(element.parentElement.childNodes[3].value) - 1;
                element.parentElement.childNodes[3].value = value;
                CalculateRowTotal();
            }
        }
    });
    productPlusBtn.forEach((element) => {
        element.onclick = () => {
            if (value < maxValue) {
                value = Number(element.parentElement.childNodes[3].value) + 1;
                element.parentElement.childNodes[3].value = value;
                CalculateRowTotal();
            }
        }
    });

    document.getElementById("qty").addEventListener('change', CalculateRowTotal);
    document.getElementById("rate").addEventListener('change', CalculateRowTotal);

    //document.getElementById("addInvDetailRow").addEventListener("click", function () {
    //    //addRow();
    //    addBlankRow();
    //});

      //* QTY spinner END */

    let itemChoicesArray = [
        { value: 0, label: ' Select Item ' },
        { value: 48, label: 'Box Printing - 911' },
        { value: 51, label: 'A/5 PAMPHLET PRINT- 8443' },
        { value: 191, label: 'SMART CARD PRINT - 555' },
        { value: 123, label: 'C.S PLOTTER - 480255' },
        // Add additional options here if needed
    ];

    itemDropdown = new Choices(document.getElementById('itemId1'), { choices: itemChoicesArray, shouldSort: false });
    itemDropdown.setChoiceByValue("0");

    uomDropdown = new Choices(document.getElementById('uom'), { shouldSort: false });
    taxDropdown = new Choices(document.getElementById('taxType'), { shouldSort: false });


    //var event = new Event('change', { bubbles: true });
    //itemDropdown.dispatchEvent(event);

    setupProductDetailValidationRules();

});



function setupProductDetailValidationRules() {

    jQuery.validator.addMethod("choiceValidation", function (value, element) {

        if (value == null || value == undefined || value == '' || value == '0') {
            return false;
        }
        else { 
            return true;
        }
   
    }, '');
}


 function validateProductDetails() {


     // Validate each input field individually
     var displayError = false;
     $("#productValidationError").empty();

     $.each(validationRules, function (field, rules) {
         var value = $("#" + field).val();
         var errorMessage = "";
        
         // Check each validation rule
         $.each(rules, function (rule, ruleValue) {
             if (rule === "required" && ruleValue && !value) {
                 errorMessage = validationMessages[field][rule];
             } else if (rule === "maxlength" && value.length > ruleValue) {
                 errorMessage = validationMessages[field][rule];
             } else if (rule === "email" && !isValidEmail(value)) {
                 errorMessage = validationMessages[field][rule];
             }
             else if (rule === "choiceValidation" && ruleValue && !isValidChoiceDropdown(value)) {
                 errorMessage = validationMessages[field][rule];
             }
         });

        //Display error message if validation fails
         if (errorMessage) {
             //$("#" + field).after("<div class='invalid-form-control'>" + errorMessage + "</div>");
             $("#productValidationError").append("" + errorMessage + "<br>");
             displayError = true;
         }

     });

     return !displayError;
}

function isValidChoiceDropdown(value) {
    if (value == null || value == undefined || value == '' || value == '0') {
        return false;
    }
    else {
        return true;
    }
}

  var validationRules = {
      itemId1: {
          choiceValidation: true
        },
        uom: {
            choiceValidation: true
        },
      qty: {
            required: true,
            digits: true
        },
      rate: {
          required: true,
          number: true
      },
      taxType: {
          choiceValidation: true
      }
    };

  var validationMessages = {
        itemId1: {
            choiceValidation: "Please select a valid value for Product"
        },
        uom: {
            choiceValidation: "Please select a valid value for UOM"
        },
        qty: {
            required: "Please enter quantity",
            digits: "Please enter a numeric value"
        },
        rate: {
            required: "Please enter the rate",
            number: "Please enter a numeric value"
        },
        taxType: {
            choiceValidation: "Please select a valid value for GST Type"
        },
    };
