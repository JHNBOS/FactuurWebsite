<%@ Page Title="Factuur" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Export.aspx.cs" Inherits="Factuur.Export" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="export-container">


        <!-- Factuur tekst en logo-->
        <div class="row">
        <div class="col-sm-3"><h1 style="text-align: left;font-weight:bold;">FACTUUR</h1>
        </div>
        <div class="col-sm-5"></div>
        <div class="col-sm-3">
            <h1>mijnFactuur</h1>
        </div>
    </div>

    <br />

    <!-- Bedrijf en debiteur adres info -->
    <div class="row">
        <div class="col-sm-3" style="vertical-align:bottom;">
            <asp:Table ID="DebTable" runat="server" BorderStyle="None">
                <asp:TableRow>
                    <asp:TableCell CssClass="ClientInfo">
                        <asp:Label ID="DebiteurNaamLabel" runat="server" Text=""></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                    <asp:TableRow CssClass="ClientInfo">
                        <asp:TableCell>
                            <asp:Label ID="DebiteurAdresLabel" runat="server" Text=""></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow CssClass="ClientInfo">
                        <asp:TableCell>
                            <asp:Label ID="DebiteurPostcodeLabel" runat="server" Text=""></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
            </asp:Table>
        </div>
        <div class="col-sm-5"></div>
        <div class="col-sm-3">
            <asp:Table ID="addressTable" runat="server" BorderStyle="None">
                <asp:TableRow>
                    <asp:TableCell CssClass="addressInfo"><b>vestigingsadres:</b></asp:TableCell>
                    <asp:TableCell>Gouvernestraat 85B <br /> 3014 PK ROTTERDAM</asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell CssClass="addressInfo"><b>postadres:</b></asp:TableCell>
                    <asp:TableCell>Postbus XXX <br /> 3014 PK ROTTERDAM</asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell CssClass="addressInfo"><b>telefoon:</b></asp:TableCell>
                    <asp:TableCell>0900 1234</asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell CssClass="addressInfo"><b>fax:</b></asp:TableCell>
                    <asp:TableCell>0900 4321</asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell CssClass="addressInfo"><b>email:</b></asp:TableCell>
                    <asp:TableCell>bedrijf@bedrijf.nl</asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell CssClass="addressInfo"><b>internet:</b></asp:TableCell>
                    <asp:TableCell>www.bedrijf.nl</asp:TableCell>
                </asp:TableRow>

            </asp:Table>
        </div>
    </div>
           
    <br />
    <br />
         
    <!-- Spacer -->
    <div class="row">
        <div class="col-sm-3"></div>
        <div class="col-sm-5"></div>
        <div class="col-sm-3"></div>
    </div>

    <br />
    <br />

    <!-- Inhoud factuur -->
    <div class="row">
        <div class="col-sm-3">
            <asp:Table ID="FactuurDatumTable" runat="server" BorderStyle="None">
                <asp:TableRow>
                    <asp:TableCell CssClass="FactInfo"><b>Factuurnummer:</b></asp:TableCell>
                    <asp:TableCell CssClass="FactInfo">
                        &emsp;
                        <asp:Label ID="FactuurNummerLabel" runat="server" Text=""></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="FactInfo"><b>Factuurdatum:</b></asp:TableCell>
                        <asp:TableCell CssClass="FactInfo">
                            &emsp;
                            <asp:Label ID="FactuurDatumLabel" runat="server" Text=""></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
            </asp:Table>
        </div>
        <div class="col-sm-5"></div>
        <div class="col-sm-3"></div>
    </div>


    <br />
    <br />
         
    <!-- Factuur inhoud -->
    <div class="row">
        <div class="col-sm-12">
            <p>Hierbij brengen wij u in rekening:</p>

            <br />

            <asp:Table ID="FactuurTable" runat="server" CssClass="table table-striped" Width="100%">
                <asp:TableHeaderRow HorizontalAlign="Center" VerticalAlign="Middle">
                    <asp:TableHeaderCell>Product</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Prijs</asp:TableHeaderCell>
                    <asp:TableHeaderCell>BTW</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Korting</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Totaal</asp:TableHeaderCell>
                </asp:TableHeaderRow>
            </asp:Table>


        </div>
    </div>



</div>
</asp:Content>
