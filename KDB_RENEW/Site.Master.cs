using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace KDB_RENEW
{
    public partial class SiteMaster : MasterPage
    {
        public static User use;
        string u;
        protected void Page_Init(object sender, EventArgs e)
        {
            u = HttpContext.Current.Request.Url.AbsoluteUri;
            DatabaseDataContext d = new DatabaseDataContext();

            var log = from u in d.User
                      where u.Name == WindowsIdentity.GetCurrent().Name
                      select u;

            if (log.Count() != 0)
            {
                use = log.First();
            }

            if (log.Count() == 0)
            {
                User usr = new User() { Name = WindowsIdentity.GetCurrent().Name };
                d.User.InsertOnSubmit(usr);
                d.SubmitChanges();
                //Server.Transfer("~/Neu.aspx");
            }
            else if (log.First().Rechte == "Admin")
            {
                            
            }
            else if (log.First().Rechte == "Lesen")
            {
                if (u.Contains("User") || u.Contains("Daten"))
                {
                    Response.Redirect("~/Pages/NoAccess.aspx");
                }
            }
            else if (log.First().Rechte == "Schreiben")
            {
                if (u.Contains("User"))
                {
                    Response.Redirect("~/Pages/NoAccess.aspx");
                }
            }


            try
            {
                d.SubmitChanges();

            }
            catch (Exception)
            {

            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
           // Page.LoadComplete += new EventHandler(Page_LoadComplete);
        }

        protected void Page_LoadComplete(object sender, EventArgs e) {

            //HtmlGenericControl menu = (HtmlGenericControl)Page.FindControl("main_menu");
            //menu.Controls.Add(new LiteralControl("<li><a runat=\"server\" href=\"~/Pages/Suche\">Suche</a></li>"));
        }

     

        private HtmlControl FindHtmlControlByIdInControl(Control control, string id)
        {
            foreach (Control childControl in control.Controls)
            {
                if (childControl.ID != null && childControl.ID.Equals(id, StringComparison.OrdinalIgnoreCase) && childControl is HtmlControl)
                {
                    return (HtmlControl)childControl;
                }

                if (childControl.HasControls())
                {
                    HtmlControl result = FindHtmlControlByIdInControl(childControl, id);
                    if (result != null) return result;
                }
            }

            return null;
        }
    }
}