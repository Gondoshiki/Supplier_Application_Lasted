$(document).ready(function () {
    $.ajax({
        method: "POST",
        url: "Service.asmx/ManualLink",
        data: {},
        dataType: "json",
        beforeSend: function (data) {

        },
        success: function (server) {

            if (server.length > 0) {
                var lbLinkManual = document.getElementById("lb_Manual");
                lbLinkManual.innerHTML = server;
            }
        }
    });
});
