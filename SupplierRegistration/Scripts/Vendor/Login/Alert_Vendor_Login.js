function showloading(val) {
    if (val == "1") {
        $('body').loadingModal('destroy');
        $('body').loadingModal({
            animation: 'circle',
            text: 'Processing...'
        });
        /*$('body').loadingModal('destroy');*/
    }
    else if (val == "0") {
        $('body').loadingModal('hide');
    }

    //var loader = document.querySelector(".loader");

    //if (val == "1") {
    //    loader.classList.remove("loader-hidden");
    //}
    //if (val == "0") {
    //    loader.classList.add("loader-hidden");
    //}                
}

function checkFile(fileupload) {
    let searchParams = new URLSearchParams(window.location.search);
    var id = null;
    var id = searchParams.get('id');
    var fileInput = fileupload.files;
    // Create empty inputValues array
    // Loop through input fields
    var inpFileApp = document.getElementById("inpFileApp");
    var lbFileApp = document.getElementById("lbFileApp");
    /*var src = window.URL.createObjectURL(fileupload.target.files)*/
    var path = URL.createObjectURL(fileupload.files[0]);
    var reader = new FileReader();
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

//Script Manager Sweet Alert
function oFileUploaded(type, title, text, Id) {
    swal.fire({
        icon: type,
        title: title,
        text: text,
        timer: 5000
    }).then(function () {
        /* window.location.href = "Vendor_login.aspx?id=" + Id */
    });
}

function HandleChange() {
    var fileinput = document.getElementById("fileInput");
    var filetext = document.getElementById("fileText");
    var countfile = document.getElementById("hdfFile");
    var files = [];
    var fileinput1 = document.getElementById("fileInput1");
    var filetext1 = document.getElementById("fileText1");
    var countfile1 = document.getElementById("hdfFile1");
    let fileSubmit = document.getElementById("btnfnUploadProcess");
    var files1 = [];
    filetext1.innerHTML = "";
    filetext.innerHTML = "";
    let i = 0;
    if (fileinput.files.length >= 1) {
        countfile.value = fileinput.files.length
        for (i = 0; i < fileinput.files.length; i++) {
            if (fileinput.files[i].name.length > 100) {
                swal.fire({
                    icon: 'warning',
                    title: 'File name length over 100',
                    text: 'File name : ' + fileinput.files[i].name,
                    timer: 5000
                });
            }
            files.push(fileinput.files[i].name);
            if (fileinput.files[i].size > "5242880") {
                swal.fire({
                    icon: 'warning',
                    title: 'File size over 5 MB',
                    text: 'File name : ' + fileinput.files[i].name,
                    timer: 5000
                });
            }
            else {
                filetext.innerHTML = filetext.innerHTML + "<br><b>" + fileinput.files[i].name; +"</b> </br>";
            }

        }
        if (fileinput.files.length == 6 || fileinput.files.length == 7) {
            fileSubmit.disabled = true;
        }

    }
    if (fileinput1.files.length >= 1) {
        countfile1.value = fileinput1.files.length;
        for (i = 0; i < fileinput1.files.length; i++) {
            files.push(fileinput1.files[i].name);
            if (fileinput1.files[i].size > "5242880") {
                swal.fire({
                    icon: 'warning',
                    title: 'File size over 5 MB',
                    text: 'File name : ' + fileinput1.files[i].name,
                    timer: 5000
                })
            }
            else {
                filetext1.innerHTML = filetext1.innerHTML + "<br><b>" + fileinput1.files[i].name; +"</b> </br>";
            }

        }
    }
}
