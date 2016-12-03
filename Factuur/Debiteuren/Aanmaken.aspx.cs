using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Factuur.Debiteuren
{
    public partial class Aanmaken : System.Web.UI.Page
    {
        FactuurDB db = new FactuurDB();

        protected void Page_Load(object sender, EventArgs e)
        {
            CreateButton.Click += CreateButton_Click;

        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            CreateDeb();

            Response.Redirect("Debiteur.aspx");
        }

        private void CreateDeb()
        {
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

        }
    }
}