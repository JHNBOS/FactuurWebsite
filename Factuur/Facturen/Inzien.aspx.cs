using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Factuur.Facturen
{
    public partial class Inzien : System.Web.UI.Page
    {
        FactuurDB db = new FactuurDB();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Get ID debiteur
            string id = Session["Show"].ToString();

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

            int debID = factuur.Debiteur;
            debiteuren deb = db.debiteuren.Find(debID);

            List<factuur_items> fiList = db.factuur_items.Where(f => f.FactuurID == ID).ToList();
            List<producten> productList = new List<producten>();
            List<int> pidList = new List<int>();

            for (int i = 0; i < fiList.Count; i++)
            {
                int productID = fiList[i].ProductID;
                pidList.Add(productID);
            }

            for (int i = 0; i < pidList.Count; i++)
            {
                int pid = pidList[i];
                producten p = db.producten.Find(pid);

                productList.Add(p);
            }

            TableCell cell = new TableCell();
            TableCell cell1 = new TableCell();
            TableCell cell2 = new TableCell();
            TableCell cell3 = new TableCell();
            TableCell cell4 = new TableCell();

            cell.Text = factuur.Factuurnummer.ToString();
            cell1.Text = String.Format("{0:dd-MM-yyyy}", factuur.Factuurdatum);
            cell2.Text = factuur.Totaalbedrag.ToString();
            cell3.Text = deb.Voornaam + " " + deb.Achternaam;

            string products = "";

            for (int i = 0; i < productList.Count; i++)
            {
                products += productList[i].Naam + "<br />";
            }

            cell4.Text = products;

            TableRow row = new TableRow();

            row.Cells.Add(cell);
            row.Cells.Add(cell1);
            row.Cells.Add(cell2);
            row.Cells.Add(cell3);
            row.Cells.Add(cell4);

            factuurTable.Rows.Add(row);

        }







    }
}