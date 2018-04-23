Imports Microsoft.VisualBasic
Imports System
Imports System.Windows
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Grid

Namespace WpfApplication2
	''' <summary>
	''' Interaction logic for MainWindow.xaml
	''' </summary>
	Partial Public Class MainWindow
		Inherits Window
		Private viewModel As ViewModel

		Public Sub New()
			DataContext = New ViewModel()
			InitializeComponent()
		End Sub

		Private Sub grid_CustomUnboundColumnData(ByVal sender As Object, ByVal e As GridColumnDataEventArgs)
			If e.Column.FieldName = "Selected" Then
				Dim key As Guid = CType(e.GetListSourceFieldValue("Id"), Guid)

				If e.IsGetData Then
					e.Value = GetIsSelected(key)
				End If

				If e.IsSetData Then
					SetIsSelected(key, CBool(e.Value))
				End If
			End If
		End Sub

		Private Function GetIsSelected(ByVal key As Guid) As Boolean
			Dim isSelected As Boolean

			viewModel = CType(Me.DataContext, ViewModel)
			If viewModel.SelectedValues.TryGetValue(key, isSelected) Then
				Return isSelected
			End If

			Return False
		End Function

		Private Sub SetIsSelected(ByVal key As Guid, ByVal value As Boolean)
			If value Then
				viewModel.SelectedValues(key) = value
			Else
				viewModel.SelectedValues.Remove(key)
			End If

			viewModel.RaisePropertyChanged("SelectedValues")
		End Sub

		Private Sub BtnInvert_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
			For i As Integer = 0 To viewModel.List.Count - 1
				Dim newIsSelected As Boolean = Not GetIsSelected(viewModel.List(i).Id)
				Dim rowHandle As Integer = grid.GetRowHandleByListIndex(i)
				grid.SetCellValue(rowHandle, "Selected", newIsSelected)
			Next i
		End Sub

		Private Sub BtnGetSelected_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
			Dim selectedIds As String = String.Empty
			For Each key As Guid In viewModel.SelectedValues.Keys
				selectedIds &= String.Format("{0}" & Constants.vbLf, key)
			Next key

			Dim caption As String = String.Format("Selected rows (Total: {0})", viewModel.SelectedValues.Count)
			MessageBox.Show(selectedIds, caption, MessageBoxButton.OK)
		End Sub

		Private Sub CheckEdit_EditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
			If e.NewValue Is Nothing Then
				Return
			End If

			Dim isChecked As Boolean = CBool(e.NewValue)
			If (Not isChecked) Then
				For i As Integer = 0 To grid.VisibleRowCount - 1
					Dim rowHandle As Integer = grid.GetRowHandleByVisibleIndex(i)
					grid.SetCellValue(rowHandle, "Selected", False)
					grid.RefreshRow(rowHandle)
				Next i
			Else
				For i As Integer = 0 To grid.VisibleRowCount - 1
					Dim rowHandle As Integer = grid.GetRowHandleByVisibleIndex(i)
					grid.SetCellValue(rowHandle, "Selected", True)
					grid.RefreshRow(rowHandle)
				Next i
			End If

		End Sub

		Private Sub view_CellValueChanging(ByVal sender As Object, ByVal e As CellValueChangedEventArgs)
			If e.Column.FieldName = "Selected" Then
				CType(sender, TableView).PostEditor()
			End If
		End Sub
	End Class
End Namespace
