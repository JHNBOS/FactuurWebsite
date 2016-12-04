using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Factuur.Facturen
{
    public partial class Wijzigen : System.Web.UI.Page
    {
        FactuurDB db = new FactuurDB();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Get ID debiteur
            string id = Session["EditID3"].ToString();

            //Event handlers
            SaveButton.Click += SaveButton_Click;

            //Execute methods
            if (!IsPostBack)
            {
                FillTextBoxes(id);
                FillDDL();
            }
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

        //Fill textboxes with info from selected debiteur
        private void FillTextBoxes(string id)
        {
            int ID = int.Parse(id);
            facturen fact = db.facturen.Find(ID);
            List<factuur_items> fiList = db.factuur_items.Where(f => f.FactuurID == ID).ToList();

            List<producten> productList = new List<producten>();
            List<int> pidList = new List<int>();

            for (int i = 0; i < fiList.Count; i++)
            {
                int pid = fiList[i].ProductID;

                pidList.Add(pid);
            }

            for (int i = 0; i < pidList.Count; i++)
            {
                int pid = pidList[i];
                producten p = db.producten.Find(pid);

                productList.Add(p);
            }

            datumBox.Text = String.Format("{0:dd-MM-yyyy}", fact.Factuurdatum);
            totaalBox.Text = String.Format("{0:C}", fact.Totaalbedrag );

        }

        //Save button to update factuur
        private void SaveButton_Click(object sender, EventArgs e)
        {
            string id = Session["EditID3"].ToString();
            int ID = int.Parse(id);

            facturen fact = db.facturen.Find(ID);
            facturen newFact = new facturen();

            if (fact != null)
            {
                //Factuur

                newFact.Factuurnummer = ID;
                newFact.Factuurdatum = DateTime.Parse(datumBox.Text);
                newFact.Totaalbedrag = decimal.Parse(totaalBox.Text);

                //----------------------------------------------------
                //Debiteur

                string debiteur = debDDL.SelectedValue;
                string[] name = debiteur.Split(' ');
                string fname = name[0];
                string lname = name[1];

                debiteuren deb = db.debiteuren.Where(d => d.Voornaam == fname && d.Achternaam == lname).SingleOrDefault();

                fact.Debiteur = deb.ID;

                //----------------------------------------------------
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

                string products = "";

                for (int i = 0; i < proList.Count; i++)
                {
                    string pname = proList[i].Naam;

                    products += pname + "\n";
                }

                //--------------------------------------
                //Save factuur to database

                try
                {
                    db.facturen.Add(fact);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }

                //--------------------------------------

            }

            db.Entry(fact).CurrentValues.SetValues(newFact);
            db.SaveChanges();

            Response.Redirect("Factuur.aspx");
        }
    }
}