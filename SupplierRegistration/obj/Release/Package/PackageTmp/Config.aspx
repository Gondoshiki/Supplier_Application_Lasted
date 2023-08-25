<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Config.aspx.cs" Inherits="SupplierRegistration.Config" %>

<asp:Content ID="LogHead" ContentPlaceHolderID="head" runat="server">

    <%--<link href="DateTime/tempus-dominus.css" rel="stylesheet" />
    <script src="DateTime/Popper.js"></script>
    <script src="DateTime/tempus-dominus.js"></script>
    <script src="DateTime/Moment.js"></script>
    <script src="DateTime/Moment_Parse.js"></script>--%>
<script src="Scripts/ShowLoading.js"></script>
</asp:Content>
<asp:Content ID="LogBody" ContentPlaceHolderID="Body" runat="server">

    <!----- Loader ------------>
    <div class="loader"></div>

    <%--Upload File Modal--%>
    <div class="modal fade" id="uploadFile_Modal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <button class="btn btn-dark" id="hdfResetEmail" type="button" runat="server" onserverclick="sendEmailResetPassword_Click" style="display: none"></button>
        <asp:HiddenField ID="hdfEmailTo" runat="server" />
        <asp:HiddenField ID="hdfNameTo" runat="server" />
        <asp:HiddenField ID="HiddenField1" runat="server" />
        <asp:HiddenField ID="hdfToken" runat="server" />
        <asp:HiddenField ID="hdfAppIDEncode" runat="server" />
        <asp:HiddenField ID="hdfTokenEncode" runat="server" />
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="exampleModalLabel">Upload File</h1>
                    <button type="button" class="close" data-dismiss="modal" style="color: black">&times;</button>
                </div>
                <div class="modal-body">
                    <div class="mb-1">
                        <label for="system-name" class="col-form-label">Folder Name:</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="FolderName" ErrorMessage="* Please fill in the fields." ForeColor="#ff0000" ValidationGroup="upload" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <input type="Text" class="form-control" id="FolderName" placeholder="Please fill folder name" runat="server" />
                        <asp:RegularExpressionValidator display="Dynamic" ValidationGroup="upload" ID="RegularExpressionFolderName" runat="server" ControlToValidate="FolderName" ErrorMessage=" Enter only a-z ก-ฮ ๐-๙ A- Z 0-9 . _ # ห้ามเว้นวรรค" ValidationExpression="^[a-zA-Z0-9ก-๙,.#_]*$" Font-Bold="true" ForeColor="Red"> </asp:RegularExpressionValidator>
                    </div>
                    <div class="mb-3">
                        <label for="system-name" class="col-form-label">File:</label>
                        <asp:RequiredFieldValidator ID="FileUpload_Validate" runat="server" ControlToValidate="fileUpload" ErrorMessage="* Please fill in the fields." ForeColor="#ff0000" ValidationGroup="upload" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <div class="input-group">
                            <input type="file" class="form-control" id="fileUpload" runat="server">
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                    <button class="btn btn-primary btn-icon-split" type="button" validationgroup="upload" onclick="PS_UploadClick('upload')">
                        <span class="text" style="margin-right: auto">Upload</span></button>
                    <button class="btn btn-dark" id="btnAddSystem" type="submit" runat="server"  validationGroup="upload" onserverclick="UploadFile_Click" style="display: none">Upload</button>
                </div>
            </div>
        </div>
    </div>
    <%-- Edit app form--%>
    <div class="modal fade" id="editFile_Modal" tabindex="-1" aria-labelledby="exampleModalLabel2" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="exampleModalLabel2">Edit File</h1>
                    <button type="button" class="close" data-dismiss="modal" style="color: black">&times;</button>
                </div>
                <div class="modal-body">
                    <div class="mb-1">
                        <label for="system-name" class="col-form-label">Folder Name:</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="inpEditfolder" ErrorMessage="* Please fill in the fields." ForeColor="#ff0000" ValidationGroup="edit" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <input type="Text" class="form-control" id="inpEditfolder" placeholder="Please fill folder name" runat="server">
                        <asp:RegularExpressionValidator ValidationGroup="edit" ID="RegularExpressionValidator1" runat="server" ControlToValidate="inpEditfolder" ErrorMessage="*Enter only a-z   A- Z  0-9   .#. NO Space bar" ValidationExpression="^[a-zA-Z0-9,.#_]*$" Font-Bold="true" ForeColor="Red"> </asp:RegularExpressionValidator>
                    </div>
                    <div class="mb-3">
                        <label for="system-name" class="col-form-label">File:</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="inpEditfile" ErrorMessage="* Please fill in the fields." ForeColor="#ff0000" ValidationGroup="edit" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <div class="input-group">
                            <input type="file" class="form-control" id="inpEditfile" runat="server" onchange="checkFile('inpEditfile')">
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                    <button class="btn btn-primary btn-icon-split" type="button" validationgroup="edit" onclick="editFile('edit')">
                        <span class="text" style="margin-right: auto">Edit</span></button>
                    <button class="btn btn-dark" id="btnEditfile" type="submit" runat="server" validationGroup="edit" onserverclick="EditFile_Click" style="display: none">Upload</button>
                </div>
            </div>
        </div>
    </div>
    <%--Add authorize Modal--%>
    <div class="modal fade" id="AddAuthorize_Modal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="exampleModalLabel1">Add Authorize</h1>
                    <button type="button" class="close" data-dismiss="modal" style="color: black">&times;</button>

                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <label for="system-name" class="col-form-label">Employee ID :</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="EmployeeID" ErrorMessage="* please fill in the fields." ForeColor="#ff0000" ValidationGroup="Authorize" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <input type="number" class="form-control" id="EmployeeID" runat="server" data-maxlength="8" oninput="this.value=this.value.slice(0,this.dataset.maxlength)" placeholder="Employee ID" pattern="[0-9]">
                    </div>
                    <div class="mb-3">
                        <label for="system-name" class="col-form-label">Phone :</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="Phone" ErrorMessage="* please fill in the fields." ForeColor="#ff0000" ValidationGroup="Authorize" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <input type="number" class="form-control" id="Phone" runat="server" data-maxlength="10" oninput="this.value=this.value.slice(0,this.dataset.maxlength)" placeholder="0-10 Digits" pattern="[0-9]">
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                    <button id="btnAuth" class="btn btn-success" validationgroup="Authorize" type="button" runat="server" onclick="return Auth('Authorize');"><b>Confirm</b></button>
                    <button id="btn_Auth" class="btn btn-success" type="submit" runat="server" style="display: none" validationGroup="Authorize" onserverclick="Auth_Click"><b>Auth</b></button>
                </div>
            </div>
        </div>
    </div>
    <%--Edit authorize Modal--%>
    <div class="modal fade" id="editAuthorize_Modal" tabindex="-1" aria-labelledby="exampleModalLabe3" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="exampleModalLabe3">Edit Authorize</h1>
                    <button type="button" class="close" data-dismiss="modal" style="color: black">&times;</button>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <label for="system-name" class="col-form-label">Employee ID :</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="empID" ErrorMessage="* please fill in the fields." ForeColor="#ff0000" ValidationGroup="editAuthorize" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <input type="number" class="form-control" id="empID" runat="server" readonly>
                    </div>
                    <div class="mb-3">
                        <label for="system-name" class="col-form-label">Email :</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="empEmail" ErrorMessage="* please fill in the fields." ForeColor="#ff0000" ValidationGroup="editAuthorize" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <input type="text" class="form-control" id="empEmail" runat="server" maxlength="100">
                    </div>
                    <div class="mb-3">
                        <label for="system-name" class="col-form-label">Phone :</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="empPhone" ErrorMessage="* please fill in the fields." ForeColor="#ff0000" ValidationGroup="editAuthorize" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <input type="number" class="form-control" id="empPhone" runat="server" data-maxlength="10" oninput="this.value=this.value.slice(0,this.dataset.maxlength)" placeholder="0-10 Digits" pattern="[0-9]" />
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                    <button id="Button1" class="btn btn-success" validationgroup="editAuthorize" type="button" runat="server" onclick="return editUser('editAuthorize');"><b>Confirm</b></button>
                    <button id="btnEditAuth" class="btn btn-success" type="submit" runat="server" style="display: none" validationgroup="editAuthorize" onserverclick="EditAuth_Click">Edit</button>
                </div>
            </div>
        </div>
    </div>
    <div class="container-fluid animated--grow-in">
        <!-- Hiiden Feild -->
        <asp:HiddenField ID="hdfFile" runat="server" />
        <asp:HiddenField ID="hdfFileName" runat="server" />
        <asp:HiddenField ID="hdfFolderName" runat="server" />
        <asp:HiddenField ID="hdfEmpID" runat="server" />

        <!-- Hidden button for delete file-->
        <button type="submit" id="btnDeleteFile" class="btn btn-success mb-3" runat="server" onserverclick="DeleteFile_Click" style="display: none">Delete</button>
        <button type="submit" id="btnDeleteAuth" class="btn btn-success mb-3" runat="server" onserverclick="DeleteAuth_Click" style="display: none">Delete</button>

        <h3 class=" font-weight-bold"><i class="fa-solid fa-gears"></i>Configuration</h3>
        <div class="card shadow mb-4 border-left-secondary">
            <div class="card-header">
                <h5 class="font-weight-bold"><i class="bi bi-folder"></i>Document for Vendor</h5>
            </div>
            <div class="card-body">
                <button class="btn btn-success mb-3" type="button" data-toggle="modal" data-target="#uploadFile_Modal">Upload Files</button>
                <div class="table-responsive">
                    <table class="table table-hover border" id="Filetable" width="100%" cellspacing="0">
                        <thead>
                            <tr>
                                <th>Folder_Name.</th>
                                <th>File_Name</th>
                                <th></th>
                                <th></th>
                            </tr>
                        </thead>

                    </table>
                </div>
            </div>
        </div>
        <script src="Model/FileTable.js">
        </script>
    
        <%--       <script src="Model/FileTable.js"></script>--%>

        <div class="card shadow mb-4 border-left-secondary">
            <div class="card-header">
                <h5 class="font-weight-bold"><i class="bi bi-shield-lock"></i>Authorization</h5>
            </div>
            <div class="card-body">
                <button type="button" class="btn btn-success mb-3" data-toggle="modal" data-target="#AddAuthorize_Modal">Add Authorize</button>
                <div class="table-responsive">
                    <table class=" table table-hover border" id="UserTable" width="100%" cellspacing="0">
                        <thead>
                            <tr>
                                <th>Employee ID.</th>
                                <th>Name</th>
                                <th>Department</th>
                                <th>Position</th>
                                <th>Email</th>
                                <th>Phone</th>
                                <th>Update Date</th>
                                <th></th>
                                <th></th>
                                <th></th>
                                <th></th>
                            </tr>
                        </thead>

                    </table>
                </div>
            </div>
        </div>
        <script src="Model/UserTable.js"></script>
    </div>
    <script src="Scripts/PS/Config/Alert_Config.js"></script>
    <script src="Scripts/PS/Config/POST_Config.js"></script>
</asp:Content>
