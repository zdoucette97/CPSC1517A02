<%@ Page Title="SqlQuery" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SqlQuery.aspx.cs" Inherits="WebApp.SamplePages.SqlQuery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Using sqlquery: non pkey queries</h1>
    <div class="offset-2">
        <asp:Label ID="Label1" runat="server" Text="Select a category: "></asp:Label>
        <asp:DropDownList ID="CategoryList" runat="server"></asp:DropDownList>
        <asp:Button ID="Fetch" runat="server" Text="Fetch" CausesValidation="false"/>
        <br /><br />
        <asp:Label ID="MessageLabel" runat="server"></asp:Label>
        <br />
        <asp:GridView ID="ProductList" runat="server" AutoGenerateColumns="False"
            cssclass="table table-striped" gridlines="Both" BorderColor="Violet" BorderStyle="None" PagerSettings-Mode="NumericFirstLast" AllowPaging="False" PageSize="5" OnPageIndexChanging="ProductList_PageIndexChanging">

            <Columns>
                <asp:CommandField SelectText="View" ShowSelectButton="true" ButtonType="Button" CausesValidation="false" />

                <!--sample pages msising stuff-->
                <asp:TemplateField HeaderText="ID">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                        <asp:Label ID="ProductID" runat="server" Text='<%#Eval("ProductID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Product">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    <ItemTemplate>
                        <!-- this is where your reference to the data on your record is placed (Eval() or Bind())-->
                        <asp:Label ID="ProductName" runat="server" Text='<%#Eval("ProductName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Qty/Unit">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    <ItemTemplate>
                        <asp:Label ID="QuantityPerUnit" runat="server" Text='<%# Eval("QuantityPerUnit") == null ? "Unknown" : Eval("QuantityPerUnit")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Price($)">
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    <ItemTemplate>
                        <asp:Label ID="Price" runat="server" Text='<%# string.Format("{0:0.00}", Eval("Price"))%>'></asp:Label>
                        <!--string.Format("{0:MM, DD,YYYY}",) formatting for dates can also work-->
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Disc">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                        <asp:CheckBox ID="Discontinued" runat="server" checked='<%#Eval("Discontinued") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                whatever string you use is printed if there is no data to display. no tags are necessary
            </EmptyDataTemplate>
        </asp:GridView>&nbsp;&nbsp;
    </div>
</asp:Content>
