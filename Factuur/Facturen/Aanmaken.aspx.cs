using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Factuur.Facturen
{
    public partial class Aanmaken : System.Web.UI.Page
    {
        FactuurDB db = new FactuurDB();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Event handlers
            CreateButton.Click += CreateButton_Click;

            //Set current date to datumBox
            datumBox.Text = String.Format("{0:yyyy-MM-dd}", DateTime.Now);

        }

        protected void Page_Init(object sender, EventArgs e)
        {
            FillDDL();
            FillGrid();
        }

        //Fill dropdownlist with debiteuren
        private void FillDDL()
        {
            List<debiteuren> debList = db.debiteuren.ToList();

            for (int i = 0; i < debList.Count; i++)
            {
                string voornaam = debList[i].Voornaam;
                string achternaam = debList[i].Achternaam;

                string naam = voornaam + " " + achternaam;

                debDDL.Items.Add(naam);
            }
        }
     

        //Fill checkboxlist with producten
        private void FillGrid()
        {
            List<producten> productList = db.producten.ToList();


            for (int i = 0; i < productList.Count; i++)
            {
                string pnaam = productList[i].Naam;

                CheckBox chk = new CheckBox();
                chk.ID = "chk_" + pnaam;
                chk.Text = pnaam;

                TextBox txtBox = new TextBox();
                txtBox.ID = "aantal_" + pnaam;
                txtBox.AutoPostBack = true;
                txtBox.TextMode = TextBoxMode.Number;
                txtBox.TextChanged += new EventHandler(TxtBox_TextChanged1);
                txtBox.PreRender += new EventHandler(TxtBox_PreRender);
                txtBox.CssClass = "form-control";
                txtBox.Text = "0";

                TableRow row = new TableRow();
                TableCell cell1 = new TableCell();
                TableCell cell2 = new TableCell();

                cell1.Controls.Add(chk);
                cell2.Controls.Add(txtBox);

                row.Cells.Add(cell1);
                row.Cells.Add(cell2);
            
                SelectTable.Rows.Add(row);
            }

        }

        private void TxtBox_TextChanged1(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("TextChanged fired up!");

            decimal total = 0;
            List<string> values = new List<string>();

            foreach (var tr in SelectTable.Controls.OfType<TableRow>())
            {
                foreach (var td in tr.Controls.OfType<TableCell>())
                {
                    foreach (var chk in td.Controls.OfType<CheckBox>())
                    {
                        if (chk.Checked)
                        {
                            values.Add(chk.Text);

                        }
                    }
                }
            }


            for (int i = 0; i < values.Count; i++)
            {
                string j = values[i];
                producten p = db.producten.Where(pr => pr.Naam == j).SingleOrDefault();

                string[] split = values[i].Split('_');
                TextBox txtBox = null;


                foreach (var tr in SelectTable.Controls.OfType<TableRow>())
                {
                    foreach (var td in tr.Controls.OfType<TableCell>())
                    {
                        foreach (var txt in td.Controls.OfType<TextBox>())
                        {
                            if (txt.ID.Contains(split[0]))
                            {
                                txtBox = txt;
                            }
                        }
                    }
                }

                if (txtBox != null)
                {
                    decimal num = decimal.Parse(txtBox.Text);
                    decimal price = (decimal)p.Prijs;
                    total += (price * num);
                }
                else
                {
                    total += 0;
                }
                
            }

            totaalBox.Text = String.Format("{0:C}", total);
        }

        private void TxtBox_PreRender(object sender, EventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            txtBox.Attributes["value"] = txtBox.Text;
        }


        private void CreateFactuur()
        {
            List<int> aantalList = new List<int>();
            facturen factuur = new facturen();
            factuur.Factuurdatum = DateTime.Parse(datumBox.Text);

            //---------------------------------
            //Debiteur
            string debiteur = debDDL.SelectedValue;
            string[] name = debiteur.Split(' ');
            System.Diagnostics.Debug.WriteLine("fname: " + name[0]);
            System.Diagnostics.Debug.WriteLine("lname: " + name[1]);

            string fname = name[0];
            string lname = name[1];

            /*
            for (int i = 1; i < name.Length; i++)
            {
                lname += name[i] + " ";
            }
            */

            debiteuren deb = db.debiteuren.Where(d => d.Voornaam == fname && d.Achternaam == lname).SingleOrDefault();
            factuur.DebiteurID = deb.ID;

            //---------------------------------
            //Product
            List<producten> proList = new List<producten>();
            List<string> values = new List<string>();

            foreach (var tr in SelectTable.Controls.OfType<TableRow>())
            {
                foreach (var td in tr.Controls.OfType<TableCell>())
                {
                    foreach (var chk in td.Controls.OfType<CheckBox>())
                    {
                        if (chk.Checked)
                        {
                            values.Add(chk.Text);

                        }
                    }
                }
            }

            string product = "";

            for (int i = 0; i < values.Count; i++)
            {
                string j = values[i];
                product = j;


                producten p = db.producten.Where(pr => pr.Naam == j).SingleOrDefault();
                proList.Add(p);
            }

            System.Diagnostics.Debug.WriteLine("size proList: " + proList.Count);


            //Totaalbedrag factuur
            string totalString = totaalBox.Text;
            string str = totalString.Replace("€", string.Empty);

            decimal total = decimal.Parse(str);

            factuur.Totaalbedrag = total;


            //--------------------------------------
            //Save factuur to database

            try
            {
                db.facturen.Add(factuur);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            //--------------------------------------

            List<factuur_items> fiList = new List<factuur_items>();

            TextBox txtBox = null;

            foreach (var tr in SelectTable.Controls.OfType<TableRow>())
            {
                foreach (var td in tr.Controls.OfType<TableCell>())
                {
                    foreach (var txt in td.Controls.OfType<TextBox>())
                    {
                        if (txt.ID.Contains(product))
                        {
                            txtBox = txt;
                        }
                    }
                }
            }

            

            if (proList.Count == 1)
            {
                System.Diagnostics.Debug.WriteLine("Only one item in list");

                factuur_items fi = new factuur_items();
                fi.FactuurID = factuur.Factuurnummer;
                fi.Aantal = int.Parse(txtBox.Text);
                fi.ProductID = proList[0].ID;

                try
                {
                    db.factuur_items.Add(fi);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
            }
            else if(proList.Count > 1)
            {
                System.Diagnostics.Debug.WriteLine("More than one item in list");

                for (int i = 0; i < proList.Count; i++)
                {
                    factuur_items sfi = new factuur_items();
                    sfi.FactuurID = factuur.Factuurnummer;
                    sfi.Aantal = int.Parse(txtBox.Text);

                    sfi.ProductID = proList[i].ID;
                    fiList.Add(sfi);

                }

                try
                {
                    db.factuur_items.AddRange(fiList);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }

            }

            
        }


        private void CreateButton_Click(object sender, EventArgs e)
        {
            CreateFactuur();

            Response.Redirect("Factuur.aspx");
        }


       

    }
}