Imports MySql.Data.MySqlClient

Public Class Cucian_Masuk
    Public Sub TampilDataGrid()
        'menampilkan tabel database
        DA = New MySqlDataAdapter("SELECT Nama, Jenis_Kelamin AS 'Jenis Kelamin', Alamat, No_HP AS 'No. HP' FROM tb_customer", CONN)
        DS = New DataSet
        DA.Fill(DS, "penyerahan_cucian")
        dgpenyerahancucian.DataSource = (DS.Tables("penyerahan_cucian"))
    End Sub

    Sub hargapermsincuci()
        Call koneksi()
        CMD = New MySqlCommand("SELECT * FROM tb_mesincuci ORDER BY ID_MesinCuci DESC LIMIT 1", CONN)
        RD = CMD.ExecuteReader
        RD.Read()
        If Not RD.HasRows Then
            txtserahhargamesin.Text = "10000"
        Else
            txtserahhargamesin.Text = RD.Item("HargaPerMesinCuci")
        End If
    End Sub

    Public Sub kodefaktur()
        'penomoran otomatis faktur
        Call koneksi()
        CMD = New MySqlCommand("SELECT * FROM tb_transaksi ORDER BY Faktur DESC LIMIT 1", CONN)
        Dim UrutanKode As String
        Dim Hitung As Long
        RD = CMD.ExecuteReader
        RD.Read()
        If Not RD.HasRows Then
            UrutanKode = Format(Now, "yyMMdd") + "0001"
        Else
            Hitung = Microsoft.VisualBasic.Right(RD.GetString(0), 9) + 1
            UrutanKode = Format(Now, "yyMMdd") + Microsoft.VisualBasic.Right("0000" & Hitung, 4)
        End If
        txtserahfaktur.Text = UrutanKode
    End Sub

    Public Sub serahharga()
        'menghitung harga
        txtserahharga.Text = txtserahmesincuci.Text * txtserahhargamesin.Text
    End Sub

    Public Sub serahtotal()
        'menghitung total harga
        txtserahtotal.Text = txtserahharga.Text - txtserahdp.Text
    End Sub

    Public Sub serahkosong()
        'mengkosongkan textbox penyerahan cucian
        txtserahfaktur.Text = ""
        txtserahcustomer.Text = ""
        txtserahmesincuci.Text = ""
        txtserahharga.Text = ""
        txtserahdp.Text = "0"
        txtserahtotal.Text = ""
        cmbserahket.Text = ""
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Label8.Text = Format(Now, "yyyy-MM-dd")
        Label9.Text = DateTime.Now.ToString("HH:mm:ss")

        If txtserahdp.Text = "" Then
            txtserahdp.Text = "0"
        Else
            If txtserahharga.Text = "" Then
                Return
            Else
                If txtserahdp.Text = txtserahharga.Text Then
                    cmbserahket.Text = "Sudah Lunas"
                Else
                    cmbserahket.Text = "Belum Lunas"
                End If
                Call serahtotal()
            End If
        End If
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtserahmesincuci.TextChanged
        If txtserahmesincuci.Text = "" Then
            txtserahharga.Text = ""
            txtserahtotal.Text = ""
            cmbserahket.Text = ""
            Return
        Else
            'txtserahmesincuci.Text = Format(Val(txtserahmesincuci.Text), "###,###")
            Call serahharga()
        End If
    End Sub

    Private Sub txtserahmesincuci_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtserahmesincuci.KeyPress
        Dim keyascii As Short = Asc(e.KeyChar)
        If (e.KeyChar Like "[0-9]" OrElse keyascii = Keys.Back) Then
            keyascii = 0
        Else
            e.Handled = CBool(keyascii)
        End If
    End Sub

    Private Sub txtserahharga_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtserahharga.KeyPress
        Dim keyascii As Short = Asc(e.KeyChar)
        If (e.KeyChar Like "[0-9]" OrElse keyascii = Keys.Back) Then
            keyascii = 0
        Else
            e.Handled = CBool(keyascii)
        End If
    End Sub

    Private Sub txtserahdp_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtserahdp.KeyPress
        Dim keyascii As Short = Asc(e.KeyChar)
        If (e.KeyChar Like "[0-9]" OrElse keyascii = Keys.Back) Then
            keyascii = 0
        Else
            e.Handled = CBool(keyascii)
        End If
    End Sub

    Private Sub txtserahtotal_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtserahtotal.KeyPress
        Dim keyascii As Short = Asc(e.KeyChar)
        If (e.KeyChar Like "[0-9]" OrElse keyascii = Keys.Back) Then
            keyascii = 0
        Else
            e.Handled = CBool(keyascii)
        End If
    End Sub

    Private Sub txtserahhargamesin_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtserahhargamesin.KeyPress
        Dim keyascii As Short = Asc(e.KeyChar)
        If (e.KeyChar Like "[0-9]" OrElse keyascii = Keys.Back) Then
            keyascii = 0
        Else
            e.Handled = CBool(keyascii)
        End If
    End Sub

    Private Sub btnsimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsimpan.Click
        If txtserahfaktur.Text = "" Or txtserahcustomer.Text = "" Or txtserahmesincuci.Text = "" Or txtserahharga.Text = "" Or txtserahdp.Text = "" Or cmbserahket.Text = "" Or txtserahtotal.Text = "" Then
            MsgBox("Data Belum Lengkap", MsgBoxStyle.Critical)
        Else
            'menyimpan data
            Call koneksi()
            Dim id_customer As String
            CMD = New MySqlCommand("SELECT * FROM tb_customer WHERE Nama = '" & txtserahcustomer.Text & "'", CONN)
            RD = CMD.ExecuteReader
            RD.Read()
            If RD.HasRows Then
                id_customer = RD.Item("ID_Customer")
            End If

            Call koneksi()
            Dim Inputtb_transaksi As String = "INSERT INTO tb_transaksi (Faktur, Tanggal_Masuk, Tanggal_Keluar, ID_Customer, ID_MesinCuci, Harga, DP, Total_Harga, Keterangan) VALUES ('" & txtserahfaktur.Text & "', '" & Label8.Text & " " & Label9.Text & "',NULL,'" & id_customer & "','" & txtserahfaktur.Text & "','" & txtserahharga.Text & "','" & txtserahdp.Text & "','" & txtserahtotal.Text & "','" & cmbserahket.Text & "')"
            CMD = New MySqlCommand(Inputtb_transaksi, CONN)
            CMD.ExecuteNonQuery()

            Dim Inputtb_mesincuci As String = "INSERT INTO tb_mesincuci (ID_mesincuci, Jumlah_MesinCuci, HargaPerMesinCuci) VALUES ('" & txtserahfaktur.Text & "','" & txtserahmesincuci.Text & "','" & txtserahhargamesin.Text & "')"
            CMD = New MySqlCommand(Inputtb_mesincuci, CONN)
            CMD.ExecuteNonQuery()

            Call Form1.dashboard()
            koneksi()
            TampilDataGrid()
            serahkosong()
            kodefaktur()
            hargapermsincuci()
        End If
    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        serahkosong()
        kodefaktur()
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Call koneksi()
        Dim caricustomer As String = "SELECT Nama, Jenis_Kelamin AS 'Jenis Kelamin', Alamat, No_HP AS 'No. HP' FROM tb_customer WHERE Nama like '%" & TextBox1.Text & "%'"
        CMD = New MySqlCommand(caricustomer, CONN)
        RD = CMD.ExecuteReader
        RD.Read()
        If RD.HasRows Then
            Call koneksi()
            DA = New MySqlDataAdapter(caricustomer, CONN)
            DS = New DataSet
            DA.Fill(DS, "ketemu")
            dgpenyerahancucian.DataSource = DS.Tables("ketemu")
        End If
    End Sub

    Private Sub dgpenyerahancucian_CellMouseDoubleClick_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgpenyerahancucian.CellMouseDoubleClick
        txtserahcustomer.Text = dgpenyerahancucian.Rows(e.RowIndex).Cells(0).Value
    End Sub
End Class
