Public Class frmSave
    Private _ConnectionString As String = "User ID=sa;Password=mocrab;Initial Catalog=transaction;Data Source=IS3\SQL2005"

    Property ConnectionString() As String
        Get
            ConnectionString = _ConnectionString
        End Get
        Set(ByVal value As String)
            _ConnectionString = value
        End Set
    End Property
    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadTypes()
    End Sub

    Sub LoadTypes()
        cboReportCategory.Items.Clear()
        Dim conn As New SqlClient.SqlConnection(_ConnectionString)
        conn.Open()
        Dim cmd As New SqlClient.SqlCommand("select ReportType from Reports where ReportProgram = 'RAWTEXT' and ISNULL(REPORTTYPE,'') <> '' Group by Reporttype", conn)
        Dim mr As SqlClient.SqlDataReader = cmd.ExecuteReader
        While mr.Read
            cboReportCategory.Items.Add(mr("ReportType") & "")
        End While
        mr.Close()
        mr = Nothing
        cmd.Dispose()
        cmd = Nothing
        conn.Close()
        conn.Dispose()
        conn = Nothing
      
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        If cboReportCategory.Text = "" Then
            MessageBox.Show("You must choose a Category for This Report")
            Exit Sub
        End If
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try

        If cboReportCategory.Text <> "" Then
                txtReportName.Focus()
            End If
            Timer1.Enabled = False
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub
End Class