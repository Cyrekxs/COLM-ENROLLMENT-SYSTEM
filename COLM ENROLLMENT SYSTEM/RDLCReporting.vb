Public Class RDLCReporting

    Private Sub RDLCReporting_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ReportViewer1.LocalReport.EnableExternalImages = True
        Me.ReportViewer1.RefreshReport()
    End Sub
End Class