<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="CreateStudent.aspx.cs" Inherits="DatabaseCommunicationViaWebApp.CreateStudent"
    MasterPageFile="~/MyLayout.Master" %>

<asp:Content ID="mainContent" runat="server" ContentPlaceHolderID="MainContentHolder">
    <form id="createStudentForm" runat="server" class="col-4">
        <div>
            <asp:Label ID="lblName" runat="server" Text="Name" CssClass="form-label"></asp:Label>
            <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="lblGender" runat="server" Text="Gender" CssClass="form-label"></asp:Label>
            <%--<asp:TextBox ID="txtGender" runat="server" CssClass="form-control"></asp:TextBox>--%>
            <asp:RadioButtonList ID="rblGender" runat="server"
                RepeatDirection="Horizontal" TextAlign="Left">
                <asp:ListItem Value="Male" Text="Male" CssClass="form-check-input"></asp:ListItem>
                <asp:ListItem Value="Female" Text="Female" CssClass="form-check-input"></asp:ListItem>
            </asp:RadioButtonList>
        </div>
        <div>
            <asp:Label ID="lblTrainer" runat="server" Text="Trainer" CssClass="form-label"></asp:Label>
            <%--<asp:TextBox ID="txtTrainer" runat="server" CssClass="form-control"></asp:TextBox>--%>
            <asp:DropDownList ID="ddlTrainers" runat="server" CssClass="form-select">
            </asp:DropDownList>
        </div>
        <div>
            <asp:Label ID="Label1" runat="server" Text="Upload Image" CssClass="form-label"></asp:Label>
            <asp:FileUpload ID="ImageFile" runat="server"
                CssClass="form-control" AllowMultiple="true"
                accept="image/*" />
        </div>
        <div>
            <asp:Button ID="btnCreate" runat="server" Text="Create" CssClass="btn btn-primary"
                OnClick="btnCreate_Click" OnClientClick="return confirm('You are about to create a new student, want to proceed?')" />
        </div>
        <div>
            <asp:Label ID="lblMessage" runat="server" CssClass="form-label" ForeColor="Red"></asp:Label>
        </div>
    </form>
</asp:Content>
