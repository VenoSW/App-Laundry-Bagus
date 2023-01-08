Imports MySql.Data.MySqlClient

Public Class Daftar_Cucian
    Public Sub TampilDataGrid()
        'menampilkan tabel database
        DA = New MySqlDataAdapter("SELECT tb_transaksi.Faktur, tb_transaksi.Tanggal_Masuk AS 'Tanggal Masuk', tb_customer.Nama, tb_mesincuci.Jumlah_MesinCuci AS 'Jumlah Mesin Cuci', concat('Rp. ', format(tb_transaksi.Harga,0, 'id_ID')) as 'Harga', concat('Rp. ', format(tb_transaksi.DP,0, 'id_ID')) as 'DP', concat('Rp. ', format(tb_transaksi.Total_Harga,0, 'id_ID')) as 'Total Harga', tb_transaksi.Keterangan FROM tb_transaksi INNER JOIN tb_mesincuci on tb_transaksi.ID_MesinCuci = tb_mesincuci.ID_MesinCuci INNER JOIN tb_customer on tb_transaksi.ID_Customer = tb_customer.ID_Customer WHERE tb_transaksi.Keterangan = 'Belum Lunas'", CONN)
        DS = New DataSet
        DA.Fill(DS, "daftar_cucian")
        dgdaftarcucian.DataSource = (DS.Tables("daftar_cucian"))
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then
            TextBox1.ReadOnly = False
            TextBox2.ReadOnly = True
        Else
            TextBox2.ReadOnly = False
            TextBox1.ReadOnly = True
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton1.Checked = True Then
            TextBox2.ReadOnly = False
            TextBox1.ReadOnly = True
        Else
            TextBox1.ReadOnly = False
            TextBox2.ReadOnly = True
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Call koneksi()
        Dim carifaktur As String = "SELECT tb_transaksi.Faktur, tb_transaksi.Tanggal_Masuk AS 'Tanggal Masuk', tb_customer.Nama, tb_mesincuci.Jumlah_MesinCuci AS 'Jumlah Mesin Cuci', tb_transaksi.Harga, tb_transaksi.DP, tb_transaksi.Total_Harga AS 'Total Harga', tb_transaksi.Keterangan FROM tb_transaksi INNER JOIN tb_mesincuci on tb_transaksi.ID_MesinCuci = tb_mesincuci.ID_MesinCuci INNER JOIN tb_customer on tb_transaksi.ID_Customer = tb_customer.ID_Customer WHERE tb_transaksi.Keterangan = 'Belum Lunas' AND Faktur like '%" & TextBox1.Text & "%'"
        CMD = New MySqlCommand(carifaktur, CONN)
        RD = CMD.ExecuteReader
        RD.Read()
        If RD.HasRows Then
            Call koneksi()
            DA = New MySqlDataAdapter(carifaktur, CONN)
            DS = New DataSet
            DA.Fill(DS, "ketemu")
            dgdaftarcucian.DataSource = DS.Tables("ketemu")
        End If
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        Call koneksi()
        Dim caricustomer As String = "SELECT tb_transaksi.Faktur, tb_transaksi.Tanggal_Masuk AS 'Tanggal Masuk', tb_customer.Nama, tb_mesincuci.Jumlah_MesinCuci AS 'Jumlah Mesin Cuci', tb_transaksi.Harga, tb_transaksi.DP, tb_transaksi.Total_Harga AS 'Total Harga', tb_transaksi.Keterangan FROM tb_transaksi INNER JOIN tb_mesincuci on tb_transaksi.ID_MesinCuci = tb_mesincuci.ID_MesinCuci INNER JOIN tb_customer on tb_transaksi.ID_Customer = tb_customer.ID_Customer WHERE tb_transaksi.Keterangan = 'Belum Lunas' AND Nama like '%" & TextBox2.Text & "%'"
        CMD = New MySqlCommand(caricustomer, CONN)
        RD = CMD.ExecuteReader
        RD.Read()
        If RD.HasRows Then
            Call koneksi()
            DA = New MySqlDataAdapter(caricustomer, CONN)
            DS = New DataSet
            DA.Fill(DS, "ketemu")
            dgdaftarcucian.DataSource = DS.Tables("ketemu")
        End If
    End Sub
End Class
