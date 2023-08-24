<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PS_BeforeResetPassword.aspx.cs" Inherits="SupplierRegistration.PS_BeforeResetPassword" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link href="Content/sb-admin-2.min.css" rel="stylesheet" />

    <link href="SweetAlert/SweetAlert.css" rel="stylesheet" />
    <script src="SweetAlert/SweetAlert.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>


    <script>
        <!-- Function Submit Button -->
        function oCheck(type, title, text) {
            swal.fire({
                icon: type,
                title: title,
                text: text,
                timer: 5000
            }).then(function () {
                //Redirect to Another Pages
                window.location.href = "PS_ResetPassword.aspx"
            });
        }
        function oCheckFail(type, title, text) {
            swal.fire({
                icon: type,
                title: title,
                text: text,
                timer: 5000
            }).then(function () {
                //Redirect to Another Pages
                 window.location.href = "Vendor_Login.aspx"
            });
        }
        function showloading() {
            swal.fire({
                title: 'Processing...',
                didOpen: () => {
                    swal.showLoading();
                },
                /*Text: 'Processing...',*/

                /*animation: 'circle'*/
            })

            return true;
            //if (Page_ClientValidate(valid)) {
            //    $('body').loadingModal({ animation: 'circle', text: 'Processing...' });

            //    return true;
            //}

            //return false;
        }
        function oAlert(type, title, text) {
            swal.fire({
                icon: type,
                title: title,
                text: text,
                timer: 5000
            }).then(function () {
                //Redirect to Another Pages
                /*window.location.href = "Vendor_Login.aspx"*/
            });
        }
    </script>

</head>
<body style="background-image:url(Images/Image_Index.jpg); background-size: cover">
    <form runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <script>
            function showloading() {
                swal.fire({
                    title: 'Processing...',
                    didOpen: () => {
                        swal.showLoading();
                    },               
                })
                return true;                
            }
            function CheckReset(valid) {
                console.log(valid)
                if (Page_ClientValidate(valid)) {
                    swal.fire({
                        icon: 'question',
                        title: 'Do you want to Login ?',
                        text: '',
                        showCancelButton: true,
                        confirmButtonColor: '#3085d6',
                        cancelButtonColor: '#d33',
                        confirmButtonText: 'Yes',
                        cancelButtonText: 'No'
                    }).then(function (result) {
                        console.log(result)
                        if (result.value) {
                            showloading();
                            $("#btn_CheckReset").click();
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
                                <div class="col-lg-6 d-none d-lg-block bg-login-image" style=" background-repeat:no-repeat; background-image: url(Images/logo.png)"></div>
                                <div class="col-lg-6">
                                    <div class="p-5">
                                        <a class="close" href="Vendor_Login.aspx" style="color: black">&times;</a>
                                        <div class="text-center">
                                            <h1 class="h4 text-gray-900 mb-4">Supplier Application</h1>
                                            <p class="mb-4">PS Check Before Reset Password</p>
                                        </div>
                                        <hr />
                                        <!-- Input Colunm -->
                                        <div class="form-group">
                                            <asp:RequiredFieldValidator ID="RequiredLogin_ID" runat="server" ControlToValidate="Login_ID" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="Check" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            <input type="text" class="form-control form-control-user"
                                                id="Login_ID" runat="server"
                                                placeholder="Enter Employee ID" data-maxlength="8" oninput="this.value=this.value.slice(0,this.dataset.maxlength)" />
                                        </div>

                                        <div class="form-group">
                                            <!-- Validator -->
                                            <asp:RequiredFieldValidator ID="RequiredLogin_Password" runat="server" ControlToValidate="Login_Password" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="Check" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            <input type="password" class="form-control form-control-user"
                                                id="Login_Password" runat="server" placeholder="Enter Password" />
                                        </div>


                                        <!-- Button -->
                                        <button id="btnCheckReset" class="btn btn-success" type="button" runat="server" onclick="return CheckReset('Check');"><b>Login</b></button>
                                        <%--<asp:Button ID="Button1" runat="server" Text="Button" OnClick="Register_Click" />--%>
                                        <button id="btn_CheckReset" class="btn btn-success" type="submit" runat="server" style="display: none" onserverclick="ResetCheck_Click"><b>Login</b></button>
                                        <hr />
                                 





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

