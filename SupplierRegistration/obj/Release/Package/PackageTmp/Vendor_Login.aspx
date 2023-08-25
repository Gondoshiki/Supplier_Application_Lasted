<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Vendor_Login.aspx.cs" Inherits="SupplierRegistration.Vendor_Login" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Supplier Registration Landing Page</title>
    <!-- Favicon-->
    <link rel="icon" type="image/x-icon" href="StartBootstrap/startbootstrap-creative-gh-pages/assets/favicon.ico" />
    <!-- Bootstrap Icons-->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.5.0/font/bootstrap-icons.css" rel="stylesheet" />

    <!-- Google fonts-->
    <link href="https://fonts.googleapis.com/css?family=Merriweather+Sans:400,700" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css?family=Merriweather:400,300,300italic,400italic,700,700italic" rel="stylesheet" type="text/css" />
    <!-- SimpleLightbox plugin CSS-->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/SimpleLightbox/2.1.0/simpleLightbox.min.css" rel="stylesheet" />
    <!-- Core theme CSS (includes Bootstrap)-->
    <link href="StartBootstrap/startbootstrap-creative-gh-pages/css/styles.css" rel="stylesheet" />
    <!-- Step Navigation-->
    <link href="StepNavigation/Step_Navigation.css" rel="stylesheet" />
    <%--Sweet Alert--%>
    <link href="SweetAlert/SweetAlert.css" rel="stylesheet" />
    <script src="SweetAlert/SweetAlert.js"></script>
    <%--Jquery--%>
    <script src="Scripts/Jquery3.1.1.js"></script>
    <script src="Scripts/select.js"></script>
    <!-- Moment -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.18.1/moment.min.js"></script>
    <%--Font Awesome--%>
    <link href="FontAwesome/css/all.min.css" rel="stylesheet" />
    <%--Modal Loading--%>
    <script src="Scripts/JqueryLoading.js"></script>
    <link href="Content/jquery.loadingModal.css" rel="stylesheet" />

    <!-- Showloading -->
    <link href="Content/loader.css" rel="stylesheet" />

    <%--<link rel="stylesheet" href="modal-loading/css/jquery.loadingModal.css">--%>


    <script src="Scripts/ShowLoading.js"></script>

