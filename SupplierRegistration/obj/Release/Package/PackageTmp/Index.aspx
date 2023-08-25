<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="SupplierRegistration.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <%--<meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0, shrink-to-fit=no" name="viewport">--%>
    <meta charset="utf-8" />
    <title>Supplier Application Index</title>
    <link href="Content/sb-admin-2.min.css" rel="stylesheet" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />
</head>
<body style="background-image: url(Images/Image_Index.jpg); background-size: cover">

    <div class="container justify-content-center align-content-center" style="margin-top: 8rem">
        <!-- Outer Row -->
        <div class="row justify-content-center">
            <div class="col-xl-10 col-lg-12 col-md-9">
                <div class="card o-hidden border-0 shadow-lg my-5">
                    <div class="card-body p-0">
                        <!-- Nested Row within Card Body -->
                        <div class="row flex-grow-1">
                            <div class="col-lg-6 d-none d-lg-block bg-login-image " style="background-image: url(Images/logo.png);">
                            </div>
                            <div class="col-lg-6" style="padding-top: 10%">
                                <div class="p-5">
                                    <div class="text-center">
                                        <h1 class="h4 text-gray-900 mb-2">Supplier Application</h1>
                                        <p class="mb-4">Please select</p>
                                    </div>
                                    <div class=" m-5">
                                        <a href="PS_Login.aspx" class="btn btn-primary btn-user btn-block">PS
                                        </a>
                                        <hr />
                                        <a href="Vendor_Login.aspx" class="btn btn-success btn-user btn-block">Vendor</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>

        </div>
    </div>
    <!-- Bootstrap core JavaScript-->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <script src="Scripts/bootstrap.bundle.min.js"></script>

    <!-- Core plugin JavaScript-->
    <script src="Scripts/jquery.easing.min.js"></script>
    <!-- Custom scripts for all pages-->
    <script src="Scripts/sb-admin-2.min.js"></script>

</body>
</html>
