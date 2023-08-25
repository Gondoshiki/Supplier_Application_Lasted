<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="PS_Revise.aspx.cs" Inherits="SupplierRegistration.PS_Revise" %>

<asp:Content ID="LogHead" ContentPlaceHolderID="head" runat="server">

    <%--<link href="DateTime/tempus-dominus.css" rel="stylesheet" />
    <script src="DateTime/Popper.js"></script>
    <script src="DateTime/tempus-dominus.js"></script>
    <script src="DateTime/Moment.js"></script>
    <script src="DateTime/Moment_Parse.js"></script>--%>
   <script src="Scripts/ShowLoading.js"></script>
    <script src="Scripts/PS/Revise/POST_PS_Revise.js"></script>
</asp:Content>


<asp:Content ID="LogBody" ContentPlaceHolderID="Body" runat="server">

    <!----- Loader ------------>
    <div class="loader"></div>
    <!-- Draft Start -->
    <div class="container-fluid animated--grow-in">
        <asp:HiddenField ID="hdfFile1" runat="server" />
        <asp:HiddenField ID="UploadFile" runat="server" />
        <h1 class="h3 mb-2 text-gray-800 font-weight-bold">Supplier Detail</h1>
        <div class="row">
            <div class="col-lg-4 col-md-4 mb-4">
                <div class="card border-left-warning shadow h-100 ">
                    <div class="card-body">
                        <div class="row no-gutters align-items-center">
                            <div class="col mr-2">
                                <div class="h5 font-weight-bold text-primary text-uppercase mb-1">
                                    Supplier Application No.
                                </div>
                                <div class="h3 mb-0 font-weight-bold text-gray-800">
                                    <asp:Label ID="App_ID" runat="server" Text=""></asp:Label>
                                </div>
                                <!-- Upload File Container-->

                            </div>
                            <%--<div class="col-auto">
                                            <i class="fas fa-calendar fa-2x text-gray-300"></i>
                                        </div>--%>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Earnings (Monthly) Card Example -->
            <div class="col-lg-8 col-md-8 mb-4">
                <div class="card border-left-warning shadow h-100 ">
                    <div class="card-body">
                        <!-- start step indicators -->
                        <div class="form-header d-flex mb-4">
                            <span class="stepIndicator finish">New Form</span>
                            <span class="stepIndicator finish">Draft</span>
                            <span class="stepIndicator finish">On Process</span>
                            <span class="stepIndicator active">Revise</span>
                            <span class="stepIndicator">Reject</span>
                            <span class="stepIndicator">Finish</span>
                        </div>
                        <!-- end step indicators -->
                    </div>
                </div>
            </div>
        </div>

    </div>
    <div class="container-fluid mb-3 animated--grow-in">

            <div class="card border-left-warning shadow ">
                <div class="card-body">
                    <div class="row no-gutters align-items-center">
                        <div class="col mr-2">
                            <div class="h5 font-weight-bold text-primary text-uppercase mb-1">
                                <h5 class="font-weight-bold text-primary text-uppercase">Comment</h5>
                            </div>
                            <div class="h5 mb-0 font-weight-bold text-gray-800">
                                <asp:Label ID="Comment" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>


    <div class="container-fluid animated--grow-in">
        <div class="card shadow text-center mb-3 border-left-warning" id="cardProcess" style="display: none">
            <div class="card-body">
                <i class="fa-solid fa-arrows-rotate fa-2xl text-info mb-2"></i>
                <h3>On Pause</h3>
                <p>Not Upload Any Files Yet.</p>
                <%--                <button class="btn btn-dark" type="button" data-toggle="modal" data-target="#uploadFileModal">Upload Files</button>--%>
            </div>
        </div>
    </div>

   <div class="container-fluid animated--grow-in">
        <div class="card shadow border-left-warning mb-3" id="UploadBar" style="display: none">
            <div class="card-body">
                <div class="text-center">
                    <i class="fa-solid fa-circle-exclamation fa-2xl" style="color: #e47d07;"></i>
                    <h3>File Uploaded</h3>
                    <button class="btn btn-primary btn-icon-split " type="button" data-toggle="collapse" data-target="#collapseTable12" aria-expanded="false" aria-controls="collapseTable12">
                        <span class="icon text-white-50"><i class="fa-solid fa-magnifying-glass"></i></span>
                        <span class="text">Show Files Upload</span>
                    </button>
                    <div class="collapse" id="collapseTable12">
                        <h5>File list</h5>
                        <%--Data Table--%>
                        <table id="ReviseTable" class="table table-hover bg-light text-left table-bordered" style="width: 100%;">
                            <thead>
                                <tr>
                                    <th>Title</th>
                                    <th>FileName</th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <script src="Model/ReviseTable.js"></script>
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
                            <input class="form-control" type="file" id="File1" runat="server" />
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        <button class="btn btn-primary btn-icon-split" type="button" onclick="uploadClick('AppRegis')">
                            <span class="text" style="margin-right: auto">Upload</span>
                        </button>
                        <button class="btn btn-dark" id="btnReplaceAppRegis" type="submit" runat="server" validationGroup="AppRegis" onserverclick="Upload_Click" style="display: none"></button>
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
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="File2" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="BOJ5" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <div class="input-group">
                            <input class="form-control" type="file" id="File2" runat="server" />
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        <button class="btn btn-primary btn-icon-split" type="button" onclick="uploadClick('BOJ5')">
                            <%--<span class="icon text-white-50" style="margin-right: auto">
                            <i class="fas fa-check"></i>
                          </span>--%>
                            <span class="text" style="margin-right: auto">Upload</span>
                        </button>
                        <button class="btn btn-dark" id="btnReplaceBOJ5" type="submit" runat="server" validationGroup="BOJ5" onserverclick="Upload_Click" style="display: none"></button>
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
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="File3" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="BookBank" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <div class="input-group">
                            <input class="form-control" type="file" id="File3" runat="server" />
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        <button class="btn btn-primary btn-icon-split" type="button" onclick="uploadClick('BookBank')">
                            <%--<span class="icon text-white-50" style="margin-right: auto">
                            <i class="fas fa-check"></i>
                          </span>--%>
                            <span class="text" style="margin-right: auto">Upload</span>
                        </button>
                        <button class="btn btn-dark" id="btnReplaceBookBank" type="submit" runat="server" validationGroup="BookBank" onserverclick="Upload_Click" style="display: none"></button>
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
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="File4" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="OrgCompany" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <div class="input-group">
                            <input class="form-control" type="file" id="File4" runat="server" />
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        <button class="btn btn-primary btn-icon-split" type="button" onclick="uploadClick('OrgCompany')">
                            <%--<span class="icon text-white-50" style="margin-right: auto">
                            <i class="fas fa-check"></i>
                          </span>--%>
                            <span class="text" style="margin-right: auto">Upload</span>
                        </button>
                        <button class="btn btn-dark" id="btnReplaceOrgCompany" type="submit" runat="server" ValidationGroup="OrgCompany" onserverclick="Upload_Click" style="display: none"></button>
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
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="File5" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="PP20" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <div class="input-group">
                            <input class="form-control" type="file" id="File5" runat="server" />
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        <button class="btn btn-primary btn-icon-split" type="button" onclick="uploadClick('PP20')">
                            <%--<span class="icon text-white-50" style="margin-right: auto">
                            <i class="fas fa-check"></i>
                          </span>--%>
                            <span class="text" style="margin-right: auto">Upload</span>
                        </button>
                        <button class="btn btn-dark" id="btnReplacePP20" type="submit" runat="server" ValidationGroup="PP20" onserverclick="Upload_Click" style="display: none"></button>
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
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="File6" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="RegisCert" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <div class="input-group">
                            <input class="form-control" type="file" id="File6" runat="server" />
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        <button class="btn btn-primary btn-icon-split" type="button"  ValidationGroup="RegisCert" onclick="uploadClick('RegisCert')"><span class="text" style="margin-right: auto">Upload</span></button>
                        <button class="btn btn-dark" id="btnReplaceRegisCert" type="submit" runat="server" onserverclick="Upload_Click" style="display: none"></button>
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
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="File7" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="SPS10" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <div class="input-group">
                            <input class="form-control" type="file" id="File7" runat="server" />
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        <button class="btn btn-primary btn-icon-split" type="button" onclick="uploadClick('SPS10')">
                            <%--<span class="icon text-white-50" style="margin-right: auto">
                            <i class="fas fa-check"></i>
                          </span>--%>
                            <span class="text" style="margin-right: auto">Upload</span>
                        </button>
                        <button class="btn btn-dark" id="btnReplaceSPS10" type="submit" runat="server" ValidationGroup="SPS10" onserverclick="Upload_Click" style="display: none"></button>
                    </div>
                </div>
            </div>
        </div>
        <!-- ReplaceSME Modal -->
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
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="File8" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="SME" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <div class="input-group">
                            <input class="form-control" type="file" id="File8" runat="server" />
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        <button class="btn btn-primary btn-icon-split" type="button" validationgroup="SME" onclick="uploadClick('SME')"><span class="text" style="margin-right: auto">Upload</span></button>
                        <button class="btn btn-dark" id="btnReplaceSME" type="submit" runat="server" onserverclick="Upload_Click" style="display: none"></button>
                    </div>
                </div>
            </div>
        </div>
    </div>







    <div class="container-fluid animated--grow-in">
        <div class="card shadow mb-4 border-left-warning">
            <div class="card-header py-3">
                <h class="h5 font-weight-bold text-warning">Revise Form</h>
            </div>
            <div class="card-body">
                <!-- Input textbox-->
                <div class="mb-3">
                    <label for="InputName" class="form-label">Vendor Name</label>
                    <!-- Required Validator Function -->
                        <input class="form-control" id="Vendor_Name_R" runat="server" readonly />
                </div>

                <div class="mb-3">
                    <label for="InputPIC" class="form-label">Vendor PIC</label>
                        <input class="form-control" id="Vendor_PIC_R" runat="server" readonly />
                </div>


                <div class="mb-3">
                    <label for="InputEmail" class="form-label">Email</label>
                        <input type="text" class="form-control" id="Email_R" runat="server" aria-describedby="EmailName" readonly>

                        <%--<input class="form-control" id="Email_R" runat="server" readonly />--%>

                        <%--                            <asp:Label ID="Email" runat="server" Text=""></asp:Label>--%>
                </div>
                <div class="mb-3">
                    <label for="InputGM" class="form-label">CC Email</label>
                        <input class="form-control" id="GmMail" runat="server" readonly />
                </div>

            </div>
        </div>
    </div>
    <!-- End Input-->
    <%--  <div class="mb-3 form-check">
    <input type="checkbox" class="form-check-input" id="exampleCheck1">
    <label class="form-check-label" for="exampleCheck1">Check me out</label>
  </div>--%>


    <div class=" container-fluid animated--grow-in">
        <div class="card shadow mb-4 border-left-warning">
            <div class="card-header">
                <h4 class="m-0 font-weight-bold text-warning">Transaction</h4>
            </div>
            <div class="card-body">
                <asp:Label ID="Transaction" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>

