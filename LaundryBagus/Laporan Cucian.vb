Imports MySql.Data.MySqlClient
Imports Microsoft.Reporting.WinForms

Public Class Laporan_Cucian
    Sub TampilDataGrid()
        'menampilkan tabel database
        DA = New MySqlDataAdapter("SELECT tb_transaksi.Faktur, tb_transaksi.Tanggal_Masuk AS 'Tanggal Masuk', tb_transaksi.Tanggal_Keluar AS 'Tanggal Keluar', tb_customer.Nama, tb_mesincuci.Jumlah_MesinCuci AS 'Jumlah Mesin Cuci', concat('Rp. ', format(tb_transaksi.Harga,0, 'id_ID')) as 'Harga', concat('Rp. ', format(tb_transaksi.DP,0, 'id_ID')) as 'DP', concat('Rp. ', format(tb_transaksi.Total_Harga,0, 'id_ID')) as 'Total Harga', tb_transaksi.Keterangan FROM tb_transaksi INNER JOIN tb_mesincuci on tb_transaksi.ID_MesinCuci = tb_mesincuci.ID_MesinCuci INNER JOIN tb_customer on tb_transaksi.ID_Customer = tb_customer.ID_Customer", CONN)
        DS = New DataSet
        DA.Fill(DS, "laporan_cucian")
        dglaporancucian.DataSource = (DS.Tables("laporan_cucian"))
    End Sub

    Private Sub Laporan_Cucian_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "MMMM yyyy"
        DateTimePicker1.Value = Format(Now)

        DateTimePicker2.Format = DateTimePickerFormat.Custom
        DateTimePicker2.CustomFormat = "MMMM yyyy"
        DateTimePicker2.Value = Format(Now)
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        If ComboBox1.SelectedItem = "Faktur" Then
            Call koneksi()
            Dim carifaktur As String = "SELECT tb_transaksi.Faktur, tb_transaksi.Tanggal_Masuk AS 'Tanggal Masuk', tb_transaksi.Tanggal_Keluar AS 'Tanggal Keluar', tb_customer.Nama, tb_mesincuci.Jumlah_MesinCuci AS 'Jumlah Mesin Cuci', concat('Rp. ', format(tb_transaksi.Harga,0, 'id_ID')) as 'Harga', concat('Rp. ', format(tb_transaksi.DP,0, 'id_ID')) as 'DP', concat('Rp. ', format(tb_transaksi.Total_Harga,0, 'id_ID')) as 'Total Harga', tb_transaksi.Keterangan FROM tb_transaksi INNER JOIN tb_mesincuci on tb_transaksi.ID_MesinCuci = tb_mesincuci.ID_MesinCuci INNER JOIN tb_customer on tb_transaksi.ID_Customer = tb_customer.ID_Customer WHERE Faktur like '%" & TextBox1.Text & "%'"
            CMD = New MySqlCommand(carifaktur, CONN)
            RD = CMD.ExecuteReader
            RD.Read()
            If RD.HasRows Then
                Call koneksi()
                DA = New MySqlDataAdapter(carifaktur, CONN)
                DS = New DataSet
                DA.Fill(DS, "ketemu")
                dglaporancucian.DataSource = DS.Tables("ketemu")
            End If
        ElseIf ComboBox1.SelectedItem = "Nama Customer" Then
            Call koneksi()
            Dim caricustomer As String = "SELECT tb_transaksi.Faktur, tb_transaksi.Tanggal_Masuk AS 'Tanggal Masuk', tb_transaksi.Tanggal_Keluar AS 'Tanggal Keluar', tb_customer.Nama, tb_mesincuci.Jumlah_MesinCuci AS 'Jumlah Mesin Cuci', concat('Rp. ', format(tb_transaksi.Harga,0, 'id_ID')) as 'Harga', concat('Rp. ', format(tb_transaksi.DP,0, 'id_ID')) as 'DP', concat('Rp. ', format(tb_transaksi.Total_Harga,0, 'id_ID')) as 'Total Harga', tb_transaksi.Keterangan FROM tb_transaksi INNER JOIN tb_mesincuci on tb_transaksi.ID_MesinCuci = tb_mesincuci.ID_MesinCuci INNER JOIN tb_customer on tb_transaksi.ID_Customer = tb_customer.ID_Customer WHERE Nama like '%" & TextBox1.Text & "%'"
            CMD = New MySqlCommand(caricustomer, CONN)
            RD = CMD.ExecuteReader
            RD.Read()
            If RD.HasRows Then
                Call koneksi()
                DA = New MySqlDataAdapter(caricustomer, CONN)
                DS = New DataSet
                DA.Fill(DS, "ketemu")
                dglaporancucian.DataSource = DS.Tables("ketemu")
            End If
        End If
    End Sub

    Private Sub DateTimePicker1_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker1.MouseLeave
        If ComboBox1.SelectedItem = "Tanggal Masuk" Then
            Call koneksi()
            Dim caritglmasuk As String = "SELECT tb_transaksi.Faktur, tb_transaksi.Tanggal_Masuk AS 'Tanggal Masuk', tb_transaksi.Tanggal_Keluar AS 'Tanggal Keluar', tb_customer.Nama, tb_mesincuci.Jumlah_MesinCuci AS 'Jumlah Mesin Cuci', concat('Rp. ', format(tb_transaksi.Harga,0, 'id_ID')) as 'Harga', concat('Rp. ', format(tb_transaksi.DP,0, 'id_ID')) as 'DP', concat('Rp. ', format(tb_transaksi.Total_Harga,0, 'id_ID')) as 'Total Harga', tb_transaksi.Keterangan FROM tb_transaksi INNER JOIN tb_mesincuci on tb_transaksi.ID_MesinCuci = tb_mesincuci.ID_MesinCuci INNER JOIN tb_customer on tb_transaksi.ID_Customer = tb_customer.ID_Customer WHERE DATE_FORMAT(Tanggal_Masuk, '%Y-%m') like '%" & Format(DateTimePicker1.Value, "yyyy-MM") & "%'"
            CMD = New MySqlCommand(caritglmasuk, CONN)
            RD = CMD.ExecuteReader
            RD.Read()
            If RD.HasRows Then
                Call koneksi()
                DA = New MySqlDataAdapter(caritglmasuk, CONN)
                DS = New DataSet
                DA.Fill(DS, "ketemu")
                dglaporancucian.DataSource = DS.Tables("ketemu")
            End If
        ElseIf ComboBox1.SelectedItem = "Tanggal Keluar" Then
            Call koneksi()
            Dim caritglkeluar As String = "SELECT tb_transaksi.Faktur, tb_transaksi.Tanggal_Masuk AS 'Tanggal Masuk', tb_transaksi.Tanggal_Keluar AS 'Tanggal Keluar', tb_customer.Nama, tb_mesincuci.Jumlah_MesinCuci AS 'Jumlah Mesin Cuci', concat('Rp. ', format(tb_transaksi.Harga,0, 'id_ID')) as 'Harga', concat('Rp. ', format(tb_transaksi.DP,0, 'id_ID')) as 'DP', concat('Rp. ', format(tb_transaksi.Total_Harga,0, 'id_ID')) as 'Total Harga', tb_transaksi.Keterangan FROM tb_transaksi INNER JOIN tb_mesincuci on tb_transaksi.ID_MesinCuci = tb_mesincuci.ID_MesinCuci INNER JOIN tb_customer on tb_transaksi.ID_Customer = tb_customer.ID_Customer WHERE DATE_FORMAT(Tanggal_Keluar, '%Y-%m') like '%" & Format(DateTimePicker1.Value, "yyyy-MM") & "%'"
            CMD = New MySqlCommand(caritglkeluar, CONN)
            RD = CMD.ExecuteReader
            RD.Read()
            If RD.HasRows Then
                Call koneksi()
                DA = New MySqlDataAdapter(caritglkeluar, CONN)
                DS = New DataSet
                DA.Fill(DS, "ketemu")
                dglaporancucian.DataSource = DS.Tables("ketemu")
            End If
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedItem = "Semua" Then
            TextBox1.ReadOnly = True
            DateTimePicker1.Enabled = False
        ElseIf ComboBox1.SelectedItem = "Faktur" Then
            TextBox1.ReadOnly = False
            DateTimePicker1.Enabled = False
        ElseIf ComboBox1.SelectedItem = "Tanggal Masuk" Then
            TextBox1.ReadOnly = True
            DateTimePicker1.Enabled = True
        ElseIf ComboBox1.SelectedItem = "Tanggal Keluar" Then
            TextBox1.ReadOnly = True
            DateTimePicker1.Enabled = True
        ElseIf ComboBox1.SelectedItem = "Nama Customer" Then
            TextBox1.ReadOnly = False
            DateTimePicker1.Enabled = False
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        If ComboBox2.SelectedItem = "Semua" Then
            DateTimePicker2.Enabled = False
            TextBox2.ReadOnly = True
            Call TampilDataGrid()
        ElseIf ComboBox2.SelectedItem = "Tanggal Masuk" Then
            DateTimePicker2.Enabled = True
            TextBox2.ReadOnly = True
        ElseIf ComboBox2.SelectedItem = "Tanggal Keluar" Then
            DateTimePicker2.Enabled = True
            TextBox2.ReadOnly = True
        ElseIf ComboBox2.SelectedItem = "Nama Customer" Then
            DateTimePicker2.Enabled = False
            TextBox2.ReadOnly = False
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If ComboBox2.SelectedItem = "Semua" Then
            GetDatabaseSetting()
            RptCucian.ReportViewer1.LocalReport.ReportPath = "Report1.rdlc"

            Dim dtClass As DataTable = GetDataTable("SELECT tb_transaksi.Faktur, tb_transaksi.Tanggal_Masuk 'Tanggal_Masuk', tb_transaksi.Tanggal_Keluar AS 'Tanggal_Keluar', tb_customer.Nama, tb_mesincuci.Jumlah_MesinCuci, tb_mesincuci.HargaPerMesinCuci, concat('Rp. ', format(tb_transaksi.Harga,0, 'id_ID')) as 'Harga', concat('Rp. ', format(tb_transaksi.DP,0, 'id_ID')) as 'DP', concat('Rp. ', format(tb_transaksi.Total_Harga,0, 'id_ID')) as 'Total_Harga', tb_transaksi.Keterangan FROM tb_transaksi INNER JOIN tb_mesincuci on tb_transaksi.ID_MesinCuci = tb_mesincuci.ID_MesinCuci INNER JOIN tb_customer on tb_transaksi.ID_Customer = tb_customer.ID_Customer")

            Dim dataSource As ReportDataSource = New ReportDataSource("DatasetCucian", dtClass)
            RptCucian.ReportViewer1.LocalReport.DataSources.Clear()
            RptCucian.ReportViewer1.LocalReport.DataSources.Add(dataSource)
            RptCucian.ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
            RptCucian.ReportViewer1.RefreshReport()
            RptCucian.Show()
        ElseIf ComboBox2.SelectedItem = "Tanggal Masuk" Then
            GetDatabaseSetting()
            RptCucian.ReportViewer1.LocalReport.ReportPath = "Report1.rdlc"

            Dim dtClass As DataTable = GetDataTable("SELECT tb_transaksi.Faktur, tb_transaksi.Tanggal_Masuk 'Tanggal_Masuk', tb_transaksi.Tanggal_Keluar AS 'Tanggal_Keluar', tb_customer.Nama, tb_mesincuci.Jumlah_MesinCuci, tb_mesincuci.HargaPerMesinCuci, concat('Rp. ', format(tb_transaksi.Harga,0, 'id_ID')) as 'Harga', concat('Rp. ', format(tb_transaksi.DP,0, 'id_ID')) as 'DP', concat('Rp. ', format(tb_transaksi.Total_Harga,0, 'id_ID')) as 'Total_Harga', tb_transaksi.Keterangan FROM tb_transaksi INNER JOIN tb_mesincuci on tb_transaksi.ID_MesinCuci = tb_mesincuci.ID_MesinCuci INNER JOIN tb_customer on tb_transaksi.ID_Customer = tb_customer.ID_Customer WHERE DATE_FORMAT(Tanggal_Masuk, '%Y-%m') like '%" & Format(DateTimePicker2.Value, "yyyy-MM") & "%'")

            Dim dataSource As ReportDataSource = New ReportDataSource("DatasetCucian", dtClass)
            RptCucian.ReportViewer1.LocalReport.DataSources.Clear()
            RptCucian.ReportViewer1.LocalReport.DataSources.Add(dataSource)
            RptCucian.ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
            RptCucian.ReportViewer1.RefreshReport()
            RptCucian.Show()
        ElseIf ComboBox2.SelectedItem = "Tanggal Keluar" Then
            GetDatabaseSetting()
            RptCucian.ReportViewer1.LocalReport.ReportPath = "Report1.rdlc"

            Dim dtClass As DataTable = GetDataTable("SELECT tb_transaksi.Faktur, tb_transaksi.Tanggal_Masuk 'Tanggal_Masuk', tb_transaksi.Tanggal_Keluar AS 'Tanggal_Keluar', tb_customer.Nama, tb_mesincuci.Jumlah_MesinCuci, tb_mesincuci.HargaPerMesinCuci, concat('Rp. ', format(tb_transaksi.Harga,0, 'id_ID')) as 'Harga', concat('Rp. ', format(tb_transaksi.DP,0, 'id_ID')) as 'DP', concat('Rp. ', format(tb_transaksi.Total_Harga,0, 'id_ID')) as 'Total_Harga', tb_transaksi.Keterangan FROM tb_transaksi INNER JOIN tb_mesincuci on tb_transaksi.ID_MesinCuci = tb_mesincuci.ID_MesinCuci INNER JOIN tb_customer on tb_transaksi.ID_Customer = tb_customer.ID_Customer WHERE DATE_FORMAT(Tanggal_Masuk, '%Y-%m') like '%" & Format(DateTimePicker2.Value, "yyyy-MM") & "%'")

            Dim dataSource As ReportDataSource = New ReportDataSource("DatasetCucian", dtClass)
            RptCucian.ReportViewer1.LocalReport.DataSources.Clear()
            RptCucian.ReportViewer1.LocalReport.DataSources.Add(dataSource)
            RptCucian.ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
            RptCucian.ReportViewer1.RefreshReport()
            RptCucian.Show()
        ElseIf ComboBox2.SelectedItem = "Nama Customer" Then
            GetDatabaseSetting()
            RptCucian.ReportViewer1.LocalReport.ReportPath = "Report1.rdlc"

            Dim dtClass As DataTable = GetDataTable("SELECT tb_transaksi.Faktur, tb_transaksi.Tanggal_Masuk 'Tanggal_Masuk', tb_transaksi.Tanggal_Keluar AS 'Tanggal_Keluar', tb_customer.Nama, tb_mesincuci.Jumlah_MesinCuci, tb_mesincuci.HargaPerMesinCuci, concat('Rp. ', format(tb_transaksi.Harga,0, 'id_ID')) as 'Harga', concat('Rp. ', format(tb_transaksi.DP,0, 'id_ID')) as 'DP', concat('Rp. ', format(tb_transaksi.Total_Harga,0, 'id_ID')) as 'Total_Harga', tb_transaksi.Keterangan FROM tb_transaksi INNER JOIN tb_mesincuci on tb_transaksi.ID_MesinCuci = tb_mesincuci.ID_MesinCuci INNER JOIN tb_customer on tb_transaksi.ID_Customer = tb_customer.ID_Customer WHERE Nama like '%" & TextBox2.Text & "%'")

            Dim dataSource As ReportDataSource = New ReportDataSource("DatasetCucian", dtClass)
            RptCucian.ReportViewer1.LocalReport.DataSources.Clear()
            RptCucian.ReportViewer1.LocalReport.DataSources.Add(dataSource)
            RptCucian.ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
            RptCucian.ReportViewer1.RefreshReport()
            RptCucian.Show()
        End If
    End Sub

    Private Sub btnkembali_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnkembali.Click
        Call Form1.Laporan2.BringToFront()
    End Sub
End Class
