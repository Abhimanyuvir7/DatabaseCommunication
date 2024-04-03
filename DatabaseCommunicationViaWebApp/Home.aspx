<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs"
    Inherits="DatabaseCommunicationViaWebApp.Home" MasterPageFile="~/MyLayout.Master" %>

<asp:Content ID="contentPage" runat="server" ContentPlaceHolderID="MainContentHolder">
    <form runat="server">
        <h2>Home Page Content</h2>
        <hr />
        <input type="text" id="Text1" runat="server" />
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <hr />
        <input type="button" value="Html Button" />
        <asp:Button ID="Button1" runat="server" Text="ASPX Button" OnClick="btnLoad_Click" />
    </form>
</asp:Content>
