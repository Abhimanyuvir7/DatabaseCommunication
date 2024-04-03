<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrainerDashboard.aspx.cs"
    Inherits="DatabaseCommunicationViaWebApp.TrainerDashboard"
    MasterPageFile="~/MyLayout.Master" %>



<asp:Content ID="pageContent" runat="server" ContentPlaceHolderID="MainContentHolder">
    <div class="container">
        <div class="row">
            <div class="col-8">
                <form runat="server">
                    <h2>All Trainers</h2>

                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                        OnRowEditing="GridView1_RowEditing" OnRowCancelingEdit="GridView1_RowCancelingEdit"
                        OnRowUpdating="GridView1_RowUpdating" OnRowDeleting="GridView1_RowDeleting" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="Trainer ID" ReadOnly="true" />
                            <asp:BoundField DataField="Name" HeaderText="Name" />
                            <asp:BoundField DataField="City" HeaderText="City" />
                            <asp:BoundField DataField="Experience" HeaderText="Experience" />
                            <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                        </Columns>
                        <EditRowStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#E3EAEB" />
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F8FAFA" />
                        <SortedAscendingHeaderStyle BackColor="#246B61" />
                        <SortedDescendingCellStyle BackColor="#D4DFE1" />
                        <SortedDescendingHeaderStyle BackColor="#15524A" />
                    </asp:GridView>

                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                </form>
            </div>
            <%--<div class="col-4 border border-3 border-danger">
                <MUC:Advertisement ID="Ads" runat="server" />
            </div>--%>
        </div>
    </div>
</asp:Content>
