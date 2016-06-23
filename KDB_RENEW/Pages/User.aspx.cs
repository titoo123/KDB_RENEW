using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KDB_RENEW.Pages
{
    public partial class User : System.Web.UI.Page
    {
        DatabaseDataContext db = new DatabaseDataContext();
        bool neu;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridView_User_Refresh();

                //var dis = from d in db.Bereiche
                //          select new { d.Name };

                //List<String> dList = new List<string>();
                //dList.Add("");
                //foreach (var d in dis)
                //{
                //    dList.Add(d.Name);
                //}

                //DropDownList_Distrikt.DataSource = dList;
                //DropDownList_Distrikt.DataBind();
            }

        }

        protected void GridView_User_SelectedIndexChanged(object sender, EventArgs e)
        {
            Button_Bearbeiten.Enabled = true;
            //Button_Löschen.Enabled = true;

            try
            {
                Label_Name.Text = "Name: " + GridView_User.Rows[GridView_User.SelectedIndex].Cells[2].Text;
            }
            catch (Exception)
            {
                Label_Name.Text = "Name: ";
            }


            try
            {
                DropDownList_Rechte.SelectedValue = GridView_User.Rows[GridView_User.SelectedIndex].Cells[3].Text;
            }
            catch (Exception)
            {
                DropDownList_Rechte.SelectedValue = "";
            }

            //try
            //{
            //    DropDownList_Distrikt.SelectedValue = GridView_User.Rows[GridView_User.SelectedIndex].Cells[4].Text;
            //}
            //catch (Exception)
            //{
            //    DropDownList_Distrikt.SelectedValue = "";
            //}
        }

        protected void Button_Speichern_Click(object sender, EventArgs e)
        {
            var usr = from u in db.User
                      where u.Id == Convert.ToInt32(GridView_User.Rows[GridView_User.SelectedIndex].Cells[1].Text)
                      select u;

            if (neu)
            {
                //....
            }
            else
            {
                usr.First().Rechte = DropDownList_Rechte.SelectedValue;
            }
            //var dis = from d in db.Bereiche
            //          where d.Name == DropDownList_Distrikt.SelectedValue
            //          select d;

            //usr.First().Rechte = DropDownList_Rechte.SelectedValue;

            //if (dis.Count() > 0)
            //{
            //    usr.First().Distrikt = dis.First().Id;
            //}
            //else
            //{
            //    usr.First().Distrikt = null;
            // }

            db.SubmitChanges();
            GridView_User_Refresh();

            DropDownList_Rechte.Enabled = false;
            DropDownList_Rechte.SelectedValue = "";
            Button_Bearbeiten.Enabled = false;
            Button_Löschen.Enabled = false;
            Button_Speichern.Enabled = false;
        }

        protected void Button_Bearbeiten_Click(object sender, EventArgs e)
        {
            DropDownList_Rechte.Enabled = true;
            //DropDownList_Distrikt.Enabled = true;
            Button_Löschen.Enabled = true;
            Button_Speichern.Enabled = true;

        }

        protected void Button_Löschen_Click(object sender, EventArgs e)
        {
            var usr = from u in db.User
                      where u.Id == Convert.ToInt32(GridView_User.Rows[GridView_User.SelectedIndex].Cells[1].Text)
                      select u;

            db.User.DeleteAllOnSubmit(usr);
            db.SubmitChanges();
        }

        protected void DropDownList_Rechte_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Button_Speichern.Enabled = true;
            //Refresh();
        }
        protected void DropDownList_Distrikt_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        void GridView_User_Refresh()
        {
            var usr = from u in db.User
                      select u;

            try
            {
                GridView_User.DataSource = usr;
                GridView_User.DataBind();
            }
            catch (Exception)
            {

            }
        }
    }
}