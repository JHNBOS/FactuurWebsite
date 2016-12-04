using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Factuur
{
    public partial class Debiteur : System.Web.UI.Page
    {
        FactuurDB db = new FactuurDB();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Event handlers
            CreateButton.Click += CreateButton_Click;

            //Execute methods
            FillGridView();

        }

      
        //Fill GridView of listed debiteuren.
        private void FillGridView()
        {
            try
            {
                List<debiteuren> deblist = db.debiteuren.ToList();

                for (int i = 0; i < deblist.Count; i++)
                {
                    string voornaam = deblist[i].Voornaam;
                    string achternaam = deblist[i].Achternaam;
                    string email = deblist[i].Email;
                    string telefoon = deblist[i].Telefoon;
                    string adres = deblist[i].Adres;
                    string postcode = deblist[i].Postcode;
                    string plaats = deblist[i].Plaats;
                    string land = deblist[i].Land;

                    Button del = new Button();
                    del.ID = "del_" + deblist[i].ID.ToString();
                    del.Text = "Verwijder";
                    del.CssClass = "btn btn-warning btn-sm";
                    del.Click += Del_Click;

                    Button edit = new Button();
                    edit.ID = "edit_" + deblist[i].ID.ToString();
                    edit.Text = "Wijzigen";
                    edit.CssClass = "btn btn-success btn-sm";
                    edit.Click += Edit_Click;

                    TableCell cell = new TableCell();
                    TableCell cell1 = new TableCell();
                    TableCell cell2 = new TableCell();
                    TableCell cell3 = new TableCell();
                    TableCell cell4 = new TableCell();
                    TableCell cell5 = new TableCell();
                    TableCell cell6 = new TableCell();
                    TableCell cell7 = new TableCell();
                    TableCell cell8 = new TableCell();
                    TableCell cell9 = new TableCell();

                    cell.Text = voornaam;
                    cell1.Text = achternaam;
                    cell2.Text = email;
                    cell3.Text = telefoon;
                    cell4.Text = adres;
                    cell5.Text = postcode;
                    cell6.Text = plaats;
                    cell7.Text = land;
                    cell8.Controls.Add(edit);
                    cell9.Controls.Add(del);

                    TableRow row = new TableRow();

                    row.Cells.Add(cell);
                    row.Cells.Add(cell1);
                    row.Cells.Add(cell2);
                    row.Cells.Add(cell3);
                    row.Cells.Add(cell4);
                    row.Cells.Add(cell5);
                    row.Cells.Add(cell6);
                    row.Cells.Add(cell7);
                    row.Cells.Add(cell8);
                    row.Cells.Add(cell9);

                    debTable.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }

        }

        //Create new debiteur button
        private void CreateButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Aanmaken.aspx");
        }

        //Edit button in table
        private void Edit_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string id = btn.ID;

            string[] split = id.Split('_');
            int ID = int.Parse(split[1]);

            Session["EditID"] = ID;
            Response.Redirect("Wijzigen.aspx");
        }

        //Delete button in table
        private void Del_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string id = btn.ID;

            string[] split = id.Split('_');
            int ID = int.Parse(split[1]);

            string confirmValue = Request.Form["confirm_value"];

            if (confirmValue == "Ja")
            {
                try
                {
                    debiteuren deb = db.debiteuren.Where(d => d.ID == ID).SingleOrDefault();
                    List<facturen> fList = db.facturen.Where(fa => fa.Debiteur == ID).ToList();
                    List<factuur_items> fiList = db.factuur_items.Where(f => f.facturen.Debiteur == ID).ToList();

                    db.factuur_items.RemoveRange(fiList);
                    db.facturen.RemoveRange(fList);
                    db.debiteuren.Remove(deb);
                    db.SaveChanges();

                    Message m = new Message();
                    m.Show("Debiteur is verwijderd!");

                    Response.Redirect(Request.RawUrl);

                }
                catch (Exception ex)
                {
                    Message m = new Message();
                    m.Show("Debiteur kon niet worden verwijderd!");
                    System.Diagnostics.Debug.WriteLine(ex);
                }
            }
            else
            {
                Response.Redirect(Request.RawUrl);
            }
        }




    }
}