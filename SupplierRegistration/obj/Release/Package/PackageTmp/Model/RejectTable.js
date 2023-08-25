$(document).ready(function () {

    let searchParams = new URLSearchParams(window.location.search);
    var id = null;
    var id = searchParams.get('id');
    $.ajax({
        method: "POST",
        //Call Function From url
        url: "Service.asmx/FileProcess",
        data: { ID: id },
        dataType: "json",
        beforeSend: function (data) {
        },
        success: function (server) {
            var dt = JSON.parse(JSON.stringify(server))
            //Command Table
            //tbFile.style.width = "100%";
            $('#cardUpload').show();
            $('#FileReject').show();
            $('#FileReject').DataTable({
                order: [],
                //searching: false,
                //paging: false,
                //info: false,
                searching: false,
                paging: false,
                info: false,
                responsive: true,
                data: dt,
                //"bDestroy": true,                    
                columns: [

                    { data: 'Title' },
                    { data: 'FileName' },
                    { data: 'Source', visible: false },
                ],
            });

        },

    });



});