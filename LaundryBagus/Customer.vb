Imports MySql.Data.MySqlClient

Public Class Customer
    Public Sub TampilDataGrid()
        'menampilkan tabel database
        DA = New MySqlDataAdapter("SELECT ID_Customer AS 'ID Customer', Nama, Jenis_Kelamin AS 'Jenis Kelamin', Alamat, No_HP AS 'No. HP' FROM tb_customer", CONN)
        DS = New DataSet
        DA.Fill(DS, "Customer")
        dgcustomer.DataSource = (DS.Tables("Customer"))
    End Sub

    Public Sub kodecustomer()
        'penomoran otomatis
        Call koneksi()
        CMD = New MySqlCommand("SELECT * FROM tb_customer ORDER BY ID_Customer DESC LIMIT 1", CONN)
        Dim UrutanKode As String
        Dim Hitung As Long
        RD = CMD.ExecuteReader
        RD.Read()
        If Not RD.HasRows Then
            UrutanKode = "CUS" + "0001"
        Else
            Hitung = Microsoft.VisualBasic.Right(RD.GetString(0), 4) + 1
            UrutanKode = "CUS" + Microsoft.VisualBasic.Right("0000" & Hitung, 4)
        End If
        txtidcustomer.Text = UrutanKode
    End Sub

    Public Sub kosongcustomer()
        'mengosongkan semua textbox
        txtidcustomer.Text = ""
        txtnamacustomer.Text = ""
        cmbjeniskelamin.Text = ""
        txtalamat.Text = ""
        txtnohp.Text = ""
    End Sub

    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged
        Call koneksi()
        Dim caricustomer As String = "SELECT * FROM tb_customer WHERE Nama like '%" & TextBox5.Text & "%'"
        CMD = New MySqlCommand(caricustomer, CONN)
        RD = CMD.ExecuteReader
        RD.Read()
        If RD.HasRows Then
            Call koneksi()
            DA = New MySqlDataAdapter(caricustomer, CONN)
            DS = New DataSet
            DA.Fill(DS, "ketemu")
            dgcustomer.DataSource = DS.Tables("ketemu")
        End If
    End Sub

    Private Sub btnsimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsimpan.Click
        If txtidcustomer.Text = "" Or txtnamacustomer.Text = "" Or cmbjeniskelamin.Text = "" Or txtalamat.Text = "" Or txtnohp.Text = "" Then
            MsgBox("Data Belum Lengkap", MsgBoxStyle.Critical)
        Else
            'menyimpan data
            Call koneksi()
            Dim InputData As String = "INSERT INTO tb_customer VALUES ('" & txtidcustomer.Text & "','" & txtnamacustomer.Text & "','" & cmbjeniskelamin.Text & "','" & txtalamat.Text & "','" & txtnohp.Text & "')"
            CMD = New MySqlCommand(InputData, CONN)
            CMD.ExecuteNonQuery()
            Call Form1.dashboard()
            koneksi()
            TampilDataGrid()
            kosongcustomer()
            kodecustomer()
        End If
    End Sub

    Private Sub btnedit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnedit.Click
        'jika masih ada textbox yang kosong
        If txtidcustomer.Text = "" Or txtnamacustomer.Text = "" Or cmbjeniskelamin.Text = "" Or txtalamat.Text = "" Or txtnohp.Text = "" Then
            MsgBox("Data Belum Lengkap", MsgBoxStyle.Critical)
        Else
            'edit data
            Call koneksi()
            Dim EditData As String = "UPDATE tb_customer SET Nama = '" & txtnamacustomer.Text & "',Jenis_Kelamin = '" & cmbjeniskelamin.Text & "',Alamat = '" & txtalamat.Text & "',No_HP = '" & txtnohp.Text & "' WHERE ID_Customer = '" & txtidcustomer.Text & "'"
            CMD = New MySqlCommand(EditData, CONN)
            CMD.ExecuteNonQuery()
            koneksi()
            TampilDataGrid()
            kosongcustomer()
            kodecustomer()
        End If
    End Sub

    Private Sub btnhapus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnhapus.Click
        'jika masih ada textbox yang kosong
        If txtidcustomer.Text = "" Or txtnamacustomer.Text = "" Or cmbjeniskelamin.Text = "" Or txtalamat.Text = "" Or txtnohp.Text = "" Then
            MsgBox("Pastikan Data Yang Akan Dihapus Terisi", MsgBoxStyle.Critical)
        Else
            'hapus data
            Call koneksi()
            Dim HapusData As String = "DELETE FROM tb_customer WHERE ID_Customer = '" & txtidcustomer.Text & "'"
            CMD = New MySqlCommand(HapusData, CONN)
            CMD.ExecuteNonQuery()
            MsgBox("Data Berhasil Dihapus")
            koneksi()
            TampilDataGrid()
            kosongcustomer()
            kodecustomer()
        End If
    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        kosongcustomer()
        kodecustomer()
    End Sub

    Private Sub txtnohp_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtnohp.KeyPress
        Dim keyascii As Short = Asc(e.KeyChar)
        If (e.KeyChar Like "[0-9]" OrElse keyascii = Keys.Back) Then
            keyascii = 0
        Else
            e.Handled = CBool(keyascii)
        End If
    End Sub

    Private Sub dgcustomer_CellMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgcustomer.CellMouseDoubleClick
        'menampilkan data dari datagrid ke textbox sesuai dengan cell yang di klik
        txtidcustomer.Text = dgcustomer.Rows(e.RowIndex).Cells(0).Value
        txtnamacustomer.Text = dgcustomer.Rows(e.RowIndex).Cells(1).Value
        cmbjeniskelamin.Text = dgcustomer.Rows(e.RowIndex).Cells(2).Value
        txtalamat.Text = dgcustomer.Rows(e.RowIndex).Cells(3).Value
        txtnohp.Text = dgcustomer.Rows(e.RowIndex).Cells(4).Value
    End Sub
End Class
