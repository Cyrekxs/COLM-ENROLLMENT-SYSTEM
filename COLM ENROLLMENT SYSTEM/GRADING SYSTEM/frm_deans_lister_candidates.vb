Imports Microsoft.Reporting.WinForms

Public Class frm_deans_lister_candidates

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim DS As New DS_DeansLister
        Dim DR As DataRow

        With DS.Tables("DT_DeansLister")
            For i = 0 To DataGridView1.Rows.Count - 1
                DR = .NewRow
                DR("StudentNo") = DataGridView1.Rows(i).Cells(0).Value
                DR("StudentName") = DataGridView1.Rows(i).Cells(1).Value
                DR("StudentCourse") = DataGridView1.Rows(i).Cells(2).Value
                DR("Average") = DataGridView1.Rows(i).Cells(3).Value
                .Rows.Add(DR)
            Next
        End With

        Dim Source1 As New ReportDataSource("DSDeansLister", DS.Tables(0))

        With frm_rdlc_report_viewer
            .ReportViewer1.LocalReport.DataSources.Clear()
            .ReportViewer1.LocalReport.DataSources.Add(Source1)
            .ReportViewer1.LocalReport.ReportEmbeddedResource = "COLM_ENROLLMENT_SYSTEM.rpt_deans_lister.rdlc"
            .ReportViewer1.RefreshReport()
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
        End With
    End Sub
End Class