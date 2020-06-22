Public Class frm_report

    Private Sub frm_report_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.ReportViewer1.RefreshReport()
    End Sub

    Private Sub ReportViewer1_Load(sender As Object, e As EventArgs) Handles ReportViewer1.Load

    End Sub

    Private Sub ReportViewer1_Print(sender As Object, e As ReportPrintEventArgs) Handles ReportViewer1.Print
        Select Case ReportViewer1.LocalReport

        End Select
    End Sub
End Class