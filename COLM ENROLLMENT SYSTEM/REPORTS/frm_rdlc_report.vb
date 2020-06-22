Public Class frm_rdlc_report_viewer

    Private Sub frm_rdlc_report_viewer_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub frm_rdlc_report_viewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ReportViewer1.RefreshReport()
    End Sub

    Private Sub ReportViewer1_Print(sender As Object, e As ReportPrintEventArgs) Handles ReportViewer1.Print
        'Select Case ReportViewer1.LocalReport.ReportEmbeddedResource
        '    Case "COLM_ENROLLMENT_SYSTEM.rtp_assessment_college.rdlc"
        '        MsgBox("Reportviewer detected that this is a college assessment report!")
        'End Select
    End Sub
End Class