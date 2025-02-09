
$(document).ready(function () {


    //SetupDataTables();

    SetupFormValidation();

    document.getElementById("customerID").addEventListener('change', ShowHideAddress);
    //document.getElementById("discount").addEventListener('change', recalculateTotals);

    // Quotation date
    flatpickr("#quotationdate", {});


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


    let itemChoicesArray = [
        //    //{ value: 0, label: ' Select Product ' },
        //    //{ value: 48, label: 'Box Printing - 911' },
        //    //{ value: 51, label: 'A/5 PAMPHLET PRINT- 8443' },
        //    //{ value: 191, label: 'SMART CARD PRINT - 555' },
        //    //{ value: 123, label: 'C.S PLOTTER - 480255' },
        //    // Add additional options here if needed
    ];
    itemDropdown = new Choices(document.getElementById('itemId1'), { choices: itemChoicesArray, shouldSort: false, itemSelectText: '' });
    uomDropdown = new Choices(document.getElementById('uom'), { shouldSort: false, itemSelectText: '' });
    taxDropdown = new Choices(document.getElementById('taxType'), { shouldSort: false, itemSelectText: '' });


    //var event = new Event('change', { bubbles: true });
    //itemDropdown.dispatchEvent(event);

    setupProductDetailValidationRules();

    document.getElementById("editRow").disabled = true;
    document.getElementById("deleteRow").disabled = true;

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

function clearAddressFields() {
    document.getElementById('quotationAddress').innerText = '';
    document.getElementById('quotationZipcode').innerText = '';
    document.getElementById('quotationContact').innerText = '';
    document.getElementById('quotationEmailAddress').innerText = '';
    document.getElementById('quotationPhone').innerText = '';

    //document.getElementById('shipAddress').innerText = '';
    //document.getElementById('shipZipcode').innerText = '';
    //document.getElementById('shipContact').innerText = '';
    //document.getElementById('shipEmailAddress').innerText = '';
    //document.getElementById('shipPhone').innerText = '';

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

function updateAddressFields(selectedAddress) {
    //clearAddressFields();

    if (selectedAddress.addressType == 1) {
        //document.getElementById('quotationAddress').value = selectedAddress.address1 + ' ' + selectedAddress.address2 + ',' + selectedAddress.city + ',' + selectedAddress.state;
        document.getElementById('quotationAddress').innerText = selectedAddress.address1 + ' ' + selectedAddress.address2 + ',' + selectedAddress.city + ',' + selectedAddress.state;
        document.getElementById('quotationZipcode').innerText = selectedAddress.zipcode;
        //document.getElementById('quotationContact').value = selectedAddress.contactName;
        document.getElementById('quotationContact').innerText = selectedAddress.contactName;
        document.getElementById('quotationEmailAddress').innerText = selectedAddress.emailAddress;
        document.getElementById('quotationPhone').innerText = selectedAddress.workPhone;
        document.getElementById('quotationAddressId').value = selectedAddress.addressID;
    }
    //else {
    //    document.getElementById('shipAddress').innerText = selectedAddress.address1 + ' ' + selectedAddress.address2 + ',' + selectedAddress.city + ',' + selectedAddress.state;
    //    document.getElementById('shipZipcode').innerText = selectedAddress.zipcode;
    //    document.getElementById('shipContact').innerText = selectedAddress.contactName;
    //    document.getElementById('shipEmailAddress').innerText = selectedAddress.emailAddress;
    //    document.getElementById('shipPhone').innerText = selectedAddress.workPhone;
    //    document.getElementById('shipAddressId').value = selectedAddress.addressID;
    //}

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

function addNewRow() {
    if (!validateProductDetails()) {
        setTimeout(function () {
            $('#solid-dangerToast').show();

        }, 5);
        /*$('#solid-dangerToast').show();*/
        return;
    }

    var rowValues = getRowValues();
    //>> Check Duplicate records based on Item
    var isExist = CheckDuplicatesProducts(rowValues.productNameVal);
    if (isExist) {
        alert('Duplicate record !!! Similar Product exists...');
        return;
    }
    //<<

    // Construct the HTML for the new row in the child table
    var newRowHtml = '<tr>';
    newRowHtml += '<td><input type="checkbox" id="row' + rowValues.rowCount + '" class="form-check-input"></td>'; // Checkbox column
    newRowHtml += '<td data-productname="' + rowValues.productNameVal + '" ><a href="#" onclick="selectRow(' + rowValues.rowCount + ');event.preventDefault();">' + rowValues.productName + '</a></td>'; // Product Name
    newRowHtml += '<td data-hsnno="' + rowValues.hsnNo + '">' + rowValues.hsnNo + '</td>'; // HSN No
    newRowHtml += '<td data-uom="' + rowValues.uomVal + '">' + rowValues.uom + '</td>'; // UOM
    newRowHtml += '<td data-qty="' + rowValues.quantity + '">' + rowValues.quantity + '</td>'; // Quantity
    newRowHtml += '<td data-rate="' + rowValues.rate + '">' + rowValues.rate + '</td>'; // Rate
    newRowHtml += '<td data-disc="' + rowValues.disc + '">' + rowValues.disc + '</td>'; // Disc
    newRowHtml += '<td data-tax="' + rowValues.taxVal + '" data-gst="' + rowValues.tax + '">' + rowValues.taxString + '</td>'; // Tax
    newRowHtml += '<td data-rowTotal="' + rowValues.total + '">' + rowValues.total + '</td>'; // Total
    newRowHtml += '</tr>';

    // Append the new row to the child table
    $('#childTableBody').append(newRowHtml);

    // Recalculate TaxTotal
    recalculateTotals();

    //Reset dropdowns and textboxes
    resetDropdownTextboxes();

    //document.getElementById("editRow").disabled = false;
    document.getElementById("deleteRow").disabled = false;
}

function getRowValues() {
    var productName = $('#itemId1 option:selected').text();
    var productNameVal = $('#itemId1 option:selected').val();
    var hsnNo = $('#hsnNo').val();
    var uom = $('#uom option:selected').text();
    var uomVal = $('#uom option:selected').val();
    var quantity = $('#qty').val();
    var rate = $('#rate').val();
    var disc = $('#disc').val();
    var pattern = /[\d\.]+/g;  // /\[(\d+)%\]/;
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
        disc: disc,
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

$(document).on('click', '#saveQuotation', function (e) {

    e.preventDefault();
    hideTabValidationError();

    if (!$("#frmQuotation").valid()) {
        console.log("invalid form quotation");
        return false;
    }

    var tableSize = $('#childTableBody tr').length;
    if (tableSize == 0) {
        alert('Quotation should have atleast one Product detail');
        return false;
    }

    // Check for duplicate products in the product detail table
    // 1 for cell/column name/position
    if (!checkForDuplicateProducts('productDetailTable', 1)) {
        return false;
    }


    let quotationid = parseInt(document.getElementById("quotationid").value);
    let customerId = document.getElementById("customerID").value;
    let quotationAddressId = document.getElementById("quotationAddressId").value;
    let currency = document.getElementById("currency").value;
    let note = document.getElementById("note").value;
    let quotationno = document.getElementById("quotationno").value;
    let finYear = parseInt(document.getElementById("finYear").value);
    let quotationdate = document.getElementById("quotationdate").value;
    let gstno = document.getElementById("gstno").value;
    let state = document.getElementById("state").value;
    let department = document.getElementById("department").value;
    let contactperson = document.getElementById("contactperson").value;
    let delivery = document.getElementById("delivery").value;
    let freight = document.getElementById("freight").value;
    let paymentterms = document.getElementById("paymentterms").value;
    let cost = document.getElementById("cost").value;
    //let subTotal = document.getElementById("subTotal").value;
    //let discount = document.getElementById("discount").value;
    //let cgst = document.getElementById("cgst").value;
    //let sgst = document.getElementById("sgst").value;
    //let totalAmount = document.getElementById("grandTotal").value;


    let quotationData = {
        "QuotationID": quotationid,
        "CustomerID": customerId,
        "QuotationAddressID": quotationAddressId,
        "Currency": currency,
        "Note": note,
        "QuotationNo": quotationno,
        "FinYear": finYear,
        "QuotationDate": quotationdate,
        "GSTNO": gstno,
        "State": state,
        "Department": department,
        "ContactPerson": contactperson,
        "Delivery": delivery,
        "Freight": freight,
        "PaymentTerms": paymentterms,
        "Cost": cost
        //"SubTotal": subTotal,
        //"Discount": discount,
        //"CGSTAmount": cgst,
        //"SGSTAmount": sgst,
        //"TotalAmount": totalAmount
    };


    // Loop and read data from the html table for ARCHallanDetails

    const apquotationDetails = [];

    const table = document.getElementById('productDetailTable');


    // Loop through the rows (skip the header row)
    for (let i = 1; i < table.rows.length; i++) {
        const row = table.rows[i];
        const apquotationDetail = {
            //StatusID: row.cells[0].textContent,
            ItemID: row.cells[1].getAttribute("data-productname"),
            HSNCode: row.cells[2].textContent,
            Uom: row.cells[3].getAttribute("data-uom"),
            Quantity: row.cells[4].getAttribute("data-qty"),
            Rate: row.cells[5].getAttribute("data-rate"),
            Disc: row.cells[6].getAttribute("data-disc"),
            //GSTRateID: row.cells[6].getAttribute("data-taxItemRate"), -- add ItemId GST Rate
            GSTRateTypeID: row.cells[7].getAttribute("data-tax"),
            Total: row.cells[8].getAttribute("data-rowtotal"),
            // Add other properties as needed
        };
        apquotationDetails.push(apquotationDetail);
    }

    const requestData = {
        quotation: quotationData,
        quotationItems: apquotationDetails,
    };

    //const requestData = {
    //    challanData,
    //    apquotationDetails
    //};

    // Convert to JSON string
    const jsonRequestBody = JSON.stringify(requestData);

    //Body should comtain Challan and Challan Details
    //console.log(jsonRequestBody);
    //return false;

    $('#loader').show();
    $.ajax({
        type: 'POST',
        //contentType: 'application/json',
        dataType: 'json',
        url: (window.location.origin + '/AP/Quotation/SaveQuotation'),
        data: requestData,
        success: function (response) {
            $('#loader').hide();
            if (response.success) {
                let challanId = 0;
                //challanId = response.ChallanId;
                if (challanId === 0) {
                    challanId = parseInt(response.data.challanId);
                    document.getElementById("quotationid").value = challanId;

                    let redirectUrl = window.location.origin + "/AP/Quotation/Index";
                    setTimeout(function () {
                        console.log("Redirecting to Index page");
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
function selectRow(rowIndex) {
    $('#selRowIndex').val(rowIndex);

    var row = $('#childTableBody tr').eq(rowIndex);
    var rowData = {
        itemName: row.find('[data-productname]').data('productname'),
        hsnNo: row.find('[data-hsnno]').data('hsnno'),
        uom: row.find('[data-uom]').data('uom') + '',
        qty: row.find('[data-qty]').data('qty'),
        rate: row.find('[data-rate]').data('rate'),
        disc: row.find('[data-disc]').data('disc'),
        tax: row.find('[data-tax]').data('tax') + '',
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
    $('#disc').val(rowData.disc);
    //$('#taxType option:selected').val(rowData.tax);
    $('#rowTotal').val(rowData.rowTotal);

    //$("#itemId1").val(rowData.itemName.toString());
    itemDropdown.setChoiceByValue(rowData.itemName.toString());
    //$("#uom").val(rowData.uom);
    uomDropdown.setChoiceByValue(rowData.uom);
    //$("#taxType").val(rowData.tax);
    taxDropdown.setChoiceByValue(rowData.tax);

    document.getElementById("addRow").disabled = true;
    document.getElementById("editRow").disabled = false;
    document.getElementById("deleteRow").disabled = true;
    //console.log(productName, hsnNo, uom, quantity, rate, tax, total);
}

function editSelectedRow() {
    if (!validateProductDetails()) {
        setTimeout(function () {
            $('#solid-dangerToast').show();

        }, 5);
        /*$('#solid-dangerToast').show();*/
        return;
    }

    var rowIndex = parseInt(document.getElementById('selRowIndex').value);
    if (isNaN(rowIndex)) { return };

    var row = $('#childTableBody tr').eq(rowIndex);
    var rowValues = getRowValues();

    // Update cell values in the row
    var productHtml = '<a href="#" onclick="selectRow(' + rowIndex + ');event.preventDefault();">' + rowValues.productName + '</a>'
    //alert(productHtml);
    //row.find('td[data-productname]').innerHTML = productHtml;
    //row.querySelector('td[data-productname]').innerHTML = productHtml;
    $(row).find('td[data-productname]').html(productHtml);
    row.find('td[data-hsnno]').text(rowValues.hsnNo);
    row.find('td[data-uom]').text(rowValues.uom);

    row.find('td[data-qty]').text(rowValues.quantity);
    row.find('td[data-rate]').text(rowValues.rate);
    row.find('td[data-disc]').text(rowValues.disc);
    row.find('td[data-tax]').text(rowValues.taxString);
    row.find('td[data-rowtotal]').text(rowValues.total);

    row.find('td[data-productname]').data("productname", rowValues.productNameVal);
    //row.find('td[data-productname]').data("productname", rowValues.productName);
    row.find('td[data-hsnno]').data("hsnno", rowValues.hsnNo);
    row.find('td[data-uom]').data("uom", rowValues.uomVal);
    row.find('td[data-qty]').data("qty", rowValues.quantity);
    row.find('td[data-rate]').data("rate", rowValues.rate);
    row.find('td[data-disc]').data("disc", rowValues.disc);
    row.find('td[data-tax]').data("tax", rowValues.taxVal);
    row.find('td[data-gst]').data("gst", rowValues.tax);
    row.find('[data-rowtotal]').data("rowtotal", rowValues.total);

    recalculateTotals();

    clearDropdownTextboxes();
}

//var temp = 1;
function deleteSelectedRows() {
    if (document.querySelectorAll('input[type="checkbox"]:checked').length == 0) {
        alert('Select row(s) to delete ...');
        return;
    }
    if (confirm("Do you wish to delete?")) {
        // code to delete the item goes here
        document.querySelectorAll('#childTableBody .form-check-input:checked').forEach(e => {
            e.parentNode.parentNode.remove()
        });
        recalculateTotals();

        var tableSize = $('#childTableBody tr').length;
        if (tableSize == 0) {
            document.getElementById("deleteRow").disabled = true;
        }
    } else {
        // do nothing or handle cancellation
    }
}

function resetDropdownTextboxes() {

    clearDropdownTextboxes();

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
        //gstRate = parseFloat($(row).find('[data-tax]').data('tax'));
        cgstTotal = cgstTotal + (rowTotal * (gstRate * 0.5) / 100);
        sgstTotal = sgstTotal + (rowTotal * (gstRate * 0.5) / 100);
    });

    if ($('#discount').val() != '' && !isNaN(($('#discount').val()))) {
        discount = parseFloat($('#discount').val()).toFixed(2);
    }

    grandTotal = subTotal + cgstTotal + sgstTotal - discount;

    $('#subTotal').val(subTotal.toFixed(2));
    $('#cgst').val(cgstTotal.toFixed(2));
    $('#sgst').val(sgstTotal.toFixed(2));
    $('#grandTotal').val(grandTotal.toFixed(2));

}

function clearDropdownTextboxes() {
    itemDropdown.setChoiceByValue("0");
    uomDropdown.setChoiceByValue("0");
    taxDropdown.setChoiceByValue("0");

    //$("#itemId1").val($("#itemId1 option[selected]").val());
    //$("#uom").val($("#uom option[selected]").val());
    //$("#taxType").val($("#taxType option[selected]").val());

    $('#hsnNo').val('');
    $('#qty').val('');
    $('#rate').val('');
    $('#disc').val('');
    $('#rowTotal').val('');
    // Enable/Disable Action buttons
    document.getElementById("addRow").disabled = false;
    document.getElementById("editRow").disabled = true;
    var tableSize = $('#childTableBody tr').length;
    if (tableSize == 0) {
        document.getElementById("deleteRow").disabled = true;
    }
    else {
        document.getElementById("deleteRow").disabled = false;
    }

}

function SetupFormValidation() {


    $("#frmQuotation").validate({

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
            quotationAddressId: {
                required: true
            },
            currency: {
                required: true
            },
            quotationno: {
                required: true,
                maxlength: 20
            },
            quotationdate: {
                required: true
            },

            state: {
                required: true
            },
            grandTotal: {
                required: true
            }
        },
        messages: {
            customerID: {
                required: "Select Party"
            },
            quotationAddressId: {
                required: "Select Address"
            },
            currency: {
                required: "Select Currency"
            },
            quotationAddressId: {
                required: "Select Address"
            },
            quotationno: {
                required: "Enter Quotation",
                maxlength: "Quotation length should not exceed 20 characters"
            },
            quotationdate: {
                required: "Select Date"
            },
            state: {
                required: "Select State"
            },
            grandTotal: {
                required: "Quotation should have valid amount"
            }
        }
    });

};

function CheckDuplicatesProducts(itemId) {
    var isValid = false;
    $('#childTableBody tr').each(function (rowIndex, row) {
        let rowitemId = $(row).find('[data-productname]').data('productname');

        if (rowitemId == itemId) {
            isValid = true;
            return isValid;
        }
    });
    return isValid;
}

/**
 * Check for duplicate products in the product detail table.
 * 
 * @param {string} tableId - The ID of the product detail table element.
 * @param {number} columnIndex - The index of the column to check for duplicates (0-based).
 * @returns {boolean} True if no duplicates are found, false otherwise.
 */
function checkForDuplicateProducts(tableId, columnIndex) {
    // Get the product detail table element
    var tableproductDetail = document.getElementById(tableId);
    if (!tableproductDetail) {
        throw new Error(`Table element with ID '${tableId}' not found`);
    }

    // Get the rows of the product detail table
    var rows = tableproductDetail.rows;

    // Create a set to store unique product values and an array to store duplicate values
    var uniqueValues = new Set();
    var duplicateValues = [];

    for (var i = 0; i < rows.length; i++) {
        // Get the text content of the specified cell in the current row
        var value = rows[i].cells[columnIndex].textContent;

        if (uniqueValues.has(value)) {
            duplicateValues.push(value);
        } else {
            uniqueValues.add(value);
        }
    }

    // Check if there are any duplicate values
    if (duplicateValues.length > 0) {
        alert('Duplicate Product found: ' + duplicateValues);
        console.log('Duplicate values:', duplicateValues);
        return false;
    }
    return true;
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
    if (value == null || value == undefined || value == '' || value == '0' || value == 0) {
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
        choiceValidation: true,
        required: true,
        digits: true
    },
    rate: {
        choiceValidation: true,
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
        choiceValidation: "Please enter valid quantity",
        required: "Please enter quantity",
        digits: "Please enter a numeric value"
    },
    rate: {
        choiceValidation: "Please enter valid rate",
        required: "Please enter rate",
        number: "Please enter a numeric value"
    },
    taxType: {
        choiceValidation: "Please select a valid value for GST Type"
    },
};
