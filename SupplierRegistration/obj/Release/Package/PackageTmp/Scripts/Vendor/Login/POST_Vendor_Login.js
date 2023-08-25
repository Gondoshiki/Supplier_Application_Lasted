function fnGetFileRevise(value) {
    $.ajax({
        method: "POST",
        url: "Service.asmx/GetFileRevise",
        data: { ID: value },
        dataType: "json",
        success: function (result) {
            if (result.length > 0) {
                $('#tbFileRevise').DataTable({
                    order: [],
                    searching: false,
                    paging: false,
                    info: false,
                    responsive: true,
                    autoWidth: false,
                    data: result,
                    //"drawCallback": function () {
                    //    $("#tbFile thead").remove();
                    //},
                    //"bDestroy": true,                    
                    columns: [
                        { data: 'Title' },
                        { data: 'Source' },
                        { data: 'Source1' }

                    ],
                    //"columnDefs": [
                    //    { "targets": [0], "class": "justify-content-center" },                       

                    //],
                });
                $('#tbFileRevise').show();
                $('#btnfnEditPreview').show();

                //========== Hide tb file Preview =======================
                $('#tbFilePreview').fadeOut(2000);
                $('#PreviewFile_Title').fadeOut(2000);
                $('#btnfnEditFile').hide();
                $('#btnfnUploadLocal').hide();
                //========== Hide Upload Display=========================
                $('#UploadDisplay').hide();
                $('#btnfnUploadPreview').hide();

                //$('#btnfnUploadProcess').show();

            }
            else {
                $('#UploadDisplay').show();
                $('#btnfnUploadPreview').show();
            }
        }
    });
}

function fnGetFilePreview(value) {
    $.ajax({
        method: "POST",
        url: "Service.asmx/GetFilePreview",
        data: { ID: value },
        dataType: "json",
        /*contentType: "application/json; charset=utf-8",*/
        success: function (response) {
            if (response.length > 0) {
                $('#tbFilePreview').dataTable({
                    order: [],
                    searching: false,
                    paging: false,
                    info: false,
                    responsive: true,
                    autoWidth: false,
                    data: response,
                    //"drawCallback": function () {
                    //    $("#tbFile thead").remove();
                    //},
                    "bDestroy": true,
                    columns: [
                        { data: 'Title' },
                        { data: 'Source' },
                        { data: 'Source1', visible: false }

                    ],
                    "columnDefs": [
                        { autoWidth: false, "targets": [2] },

                    ],
                });
                $('#tbFilePreview').fadeIn(2000);
                $('#PreviewFile_Title').fadeIn(2000);
                $('#btnfnEditFile').show();
                $('#btnfnUploadLocal').show();
                //========== Hide tb File Revise =======================
                $('#tbFileRevise').hide();
                $('#btnfnEditPreview').hide();
                $('#btnfnCancelEdit').hide();
                //========== Hide Upload Display=========================
                $('#UploadDisplay').hide();
                $('#btnfnUploadPreview').hide();
                if ($('#hdfStatus').val() == "P") {
                    $('#btnfnResetProcess').show();
                }
                else if ($('#hdfStatus').val() == "R") {
                    $('#btnfnResetRevise').show();
                }
            }
            else {
                $('#UploadDisplay').show();
                $('#btnfnUploadPreview').show();
            }
        }
    });
}

function SearchClick(valid) {
    if (Page_ClientValidate(valid)) {
        var id = $('#App_ID').val();
        window.location.href = "Vendor_login.aspx?id=" + id
        $.ajax({
            method: "POST",
            url: "Service.asmx/GetAppDetail",
            data: { ID: id },
            dataType: "json",
            beforeSend: function (data) {

            },
            success: function (server) {
                if (server.length > 0) {
                    $('#AppDetail').show();
                    $('#AppDetail1').hide();
                    document.getElementById("detail").scrollIntoView();
                }
                else {
                    swal.fire({
                        icon: 'error',
                        title: 'Not found',
                        text: 'Sorry, Please enter application ID again',
                        timer: 5000
                    });
                }
            }
        })
    }
    else {
        $('#errorMessage').show();
    }
}

