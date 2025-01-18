

$(document).on('click', '#saveCustomer', function (e) {

    hideTabValidationError();
    if (!$("#frmCustomer").valid()) {
        console.log("invalid form customer");
        return false;
    }

    let customerId = 0;

    //let customerId = parseInt(document.getElementById("customerId").value);
    let title = document.getElementById("title").value;
    let code = document.getElementById("code").value;
    let firstName = document.getElementById("firstName").value;
    let lastName = document.getElementById("lastName").value;
    let gstNo = document.getElementById("gstNo").value;
    let emailAddress = document.getElementById("emailAddress").value;
    let currency = document.getElementById("currency").value;
    let workPhone = document.getElementById("workPhone").value;
    let mobilephone = document.getElementById("mobilePhone").value;
    let panNo = document.getElementById("panNo").value;
    //let sezCustomer = document.getElementById("sezCustomer-yes").checked == true ? 1 : 0;
    let sezCustomer = document.getElementById("sezCustomer-yes").checked;
    let msmeCustomer = document.getElementById("msmeCustomer-yes").checked;
    let paymentTermId = document.getElementById("paymentTerms").value;

    let billContactName = document.getElementById("billContactName").value;
    let billAddress1 = document.getElementById("billAddress1").value;
    let billAddress2 = document.getElementById("billAddress2").value;
    let billCity = document.getElementById("billCity").value;
    let billState = document.getElementById("billState").value;
    let billZipcode = document.getElementById("billZipcode").value;
    let billWorkPhone = document.getElementById("billWorkPhone").value;
    let billWhatsappPhone = document.getElementById("billWhatsappPhone").value;

    let shipContactName = document.getElementById("shipContactName").value;
    let shipAddress1 = document.getElementById("shipAddress1").value;
    let shipAddress2 = document.getElementById("shipAddress2").value;
    let shipCity = document.getElementById("shipCity").value;
    let shipState = document.getElementById("shipState").value;
    let shipZipcode = document.getElementById("shipZipcode").value;
    let shipWorkPhone = document.getElementById("shipWorkPhone").value;
    let shipWhatsappPhone = document.getElementById("shipWhatsappPhone").value;

    let udf1 = document.getElementById("udf1").value;
    let udf2 = document.getElementById("udf2").value;
    let udf3 = document.getElementById("udf3").value;
    let udf4 = document.getElementById("udf4").value;
    let udf5 = document.getElementById("udf5").value;

    let requestBody = {
        "ID": customerId,
        "Title": title,
        "Code": code,
        "FirstName": firstName,
        "LastName": lastName,
        "GSTNo": gstNo,
        "EmailAddress": emailAddress,
        "Currency": currency,
        "WorkPhone": workPhone,
        "MobilePhone": mobilephone,
        "PanNo": panNo,
        "SEZCustomer": sezCustomer,
        "MSMECustomer": msmeCustomer,
        "BillContactName": billContactName,
        "BillAddress1": billAddress1,
        "BillAddress2": billAddress2,
        "BillCity": billCity,
        "BillState": billState,
        "BillZipcode": billZipcode,
        "BillWorkPhone": billWorkPhone,
        "BillWhatsappPhone": billWhatsappPhone,
        "ShipContactName": shipContactName,
        "ShipAddress1": shipAddress1,
        "ShipAddress2": shipAddress2,
        "ShipCity": shipCity,
        "ShipState": shipState,
        "ShipZipcode": shipZipcode,
        "ShipWorkPhone": shipWorkPhone,
        "ShipWhatsappPhone": shipWhatsappPhone,
        "TermsId": paymentTermId,
        "UDF1": udf1,
        "UDF2": udf2,
        "UDF3": udf3,
        "UDF4": udf4,
        "UDF5": udf5
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




function ShowCustomerBasicModal() {
    $("#customerBasicModal").modal('show');
};

function ShowEditCustomerBasic(customerId) {
    //let customerId = parseInt(document.getElementById("customerId").value);
   
    let requestBody = { "customerId": customerId };
    //console.log(requestBody);

    $('#loader').show();
    $.ajax({
        type: "POST",
        dataType: "json",
        url: (window.location.origin + "/AR/Customer/GetCustomer"),
        data: requestBody,
        success: function (response) {
            $('#loader').hide();
            if (response.success) {
                let customer = response.data;
                //console.log(customer);

                clearCustomerModalFields();

                document.getElementById("customerId").value = parseInt(customerId);
                //document.getElementById("saveCustomerContactTitle").innerHTML = "Update Customer Contact";


                //document.getElementById("customerContactID").value = customer.id;
                //$("#customerId").val(customer.id);
                document.getElementById("code").value = customer.code;
                document.getElementById("title").value = customer.title;
                document.getElementById("firstName").value = customer.firstName;
                document.getElementById("lastName").value = customer.lastName;
                document.getElementById("gstNo").value = customer.gstNo;
                document.getElementById("panNo").value = customer.panNo;
                document.getElementById("paymentTerms").value = customer.paymentTermID;
                document.getElementById("emailAddress").value = customer.emailAddress;
                document.getElementById("currency").value = customer.currency;
                $("#workPhone").val(customer.workPhone);
                $("#mobilePhone").val(customer.mobilePhone);
                document.getElementById("remarks").value = customer.remarks;

                document.getElementById("sezCustomer-yes").checked = customer.sezCustomer;
                document.getElementById("sezCustomer-no").checked = !customer.sezCustomer;

                document.getElementById("msmeCustomer-yes").checked = customer.msmeCustomer;
                document.getElementById("msmeCustomer-no").checked = !customer.msmeCustomer;

                $("#customerBasicModal").modal('show');
            }
        },
        error: function (er) {
            $('#loader').hide();
        }
    });
};


function ShowEditCustomerAddressOLD(customerId, addressType)
{
    
    //$("#customerAddressModal").modal('show');

    let requestBody = { "customerId": customerId, "addressType": addressType };
    //console.log(requestBody);

    $('#loader').show();
    $.ajax({
        type: "POST",
        dataType: "json",
        url: (window.location.origin + "/AR/Customer/GetDefaultAddress"),
        data: requestBody,
        success: function (response) {
            $('#loader').hide();
            if (response.success) {
                let customerAddress = response.data;

                clearAddressModalFields();

                document.getElementById("customerAddressId").value = parseInt(customerId);
                document.getElementById("addressType").value = parseInt(addressType);

                if (customerAddress == null) {
                    $("#customerAddressModal").modal('show');
                    return;
                }
                          
                
                document.getElementById("contactName").value = customerAddress.contactName;
                document.getElementById("address1").value = customerAddress.address1;
                document.getElementById("address2").value = customerAddress.address2;
                document.getElementById("city").value = customerAddress.city;
                document.getElementById("state").value = customerAddress.state;
                document.getElementById("zipcode").value = customerAddress.zipcode;
                document.getElementById("workPhone").value = customerAddress.workPhone;
                document.getElementById("whatsappPhone").value = customerAddress.whatsAppPhone;
                $("#customerAddressModal").modal('show');
               
            }
        },
        error: function (er) {
            $('#loader').hide();
        }
    });

}

function AddNewCustomerAddress(addressType, customerId, addressId, listItem) {
    clearAddressModalFields();
    document.getElementById("addressType").value = addressType;
    ShowEditCustomerAddress(customerId, addressId, listItem);
}

function ShowEditCustomerAddress(customerId, addressId, listItem) {

    //$("#customerAddressModal").modal('show');

    if (addressId == 0) {
        //let addressType = document.getElementById("addressType").value;
        //console.log("address Type: " + addressType);
        document.getElementById("customerId").value = parseInt(customerId);
        $("#customerAddressModal").modal('show');
        return;
    }

    let requestBody = { "customerId": customerId, "addressId": addressId };
    //console.log(requestBody);

    $('#loader').show();
    $.ajax({
        type: "POST",
        dataType: "json",
        url: (window.location.origin + "/AR/Customer/GetCustomerAddress"),
        data: requestBody,
        success: function (response) {
            $('#loader').hide();
            if (response.success) {
                let customerAddress = response.data;

               
                //document.getElementById("addressType").value = parseInt(addressType);
                clearAddressModalFields();

                document.getElementById("customerId").value = parseInt(customerId);
                document.getElementById("customerAddressId").value = parseInt(addressId);

                if (customerAddress == null) {
                    $("#customerAddressModal").modal('show');
                    return;
                }

                document.getElementById("addressType").value = parseInt(customerAddress.addressType);
                document.getElementById("contactName").value = customerAddress.contactName;
                document.getElementById("address1").value = customerAddress.address1;
                document.getElementById("address2").value = customerAddress.address2;
                document.getElementById("city").value = customerAddress.city;
                //document.getElementById("custstate").value = customerAddress.state;
                stateDropdown.setChoiceByValue(customerAddress.state);
                document.getElementById("zipcode").value = customerAddress.zipcode;
                document.getElementById("contactEmail").value = customerAddress.emailAddress;
                document.getElementById("contactWorkPhone").value = customerAddress.workPhone;
                document.getElementById("contactWhatsappPhone").value = customerAddress.whatsAppPhone;
                $("#customerAddressModal").modal('show');

            }
        },
        error: function (er) {
            $('#loader').hide();
        }
    });

}

function UpdateCustomerAddress() {

    hideTabValidationError();
    if (!$("#frmCustomerAddress").valid()) {
        console.log("invalid form customer address");
        return false;
    }

    let customerId = parseInt(document.getElementById("customerId").value);
    let addressId = document.getElementById("customerAddressId").value;
    let addressType = document.getElementById("addressType").value;
    let contactName = document.getElementById("contactName").value;
    let address1 = document.getElementById("address1").value;
    let address2 = document.getElementById("address2").value;
    let city = document.getElementById("city").value;
    let state = document.getElementById("state").value;
    let zipcode = document.getElementById("zipcode").value;
    let workPhone = document.getElementById("contactWorkPhone").value;
    let whatsappPhone = document.getElementById("contactWhatsappPhone").value;
    let contactEmail = document.getElementById("contactEmail").value;

    
    let requestBody = {
        "AddressID": addressId,
        "CustomerID": customerId,
        "AddressType": addressType,
        "ContactName": contactName,
        "Address1": address1,
        "Address2": address2,
        "City": city,
        "State": state,
        "Zipcode": zipcode,
        "WorkPhone": workPhone,
        "WhatsAppPhone": whatsappPhone,
        "EmailAddress": contactEmail
    };

    console.log(requestBody);
    //return false;

    $('#loader').show();
    $.ajax({
        type: "POST",
        dataType: "json",
        url: (window.location.origin + "/AR/Customer/SaveCustomerAddress"),
        data: requestBody,
        success: function (response) {
            $('#loader').hide();
            if (response.success) {

                refreshBillingAddressList(response);

                setTimeout(function () {
                    $("#customerAddressModal").modal('hide');
                }, 2000);
            }
        },
        error: function (er) {
            $('#loader').hide();
        }
    });
    
}

function SetDefaultCustomerAddress(customerId, addressId, addressType) {

    let requestBody = { "customerId": customerId, "addressId": addressId };
    //console.log(requestBody);

    $('#loader').show();
    $.ajax({
        type: "POST",
        dataType: "json",
        url: (window.location.origin + "/AR/Customer/UpdateCustomerDefaultAddress"),
        data: requestBody,
        success: function (response) {
            $('#loader').hide();
            if (response.success) {
                
                refreshBillingAddressList(response);

                setTimeout(function () {
                    $("#customerAddressModal").modal('hide');
                }, 2000);
            }
        },
        error: function (er) {
            $('#loader').hide();
        }
    });

}

function refreshBillingAddressList(data) {

    var htmlContent = '';
    data.adrressList.forEach(function (address) {
        htmlContent += '<li id="addressItem_' + address.addressID + '" class="list-group-item fw-semibold">';
        htmlContent += '<div class="d-flex align-items-center justify-content-between mb-2">';
        htmlContent += '<span class="fw-semibold">' + address.contactName + '</span>';

        if (address.isDefault == null || !address.isDefault) {
            htmlContent += '<div><button type="button" class="m-1 btn btn-success btn-sm shadow-sm btn-wave" onclick="SetDefaultCustomerAddress(' + address.customerID + ',' + address.addressID + ',' + address.addressType + ');">Default</button>';
            htmlContent += '<button type="button" class="m-1 btn btn-danger btn-icon btn-primary btn-sm" onclick="ShowDeleteCustomerAddress(' + address.customerID + ',' + address.addressID + ');"><i class="ri-delete-bin-line"></i></button>';
            htmlContent += '<button type="button" class="m-1 btn btn-icon btn-wave btn-primary btn-sm" onclick="ShowEditCustomerAddress(' + address.customerID + ', ' + address.addressID + ', \'addressItem_' + address.addressID + '\');"><i class="ri-pencil-line"></i></button></div>';
        }
        else {
            htmlContent += '<div><button type="button" class="btn btn-icon btn-wave btn-primary btn-sm" onclick="ShowEditCustomerAddress(' + address.customerID + ', ' + address.addressID + ', \'addressItem_' + address.addressID + '\');"><i class="ri-pencil-line"></i></button></div>';
        }
        htmlContent += '</div>'
        htmlContent += '<p class="mb-1 text-muted fs-14">' + address.address1 + '</p>';
        htmlContent += '<p class="mb-1 text-muted fs-14">' + address.address2 + '</p>';
        htmlContent += '<p class="mb-1 text-muted fs-14">' + address.city + ', ' + address.state + ' ' + address.zipcode + '</p>';
        htmlContent += '</li>';

    });

    if (data.addressType == 1)
        $('#lstBillingAddress').html(htmlContent);
    else
        $('#lstShippingAddress').html(htmlContent);

}


function UpdateCustomerBasic() {
    hideTabValidationError();
    if (!$("#frmCustomerBasic").valid()) {
        console.log("invalid form customer");
        return false;
    }

    let customerId = parseInt(document.getElementById("customerId").value);
    let title = document.getElementById("title").value;
    let code = document.getElementById("code").value;
    let firstName = document.getElementById("firstName").value;
    let lastName = document.getElementById("lastName").value;
    let gstNo = document.getElementById("gstNo").value;
    let emailAddress = document.getElementById("emailAddress").value;
    let currency = document.getElementById("currency").value;
    let workPhone = document.getElementById("workPhone").value;
    let mobilephone = document.getElementById("mobilePhone").value;
    let panNo = document.getElementById("panNo").value;

    let sezCustomer = document.getElementById("sezCustomer-yes").checked;
    let msmeCustomer = document.getElementById("msmeCustomer-yes").checked;
    let paymentTermId = document.getElementById("paymentTerms").value;

    
    let requestBody = {
        "ID": customerId,
        "Title": title,
        "Code": code,
        "FirstName": firstName,
        "LastName": lastName,
        "GSTNo": gstNo,
        "EmailAddress": emailAddress,
        "Currency": currency,
        "WorkPhone": workPhone,
        "MobilePhone": mobilephone,
        "PanNo": panNo,
        "SEZCustomer": sezCustomer,
        "MSMECustomer": msmeCustomer,
        "TermsID": paymentTermId
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

                $("#lblTitle").text(title + '(' + code + ')');
                $("#lblEmailAddress").text(emailAddress);
                $("#lblName").text(firstName + ' ' + lastName);
                $("#lblMobilePhone").text(mobilephone);
                $("#lblGSTNo").text(gstNo);
                $("#lblPanNo").text(panNo);
                $("#lblSEZCustomer").text(sezCustomer===true?'Yes':'No');
                $("#lblMSMECustomer").text(msmeCustomer===true?'Yes':'No');
                
                    setTimeout(function () {
                        $("#customerBasicModal").modal('hide');
                    }, 2000);
            }
        },
        error: function (er) {
            $('#loader').hide();
        }
    });
}

