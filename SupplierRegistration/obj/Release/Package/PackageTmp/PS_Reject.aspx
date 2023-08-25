<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="PS_Reject.aspx.cs" Inherits="SupplierRegistration.PS_Reject" %>

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
    <!-- Draft Start -->
    <div class="container-fluid animated--grow-in">
        <h1 class="h3 mb-2 text-gray-800 font-weight-bold">Supplier Detail</h1>
        <div class="row">
            <div class="col-lg-4 col-md-4 mb-4">
                <div class="card border-left-danger shadow h-100 ">
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
                            <div class="col-auto">
                                <i class="fas fa-calendar fa-2x text-gray-300"></i>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Earnings (Monthly) Card Example -->
            <div class="col-lg-8 col-md-8 mb-4">
                <div class="card border-left-danger shadow h-100 ">
                    <div class="card-body">
                        <!-- start step indicators -->
                        <div class="form-header d-flex mb-4">
                            <span class="stepIndicator finish">New Form</span>
                            <span class="stepIndicator finish">Draft</span>
                            <span class="stepIndicator finish">On Process</span>
                            <span class="stepIndicator finish">Revise</span>
                            <span class="stepIndicator active">Reject</span>
                            <span class="stepIndicator">Finish</span>
                        </div>
                        <!-- end step indicators -->
                        <%--<ul class="steps">
                                          <li class="step step-success">
                                            <div class="step-content">
                                              <span class="step-circle">1</span>
                                              <span class="step-text">New Form</span>
                                            </div>
                                          </li>
                                          <li class="step step-active">
                                            <div class="step-content">
                                              <span class="step-circle">2</span>
                                              <span class="step-text">Draft</span>
                                            </div>
                                          </li>
                                          <li class="step">
                                            <div class="step-content">
                                              <span class="step-circle">3</span>
                                              <span class="step-text">On Process </span>
                                            </div>
                                          </li>
                                          <li class="step">
                                            <div class="step-content">
                                              <span class="step-circle">4</span>
                                              <span class="step-text">Revise</span>
                                            </div>
                                          </li>
                                           <li class="step">
                                            <div class="step-content">
                                              <span class="step-circle">5</span>
                                              <span class="step-text">Reject</span>
                                            </div>
                                          </li>   
                                          <li class="step">
                                            <div class="step-content">
                                              <span class="step-circle">6</span>
                                              <span class="step-text">Finish</span>
                                            </div>
                                          </li>
                                        </ul>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="container-fluid animated--grow-in">
        <div class="card border-left-danger shadow h-100 mb-4">
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

    <div class="container-fluid animated--grow-in" id="cardUpload" style="display: none">
        <div class="card shadow mb-3 border-left-danger">
            <div class="card-body">
                <div class="text-center">
                    <i class="fa-solid fa-circle-xmark fa-2xl" style="color: red;"></i>
                    <h3>File Uploaded</h3>
                    <button class="btn btn-primary btn-icon-split " type="button" data-toggle="collapse" data-target="#collapseTable12" aria-expanded="false" aria-controls="collapseTable12">
                        <span class="icon text-white-50"><i class="fa-solid fa-magnifying-glass"></i></span>
                        <span class="text">Show Files Upload</span>

                    </button>
                    <div class="collapse" id="collapseTable12">
                        <%--Data Table--%>
                        <table id="FileReject" class="table table-hover bg-light text-left" style="width: 100%;">
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
    <div class="container-fluid animated--grow-in">
        <div class="card shadow mb-4 border-left-danger">
            <div class="card-header py-3">
                <h6 class="h5 m-0 font-weight-bold text-danger">Reject Form</h6>

            </div>
            <div class="card-body">
                <%--<label for="InputName" class="form-label">App ID</label>
                <div class=" border form-control">
                <asp:Label ID="App_ID" runat="server" Text=""></asp:Label>
                </div>
                
                <label for="InputName" class="form-label">Status</label>
                <div class=" border form-control">
                <asp:Label ID="Status" runat="server" Text=""></asp:Label>
                </div>--%>

                <!-- Input textbox-->
                <div class="mb-3">
                    <label for="InputName" class="form-label">Vendor Name</label>
                    <!-- Required Validator Function -->
                    <%--                            <asp:Label ID="Vendor_Name" runat="server" Text=""></asp:Label>--%>
                    <input class="form-control" id="Vendor_Name" runat="server" readonly />
                </div>
                <div class="mb-3">
                    <label for="InputPIC" class="form-label">Vendor PIC</label>
                    <input class="form-control" id="Vendor_PIC" runat="server" readonly />
                    <%--                            <asp:Label ID="Vendor_PIC" runat="server" Text=""></asp:Label>--%>
                </div>

                <div class="mb-3">
                    <label for="InputEmail" class="form-label">Email</label>
                    <input class="form-control" id="Email" runat="server" readonly />
                    <%--                            <asp:Label ID="Email" runat="server" Text=""></asp:Label>--%>
                </div>
                <div class="mb-3">
                    <label for="InputGM" class="form-label">CC Email</label>
                    <input class="form-control" id="GmMail" runat="server" readonly />
                </div>
            </div>
            <!-- End Input-->
            <%--  <div class="mb-3 form-check">
    <input type="checkbox" class="form-check-input" id="exampleCheck1">
    <label class="form-check-label" for="exampleCheck1">Check me out</label>
  </div>--%>
        </div>
    </div>
    <div class=" container-fluid animated--grow-in">
        <div class="card shadow mb-4 border-left-danger">
            <div class="card-header">
                <h4 class="m-0 font-weight-bold text-danger">Transaction</h4>
            </div>
            <div class="card-body">
                <asp:Label ID="Transaction" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>
    <script src="Model/RejectTable.js"></script>


</asp:Content>



