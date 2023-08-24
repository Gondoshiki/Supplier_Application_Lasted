$(document).ready(function () {

    let searchParams = new URLSearchParams(window.location.search);
    var id = null;
    var id = searchParams.get('id');
    $.ajax({
        method: "POST",
        //Call Function From url
        url: "ServiceTable.asmx/ReviseUploadList",
        data: { ID: id },
        dataType: "json",
        beforeSend: function (data) {
        },
        success: function (server) {
            var dt = JSON.parse(JSON.stringify(server))
            //Command Table
            //tbFile.style.width = "100%";
            $('#ReviseTable').show();
            $('#ReviseTable').DataTable({
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

                ],
            });
        },

    });
});