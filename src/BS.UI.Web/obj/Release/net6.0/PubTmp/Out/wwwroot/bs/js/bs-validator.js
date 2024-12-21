$.validator.addMethod('inTelephone', function (value) {
    if (value == null || value == undefined || value == '')
        return true;

    return /^\s*(?:\+?(\d{1,3}))?[-. (]*(\d{3})[-. )]*(\d{3})[-. ]*(\d{4})(?: *x(\d+))?\s*$/.test(value);
}, '');