    function oRegis(type, title, text) {
        swal.fire({
            icon: type,
            title: title,
            text: text,
            timer: 5000
        }).then(function () {
            window.location.href = "Vendor_Login.aspx"
        });
    }

function oAlert(type, title, text) {
    swal.fire({
        icon: type,
        title: title,
        text: text,
        timer: 5000
    });
}