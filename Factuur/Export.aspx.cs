using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.css;
using iTextSharp.tool.xml.html;
using iTextSharp.tool.xml.parser;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.pipeline.html;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.util.zlib;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.WebRequestMethods;

namespace Factuur
{
    public partial class Export : System.Web.UI.Page
    {
        FactuurDB db = new FactuurDB();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Get ID debiteur
            string id = Session["Export"].ToString();

            //Event handlers

            //Execute methods
            if (!IsPostBack)
            {
                ShowInfo(id);
            }
        }


        //Fill textboxes with info from selected debiteur
        private void ShowInfo(string id)
        {
            int ID = int.Parse(id);

            facturen factuur = db.facturen.Find(ID);

            int debID = factuur.DebiteurID;
            debiteuren deb = db.debiteuren.Find(debID);

            List<factuur_items> fiList = db.factuur_items.Where(f => f.FactuurID == ID).ToList();
            List<producten> productList = new List<producten>();
            List<int> pidList = new List<int>();
            List<int> aantalList = new List<int>();

            for (int i = 0; i < fiList.Count; i++)
            {
                int productID = fiList[i].ProductID;
                pidList.Add(productID);
            }

            for (int i = 0; i < fiList.Count; i++)
            {
                int aantal = fiList[i].Aantal;
                aantalList.Add(aantal);
            }

            for (int i = 0; i < pidList.Count; i++)
            {
                int pid = pidList[i];
                producten p = db.producten.Find(pid);

                productList.Add(p);
            }

            //Table cells
            TableCell cell = new TableCell();
            TableCell cell1 = new TableCell();
            TableCell cell2 = new TableCell();
            TableCell cell3 = new TableCell();
            TableCell cell4 = new TableCell();
            TableCell cell5 = new TableCell();

            //Set debiteur naam and address label
            DebiteurNaamLabel.Text = deb.Voornaam + " " + deb.Achternaam;
            DebiteurAdresLabel.Text = deb.Adres;

            if (deb.Land == "Nederland") {
                DebiteurPostcodeLabel.Text = deb.Postcode + " " + deb.Plaats;
            }
            else {
                DebiteurPostcodeLabel.Text = deb.Postcode + " " + deb.Plaats + " " + deb.Land;
            }

            //Set factuurnummer and date label
            FactuurNummerLabel.Text = factuur.Factuurnummer.ToString();
            FactuurDatumLabel.Text = String.Format("{0:dd-MM-yyyy}", factuur.Factuurdatum);

            //Set cell text
            for (int i = 0; i < productList.Count; i++)
            {
                producten pr = productList[i];

                string aantallen = "";

                for (int j = 0; j < aantalList.Count; j++)
                {
                    aantallen += aantalList[j] + "<br />";
                }

                cell.Text = pr.Naam;
                cell1.Text = String.Format("{0:C}", pr.Prijs);
                cell2.Text = pr.BTW + "%";
                cell3.Text = pr.Korting + "%";
                cell4.Text = aantallen;
                cell5.Text = String.Format("{0:C}", factuur.Totaalbedrag);

                TableRow row = new TableRow();

                row.Cells.Add(cell);
                row.Cells.Add(cell1);
                row.Cells.Add(cell2);
                row.Cells.Add(cell3);
                row.Cells.Add(cell4);
                row.Cells.Add(cell5);

                FactuurTable.Rows.Add(row);

            }

            FillTotal(productList, factuur);

        }



        private void FillTotal(List<producten> products, facturen factuur) {
            for (int i = 0; i < products.Count; i++)
            {
                producten pr = products[i];

                decimal? subtotal = (decimal?)(factuur.Totaalbedrag * 100) / (100 + pr.BTW);
                SubtotaalLabel.Text = String.Format("{0:C}", subtotal);
                BTWLabel.Text = pr.BTW + "%";
                TotaalLabel.Text = String.Format("{0:C}", factuur.Totaalbedrag);
            }
           
        }

    }
}