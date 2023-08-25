﻿function checkFile() {
    var fileupload = document.getElementById("Body_fileUpload");
    var fileInput = fileupload.files;
    console.log(fileupload);
    // Create empty inputValues array
    // Loop through input fields   
    /*var src = window.URL.createObjectURL(fileupload.target.files)*/
    //var path = URL.createObjectURL(fileupload.files[0]);
    //var reader = new FileReader();
    //reader.readAsDataURL(fileupload.files[0]);
    for (let i = 0; i < fileInput.length; i++) {
        // Push values of each input field into an array
        //inputFields[i].files.name.length;
        if (fileInput[i].name.length > 0) {
            const fileSize = fileInput[i].size;
            const fileMb = fileSize / 1024 ** 2;
            var extension = fileInput[i].name.split('.').pop().toLowerCase();
            var validFileExtensions = ['jpeg', 'jpg', 'png', 'pdf', 'xlsx', 'xml'];
            //Get file for preview
            //reader.readAsDataURL(fileInput[i]);
            //reader.onload = function (e) {
            //    fileupload.src = e.target.result;
            //    window.open('http://localhost:56377/Vendor_Login.aspx');
            //    lbFileApp.innerHTML = "<a href='" + encodeURI(e.target.result) + "' target='_blank'>" + fileupload.files[i].name + "</a>";
            //}

            //Check file extension in the array.if -1 that means the file extension is not in the list. 
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
            /*filetext.innerHTML = filetext.innerHTML + "<br><b>" + fileinput.files[i].name; +"</b> </br>";*/
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