function UploadPreview(valid) {
    if (Page_ClientValidate(valid)) {

        //var tbl = $("#tbFilePreview").DataTable();
        //var pageData = tbl.rows({ page: 'current' }).data();
        //for (var i = 0; i < pageData.length; i) {
        //    idList.push(pageData[i][0]);
        //}
        var form_data = new FormData();
        var appID = $('#hdfApp_ID').val();
        var fileApp = $('#inpFileApp')[0].files[0];
        var fileSME = $('#inpFileSME')[0].files[0];
        var fileRegisCert = $('#inpFileRegisCert')[0].files[0];
        var filePP20 = $('#inpFilePP20')[0].files[0];
        var fileBookBank = $('#inpFileBookBank')[0].files[0];
        var fileBOJ5 = $('#inpFileBOJ5')[0].files[0];
        var fileOrgCompany = $('#inpFileOrgCompany')[0].files[0];
        var fileSPS10 = $('#inpFileSPS10')[0].files[0];
        //Object.entries(fileApp).forEach(([key, value]) => {
        //    formData.append(key, value);
        //});
        form_data.append("id", appID);
        form_data.append("fileApp", fileApp);
        form_data.append("fileSME", fileSME);
        form_data.append("fileRegisCert", fileRegisCert);
        form_data.append("filePP20", filePP20);
        form_data.append("fileBookBank", fileBookBank);
        form_data.append("fileBOJ5", fileBOJ5);
        form_data.append("fileOrgCompany", fileOrgCompany);
        form_data.append("fileSPS10", fileSPS10);
        showloading("1");
        $.ajax({
            method: "POST",
            url: "Service.asmx/SaveToPreview",
            processData: false,
            contentType: false,
            cache: false,
            data: form_data,
            dataType: "json",
            enctype: 'multipart/form-data',
            /*enctype: 'multipart/form-data',*/
            /*contentType: "application/json; charset=utf-8",*/
            success: function (result) {
                if (result == "success") {
                    let searchParams = new URLSearchParams(window.location.search);
                    var id = searchParams.get('id');
                    fnGetFilePreview(id);

                    $('#UploadDisplay').hide();
                    $('#btnfnUploadPreview').hide();
                }
                else if (result == "fail") {
                    oFileUploaded('error', 'Upload Fail', 'No file uploaded or not found ID', '');
                }
                showloading("0");
            }

        });

        //var fileApp = document.getElementById("inpFileApp");                    
        //var fileRegisCert = document.getElementById("inpFileRegisCert");
        //var filePP20 = document.getElementById("inpFilePP20");
        //var fileBookBank = document.getElementById("inpFileBookBank");
        //var fileBOJ5 = document.getElementById("inpFileBOJ5");
        //var fileOrgCompany = document.getElementById("inpFileOrgCompany");
        //var fileSPS10 = document.getElementById("inpFileSPS10");
        //var countFiles = fileApp.files.length + fileRegisCert.files.length + filePP20.files.length + fileBookBank.files.length + fileBOJ5.files.length + fileOrgCompany.files.length + fileSPS10.files.length
        //$('#hdfFile').val(countFiles);
        //var appID = $('#hdfApp_ID').val()
        //if (countFiles == 6 || countFiles == 7) {

        //    $("#btnSavePreview").click();
        //    showloading();
        //}
        //else {
        //    swal.fire({
        //        icon: 'error',
        //        title: 'Upload fail',
        //        text: 'ไฟล์ไม่ครบ.',
        //        timer: 5000
        //    })
        //}

    }
}

