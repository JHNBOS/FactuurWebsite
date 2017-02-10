<%@ Page Title="Factuur" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Export.aspx.cs" Inherits="Factuur.Export" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.2.61/jspdf.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/html2canvas/0.5.0-beta4/html2canvas.js"></script>


    <script src="https://code.jquery.com/jquery-3.1.1.min.js"></script>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=export.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><title>Factuur</title>');
            printWindow.document.write('<link rel="stylesheet" type="text/css" href="Content/Site.css">');
            printWindow.document.write('<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous">');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>
    <script type="text/javascript">
        function PrintPage() {
            var panel = document.getElementById("<%=export.ClientID %>");

            var str1 = '<html><head><title>Factuur</title>';
            var str2 = '<link rel="stylesheet" type="text/css" href="Content/Site.css">';
            var str3 = '<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous">';
            var str4 = '</head><body>';
            var str5 = panel.innerHTML;
            var str6 = '</body></html>';

            var res = str5;

            console.log(res);

            var hiddenField = document.getElementById('<%=htmlText.ClientID%>');
            hiddenField.value = res;

            if (hiddenField.value != res) {
                document.getElementById('<%= PDFButton.ClientID%>').click();
            }
        }
    </script>
    <script type="text/javascript">
        function PDF1() {

            var panel = document.getElementById("<%=export.ClientID %>");

            var str1 = '<html><head><title>Factuur</title>';
            var str2 = '<link rel="stylesheet" type="text/css" href="Content/Site.css">';
            var str3 = '<link rel="stylesheet"href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous">';
            var str4 = '</head><body>';
            var str5 = panel.innerHTML;
            var str6 = '</body></html>';

            var res = str1 + str2 + str3 + str4 + str5 + str6;

            var hiddenField = document.getElementById("<%=htmlText.ClientID%>");
            hiddenField.value = res;

            var pdf = new jsPDF('p', 'pt', 'a3');

            specialElementHandlers = {
                // element with id of "bypass" - jQuery style selector
                '#noprint': function (element, renderer) {
                    // true = "handled elsewhere, bypass text extraction"
                    return true;
                }
            };
            margins = {
                top: 0,
                left: 0,
                right: 0,
                bottom: 0
            };

            //source = document.getElementById("<%=htmlText.ClientID%>").innerHTML;
            var source = res;

            pdf.fromHTML(
                source, // HTML string or DOM elem ref.
                margins.left, // x coord
                margins.top, { // y coord
                    'width': pdf.internal.pageSize.width, // max width of content on PDF
                    'height': pdf.internal.pageSize.height,
                    'elementHandlers': specialElementHandlers
                }, function () {
                    //pdf.save('Test.pdf');
                });

            pdf.save('Factuur.pdf');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:HiddenField ID="htmlText" runat="server" />

    <div class="row">
        <div class="col-sm-3">
            <asp:Button ID="PDFButton" Text="Download PDF" OnClientClick="PDF1()//" runat="server" CssClass="btn btn-primary" UseSubmitBehavior="false" />
            <br />
            <br />
        </div>
        <div class="col-sm-5"></div>
        <div class="col-sm-3"></div>
    </div>


    <div class="exportContainer" id="export" runat="server">


        <!-- Factuur tekst en logo-->
        <div class="row">
            <div class="col-sm-3">
                <h1 style="text-align: left; font-weight: bold;">FACTUUR</h1>
            </div>
            <div class="col-sm-5"></div>
            <div class="col-sm-3">
                <h1>mijnFactuur</h1>
            </div>
        </div>

        <br />

        <!-- Bedrijf en debiteur adres info -->
        <div class="row">
            <div class="col-sm-3" style="vertical-align: bottom;">
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

        <!-- Factuur datum-->
        <div class="row">
            <div class="col-sm-3">
                <asp:Table ID="FactuurDatumTable" runat="server" BorderStyle="None">
                    <asp:TableRow>
                        <asp:TableCell CssClass="FactInfo"><b>Factuurnummer:</b></asp:TableCell>
                        <asp:TableCell CssClass="FactInfo">
                            <asp:Label ID="FactuurNummerLabel" runat="server" Text=""></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="FactInfo"><b>Factuurdatum:</b></asp:TableCell>
                        <asp:TableCell CssClass="FactInfo">
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

                <asp:Table ID="FactuurTable" runat="server" CssClass="table table-striped" Width="78%">
                    <asp:TableHeaderRow HorizontalAlign="Center" VerticalAlign="Middle">
                        <asp:TableHeaderCell>Product</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Prijs</asp:TableHeaderCell>
                        <asp:TableHeaderCell>BTW</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Korting</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Aantal</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Totaal</asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                </asp:Table>


            </div>
        </div>

        <br />
        <br />
        <br />


        <div class="row">
            <div class="col-sm-3"></div>
            <div class="col-sm-5"></div>
            <div class="col-sm-3">
                <asp:Table ID="TotalTable" BorderStyle="None" runat="server">
                    <asp:TableRow>
                        <asp:TableCell>Subtotaal</asp:TableCell>
                        <asp:TableCell>
                            <asp:Label ID="SubtotaalLabel" runat="server" Text=""></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>BTW</asp:TableCell>
                        <asp:TableCell>
                            <asp:Label ID="BTWLabel" runat="server" Text=""></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>Totaal</asp:TableCell>
                        <asp:TableCell>
                            <asp:Label ID="TotaalLabel" runat="server" Text=""></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </div>
        </div>



    </div>
</asp:Content>
