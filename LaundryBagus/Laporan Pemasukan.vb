Imports MySql.Data.MySqlClient
Imports Microsoft.Reporting.WinForms

Public Class Laporan_Pemasukan

    Sub TampilDataGrid()
        'menampilkan tabel database
        DA = New MySqlDataAdapter("SELECT tb_transaksi.Faktur, tb_transaksi.Tanggal_Masuk AS 'Tanggal Masuk', tb_transaksi.Tanggal_Keluar AS 'Tanggal Keluar', tb_customer.Nama, tb_mesincuci.Jumlah_MesinCuci AS 'Jumlah Mesin Cuci', concat('Rp. ', format(tb_transaksi.Harga,0, 'id_ID')) as 'Harga', concat('Rp. ', format(tb_transaksi.DP,0, 'id_ID')) as 'DP', concat('Rp. ', format(tb_transaksi.Total_Harga,0, 'id_ID')) as 'Total Harga', tb_transaksi.Keterangan FROM tb_transaksi INNER JOIN tb_mesincuci on tb_transaksi.ID_MesinCuci = tb_mesincuci.ID_MesinCuci INNER JOIN tb_customer on tb_transaksi.ID_Customer = tb_customer.ID_Customer WHERE tb_transaksi.Keterangan = 'Sudah Lunas'", CONN)
        DS = New DataSet
        DA.Fill(DS, "laporan_pemasukan")
        dglaporanpemasukan.DataSource = (DS.Tables("laporan_pemasukan"))
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedItem = "Semua" Then
            TextBox1.ReadOnly = True
            DateTimePicker1.Enabled = False
            Call TampilDataGrid()
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

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        If ComboBox1.SelectedItem = "Faktur" Then
            Call koneksi()
            Dim carifaktur As String = "SELECT tb_transaksi.Faktur, tb_transaksi.Tanggal_Masuk AS 'Tanggal Masuk', tb_transaksi.Tanggal_Keluar AS 'Tanggal Keluar', tb_customer.Nama, tb_mesincuci.Jumlah_MesinCuci AS 'Jumlah Mesin Cuci', concat('Rp. ', format(tb_transaksi.Harga,0, 'id_ID')) as 'Harga', concat('Rp. ', format(tb_transaksi.DP,0, 'id_ID')) as 'DP', concat('Rp. ', format(tb_transaksi.Total_Harga,0, 'id_ID')) as 'Total Harga', tb_transaksi.Keterangan FROM tb_transaksi INNER JOIN tb_mesincuci on tb_transaksi.ID_MesinCuci = tb_mesincuci.ID_MesinCuci INNER JOIN tb_customer on tb_transaksi.ID_Customer = tb_customer.ID_Customer WHERE Keterangan = 'Sudah Lunas' AND Faktur like '%" & TextBox1.Text & "%'"
            CMD = New MySqlCommand(carifaktur, CONN)
            RD = CMD.ExecuteReader
            RD.Read()
            If RD.HasRows Then
                Call koneksi()
                DA = New MySqlDataAdapter(carifaktur, CONN)
                DS = New DataSet
                DA.Fill(DS, "ketemu")
                dglaporanpemasukan.DataSource = DS.Tables("ketemu")
            End If
        ElseIf ComboBox1.SelectedItem = "Nama Customer" Then
            Call koneksi()
            Dim caricustomer As String = "SELECT tb_transaksi.Faktur, tb_transaksi.Tanggal_Masuk AS 'Tanggal Masuk', tb_transaksi.Tanggal_Keluar AS 'Tanggal Keluar', tb_customer.Nama, tb_mesincuci.Jumlah_MesinCuci AS 'Jumlah Mesin Cuci', concat('Rp. ', format(tb_transaksi.Harga,0, 'id_ID')) as 'Harga', concat('Rp. ', format(tb_transaksi.DP,0, 'id_ID')) as 'DP', concat('Rp. ', format(tb_transaksi.Total_Harga,0, 'id_ID')) as 'Total Harga', tb_transaksi.Keterangan FROM tb_transaksi INNER JOIN tb_mesincuci on tb_transaksi.ID_MesinCuci = tb_mesincuci.ID_MesinCuci INNER JOIN tb_customer on tb_transaksi.ID_Customer = tb_customer.ID_Customer WHERE Keterangan = 'Sudah Lunas' AND Nama like '%" & TextBox1.Text & "%'"
            CMD = New MySqlCommand(caricustomer, CONN)
            RD = CMD.ExecuteReader
            RD.Read()
            If RD.HasRows Then
                Call koneksi()
                DA = New MySqlDataAdapter(caricustomer, CONN)
                DS = New DataSet
                DA.Fill(DS, "ketemu")
                dglaporanpemasukan.DataSource = DS.Tables("ketemu")
            End If
        End If
    End Sub

    Private Sub Laporan_Pemasukan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "MMMM yyyy"
        DateTimePicker1.Value = Format(Now)

        DateTimePicker2.Format = DateTimePickerFormat.Custom
        DateTimePicker2.CustomFormat = "MMMM yyyy"
        DateTimePicker2.Value = Format(Now)
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        If ComboBox2.SelectedItem = "Semua" Then
            DateTimePicker2.Enabled = False
            TextBox2.ReadOnly = True
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
            RptPemasukan.ReportViewer1.LocalReport.ReportPath = "Report2.rdlc"

            Dim dtClass As DataTable = GetDataTable("SELECT tb_transaksi.Faktur, tb_transaksi.Tanggal_Masuk 'Tanggal_Masuk', tb_transaksi.Tanggal_Keluar AS 'Tanggal_Keluar', tb_customer.Nama, tb_mesincuci.Jumlah_MesinCuci, tb_mesincuci.HargaPerMesinCuci, concat('Rp. ', format(tb_transaksi.Harga,0, 'id_ID')) as 'Harga', concat('Rp. ', format(tb_transaksi.DP,0, 'id_ID')) as 'DP', concat('Rp. ', format(tb_transaksi.Total_Harga,0, 'id_ID')) as 'Total_Harga', tb_transaksi.Keterangan FROM tb_transaksi INNER JOIN tb_mesincuci on tb_transaksi.ID_MesinCuci = tb_mesincuci.ID_MesinCuci INNER JOIN tb_customer on tb_transaksi.ID_Customer = tb_customer.ID_Customer WHERE Keterangan = 'Sudah Lunas'")

            Dim dtPemasukan As DataTable = GetDataTable("SELECT concat('Rp. ', format(SUM(IF (Keterangan = 'Sudah Lunas', Total_Harga, 0)),0, 'id_ID')) AS Total_Pemasukan  FROM tb_transaksi WHERE Keterangan = 'Sudah Lunas'")

            Dim dataSource As ReportDataSource = New ReportDataSource("DatasetPemasukan", dtClass)
            Dim dataTotPemasukan As ReportDataSource = New ReportDataSource("DatasetTotalPemasukan", dtPemasukan)
            RptPemasukan.ReportViewer1.LocalReport.DataSources.Clear()
            RptPemasukan.ReportViewer1.LocalReport.DataSources.Add(dataSource)
            RptPemasukan.ReportViewer1.LocalReport.DataSources.Add(dataTotPemasukan)
            RptPemasukan.ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
            RptPemasukan.ReportViewer1.RefreshReport()
            RptPemasukan.Show()

        ElseIf ComboBox2.SelectedItem = "Tanggal Masuk" Then
            GetDatabaseSetting()
            RptPemasukan.ReportViewer1.LocalReport.ReportPath = "Report2.rdlc"

            Dim dtClass As DataTable = GetDataTable("SELECT tb_transaksi.Faktur, tb_transaksi.Tanggal_Masuk 'Tanggal_Masuk', tb_transaksi.Tanggal_Keluar AS 'Tanggal_Keluar', tb_customer.Nama, tb_mesincuci.Jumlah_MesinCuci, tb_mesincuci.HargaPerMesinCuci, concat('Rp. ', format(tb_transaksi.Harga,0, 'id_ID')) as 'Harga', concat('Rp. ', format(tb_transaksi.DP,0, 'id_ID')) as 'DP', concat('Rp. ', format(tb_transaksi.Total_Harga,0, 'id_ID')) as 'Total_Harga', tb_transaksi.Keterangan FROM tb_transaksi INNER JOIN tb_mesincuci on tb_transaksi.ID_MesinCuci = tb_mesincuci.ID_MesinCuci INNER JOIN tb_customer on tb_transaksi.ID_Customer = tb_customer.ID_Customer WHERE Keterangan = 'Sudah Lunas' AND DATE_FORMAT(Tanggal_Masuk, '%Y-%m') like '%" & Format(DateTimePicker2.Value, "yyyy-MM") & "%'")

            Dim dtPemasukan As DataTable = GetDataTable("SELECT concat('Rp. ', format(SUM(IF (DATE_FORMAT(Tanggal_Masuk, '%Y-%m') = '" & Format(DateTimePicker2.Value, "yyyy-MM") & "', Total_Harga, 0)),0, 'id_ID')) AS Total_Pemasukan  FROM tb_transaksi WHERE Keterangan = 'Sudah Lunas' AND DATE_FORMAT(Tanggal_Masuk, '%Y-%m') = '" & Format(DateTimePicker2.Value, "yyyy-MM") & "'")

            Dim dataSource As ReportDataSource = New ReportDataSource("DatasetPemasukan", dtClass)
            Dim dataTotPemasukan As ReportDataSource = New ReportDataSource("DatasetTotalPemasukan", dtPemasukan)
            RptPemasukan.ReportViewer1.LocalReport.DataSources.Clear()
            RptPemasukan.ReportViewer1.LocalReport.DataSources.Add(dataSource)
            RptPemasukan.ReportViewer1.LocalReport.DataSources.Add(dataTotPemasukan)
            RptPemasukan.ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
            RptPemasukan.ReportViewer1.RefreshReport()
            RptPemasukan.Show()

        ElseIf ComboBox2.SelectedItem = "Tanggal Keluar" Then
            GetDatabaseSetting()
            RptPemasukan.ReportViewer1.LocalReport.ReportPath = "Report2.rdlc"

            Dim dtClass As DataTable = GetDataTable("SELECT tb_transaksi.Faktur, tb_transaksi.Tanggal_Masuk 'Tanggal_Masuk', tb_transaksi.Tanggal_Keluar AS 'Tanggal_Keluar', tb_customer.Nama, tb_mesincuci.Jumlah_MesinCuci, tb_mesincuci.HargaPerMesinCuci, concat('Rp. ', format(tb_transaksi.Harga,0, 'id_ID')) as 'Harga', concat('Rp. ', format(tb_transaksi.DP,0, 'id_ID')) as 'DP', concat('Rp. ', format(tb_transaksi.Total_Harga,0, 'id_ID')) as 'Total_Harga', tb_transaksi.Keterangan FROM tb_transaksi INNER JOIN tb_mesincuci on tb_transaksi.ID_MesinCuci = tb_mesincuci.ID_MesinCuci INNER JOIN tb_customer on tb_transaksi.ID_Customer = tb_customer.ID_Customer WHERE Keterangan = 'Sudah Lunas' AND DATE_FORMAT(Tanggal_Keluar, '%Y-%m') like '%" & Format(DateTimePicker2.Value, "yyyy-MM") & "%'")

            Dim dtPemasukan As DataTable = GetDataTable("SELECT concat('Rp. ', format(SUM(IF (DATE_FORMAT(Tanggal_Keluar, '%Y-%m') = '" & Format(DateTimePicker2.Value, "yyyy-MM") & "', Total_Harga, 0)),0, 'id_ID')) AS Total_Pemasukan  FROM tb_transaksi WHERE Keterangan = 'Sudah Lunas' AND DATE_FORMAT(Tanggal_Keluar, '%Y-%m') = '" & Format(DateTimePicker2.Value, "yyyy-MM") & "'")

            Dim dataSource As ReportDataSource = New ReportDataSource("DatasetPemasukan", dtClass)
            Dim dataTotPemasukan As ReportDataSource = New ReportDataSource("DatasetTotalPemasukan", dtPemasukan)
            RptPemasukan.ReportViewer1.LocalReport.DataSources.Clear()
            RptPemasukan.ReportViewer1.LocalReport.DataSources.Add(dataSource)
            RptPemasukan.ReportViewer1.LocalReport.DataSources.Add(dataTotPemasukan)
            RptPemasukan.ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
            RptPemasukan.ReportViewer1.RefreshReport()
            RptPemasukan.Show()

        ElseIf ComboBox2.SelectedItem = "Nama Customer" Then
            GetDatabaseSetting()
            RptPemasukan.ReportViewer1.LocalReport.ReportPath = "Report2.rdlc"

            Dim dtClass As DataTable = GetDataTable("SELECT tb_transaksi.Faktur, tb_transaksi.Tanggal_Masuk 'Tanggal_Masuk', tb_transaksi.Tanggal_Keluar AS 'Tanggal_Keluar', tb_customer.Nama, tb_mesincuci.Jumlah_MesinCuci, tb_mesincuci.HargaPerMesinCuci, concat('Rp. ', format(tb_transaksi.Harga,0, 'id_ID')) as 'Harga', concat('Rp. ', format(tb_transaksi.DP,0, 'id_ID')) as 'DP', concat('Rp. ', format(tb_transaksi.Total_Harga,0, 'id_ID')) as 'Total_Harga', tb_transaksi.Keterangan FROM tb_transaksi INNER JOIN tb_mesincuci on tb_transaksi.ID_MesinCuci = tb_mesincuci.ID_MesinCuci INNER JOIN tb_customer on tb_transaksi.ID_Customer = tb_customer.ID_Customer WHERE Nama like '%" & TextBox2.Text & "%'")

            Dim dtPemasukan As DataTable = GetDataTable("SELECT concat('Rp. ', format(SUM(IF (" & TextBox2.Text & ", Total_Harga, 0)),0, 'id_ID')) AS Total_Pemasukan  FROM tb_transaksi WHERE Keterangan = 'Sudah Lunas' AND Nama = '" & TextBox2.Text & "'")

            Dim dataSource As ReportDataSource = New ReportDataSource("DatasetPemasukan", dtClass)
            Dim dataTotPemasukan As ReportDataSource = New ReportDataSource("DatasetTotalPemasukan", dtPemasukan)
            RptPemasukan.ReportViewer1.LocalReport.DataSources.Clear()
            RptPemasukan.ReportViewer1.LocalReport.DataSources.Add(dataSource)
            RptPemasukan.ReportViewer1.LocalReport.DataSources.Add(dataTotPemasukan)
            RptPemasukan.ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
            RptPemasukan.ReportViewer1.RefreshReport()
            RptPemasukan.Show()
        End If
    End Sub

    Private Sub btnkembali_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnkembali.Click
        Call Form1.Laporan2.BringToFront()
    End Sub
End Class