function editPreview(value) {
    var form_data = new FormData();
    //var tbl = $("#tbFilePreview").DataTable();
    //var pageData = tbl.rows({ page: 'current' }).data();
    //for (var i = 0; i < pageData.length; i) {
    //    idList.push(pageData[i][0]);
    //}

    var appID = $('#hdfApp_ID').val();
    var fileApp = null;
    var fileSME = null;
    var fileRegisCert = null;
    var filePP20 = null;
    var fileBookBank = null;
    var fileBOJ5 = null;
    var fileOrgCompany = null;
    var fileSPS10 = null;
    var count = 0;
    if ($('#inpAppForm_temp').val() != null) {
        var fileApp = $('#inpAppForm_temp')[0].files[0];
    }
    if ($('#inpSME_temp').val() != null) {
        var fileSME = $('#inpSME_temp')[0].files[0];
    }
    if ($('#inpRegisCert_temp').val() != null) {
        var fileRegisCert = $('#inpRegisCert_temp')[0].files[0];
    }
    if ($('#inpPP20_temp').val() != null) {
        var filePP20 = $('#inpPP20_temp')[0].files[0];
    }
    if ($('#inpBookBank_temp').val() != null) {
        var fileBookBank = $('#inpBookBank_temp')[0].files[0];
    }
    if ($('#inpBOJ5_temp').val() != null) {
        var fileBOJ5 = $('#inpBOJ5_temp')[0].files[0];
    }
    if ($('#inpOrgCompany_temp').val() != null) {
        var fileOrgCompany = $('#inpOrgCompany_temp')[0].files[0];
    }
    if ($('#inpSPS10_temp').val() != null) {
        var fileSPS10 = $('#inpSPS10_temp')[0].files[0];
    }

    //Object.entries(fileApp).forEach(([key, value]) => {
    //    formData.append(key, value);
    //});
    form_data.append("id", appID);
    form_data.append("fileApp_temp", fileApp);
    form_data.append("fileSME_temp", fileSME);
    form_data.append("fileRegisCert_temp", fileRegisCert);
    form_data.append("filePP20_temp", filePP20);
    form_data.append("fileBookBank_temp", fileBookBank);
    form_data.append("fileBOJ5_temp", fileBOJ5);
    form_data.append("fileOrgCompany_temp", fileOrgCompany);
    form_data.append("fileSPS10_temp", fileSPS10);

    swal.fire({
        icon: 'question',
        title: 'Do you want to upload this files?',
        Text: '',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes',
        cancelButtonText: 'No'

    }).then(function (result) {
        if (result.value) {
            showloading('1');
            $.ajax({
                method: "POST",
                url: "Service.asmx/EditPreview",
                processData: false,
                contentType: false,
                cache: false,
                data: form_data,
                dataType: "json",
                enctype: 'multipart/form-data',
                /*contentType: "application/json; charset=utf-8",*/
                success: function (result) {
                    showloading('0');
                    if (result == "success") {
                        let searchParams = new URLSearchParams(window.location.search);
                        var id = searchParams.get('id');
                        fnGetFilePreview(id);
                        //var table = $('#tbFilePreview').DataTable({
                        //    "ajax": {
                        //        "url": "Service.asmx/GetFilePreview",
                        //        "type": "POST",
                        //        "data": { ID: id },
                        //        "bDestroy": true,
                        //    }                                    
                        //});
                        //table.ajax.reload();
                    }
                    if (result == "fail") {
                        oFileUploaded('error', 'Upload fail', 'Please, upload file.', '');
                    }
                }

            })


            /*$('#btnSavePreview').click();*/
            //showloading();
        }

    });
}

function resetPreview(value) {
    swal.fire({
        icon: 'question',
        title: 'Do you want to reset file preview ?',
        Text: '',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes',
        cancelButtonText: 'No'

    }).then(function (result) {
        if (result.value) {
            var id = $('#hdfApp_ID').val()
            showloading('1');
            $.ajax({
                method: "POST",
                url: "Service.asmx/DeletePreview",
                data: { ID: id },
                dataType: "json",
                /*contentType: "application/json; charset=utf-8",*/
                success: function (result) {
                    showloading('0');
                    if (result == "success") {
                        oFileUploaded('success', 'Reset success', '', '');
                        if (value == "P") {
                            fnGetUploadDisplay();
                            $('input[type="file"]').val(null);
                            //$('#UploadDisplay').show();
                            //$('#btnfnReset').hide();
                            //$('#btnfnEditFile').hide();
                            //$('#btnfnUploadLocal').hide();
                            //$('#btnfnUploadPreview').show();
                            //$('input[type="file"]').val(null);
                            //$('#PreviewFile_Title').fadeToggle(2000);
                            //$('#tbFilePreview').fadeToggle(2000);
                        }
                        else if (value == "R") {
                            $('#tbFileRevise').show();
                            $('#btnfnEditPreview').show();

                            //========== Hide tb file Preview =======================
                            $('#tbFilePreview').fadeOut(2000)
                            $('#PreviewFile_Title').fadeOut(2000);
                            $('#btnfnEditFile').hide();
                            $('#btnfnUploadLocal').hide();
                            //========== Hide Upload Display=========================
                            $('#UploadDisplay').hide();
                            $('#btnfnUploadPreview').hide();

                        }

                    }
                    if (result == "fail") {
                        oFileUploaded('error', 'Upload fail', 'Can not found your ID', '');
                    }
                }

            })
        }
    })
}

