//SweetAlertDark Function
function Auth(valid) {
    if (Page_ClientValidate(valid)) {
        swal.fire({
            icon: 'question',
            title: 'Do you want to add Authorize?',
            text: '',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes',
            cancelButtonText: 'No'
        }).then(function (result) {
            if (result.value) {
                showloading('1');
                $("#Body_btn_Auth").click();
            }
        });
    }
}
//SweetAlertDark Function
function UpdateDate(valid) {
    if (Page_ClientValidate(valid)) {
        swal.fire({
            icon: 'question',
            title: 'Do you want to add Authorize?',
            text: '',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes',
            cancelButtonText: 'No'
        }).then(function (result) {
            if (result.value) {
                $("#Body_btn_Update").click();
                showloading();
            }
        });
    }
}
//Upload file
function PS_UploadClick(valid) {
    if (Page_ClientValidate(valid)) {
        var fileApp = document.getElementById("Body_fileUpload");
        var files = [];
        var countFiles = fileApp.files.length
        $('#Body_hdfFile').val(countFiles);
        swal.fire({
            icon: 'question',
            title: 'Do you want to upload this files ?',
            text: '',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes',
            cancelButtonText: 'No'
        }).then(function (result) {
            if (result.value) {
                showloading('1');
                $("#Body_btnAddSystem").click();

            }
        });
    }
}
//Edit file modal
function editFile_Modal(fileName, folderName) {
    if (fileName.length > 0 && folderName.length > 0) {
        $('#Body_hdfFileName').val(decodeURI(fileName));
        $('#Body_hdfFolderName').val(folderName);
        $('#Body_inpEditfolder').val(folderName);
        $('#editFile_Modal').modal('show')
    }
}
//Edit file click
function editFile(valid) {
    if (Page_ClientValidate(valid)) {
        var fileEditApp = document.getElementById("Body_inpEditfile");
        var files = [];
        var countFiles = fileEditApp.files.length
        $('#Body_hdfFile').val(countFiles);
        swal.fire({
            icon: 'question',
            title: 'Do you want to edit this file ?',
            text: '',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes',
            cancelButtonText: 'No'
        }).then(function (result) {
            if (result.value) {
                showloading('1');
                $("#Body_btnEditfile").click();

            }
        });
    }
}
//Delete file click
function deleteFile(fileName, folderName) {
    //Uri.EscapeUriString
    if (fileName.length > 0 && folderName.length > 0) {
        $('#Body_hdfFileName').val(decodeURI(fileName));
        $('#Body_hdfFolderName').val(folderName);
        swal.fire({
            icon: 'warning',
            title: 'Do you want to delete this file ?',
            text: '',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes',
            cancelButtonText: 'No'
        }).then(function (result) {
            if (result.value) {
                showloading('1');
                $('#Body_btnDeleteFile').click();
            }
        });
    }
}
//Edit user modal
function editUser_Modal(empID, email, phone) {
    $('#Body_hdfEmpID').val(empID);
    $('#Body_empID').val(empID);
    $('#Body_empEmail').val(email);
    $('#Body_empPhone').val(phone);
    $('#editAuthorize_Modal').modal('show');
}

function Reset_Password(empID, TokenID) {
    $('#Body_hdfToken').val(TokenID);
    $('#Body_hdfEmpID').val(empID);
    if (empID != null) {
        swal.fire({
            icon: 'question',
            title: 'Do you want to Reset Password?',
            text: '',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes',
            cancelButtonText: 'No'
        }).then(function (result) {
            if (result.value) {
                showloading('1');
                $('#Body_hdfResetEmail').click();


            }
        });
    }

}
//Edit user modal
function editUser(valid) {
    if (Page_ClientValidate(valid)) {
        swal.fire({
            icon: 'question',
            title: 'Do you want to edit this employee ?',
            text: '',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes',
            cancelButtonText: 'No'
        }).then(function (result) {
            if (result.value) {
                showloading('1');
                $('#Body_btnEditAuth').click();
            }
        });

    }
}

function deleteUser(empID) {
    if (empID != "") {
        $('#Body_hdfEmpID').val(empID);
        swal.fire({
            icon: 'warning',
            title: 'Do you want to delete this employee ?',
            text: '',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes',
            cancelButtonText: 'No'
        }).then(function (result) {
            if (result.value) {
                showloading('1');
                $('#Body_btnDeleteAuth').click();
            }
        });

    }
}