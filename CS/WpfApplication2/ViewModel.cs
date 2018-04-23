using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace WpfApplication2
{
	public class ViewModel : INotifyPropertyChanged
	{
		public ObservableCollection<TestData> List { get; set; }
		public Dictionary<Guid, bool> SelectedValues { get; set; }

		public ViewModel()
		{
			SelectedValues = new Dictionary<Guid, bool>();
			List = new ObservableCollection<TestData>();

			GenerateData(20);
		}

		private void GenerateData(int objectCount)
		{
			for ( int i = 0; i < objectCount; i++ )
				List.Add(new TestData() { Id = Guid.NewGuid(), Number = i });
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public virtual void RaisePropertyChanged(string propertyName)
		{
			if ( PropertyChanged != null )
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}


	public class TestData
	{
		public Guid Id { get; set; }
		public int Number { get; set; }
	}
}

