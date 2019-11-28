<%@ Page Title="MultiRecord ODS" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MultiRecordODS.aspx.cs" Inherits="WebApp.SamplePages.MultiRecordODS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <div class="page-header">
        <h1>Product MultiRecord Object DataSource</h1>
    </div>

    <div class="row col-md-12">
        <div class="alert alert-warning">
            <blockquote style="font-style: italic">
                This illustrates a display of multiple records from a query.
                The parameter will be submitted from either a drop down list
                or a textbox. The resulting dataset is of the enity Product.
                The output will be displayed in a customer GridView.

                NOTE: There will be times when you must use codebehind with ODS.
                For exmaple, you can have validation inside the codebehind with the ODS 
            </blockquote>
        </div>
    </div>
    <div class="row">
        <asp:Literal ID="Literal1" runat="server" Text="Categories:"></asp:Literal>
        &nbsp;&nbsp;
        <asp:DropDownList ID="CategoryList" runat="server" DataSourceID="CategoryListODS" DataTextField="CategoryName" DataValueField="CategoryID"
            AppendDataBoundItems="true">
            <asp:ListItem Value="0"> select ...</asp:ListItem>
        </asp:DropDownList>
        &nbsp;&nbsp;
        <asp:LinkButton ID="FetchCategoryProducts" runat="server" OnClick="FetchCategoryProducts_Click">Fetch</asp:LinkButton>
        <br />
    </div>
   
    <div class="row">
        <asp:GridView ID="ProductList" runat="server"
            AutoGenerateColumns="False" OnSelectedIndexChanged="ProductList_SelectedIndexChanged"
            DataSourceID="ProductListODS" AllowPaging="True" PageSize="5" 
            CssClass="table table-striped" GridLines="Horizontal" BorderStyle="None">
              <Columns>
                <asp:CommandField SelectText="View" ShowSelectButton="True"></asp:CommandField>
                <asp:TemplateField HeaderText="ID">
                    <ItemTemplate>
                        <asp:Label ID="ProductID" runat="server" 
                            Text='<%# Eval("ProductID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Product">
                    <ItemTemplate>
                        <asp:Label ID="ProductName" runat="server" 
                            Text='<%# Eval("ProductName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Supplier">
                    <ItemTemplate>
                       <%--<asp:Label ID="SupplierID" runat="server" 
                            Text='<%# Eval("SupplierID") %>'></asp:Label>--%>
                        <asp:DropDownList ID="SupplierList" runat="server" 
                            DataSourceID="SupplierListODS" DataTextField="CompanyName" DataValueField="SupplierID"
                            enabled="false" selectedvalue="<%# Eval("SupplierID") %>">
                            <!--once displayed enabled=false so user cannot change it-->
                            <!--selectedvalue ensures that the suppliers correspond with their ID -->
                        </asp:DropDownList>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Category">
                    <ItemTemplate>
                        <%--<asp:Label ID="CategoryID" runat="server" 
                            Text='<%# Eval("CategoryID") %>'></asp:Label>--%>
                        <asp:DropDownList ID="CategoryListGV" runat="server" 
                            DataSourceID="CategoryListODS" DataTextField="CategoryName" DataValueField="CategoryID"
                            enabled="false" selectedvalue="<%# Eval("CategoryID") %>">
                            <!--GV distinguishes the categorylist as the one from the gridview-->
                        </asp:DropDownList>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="($)">
                    <ItemTemplate>
                        <asp:Label ID="UnitPrice" runat="server"
                            Text='<%# string.Format("{0:0.00}",Eval("UnitPrice")) %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Disc.">
                    <ItemTemplate>
                        <asp:CheckBox ID="Discontinued" runat="server"
                             Checked='<%# Eval("Discontinued") %>' 
                             Enabled="false"/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>No data to display at this time</EmptyDataTemplate>
          <%--Using emptydatatemplate, you do not need a validation for the fetchcategoryproducts_click--%>
        </asp:GridView>
        <br /><br />
        <asp:Label ID="Message" runat="server" ></asp:Label>
    </div>
    <!--exact same thing as BindCategoryList() method from the codebehind-->
    <asp:ObjectDataSource ID="CategoryListODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Categories_List" TypeName="NorthwindSystem.BLL.CategoryController">

    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ProductListODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Products_FindByCategory" TypeName="NorthwindSystem.BLL.ProductController">
        <SelectParameters>
            <asp:ControlParameter ControlID="CategoryList" PropertyName="SelectedValue" DefaultValue="0" Name="categoryid" Type="Int32"></asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="SupplierListODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Supplier_List" TypeName="NorthwindSystem.BLL.SupplierController"></asp:ObjectDataSource>
</asp:Content>
