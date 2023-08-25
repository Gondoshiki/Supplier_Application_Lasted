$(document).ready(function () {

$.ajax({
    method: "POST",
    //Call Function From url
    url: "ServiceTable.asmx/GetMyTask",
    data: { },
    dataType: "json",
    beforeSend: function (data) {
    },
    success: function (server) {
        var dt = JSON.parse(JSON.stringify(server))
        //Command Table
        //tbFile.style.width = "100%";
        $('#TaskTable').show();
        $('#TaskTable').DataTable({
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
                { data: 'UpdateBy' },
                { data: 'UpdateHidden', visible: false }

            ],
            "columnDefs": [

                //    { "targets": [5], "class": "Notwrap" },
                { "orderData": [6], "targets": 5 }, //search จาก col ที่ 2 โดยกด colที่ 1
                //    { "orderData": [13], "targets": 12 }

            ],
            order: [[6, 'des']], //เข้ามาครั้งแรก เรียงจาก col ที่ 13 มากไปน้อย
        });
        //tbFile.style.display = "inline";
        //$('#tbFile').style.display = "inline";


    },
    //error: function (result) {
    //    alert(result.message);
    //}
});
});