</head>
<body id="page-top">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <!----- Loader ------------>
        <div class="loader"></div>
        
        <!-- File upload Modal -->
        <div class="modal fade modal-lg" id="uploadFileModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">Files Upload</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">

                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body" id="inputFormFile">
                        <div id="UploadDisplay" style="display: none">
                            <div class="mb-3">
                                <label class="form-label" for="inpAppFile">Application form (.xlsx) :</label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="inpFileApp" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="ProcessUpload" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                <asp:Label ID="lbFileApp" runat="server" Text=""></asp:Label>
                                <div class="input-group">
                                    <input class="form-control" type="file" id="inpFileApp" runat="server" onchange="checkFile(inpFileApp)" />
                                </div>

                            </div>
                            <div class="mb-3">
                                <label class="form-label" for="fileInput">SMEs Research :</label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="inpFileSME" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="ProcessUpload" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                <asp:Label ID="lbFileSME" runat="server" Text=""></asp:Label>
                                <div class="input-group">
                                    <input class="form-control" type="file" id="inpFileSME" runat="server" onchange="checkFile(inpFileSME)" />
                                </div>

                            </div>
                            <div class="mb-3">
                                <label class="form-label" for="fileInput">หนังสือรับรอง :</label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="inpFileRegisCert" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="ProcessUpload" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                <asp:Label ID="lbFileRegisCert" runat="server" Text=""></asp:Label>
                                <div class="input-group">
                                    <input class="form-control" type="file" id="inpFileRegisCert" runat="server" onchange="checkFile(inpFileRegisCert)" />
                                </div>
                            </div>
                            <div class="mb-3">
                                <label class="form-label" for="fileInput">ภพ.20 :</label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="inpFilePP20" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="ProcessUpload" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                <asp:Label ID="lbFilePP20" runat="server" Text=""></asp:Label>
                                <div class="input-group">
                                    <input class="form-control" type="file" id="inpFilePP20" runat="server" onchange="checkFile(inpFilePP20)" />
                                </div>

                            </div>
                            <div class="mb-3">
                                <label class="form-label" for="fileInput">Book Bank :</label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="inpFileBookBank" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="ProcessUpload" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                <asp:Label ID="lbFileBookBank" runat="server" Text=""></asp:Label>
                                <div class="input-group">
                                    <input class="form-control" type="file" id="inpFileBookBank" runat="server" onchange="checkFile(inpFileBookBank)" />
                                </div>

                            </div>
                            <div class="mb-3">
                                <label class="form-label" for="fileInput">บอจ.5 (สำเนารายชื่อผู้ถือหุ้น) :</label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="inpFileBOJ5" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="ProcessUpload" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                <asp:Label ID="lbFileBOJ5" runat="server" Text=""></asp:Label>
                                <div class="input-group">
                                    <input class="form-control" type="file" id="inpFileBOJ5" runat="server" onchange="checkFile(inpFileBOJ5)" />
                                </div>

                            </div>
                            <div class="mb-3">
                                <label class="form-label" for="fileInput">Organization company :</label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="inpFileOrgCompany" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="ProcessUpload" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                <asp:Label ID="lbFileOrgCompany" runat="server" Text=""></asp:Label>
                                <div class="input-group">
                                    <input class="form-control" type="file" id="inpFileOrgCompany" runat="server" onchange="checkFile(inpFileOrgCompany)" />
                                </div>

                            </div>
                            <div class="mb-3">
                                <label class="form-label" for="fileInput5">สปส1-10 หรือ งบการเงิน (<span style="color: #ff0000">Only SMEs</span>) :</label>
                                <asp:Label ID="lbFileSPS10" runat="server" Text=""></asp:Label>
                                <div class="input-group">
                                    <input class="form-control" type="file" id="inpFileSPS10" runat="server" onchange="checkFile(inpFileSPS10)" />
                                </div>
                            </div>
                        </div>

                        <!-- Hidden Field -->
                        <asp:HiddenField ID="hdfFile" runat="server" />
                        <asp:HiddenField ID="hdfFileUploaded" runat="server" />
                        <asp:HiddenField ID="hdfStatus" runat="server" />
                        <div class="alert alert-info" id="PreviewFile_Title" style="display: none">
                            <h3>Preview File</h3>
                        </div>
                        <table id="tbFilePreview" class="table table-hover bg-light" style="width: 100%; display: none">
                            <thead>
                                <tr>
                                    <th>File list</th>
                                    <th>Your draft file</th>
                                    <th></th>
                                </tr>
                            </thead>
                        </table>
                        <table id="tbFileRevise" class="table table-hover bg-light" style="width: 100%; display: none">
                            <thead>
                                <tr>
                                    <th>File list</th>
                                    <th></th>
                                    <th></th>
                                </tr>
                            </thead>
                        </table>

                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        <!-------------------For Preview file -------------------------------------->
                        <button class="btn btn-danger" id="btnfnResetProcess" type="button" onclick="resetPreview('P')" style="display:none">Reset</button>
                        <button class="btn btn-danger" id="btnfnResetRevise" type="button" onclick="resetPreview('R')" style="display:none">Reset</button>
                        <button class="btn btn-dark" id="btnfnEditFile" onclick="dataTableController('1')" type="button" style="display: none">Edit File</button>
                        <button class="btn btn-danger" id="btnfnCancelEdit" onclick="dataTableController('1')" type="button" style="display: none">Cancel Edit</button>


                        <!-------------------For Preview Revise file -------------------------------------->
                        <button class="btn btn-dark" id="btnfnEditRevise" onclick="dataTableController('2')" type="button" style="display: none">Edit File</button>
                        <button class="btn btn-danger" id="btnfnCancelEditRevise" onclick="dataTableController('2')" type="button" style="display: none">Cancel Edit</button>

                        <button class="btn btn-dark" id="btnfnEditPreview" onclick="editPreview()" type="button" style="display: none">Preview</button>
                        <button class="btn btn-dark btn-icon-split" type="button" id="btnfnUploadPreview" validationgroup="ProcessUpload" onclick="UploadPreview('ProcessUpload')" style="display: none">
                            <%--<span class="icon text-white-50" style="margin-right: auto">
                            <i class="fas fa-check"></i>
                          </span>--%>
                            <span class="text" style="margin-right: auto">Preview</span>
                        </button>                       

                        <button class="btn btn-success" id="btnfnUploadLocal" type="button" runat="server" onclick="UploadLocal()" style="display: none">Upload</button>
                        <button class="btn btn-success" id="btnfnUploadRevise" type="button" runat="server" onclick="editPreview()" style="display: none">Upload Revise</button>

                        <button class="btn btn-dark" id="btnUploadLocal" type="submit" runat="server" onserverclick="UploadLocal_Click" style="display: none">Move to Local</button>
                       
                    </div>
                </div>
            </div>
        </div>
        <!-- Navigation-->
        <nav class="navbar navbar-expand-lg navbar-light fixed-top py-3" id="mainNav">
            <div class="container px-4">
                <a class="navbar-brand" href="javascript:void(0)" id="NavHeader" onclick="NavHeaderClick()">Supplier Application</a>
                <button class="navbar-toggler navbar-toggler-right" type="button" data-toggle="collapse" data-target="#navbarResponsive" aria-controls="navbarResponsive" aria-expanded="false" aria-label="Toggle navigation"><span class="navbar-toggler-icon"></span></button>
                <div class="collapse navbar-collapse" id="navbarResponsive">
                    <ul class="navbar-nav ms-auto my-2 my-lg-0">
                        <li class="nav-item my-auto"><a class="nav-link" href="#download" id="NavDownload" onclick="NavDownloadClick()">Download</a></li>
                        <li class="nav-item my-auto"><a class="nav-link" href="#detail" id="NavDetail" onclick="NavDetailClick()">Detail</a></li>
                        <li class="nav-item my-auto"><a class="nav-link" href="#contact" id="NavContact" onclick="NavContactClick()">Contact</a></li>
                        <li class="nav-item my-auto">
                            <a class="nav-link" href="PS_Login.aspx" id="Login">
                                <span class="btn btn-danger" <%--style="border-radius:10rem;"--%>>
                                    <img src="StartBootstrap/startbootstrap-creative-gh-pages/assets/img/hino-logo1.png" style="width: 35px; height: auto;" />
                                    <span class="text-white" <%--style="color:rgba(255, 255, 255, 0.7)"--%>>Hino Login</span>
                                </span>

                            </a>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>
        <!-- Masthead-->
        <header id="download" class="masthead h-100" <%--class="container-fluid p-0 mt-3"--%> <%--style="height:100vh auto;"--%>>
            <div class="row" style="/*background: url('../StartBootstrap/startbootstrap-creative-gh-pages/assets/img/M0003.jpg'); */  background-position: left; background-repeat: no-repeat; background-attachment: scroll; background-size: cover;">
                <div class="col-lg-6 col-md-6" style="background-position: left; background-repeat: no-repeat; background-attachment: scroll; background-size: cover;"></div>
                <%--<div style="width:100%; background:url('../assets/img/M0003.jpg');"></div>--%>
                <div class="col-lg-6 col-md-6 px-4 px-lg-5" style="/*background: linear-gradient(to bottom, rgba(92, 77, 66, 0.8) 0%, rgba(92, 77, 66, 0.8) 100%); */  background: rgba(0, 0, 0, 0.68);">
                    <%--<div style="margin-top:6rem; margin-bottom: calc(10rem - 4.5rem) auto;">--%>
                    <div class="row align-items-center justify-content-center text-center">
                        <div class="col-lg-10 align-self-end" style="margin-top: 7rem">
                            <p>
                                <h1 class="text-white font-weight-bold">Download Application Form</h1>
                            </p>
                            <hr class="divider" />
                        </div>
                        <div class="col-lg-10 align-self-end" <%--style="margin-bottom:3rem;"--%>>
                            <p class="text-white-75 mb-5">Please download files below to fill in forms and upload files to website from link we sent to your E-mail </p>
                            <asp:Label ID="lb_BtnDownload" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <%--</div>--%>
                </div>
            </div>

        </header>
        <!-- About-->
        <%-- <section class="page-section bg-primary" id="download">
                <div class="container px-4 px-lg-5">
                    <div class="row gx-4 gx-lg-5 justify-content-center">
                        <div class="col-lg-8 text-center">
                            <h2 class="text-white mt-0">Download Application Form!</h2>--%>
        <%--<hr class="divider divider-light" />--%>
        <%--                            <p class="text-white-75 mb-4">Start Bootstrap has everything you need to get your new website up and running in no time! Choose one of our open source, free to download, and easy to use themes! No strings attached!</p>--%>
        <%--<asp:Label ID="lb_BtnDownload" runat="server" Text=""></asp:Label>                            
                        </div>
                    </div>
                </div>
            </section>--%>
        <!-- Detail-->
        <section class="page-section" id="detail" style="display: none">
            <div id="AppDetail" class="container">
                <h2 class="text-center mt-6">Application ID : 
                        <asp:Label ID="lbApp_ID" runat="server" Text=""></asp:Label>
                    <input id="hdfApp_ID" runat="server" style="display: none" />
                </h2>
                <hr class="divider" />
                <div class="form-header d-flex mb-4">
                    <span class="stepIndicator" id="N">New Form</span>
                    <span class="stepIndicator" id="D">Draft</span>
                    <span class="stepIndicator" id="P">On Process</span>
                    <span class="stepIndicator" id="R">Revise</span>
                    <span class="stepIndicator" id="J">Reject</span>
                    <span class="stepIndicator" id="F">Finish</span>
                </div>
                <div class="row mt-5" <%--style="background: #f6e1c5"--%> style="background-color: rgb(245, 245, 245)">
                    <%--<div class="align-items-center" style="background-color:rgb(245, 245, 245)">--%>
                    <div class="col-lg" style="margin-top: auto; margin-bottom: auto">
                        <div class="text-center mt-3">
                            <div class="mb-2"><i class="fa-solid fa-circle-user fs-1 text-primary"></i><%--<i class="bi-gem fs-1 text-primary"></i>--%></div>
                            <h3 class="h4 mb-2">Vendor Information</h3>
                        </div>
                        <div class="card shadow mb-4">
                            <div class="card-body">
                                <p>
                                    Vendor Name :
                                    <asp:Label ID="lbVendorName" runat="server" Text=""></asp:Label>
                                </p>
                                <p>
                                    Vendor PIC :
                                    <asp:Label ID="lbVendorPIC" runat="server" Text=""></asp:Label>
                                </p>
                                <p>
                                    Email :
                                    <asp:Label ID="lbEmail" runat="server" Text=""></asp:Label>
                                </p>
                                <p id="UpdateDate">
                                    Update_Date :
                                    <asp:Label ID="lbUpdate_Date" runat="server" Text=""></asp:Label>
                                </p>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg mb-3" style="margin-top: auto; align-content: center">

                        <%--Finish card--%>
                        <div class="card shadow mb-3 mt-3" id="cardUploadedF" style="display: none">

                            <div class="card-body">
                                <div class="text-center">
                                    <i class="fa-solid fa-circle-check fs-1 text-success mb-2"></i>
                                    <h3>File Uploaded</h3>
                                    <h6>Form Completed.</h6>
                                    <button class="btn btn-primary btn-icon-split" type="button" data-toggle="collapse" data-target="#collapseTableF" aria-expanded="false" aria-controls="collapseTableF">
                                        <span class="icon text-white-50"><i class="fa-solid fa-magnifying-glass"></i></span>
                                        <span class="text">Show files uploaded</span>
                                    </button>
                                </div>
                                <div class="collapse" id="collapseTableF">
                                    <%--<h5>File list</h5>--%>
                                    <%--Data Table--%>

                                    <table id="tbFileF" class="table table-hover bg-light" style="width: 100%;">
                                        <thead>
                                            <tr>
                                                <th>File list</th>
                                                <th></th>
                                            </tr>
                                        </thead>
                                    </table>
                                </div>
                            </div>
                        </div>

                        <%--Uploaded card--%>
                        <div class="card shadow mb-3 mt-3" id="cardUploaded" style="display: none">
                            <div class="card-body">
                                <div class="text-center">
                                    <i class="fa-solid fa-circle-check fs-1 text-success mb-2"></i>
                                    <h3>File Uploaded</h3>
                                    <h6>Please waiting for verify.</h6>
                                    <button class="btn btn-primary btn-icon-split" type="button" data-toggle="collapse" data-target="#collapseTable" aria-expanded="false" aria-controls="collapseTable">
                                        <span class="icon text-white-50"><i class="fa-solid fa-magnifying-glass"></i></span>
                                        <span class="text">Show files uploaded</span>
                                    </button>
                                </div>
                                <div class="collapse" id="collapseTable">
                                    <%--<h5>File list</h5>--%>
                                    <%--Data Table--%>

                                    <table id="tbFile" class="table table-hover bg-light" style="width: 100%;">
                                        <thead>
                                            <tr>
                                                <th>File list</th>
                                                <th></th>
                                            </tr>
                                        </thead>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <%--Draft card--%>
                        <div class="card shadow text-center mb-3" id="cardDraft" style="display: none">
                            <div class="card-body">
                                <i class="fa-solid fa-pen-to-square fs-1 text-secondary mb-2"></i>
                                <h3>Draft</h3>
                                <p>This application is draft.</p>
                                <asp:Label ID="lbDraft" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <%--Processing card--%>
                        <div class="card shadow text-center mb-3" id="cardProcess" style="display: none;">
                            <div class="card-body">
                                <i class="fa-solid fa-arrows-rotate fs-1 text-info mb-2"></i>
                                <h3>On Process</h3>
                                <p>Please Upload Application file.</p>
                                <button class="btn btn-dark" type="button" data-toggle="modal" data-target="#uploadFileModal">Upload Files</button>
                            </div>
                        </div>
                        <%--Revise card--%>

                        <div class="card shadow text-center mb-3 " id="cardRevise" style="display: none">
                            <div class="card-body mt-3">
                                <i class="fa-solid fa-triangle-exclamation fs-1 text-warning mb-2"></i>
                                <h3>Revise</h3>
                                <br />
                                <h4 class="mb-2">Comment</h4>
                                <p>
                                    <asp:Label ID="lbComment" runat="server" Text=""></asp:Label>
                                </p>

                                <button class="btn btn-dark" type="button" data-toggle="modal" data-target="#uploadFileModal">Edit files</button>
                            </div>
                        </div>
                        <%--Reject card--%>
                        <div class="card shadow text-center mb-3" id="cardRejectNo" style="display: none">
                            <div class="card-body">
                                <i class="fa-solid fa-circle-xmark fa-2xl text-danger mb-2"></i>
                                <h3 class="mt-3">Reject</h3>
                                <h5 class="mb-2">Comment</h5>
                                <asp:Label ID="lbReject1" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="card shadow text-center mb-3" id="cardReject" style="display: none">
                            <div class="card-body">
                                <i class="fa-solid fa-circle-xmark fa-2xl text-danger mb-2"></i>
                                <h3 class="mt-2">Reject</h3>
                                <br />
                                <h4 class="mb-2">Comment</h4>
                                <asp:Label ID="lbReject" runat="server" Text=""></asp:Label>
                                <br />
                                <button class="btn btn-danger btn-icon-split mb-2 mt-2" type="button" data-toggle="collapse" data-target="#collapseTableJ" aria-expanded="false" aria-controls="collapseTableJ">
                                    <span class="icon text-white-50"><i class="fa-solid fa-magnifying-glass"></i></span>
                                    <span class="text">Show files uploaded</span>
                                </button>

                                <div class="collapse" id="collapseTableJ">
                                    <%--<h5>File list</h5>--%>
                                    <%--Data Table--%>

                                    <table id="tbFileJ" class="table table-hover bg-light" style="width: 100%;">
                                        <thead>
                                            <tr>
                                                <th>File list</th>
                                                <th></th>
                                            </tr>
                                        </thead>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <%--Finish card--%>
                        <div class="card shadow text-center mb-3" id="cardFinish" style="display: none">
                            <div class="card-body">
                                <span role="img" aria-bel="image" style="font-size: 50px; display: block;">🎉</span>
                                <h3>Finished</h3>
                                <asp:Label ID="lbFinished" runat="server" Text=""></asp:Label>
                            </div>
                        </div>

                        <%--Upload card--%>
                        <%--<div class="card shadow mb-3" id="cardUpload" style="display: none">
                                    <div class="card-header">
                                        <h3>Upload File</h3>
                                    </div>
                                    <div class="card-body">
                                        <asp:RequiredFieldValidator ID="File_Validate" runat="server" ControlToValidate="fileInput" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="Upload" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <input class="form-control" type="file" id="fileInput" runat="server" onchange="HandleChange()"/>
                                        <asp:Label ID="fileText" runat="server" Text=""></asp:Label>
                                        <asp:HiddenField ID="hdfFile" runat="server" />
                                    </div>
                                    <div class="card-footer">
                                        <button class="btn btn-success btn-icon-split" type="button" onclick="uploadClick('Upload')">
                                            <span class="icon text-white-50" style="margin-right: auto">
                                              <i class="fas fa-check"></i>
                                          </span>
                                          <span class="text" style="margin-right: auto">Upload</span>
                                        </button>
                                        <button class="btn btn-success" id="btnUpload" type="submit" runat="server" onserverclick="Upload_Click" style="display: none"></button>
                                    </div>
                                </div>--%>
                    </div>
                    <%--</div>--%>


                    <%--<div class="col-lg-3 col-md-6 text-center">
                            <div class="mt-5">
                                <div class="mb-2"><i class="bi-gem fs-1 text-primary"></i></div>
                                <h3 class="h4 mb-2">Sturdy Themes</h3>
                                <p class="text-muted mb-0">Our themes are updated regularly to keep them bug free!</p>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-6 text-center">
                            <div class="mt-5">
                                <div class="mb-2"><i class="bi-laptop fs-1 text-primary"></i></div>
                                <h3 class="h4 mb-2">Up to Date</h3>
                                <p class="text-muted mb-0">All dependencies are kept current to keep things fresh.</p>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-6 text-center">
                            <div class="mt-5">
                                <div class="mb-2"><i class="bi-globe fs-1 text-primary"></i></div>
                                <h3 class="h4 mb-2">Ready to Publish</h3>
                                <p class="text-muted mb-0">You can use this design as is, or you can make changes!</p>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-6 text-center">
                            <div class="mt-5">
                                <div class="mb-2"><i class="bi-heart fs-1 text-primary"></i></div>
                                <h3 class="h4 mb-2">Made with Love</h3>
                                <p class="text-muted mb-0">Is it really open source if it's not made with love?</p>
                            </div>
                        </div>--%>
                </div>
            </div>
            <%-- <div id="notfoundPage" class="container">
                    <h2 class="text-center mt-0">Please Enter Your Application ID :) </h2>
                    <hr class="divider" />
                    <div class="row gx-4 gx-lg-5 justify-content-center">
                        <div class="col-lg-8 text-center">
                            <button class="btn btn-dark btn-xl" type="button" onclick="RedirectClick()">Click Here!</button>
                            
                        </div>
                    </div>
                                      
                </div>--%>
        </section>
        <!-- Contact-->
        <section class="bg-dark text-white" id="contact">
            <div class="container-fluid px-4 px-lg-1 text-center" style="padding: 2rem">
                <div class="mb-4">
                    <img src="StartBootstrap/startbootstrap-creative-gh-pages/assets/img/Hino-logo.png" style="width: 150px" />
                </div>
                <h4 class="mb-4" style="color: rgb(245, 245, 245);">Hino motors manufacturing (thailand) ltd</h4>
                <p><i class="fa-solid fa-location-dot"></i>: 99 Moo 3, Teparak Road, Teparak, Muang Samutprakarn 10270</p>
                <p><i class="fa-solid fa-phone"></i>: 0-2384-2900, Ext 408 (Natchawan), Ext 415 (Nuttanon), Ext 403 (Warinthon)</p>
                <p><i class="fa-solid fa-fax"></i>: 02-384-0329</p>
                <p><i class="fa-solid fa-envelope"></i>: Contact@hinothailand.com</p>
                <%--<a class="btn btn-light btn-xl" href="https://startbootstrap.com/theme/creative/">Download Now!</a>--%>
            </div>
        </section>


        <!-- Footer-->
        <%--<footer class="bg-light py-3">
           <div class="container px-4 px-lg-5"><div class="small text-center text-muted">Copyright &copy; 2023 - Company Name</div></div>
       </footer>--%>
        <input id="hdfCC" runat="server" style="display: none" />
        <input id="hdfMail" runat="server" style="display: none" />
        <input id="hdfName" runat="server" style="display: none" />
        <input id="hdfPhone" runat="server" style="display: none" />
        <input id="hdfCreateID" runat="server" style="display: none" />
    </form>


    <!-- Bootstrap core JS-->
    <script src="Scripts/bootstrap.bundle.min.js"></script>
    <%--<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js"></script>--%>
    <!-- SimpleLightbox plugin JS-->
    <%--<script src="https://cdnjs.cloudflare.com/ajax/libs/SimpleLightbox/2.1.0/simpleLightbox.min.js"></script>--%>
    <!-- Core plugin JavaScript-->
    <script src="Scripts/jquery.easing.min.js"></script>
    <!-- Core theme JS-->
    <%--<script src="Scripts/jquery.min.js"></script>--%>
    <!-- Modal loading -->
    <script src="Content/jquery.loadingModal.css"></script>
    <script src="StartBootstrap/startbootstrap-creative-gh-pages/js/scripts.js"></script>
    <!-- Step Navigation-->
    <script src="StepNavigation/Step_Navigation.js"></script>
    <!-- * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *-->
    <!-- * *                               SB Forms JS                               * *-->
    <!-- * * Activate your form at https://startbootstrap.com/solution/contact-forms * *-->
    <!-- * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *-->
    <!--Data Tables -->
    <link href="DataTables/DataTables1.css" type="text/css" rel="stylesheet" />
    <script src="DataTables/DataTables1.js"></script>
    <!-- ajax script-->
    <script src="Scripts/Vendor.js"></script>
    <script src="Model/RejectTable.js"></script>
    <script src="Scripts/Vendor/Login/Alert_Vendor_Login.js"></script>
    <script src="Scripts/Vendor/Login/Get_Vendor_Login.js"></script>
    <script src="Scripts/Vendor/Login/POST_Vendor_Login.js"></script>
</body>

</html>
