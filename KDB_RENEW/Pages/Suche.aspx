<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Suche.aspx.cs" Inherits="KDB_RENEW.Pages.Suche"  MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="ExtendedGridView" Namespace="ExtendedControls" TagPrefix="ext" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--  <fieldset><legend>Suche</legend>--%>
<div class="container">
    <div class="jumbotron">
        <h1>Suche</h1>
        <p>Hier kann nach Informationen der Kräne gesucht werden!</p>
    </div>

    <div class="row">
        <div class="col-sm-4">
         <%--   <div class="input-group">--%>
                <h3>Kran</h3>
                <asp:Label ID="Label1" runat="server" Text="Nr. :"></asp:Label><br />
                <asp:TextBox ID="TextBox_suche_KNR" runat="server" CssClass="form-control"></asp:TextBox><br />
            <%--</div>--%>
        </div>
        <div class="col-sm-4">
            <h3>Bereich</h3>
            <asp:Label ID="Label6" runat="server" Text="Bereichsname:"></asp:Label><br />
            <asp:TextBox ID="TextBox_Bereich" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
    <div class="col-sm-4">
      <h3>Teil</h3>
        <asp:Label ID="Label3" runat="server" Text="Name:"></asp:Label><br />
        <asp:TextBox ID="TextBox_Teil_Name" runat="server" CssClass="form-control"></asp:TextBox><br />
        <asp:Label ID="Label4" runat="server" Text="ZeichnungsNr:"></asp:Label><br />
        <asp:TextBox ID="TextBox_Teil_ZNR" runat="server" CssClass="form-control"></asp:TextBox><br />
        <asp:Label ID="Label5" runat="server" Text="ArtikelNr:"></asp:Label><br />
        <asp:TextBox ID="TextBox_Teil_ANR" runat="server" CssClass="form-control"></asp:TextBox><br />
        <br />
    </div>

        <asp:Button ID="Button_Suchen" runat="server" Text="Suchen" class="btn btn-primary btn-lg"
            OnClick="Button_Suchen_Click" />
    </div>

</div><br />
    <ext:ExtendedGridView ID="GridView_Suche" runat="server" OnDataBound="GridView_Suche_OnDataBound"
        OnSelectedIndexChanged="GridView_Suche_SelectedIndexChanged"
        BorderStyle="None" GridLines="Horizontal" AllowPaging="True" OnPageIndexChanging="GridView_Suche_PageIndexChanging"
        PageSize="50" Width="100%">
        <AlternatingRowStyle Width="200px" />
        <RowStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="300px" />
    </ext:ExtendedGridView>


</asp:Content>
