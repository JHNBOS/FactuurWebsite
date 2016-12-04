﻿<%@ Page Title="Factuur aanmaken" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Aanmaken.aspx.cs" Inherits="Factuur.Facturen.Aanmaken" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <h2 style="text-align: center;">Factuur aanmaken</h2>
    <div class="row">
        <div class="col-sm-1"></div>
        <div class="col-sm-10">
            <h3></h3>

            <a href="Factuur.aspx" class="btn btn-link">Terug naar overzicht facturen</a>

            <br />

            <asp:Table ID="CreateTable" runat="server" GridLines="None" CssClass="table table-hover">
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Right"><asp:Label ID="Label1" runat="server" Text="Factuurdatum" /></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="datumBox" runat="server" TextMode="Date" Width="280" CssClass="form-control" /></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Right">
                        <asp:Label ID="Label2" runat="server" Text="Debiteur" />
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList ID="debDDL" runat="server"></asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Right"><asp:Label ID="Label3" runat="server" Text="Producten" /></asp:TableCell>
                    <asp:TableCell>
                        <asp:CheckBoxList ID="productCheckBoxList" AutoPostBack="true" OnSelectedIndexChanged="productCheckBoxList_SelectedIndexChanged1" runat="server"></asp:CheckBoxList>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Right"><asp:Label ID="Label4" runat="server" Text="Totaalbedrag" /></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="totaalBox" runat="server" Width="280" CssClass="form-control" /></asp:TableCell>
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
