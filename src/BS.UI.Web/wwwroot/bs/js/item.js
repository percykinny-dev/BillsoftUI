

$(document).ready(function () {

    SetupFormValidation();

});

$(document).on('click', '#saveItem', function (e) {

    e.preventDefault();

    hideTabValidationError();

    if (!$("#frmItem").valid()) {
        console.log("invalid form item");
        return false;
    }

    let itemId = parseInt(document.getElementById("itemId").value);
    let itemname = document.getElementById("itemname").value;
    let hsnno = document.getElementById("hsnno").value;
    let rate = document.getElementById("rate").value;
    let uom = document.getElementById("uom").value;
    let category = document.getElementById("category").value;
  
    let cgst = document.getElementById("cgst").value;
    let sgst = document.getElementById("sgst").value;
    let igst = document.getElementById("igst").value;

    let itemData = {
        "ItemID": itemId,
        "ItemName": itemname,
        "HSNNO": hsnno,
        "Rate": rate,
        "UOM": uom,
        "Category": category,
        "CGST": cgst,
        "SGST": sgst,
        "IGST": igst
    };

    const requestData = {
        item: itemData
    };

  
    // Convert to JSON string
    const jsonRequestBody = JSON.stringify(requestData);

    //Body should comtain Item 
    //console.log(jsonRequestBody);
    //return false;

    $('#loader').show();
    $.ajax({
        type: 'POST',
        //contentType: 'application/json',
        dataType: 'json',
        url: (window.location.origin + '/AR/Item/SaveItem'),
        data: requestData,
        success: function (response) {
            $('#loader').hide();
            if (response.success) {
                let itemId = 0;
                //itemId = response.ItemId;
                if (itemId === 0) {
                    itemId = parseInt(response.data.itemId);
                    document.getElementById("itemId").value = itemId;

                    let redirectUrl = window.location.origin + "/AR/Item/Index";
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


    $("#frmItem").validate({
        errorClass: "invalid-form-control",
        ignore: "",
        invalidHandler: function (event, validator) {
            setTimeout(function () {
                showTabValidationError();
            });
        },
        rules: {
            itemname: {
                required: true,
                maxlength: 200
            },
            hsnno: {
                required: true,
                maxlength: 10
            },
            rate: {
                required: true
            },
            uom: {
                required: true
            },
            category: {
                required: true
            },
            cgst: {
                required: true
            },
            sgst: {
                required: true
            },
            igst: {
                required: true
            }
        },
        messages: {
            itemname: {
                required: "Enter Item Name",
                maxlength: "Item Name length should not exceed 200 characters"
            },
            hsnno: {
                required: "Enter HSNNo",
                maxlength: "HSNNo length should not exceed 10 characters"
            },
            rate: {
                required: "Enter Valid Rate"
            },
            uom: {
                required: "Select UOM"
            },
            category: {
                required: "Select Category"
            },
            cgst: {
                required: "Enter Valid CGST"
            },
            sgst: {
                required: "Enter Valid SGST"
            },
            igst: {
                required: "Enter Valid IGST"
            }

        }
    });
};

