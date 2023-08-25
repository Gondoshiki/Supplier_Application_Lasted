<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="PS_Form.aspx.cs" Inherits="SupplierRegistration.PS_Form" %>

<asp:Content ID="LogHead" ContentPlaceHolderID="head" runat="server">

    <%--<link href="DateTime/tempus-dominus.css" rel="stylesheet" />
    <script src="DateTime/Popper.js"></script>
    <script src="DateTime/tempus-dominus.js"></script>
    <script src="DateTime/Moment.js"></script>
    <script src="DateTime/Moment_Parse.js"></script>--%>
    <script src="Scripts/ShowLoading.js"></script>
</asp:Content>


<asp:Content ID="LogBody" ContentPlaceHolderID="Body" runat="server">

    <!----------Show loading ---------------->
    <div class="loader"></div>
    <div class="container-fluid animated--grow-in">
        <h1 class="h3 mb-2 text-gray-800 font-weight-bold"><i class="fa-solid fa-file-circle-plus"></i>Supplier Form</h1>
        <div class="card shadow border-left-dark">
            <div class="card-header py-3">
                <h4 class="m-0 font-weight-bold text-danger">Form</h4>
            </div>
            <div class="card-body">
                <!-- Input textbox-->
                <div class="mb-3">
                    <label for="InputName" class="form-label">Vendor Name :</label>
                    <!-- Required Validator Function -->
                    <asp:RequiredFieldValidator ID="RequiredVendor_Name" runat="server" ControlToValidate="Vendor_Name" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="Import" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    <input type="text" class="form-control" id="Vendor_Name" runat="server" aria-describedby="VendorName" placeholder="Please enter company name.">
                    <%--<asp:RegularExpressionValidator display="Dynamic" ValidationGroup="Import" ID="RegularExpressionVendor_Name" runat="server"
                            ControlToValidate="Vendor_Name" ErrorMessage=" Enter only a-z ก-ฮ ๐-๙ A- Z 0-9 . _"
                            ValidationExpression="^[a-zA-Z0-9ก-๙,._ ]*$" Font-Bold="true" ForeColor="Red">
                        </asp:RegularExpressionValidator>--%>
                </div>

                <div class="mb-3">
                    <label for="InputPIC" class="form-label">Vendor PIC :</label>
                    <asp:RequiredFieldValidator ID="RequiredVendor_PIC" runat="server" ControlToValidate="Vendor_PIC" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="Import" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    <input type="text" class="form-control" id="Vendor_PIC" runat="server" aria-describedby="VendorPIC" placeholder="Please enter PIC name.">
                    <p style="color: red"><small>*กรณีที่มีชื่อมากกว่า 1 ให้ใส่เครื่องหมาย , หลังชื่อทุกครั้ง เช่น สมชาย มั่งมี, บุญมา บุญมี , เป็นต้น</small></p>
                    <%--<asp:RegularExpressionValidator display="Dynamic" ValidationGroup="Import" ID="RegularExpressionVendor_PIC" runat="server"
                            ControlToValidate="Vendor_PIC" ErrorMessage=" Enter only ก-ฮ a-z A- Z"
                            ValidationExpression="^[a-zA-Zก-ฮ,; ]*$" Font-Bold="true" ForeColor="Red">
                        </asp:RegularExpressionValidator>--%>
                </div>

                <div class="mb-3">
                    <label for="InputEmail" class="form-label">Email :</label>
                    <asp:RequiredFieldValidator ID="RequiredEmail" runat="server" ControlToValidate="Email" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="Import" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    <input type="email" class="form-control" id="Email" runat="server" placeholder="abcd@example.com">
                    <p style="color: red"><small>*กรณีที่มี E-mail มากกว่า 1 ให้ใส่เครื่องหมาย ; หลัง E-mail ทุกครั้ง เช่น Test@hotmail.com; Done@gmail.com; เป็นต้น</small></p>
                </div>
                <!-- End Input-->
                <div class="mb-3">
                    <label for="InputEmail" class="form-label">CC Email :</label>
                    <asp:RequiredFieldValidator ID="Required_GM_Email" runat="server" ControlToValidate="GM_Email" ErrorMessage="*" ForeColor="#ff0000" ValidationGroup="Import" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    <input type="email" class="form-control" id="GM_Email" runat="server" placeholder="abcd@example.com" />
                </div>
                <button id="btnRequest" class="btn btn-success" validationgroup="Import" type="button" runat="server" onclick="return Submit('Import');"><b>Submit</b></button>
                <button id="btn_Submit" class="btn btn-success" type="submit" runat="server" style="display: none" validationgroup="Import" onserverclick="Submit_Click"><b>Request</b></button>
                <button id="btnDraft" class="btn btn-secondary" type="button" runat="server" onclick="return Draft('Draft');"><b>Save Draft</b></button>
                <button id="btn_Draft" class="btn btn-warning" type="submit" runat="server" style="display: none" onserverclick="Draft_Click"><b>Request</b></button>
            </div>
        </div>
    </div>
    <script src="Scripts/PS/Form/POST_PS_Form.js"></script>
</asp:Content>
