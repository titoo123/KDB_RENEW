<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Kranpage.aspx.cs" Inherits="KDB_RENEW.Pages.Kranpage" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="ExtendedGridView" Namespace="ExtendedControls" TagPrefix="ext" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <div class="jumbotron">
            <h1>Daten</h1>
            <p>Hier können Informationen einzelner Kräne eingesehen werden.</p>
        </div>
        <div class="row">
            <div class="col-sm-4">
                <h3>Kran</h3>
                <ext:ExtendedGridView ID="GridView_Kran" runat="server" AutoPostBack="true"
                    AutoGenerateSelectButton="True"
                    OnSelectedIndexChanged="GridView_Kran_SelectedIndexChanged"
                    BorderStyle="None" GridLines="Horizontal" AllowPaging="True" OnPageIndexChanging="GridView_Kran_PageIndexChanging"
                    PageSize="8" Width="330px">
                   <%-- <AlternatingRowStyle Width="500px" />
                    <RowStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="300px" />--%>
                </ext:ExtendedGridView>
                <br /><br /><br />
                <asp:Label ID="Label1" runat="server" Text="Nr:"></asp:Label><br />
                <asp:TextBox ID="TextBox_Kran_Nr" runat="server" Enabled="False" Width="340px" CssClass="form-control"></asp:TextBox>
                <asp:CustomValidator ID="CustomValidator_ID" runat="server"
                    ErrorMessage="Kran bereits vorhanden!"
                    ControlToValidate="TextBox_Kran_Nr" ValidationGroup="vGroup1"
                    OnServerValidate="CustomValidator_ID_ServerValidate"></asp:CustomValidator>

            </div>
            <div class="col-sm-4">
                <h3>Bereich</h3>
                <%-- <asp:Label ID="Label2" runat="server" Text="Name:"></asp:Label><br />--%>
                <asp:ListBox ID="ListBox_B" runat="server" AutoPostBack="True"
                    OnSelectedIndexChanged="ListBox_B_SelectedIndexChanged" Width="330px"
                    Height="273px"></asp:ListBox>
                <br /><br />
                Name:<br />
                <asp:DropDownList ID="DropDownList_Bereich" runat="server"  CssClass="form-control"
                    Width="349px" Enabled="False"
                    OnSelectedIndexChanged="DropDownList_Bereich_SelectedIndexChanged">
                    <asp:ListItem>Grosser-Hub</asp:ListItem>
                    <asp:ListItem>Katzfahrt</asp:ListItem>
                    <asp:ListItem Selected="True">Kranfahrt</asp:ListItem>
                    <asp:ListItem>Kleiner-Hub</asp:ListItem>
                    <asp:ListItem>Haltewerk</asp:ListItem>
                    <asp:ListItem>Schließwerk</asp:ListItem>
                    <asp:ListItem>Hilfshub</asp:ListItem>
                    <asp:ListItem>Säule</asp:ListItem>
                    <asp:ListItem>Traverse</asp:ListItem>
                    <asp:ListItem>Drehwerk</asp:ListItem>
                </asp:DropDownList><br />
                <asp:CustomValidator ID="CustomValidator_Bereich" runat="server"
                    ErrorMessage="Bereich ist für diesen Kran bereits vergeben!"
                    ValidationGroup="vGroup2" ControlToValidate="DropDownList_Bereich"
                    OnServerValidate="CustomValidator_Bereich_ServerValidate"></asp:CustomValidator>


            </div>
            <div class="col-sm-4">
                <h3>Teil</h3>
                <asp:Label ID="Label3" runat="server" Text="Name:"></asp:Label><br />
                <asp:TextBox ID="TextBox_Teil_Name" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                <br />
                <asp:Label ID="Label4" runat="server" Text="ZeichnungsNr:"></asp:Label><br />
                <asp:TextBox ID="TextBox_Teil_ZeichnungsNr" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                <br />
                <asp:Label ID="Label5" runat="server" Text="ArtikelNr:"></asp:Label><br />
                <asp:TextBox ID="TextBox_Teil_ArtikelNr" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                <br />
                <asp:Label ID="Label6" runat="server" Text="Bemerkungen:"></asp:Label><br />
                <asp:TextBox ID="TextBox_Teil_Bemerkungen" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                <br />
                <asp:Label ID="Label7" runat="server" Text="Teil:"></asp:Label><br />
                <asp:DropDownList ID="DropDownList_Teil" runat="server" CssClass="form-control"
                    Width="350px" Enabled="False">
                </asp:DropDownList><br /><br />

            </div>
            <div class="col-sm-5">
                   <asp:Button ID="Button_Kran_Neu" runat="server" Text="Neu" class="btn btn-primary" OnClick="Button_Kran_Neu_Click" />

                    <asp:Button ID="Button_Kran_Speichern" runat="server" Text="Speichern" class="btn btn-success" ValidationGroup="vGroup1" Enabled="False" OnClick="Button_Kran_Speichern_Click" />
                    <asp:Button ID="Button_Kran_Bearbeiten" runat="server" Text="Bearbeiten" CausesValidation="false" class="btn btn-warning"
                        Enabled="False" OnClick="Button_Kran_Bearbeiten_Click" />
                    <asp:Button ID="Button_Kran_Löschen" runat="server" Text="Löschen" class="btn btn-danger"
                        Enabled="false" OnClick="Button_Kran_Löschen_Click" OnClientClick="if(!confirm('Wirklich löschen?')) return false;" />

            </div>
            <div class="col-sm-5">
                       <asp:Button ID="Button_Bereich_Neu" runat="server" Text="Neu" Enabled="False" class="btn btn-primary"
                        OnClick="Button_Bereich_Neu_Click" />

                    <asp:Button ID="Button_Bereich_Speichern" runat="server" Text="Hinzufügen" ValidationGroup="vGroup2" class="btn btn-success"
                        Enabled="False" OnClick="Button_Bereich_Speichern_Click" />


                    <asp:Button ID="Button_Bereich_Löschen" runat="server" Text="Löschen" class="btn btn-danger"
                        Enabled="False" OnClick="Button_Bereich_Löschen_Click" OnClientClick="if(!confirm('Wirklich löschen?')) return false;" />
                 
            </div>
            <div class="col-sm-5">
                    <asp:Button ID="Button_Teil_Neu" runat="server" Text="Neu" Enabled="False" class="btn btn-primary"
                        OnClick="Button_Teil_Neu_Click" />

                    <asp:Button ID="Button_Teil_Speichern" runat="server" Text="Speichern" class="btn btn-success"
                        Enabled="False" OnClick="Button_Teil_Speichern_Click" />

                    <asp:Button ID="Button_Teil_Bearbeiten" runat="server" Text="Bearbeiten" class="btn btn-warning"
                        Enabled="False" OnClick="Button_Teil_Bearbeiten_Click" />

                    <asp:Button ID="Button_Teil_Löschen" runat="server" Text="Löschen" class="btn btn-danger"
                        Enabled="False" OnClick="Button_Teil_Löschen_Click" OnClientClick="if(!confirm('Wirklich löschen?')) return false;" />
            </div>
        </div>
    </div>

    <br />



    <fieldset>
        <legend>Teil</legend>

        <br />
        <ext:ExtendedGridView ID="GridView_Teil" runat="server"  CssClass=""
            AutoGenerateSelectButton="True" OnDataBound="GridView_Teil_OnDataBound"
            OnSelectedIndexChanged="GridView_Teil_SelectedIndexChanged"
            BorderStyle="None" GridLines="Horizontal" AllowPaging="True" OnPageIndexChanging="GridView_Teil_PageIndexChanging"
            PageSize="50" Width="100%">
            <AlternatingRowStyle Width="150px" />
            <RowStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px" />
        </ext:ExtendedGridView>



    </fieldset>
</asp:Content>
