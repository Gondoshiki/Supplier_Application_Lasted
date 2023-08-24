<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PS_Register.aspx.cs" Inherits="SupplierRegistration.PS_Register" %>

<%--<link href="DateTime/tempus-dominus.css" rel="stylesheet" />
    <script src="DateTime/Popper.js"></script>
    <script src="DateTime/tempus-dominus.js"></script>
    <script src="DateTime/Moment.js"></script>
    <script src="DateTime/Moment_Parse.js"></script>--%>
<html>
<head>

    <title>Supplier Registration Register Page</title>
    <link href="Content/sb-admin-2.min.css" rel="stylesheet" />

    <link href="SweetAlert/SweetAlert.css" rel="stylesheet" />
    <script src="SweetAlert/SweetAlert.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>

    <script src="Scripts/ShowLoading.js"></script>
    <script src="Scripts/PS/Register/Alert_PS_Register.js"></script>
    <script src="Scripts/PS/Register/POST_PS_Register.js"></script>
</head>
<body style="background-image: url(Images/Image_Index.jpg); background-size: cover">
    <form runat="server">
        <asp:HiddenField ID="hdfToken" runat="server" />
        <asp:HiddenField ID="hdfID" runat="server" />
        <asp:HiddenField ID="hdfSS" runat="server" />
        <!-- ScriptManager -->
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <!----- Loader ------------>
        <div class="loader"></div>
        <div class="container animated--grow-in">

            <!-- Outer Row -->
            <div class="row justify-content-center">

                <div class="col-xl-10 col-lg-12 col-md-9" style="padding-top: 8rem">

                    <div class="card o-hidden border-0 shadow-lg my-5">
                        <div class="card-body p-0">


                            <!-- Nested Row within Card Body -->
                            <div class="row">
                                <div class="col-lg-6 d-none d-lg-block bg-login-image" style="background-image: url(Images/logo.png)"></div>
                                <div class="col-lg-6">
                                    <div class="p-5">
                                        <a class="close" href="Vendor_Login.aspx" style="color: black">&times;</a>
                                        <div class="text-center">
                                            <h1 class="h4 text-gray-900 mb-4">Supplier Application</h1>
                                            <p class="mb-4">PS Register</p>
                                        </div>
                                        <hr />

                                        <!-- Input Form-->
                                        <div class="collapse-header" id="ID_Check">
                                            <div class="form-group">
                                                <asp:RequiredFieldValidator ID="RequiredFieldRegister_ID" runat="server" ControlToValidate="Register_ID" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="Register" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <input type="text" class="form-control form-control-user" id="Register_ID" placeholder="Employee ID" runat="server" maxlength="8" />
                                            </div>

                                            <%--<div class="form-group">
                                                <input type="text" class="form-control form-control-user" id="Token_ID" placeholder="Token ID" runat="server" readonly />
                                            </div>--%>
                                            <div class=" form-group">
                                                <asp:RequiredFieldValidator ID="RequiredFieldPassword" runat="server" ControlToValidate="Password" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="Register" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <input type="password" class="form-control form-control-user" id="Password" placeholder="Password" runat="server" />
                                            </div>
                                            <div class=" form-group">
                                                <asp:RequiredFieldValidator ID="RequiredFieldPassword_Confirm" runat="server" ControlToValidate="Password_Confirm" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="Register" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <input type="password" class="form-control form-control-user" id="Password_Confirm" placeholder="Password" runat="server" />
                                            </div>
                                            <button id="btnRegister" class="btn btn-success" type="button" runat="server" onclick="return Register('Register');"><b>Register</b></button>
                                            <%--<asp:Button ID="Button1" runat="server" Text="Button" OnClick="Register_Click" />--%>
                                            <button id="btn_Register" class="btn btn-success" type="submit" runat="server" style="display: none" validationgroup="Register" onserverclick="Register_Click"><b>Register</b></button>
                                        </div>

                                        <%-- <asp:HiddenField ID="hdfRegisID" runat="server" />--%>

                                        <hr />
                                        <%--<div class="collapse animated--fade-in" id="collapseRegister" aria-hidden="true">
                                            <div class="form-group">
                                                <input type="text" class="form-control form-control-user" id="ID_Show" data-maxlength="8" oninput="this.value=this.value.slice(0,this.dataset.maxlength)" runat="server" readonly />
                                            </div>
                                            <div class=" form-group">
                                                <asp:RequiredFieldValidator ID="RequiredRegister_Password" runat="server" ControlToValidate="Register_Password" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="Register" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <input type="password" class="form-control form-control-user"
                                                    id="Register_Password" placeholder="Password" runat="server" />
                                            </div>
                                            <div class="form-group">
                                                <asp:RequiredFieldValidator ID="RequiredConfirmPassword" runat="server" ControlToValidate="ConfirmPassword" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="Register" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <input type="password" class="form-control form-control-user"
                                                    id="ConfirmPassword" placeholder="Comfirm Password" runat="server" />
                                            </div>
                                            <button id="btnRequest" class="btn btn-success" type="button" runat="server" onclick="return Register('Register');"><b>Register</b></button>
                                            <%--<asp:Button ID="Button1" runat="server" Text="Button" OnClick="Register_Click" />
                                          <button id="btn_Register" class="btn btn-success" type="submit" runat="server" style="display: none" onserverclick="Register_Click"><b>Register</b></button>
                                        </div>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>

            </div>

        </div>
    </form>
    <!-- Core plugin JavaScript-->
    <script src="Scripts/jquery.easing.min.js"></script>
</body>
</html>
