$(document).ready(function () {

    $.ajax({
        method: "POST",
        //Call Function From url
        url: "Service.asmx/Upload_Click",
        data: {
            ID: App_ID,
            ReAppRegis = File1.Value,
            ReBOJ5 = File2.Value,
            ReBookBank = File3.Value,
            ReOrgCompany = File4.Value,
            RePP20 = File5.Value,
            ReRegisCert = File6.Value,
            ReSPS10 = File7.Value},
        dataType: "json",
        beforeSend: function (data) {
        },

    });


});