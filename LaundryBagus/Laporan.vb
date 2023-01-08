Public Class Laporan

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Call Form1.Laporan_Cucian2.BringToFront()

        Call koneksi()
        Call Form1.Laporan_Cucian2.TampilDataGrid()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Call Form1.Laporan_Pemasukan1.BringToFront()

        Call koneksi()
        Call Form1.Laporan_Pemasukan1.TampilDataGrid()
    End Sub
End Class
