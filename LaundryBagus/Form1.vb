Imports MySql.Data.MySqlClient

Public Class Form1

    Public Sub dashboard()
        Call koneksi()
        CMD = New MySqlCommand("SELECT COUNT(Tanggal_Masuk) AS 'Jumlah_Cucian' FROM tb_transaksi WHERE DATE_FORMAT(Tanggal_Masuk, '%Y-%m-%d') = '" & Format(Now, "yyyy-MM-dd") & "'", CONN)
        RD = CMD.ExecuteReader
        RD.Read()
        If RD.HasRows Then
            Label1.Text = RD.Item("Jumlah_Cucian")
        End If

        Call koneksi()
        CMD = New MySqlCommand("SELECT COUNT(Tanggal_Keluar) AS 'Jumlah_Pengembalian' FROM tb_transaksi WHERE DATE_FORMAT(Tanggal_Keluar, '%Y-%m-%d') = '" & Format(Now, "yyyy-MM-dd") & "'", CONN)
        RD = CMD.ExecuteReader
        RD.Read()
        If RD.HasRows Then
            Label4.Text = RD.Item("Jumlah_Pengembalian")
        End If

        Call koneksi()
        CMD = New MySqlCommand("SELECT COUNT(ID_Customer) AS 'Jumlah_Customer' FROM tb_customer", CONN)
        RD = CMD.ExecuteReader
        RD.Read()
        If RD.HasRows Then
            Label6.Text = RD.Item("Jumlah_Customer")
        End If
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call dashboard()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Button1.Enabled = False
        Button2.Enabled = True
        Button3.Enabled = True
        Button4.Enabled = True
        Button5.Enabled = True

        Cucian_Masuk2.BringToFront()
        Call koneksi()
        Call Cucian_Masuk2.TampilDataGrid()
        Call Cucian_Masuk2.serahkosong()
        Call Cucian_Masuk2.kodefaktur()
        Call Cucian_Masuk2.hargapermsincuci()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Button1.Enabled = True
        Button2.Enabled = False
        Button3.Enabled = True
        Button4.Enabled = True
        Button5.Enabled = True

        Cucian_Keluar2.BringToFront()
        Call koneksi()
        Call Cucian_Keluar2.TampilDataGrid()
        Call Cucian_Keluar2.pengembaliankosong()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Button1.Enabled = True
        Button2.Enabled = True
        Button3.Enabled = False
        Button4.Enabled = True
        Button5.Enabled = True

        Daftar_Cucian2.BringToFront()
        Call koneksi()
        Call Daftar_Cucian2.TampilDataGrid()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Button1.Enabled = True
        Button2.Enabled = True
        Button3.Enabled = True
        Button4.Enabled = False
        Button5.Enabled = True

        Customer2.BringToFront()
        Call koneksi()
        Call Customer2.TampilDataGrid()
        Call Customer2.kosongcustomer()
        Call Customer2.kodecustomer()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Button1.Enabled = True
        Button2.Enabled = True
        Button3.Enabled = True
        Button4.Enabled = True
        Button5.Enabled = False

        Laporan2.BringToFront()
    End Sub
End Class
