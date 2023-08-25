<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PS_Login.aspx.cs" Inherits="SupplierRegistration.PS_Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Supplier Registration Login Page</title>

    <link href="Content/sb-admin-2.min.css" rel="stylesheet" />

    <link href="SweetAlert/SweetAlert.css" rel="stylesheet" />
    <script src="SweetAlert/SweetAlert.js"></script>
    <!-- Showloading -->
    <link href="Content/PS_loader.css" rel="stylesheet" />

    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    
    <script src="Scripts/PS/Login/Alert_PS_Login.js"></script>
    <script src="Scripts/PS/Login/POST_PS_Login.js"></script>
    <script src="Scripts/ShowLoading.js"></script>


</head>
<body style="background-image: url(Images/Image_Index.jpg); background-size: cover">
    <form runat="server" style="display: flex">
        <asp:HiddenField ID="LinkURL" runat="server" />

        <!----- Loader ------------>
        <div class="loader"></div>

        <div class="container animated--fade-in justify-content-center ">

            <!-- Outer Row -->
            <div class="row">
                <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12" style="padding-top: 8rem">

                    <div class="card o-hidden border-0 shadow-lg my-5">
                        <div class="card-body p-0">
                            <!-- Nested Row within Card Body -->
                            <div class="row">
                                <div class="col-lg-6 d-none d-lg-block bg-login-image" style="background-repeat: no-repeat; background-image: url(Images/logo.png)"></div>
                                <div class="col-lg-6">
                                    <div class="p-5">
                                        <div class="text-center">
                                            <h1 class="h4 text-gray-900 mb-4">Supplier Application</h1>
                                            <p class="mb-4">PS Login</p>
                                        </div>
                                        <hr />
                                        <!-- Input Colunm -->
                                        <div class="form-group">
                                            <asp:RequiredFieldValidator ID="RequiredLogin_ID" runat="server" ControlToValidate="Login_ID" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="Login" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            <input type="text" class="form-control form-control-user" id="Login_ID" runat="server" placeholder="Enter Employee ID" data-maxlength="8" oninput="this.value=this.value.slice(0,this.dataset.maxlength)" />
                                        </div>

                                        <div class="form-group">
                                            <!-- Validator -->
                                            <asp:RequiredFieldValidator ID="RequiredLogin_Password" runat="server" ControlToValidate="Login_Password" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="Login" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            <input type="password" class="form-control form-control-user"
                                                id="Login_Password" runat="server" placeholder="Enter Password" />
                                        </div>

                                        <!-- Button -->
                                        <button id="btnLogin" class="btn btn-success btn-block" type="button" validationgroup="Login" runat="server" onclick="return Login('Login');"><b>Login</b></button>
                                        <%--<asp:Button ID="Button1" runat="server" Text="Button" OnClick="Register_Click" />--%>
                                        <button id="btn_Login" class="btn btn-success " type="submit" runat="server" style="display: none" validationgroup="Login" onserverclick="Login_Click"><b>Login</b></button>
                                        <hr />
                                        <div class=" text-center">
                                            <a href="Vendor_Login.aspx" class="btn btn-primary btn-user btn-block font-weight-bold">Vendor</a>
                                        </div>





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

