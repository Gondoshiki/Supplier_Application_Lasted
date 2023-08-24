$(document).ready(function () {
    
    $.ajax({
        method: "POST",
        //Call Function From url
        url: "ServiceTable.asmx/SA_User",
        data: { },
        dataType: "json",
        beforeSend: function (data) {
        },
        success: function (server) {
            var dt = JSON.parse(JSON.stringify(server))
            //Command Table
            //tbFile.style.width = "100%";
            $('#UserTable').show();
            $('#UserTable').DataTable({
                //searching: false,
                //paging: false,
                //info: false,
                responsive: true,
                data: dt,
                //"bDestroy": true,                    
                columns: [
                    // { data: 'FileName' },
                    { data: 'EmpID' },
                    { data: 'FullName' },
                    { data: 'Department' },
                    { data: 'PositionName' },
                    { data: 'Email' },
                    { data: 'Phone' },
                    { data: 'Update_Date' },
                    { data: 'Source0' },
                    { data: 'Source2' ,visible: false },
                    { data: 'Source' },
                    { data: 'Source1' },
                    { data: 'Hidden_Date', visible: false }
                ],
                "columnDefs": [

                //    { "targets": [5], "class": "Notwrap" },
                    { "orderData": [6], "targets": 9 }, //search จาก col ที่ 2 โดยกด colที่ 1
                //    { "orderData": [13], "targets": 12 }

                ],
                 order: [[9, 'des']], //เข้ามาครั้งแรก เรียงจาก col ที่ 13 มากไปน้อย
            });
            //tbFile.style.display = "inline";
            //$('#tbFile').style.display = "inline";


        },
        //error: function (result) {
        //    alert(result.message);
        //}
    });
});