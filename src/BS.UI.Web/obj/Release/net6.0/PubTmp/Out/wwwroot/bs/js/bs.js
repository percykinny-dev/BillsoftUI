function formatPhoneNumber(obj) {
    var val = obj.value;
    obj.value = val
        .replace(/\D/g, '')
        .replace(/(\d{1,3})(\d{1,3})?(\d{1,4})?/g, function (txt, f, s, t) {
            if (t) {
                return `(${f}) ${s}-${t}`
            } else if (s) {
                return `(${f}) ${s}`
            } else if (f) {
                return `(${f})`
            }
        });
}

function IsValidDate(date) {
    return date instanceof Date && !isNaN(date);
}

function formatNumbers() {
    $('.formatNumber').each(function (i, obj) {
        let strNum = obj.innerHTML;
        var x = strNum.replace(',', '');
        let num = parseFloat(x);

        if (num < 0) {
            obj.style.color = "red";
        }
        obj.style.textAlign = "right";
        /*obj.style.fontWeight = "bold";*/
        obj.innerHTML = num.toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
    });

    $('.formatNumberInput').each(function (i, obj1) {
        let strNum1 = obj1.value;
        var x1 = strNum1.replace(',', '');
        let num1 = parseFloat(x1);

        if (isNaN(num1)) {
            console.log('nan num1', obj1.value);
            obj1.value = "0.00";
        }
        else {
            obj1.value = num1.toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
        }
    });
}

function ConvertBase64ToArrayBuffer(base64) {
    var binaryString = window.atob(base64);
    var binaryLen = binaryString.length;
    var bytes = new Uint8Array(binaryLen);
    for (var i = 0; i < binaryLen; i++) {
        var ascii = binaryString.charCodeAt(i);
        bytes[i] = ascii;
    }
    return bytes;
}

function saveByteArray(filename, mimetype, bytes) {
    var blob = new Blob([bytes], { type: mimetype });
    var link = document.createElement('a');
    link.href = window.URL.createObjectURL(blob);
    link.download = filename;
    $('#loader').hide();
    link.click();
};

function download(response, reportType) {
    $('#loader').show();
    var filename = response.filename;
    var filedata = response.filedata;
    var mimetype = getmimetype(reportType);

    var sampleArr = ConvertBase64ToArrayBuffer(filedata);
    saveByteArray(filename, mimetype, sampleArr);
}

function getmimetype(reportType) {
    if (reportType === 1)
        return "application/pdf";
    if (reportType === 2)
        return "application/doc";
    if (reportType === 3)
        return "application/xls";
}

$(document).on("click", "#app", function () {
    if ($(this).hasClass("app-sidebar-minified")) {
        $(".menu-caret").hide();
    }
    else {
        $(".menu-caret").show();
    }
});

$(document).on("click", ".paginate_button", function (e) {
    formatNumbers();
});

$(document).on("click", ".sorting", function (e) {
    formatNumbers();
});

$(document).on("blur", "input.formatNumber", function (e) {
    formatNumbers();
});

//$(document).ajaxComplete(function (event, xhr, settings) {
//    console.log("ajaxComplete");
//    console.log(event);
//    console.log(xhr);
//});

$(document).ajaxError(function (event, jqxhr, settings, thrownError) {
    //console.log("ajaxError");
    //console.log(event);
    //console.log(jqxhr);

    if (jqxhr.statusText === "error" && jqxhr.status === 401)
        window.location.href = window.location.origin + "/login?code=1001";
});

function showTabValidationError() {
    $('span.errorText').removeClass("errorText");
    $('.tab-content.tab-validate .tab-pane:has(input.invalid-form-control)').each(function () {
        let id = $(this).attr('id');
        console.log('input', id);
        $('.nav-tabs').find('div[data-target^="#' + id + '"]').find('span').addClass("errorText");
    });

    $('.tab-content.tab-validate .tab-pane:has(select.invalid-form-control)').each(function () {
        let id = $(this).attr('id');
        console.log('select', id);
        $('.nav-tabs').find('div[data-target^="#' + id + '"]').find('span').addClass("errorText");
    });
}

function hideTabValidationError() {
    $('span.errorText').removeClass('errorText');
}

function ping() {
    console.log("call to ping");
    $.ajax({
        type: "GET",
        dataType: "json",
        url: (window.location.origin + "/sy/dashboard/ping"),
        success: function (response) {
            console.log(response);
            if (response.success == false) {
                alert(response.msg);
            }
        },
        error: function (er) {
        }
    });
}

////function getDefaultSegments(callbackFunc) {
////    if (defaultSegmentsLoaded == false) {
////        $.ajax({
////            type: "GET",
////            dataType: "json",
////            url: (window.location.origin + "/gl/segment/getdefaultsegments"),
////            success: function (response) {
////                if (response.success == true) {
////                    defaultsegments = response.data;
////                    defaultSegmentsLoaded = true;
////                    if (response.data.length > 0) {
////                        defSeg1Id = defaultsegments.find(item => item.seq === 1);
////                        defSeg2Id = defaultsegments.find(item => item.seq === 2);
////                        defSeg3Id = defaultsegments.find(item => item.seq === 3);
////                        defSeg4Id = defaultsegments.find(item => item.seq === 4);
////                        defSeg5Id = defaultsegments.find(item => item.seq === 5);
////                        callbackFunc;
////                    }
////                }
////            },
////            error: function () {
////            }
////        });
////    }
////    else {
////        callbackFunc;
////    }
////}

