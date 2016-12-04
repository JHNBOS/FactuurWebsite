<%@ Page Title="Debiteur wijzigen" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Wijzigen.aspx.cs" Inherits="Factuur.Debiteuren.Wijzigen" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h2 style="text-align: center;">Debiteur wijzigen</h2>
    <div class="row">
        <div class="col-sm-1"></div>
        <div class="col-sm-10">
            <h3></h3>

            <a href="Debiteur.aspx" class="btn btn-link">Terug naar overzicht debiteuren</a>

            <br />

            <asp:Table ID="EditTable" runat="server" GridLines="None" CssClass="table table-hover">
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Right"><asp:Label ID="Label1" runat="server" Text="Voornaam" /></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="voornaamBox" runat="server" CssClass="form-control" /></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Right"><asp:Label ID="Label2" runat="server" Text="Achternaam" /></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="achternaamBox" runat="server" CssClass="form-control" /></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Right"><asp:Label ID="Label3" runat="server" Text="Email" /></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="emailBox"  runat="server" TextMode="Email" CssClass="form-control" /></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Right"><asp:Label ID="Label4" runat="server" Text="Telefoon" /></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="telefoonBox" runat="server" CssClass="form-control" /></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Right"><asp:Label ID="Label5" runat="server" Text="Adres" /></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="adresBox" runat="server" CssClass="form-control"  /></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Right"><asp:Label ID="Label6" runat="server" Text="Postcode" /></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="postcodeBox" runat="server" CssClass="form-control" /></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Right"><asp:Label ID="Label7" runat="server" Text="Plaats" /></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="plaatsBox" runat="server" CssClass="form-control" /></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Right"><asp:Label ID="Label8" runat="server" Text="Land" /></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="landBox"  runat="server" CssClass="form-control" /></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell></asp:TableCell>
                    <asp:TableCell>
                        <br />
                        <asp:Button ID="SaveButton" runat="server" CssClass="btn btn-primary" 
                            Text="Opslaan" Width="120" /></asp:TableCell>
                </asp:TableRow>
            </asp:Table>

            
        </div>
        <div class="col-sm-1"></div>
    </div>
</div>
</asp:Content>
