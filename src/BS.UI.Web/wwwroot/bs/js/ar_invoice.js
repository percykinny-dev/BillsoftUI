

$(document).on('click', '#saveCustomer', function (e) {

    hideTabValidationError();
    if (!$("#frmCustomer").valid()) {
        console.log("invalid form customer");
        return false;
    }

    let customerId = 0;

    //let customerId = parseInt(document.getElementById("customerId").value);
    let code = document.getElementById("code").value;
    let firstname = document.getElementById("firstName").value;
    let lastname = document.getElementById("lastName").value;
    let gstno = document.getElementById("gstNo").value;
    let emailaddress = document.getElementById("emailAdress").value;
    let mobilephone = document.getElementById("mobilePhone").value;
    
    let requestBody = {
        "ID": customerId,
        "Code": code,
        "FirstName": firstname,
        "LastName": lastname,
        "GstNo": gstno,
        "EmailAdress": emailaddress,
        "MobilePhone": mobilephone
    };

    console.log(requestBody);
    //return false;

    //$('#loader').show();
    //$.ajax({
    //    type: "POST",
    //    dataType: "json",
    //    url: (window.location.origin + "/AR/Customers/SaveCustomer"),
    //    data: requestBody,
    //    success: function (response) {
    //        $('#loader').hide();
    //        if (response.success) {
    //            if (customerId === 0) {
    //                customerId = parseInt(response.customerId);
    //                document.getElementById("customerId").value = customerId;
    //                let newUrl = `/AR/Customers/Detail/${customerId}`;
    //                window.history.pushState(`arcustomer${customerId}`, 'Customers', newUrl);
    //            }
    //        }
    //    },
    //    error: function (er) {
    //        $('#loader').hide();
    //    }
    //});
});

function ShowHideAddress() {
    let customerid = document.getElementById("customerID");
    let addressRow = document.getElementById('addressRow');

    var isCustomerSelected = (customerid.value !== '');

    // Toggle the 'hidden-element' class based on the selection
    if (isCustomerSelected) {
        addressRow.classList.remove('hidden-element');
    } else {
        addressRow.classList.add('hidden-element');
    }
}

// Add New Invoice Detail row
function addRow() {
    // Clone the existing row

    
    var existingRow = document.getElementById("itemRow1");
    var newRow = existingRow.cloneNode(true);

    // Modify attributes and IDs to make them unique
    let rowNumber = document.querySelectorAll("[id^='itemRow']").length + 1;
    var newRowId = "itemRow" + (document.querySelectorAll("[id^='itemRow']").length + 1);
    newRow.setAttribute("id", newRowId);

    // Modify child elements' IDs
    newRow.querySelectorAll("[id^='itemId']")[0].setAttribute("id", "itemId" + newRowId.slice(-1));
    var clonedDropdown = newRow.querySelector("[id^='itemId']");
    clonedDropdown.setAttribute("data-trigger", "");
    initializeDropdown(clonedDropdown);

    newRow.querySelectorAll("[id^='qty']")[0].setAttribute("id", "qty" + newRowId.slice(-1));
    newRow.querySelectorAll("[id^='price']")[0].setAttribute("id", "price" + newRowId.slice(-1));
    newRow.querySelectorAll("[id^='tax']")[0].setAttribute("id", "tax" + newRowId.slice(-1));
    newRow.querySelectorAll("[id^='total']")[0].setAttribute("id", "total" + newRowId.slice(-1));

    //newRow.querySelectorAll("[id^='itemId']")[0].remove();
    //var clonedDropdown = newRow.querySelector("#itemId");
    //clonedDropdown.remove();

    // Attach event listeners to the new row's delete button
    newRow.querySelector(".btn-danger-light").addEventListener("click", function () {
        removeRow(newRowId);
    });

    // Append the new row to the table body

    document.getElementById("invoiceTableBody").appendChild(newRow);
    //var clonedDropdown = document.getElementById('itemId' + rowNumber.toString());
    //var clonedDropdown = newRow.querySelector("#choices");
    //clonedDropdown.remove();
    //initializeDropdown(clonedDropdown);

}

function removeRow(rowId) {
    var rowToRemove = document.getElementById(rowId);
    rowToRemove.parentNode.removeChild(rowToRemove);
}

function initializeDropdown(newDropdown) {
    //var element = genericExamples[i];
    new Choices(newDropdown, {
        allowHTML: true,
        placeholderValue: "This is a placeholder set in the config",
        searchPlaceholderValue: "Search",
    });

    //var genericExamples = document.querySelectorAll("[data-trigger]");
    //for (let i = 0; i < genericExamples.length; ++i) {
    //    var element = genericExamples[i];
    //    new Choices(element, {
    //        allowHTML: true,
    //        placeholderValue: "This is a placeholder set in the config",
    //        searchPlaceholderValue: "Search",
    //    });
    //}

}

