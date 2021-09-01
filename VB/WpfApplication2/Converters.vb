Imports System
Imports System.Collections
Imports System.Globalization
Imports System.Windows.Data

Namespace WpfApplication2
	Public Class CollectionToIsCheckedConverter
		Implements IMultiValueConverter

		Private dataContext As ViewModel

		Public Function Convert(ByVal values() As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IMultiValueConverter.Convert
			Dim collection As ICollection = TryCast(values(0), ICollection)
			dataContext = TryCast(values(1), ViewModel)

			If collection Is Nothing OrElse dataContext Is Nothing Then
				Throw New NotSupportedException()
			End If

			If collection.Count = 0 Then
				Return False
			End If

			If collection.Count = dataContext.List.Count Then
				Return True
			End If

			Return Nothing
		End Function

		Public Function ConvertBack(ByVal value As Object, ByVal targetType() As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object() Implements IMultiValueConverter.ConvertBack
			Throw New NotSupportedException()
		End Function
	End Class

	Public Class CollectionToIsThreeStateConverter
		Implements IMultiValueConverter

		Public Function Convert(ByVal values() As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IMultiValueConverter.Convert
			Dim collection As ICollection = TryCast(values(0), ICollection)
			Dim dataContext As ViewModel = TryCast(values(1), ViewModel)

			If collection Is Nothing OrElse dataContext Is Nothing Then
				Throw New NotSupportedException()
			End If

			If collection.Count = 0 OrElse collection.Count = dataContext.List.Count Then
				Return False
			End If

			Return True
		End Function

		Public Function ConvertBack(ByVal value As Object, ByVal targetType() As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object() Implements IMultiValueConverter.ConvertBack
			Throw New NotSupportedException()
		End Function
	End Class
End Namespace

