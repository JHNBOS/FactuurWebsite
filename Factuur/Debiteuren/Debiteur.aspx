<%@ Page Title="Debiteuren" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Debiteur.aspx.cs" Inherits="Factuur.Debiteur" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="container">
        <h1 style="text-align: center;">Debiteuren</h1>
    <div class="row">
        <div class="col-sm-1"></div>
        <div class="col-sm-10">
            <h3>Lijst van debiteuren</h3>

            <asp:Table ID="debTable" runat="server" CellPadding="5" CellSpacing="5" CssClass="table table-striped" Width="100%">
                <asp:TableHeaderRow HorizontalAlign="Center" VerticalAlign="Middle">
                    <asp:TableHeaderCell>Voornaam</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Achternaam</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Email</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Telefoon</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Adres</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Postcode</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Plaats</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Land</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Wijzigen</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Verwijderen</asp:TableHeaderCell>
                </asp:TableHeaderRow>
            </asp:Table>
            <asp:Button ID="CreateButton" runat="server" Text="Aanmaken" CssClass="btn btn-primary" />
        </div>
        <div class="col-sm-1"></div>
    </div>
</div>

</asp:Content>
