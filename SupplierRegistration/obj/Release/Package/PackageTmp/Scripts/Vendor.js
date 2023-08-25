$(document).ready(function () {

    //Get App Detail
    $('#inpFileApp').on("change", function (event) {
    })

    let searchParams = new URLSearchParams(window.location.search);
    var id = null;
    var id = searchParams.get('id');
    //Get file application form
    $.ajax({
        method: "POST",
        url: "Service.asmx/GetApplicationForm",
        data: {},
        dataType: "json",
        beforeSend: function (data) {
        },
        success: function (server) {
            if (server.length > 0) {
                var lbBtnDownload = document.getElementById("lb_BtnDownload");
                lbBtnDownload.innerHTML = server;
            }
        }
    });

    //function fnGetFileRevise(value) {
    //    $.ajax({
    //        method: "POST",
    //        url: "Service.asmx/GetFileRevise",
    //        data: { ID: value },
    //        dataType: "json",
    //        success: function (result) {
    //            if (result.length > 0) {
    //                $('#tbFileRevise').show();
    //                $('#tbFileRevise').DataTable({
    //                    searching: false,
    //                    paging: false,
    //                    info: false,
    //                    responsive: true,
    //                    autoWidth: false,
    //                    data: result,
    //                    //"drawCallback": function () {
    //                    //    $("#tbFile thead").remove();
    //                    //},
    //                    //"bDestroy": true,                    
    //                    columns: [
    //                        { data: 'Title' },
    //                        { data: 'Source' },
    //                        { data: 'Source1' }

    //                    ],
    //                    //"columnDefs": [
    //                    //    { "targets": [0], "class": "justify-content-center" },                       

    //                    //],
    //                });

    //                $('#btnfnEditPreview').show();
    //                //$('#btnfnUploadProcess').show();

    //            }
    //            else {
    //                $('#UploadDisplay').show();
    //                $('#btnfnUploadPreview').show();
    //            }
    //        }
    //    });
    //}
    //function fnGetFilePreview(value) {
    //    $.ajax({
    //        method: "POST",
    //        url: "Service.asmx/GetFilePreview",
    //        data: { ID: value },
    //        dataType: "json",
    //        /*contentType: "application/json; charset=utf-8",*/
    //        success: function (response) {
    //            if (response.length > 0) {
    //                $('#tbFilePreview').dataTable().fnClearTable();
    //                $('#tbFilePreview').dataTable({
    //                    searching: false,
    //                    paging: false,
    //                    info: false,
    //                    responsive: true,
    //                    autoWidth: false,
    //                    data: server,
    //                    //"drawCallback": function () {
    //                    //    $("#tbFile thead").remove();
    //                    //},
    //                    "bDestroy": true,
    //                    columns: [
    //                        { data: 'Title' },
    //                        { data: 'Source' },
    //                        { data: 'Source1', visible: false }

    //                    ],
    //                });
    //                $('#tbFilePreview').show();
    //                $('#tbFileRevise').hide();
    //                $('#btnfnEditPreview').hide();
    //                $('#btnfnCancelEdit').hide();
    //                $('#btnfnEditFile').show();
    //                $('#btnfnUploadLocal').show();
    //            }
    //            else {
    //                $('#UploadDisplay').show();
    //                $('#btnfnUploadPreview').show();
    //            }

    //        }
    //    });
    //    //$.ajax({
    //    //    method: "POST",
    //    //    url: "Service.asmx/GetFilePreview",
    //    //    data: { ID: value },
    //    //    dataType: "json",
    //    //    success: function (result) {
    //    //        if (result.length > 0) {
    //    //            $('#tbFilePreview').show();
    //    //            $('#tbFilePreview').DataTable({
    //    //                searching: false,
    //    //                paging: false,
    //    //                info: false,
    //    //                responsive: true,
    //    //                autoWidth: false,
    //    //                data: result,
    //    //                //"drawCallback": function () {
    //    //                //    $("#tbFile thead").remove();
    //    //                //},
    //    //                "bDestroy": true,
    //    //                columns: [
    //    //                    { data: 'Title' },
    //    //                    { data: 'Source' },
    //    //                    { data: 'Source1', visible: false }

    //    //                ],
    //    //                //"columnDefs": [
    //    //                //    { "targets": [0], "class": "justify-content-center" },                       

    //    //                //],
    //    //            });

    //    //            $('#btnfnEditFile').show();
    //    //            //$('#btnfnUploadProcess').show();
    //    //            //$('#btnfnUploadLocal').show();

    //    //            //var reader = new FileReader();
    //    //            //var fileApp = document.getElementById("inpFileApp");
    //    //            //const file = document.querySelector("input[type=file]").files[0];
    //    //            //result.forEach((data) => {
    //    //            //    if (data.FileName.length == 12) {
    //    //            //        fileApp.Value = "http://localhost:56377/Document/fileAttach/temp/A202308-0043/Application_Form/Inv%20&%20PL.pdf"
    //    //            //    }

    //    //            //    reader.readAsDataURL("http://localhost:56377/Document/fileAttach/temp/A202308-0043/Application_Form/Inv%20&%20PL.pdf");
    //    //            //    reader.onload = function (e) {
    //    //            //        fileupload.src = e.target.result;
    //    //            //        /*lbFileApp.innerHTML = "<a href='" + encodeURI(e.target.result) + "' target='_blank'>" + fileupload.files[i].name + "</a>";*/
    //    //            //    }
    //    //            //})
    //    //        }
    //    //        else {
    //    //            $('#UploadDisplay').show();
    //    //            $('#btnfnUploadPreview').show();
    //    //        }
    //    //    }
    //    //});
    //}
    //Get Application Detail for show
    $.ajax({
        method: "POST",
        url: "Service.asmx/GetAppDetail",
        data: { ID: id },
        dataType: "json",
        beforeSend: function (data) {

        },
        success: function (server) {
            if (server.length > 0) {
                $.each(server, function (index, value) {

                    $('#lbApp_ID').text(value.App_ID);
                    $('#hdfApp_ID').val(value.App_ID);
                    $('#lbVendorName').text(value.Vendor_Name);
                    $('#lbVendorPIC').text(value.Vendor_PIC);
                    $('#lbEmail').text(value.Email);
                    $('#hdfMail').val(value.Email);
                    if (value.Status == 'F' || value.Status == 'J') {
                        var date = new Date(value.TimeStamp);
                        var newdate = moment(date).format("YYYY-MM-DD HH:mm");
                    }
                    else {
                        var date = new Date(value.Update_Date);
                        var newdate = moment(date).format("YYYY-MM-DD HH:mm");
                    }
                    $('#lbUpdate_Date').text(newdate);
                    $('#lbComment').text(value.Comment);
                    $('#lbReject').text(value.Comment);
                    $('#lbReject1').text(value.Comment);
                    $('#hdfStatus').val(value.Status);
                    $('#hdfFileUploaded').val(value.FileUpload);
                    $('#hdfCreateID').val(value.CreateByID);
                    stepIndicator(value.Status);
                    /*$("#Body_Department").val(value.DisplayName)*/
                    //$("#Body_Department").attr(value.DisplayName)

                    //Get files list                   
                    var checkFileUploaded = $('#hdfFileUploaded').val();
                    if (checkFileUploaded == "1") {
                        $.ajax({
                            method: "POST",
                            url: "Service.asmx/GetFileList",
                            data: { ID: id },
                            dataType: "json",
                            beforeSend: function (data) {

                            },
                            success: function (server) {
                                var dt = JSON.parse(JSON.stringify(server))
                                if (server.length > 0) {
                                    if (value.Status == "R") {
                                        fnGetFileRevise(id);                                        
                                    }
                                    if (value.Status == "F") {

                                        $('#tbFileF').show();
                                        $('#tbFileF').DataTable({
                                            order: [],
                                            searching: false,
                                            paging: false,
                                            info: false,
                                            responsive: true,
                                            autoWidth: false,
                                            data: dt,
                                            //"drawCallback": function () {
                                            //    $("#tbFile thead").remove();
                                            //},
                                            //"bDestroy": true,                    
                                            columns: [
                                                { data: 'Title' },
                                                { data: 'Source' }
                                            ],
                                            //"columnDefs": [
                                            //    { "targets": [0], "class": "justify-content-center" },                       

                                            //],
                                        });
                                    }
                                    else {
                                        $('#tbFile').show();
                                        $('#tbFile').DataTable({
                                            order: [],
                                            searching: false,
                                            paging: false,
                                            info: false,
                                            responsive: true,
                                            autoWidth: false,
                                            data: dt,
                                            //"drawCallback": function () {
                                            //    $("#tbFile thead").remove();
                                            //},
                                            //"bDestroy": true,                    
                                            columns: [
                                                { data: 'Title' },
                                                { data: 'Source' }
                                            ],
                                            //"columnDefs": [
                                            //    { "targets": [0], "class": "justify-content-center" },                       

                                            //],
                                        });
                                    }
                                }

                            }

                        });
                    }
                    else {
                        if (value.Status == 'P') {
                            fnGetFilePreview(id);
                        }
                    }
                    //Check Status for show card
                    if (value.Status == "D") {
                        $('#cardDraft').show();
                    }
                    if (value.Status == "F") {
                        if (checkFileUploaded == "1") {
                            $('#cardUploaded').hide();
                            $('#cardUploadedF').show();
                        }
                        else {

                            $('#cardFinish').show();
                        }
                    }
                    if (value.Status == "P") {
                        if (checkFileUploaded == "1") {
                            $('#cardUploaded').show();
                        }
                        else {
                            $('#cardProcess').show();
                        }

                        /*$('#btnfnUploadProcess').show();*/
                    }
                    else if (value.Status == "R") {
                        /*$('#btnfnUploadRevise').show();*/
                        $('#cardRevise').show();
                    }
                    else if (value.Status == "J") {
                        if (checkFileUploaded == "1") {
                            $('#cardRejectNo').hide();
                            $('#cardReject').show();
                        }
                        else {

                            $('#cardRejectNo').show();
                        }
                    }


                });
                $('#detail').show();
                $('#notfoundPage').hide();
                document.getElementById("detail").scrollIntoView();
            }
        },

    });
    //Step Indicator
    function stepIndicator(value) {
        $('.stepIndicator').each(function () {
            $('.stepIndicator').attr('class', 'stepIndicator');
        });

        if (value == 'N') {
            $(this).attr('class', 'stepIndicator active');
            return false;
        }

        else if (value == 'D') {
            $('#N').attr('class', 'stepIndicator finish');
            $('#D').attr('class', 'stepIndicator active');
            $('#P').attr('class', 'stepIndicator');
            $('#R').attr('class', 'stepIndicator');
            $('#J').attr('class', 'stepIndicator');
            $('#F').attr('class', 'stepIndicator');

        }
        else if (value == 'P') {
            $('#N').attr('class', 'stepIndicator finish');
            $('#D').attr('class', 'stepIndicator finish');
            $('#P').attr('class', 'stepIndicator active');
            $('#R').attr('class', 'stepIndicator');
            $('#J').attr('class', 'stepIndicator');
            $('#F').attr('class', 'stepIndicator');
        }
        else if (value == 'R') {
            $('#N').attr('class', 'stepIndicator finish');
            $('#D').attr('class', 'stepIndicator finish');
            $('#P').attr('class', 'stepIndicator finish');
            $('#R').attr('class', 'stepIndicator active');
            $('#J').attr('class', 'stepIndicator');
            $('#F').attr('class', 'stepIndicator');
            $('#cardMessage').show();
        }
        else if (value == 'J') {
            $('#N').attr('class', 'stepIndicator finish');
            $('#D').attr('class', 'stepIndicator finish');
            $('#P').attr('class', 'stepIndicator finish');
            $('#R').attr('class', 'stepIndicator finish');
            $('#J').attr('class', 'stepIndicator active');
            $('#F').attr('class', 'stepIndicator');
        }
        else if (value == 'F') {
            $('#N').attr('class', 'stepIndicator finish');
            $('#D').attr('class', 'stepIndicator finish');
            $('#P').attr('class', 'stepIndicator finish');
            $('#R').attr('class', 'stepIndicator finish');
            $('#J').attr('class', 'stepIndicator finish');
            $('#F').attr('class', 'stepIndicator finish');
        }
    }


    //Navbar Active Click
    var header = document.getElementById("navbarResponsive");
    var Navs = header.getElementsByClassName("nav-link");

    for (var i = 0; i < Navs.length; i++) {
        Navs[i].addEventListener("click", function () {
            $(".navbar-nav li a").removeClass('active');
            $(this).addClass("active");

        });
    }
    //

    //$(window).on('scroll', function () {
    //    addClassOnScroll();
    //});

    //var addClassOnScroll = function () {
    //    $('section[id]').each(function (index, elem) {
    //        if ($(elem).is(':appeared')) {
    //            const elemId = $(elem).attr('id');
    //            $(".navbar-nav ul li a.active").removeClass('active');
    //            $(this).addClass('active');
    //        }
    //    });
    //};

});