function oLog(type, title, text) {
    swal.fire({
        icon: type,
        title: title,
        text: text,
        timer: 5000
    }).then(function () {
        //Redirect to Another Pages
        window.location.href = "PS_MyTask.aspx"
    });
}
function oLogFail(type, title, text) {
    swal.fire({
        icon: type,
        title: title,
        text: text,
        timer: 5000
    }).then(function () {
        //Redirect to Another Pages
        window.location.href = "PS_Login.aspx"
    });
}
function oAlert(type, title, text) {
    swal.fire({
        icon: type,
        title: title,
        text: text,
        timer: 5000
    }).then(function () {
        //Redirect to Another Pages
        // window.location.href = "PS_Login.aspx"
    });
}
function showloading() {
    swal.fire({
        title: 'Processing...',
        didOpen: () => {
            swal.showLoading();
        },
    })
    return true;
}
function oRedirect(type, title, text, linkURL) {
    swal.fire({
        icon: type,
        title: title,
        text: text,
        timer: 5000
    }).then(function () {

        //Redirect to Another Pages
        window.location.href = linkURL
    });
}
