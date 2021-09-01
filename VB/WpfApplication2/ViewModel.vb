Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Collections.ObjectModel

Namespace WpfApplication2
	Public Class ViewModel
		Implements INotifyPropertyChanged

		Public Property List() As ObservableCollection(Of TestData)
		Public Property SelectedValues() As Dictionary(Of Guid, Boolean)

		Public Sub New()
			SelectedValues = New Dictionary(Of Guid, Boolean)()
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

		Public Event PropertyChanged As PropertyChangedEventHandler
		Public Overridable Sub RaisePropertyChanged(ByVal propertyName As String)
			RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
		End Sub
	End Class


	Public Class TestData
		Public Property Id() As Guid
		Public Property Number() As Integer
	End Class
End Namespace

