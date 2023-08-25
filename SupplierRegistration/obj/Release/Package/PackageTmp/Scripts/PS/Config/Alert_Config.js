function checkFile(fileupload) {
    /*var fileupload = document.getElementById("Body_fileUpload");*/
    var fileInput = fileupload.files;
    
    for (let i = 0; i < fileInput.length; i++) {
        // Push values of each input field into an array
        //inputFields[i].files.name.length;
        if (fileInput[i].name.length > 0) {
            const fileSize = fileInput[i].size;
            const fileMb = fileSize / 1024 ** 2;
            var extension = fileInput[i].name.split('.').pop().toLowerCase();
            var validFileExtensions = ['jpeg', 'jpg', 'png', 'pdf', 'xlsx', 'xml'];
            
            if ($.inArray(extension, validFileExtensions) == -1) {
                swal.fire({
                    icon: 'warning',
                    title: 'Wrong file type',
                    text: 'อัพโหลดได้เฉพาะไฟล์ pdf xlsx และรูปภาพ เท่านั้น , โปรดลองใหม่อีกครั้ง',
                    timer: 5000
                });
                $(fileupload).val(null)
            }
            if (fileInput[i].name.length > 30) {
                swal.fire({
                    icon: 'warning',
                    title: 'ชื่อไฟล์เกิน 30 ตัวอักษร, โปรดลองใหม่อีกครั้ง',
                    text: 'File name : ' + fileInput[i].name,
                    timer: 5000
                });
                $(fileupload).val(null)
            }
            if (fileMb > 3) {
                swal.fire({
                    icon: 'warning',
                    title: 'ขนาดไฟล์เกิน 3 MB , โปรดลองใหม่อีกครั้ง',
                    text: 'File name : ' + fileInput[i].name,
                    timer: 5000
                });
                $(fileupload).val(null)
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