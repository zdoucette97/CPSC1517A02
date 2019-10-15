<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HelloWorld.aspx.cs" Inherits="WebApp.SamplePages.HelloWorld" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Hello World</h1>
    <asp:Label ID="HelloText" runat="server" Text="Howdy"></asp:Label>
    <asp:Button ID="Submit" runat="server" Text="Submit" OnClick="Submit_Click" />
</asp:Content>
