$(document).ready(function () {

    let searchParams = new URLSearchParams(window.location.search);
    var id = null;
    var id = searchParams.get('id');
    var CookieID = $('#Body_CreateID').val();
    var CreateBy = $('#Body_CreateCom').val();
    $.ajax({
        method: "POST",
        //Call Function From url
        url: "Service.asmx/FileProcess",
        data: { ID: id },
        dataType: "json",
        beforeSend: function (data) {
        },
        success: function (server) {
            if (CookieID == CreateBy) {
                var dt = JSON.parse(JSON.stringify(server))
                //Command Table
                //tbFile.style.width = "100%";
                $('#FileList').show();
                $('#FileList').DataTable({
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
                    ],
                });

            }
            else {
                var dt = JSON.parse(JSON.stringify(server))
                //Command Table
                //tbFile.style.width = "100%";
                $('#FileList').show();
                $('#FileList').DataTable({
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
                        { data: 'Source', visible: false },
                    ],
                });
            }
        },

    });




    var CookieID = $('#Body_CreateID').val();
    var CreateBy = $('#Body_CreateCom').val();



    var UploadFile = $('#Body_UploadFile').val();
    if (CookieID == CreateBy) {
        $("#btnFinishModal").show(); $("#btnRejectModal").show(); $("#btnReviseModal").show();
    }
    else {
        $("#btnFinishModal").hide(); $("#btnRejectModal").hide(); $("#btnReviseModal").hide();

    }
    if (UploadFile == 1) {
        $("#cardUpload").show();
    }
    else {
        $("#cardProcess").show();
        $("#btnFinishModal").hide();
        $("#btnReviseModal").hide();
    }
});