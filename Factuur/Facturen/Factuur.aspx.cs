using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Factuur.Facturen
{
    public partial class Factuur : System.Web.UI.Page
    {
        FactuurDB db = new FactuurDB();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Event handlers
            CreateButton.Click += CreateButton_Click;

            //Execute methods
            FillGridView();
        }

        //Fill GridView of listed debiteuren.
        private void FillGridView()
        {
            try
            {
                List<facturen> factuurlist = db.facturen.ToList();

                for (int i = 0; i < factuurlist.Count; i++)
                {
                    string fnummer = factuurlist[i].Factuurnummer.ToString();
                    string fdatum = String.Format("{0:dd-MM-yyyy}", factuurlist[i].Factuurdatum);
                    string totaal = String.Format("{0:C}", factuurlist[i].Totaalbedrag);

                    debiteuren deb = db.debiteuren.Find(factuurlist[i].DebiteurID);

                    string debiteur = deb.Voornaam + " " + deb.Achternaam;

                    Button del = new Button();
                    del.ID = "del_" + factuurlist[i].Factuurnummer.ToString();
                    del.Text = "Verwijder";
                    del.CssClass = "btn btn-warning btn-sm";
                    del.Click += Del_Click;
                    del.OnClientClick = "Confirm()";


                    /*
                    Button edit = new Button();
                    edit.ID = "edit_" + factuurlist[i].Factuurnummer.ToString();
                    edit.Text = "Wijzigen";
                    edit.CssClass = "btn btn-success btn-sm";
                    edit.Click += Edit_Click;
                    */

                    Button export = new Button();
                    export.ID = "export_" + factuurlist[i].Factuurnummer.ToString();
                    export.Text = "Exporteer";
                    export.CssClass = "btn btn-danger btn sm";
                    export.Click += Export_Click;

                    Button show = new Button();
                    show.ID = "show_" + factuurlist[i].Factuurnummer.ToString();
                    show.Text = "Inzien";
                    show.CssClass = "btn btn-info btn-sm";
                    show.Click += Show_Click;

                    TableCell cell = new TableCell();
                    TableCell cell1 = new TableCell();
                    TableCell cell2 = new TableCell();
                    TableCell cell3 = new TableCell();
                    TableCell cell4 = new TableCell();
                    TableCell cell5 = new TableCell();
                    TableCell cell6 = new TableCell();

                    cell.Text = fnummer;
                    cell1.Text = fdatum;
                    cell2.Text = totaal;
                    cell3.Text = debiteur;
                    cell4.Controls.Add(show);
                    cell5.Controls.Add(export);
                    cell6.Controls.Add(del);

                    TableRow row = new TableRow();

                    row.Cells.Add(cell);
                    row.Cells.Add(cell1);
                    row.Cells.Add(cell2);
                    row.Cells.Add(cell3);
                    row.Cells.Add(cell4);
                    row.Cells.Add(cell5);
                    row.Cells.Add(cell6);

                    factuurTable.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }

        }

        private void Export_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string id = btn.ID;

            string[] split = id.Split('_');
            int ID = int.Parse(split[1]);

            Session["Export"] = ID;
            Response.Redirect("../Export.aspx");
        }

        private void Show_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string id = btn.ID;

            string[] split = id.Split('_');
            int ID = int.Parse(split[1]);

            Session["Show"] = ID;
            Response.Redirect("Inzien.aspx");
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string id = btn.ID;

            string[] split = id.Split('_');
            int ID = int.Parse(split[1]);

            Session["EditID3"] = ID;
            Response.Redirect("Wijzigen.aspx");
        }

        private void Del_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string id = btn.ID;

            string[] split = id.Split('_');
            int ID = int.Parse(split[1]);

            string confirmValue = Request.Form["confirm_value"];

            if (confirmValue == "Ja")
            {
                try
                {
                    facturen fact = db.facturen.Where(d => d.Factuurnummer == ID).SingleOrDefault();

                    List<factuur_items> itemList = db.factuur_items.Where(f => f.FactuurID == ID).ToList();

                    db.factuur_items.RemoveRange(itemList);
                    db.facturen.Remove(fact);
                    db.SaveChanges();

                    Message m = new Message();
                    m.Show("Factuur is verwijderd!");

                    Response.Redirect(Request.RawUrl);
                }
                catch (Exception ex)
                {
                    Message m = new Message();
                    m.Show("Factuur kon niet worden verwijderd!");
                    System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                }
            }
            else
            {
                Response.Redirect(Request.RawUrl);
            }
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Aanmaken.aspx");
        }
    }
}