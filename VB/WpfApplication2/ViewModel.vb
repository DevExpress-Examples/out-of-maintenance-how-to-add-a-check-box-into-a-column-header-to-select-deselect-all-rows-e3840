Imports System
Imports System.Collections.ObjectModel
Imports DevExpress.Mvvm

Namespace WpfApplication2
	Public Class ViewModel
		Inherits ViewModelBase

		Public Property List() As ObservableCollection(Of TestData)
			Get
				Return GetValue(Of ObservableCollection(Of TestData))()
			End Get
			Set(ByVal value As ObservableCollection(Of TestData))
				SetValue(value)
			End Set
		End Property

		Public Sub New()
			List = New ObservableCollection(Of TestData)()
			GenerateData(20)
		End Sub

		Private Sub GenerateData(ByVal objectCount As Integer)
			For i As Integer = 0 To objectCount - 1
				List.Add(New TestData() With {
					.Id = Guid.NewGuid(),
					.Number = i
				})
			Next i
		End Sub
	End Class
	Public Class TestData
		Public Property Id() As Guid
		Public Property Number() As Integer
		Public Property IsChecked() As Boolean
	End Class
End Namespace