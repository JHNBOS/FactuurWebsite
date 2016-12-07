using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Factuur.Producten
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
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            CreateProduct();
            Response.Redirect("Product.aspx");
        }

        private void CreateProduct()
        {
            producten product = new producten();

            product.Naam = naamBox.Text;
            product.Prijs = decimal.Parse(prijsBox.Text);
            product.BTW = int.Parse(btwBox.Text);
            product.Korting = int.Parse(kortingBox.Text);

            try
            {
                db.producten.Add(product);
                db.SaveChanges();

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            string debiteur = DebDDL.SelectedValue;
            
            if (DebDDL.SelectedValue != "")
            {
                string[] name = debiteur.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string fname = name[0];
                string lname = "";

                for (int i = 1; i < name.Length; i++)
                {
                    lname += name[i] + " ";
                }

                debiteuren deb = db.debiteuren.Where(d => d.Voornaam == fname && d.Achternaam == lname).SingleOrDefault();

                toewijzen toewijzing = new toewijzen();
                producten prod = db.producten.Where(p => p.Naam == naamBox.Text).SingleOrDefault();
                toewijzing.ProductID = prod.ID;
                toewijzing.DebiteurID = deb.ID;

                try
                {
                    db.toewijzen.Add(toewijzing);
                    db.SaveChanges();

                    Message m = new Message();
                    m.Show("Product is aangemaakt!");
                }
                catch (Exception ex)
                {
                    Message m = new Message();
                    m.Show("Product kon niet worden aangemaakt!");
                    System.Diagnostics.Debug.WriteLine(ex);
                }
            }
            else
            {
                Message m = new Message();
                m.Show("Product is aangemaakt!");
            }
            

        }

        private void FillDDL()
        {
            List<debiteuren> debList = db.debiteuren.ToList();

            DebDDL.Items.Add("");

            for (int i = 0; i < debList.Count; i++)
            {
                string name = debList[i].Voornaam + " " + debList[i].Achternaam;

                DebDDL.Items.Add(name);
            }
        }
    }
}