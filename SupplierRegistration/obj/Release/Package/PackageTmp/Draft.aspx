<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Draft.aspx.cs" Inherits="SupplierRegistration.Draft" %>

<asp:Content ID="LogHead" ContentPlaceHolderID="head" runat="server">

    <%--<link href="DateTime/tempus-dominus.css" rel="stylesheet" />
    <script src="DateTime/Popper.js"></script>
    <script src="DateTime/tempus-dominus.js"></script>
    <script src="DateTime/Moment.js"></script>
    <script src="DateTime/Moment_Parse.js"></script>--%>
    <script src="Scripts/ShowLoading.js"></script>
</asp:Content>


<asp:Content ID="LogBody" ContentPlaceHolderID="Body" runat="server">


    <div class="container-fluid">
        <h1 class="h3 mb-2 text-gray-800">Supplier Detail</h1>
        <div class="row">
            <div class="col-lg-4 col-md-4 mb-4">
                <div class="card border-left-secondary shadow h-100 ">
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
                            <%--<div class="col-auto">
                                            <i class="fas fa-calendar fa-2x text-gray-300"></i>
                                        </div>--%>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Earnings (Monthly) Card Example -->
            <div class="col-lg-8 col-md-8 mb-4">
                <div class="card border-left-secondary shadow h-100 ">
                    <div class="card-body">
                        <!-- start step indicators -->
                        <div class="form-header d-flex mb-4">
                            <span class="stepIndicator finish">New Form</span>
                            <span class="stepIndicator active">Draft</span>
                            <span class="stepIndicator">On Process</span>
                            <span class="stepIndicator">Revise</span>
                            <span class="stepIndicator">Reject</span>
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
        <%--<form id="monthlyProcessForm" action="#!">--%>
        <!-- start step indicators -->
        <%--<div class="form-header d-flex mb-4">
                        <span class="stepIndicator finish">Import Warranty Claim</span>
                        <span class="stepIndicator finish" style="cursor:pointer;" onclick="onClickReview();">Maintenance & Review</span>
                        <span class="stepIndicator active" style="cursor:pointer;" onclick="onClickApprove();">Manager Approve</span>
                        <span class="stepIndicator" style="cursor:pointer;" onclick="onClickConfirm();">Supplier Confirm</span>
                        <span class="stepIndicator" style="cursor:pointer;" onclick="onClickReport();">Generate Office & Monthly Report</span>
                        <span class="stepIndicator" style="cursor:pointer;" onclick="onClickFinal();">Final Approve</span>
                    </div>--%>
        <!-- end step indicators -->
        <%--</form>--%>

        <%--  <div class="card shadow mb-4">
                <div class="card-header">Comment</div>
                <div class="card-body">
                    <p>lasdjflkasjdflkjasdlfjasdkjflsajdf</p>
                </div>
            </div>--%>
        <div class="card shadow mb-4 border-left-secondary">
            <div class="card-header py-3">
                <h4 class="m-0 font-weight-bold text-black-50">Form</h4>
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
                    <asp:RequiredFieldValidator ID="RequiredVendor_Name" runat="server" ControlToValidate="Vendor_Name" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="Import" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    <input type="text" class="form-control" id="Vendor_Name" runat="server" aria-describedby="VendorName" placeholder="Please enter your company name.">
                </div>

                <div class="mb-3">
                    <label for="InputPIC" class="form-label">Vendor PIC</label>
                    <asp:RequiredFieldValidator ID="RequiredVendor_PIC" runat="server" ControlToValidate="Vendor_PIC" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="Import" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    <input type="text" class="form-control" id="Vendor_PIC" runat="server" aria-describedby="VendorPIC" placeholder="Please enter your name.">
                    <p style="color: red"><small>*กรณีที่มีชื่อมากกว่า 1 ให้ใส่เครื่องหมาย , หลังชื่อทุกครั้ง เช่น สมชาย มั่งมี, บุญมา บุญมี , เป็นต้น</small></p>

                </div>

                <div class="mb-3">
                    <label for="InputEmail" class="form-label">Email</label>
                    <asp:RequiredFieldValidator ID="RequiredEmail" runat="server" ControlToValidate="Email" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="Import" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    <input type="text" class="form-control" id="Email" runat="server" placeholder="Please enter your email.">
                    <p style="color: red"><small>*กรณีที่มี E-mail มากกว่า 1 ให้ใส่เครื่องหมาย ; หลัง E-mail ทุกครั้ง เช่น Test@hotmail.com; Done@gmail.com; เป็นต้น</small></p>
                </div>

                <div class="mb-3">
                    <label for="InputEmail" class="form-label">CC Email</label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="GM_Email" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="Import" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    <input type="text" class="form-control" id="GM_Email" runat="server" placeholder="abcd@example.com">
                </div>
                <!-- End Input-->
                <%--  <div class="mb-3 form-check">
    <input type="checkbox" class="form-check-input" id="exampleCheck1">
    <label class="form-check-label" for="exampleCheck1">Check me out</label>
  </div>--%>

                <button id="btnRequest" class="btn btn-success" validationgroup="Import" type="button" runat="server" onclick="return Submit('Import');"><b>Submit</b></button>
                <button id="btn_Submit" class="btn btn-success" type="submit" runat="server" style="display: none" validationgroup="Import" onserverclick="Submit_Click"><b>Request</b></button>
                <button id="btnDraft" class="btn btn-secondary"  type="button" runat="server" onclick="return Draft('Draft');"><b>Save Draft</b></button>
                <button id="btn_Draft" class="btn btn-warning" type="submit" runat="server" style="display: none"  onserverclick="Draft_Click"><b>Request</b></button>



            </div>
        </div>
        <div class="card shadow mb-4 border-left-secondary">
            <div class="card-header">
                <h4 class="m-0 font-weight-bold text-black-50">Transaction</h4>
            </div>
            <div class="card-body">
                <asp:Label ID="Transaction" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>
    <script src="Scripts/PS/Draft/POST_Draft.js"></script>
</asp:Content>