function replicateDropdown() {
    // Clone the dropdown
    var originalDropdown = document.getElementById('itemId1');
    var clonedDropdown = originalDropdown.cloneNode(true);

    // Update the cloned dropdown's id to ensure uniqueness
    //var rowCount = document.querySelectorAll('tbody tr').length + 1;
    clonedDropdown.id = 'itemId5'; 
    document.getElementById('tstProduct').appendChild(clonedDropdown);
    initializeDropdown(clonedDropdown);
    
}

function addBlankRow() {
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
        changeQty(quantityMinusButton,0);
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
    document.getElementById('invoiceTableBody').appendChild(newRow);
    initializeDropdown(dropdown);
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


    $("#frmCustomer").validate({
        errorClass: "invalid-form-control",
        ignore: "",
        invalidHandler: function () {
            setTimeout(function () {
                showTabValidationError();
            });
        },
        rules: {
            code: {
                required: true,
                maxlength: 20
            },
            firstName: {
                required: true,
                maxlength: 100
            },
            lastName: {
                required: true,
                maxlength: 100
            },
            gstNo: {
                maxlength: 15
            },
            emailAddress: {
                email: true,
                maxlength: 100
            },
            mobilePhone: {
                inTelephone: true,
                maxlength: 20
            }
        },
        messages: {
            code: {
                required: "Enter code",
                maxlength: "Code length should not exceed 20 characters"
            },
            firstName: {
                required: "Enter firstname",
                maxlength: "First Name length should not exceed 100 characters"
            },
            lastName: {
                required: "Enter lastname",
                maxlength: "First Name length should not exceed 100 characters"
            },
            gstNo: {
                maxlength: "GSTNo length should not exceed 15 characters"
            },
            emailAddress: {
                email:      "Please enter valid emailaddress",
                maxlength: "Email Address length should not exceed 10 characters"
            },
            mobilePhone: {
                inTelephone: "Please enter valid Phone number",
                maxlength: "Mobile Phone length should not exceed 20 characters"
            }
        }
    });

}



// Function to dynamically populate and format the address list
function populateAddressList() {
    //var addressListContainer = document.getElementById("addressListContainer");

    // Clear existing content
    //addressListContainer.innerHTML = '';

    // Sample JSON array of addresses
    var addresses = [
        {
            Address1: "123 Main St",
            Address2: "Apt 4",
            City: "Cityville",
            State: "CA",
            Zip: "12345",
            Phone: "555-1234"
        },
        {
            Address1: "Krishna Nagar",
            Address2: "Saki Naka",
            City: "Mumbai",
            State: "MH",
            Zip: "400027",
            Phone: "9819179555"
        },
        {
            Address1: "23/6-7, 2nd Floor,",
            Address2: " EAST PATEL NAGAR",
            City: "Delhi",
            State: "Delhi",
            Zip: "110008",
            Phone: "7789179555"
        },
        {
            Address1: "4022 4TH FLR ONE AEROCITY,CORPORATE PARK",
            Address2: "SAFED POOL ANDHERI KURLA RD SAKINAKA",
            City: "Mumabi",
            State: "MH",
            Zip: "400072",
            Phone: "7208968391"
        },
        // Add more addresses as needed
    ];


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
            <div>${address.Address1}</div>
            <div>${address.Address2}</div>
            <div>${address.City}, ${address.State} ${address.Zip}</div>
            <div>${address.Phone}</div>
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

function updateAddressFields(selectedAddress) {
    document.getElementById('billAddress').value = selectedAddress.Address1 + ' ' + selectedAddress.Address2 + ',' + selectedAddress.City + ',' + selectedAddress.State;
    document.getElementById('billZipcode').value = selectedAddress.Zip;
}

// Call the function to populate the address list when the modal is shown
$('#addressModal').on('shown.bs.modal', function () {
    populateAddressList();
});

function ShowAddressList() {

    $("#addressModal").modal('show');
}

$(document).ready(function () {

    //SetupDataTables();

    //SetupFormValidation();

    //var simpleBarElement = document.querySelector('.simplebar');
    //var simpleBarElement = document.getElementById("invCard");
    //var simpleBar = new SimpleBar(simpleBarElement, { autoHide: false });

    document.getElementById("customerID").addEventListener('change', ShowHideAddress);

    // Date issued 
    flatpickr("#issuedDate", {});

    // Due date 
    flatpickr("#dueDate", {});

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
            }
        }
    });
    productPlusBtn.forEach((element) => {
        element.onclick = () => {
            if (value < maxValue) {
                value = Number(element.parentElement.childNodes[3].value) + 1;
                element.parentElement.childNodes[3].value = value;
            }
        }
    });

    document.getElementById("addInvDetailRow").addEventListener("click", function () {
        //addRow();
        addBlankRow();
    });

      //* QTY spinner END */
});