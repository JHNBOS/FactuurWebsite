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
        }
    }
}