function clearCustomerModalFields() {
    // Get all input elements within the modal
    var inputs = document.querySelectorAll('#customerBasicModal input');

    // Iterate through each input and set its value to an empty string
    inputs.forEach(function (input) {
        input.value = '';
    });

    // Handle radio buttons separately
    var radioButtons = document.querySelectorAll('#customerBasicModal input[type="radio"]');
    radioButtons.forEach(function (radioButton) {
        radioButton.checked = false;
    });

    // Clear textarea
    document.getElementById('remarks').value = '';
};

function clearAddressModalFields() {
    // Get all input elements within the modal
    var inputs = document.querySelectorAll('#customerAddressModal input');

    // Iterate through each input and set its value to an empty string
    inputs.forEach(function (input) {
        input.value = '';
    });

    // Handle radio buttons separately
    var radioButtons = document.querySelectorAll('#customerAddressModal input[type="radio"]');
    radioButtons.forEach(function (radioButton) {
        radioButton.checked = false;
    });

   
};

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
        invalidHandler: function (event, validator) {
            setTimeout(function () {
                showTabValidationError();
            });

            var errors = validator.numberOfInvalids();
            if (errors) {
                var invalidElement = $(validator.errorList[0].element);
                var accordionItem = invalidElement.closest('.accordion-item');

                // If the invalid field is inside an accordion item, open that item
                if (accordionItem.length > 0) {

                    var dataBsTarget = accordionItem.find('[data-bs-toggle="collapse"]').attr('data-bs-target');

                    var accordionItemId = dataBsTarget ? dataBsTarget.replace('#', '') : undefined;

                    // Trigger Bootstrap collapse event to open the accordion item
                    if (accordionItemId) {
                        var accordionInstance = new bootstrap.Collapse(document.getElementById(accordionItemId));
                        accordionInstance.show();
                    }
                }
            }

        },
        rules: {
            title: {
                required: true,
                maxlength: 200
            },
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
            },
            billAddress1: {
                required: true,
                maxlength: 100
            },
            billZipcode: {
                required: true,
                maxlength: 10
            },
        },
        messages: {
            title: {
                required: "Enter title",
                maxlength: "title length should not exceed 200 characters"
            },
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
                email: "Please enter valid emailaddress",
                maxlength: "Email Address length should not exceed 10 characters"
            },
            mobilePhone: {
                inTelephone: "Please enter valid Phone number",
                maxlength: "Mobile Phone length should not exceed 20 characters"
            },
            billAddress1: {
                required: "Please enter billing address",
                maxlength: "Address should length not exceed 100 characters"
            },
            billZipcode: {
                maxlength: "Zipcode length should not exceed 10 characters"
            }
        }
    });

    $("#frmCustomerBasic").validate({
        errorClass: "invalid-form-control",
        ignore: "",
        invalidHandler: function (event, validator) {
            setTimeout(function () {
                showTabValidationError();
            });
        },
        rules: {
            title: {
                required: true,
                maxlength: 200
            },
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
            },
            paymentTerms: {
                required: true
            }
        },
        messages: {
            title: {
                required: "Enter title",
                maxlength: "title length should not exceed 200 characters"
            },
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
                email: "Please enter valid emailaddress",
                maxlength: "Email Address length should not exceed 10 characters"
            },
            mobilePhone: {
                inTelephone: "Please enter valid Phone number",
                maxlength: "Mobile Phone length should not exceed 20 characters"
            },
            paymentTerms: {
                required: "select a payment Term"
            }
        }
    });

    $("#frmCustomerAddress").validate({
        errorClass: "invalid-form-control",
        ignore: "",
        invalidHandler: function (event, validator) {
            setTimeout(function () {
                showTabValidationError();
            });
        },
        rules: {
            contactName: {
                required: true,
                maxlength: 200
            },
            address1: {
                required: true,
                maxlength: 200
            },
            zipcode: {
                required: true,
                maxlength: 20
            },
    
            contactEmail: {
                email: true,
                maxlength: 100
            }
        },
        messages: {
            contactName: {
                required: "Enter contact name",
                maxlength: "contact name length should not exceed 200 characters"
            },
            address1: {
                required: "Enter address1",
                maxlength: "address1 length should not exceed 200 characters"
            },
            zipcode: {
                required: "Enter zipcode",
                maxlength: "zipcode length should not exceed 20 characters"
            },
            contactEmail: {
                email: "Please enter valid emailaddress",
                maxlength: "Email Address length should not exceed 10 characters"
            }
        }
    });

};

