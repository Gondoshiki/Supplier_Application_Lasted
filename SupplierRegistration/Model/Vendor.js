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
                                        for (var i = 0; i < server.length; i++) {
                                            //var url = require('node:url');
                                            //var ofiles = url.pathToFileURL(server[i].FileName)                                           
                                            if (i == 0) {
                                                var lbFileApp = document.getElementById("lbFileApp");
                                                lbFileApp.innerHTML = "<span>" + server[i].Source + "</span>";
                                            }
                                            if (i == 1) {
                                                var lbfileRegisCert = document.getElementById("lbFileRegisCert");
                                                lbfileRegisCert.innerHTML = "<span>" + server[i].Source + "</span>";
                                            }
                                            if (i == 2) {
                                                var lbFilePP20 = document.getElementById("lbFilePP20");
                                                lbFilePP20.innerHTML = "<span>" + server[i].Source + "</span>";
                                            }

                                            if (i == 3) {
                                                var lbFileBookBank = document.getElementById("lbFileBookBank");
                                                lbFileBookBank.innerHTML = "<span>" + server[i].Source + "</span>";
                                            }
                                            if (i == 4) {
                                                var lbFileBOJ5 = document.getElementById("lbFileBOJ5");
                                                lbFileBOJ5.innerHTML = "<span>" + server[i].Source + "</span>";
                                            }
                                            if (i == 5) {
                                                var lbFileOrgCompany = document.getElementById("lbFileOrgCompany");
                                                lbFileOrgCompany.innerHTML = "<span>" + server[i].Source + "</span>";
                                            }
                                            if (i == 6) {
                                                var lbFileSPS10 = document.getElementById("lbFileSPS10");
                                                lbFileSPS10.innerHTML = "<span>" + server[i].Source + "</span>";
                                            }
                                        }
                                    }
                                    else {
                                        if (value.Status == "F") {
                                            $('#tbFileF').show();
                                            $('#tbFileF').DataTable({
                                                searching: false,
                                                paging: false,
                                                info: false,
                                                responsive: true,
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
                                                searching: false,
                                                paging: false,
                                                info: false,
                                                responsive: true,
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

                            }
                        });
                    }
                    //Check Status for show card
                    if (value.Status == "D") {
                        $('#cardDraft').show();
                    }
                    if (value.Status == "P") {
                        if (value.FileUpload == "1") {
                            $('#cardUploaded').show();
                        }
                        else {
                            $('#cardProcess').show();
                        }
                        $('#btnfnUploadProcess').show();
                    }
                    else if (value.Status == "R") {
                        $('#btnfnUploadRevise').show();
                        $('#cardRevise').show();
                    }
                    else if (value.Status == "J") {
                        $('#cardReject').show();
                    }
                    else if (value.Status == "F") {
                        if (value.FileUpload == "1") {
                            $('#cardUploaded').hide();
                            $('#cardUploadedF').show();
                        }
                        else {

                            $('#cardFinish').show();
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