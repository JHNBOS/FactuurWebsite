using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Factuur.Producten
{
    public partial class Wijzigen : System.Web.UI.Page
    {
        FactuurDB db = new FactuurDB();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Get ID debiteur
            string id = Session["EditID2"].ToString();

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
            producten product = db.producten.Find(ID);

            naamBox.Text = product.Naam;
            prijsBox.Text = String.Format("{0:N2}", product.Prijs);
            btwBox.Text = product.BTW.ToString();
            kortingBox.Text = product.Korting.ToString();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            string id = Session["EditID2"].ToString();
            int ID = int.Parse(id);

            producten product = db.producten.Find(ID);
            producten newProduct = new producten();

            if (product != null)
            {
                newProduct.ID = ID;
                newProduct.Naam = naamBox.Text;
                newProduct.Prijs = decimal.Parse(prijsBox.Text);
                newProduct.BTW = int.Parse(btwBox.Text);
                newProduct.Korting = int.Parse(kortingBox.Text);
            }

            db.Entry(product).CurrentValues.SetValues(newProduct);
            db.SaveChanges();

            Response.Redirect("Product.aspx");
        }
    }
}