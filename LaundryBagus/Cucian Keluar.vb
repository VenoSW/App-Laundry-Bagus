Imports MySql.Data.MySqlClient

Public Class Cucian_Keluar
    Public Sub TampilDataGrid()
        'menampilkan tabel database
        DA = New MySqlDataAdapter("SELECT tb_transaksi.Faktur, tb_transaksi.Tanggal_Masuk AS 'Tanggal Masuk', tb_customer.Nama, tb_mesincuci.Jumlah_MesinCuci AS 'Jumlah Mesin Cuci', concat('Rp. ', format(tb_transaksi.Harga,0, 'id_ID')) as 'Harga', concat('Rp. ', format(tb_transaksi.DP,0, 'id_ID')) as 'DP', concat('Rp. ', format(tb_transaksi.Total_Harga,0, 'id_ID')) as 'Total Harga', tb_transaksi.Keterangan FROM tb_transaksi INNER JOIN tb_mesincuci on tb_transaksi.ID_MesinCuci = tb_mesincuci.ID_MesinCuci INNER JOIN tb_customer on tb_transaksi.ID_Customer = tb_customer.ID_Customer WHERE tb_transaksi.Keterangan = 'Belum Lunas'", CONN)
        DS = New DataSet
        DA.Fill(DS, "cucian_masuk")
        dgpengembaliancucian.DataSource = (DS.Tables("cucian_masuk"))
    End Sub

    Public Sub pengembaliankosong()
        'mengkosongkan textbox pengembalian cucian
        txtkembalifaktur.Text = ""
        txtkembalicustomer.Text = ""
        txtkembalimesincuci.Text = ""
        txtkembaliharga.Text = ""
        txtkembalidp.Text = ""
        txtkembalitotal.Text = ""
        cmbkembaliket.Text = ""
        txtkembalitanggal.Text = ""
    End Sub

    Private Sub Cucian_Keluar_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        koneksi()
        TampilDataGrid()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Label8.Text = Format(Now, "yyyy-MM-dd")
        Label9.Text = DateTime.Now.ToString("HH:mm:ss")
    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        pengembaliankosong()
    End Sub

    Private Sub btnsimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsimpan.Click
        If txtkembalifaktur.Text = "" Or txtkembalicustomer.Text = "" Or txtkembalimesincuci.Text = "" Or txtkembaliharga.Text = "" Or txtkembalidp.Text = "" Or cmbkembaliket.Text = "" Or txtkembalitotal.Text = "" Then
            MsgBox("Pilih Data Pengembalian Cucian", MsgBoxStyle.Critical)
        End If
        If cmbkembaliket.Text = "Belum Lunas" Then
            MsgBox("Harap Lunaskan Pembayaran")
        Else
            'menyimpan data
            Call koneksi()

            Dim Updatetb_keterangan As String = "UPDATE tb_transaksi SET Keterangan = '" & cmbkembaliket.Text & "', Tanggal_Keluar = '" & Label8.Text & " " & Label9.Text & "' WHERE Faktur = '" & txtkembalifaktur.Text & "'"
            CMD = New MySqlCommand(Updatetb_keterangan, CONN)
            CMD.ExecuteNonQuery()

            Call Form1.dashboard()
            koneksi()
            TampilDataGrid()
            pengembaliankosong()
        End If
    End Sub

    Private Sub dgpengembaliancucian_CellMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgpengembaliancucian.CellMouseDoubleClick
        txtkembalifaktur.Text = dgpengembaliancucian.Rows(e.RowIndex).Cells(0).Value
        txtkembalitanggal.Text = dgpengembaliancucian.Rows(e.RowIndex).Cells(1).Value
        txtkembalicustomer.Text = dgpengembaliancucian.Rows(e.RowIndex).Cells(2).Value
        txtkembalimesincuci.Text = dgpengembaliancucian.Rows(e.RowIndex).Cells(3).Value
        txtkembaliharga.Text = dgpengembaliancucian.Rows(e.RowIndex).Cells(4).Value
        txtkembalidp.Text = dgpengembaliancucian.Rows(e.RowIndex).Cells(5).Value
        txtkembalitotal.Text = dgpengembaliancucian.Rows(e.RowIndex).Cells(6).Value
        cmbkembaliket.Text = dgpengembaliancucian.Rows(e.RowIndex).Cells(7).Value
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Call koneksi()
        Dim caricustomer As String = "SELECT tb_transaksi.Faktur, tb_transaksi.Tanggal_Masuk AS 'Tanggal Masuk', tb_customer.Nama, tb_mesincuci.Jumlah_MesinCuci AS 'Jumlah Mesin Cuci', concat('Rp. ', format(tb_transaksi.Harga,0, 'id_ID')) as 'Harga', concat('Rp. ', format(tb_transaksi.DP,0, 'id_ID')) as 'DP', concat('Rp. ', format(tb_transaksi.Total_Harga,0, 'id_ID')) as 'Total Harga', tb_transaksi.Keterangan FROM tb_transaksi INNER JOIN tb_mesincuci on tb_transaksi.ID_MesinCuci = tb_mesincuci.ID_MesinCuci INNER JOIN tb_customer on tb_transaksi.ID_Customer = tb_customer.ID_Customer WHERE tb_transaksi.Keterangan = 'Belum Lunas' AND Nama like '%" & TextBox1.Text & "%'"
        CMD = New MySqlCommand(caricustomer, CONN)
        RD = CMD.ExecuteReader
        RD.Read()
        If RD.HasRows Then
            Call koneksi()
            DA = New MySqlDataAdapter(caricustomer, CONN)
            DS = New DataSet
            DA.Fill(DS, "ketemu")
            dgpengembaliancucian.DataSource = DS.Tables("ketemu")
        End If
    End Sub
End Class
