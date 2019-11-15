<%@ Page Title="SqlQuery" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SqlQuery.aspx.cs" Inherits="WebApp.SamplePages.SqlQuery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Using sqlquery: non pkey queries</h1>
    <div class="offset-2">
        <asp:Label ID="Label1" runat="server" Text="Select a category: "></asp:Label>
        <asp:DropDownList ID="CategoryList" runat="server"></asp:DropDownList>
        <asp:Button ID="Fetch" runat="server" Text="Fetch" CausesValidation="false"/>
        <br /><br />
        <asp:Label ID="MessageLabel" runat="server"></asp:Label>
        <br /><br />
        <asp:GridView ID="ProductList" runat="server"></asp:GridView>&nbsp;&nbsp;
    </div>
</asp:Content>