function UploadLocal() {
    //var tbl = $("#tbFilePreview").DataTable();
    //var pageData = tbl.rows({ page: 'current' }).data();
    //for (var i = 0; i < pageData.length; i) {
    //    idList.push(pageData[i][0]);
    //}
    swal.fire({
        icon: 'question',
        title: 'Do you want to upload this files?',
        Text: '',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes',
        cancelButtonText: 'No'

    }).then(function (result) {
        if (result.value) {
            showloading('1');
            $('#btnUploadLocal').click();
        }
    })

}

function MoveToLocal() {
    var form_data = new FormData
}

function UploadRevise() {
    var form_data = new FormData()
    var appID = $('#hdfApp_ID').val();
    var fileApp = $('#inpAppForm_temp')[0].files[0];
    var fileSME = $('#inpSME_temp')[0].files[0];
    var fileRegisCert = $('#inpRegisCert_temp')[0].files[0];
    var filePP20 = $('#inpPP20_temp')[0].files[0];
    var fileBookBank = $('#inpBookBank_temp')[0].files[0];
    var fileBOJ5 = $('#inpBOJ5_temp')[0].files[0];
    var fileOrgCompany = $('#inpOrgCompany_temp')[0].files[0];
    var fileSPS10 = $('#inpSPS10_temp')[0].files[0];
    //Object.entries(fileApp).forEach(([key, value]) => {
    //    formData.append(key, value);
    //});
    var form_data = new FormData();
    form_data.append("id", appID);
    form_data.append("fileApp_temp", fileApp);
    form_data.append("fileSME_temp", fileSME);
    form_data.append("fileRegisCert_temp", fileRegisCert);
    form_data.append("filePP20_temp", filePP20);
    form_data.append("fileBookBank_temp", fileBookBank);
    form_data.append("fileBOJ5_temp", fileBOJ5);
    form_data.append("fileOrgCompany_temp", fileOrgCompany);
    form_data.append("fileSPS10_temp", fileSPS10);

    swal.fire({
        icon: 'question',
        title: 'Do you want to upload this files?',
        Text: '',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes',
        cancelButtonText: 'No'

    }).then(function (result) {
        if (result.value) {
            $.ajax({
                method: "POST",
                url: "Service.asmx/UploadRevise",
                processData: false,
                contentType: false,
                cache: false,
                data: form_data,
                dataType: "json",
                enctype: 'multipart/form-data',
                /*contentType: "application/json; charset=utf-8",*/
                success: function (result) {
                    if (result == "success") {
                        let searchParams = new URLSearchParams(window.location.search);
                        var id = searchParams.get('id');
                        fnGetFilePreview(id);
                        //var table = $('#tbFilePreview').DataTable({
                        //    "ajax": {
                        //        "url": "Service.asmx/GetFilePreview",
                        //        "type": "POST",
                        //        "data": { ID: id },
                        //        "bDestroy": true,
                        //    }                                    
                        //});
                        //table.ajax.reload();
                    }
                    if (result == "fail") {

                        oFileUploaded('error', 'Upload fail', 'Please, upload file.', '');
                    }

                }
            });
        }
    })

}

function RedirectClick() {
    document.getElementById("download").scrollIntoView();
}

function NavHeaderClick() {
    //var element = document.getElementById("NavHeader");
    //element.classList.add("active");
    document.getElementById("download").scrollIntoView();
}

function NavDownloadClick() {
    //var element = document.getElementById("NavDownload");
    //element.classList.add("active");
    document.getElementById("download").scrollIntoView();
}

function NavDetailClick() {
    //var element = document.getElementById("NavDetail");
    //element.classList.add("active");
    document.getElementById("detail").scrollIntoView();
}

function NavManualClick() {
    //var element = document.getElementById("NavManual");
    //element.classList.add("active");
    document.getElementById("manual").scrollIntoView();
}

function NavContactClick() {
    //var element = document.getElementById("NavContact");
    //element.classList.add("active");
    document.getElementById("contact").scrollIntoView();
}
