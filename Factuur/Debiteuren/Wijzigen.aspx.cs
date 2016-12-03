using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Factuur.Debiteuren
{
    public partial class Wijzigen : System.Web.UI.Page
    {
        FactuurDB db = new FactuurDB();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Get ID debiteur
            string id = Session["EditID"].ToString();

            //Event handlers
            SaveButton.Click += SaveButton_Click;

            //Execute methods
            if (!IsPostBack)
            {
                FillTextBoxes(id);
            }
        }

        //Fill textboxes with info from selected debiteur
        private void FillTextBoxes(string id)
        {
            int ID = int.Parse(id);
            debiteuren deb = db.debiteuren.Find(ID);

            voornaamBox.Text = deb.Voornaam;
            achternaamBox.Text = deb.Achternaam;
            emailBox.Text = deb.Email;
            telefoonBox.Text = deb.Telefoon;
            adresBox.Text = deb.Adres;
            postcodeBox.Text = deb.Postcode;
            plaatsBox.Text = deb.Plaats;
            landBox.Text = deb.Land;
        }

        //Save button to update debiteur
        private void SaveButton_Click(object sender, EventArgs e)
        {
            string id = Session["EditID"].ToString();
            int ID = int.Parse(id);

            debiteuren deb = db.debiteuren.Find(ID);
            debiteuren newDeb = new debiteuren();

            if (deb != null)
            {
                newDeb.ID = ID;
                newDeb.Voornaam = voornaamBox.Text;
                newDeb.Achternaam = achternaamBox.Text;
                newDeb.Email = emailBox.Text;
                newDeb.Telefoon = telefoonBox.Text;
                newDeb.Adres = adresBox.Text;
                newDeb.Postcode = postcodeBox.Text;
                newDeb.Plaats = plaatsBox.Text;
                newDeb.Land = landBox.Text;
            }

            db.Entry(deb).CurrentValues.SetValues(newDeb);
            db.SaveChanges();

            Response.Redirect("Debiteur.aspx");
        }



    }
}