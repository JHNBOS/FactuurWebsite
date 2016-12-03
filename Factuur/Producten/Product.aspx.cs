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

                    cell.Text = naam;
                    cell1.Text = prijs.ToString();
                    cell2.Text = btw.ToString();
                    cell3.Text = korting.ToString();
                    cell4.Controls.Add(edit);
                    cell5.Controls.Add(del);

                    TableRow row = new TableRow();

                    row.Cells.Add(cell);
                    row.Cells.Add(cell1);
                    row.Cells.Add(cell2);
                    row.Cells.Add(cell3);
                    row.Cells.Add(cell4);
                    row.Cells.Add(cell5);

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

            producten prod = db.producten.Where(d => d.ID == ID).SingleOrDefault();

            db.producten.Remove(prod);
            db.SaveChanges();

            Response.Redirect(Request.RawUrl);
        }


        //Create button for new product
        private void CreateButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Aanmaken.aspx");
        }



    }
}