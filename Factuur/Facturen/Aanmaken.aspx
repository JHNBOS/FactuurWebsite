﻿<%@ Page Title="Factuur aanmaken" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Aanmaken.aspx.cs" Inherits="Factuur.Facturen.Aanmaken" MaintainScrollPositionOnPostBack="true"
 %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <h1 style="text-align: center;">Factuur aanmaken</h1>
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
                        

                        <asp:Table ID="SelectTable" GridLines="None" runat="server" CssClass="table table-hover" ViewStateMode="Enabled">
                            
                        </asp:Table>


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
