<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PS_ResetPassword.aspx.cs" Inherits="SupplierRegistration.PS_ResetPassword" %>


<%--<link href="DateTime/tempus-dominus.css" rel="stylesheet" />
    <script src="DateTime/Popper.js"></script>
    <script src="DateTime/tempus-dominus.js"></script>
    <script src="DateTime/Moment.js"></script>
    <script src="DateTime/Moment_Parse.js"></script>--%>

<html>
<head>
    <title>Supplier Registration Reset Password Page</title>
    <link href="Content/sb-admin-2.min.css" rel="stylesheet" />

    <link href="SweetAlert/SweetAlert.css" rel="stylesheet" />
    <script src="SweetAlert/SweetAlert.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>

    <script>
        <!-- Function Submit Button -->
        function oReset(type, title, text) {
        swal.fire({
            icon: type,
            title: title,
            text: text,
            timer: 5000
        }).then(function () {
            window.location.href = "PS_Login.aspx"
        });
    }
    function oAlert(type, title, text) {
        swal.fire({
            icon: type,
            title: title,
            text: text,
            timer: 5000
        });
    }

    </script>

</head>
<body style="background-image: url(Images/Image_Index.jpg); background-size: cover">
    <form runat="server">
        <asp:HiddenField id="hdfCheck" runat="server"/>
        <asp:HiddenField ID="hdfID" runat="server" />
        <asp:HiddenField ID="hdfToken" runat="server" />
        <asp:HiddenField ID="HiddenField2" runat="server" />
        <!-- ScriptManager -->
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <script>
            function Reset(valid) {
                console.log(valid)
                if (Page_ClientValidate(valid)) {
                    swal.fire({
                        icon: 'question',
                        title: 'Do you want to Reset Your Password ?',
                        text: '',
                        showCancelButton: true,
                        confirmButtonColor: '#3085d6',
                        cancelButtonColor: '#d33',
                        confirmButtonText: 'Yes',
                        cancelButtonText: 'No'
                    }).then(function (result) {
                        console.log(result)
                        if (result.value) {
                            $("#btn_Reset").click();
                        }
                    });
                }
            }

        </script>

        <div class="container animated--fade-in">

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
                                            <p class="mb-4">PS Reset Password</p>
                                        </div>
                                        <hr />
                                        <!-- Input Form-->
                                        <%-- <asp:HiddenField ID="hdfRegisID" runat="server" />--%>


                                        <hr />
                                        <div class="form-group">
                                        <div class="animated--fade-in" id="collapseRegister" aria-hidden="true">
                                            <div class=" form-group">
                                                <asp:RequiredFieldValidator ID="RequiredRegister_ID" runat="server" ControlToValidate="User_ID" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="Reset" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <input type="text" class="form-control form-control-user" id="User_ID" placeholder="Enter User ID" runat="server" maxlength="8"/>
                                            </div>
                                            <div class="form-group">
                                                <asp:RequiredFieldValidator ID="RequiredPassword" runat="server" ControlToValidate="Register_Password" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="Reset" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <input type="password" class="form-control form-control-user" id="Register_Password" placeholder="Password" runat="server" />
                                            </div>
                                            <div class="form-group">
                                                <asp:RequiredFieldValidator ID="RequiredConfirmPassword" runat="server" ControlToValidate="ConfirmPassword" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="Reset" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <input type="password" class="form-control form-control-user" id="ConfirmPassword" placeholder="Confirm Password" runat="server" />
                                            </div>
                                            </div>
                                            <button id="btnReset" class="btn btn-success" type="button" runat="server" onclick="return Reset('Reset');"><b>Reset Password</b></button>
                                            <%--<asp:Button ID="Button1" runat="server" Text="Button" OnClick="Register_Click" />--%>
                                            <button id="btn_Reset" class="btn btn-success" type="submit" runat="server" style="display: none"  validationGroup="Reset" onserverclick="ResetPassword_Click"><b>Reset Password</b></button>
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
