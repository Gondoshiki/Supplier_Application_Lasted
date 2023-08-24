<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="PS_Detail.aspx.cs" Inherits="SupplierRegistration.PS_Detail" %>

<asp:Content ID="LogHead" ContentPlaceHolderID="head" runat="server">

    <%--<link href="DateTime/tempus-dominus.css" rel="stylesheet" />
    <script src="DateTime/Popper.js"></script>
    <script src="DateTime/tempus-dominus.js"></script>
    <script src="DateTime/Moment.js"></script>
    <script src="DateTime/Moment_Parse.js"></script>--%>
    <script src="Scripts/ShowLoading.js"></script>
</asp:Content>

<asp:Content ID="LogBody" ContentPlaceHolderID="Body" runat="server">

    <!----------Show loading ---------------->
    <div class="loader"></div>
    <!-- Draft Start -->
    <div class="container-fluid animated--grow-in">
        <%--        <asp:HiddenField ID="Email_Revise" runat="server" />
        <asp:HiddenField ID="Email_Finish" runat="server" />
        <asp:HiddenField ID="Email_Reject" runat="server" />--%>
        <asp:HiddenField ID="Encode_ID" runat="server" />
        <asp:HiddenField ID="hdfFile1" runat="server" />
        <asp:HiddenField ID="UploadFile" runat="server" />
        <asp:HiddenField ID="CreateCom" runat="server" />
        <asp:HiddenField ID="CreateID" runat="server" />
        <h1 class="h3 mb-2 text-gray-800 font-weight-bold">Supplier Detail</h1>
        <div class="row">
            <div class="col-lg-4 col-md-4 mb-4">
                <div class="card border-left-info shadow h-100 ">
                    <div class="card-body">
                        <div class="row no-gutters align-items-center">
                            <div class="col mr-2">
                                <div class="h5 font-weight-bold text-primary text-uppercase mb-1">
                                    Supplier Application No.
                                </div>
                                <div class="h3 mb-0 font-weight-bold text-gray-800">
                                    <asp:Label ID="App_ID" runat="server" Text=""></asp:Label>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-8 col-md-8 mb-4">
                <div class="card border-left-info shadow h-100 mb-4">
                    <div class="card-body">
                        <!-- start step indicators -->
                        <div class="form-header d-flex mb-4">

                            <span class="stepIndicator finish">New Form</span>
                            <span class="stepIndicator finish">Draft</span>
                            <span class="stepIndicator active">On Process</span>
                            <span class="stepIndicator">Revise</span>
                            <span class="stepIndicator">Reject</span>
                            <span class="stepIndicator">Finish</span>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <!-- Upload File Cocollapse -->

    <div class="container-fluid animated--grow-in" id="cardUpload" style="display: none">
        <div class="card shadow mb-3 border-left-info">
            <div class="card-body">
                <div class="text-center">
                    <i class=" fa-solid fa-circle-check fa-2xl text-info mb-2"></i>
                    <h3>File Uploaded</h3>
                    <button class="btn btn-primary btn-icon-split " type="button" data-toggle="collapse" data-target="#collapseTable12" aria-expanded="false" aria-controls="collapseTable12">
                        <span class="icon text-white-50"><i class="fa-solid fa-magnifying-glass"></i></span>
                        <span class="text">Show Files Upload</span>

                    </button>
                    <div class="collapse" id="collapseTable12">
                        <%--Data Table--%>
                        <table id="FileList" class="table table-hover bg-light text-left" style="width: 100%;">
                            <thead>
                                <tr>
                                    <th>Title</th>
                                    <th>FileName</th>
                                    <th>Source</th>

                                </tr>
                            </thead>
                        </table>
                    </div>

                </div>
            </div>
        </div>
    </div>

    <!-- Upload File Container-->
    <div class="container-fluid animated--grow-in">
        <div class="card shadow text-center mb-3 border-left-info" id="cardProcess" style="display: none">
            <div class="card-body">
                <i class="fa-solid fa-arrows-rotate fa-2xl text-info mb-2"></i>
                <h3>On Process</h3>
                <p>Not Upload Any Files Yet.</p>
                <%--                <button class="btn btn-dark" type="button" data-toggle="modal" data-target="#uploadFileModal">Upload Files</button>--%>
            </div>
        </div>
    </div>

    <div id="Modal_Replace">
        <!-- ReplaceAppRegis Modal -->
        <div class="modal fade" id="ReplaceAppRegis" tabindex="-1" role="dialog" aria-labelledby="ReplaceFileModal" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Application for Registration</h5>

                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <label class="form-label" for="inpAppFile">Choose File :</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="File1" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="AppRegis" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <div class="input-group">
                            <input class="form-control" type="file" id="File1" runat="server" onchange="checkFile('File1')" />
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" validationgroup="AppRegis" data-dismiss="modal">Close</button>
                        <button class="btn btn-primary btn-icon-split" type="button" validationgroup="AppRegis" onclick="uploadClick('AppRegis')">
                            <span class="text" style="margin-right: auto">Upload</span>
                        </button>
                        <button class="btn btn-dark" id="btnReplaceAppRegis" type="submit" runat="server" validationgroup="AppRegis" onserverclick="Upload_Click" style="display: none"></button>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="ReplaceSME" tabindex="-1" role="dialog" aria-labelledby="ReplaceFileModal" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">SMEs Research</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <label class="form-label" for="inpAppFile">Choose File :</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="File2" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="SME" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <div class="input-group">
                            <input class="form-control" type="file" id="File2" runat="server" onchange="checkFile('File2')" />
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" validationgroup="SME" data-dismiss="modal">Close</button>
                        <button class="btn btn-primary btn-icon-split" type="button" validationgroup="SME" onclick="uploadClick('SME')">
                            <%--<span class="icon text-white-50" style="margin-right: auto">
                            <i class="fas fa-check"></i>
                          </span>--%>
                            <span class="text" style="margin-right: auto">Upload</span>
                        </button>
                        <button class="btn btn-dark" id="btnReplaceSME" type="submit" runat="server" validationgroup="SME" onserverclick="Upload_Click" style="display: none"></button>
                    </div>
                </div>
            </div>
        </div>
        <!-- ReplaceRegisCert Modal -->
        <div class="modal fade" id="ReplaceRegisCert" tabindex="-1" role="dialog" aria-labelledby="ReplaceFileModal" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">หนังสือรับรอง</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <label class="form-label" for="inpAppFile">Choose File :</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="File3" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="RegisCert" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <div class="input-group">
                            <input class="form-control" type="file" id="File3" runat="server" onchange="checkFile('File3')" />
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" validationgroup="RegisCert" data-dismiss="modal">Close</button>
                        <button class="btn btn-primary btn-icon-split" type="button" validationgroup="RegisCert" onclick="uploadClick('RegisCert')">
                            <%--<span class="icon text-white-50" style="margin-right: auto">
                            <i class="fas fa-check"></i>
                          </span>--%>
                            <span class="text" style="margin-right: auto">Upload</span>
                        </button>
                        <button class="btn btn-dark" id="btnReplaceRegisCert" type="submit" runat="server" validationgroup="RegisCert" onserverclick="Upload_Click" style="display: none"></button>
                    </div>
                </div>
            </div>
        </div>
        <!-- ReplacePP20 Modal -->
        <div class="modal fade" id="ReplacePP20" tabindex="-1" role="dialog" aria-labelledby="ReplaceFileModal" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">ภพ.20</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <label class="form-label" for="inpAppFile">Choose File :</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="File4" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="PP20" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <div class="input-group">
                            <input class="form-control" type="file" id="File4" runat="server" onchange="checkFile('File4')" />
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" validationgroup="PP20" data-dismiss="modal">Close</button>
                        <button class="btn btn-primary btn-icon-split" type="button" validationgroup="PP20" onclick="uploadClick('PP20')">
                            <%--<span class="icon text-white-50" style="margin-right: auto">
                            <i class="fas fa-check"></i>
                          </span>--%>
                            <span class="text" style="margin-right: auto">Upload</span>
                        </button>
                        <button class="btn btn-dark" id="btnReplacePP20" type="submit" runat="server" validationgroup="PP20" onserverclick="Upload_Click" style="display: none"></button>
                    </div>
                </div>
            </div>
        </div>
        <!-- ReplaceBookBank Modal -->
        <div class="modal fade" id="ReplaceBookBank" tabindex="-1" role="dialog" aria-labelledby="ReplaceFileModal" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Book Bank</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <label class="form-label" for="inpAppFile">Choose File :</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="File5" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="BookBank" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <div class="input-group">
                            <input class="form-control" type="file" id="File5" runat="server" onchange="checkFile('File5')" />
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" validationgroup="BookBank" data-dismiss="modal">Close</button>
                        <button class="btn btn-primary btn-icon-split" type="button" validationgroup="BookBank" onclick="uploadClick('BookBank')">
                            <%--<span class="icon text-white-50" style="margin-right: auto">
                            <i class="fas fa-check"></i>
                          </span>--%>
                            <span class="text" style="margin-right: auto">Upload</span>
                        </button>
                        <button class="btn btn-dark" id="btnReplaceBookBank" type="submit" runat="server" validationgroup="BookBank" onserverclick="Upload_Click" style="display: none"></button>
                    </div>
                </div>
            </div>
        </div>
        <!-- ReplaceBOJ5 Modal -->
        <div class="modal fade" id="ReplaceBOJ5" tabindex="-1" role="dialog" aria-labelledby="ReplaceFileModal" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">บอจ.5</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <label class="form-label" for="inpAppFile">Choose File :</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="File6" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="BOJ5" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <div class="input-group">
                            <input class="form-control" type="file" id="File6" runat="server" onchange="checkFile('File6')" />
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" validationgroup="BOJ5" data-dismiss="modal">Close</button>
                        <button class="btn btn-primary btn-icon-split" type="button" validationgroup="BOJ5" onclick="uploadClick('BOJ5')">
                            <%--<span class="icon text-white-50" style="margin-right: auto">
                            <i class="fas fa-check"></i>
                          </span>--%>
                            <span class="text" style="margin-right: auto">Upload</span>
                        </button>
                        <button class="btn btn-dark" id="btnReplaceBOJ5" type="submit" runat="server" validationgroup="BOJ5" onserverclick="Upload_Click" style="display: none"></button>
                    </div>
                </div>
            </div>
        </div>
        <!-- ReplaceOrgCompany Modal -->
        <div class="modal fade" id="ReplaceOrgCompany" tabindex="-1" role="dialog" aria-labelledby="ReplaceFileModal" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Organization Company</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <label class="form-label" for="inpAppFile">Choose File :</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="File7" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="OrgCompany" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <div class="input-group">
                            <input class="form-control" type="file" id="File7" runat="server" onchange="checkFile('File7')" />
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" validationgroup="OrgCompany" data-dismiss="modal">Close</button>
                        <button class="btn btn-primary btn-icon-split" type="button" validationgroup="OrgCompany" onclick="uploadClick('OrgCompany')">
                            <%--<span class="icon text-white-50" style="margin-right: auto">
                            <i class="fas fa-check"></i>
                          </span>--%>
                            <span class="text" style="margin-right: auto">Upload</span>
                        </button>
                        <button class="btn btn-dark" id="btnReplaceOrgCompany" type="submit" runat="server" validationgroup="OrgCompany" onserverclick="Upload_Click" style="display: none"></button>
                    </div>
                </div>
            </div>
        </div>
        <!-- ReplaceSPS10 Modal -->
        <div class="modal fade" id="ReplaceSPS10" tabindex="-1" role="dialog" aria-labelledby="ReplaceFileModal" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">สปส1-10</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <label class="form-label" for="inpAppFile">Choose File :</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="File8" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="SPS10" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <div class="input-group">
                            <input class="form-control" type="file" id="File8" runat="server" onchange="checkFile('File8')" />
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" validationgroup="SPS10" data-dismiss="modal">Close</button>
                        <button class="btn btn-primary btn-icon-split" type="button" validationgroup="SPS10" onclick="uploadClick('SPS10')">
                            <%--<span class="icon text-white-50" style="margin-right: auto">
                            <i class="fas fa-check"></i>
                          </span>--%>
                            <span class="text" style="margin-right: auto">Upload</span>
                        </button>
                        <button class="btn btn-dark" id="btnReplaceSPS10" type="submit" runat="server" validationgroup="SPS10" onserverclick="Upload_Click" style="display: none"></button>
                    </div>
                </div>
            </div>
        </div>


    </div>


    <!-- Upload File Modal -->
    <%--    <div class="modal fade" id="uploadFileModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Files Upload</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <label class="form-label" for="inpAppFile">Application form (.xlsx) :</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="AppformLink" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="ProcessUpload" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <div class="input-group">
                            <input class="form-control" type="file" id="AppformLink" runat="server" />
                        </div>

                    </div>
                    <div class="mb-3">
                        <label class="form-label" for="fileInput">หนังสือรับรอง :</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="inpFileRegisCert" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="ProcessUpload" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <div class="input-group">
                            <input class="form-control" type="file" id="inpFileRegisCert" runat="server" />
                        </div>

                    </div>
                    <div class="mb-3">
                        <label class="form-label" for="fileInput">ภพ.20 :</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="inpFilePP20" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="ProcessUpload" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <div class="input-group">
                            <input class="form-control" type="file" id="inpFilePP20" runat="server" />
                        </div>

                    </div>
                    <div class="mb-3">
                        <label class="form-label" for="fileInput">Book Bank :</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="inpFileBookBank" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="ProcessUpload" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <div class="input-group">
                            <input class="form-control" type="file" id="inpFileBookBank" runat="server" />
                        </div>

                    </div>
                    <div class="mb-3">
                        <label class="form-label" for="fileInput">บอจ.5 (สำเนารายชื่อผู้ถือหุ้น) :</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="inpFileBOJ5" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="ProcessUpload" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <div class="input-group">
                            <input class="form-control" type="file" id="inpFileBOJ5" runat="server" />
                        </div>

                    </div>
                    <div class="mb-3">
                        <label class="form-label" for="fileInput">Organization company :</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="inpFileOrgCompany" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="ProcessUpload" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <div class="input-group">
                            <input class="form-control" type="file" id="inpFileOrgCompany" runat="server" />
                        </div>

                    </div>
                    <div class="mb-3">
                        <label class="form-label" for="">สปส1-10 หรือ งบการเงิน (<span style="color: #ff0000">Only SMEs</span>) :</label>
                        <div class="input-group">
                            <input class="form-control" type="file" id="inpFileSPS10" runat="server" />
                        </div>
                    </div>
                    <!-- Hidden Field -->
                    <asp:HiddenField ID="hdfFile" runat="server" />
                    <asp:HiddenField ID="hdfFileUploaded" runat="server" />
                    <asp:HiddenField ID="hdfStatus" runat="server" />

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <button class="btn btn-primary btn-icon-split" type="button" onclick="uploadClick('ProcessUpload')">
  
                        <span class="text" style="margin-right: auto">Upload</span>
                    </button>
                    <button class="btn btn-dark" id="btnUpload" type="submit" runat="server" onserverclick="Upload_Click" style="display: none"></button>
                </div>
            </div>
        </div>
    </div>--%>



    <div class="container-fluid animated--grow-in">
        <div class="card shadow border-left-info mb-4">
            <div class="card-header py-3">
                <h6 class="h5 m-0 font-weight-bold text-info">Process Form</h6>
            </div>
            <div class="card-body mb-0">
                <!-- Input textbox-->
                <div class="mb-3">
                    <label for="InputName" class="form-label">Vendor Name</label>
                    <!-- Required Validator Function -->

                    <input class="form-control" id="Vendor_Name" runat="server" readonly />

                </div>

                <div class="mb-3">
                    <label for="InputPIC" class="form-label">Vendor PIC</label>

                    <input class="form-control" id="Vendor_PIC" runat="server" readonly />

                </div>
                <div class="mb-3">
                    <label for="InputEmail" class="form-label">Email</label>

                    <input class="form-control" id="Email" runat="server" readonly />

                </div>
                <div class="mb-3">
                    <label for="InputGM" class="form-label">CC Email</label>

                    <input class="form-control" id="GmMail" runat="server" readonly />

                </div>


                <div id="Button trigger modal">
                    <div class="container-fiuld">
                        <!-- Button trigger modal -->
                        <%--<button id="Body_btnSubmit" class="btn btn-warning btn-icon-split" type="button" validationgroup="Import" onclick="return showModal('Import')">
                                        <span class="icon text-black-50">
                                            <i class="fas fa-file-import"></i>
                                        </span>
                                        <span class="text" style="color:#000"><b>
                                            <span id="Body_lbImport">Import</span></b>
                                        </span>
                                    </button>--%>


                        <button id="btnReviseModal" type="button" class="btn btn-warning"
                            data-toggle="modal" data-target="#Revise_Modal">
                            <span class="icon text-black-50">
                                <i class="bi bi-pencil-square"></i></span>Revise
                        </button>
                        <button id="btnRejectModal" type="button" class="btn btn-danger"
                            data-toggle="modal" data-target="#Reject_Modal">
                            <span class="icon text-black-50"><i class="bi bi-x-circle-fill"></i></span>Reject
                        </button>

                        <button id="btnFinishModal" type="button" class="btn btn-success"
                            data-toggle="modal" data-target="#Finish_Modal">
                            <span class="icon text-black-50"><i class="bi bi-check-circle-fill"></i></span>Finish
                        </button>
                    </div>

                    <!-- Modal -->
                    <div class="modal fade" id="Revise_Modal">
                        <div class="modal-dialog modal-xl">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title">Comment</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">× </span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="mb-3">
                                        <label for="Email_Sent" class="col-form-label">Email:</label>
                                        <asp:RequiredFieldValidator ID="RequiredEmail_Sent" runat="server" ControlToValidate="Email_Revise" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="Revise" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <input type="text" class="form-control" id="Email_Revise" runat="server" placeholder="Please Enter Email">
                                    </div>
                                    <div class="mb-3">
                                        <label for="Comment-text" class="col-form-label">Comment:</label>
                                        <asp:RequiredFieldValidator ID="RequiredCommentRevise" runat="server" ControlToValidate="Comment_Revise" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="Revise" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <textarea type="text" class="form-control" id="Comment_Revise" runat="server"></textarea>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                    <button id="btnRevise" class="btn btn-warning" validationgroup="Revise" type="button" runat="server" onclick="return Revise('Revise');"><b>Submit Revise</b></button>
                                    <button id="btn_SubmitRevise" class="btn btn-primary" type="submit" runat="server" style="display: none" validationgroup="Revise" onserverclick="Revise_Click"><b>Revise</b></button>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal fade" id="Reject_Modal">
                        <div class="modal-dialog modal-xl">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title">Comment</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">× </span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="mb-3">
                                        <label for="Email_Sent" class="col-form-label">Email:</label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Email_Rej" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="Reject" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <input type="text" class="form-control" id="Email_Rej" runat="server" placeholder="Please Enter Email">
                                    </div>
                                    <div class="mb-3">
                                        <label for="Comment-text" class="col-form-label">Comment:</label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Comment_Reject" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="Reject" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <textarea type="text" class="form-control" id="Comment_Reject" runat="server"></textarea>
                                    </div>
                                </div>

                                <div class="modal-footer">
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                    <button id="btnReject" class="btn btn-danger" validationgroup="Reject" type="button" runat="server" onclick="return Reject('Reject');"><b>Reject</b></button>
                                    <button id="btn_Reject" class="btn btn-danger" type="submit" runat="server" style="display: none" validationgroup="Reject" onserverclick="Reject_Click"><b>Reject</b></button>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal fade" id="Finish_Modal">
                        <div class="modal-dialog modal-xl">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title">Comment</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">× </span></button>
                                </div>
                                <div class="modal-body">
                                    <div class="mb-3">
                                        <label for="Email_Sent" class="col-form-label">Email:</label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Email_Finish" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="Finish" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <input type="text" class="form-control" id="Email_Finish" runat="server" placeholder="Please Enter Email">
                                    </div>
                                    <div class="mb-3">
                                        <label for="Comment-text" class="col-form-label">Comment:</label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="Comment_Finish" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="Finish" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <textarea type="text" class="form-control" id="Comment_Finish" runat="server"></textarea>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                    <button id="btnFinish" class="btn btn-success" validationgroup="Finish" type="button" runat="server" onclick="return Finish('Finish');"><b>Finish</b></button>
                                    <button id="btn_Finish" class="btn btn-success" type="submit" runat="server" style="display: none" validationgroup="Finish" onserverclick="Finish_Click"><b>Finish</b></button>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
            <!-- End Input-->
            <%--  <div class="mb-3 form-check">
    <input type="checkbox" class="form-check-input" id="exampleCheck1">
    <label class="form-check-label" for="exampleCheck1">Check me out</label>
  </div>--%>
        </div>
    </div>

    <div class=" container-fluid">
        <div class="card shadow mb-4 border-left-info">
            <div class="card-header">
                <h4 class="m-0 font-weight-bold text-info">Transaction</h4>
            </div>
            <div class="card-body">
                <asp:Label ID="Transaction" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>
    <script src="Scripts/PS/Detail/Alert_PS_Detail.js"></script>
    <script src="Scripts/PS/Detail/POST_PS_Detail.js"></script>
    <script src="Model/FileProcess.js"></script>
</asp:Content>


