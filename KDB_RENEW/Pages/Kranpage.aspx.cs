using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KDB_RENEW.Pages
{
    public partial class Kranpage : System.Web.UI.Page
    {
        static bool kran_neu;
        static bool bereich_neu;
        static bool teil_neu;

        static int kran_nr;
        static int bereich_nr;
        static int teil_nr;

        static String message = "Fehler bei Datenübertagung!";

        private static List<Bereich> bList = new List<Bereich>();



        protected void Page_Load(object sender, EventArgs e)
        {  
            if (!IsPostBack)
            {
                kran_nr = Convert.ToInt32(Session["IDKran"]);
            }
            GridView_Kran_Refresh();
        }

        protected void GridView_Kran_Refresh()
        {

            DatabaseDataContext d = new DatabaseDataContext();
            var kra = from k in d.Kran
                      select new { Kran = k.Id };

            GridView_Kran.DataSource = kra;
            GridView_Kran.DataBind();
        }

        protected void GridView_Teil_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisableTeil();
            DeleteTextBoxTeilStrings();
            if (GridView_Teil.SelectedIndex != -1)
            {
                teil_nr = Convert.ToInt32(GridView_Teil.Rows[GridView_Teil.SelectedIndex].Cells[1].Text);

                DatabaseDataContext d = new DatabaseDataContext();
                //suche Teile
                var dat = from t in d.Teil
                          where t.Id == teil_nr
                          select t;

                //Fülle Boxen
                TextBox_Teil_Name.Text = dat.First().Name;
                TextBox_Teil_ZeichnungsNr.Text = dat.First().Zeichnungsnummer;
                TextBox_Teil_ArtikelNr.Text = dat.First().Artikelnummer;
                TextBox_Teil_Bemerkungen.Text = dat.First().Bermerkungen;

                try
                {
                    DropDownList_Teil.SelectedValue = Convert.ToString(dat.First().Id_Teil);
                }
                catch (Exception)
                {
                    message = "Teil nicht gefunden!";
                }
                Button_Teil_Bearbeiten.Enabled = true;
                Button_Teil_Löschen.Enabled = true;
            }
            else
            {
                Button_Teil_Bearbeiten.Enabled = false;
                Button_Teil_Löschen.Enabled = false;
            }

        }
        protected void GridView_Teil_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView_Teil.PageIndex = e.NewPageIndex;
            ListBox_B_SelectedIndexChanged(sender, e);
        }
        protected void GridView_Teil_OnDataBound(object sender, EventArgs e)
        {

        }

        protected void Button_Kran_Neu_Click(object sender, EventArgs e)
        {
            TextBox_Kran_Nr.Enabled = true;
            Button_Kran_Speichern.Enabled = true;
            Button_Kran_Bearbeiten.Enabled = true;
            //Button_Kran_Löschen.Enabled = true;
            kran_neu = true;

        }
        protected void Button_Kran_Speichern_Click(object sender, EventArgs e)
        {
            DatabaseDataContext d = new DatabaseDataContext();

            if (kran_neu)
            {
                Kran k = new Kran() { Id = Convert.ToInt32(TextBox_Kran_Nr.Text) };
                d.Kran.InsertOnSubmit(k);
            }
            else
            {
                Kran k = new Kran() { Id = Convert.ToInt32(TextBox_Kran_Nr.Text) };
                d.Kran.InsertOnSubmit(k);

                var ber = from ka in d.Bereich
                          where ka.Id_Kran == kran_nr
                          select ka;

                foreach (var Bereich in ber)
                {
                    Bereich.Id_Kran = Convert.ToInt32(TextBox_Kran_Nr.Text);
                }
                //kra.First().Id_Kran = Convert.ToInt32(TextBox_Kran_Nr.Text);

                var kra = from kr in d.Kran
                          where kr.Id == kran_nr
                          select kr;
                d.Kran.DeleteAllOnSubmit(kra);
            }
            try
            {
                d.SubmitChanges();
            }
            catch (Exception)
            {
                //...
            }

            TextBox_Kran_Nr.Enabled = false;
            Button_Kran_Löschen.Enabled = true;

            Button_Bereich_Neu.Enabled = true;

            GridView_Kran_Refresh();
        }
        protected void Button_Kran_Bearbeiten_Click(object sender, EventArgs e)
        {
            kran_neu = false;
            TextBox_Kran_Nr.Enabled = true;
            Button_Kran_Speichern.Enabled = true;
            Button_Kran_Löschen.Enabled = true;
        }
        protected void Button_Kran_Löschen_Click(object sender, EventArgs e)
        {
            DatabaseDataContext d = new DatabaseDataContext();
            var kra = from k in d.Kran
                      where k.Id == kran_nr
                      select k;

            var ber = from b in d.Bereich
                      where b.Id_Kran == kran_nr
                      select b;

            foreach (var Bereich in ber)
            {
                var tel = from t in d.Teil
                          where t.Id_Bereich == Bereich.Id
                          select t;
                d.Teil.DeleteAllOnSubmit(tel);
            }

            d.Bereich.DeleteAllOnSubmit(ber);
            d.Kran.DeleteAllOnSubmit(kra);
            d.SubmitChanges();
        }

        protected void Button_Bereich_Speichern_Click(object sender, EventArgs e)
        {
            DatabaseDataContext d = new DatabaseDataContext();
            if (bereich_neu)
            {
                Bereich b = new Bereich() { Id_Kran = kran_nr, Name = DropDownList_Bereich.SelectedItem.ToString() };
                d.Bereich.InsertOnSubmit(b);
            }
            else
            {
                var kra = from k in d.Kran
                          where k.Id == kran_nr
                          select k;
                //nochmal überdenken
                var dat = from b in d.Bereich
                          where b.Name == DropDownList_Bereich.SelectedValue && b.Id_Kran == kra.First().Id
                          select b;
                dat.First().Name = DropDownList_Bereich.SelectedItem.ToString();

            }
            d.SubmitChanges();
            DropDownList_Bereich.Enabled = false;
            Button_Bereich_Speichern.Enabled = false;
            GridView_Kran_SelectedIndexChanged(sender, e);
            //Button_Bereich_Bearbeiten.Enabled = true;
        }

        protected void CustomValidator_ID_ServerValidate(object source, ServerValidateEventArgs args)
        {
            DatabaseDataContext d = new DatabaseDataContext();
            var dat = from k in d.Kran
                      where k.Id == Convert.ToInt32(TextBox_Kran_Nr.Text)
                      select k;

            if (dat.Count() == 0)
            {
                args.IsValid = true;
            }
            else
            {
                if (kran_neu)
                {
                    args.IsValid = false;
                }

            }

        }
        protected void GridView_Kran_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox_B.SelectedIndex = -1;
            Button_Bereich_Löschen.Enabled = false;
            DisableTeil();
            DeleteTextBoxTeilStrings();
            Button_Teil_Neu.Enabled = false;

            TextBox_Kran_Nr.Text = GridView_Kran.Rows[GridView_Kran.SelectedIndex].Cells[1].Text;
            kran_nr = Convert.ToInt32(GridView_Kran.Rows[GridView_Kran.SelectedIndex].Cells[1].Text);
            Button_Kran_Bearbeiten.Enabled = true;
            //Button_Bereich_Löschen.Enabled = true;

            Button_Bereich_Neu.Enabled = true;
            TextBox_Kran_Nr.Enabled = false;
            DatabaseDataContext d = new DatabaseDataContext();
            var ber = from b in d.Bereich
                      where b.Id_Kran == kran_nr
                      select b;

            bList = ber.ToList();


            var ben = from bn in d.Bereich
                      where bn.Id_Kran == kran_nr
                      select bn.Name;

            ListBox_B.DataSource = ben;
            ListBox_B.DataBind();

            GridView_Teil.DataBind();

        }
        protected void GridView_Kran_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            GridView_Kran.PageIndex = e.NewPageIndex;
            GridView_Kran_Refresh();
        }

        protected void Button_Bereich_Neu_Click(object sender, EventArgs e)
        {
            bereich_neu = true;
            DropDownList_Bereich.Enabled = true;
            Button_Bereich_Speichern.Enabled = true;
        }
        protected void DropDownList_Bereich_SelectedIndexChanged(object sender, EventArgs e)
        {
            Button_Bereich_Speichern.Enabled = true;
        }
        protected void ListBox_B_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisableTeil();
            DeleteTextBoxTeilStrings();
            if (ListBox_B.SelectedIndex != -1)
            {
                Button_Bereich_Löschen.Enabled = true;
                try
                {
                    DropDownList_Bereich.SelectedValue = ListBox_B.SelectedValue;
                }
                catch (Exception)
                {

                }


                DataGrid_Teil_Refresh();




                Button_Teil_Neu.Enabled = true;
            }
            else
            {
                Button_Teil_Neu.Enabled = false;
                Button_Bereich_Löschen.Enabled = false;
            }

        }

        protected void Button_Bereich_Löschen_Click(object sender, EventArgs e)
        {
            DatabaseDataContext d = new DatabaseDataContext();

            var ber = from b in d.Bereich
                      where b.Name == ListBox_B.SelectedValue && b.Id == bereich_nr
                      select b;

            var tei = from t in d.Teil
                      where t.Id_Bereich == ber.First().Id
                      select t;

            d.Teil.DeleteAllOnSubmit(tei);

            d.Bereich.DeleteAllOnSubmit(ber);

            d.SubmitChanges();
            GridView_Kran_SelectedIndexChanged(sender, e);
        }

        protected void CustomValidator_Bereich_ServerValidate(object source, ServerValidateEventArgs args)
        {
            DatabaseDataContext d = new DatabaseDataContext();

            var kra = from k in d.Kran
                      where k.Id == kran_nr
                      select k;
            //nochmal überdenken
            var dat = from b in d.Bereich
                      where b.Name == DropDownList_Bereich.SelectedValue && b.Id_Kran == kra.First().Id
                      select b;

            if (dat.Count() == 0)
            {
                args.IsValid = true;
            }
            else
            {
                if (kran_neu)
                {
                    args.IsValid = false;
                }

            }
        }

        protected void Button_Teil_Bearbeiten_Click(object sender, EventArgs e)
        {
            TextBox_Teil_ArtikelNr.Enabled = true;
            TextBox_Teil_Bemerkungen.Enabled = true;
            TextBox_Teil_Name.Enabled = true;
            TextBox_Teil_ZeichnungsNr.Enabled = true;
            DropDownList_Teil.Enabled = true;
            Button_Teil_Speichern.Enabled = true;
        }

        protected void Button_Teil_Neu_Click(object sender, EventArgs e)
        {
            teil_neu = true;
            Button_Teil_Speichern.Enabled = true;

            TextBox_Teil_Name.Enabled = true;
            TextBox_Teil_ZeichnungsNr.Enabled = true;
            TextBox_Teil_ArtikelNr.Enabled = true;
            TextBox_Teil_Bemerkungen.Enabled = true;
            DropDownList_Teil.Enabled = true;
        }

        void DisableTeil()
        {

            TextBox_Teil_ZeichnungsNr.Enabled = false;
            TextBox_Teil_Name.Enabled = false;
            TextBox_Teil_Bemerkungen.Enabled = false;
            TextBox_Teil_ArtikelNr.Enabled = false;
            DropDownList_Teil.Enabled = false;

            Button_Teil_Speichern.Enabled = false;
            Button_Teil_Löschen.Enabled = false;
            Button_Teil_Bearbeiten.Enabled = false;
        }
        void DeleteTextBoxTeilStrings()
        {

            TextBox_Teil_ArtikelNr.Text = String.Empty;
            TextBox_Teil_Bemerkungen.Text = String.Empty;
            TextBox_Teil_Name.Text = String.Empty;
            TextBox_Teil_ZeichnungsNr.Text = String.Empty;
            DropDownList_Teil.SelectedValue = "";
        }

        protected void Button_Teil_Speichern_Click(object sender, EventArgs e)
        {
            DatabaseDataContext d = new DatabaseDataContext();

            if (teil_neu)
            {
                if (DropDownList_Teil.SelectedValue == "")
                {
                    Teil t = new Teil()
                    {
                        Id_Bereich = bereich_nr,
                        Name = TextBox_Teil_Name.Text,
                        Zeichnungsnummer = TextBox_Teil_ZeichnungsNr.Text,
                        Artikelnummer = TextBox_Teil_ArtikelNr.Text,
                        Bermerkungen = TextBox_Teil_Bemerkungen.Text

                    };
                    d.Teil.InsertOnSubmit(t);
                    d.SubmitChanges();
                }
                else
                {
                    Teil t = new Teil()
                    {
                        Id_Bereich = bereich_nr,
                        Name = TextBox_Teil_Name.Text,
                        Zeichnungsnummer = TextBox_Teil_ZeichnungsNr.Text,
                        Artikelnummer = TextBox_Teil_ArtikelNr.Text,
                        Bermerkungen = TextBox_Teil_Bemerkungen.Text,
                        Id_Teil = Convert.ToInt32(DropDownList_Teil.SelectedValue)
                    };
                    d.Teil.InsertOnSubmit(t);
                    d.SubmitChanges();
                }



            }
            else
            {
                var tel = from t in d.Teil
                          where t.Id == teil_nr
                          select t;

                tel.First().Zeichnungsnummer = TextBox_Teil_ZeichnungsNr.Text;
                tel.First().Artikelnummer = TextBox_Teil_ArtikelNr.Text;
                tel.First().Bermerkungen = TextBox_Teil_Bemerkungen.Text;
                tel.First().Name = TextBox_Teil_Name.Text;
                tel.First().Id_Teil = Convert.ToInt32(DropDownList_Teil.SelectedValue);
            }

            try
            {
                d.SubmitChanges();
            }
            catch (Exception)
            {
                message = "Keine Datenbankverbindung";
            }
            DisableTeil();
            DeleteTextBoxTeilStrings();
            ListBox_B_SelectedIndexChanged(sender, e);
            //GridView_Kran_Refresh();
        }

        protected void Button_Teil_Löschen_Click(object sender, EventArgs e)
        {
            DatabaseDataContext d = new DatabaseDataContext();
            var los = from h in d.Teil
                      where h.Id == teil_nr
                      select h;

            d.Teil.DeleteAllOnSubmit(los);


            try
            {
                d.SubmitChanges();
            }
            catch (Exception)
            {
                message = "Keine Datenbankverbindung";
            }
            DisableTeil();
            DeleteTextBoxTeilStrings();
            ListBox_B_SelectedIndexChanged(sender, e);
        }

        void DataGrid_Teil_Refresh()
        {

            DatabaseDataContext d = new DatabaseDataContext();

            var ber = from b in d.Bereich
                      where b.Name == ListBox_B.SelectedValue && b.Id_Kran == kran_nr
                      select b;
            bereich_nr = ber.First().Id;

            var tel = from t in d.Teil
                      where t.Id_Bereich == ber.First().Id
                      select new { t.Id, t.Name, ZeichnungsNr = t.Zeichnungsnummer, ArtikelNr = t.Artikelnummer, t.Bermerkungen, Teil_von = t.Id_Teil };

            //Für Dropdownbox
            var tet = from g in tel
                      select new { g.Id };
            List<String> tList = new List<string>();
            tList.Add("");
            foreach (var i in tet)
            {
                tList.Add(Convert.ToString(i.Id));
            }

            DropDownList_Teil.DataSource = tList;
            DropDownList_Teil.DataBind();

            //Fülle Gridview
            GridView_Teil.DataSource = tel;
            GridView_Teil.DataBind();
        }
    }
}