let defaultSegmentsLoaded = false;
let defaultsegments = [];
let defSeg1Id = null;
let defSeg2Id = null;
let defSeg3Id = null;
let defSeg4Id = null;
let defSeg5Id = null;

function setDefaultSegments(ctrlId1, ctrlId2, ctrlId3, ctrlId4, ctrlId5) {
    if (defaultSegmentsLoaded == false) {
        $.ajax({
            type: "GET",
            dataType: "json",
            async: "false",
            url: (window.location.origin + "/gl/segment/getdefaultsegments"),
            success: function (response) {
                if (response.success == true) {
                    defaultsegments = response.data;
                    defaultSegmentsLoaded = true;
                    if (response.data.length > 0) {
                        defSeg1Id = defaultsegments.find(item => item.seq === 1);
                        defSeg2Id = defaultsegments.find(item => item.seq === 2);
                        defSeg3Id = defaultsegments.find(item => item.seq === 3);
                        defSeg4Id = defaultsegments.find(item => item.seq === 4);
                        defSeg5Id = defaultsegments.find(item => item.seq === 5);

                        setDefaultSegmentValues();
                    }
                }
            },
            error: function () {
            }
        });
    }
    else {
        setDefaultSegmentValues();
    }

    function setDefaultSegmentValues() {
        let seg1Ctrl = document.getElementById(ctrlId1);
        if (seg1Ctrl != null && seg1Ctrl != undefined && seg1Ctrl.value == "" && defSeg1Id != null && defSeg1Id != undefined) {
            seg1Ctrl.value = defSeg1Id.id;
            seg1Ctrl.focus();
            seg1Ctrl.blur();
        }

        let seg2Ctrl = document.getElementById(ctrlId2);
        if (seg2Ctrl != null && seg2Ctrl != undefined && seg2Ctrl.value == "" && defSeg2Id != null && defSeg2Id != undefined) {
            seg2Ctrl.value = defSeg2Id.id;
            seg2Ctrl.focus();
            seg2Ctrl.blur();
        }

        let seg3Ctrl = document.getElementById(ctrlId3);
        if (seg3Ctrl != null && seg3Ctrl != undefined && seg3Ctrl.value == "" && defSeg3Id != null && defSeg3Id != undefined) {
            seg3Ctrl.value = defSeg3Id.id;
            seg3Ctrl.focus();
            seg3Ctrl.blur();
        }

        let seg4Ctrl = document.getElementById(ctrlId4);
        if (seg4Ctrl != null && seg4Ctrl != undefined && seg4Ctrl.value == "" && defSeg4Id != null && defSeg4Id != undefined) {
            seg4Ctrl.value = defSeg4Id.id;
            seg4Ctrl.focus();
            seg4Ctrl.blur();
        }

        let seg5Ctrl = document.getElementById(ctrlId5);
        if (seg5Ctrl != null && seg5Ctrl != undefined && seg5Ctrl.value == "" && defSeg5Id != null && defSeg5Id != undefined) {
            seg5Ctrl.value = defSeg5Id.id;
            seg5Ctrl.focus();
            seg5Ctrl.blur();
        }
    }
}

Date.prototype.addDays = function (days) {
    var date = new Date(this.valueOf());
    date.setDate(date.getDate() + days);
    return date;
}

function convertDateToString(inputDate) {
    let date, month, year;

    date = inputDate.getDate();
    month = inputDate.getMonth() + 1;
    year = inputDate.getFullYear();

    date = date
        .toString()
        .padStart(2, '0');

    month = month
        .toString()
        .padStart(2, '0');

    return `${month}/${date}/${year}`;
}


// Create an Intl.NumberFormat formatter with desired options
////const masNumberFormatter = new Intl.NumberFormat('en-US', {
////    style: 'decimal',
////    minimumFractionDigits: 2,
////    maximumFractionDigits: 2
////});

const masDecimalNumberOptions = {
    minimumFractionDigits: 2,
    maximumFractionDigits: 2
};

//SEARCH TEXT BOX FUNCTIONALITY
function showResult(event, ui) {
    //console.log(event);
    event.preventDefault();
    console.log(ui);
    const filtered_menuItems = menuItems.filter(item => item.label === ui.item.label);
    if (filtered_menuItems != null && filtered_menuItems != undefined && filtered_menuItems.length == 1) {
        //window.location.href = ui.item.value;
        window.location.href = filtered_menuItems[0].value;
    }

}

var menuItems = [];
var menus = [];

$(document).ready(function (e) {

    if (document.body.clientWidth <= 1500) {
        const viewport = document.querySelector("meta[name=viewport]");
        viewport.setAttribute("content", "width=device-width, initial-scale=0.67, user-scalable=0");
    }


    $("div.hasnosub").each(function (index) {
        //console.log($(this).text());
        //console.log($(this).find('a.menu-link').attr("href"));
        //console.log($(this).find('a.menu-link span.menu-text').text());

        let k = $(this).find('a.menu-link span.menu-text').text();
        let v = $(this).find('a.menu-link').attr("href");

        let obj = {
            label: k,
            value: window.location.origin + v
        };

        menus.push(k);
        menuItems.push(obj);
    });

    $("#searched2").autocomplete({
        source: menus,
        minLength: 2,
        maxShowItems: 10,
        max: 10,
        select: showResult,
        //focus: showResult,
        change: showResult
    });

    //console.log(menuItems);

    let tims = 1000 * 60 * 5;
    setInterval(function () { ping(); }, tims);

});