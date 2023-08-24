<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="PS_MyTask.aspx.cs" Inherits="SupplierRegistration.PS_MyTask" %>

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
    <div class="container-fluid animated--grow-in">
        <!-- Page Heading -->
        <div class="d-sm-flex align-items-center justify-content-between mb-4">
            <h3 class="mb-0 text-gray-800 font-weight-bold"><i class="bi bi-bar-chart"></i>Summary</h3>
        </div>

        <!-- Content Row -->
        <div class="row">

            <!-- Earnings (Monthly) Card Example -->
            <div class="col mb-4">
                <div class="card border-left-secondary shadow h-100 py-2">
                    <div class="card-body">
                        <div class="row no-gutters align-items-center">
                            <div class="col mr-2">
                                <div class="text-xs font-weight-bold text-secondary text-uppercase mb-1">
                                    Draft
                                </div>
                                <div class="h5 mb-0 font-weight-bold text-gray-800">
                                    <asp:Label ID="DraftC" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="col-auto">
                                <i class="fa-solid fa-file-lines fa-fade fa-2xl" style="color: #a1a1a1;"></i>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Earnings (Monthly) Card Example -->
            <div class="col mb-4">
                <div class="card border-left-primary shadow h-100 py-2">
                    <div class="card-body">
                        <div class="row no-gutters align-items-center">
                            <div class="col mr-2">
                                <div class="text-xs font-weight-bold text-primary text-uppercase mb-1">
                                    Process
                                </div>
                                <div class="h5 mb-0 font-weight-bold text-gray-800">
                                    <asp:Label ID="ProcessC" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="col-auto">
                                <i class="fa-solid fa-circle-notch fa-spin fa-2xl" style="color: #336fd7;"></i>
                            </div>
                        </div>
                    </div>
                </div>
            </div>            

            <!-- Earnings (Monthly) Card Example -->
            <div class="col mb-4">
                <div class="card border-left-warning shadow h-100 py-2">
                    <div class="card-body">
                        <div class="row no-gutters align-items-center">
                            <div class="col mr-2">
                                <div class="text-xs font-weight-bold text-warning text-uppercase mb-1">
                                    Revise
                                </div>
                                <div class="h5 mb-0 font-weight-bold text-gray-800">
                                    <asp:Label ID="ReviceC" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="col-auto">
                                <i class="fa-solid fa-triangle-exclamation fa-fade fa-2xl" style="color: orange;"></i>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div class="container-fluid animated--grow-in">
        <!-- Page Heading -->
        <h3 class="mb-2 text-gray-800 font-weight-bold"><i class="fa-solid fa-layer-group"></i> My Task List </h3>
        <%--<p class="mb-4">DataTables is a third party plugin that is used to generate the demo table below.
                        For more information about DataTables, please visit the 
                        <a target="_blank" href="https://datatables.net" class="text-danger">official DataTables documentation</a>.</p>--%>

        <!-- DataTales Example -->
        <div class="card shadow mb-4 border-left-primary">
            <div class="card-header py-3">
                <h4 class="m-0 font-weight-bold text-primary">Data</h4>
            </div>
            <div class="card-body">
                <div class="table-responsive"></div>
                <table class="table table-hover border" id="TaskTable" width="100%" cellspacing="0">
                    <thead>
                        <tr>
                            <th>APP_ID</th>
                            <th>Vendor_Name</th>
                            <th>Vendor_PIC</th>
                            <th>Email</th>
                            <th>Status</th>
                            <th>Date</th>
                        </tr>
                    </thead>


                </table>
            </div>
        </div>
    </div>
    <!-- Scroll to Top Button-->

    <a class=" scroll-to-top rounded" href="#page-top">
        <i class="fas fa-angle-up"></i>
    </a>
    <!-- Call .js -->
    <script src="Model/TaskTable.js"></script>
</asp:Content>
