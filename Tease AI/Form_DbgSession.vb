Imports Tai.Common

Public Class dbgSessionForm

	Private Sub dbgForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		TmrUpdate.Start()
		PropertyGrid1.SelectedObject = My.Application.Session
	End Sub

	Private Sub TmrUpdate_Tick(sender As Object, e As EventArgs) Handles TmrUpdate.Tick
		' Check if Session has been disposed and if so update object reference.
		If PropertyGrid1.SelectedObject IsNot Nothing _
			AndAlso TypeOf (PropertyGrid1.SelectedObject) Is SessionState Then
			PropertyGrid1.SelectedObject = My.Application.Session
		End If

		PropertyGrid1.Refresh()
	End Sub
End Class