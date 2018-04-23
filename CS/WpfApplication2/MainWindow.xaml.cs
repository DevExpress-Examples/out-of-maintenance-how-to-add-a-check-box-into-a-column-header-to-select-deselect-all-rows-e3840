using System;
using System.Windows;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;

namespace WpfApplication2
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private ViewModel viewModel;

		public MainWindow()
		{
			DataContext = new ViewModel();
			InitializeComponent();
		}

		private void grid_CustomUnboundColumnData(object sender, GridColumnDataEventArgs e)
		{
			if ( e.Column.FieldName == "Selected" )
			{
				Guid key = (Guid)e.GetListSourceFieldValue("Id");

				if ( e.IsGetData )
					e.Value = GetIsSelected(key);

				if ( e.IsSetData )
					SetIsSelected(key, (bool)e.Value);
			}
		}

		private bool GetIsSelected(Guid key)
		{
			bool isSelected;

			viewModel = (ViewModel)this.DataContext;
			if ( viewModel.SelectedValues.TryGetValue(key, out isSelected) )
				return isSelected;

			return false;
		}

		private void SetIsSelected(Guid key, bool value)
		{
			if ( value )
				viewModel.SelectedValues[key] = value;
			else
				viewModel.SelectedValues.Remove(key);

			viewModel.RaisePropertyChanged("SelectedValues");
		}

		private void BtnInvert_Click(object sender, RoutedEventArgs e)
		{
			for ( int i = 0; i < viewModel.List.Count; i++ )
			{
				bool newIsSelected = !GetIsSelected(viewModel.List[i].Id);
				int rowHandle = grid.GetRowHandleByListIndex(i);
				grid.SetCellValue(rowHandle, "Selected", newIsSelected);
			}
		}

		private void BtnGetSelected_Click(object sender, RoutedEventArgs e)
		{
			string selectedIds = string.Empty;
			foreach ( Guid key in viewModel.SelectedValues.Keys )
				selectedIds += string.Format("{0}\n", key);

			string caption = string.Format("Selected rows (Total: {0})", viewModel.SelectedValues.Count);
			MessageBox.Show(selectedIds, caption, MessageBoxButton.OK);
		}

		private void CheckEdit_EditValueChanged(object sender, EditValueChangedEventArgs e)
		{
			if ( e.NewValue == null )
				return;

			bool isChecked = (bool)e.NewValue;
			if ( !isChecked )
			{
				for ( int i = 0; i < grid.VisibleRowCount; i++ )
				{
					int rowHandle = grid.GetRowHandleByVisibleIndex(i);
					grid.SetCellValue(rowHandle, "Selected", false);
					grid.RefreshRow(rowHandle);		
				}
			}
			else
			{
				for ( int i = 0; i < grid.VisibleRowCount; i++ )
				{
					int rowHandle = grid.GetRowHandleByVisibleIndex(i);
					grid.SetCellValue(rowHandle, "Selected", true);
					grid.RefreshRow(rowHandle);
				}
			}
			
		}

		private void view_CellValueChanging(object sender, CellValueChangedEventArgs e)
		{
			if (e.Column.FieldName == "Selected" )
				((TableView)sender).PostEditor();
		}
	}
}
