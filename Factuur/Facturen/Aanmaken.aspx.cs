using System;
using System.Collections.Generic;
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

            //Execute methods
            FillDDL();
            FillCheckBoxList();
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
        private void FillCheckBoxList()
        {
            List<producten> productList = db.producten.ToList();

            for (int i = 0; i < productList.Count; i++)
            {
                string pnaam = productList[i].Naam;

                productCheckBoxList.Items.Add(pnaam);
            }
        }

        private void CreateFactuur()
        {
            facturen factuur = new facturen();
            factuur.Factuurdatum = DateTime.Parse(datumBox.Text);

            //---------------------------------
            //Debiteur
            string debiteur = debDDL.SelectedValue;
            string[] name = debiteur.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string fname = name[0];
            string lname = "";

            for (int i = 1; i < name.Length; i++)
            {
                lname += name[i] + " ";
            }

            debiteuren deb = db.debiteuren.Where(d => d.Voornaam == fname && d.Achternaam == lname).SingleOrDefault();
            factuur.Debiteur = deb.ID;

            //---------------------------------
            //Product
            List<producten> proList = new List<producten>();
            List<string> values = new List<string>();

            foreach (ListItem Item in productCheckBoxList.Items)
            {
                if (Item.Selected)
                {
                    values.Add(Item.Value);
                }
            }

            for (int i = 0; i < values.Count; i++)
            {
                string j = values[i];
                producten p = db.producten.Where(pr => pr.Naam == j).SingleOrDefault();
                proList.Add(p);
            }


            //Totaalbedrag factuur
            decimal total = 0;

            for (int i = 0; i < proList.Count; i++)
            {
                total += (decimal) proList[i].Prijs;
            }

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

            for (int i = 0; i < proList.Count; i++)
            {
                factuur_items fi = new factuur_items();
                fi.FactuurID = factuur.Factuurnummer;
                fi.ProductID = proList[i].ID;

                fiList.Add(fi);
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


        private void CreateButton_Click(object sender, EventArgs e)
        {
            CreateFactuur();

            Response.Redirect("Factuur.aspx");
        }


    }
}