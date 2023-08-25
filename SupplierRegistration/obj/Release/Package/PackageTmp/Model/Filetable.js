$(document).ready(function () {

    $.ajax({
        method: "POST",
        //Call Function From url
        url: "Service.asmx/GetFormList",
        data: {},
        dataType: "json",
        beforeSend: function (data) {
        },
        success: function (server) {
            var dt = JSON.parse(JSON.stringify(server))
            //Command Table
            //tbFile.style.width = "100%";
            $('#Filetable').show();
            $('#Filetable').DataTable({
                order: [],
                searching: false,
                paging: false,
                info: false,
                responsive: true,
                data: dt,
                //"bDestroy": true,                    
                columns: [
                    // { data: 'FileName' },
                    { data: 'Title' },
                    { data: 'FileName' },
                    { data: 'Source' },
                    { data: 'Source1' }
                ],
                //"columnDefs": [

                //    { "targets": [5], "class": "Notwrap" },
                //    { "orderData": [2], "targets": 1 }, //search จาก col ที่ 2 โดยกด colที่ 1
                //    { "orderData": [13], "targets": 12 }

                //],
                /* order: [[13, 'des']],*/ //เข้ามาครั้งแรก เรียงจาก col ที่ 13 มากไปน้อย
            });
            //tbFile.style.display = "inline";
            //$('#tbFile').style.display = "inline";


        },
        //error: function (result) {
        //    alert(result.message);
        //}
    });

});