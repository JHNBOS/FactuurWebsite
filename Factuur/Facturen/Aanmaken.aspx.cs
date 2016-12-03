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
            /*
            debiteuren deb = new debiteuren();

            deb.Voornaam = voornaamBox.Text;
            deb.Achternaam = achternaamBox.Text;
            deb.Email = emailBox.Text;
            deb.Telefoon = telefoonBox.Text;
            deb.Adres = adresBox.Text;
            deb.Postcode = postcodeBox.Text;
            deb.Plaats = plaatsBox.Text;
            deb.Land = landBox.Text;

            try
            {
                db.debiteuren.Add(deb);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            */
        }


        private void CreateButton_Click(object sender, EventArgs e)
        {
            CreateFactuur();

            Response.Redirect("Factuur.aspx");
        }


    }
}