using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxTabControl;

public partial class Delayed_Detail_DataBinding : System.Web.UI.Page {

    protected void relatedProducts_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e) {

        ASPxGridView g_relatedProducts = (ASPxGridView)sender;
        int visibleIndex = int.Parse(e.Parameters);
        ASPxPageControl pc = (ASPxPageControl)grid.FindDetailRowTemplateControl(visibleIndex, "ASPxPageControl1");
        HtmlInputText editor = (HtmlInputText)pc.TabPages[0].FindControl("SearchString");

        string searchString = editor.Value;

        string selectCommand = "select * from [Products] where (([CategoryID]=" + g_relatedProducts.GetMasterRowKeyValue().ToString() + ") and ([ProductName] Like '%" + searchString + "%'))";
        Session[g_relatedProducts.GetMasterRowKeyValue().ToString()] = selectCommand;
        AccessDataSource2.SelectCommand = selectCommand;
        DataView view = (DataView)AccessDataSource2.Select(DataSourceSelectArguments.Empty);

        g_relatedProducts.DataSource = view;
        g_relatedProducts.DataBind();
    }

    protected void g_relatedProducts_BeforePerformDataSelect(object sender, System.EventArgs e) {
        ASPxGridView gridView = (ASPxGridView)sender;
	if(gridView.DataSource != null) return;
        if(Session[gridView.GetMasterRowKeyValue().ToString()] != null) {
            string selectCommand = (string)Session[gridView.GetMasterRowKeyValue().ToString()];
            AccessDataSource2.SelectCommand = selectCommand;
            DataView view = (DataView)AccessDataSource2.Select(DataSourceSelectArguments.Empty);
            gridView.DataSource = view;
            gridView.DataBind();
        }
    }

    protected void grid_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e) {
        if(e.RowType == GridViewRowType.Detail) {
            ASPxGridView gridView = (ASPxGridView)sender;
            ASPxPageControl pc = (ASPxPageControl)gridView.FindDetailRowTemplateControl(e.VisibleIndex, "ASPxPageControl1");
            ASPxGridView detailGridView = (ASPxGridView)pc.TabPages[0].FindControl("g_relatedProducts");
            detailGridView.ClientInstanceName = "detailGridView" + e.VisibleIndex.ToString();
            Button button = (Button)pc.TabPages[0].FindControl("Button1");
            button.OnClientClick = "return OnButtonClick(" + e.VisibleIndex.ToString() + ")";
            object selectC = Session[detailGridView.GetMasterRowKeyValue().ToString()];
            if(selectC != null) {
                HtmlInputText editor = (HtmlInputText)pc.TabPages[0].FindControl("SearchString");
                string selectCommand = selectC.ToString();
                int indexStart = selectCommand.IndexOf("%");
                int indexFinish = selectCommand.IndexOf("%", indexStart + 1);
                editor.Value = selectCommand.ToString().Substring(indexStart + 1, indexFinish - indexStart - 1);
            }
        }
    }
}
