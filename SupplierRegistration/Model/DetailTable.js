$(document).ready(function () {
    //const urlParams = new URLSearchParams(window.location.search);
    //const id = urlParams.get('id');
    //var incidentStatus = $('#Body_IncidentStatus').val();
    $.ajax({
        method: "POST",
        //Call Function From url
        url: "ServiceTable.asmx/GetListDetail",
        data: {},
        dataType: "json",
        beforeSend: function (data) {
        },
        success: function (server) {
            var dt = JSON.parse(JSON.stringify(server))
            //Command Table
            //tbFile.style.width = "100%";
            $('#DetailTable').show();
            $('#DetailTable').DataTable({
                //searching: false,
                //paging: false,
                //info: false,
                responsive: true,
                data: dt,
                //"bDestroy": true,                    
                columns: [
                    // { data: 'FileName' },
                    { data: 'App_ID' },
                    { data: 'V_Name' },
                    { data: 'V_PIC' },
                    { data: 'Email' },
                    { data: 'Status' },

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