function ShowDeleteCustomerAddress(customerId, customerAddressId) {
    document.getElementById("delCustomerAddressId").value = customerAddressId;
    document.getElementById("delCustomerId").value = customerId;

    $('#delcustomerAddressModal').modal('show');
};

$(document).on('click', '#delCustomerAddressYes', function (e) {

    let customerId = document.getElementById("delCustomerId").value;
    let addressId = parseInt(document.getElementById("delCustomerAddressId").value);

    let requestBody = {
        "customerId": customerId,
        "customerAddressId": addressId
    };

    console.log(requestBody);
    //return false;

    $('#loader').show();
    $.ajax({
        type: "POST",
        dataType: "json",
        url: (window.location.origin + "/AR/Customer/DeleteCustomerAddress"),
        data: requestBody,
        success: function (response) {
            $('#loader').hide();
            if (response.success) {

                refreshBillingAddressList(response);

                setTimeout(function () {
                    $("#delcustomerAddressModal").modal('hide');
                }, 2000);
            }
        },
        error: function (er) {
            $('#loader').hide();
        }
    });
});


let stateDropdown;

$(document).ready(function () {

    //SetupDataTables();
    //console.log("In ar_customer js file");

    SetupFormValidation();

    
    stateDropdown = new Choices(document.getElementById('state'), {});
    // Add the options
    //stateDropdown.setValue([
    //    { value: 'AP', label: 'Andhra Pradesh' },
    //    { value: 'BR', label: 'Bihar' },
    //    { value: 'MP', label: 'Madhya Pradesh' },
    //    { value: 'MH', label: 'Maharashtra' },
    //    { value: 'MN', label: 'Manipur' }
    //], 'value', 'label', true);

                     
});