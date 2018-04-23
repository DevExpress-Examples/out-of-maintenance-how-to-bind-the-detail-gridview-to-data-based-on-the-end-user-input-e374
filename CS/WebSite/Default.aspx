<%-- BeginRegion PageSetup --%>
<%@ Page Language="C#" AutoEventWireup="false" CodeFile="Default.aspx.cs" Inherits="Delayed_Detail_DataBinding" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v8.1"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v8.1"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v8.1" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v8.1" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v8.1" Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dxtc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%-- EndRegion --%>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>How to bind the detail GridView to data based on the end-user input</title>
    <script type="text/javascript">
        function OnButtonClick(visibleIndex) {
            var gridView = eval('window.detailGridView' + visibleIndex);
            gridView.PerformCallback(visibleIndex);
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    
<dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" KeyFieldName="CategoryID" AutoGenerateColumns="False" Width="900px" runat="server" DataSourceID="AccessDataSource1" OnHtmlRowCreated="grid_HtmlRowCreated">
    <Columns>            
        <dxwgv:GridViewDataTextColumn FieldName="CategoryID" ReadOnly="True" VisibleIndex="0">
            <EditFormSettings Visible="False" />
        </dxwgv:GridViewDataTextColumn>
        <dxwgv:GridViewDataTextColumn FieldName="CategoryName" VisibleIndex="1">
        </dxwgv:GridViewDataTextColumn>
        <dxwgv:GridViewDataTextColumn FieldName="Description" VisibleIndex="2">
        </dxwgv:GridViewDataTextColumn>
    </Columns>
    <SettingsPager PageSize=25></SettingsPager>
    <SettingsDetail ShowDetailRow="True" />
         <Templates>
             <DetailRow>
                <dxtc:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" Width='100%'>
                    <TabPages>
                        <dxtc:TabPage Name="relatedProducts" Text="Related Products">
                            <Controls>
                                    <div style="text-align:right;padding:10px;" runat="server">
                                        <asp:Panel ID="Panel2" runat="server" Width="325px" DefaultButton="Button1">

                                             SEARCH PRODUCT NAME:&nbsp;

                                            <input type='text' ID='SearchString' runat="server">
                                            <asp:Button ID="Button1" runat="server" Text="Go" />
                                            &nbsp;

                                        </asp:Panel>
                                    </div>
                                 <dxwgv:ASPxGridView ID="g_relatedProducts" runat="server" AutoGenerateColumns="False" Width="100%" OnCustomCallback="relatedProducts_CustomCallback" KeyFieldName="ProductID" OnBeforePerformDataSelect="g_relatedProducts_BeforePerformDataSelect">
                                     <Columns>       
                                         <dxwgv:GridViewDataTextColumn FieldName="ProductID" ReadOnly="True" VisibleIndex="0">
                                             <EditFormSettings Visible="False" />
                                         </dxwgv:GridViewDataTextColumn>
                                         <dxwgv:GridViewDataTextColumn FieldName="ProductName" VisibleIndex="1">
                                         </dxwgv:GridViewDataTextColumn>
                                         <dxwgv:GridViewDataTextColumn FieldName="SupplierID" VisibleIndex="2">
                                         </dxwgv:GridViewDataTextColumn>
                                         <dxwgv:GridViewDataTextColumn FieldName="CategoryID" VisibleIndex="3">
                                         </dxwgv:GridViewDataTextColumn>
                                         <dxwgv:GridViewDataTextColumn FieldName="QuantityPerUnit" VisibleIndex="4">
                                         </dxwgv:GridViewDataTextColumn>
                                         <dxwgv:GridViewDataTextColumn FieldName="UnitPrice" VisibleIndex="5">
                                         </dxwgv:GridViewDataTextColumn>
                                         <dxwgv:GridViewDataTextColumn FieldName="UnitsInStock" VisibleIndex="6">
                                         </dxwgv:GridViewDataTextColumn>
                                         <dxwgv:GridViewDataTextColumn FieldName="UnitsOnOrder" VisibleIndex="7">
                                         </dxwgv:GridViewDataTextColumn>
                                         <dxwgv:GridViewDataTextColumn FieldName="ReorderLevel" VisibleIndex="8">
                                         </dxwgv:GridViewDataTextColumn>
                                         <dxwgv:GridViewDataCheckColumn FieldName="Discontinued" VisibleIndex="9">
                                         </dxwgv:GridViewDataCheckColumn>
                                         <dxwgv:GridViewDataTextColumn FieldName="EAN13" VisibleIndex="10">
                                         </dxwgv:GridViewDataTextColumn>
                                     </Columns>
                                     <SettingsPager PageSize=25></SettingsPager>
                                     <SettingsDetail IsDetailGrid="True" />
                                 </dxwgv:ASPxGridView>
                                 
                              </Controls>
                              
                        </dxtc:TabPage>
                 </TabPages>
               </dxtc:ASPxPageControl>  
             </DetailRow>
         </Templates>
     </dxwgv:ASPxGridView>
        <asp:AccessDataSource ID="AccessDataSource1" runat="server" DataFile="~/App_Data/nwind.mdb"
            SelectCommand="SELECT * FROM [Categories]"></asp:AccessDataSource>
        &nbsp;
        <asp:AccessDataSource ID="AccessDataSource2" runat="server" DataFile="~/App_Data/nwind.mdb">
        </asp:AccessDataSource>


    </form>
</body>
</html>
