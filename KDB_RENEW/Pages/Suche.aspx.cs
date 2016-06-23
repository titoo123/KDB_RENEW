using KDB_RENEW.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KDB_RENEW.Pages
{
    public partial class Suche : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView_Suche.RowCreated += RowEvent.OnCustomRow;
        }

        protected void GridView_Suche_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GridView_Suche.SelectedIndex != -1)
            {
                Session["IDKran"] = GridView_Suche.GetItemIDByGridView(0).X;

                if (SiteMaster.use != null && SiteMaster.use.Rechte == "Admin" || SiteMaster.use.Rechte == "Schreiben")
                {//
                    int t;
                    if (int.TryParse(Session["IDKran"].ToString(),out t))
                    {
                        Server.Transfer("Kranpage.aspx");
                    }
                }

            }
        }

        protected void GridView_Suche_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView_Suche.PageIndex = e.NewPageIndex;
            Button_Suchen_Click(sender, e);
        }
        protected void GridView_Suche_OnDataBound(object sender, EventArgs e)
        {

        }

     
        protected void Button_Suchen_Click(object sender, EventArgs e)
        {
            DatabaseDataContext d = new DatabaseDataContext();

            var meg = from k in d.Kran

                      join b in d.Bereich on k.Id equals b.Id_Kran
                      into bGroups
                      from b in bGroups.DefaultIfEmpty()

                      join t in d.Teil on b.Id equals t.Id_Bereich
                      into tGroups
                      from t in tGroups.DefaultIfEmpty()
                      select new { Kran = k.Id, b.Name, Teil = t.Name, t.Zeichnungsnummer, t.Artikelnummer, t.Bermerkungen };


            meg = from f in meg
                  where f.Teil.Contains(TextBox_Teil_Name.Text)
                        && f.Zeichnungsnummer.Contains(TextBox_Teil_ZNR.Text)
                        && f.Artikelnummer.Contains(TextBox_Teil_ANR.Text)
                        && f.Name.Contains(TextBox_Bereich.Text)
                  select f;

            if (TextBox_suche_KNR.Text.Length > 0)
            {
                meg = from l in meg
                      where l.Kran == Convert.ToInt32(TextBox_suche_KNR.Text)
                      select l;
            }
            
            GridView_Suche.DataSource = meg;
            GridView_Suche.DataBind();
        }


    }
}