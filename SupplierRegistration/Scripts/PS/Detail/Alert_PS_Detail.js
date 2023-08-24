function checkFile(fileupload) {
    var fileEditApp = null;
    switch (fileupload) {
        case 'File1':
            fileEditApp = document.getElementById("Body_File1");
            break;
        case 'File2':
            fileEditApp = document.getElementById("Body_File2");
            break;
        case 'File3':
            fileEditApp = document.getElementById("Body_File3");
            break;
        case 'File4':
            fileEditApp = document.getElementById("Body_File4");
            break;
        case 'File5':
            fileEditApp = document.getElementById("Body_File5");
            break;
        case 'File6':
            fileEditApp = document.getElementById("Body_File6");
            break;
        case 'File7':
            fileEditApp = document.getElementById("Body_File7");
            break;
        case 'File8':
            fileEditApp = document.getElementById("Body_File8");
            break;
    }
    /*const inputFields = document.querySelectorAll("#inputFormFile input[type='file']");*/
    //let btnUpload = document.getElementById("btnfnUploadProcess");
    //let btnRevise = document.getElementById("btnfnUploadRevise");
    var fileInput = fileEditApp.files;
    // Create empty inputValues array
    // Loop through input fields
    for (let i = 0; i < fileInput.length; i++) {
        // Push values of each input field into an array
        //inputFields[i].files.name.length;
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
                    $(fileupload).val(null)
                });

            }

            if (fileInput[i].name.length > 30) {
                swal.fire({
                    icon: 'warning',
                    title: 'ชื่อไฟล์เกิน 30 ตัวอักษร, โปรดลองใหม่อีกครั้ง',
                    text: 'File name : ' + fileInput[i].name,
                    timer: 5000
                }).then(function () {
                    // window.location.href = "Vendor_login.aspx?id=" + id
                    $(fileEditApp).val(null)
                });
            }
            if (fileMb > 3) {
                swal.fire({
                    icon: 'warning',
                    title: 'ขนาดไฟล์เกิน 3 MB , โปรดลองใหม่อีกครั้ง',
                    text: 'File name : ' + fileInput[i].name,
                    timer: 5000
                }).then(function () {
                    //window.location.href = "Vendor_login.aspx?id=" + id
                    $(fileEditApp).val(null)
                });

            }
        }

    }
    // Log array in the console
}
function oFileUploaded(type, title, text, id) {
    swal.fire({
        icon: type,
        title: title,
        text: text,
        timer: 5000
    }).then(function () {
        window.location.href = "PS_Detail.aspx?id=" + id
    });
}
function SearchClick(valid) {



}
