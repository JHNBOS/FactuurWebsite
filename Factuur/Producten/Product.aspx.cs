using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Factuur.Producten
{
    public partial class Product : System.Web.UI.Page
    {
        FactuurDB db = new FactuurDB();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Event handlers
            CreateButton.Click += CreateButton_Click;

            //Execute methods
            FillGridView();
        }
         

        //Fill table with products.
        private void FillGridView()
        {
            try
            {
                List<producten> prodlist = db.producten.ToList();
                List<toewijzen> toewijzenList = db.toewijzen.ToList();

                for (int i = 0; i < prodlist.Count; i++)
                {
                    string naam = prodlist[i].Naam;
                    decimal prijs = (decimal) prodlist[i].Prijs;
                    int btw = (int) prodlist[i].BTW;
                    int korting = (int) prodlist[i].Korting;

                    Button del = new Button();
                    del.ID = "del_" + prodlist[i].ID.ToString();
                    del.Text = "Verwijder";
                    del.CssClass = "btn btn-warning btn-sm";
                    del.Click += Del_Click;
                    del.OnClientClick = "Confirm()";

                    Button edit = new Button();
                    edit.ID = "edit_" + prodlist[i].ID.ToString();
                    edit.Text = "Wijzigen";
                    edit.CssClass = "btn btn-success btn-sm";
                    edit.Click += Edit_Click;

                    TableCell cell = new TableCell();
                    TableCell cell1 = new TableCell();
                    TableCell cell2 = new TableCell();
                    TableCell cell3 = new TableCell();
                    TableCell cell4 = new TableCell();
                    TableCell cell5 = new TableCell();
                    TableCell cell6 = new TableCell();

                    int pid = prodlist[i].ID;

                    toewijzen tw = toewijzenList.Where(t => t.ProductID == pid).SingleOrDefault();

                    if (tw != null)
                    {
                        int id = tw.DebiteurID;
                        debiteuren deb = db.debiteuren.Find(id);

                        cell4.Text = deb.Voornaam + " " + deb.Achternaam;
                    }
                    else
                    {
                        cell4.Text = "";
                    }

                    cell.Text = naam;
                    cell1.Text = String.Format("{0:C}", prijs);
                    cell2.Text = String.Format("{0}%", btw);
                    cell3.Text = String.Format("{0}%", korting);
                    cell5.Controls.Add(edit);
                    cell6.Controls.Add(del);

                    TableRow row = new TableRow();

                    row.Cells.Add(cell);
                    row.Cells.Add(cell1);
                    row.Cells.Add(cell2);
                    row.Cells.Add(cell3);
                    row.Cells.Add(cell4);
                    row.Cells.Add(cell5);
                    row.Cells.Add(cell6);

                    productTable.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }

        }

        private void Edit_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string id = btn.ID;

            string[] split = id.Split('_');
            int ID = int.Parse(split[1]);

            Session["EditID2"] = ID;
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
                    producten prod = db.producten.Where(d => d.ID == ID).SingleOrDefault();
                    List<factuur_items> fiList = db.factuur_items.Where(f => f.ProductID == prod.ID).ToList();
                    List<facturen> factuurList = new List<facturen>();
                    List<toewijzen> toewijsList = db.toewijzen.Where(t => t.ProductID == ID).ToList();

                    for (int i = 0; i < fiList.Count; i++)
                    {
                        int fid = fiList[i].FactuurID;

                        facturen fact = db.facturen.Find(fid);
                        factuurList.Add(fact);
                    }

                    db.factuur_items.RemoveRange(fiList);
                    db.facturen.RemoveRange(factuurList);
                    db.toewijzen.RemoveRange(toewijsList);
                    db.producten.Remove(prod);
                    db.SaveChanges();

                    Message m = new Message();
                    m.Show("Product is verwijderd!");

                    Response.Redirect(Request.RawUrl);
                }
                catch (Exception ex)
                {
                    Message m = new Message();
                    m.Show("Product kon niet worden verwijderd!");
                    System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                }
            }
            else
            {
                Response.Redirect(Request.RawUrl);
            }

        }


        //Create button for new product
        private void CreateButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Aanmaken.aspx");
        }



    }
}