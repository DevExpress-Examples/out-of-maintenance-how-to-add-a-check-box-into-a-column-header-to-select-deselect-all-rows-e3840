using System;
using System.Collections.ObjectModel;
using DevExpress.Mvvm;

namespace WpfApplication2 {
	public class ViewModel : ViewModelBase {
		public ObservableCollection<TestData> List {
			get { return GetValue<ObservableCollection<TestData>>(); }
			set { SetValue(value); }
		}

		public ViewModel() {
			List = new ObservableCollection<TestData>();
			GenerateData(20);
		}

		private void GenerateData(int objectCount) {
			for (int i = 0; i < objectCount; i++)
				List.Add(new TestData() { Id = Guid.NewGuid(), Number = i });
		}
	}
	public class TestData {
		public Guid Id { get; set; }
		public int Number { get; set; }
		public bool IsChecked { get; set; }
	}
}