<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="KDB_RENEW.Pages.User" %>

<%@ Register Assembly="ExtendedGridView" Namespace="ExtendedControls" TagPrefix="ext" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   
    <ext:ExtendedGridView ID="GridView_User" runat="server" AutoGenerateSelectButton="True"
        BorderStyle="None" CellPadding="10" GridLines="Horizontal"
        OnSelectedIndexChanged="GridView_User_SelectedIndexChanged">
    </ext:ExtendedGridView>
    <br />
    <asp:Label ID="Label_Name" runat="server" Text="Name: "
        Style="font-weight: 700"></asp:Label><br />
    <br />
    <asp:DropDownList ID="DropDownList_Rechte" runat="server" Enabled="False"  CssClass="form-control"
        OnSelectedIndexChanged="DropDownList_Rechte_SelectedIndexChanged"
        >
        <asp:ListItem>Admin</asp:ListItem>
        <asp:ListItem Selected="True"></asp:ListItem>
        <asp:ListItem>Lesen</asp:ListItem>
        <asp:ListItem>Schreiben</asp:ListItem>
    </asp:DropDownList>

    <br />
    <asp:Button ID="Button_Speichern" runat="server" Text="Speichern" Enabled="False" class="btn btn-success"
        OnClick="Button_Speichern_Click" />
    <asp:Button ID="Button_Bearbeiten" runat="server" Text="Bearbeiten" Enabled="False" class="btn btn-warning"
        OnClick="Button_Bearbeiten_Click" />
    <asp:Button ID="Button_Löschen" runat="server" Text="Löschen" Enabled="False"  class="btn btn-danger"
        OnClick="Button_Löschen_Click" />
</asp:Content>
