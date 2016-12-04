﻿<%@ Page Title="Factuur inzien" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inzien.aspx.cs" Inherits="Factuur.Facturen.Inzien" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <h2 style="text-align: center;">Factuur inzien</h2>
    <div class="row">
        <div class="col-sm-1"></div>
        <div class="col-sm-10">
            <h3></h3>

            <asp:Table ID="factuurTable" runat="server" CellPadding="5" CellSpacing="5" CssClass="table table-striped" Width="100%">
                <asp:TableHeaderRow HorizontalAlign="Center" VerticalAlign="Middle">
                    <asp:TableHeaderCell>Factuurnummer</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Factuurdatum</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Totaalbedrag</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Debiteur</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Product(en)</asp:TableHeaderCell>
                </asp:TableHeaderRow>
            </asp:Table>

            <br />
        </div>
        <div class="col-sm-1"></div>
    </div>
</div>

</asp:Content>
