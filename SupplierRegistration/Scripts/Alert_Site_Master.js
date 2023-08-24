function oConfig(type, title, text) {
    swal.fire({
        icon: type,
        title: title,
        text: text,
        timer: 5000
    }).then(function () {
        window.location.href = "Config.aspx";
    })
}
function oAlert(type, title, text) {
    swal.fire({
        icon: type,
        title: title,
        text: text,
        timer: 5000
    });
}
function oSubmit(type, title, text, Id) {
    swal.fire({
        icon: type,
        title: title,
        text: text,
        timer: 5000
    }).then(function () {
        window.location.href = "PS_Detail.aspx?id=" + Id
    });
}
function oSaveDraft(type, title, text, Id) {
    swal.fire({
        icon: type,
        title: title,
        text: text,
        timer: 5000
    }).then(function () {
        window.location.href = "Draft.aspx?id=" + Id
    });
}
function oFail(type, title, text) {
    document.body.classList.add("CookiesAlert");
    swal.fire({
        icon: type,
        title: title,
        text: text,
        /* timer: 3000*/
    }).then(function () {
        window.location.href = "PS_Login.aspx"
        document.body.classList.remove('CookiesAlert');
    });
}
function oSaveRevise(type, title, text, Id) {
    swal.fire({
        icon: type,
        title: title,
        text: text,
        timer: 5000
    }).then(function () {
        window.location.href = "PS_Revise.aspx?id=" + Id
    });

}
function oSaveReject(type, title, text, Id) {
    swal.fire({
        icon: type,
        title: title,
        text: text,
        timer: 5000
    }).then(function () {
        window.location.href = "PS_Reject.aspx?id=" + Id
    });
}
function oFinish(type, title, text, Id) {
    swal.fire({
        icon: type,
        title: title,
        text: text,
        timer: 5000
    }).then(function () {
        window.location.href = "PS_Finish.aspx?id=" + Id
    });
}
function oList(type, title, text) {
    swal.fire({
        icon: type,
        title: title,
        text: text,
        timer: 5000
    }).then(function () {
        window.location.href = "PS_MyTask.aspx"
    });
}
