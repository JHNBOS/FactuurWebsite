﻿<%@ Page Title="Product aanmaken" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Aanmaken.aspx.cs" Inherits="Factuur.Producten.Aanmaken" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <h1 style="text-align: center;">Product aanmaken</h1>
    <div class="row">
        <div class="col-sm-1"></div>
        <div class="col-sm-10">
            <h3></h3>

            <a href="Product.aspx" class="btn btn-link">Terug naar overzicht producten</a>

            <br />

            <asp:Table ID="CreateTable" runat="server" GridLines="None" CssClass="table table-hover">
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Right"><asp:Label ID="Label1" runat="server" Text="Naam" /></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="naamBox" runat="server" CssClass="form-control" /></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Right"><asp:Label ID="Label2" runat="server" Text="Prijs" /></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="prijsBox" runat="server" CssClass="form-control" /></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Right"><asp:Label ID="Label3" runat="server" Text="BTW" /></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="btwBox"  runat="server" TextMode="Number" Width="280" CssClass="form-control" /></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Right"><asp:Label ID="Label4" runat="server" Text="Korting" /></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="kortingBox" runat="server" TextMode="Number" Width="280" CssClass="form-control" /></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Right"><asp:Label ID="Label5" runat="server" Text="Toewijzen aan debiteur" /></asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList ID="DebDDL" runat="server" Width="280" CssClass="form-control"></asp:DropDownList></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell></asp:TableCell>
                    <asp:TableCell>
                        <br />
                        <asp:Button ID="CreateButton" runat="server" CssClass="btn btn-primary" 
                            Text="Aanmaken" Width="120" /></asp:TableCell>
                </asp:TableRow>
            </asp:Table>

            
        </div>
        <div class="col-sm-1"></div>
    </div>
</div>

</asp:Content>
