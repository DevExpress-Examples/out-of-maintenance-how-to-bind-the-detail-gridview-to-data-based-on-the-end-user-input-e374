Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.Data
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports DevExpress.Web.ASPxGridView
Imports DevExpress.Web.ASPxTabControl

Partial Public Class Delayed_Detail_DataBinding
	Inherits System.Web.UI.Page

	Protected Sub relatedProducts_CustomCallback(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs)

		Dim g_relatedProducts As ASPxGridView = CType(sender, ASPxGridView)
		Dim visibleIndex As Integer = Integer.Parse(e.Parameters)
		Dim pc As ASPxPageControl = CType(grid.FindDetailRowTemplateControl(visibleIndex, "ASPxPageControl1"), ASPxPageControl)
		Dim editor As HtmlInputText = CType(pc.TabPages(0).FindControl("SearchString"), HtmlInputText)

		Dim searchString As String = editor.Value

		Dim selectCommand As String = "select * from [Products] where (([CategoryID]=" & g_relatedProducts.GetMasterRowKeyValue().ToString() & ") and ([ProductName] Like '%" & searchString & "%'))"
		Session(g_relatedProducts.GetMasterRowKeyValue().ToString()) = selectCommand
		AccessDataSource2.SelectCommand = selectCommand
		Dim view As DataView = CType(AccessDataSource2.Select(DataSourceSelectArguments.Empty), DataView)

		g_relatedProducts.DataSource = view
		g_relatedProducts.DataBind()
	End Sub

	Protected Sub g_relatedProducts_BeforePerformDataSelect(ByVal sender As Object, ByVal e As System.EventArgs)
		Dim gridView As ASPxGridView = CType(sender, ASPxGridView)
	If gridView.DataSource IsNot Nothing Then
		Return
	End If
		If Session(gridView.GetMasterRowKeyValue().ToString()) IsNot Nothing Then
			Dim selectCommand As String = CStr(Session(gridView.GetMasterRowKeyValue().ToString()))
			AccessDataSource2.SelectCommand = selectCommand
			Dim view As DataView = CType(AccessDataSource2.Select(DataSourceSelectArguments.Empty), DataView)
			gridView.DataSource = view
			gridView.DataBind()
		End If
	End Sub

	Protected Sub grid_HtmlRowCreated(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs)
		If e.RowType = GridViewRowType.Detail Then
			Dim gridView As ASPxGridView = CType(sender, ASPxGridView)
			Dim pc As ASPxPageControl = CType(gridView.FindDetailRowTemplateControl(e.VisibleIndex, "ASPxPageControl1"), ASPxPageControl)
			Dim detailGridView As ASPxGridView = CType(pc.TabPages(0).FindControl("g_relatedProducts"), ASPxGridView)
			detailGridView.ClientInstanceName = "detailGridView" & e.VisibleIndex.ToString()
			Dim button As Button = CType(pc.TabPages(0).FindControl("Button1"), Button)
			button.OnClientClick = "return OnButtonClick(" & e.VisibleIndex.ToString() & ")"
			Dim selectC As Object = Session(detailGridView.GetMasterRowKeyValue().ToString())
			If selectC IsNot Nothing Then
				Dim editor As HtmlInputText = CType(pc.TabPages(0).FindControl("SearchString"), HtmlInputText)
				Dim selectCommand As String = selectC.ToString()
				Dim indexStart As Integer = selectCommand.IndexOf("%")
				Dim indexFinish As Integer = selectCommand.IndexOf("%", indexStart + 1)
				editor.Value = selectCommand.ToString().Substring(indexStart + 1, indexFinish - indexStart - 1)
			End If
		End If
	End Sub
End Class
