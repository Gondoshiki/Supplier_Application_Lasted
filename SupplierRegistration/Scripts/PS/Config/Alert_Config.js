function checkFile(fileupload) {
    var fileEditApp = document.getElementById("Body_inpEditfile");
    var fileInput = fileEditApp.files;

    for (let i = 0; i < fileInput.length; i++) {

        if (fileInput[i].name.length > 0) {
            const fileSize = fileInput[i].size;
            const fileMb = fileSize / 1024 ** 2;
            var extension = fileInput[i].name.split('.').pop().toLowerCase();

            var validFileExtensions = ['jpeg', 'jpg', 'png', 'pdf', 'xlsx', 'xml'];
            //var validFileExtensions = ['jpeg', 'jpg', 'png', 'gif', 'bmp', 'pdf', 'xlsx', 'csv', 'xml', 'xps'];
            //Check file extension in the array.if -1 that means the file extension is not in the list. 
            if ($.inArray(extension, validFileExtensions) == -1) {
                swal.fire({
                    icon: 'warning',
                    title: 'Wrong file type',
                    text: 'อัพโหลดได้เฉพาะไฟล์ pdf xlsx และรูปภาพ เท่านั้น , โปรดลองใหม่อีกครั้ง',
                    timer: 5000
                }).then(function () {
                    // window.location.href = "Vendor_login.aspx?id=" + id
                });
                $(fileupload).val(null)
            }
            if (fileInput[i].name.length > 50) {
                swal.fire({
                    icon: 'warning',
                    title: 'ชื่อไฟล์เกิน 50 ตัวอักษร, โปรดลองใหม่อีกครั้ง',
                    text: 'File name : ' + fileInput[i].name,
                    timer: 5000
                }).then(function () {
                    // window.location.href = "Vendor_login.aspx?id=" + id
                });
                $(fileEditApp).val(null)
            }


            if (fileMb > 5) {
                swal.fire({
                    icon: 'warning',
                    title: 'ขนาดไฟล์เกิน 5 MB , โปรดลองใหม่อีกครั้ง',
                    text: 'File name : ' + fileInput[i].name,
                    timer: 5000
                }).then(function () {
                    //window.location.href = "Vendor_login.aspx?id=" + id
                });
                $(fileEditApp).val(null)
            }
        }

    }
    // Log array in the console
}

function oFileUploaded(type, title, text) {
    swal.fire({
        icon: type,
        title: title,
        text: text,
        timer: 5000
    }).then(function () {
        window.location.href = "Config.aspx"
    });
}

function oResetPassword(type, title, text) {
    swal.fire({
        icon: type,
        title: title,
        text: text,
        timer: 5000
    }).then(function () {
        window.location.href = "PS_ResetPassword.aspx"
    });
}