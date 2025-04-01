
Imports System.IO
Imports System.Net
Imports System.Text

Public Class RawFileEdit
    Inherits WeifenLuo.WinFormsUI.DockContent

    Private _ProgramOptionsTable As DataTable
    Private _ProgramOption As Int32
    Private _AvailableTask As New Specialized.StringCollection
    Private _AvailableOptions As Specialized.StringCollection

    'Private _connectionstring As String = My.Settings.ConnectionStirng
    Dim _currentLabel As String = ""
    Dim _currentparent As String = ""
    Private _UserCode As String = "bhamer"
    Private _ConnectionString As String = "User ID=sa;Password=mocrab;Initial Catalog=TRANSACTIONJAC;Data Source=10.6.0.4"
    Dim boolStartup As Boolean = True
    'Private _ConnectionString As String = "User ID=AUTOSEQUENCE;Password=AUTOSEQUENCE;Initial Catalog=AUTOSEQUENCE;Data Source=10.113.14.9,8484"
    Property ProgramOption() As Int32
        Get
            ProgramOption = _ProgramOption
        End Get
        Set(ByVal value As Int32)
            _ProgramOption = value
        End Set
    End Property
    Property UserCode() As String
        Get
            UserCode = _UserCode
        End Get
        Set(ByVal value As String)
            _UserCode = value
        End Set
    End Property
    Property ConnectionString() As String
        Get
            ConnectionString = _ConnectionString
        End Get
        Set(ByVal value As String)
            _ConnectionString = value
        End Set
    End Property
    Property ProgramOptionsTable() As DataTable
        Get
            Return _ProgramOptionsTable
        End Get
        Set(ByVal value As DataTable)
            _ProgramOptionsTable = value
        End Set
    End Property
    Public ReadOnly Property AvailableTask() As Specialized.StringCollection
        Get
            Return _AvailableTask
        End Get

    End Property
    Public ReadOnly Property AvailableOptions() As Specialized.StringCollection
        Get
            Return _AvailableOptions
        End Get

    End Property
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Loaditup()
        ComboBox1.SelectedIndex = 1

    End Sub
    Sub LoadFiles()
        Try

            Dim Conn As New SqlClient.SqlConnection(_ConnectionString)
            Dim cmd As New SqlClient.SqlCommand

            cmd.Connection = Conn
            cmd.Connection.Open()

            Dim i As Int32 = 0
            For i = 0 To TreeView1.Nodes.Count - 1
                cmd.CommandText = "Select REportDescription from Reports where ReportType = '" & TreeView1.Nodes(i).Text & "' and ReportProgram = 'RAWTEXT'"
                Dim dr As SqlClient.SqlDataReader = cmd.ExecuteReader
                While dr.Read
                    'do stuff here
                    TreeView1.Nodes(i).Nodes.Add(dr("ReportDescription"), dr("ReportDescription"), "FILE", "FILE")
                End While
                dr.Close()
            Next
            cmd.Dispose()
            Conn.Close()
            Conn.Dispose()
        Catch ex As Exception

        End Try
    End Sub
    Sub FillTYpe()

        Dim Conn As New SqlClient.SqlConnection(_ConnectionString)
        Dim cmd As New SqlClient.SqlCommand
        TreeView1.Nodes.Clear()
        cmd.Connection = Conn
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "Select ReportType from Reports where ReportProgram = 'RAWTEXT' and isnull(ReportTYPE,'') <> '' Group by ReportType "
        Try

            cmd.Connection.Open()
            Dim dr As SqlClient.SqlDataReader = cmd.ExecuteReader
            While dr.Read
                TreeView1.Nodes.Add(dr("REPORTTYPE"), dr("REPORTTYPE"), "FOLDER", "FOLDER")
            End While
            dr.Close()
            dr = Nothing

            LoadFiles()
        Catch ex As SqlClient.SqlException

        Catch ex As Exception


        Finally
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
                Conn.Dispose()
                Conn = Nothing
            End If
            cmd.Dispose()
            cmd = Nothing
        End Try

    End Sub


    Private Sub SaveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem.Click
        Dim Conn As New SqlClient.SqlConnection(_ConnectionString)
        Dim cmd As SqlClient.SqlCommand = Nothing
        Try
            If TreeView1.SelectedNode.Level = 0 Then
                Exit Sub
            End If
            If MessageBox.Show("Are you Sure you want to Save the Label:" & _currentLabel, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If

            Conn.Open()
            cmd = New SqlClient.SqlCommand
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "Update Reports Set ReportFile = @LabelContent Where REPORTDESCRIPTION = '" & _currentLabel & "'"

            Dim b As Byte() = Encoding.Unicode.GetBytes(RichTextBox1.Text)
            cmd.Parameters.AddWithValue("@LabelContent", b)

            cmd.Connection = Conn
            cmd.ExecuteNonQuery()
        Catch ex As SqlClient.SqlException

        Catch ex As Exception

        Finally
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
                Conn.Dispose()
                Conn = Nothing
            End If
            '    cmd.Dispose()
            '   cmd = Nothing
        End Try

    End Sub
    Sub Loaditup()
        FillTYpe()
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        Dim Conn As New SqlClient.SqlConnection(_ConnectionString)
        Dim cmd As SqlClient.SqlCommand = Nothing
        Try
            If TreeView1.SelectedNode.Level = 0 Then
                Exit Sub
            End If
            If MessageBox.Show("Are you Sure you want to Delete the label:" & _currentLabel, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Conn.Open()
                cmd = New SqlClient.SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandText = "Delete From Reports where ReportDescription = '" & _currentLabel & "'"
                cmd.Connection = Conn
                cmd.ExecuteNonQuery()
            End If
            TreeView1.Nodes(_currentparent).Nodes(_currentLabel).Remove()
            CheckParentDelete(_currentparent)
        Catch ex As SqlClient.SqlException

        Catch ex As Exception

        Finally
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
                Conn.Dispose()
                Conn = Nothing
            End If
            '         cmd.Dispose()
            '         cmd = Nothing
        End Try
    End Sub
    Function doesExist(ByVal strDESCRIPTION As String) As Boolean
        Dim result As Object
        Dim Conn As New SqlClient.SqlConnection(_ConnectionString)
        Dim cmd As SqlClient.SqlCommand = Nothing
        Try
            Conn.Open()
            cmd = New SqlClient.SqlCommand
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "Select recordno from Reports where ReportDescription = @ReportName"
            cmd.Parameters.AddWithValue("@ReportName", strDESCRIPTION)
            cmd.Connection = Conn
            result = cmd.ExecuteScalar
            If IsNothing(result) Then
                doesExist = False
            Else
                doesExist = True
            End If
        Catch ex As SqlClient.SqlException

        Catch ex As Exception

        Finally
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
                Conn.Dispose()
                Conn = Nothing
            End If
            cmd.Dispose()
            cmd = Nothing
        End Try

    End Function
    Sub CheckforNodeExistence(ByVal strNode As String)
        Try
            Dim i As Int32 = 0
            For i = 0 To TreeView1.Nodes.Count - 1
                If TreeView1.Nodes(i).Text = strNode Then
                    Return
                End If
            Next
            TreeView1.Nodes.Add(strNode, strNode, 0, 0)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub AddToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddToolStripMenuItem.Click
        Dim FO As New OpenFileDialog
        If FO.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim fu As New frmSave
            fu.ConnectionString = _ConnectionString
            If TreeView1.Nodes.Count > 0 Then
                If TreeView1.SelectedNode.Level = 0 Then
                    fu.cboReportCategory.Text = TreeView1.SelectedNode.Text
                End If
            End If
            fu.ShowDialog()

            If fu.DialogResult = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If
            Dim strLabelName As String = fu.txtReportName.Text
            Dim strLabelType As String = fu.cboReportCategory.Text
            If doesExist(strLabelName) Then
                MessageBox.Show("Report " & strLabelName & "already exist. Please Choose another Name.")
                fu.ShowDialog()
            End If
            If strLabelName = "" Then
                Exit Sub
            End If


            Dim Conn As New SqlClient.SqlConnection(_ConnectionString)
            Dim cmd As SqlClient.SqlCommand = Nothing
            Try
                Conn.Open()
                cmd = New SqlClient.SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandText = "INSERT INTO Reports(ReportDescription, REPORTFILE,REPORTPROGRAM,REPORTTYPE) VALUES (@LABELNAME, @LABELCONTENT,'RAWTEXT',@REPORTTYPE)"
                cmd.Parameters.AddWithValue("@LABELNAME", strLabelName)
                cmd.Parameters.AddWithValue("@REPORTTYPE", strLabelType)
                Dim lc As New System.IO.StreamReader(FO.FileName)
                Dim strLABELCONTENTS As String = lc.ReadToEnd
                lc.Close()
                Dim b As Byte() = Encoding.Unicode.GetBytes(strLABELCONTENTS)
                cmd.Parameters.AddWithValue("@LabelContent", b)
                cmd.Connection = Conn
                cmd.ExecuteNonQuery()
                CheckforNodeExistence(strLabelType)
                TreeView1.Nodes(strLabelType).Nodes.Add(strLabelName, strLabelName, 1, 1)
                TreeView1.SelectedNode = TreeView1.Nodes(strLabelType).Nodes(strLabelName)
                _currentparent = strLabelType
                _currentLabel = strLabelName

            Catch ex As SqlClient.SqlException

            Catch ex As Exception

            Finally
                If Conn.State = ConnectionState.Open Then
                    Conn.Close()
                    Conn.Dispose()
                    Conn = Nothing
                End If
                cmd.Dispose()
                cmd = Nothing
            End Try

        End If
    End Sub
    Sub CheckParentDelete(ByVal strParent As String)
        Try
            Dim i As Int32 = 0
            For i = 0 To TreeView1.Nodes.Count - 1
                If TreeView1.Nodes(i).Text = strParent Then
                    If TreeView1.Nodes(i).Nodes.Count = 0 Then
                        TreeView1.Nodes(strParent).Remove()
                        Return
                    End If
                End If
            Next

        Catch ex As Exception

        End Try

    End Sub
    Private Sub TreeView1_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect
        Dim mn As TreeNode = e.Node

        If mn.Level <> 0 Then
            Dim result As Object
            Dim Conn As New SqlClient.SqlConnection(_ConnectionString)
            Dim cmd As SqlClient.SqlCommand = Nothing
            Try
                _currentLabel = mn.Text
                _currentparent = mn.Parent.Text
                Conn.Open()
                cmd = New SqlClient.SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandText = "Select ReportFile from Reports Where ReportDescription = '" & mn.Text & "'"

                cmd.Connection = Conn
                result = cmd.ExecuteScalar
                Dim strModified As String = System.Text.Encoding.Unicode.GetString(result)
                RichTextBox1.Text = strModified
                If RichTextBox1.Text.StartsWith("^XA") Then
                    pnlZPL.Visible = True
                    DrawLabel()
                    SizeBox()
                    RichTextBox1.Dock = DockStyle.Left
                Else
                    RichTextBox1.Dock = DockStyle.Fill
                    pnlZPL.Visible = False
                End If


            Catch ex As SqlClient.SqlException
                MsgBox(ex.ToString)
            Catch ex As Exception
                MsgBox(ex.ToString)
            Finally
                If Conn.State = ConnectionState.Open Then
                    Conn.Close()
                    Conn.Dispose()
                    Conn = Nothing
                End If
                cmd.Dispose()
                cmd = Nothing
            End Try
        End If
    End Sub

    Sub SizeBox()
        Try
            Dim h = TextBox1.Text
            Dim w = TextBox2.Text
            Dim graphics As Graphics = Me.CreateGraphics()
            Dim dpiX As Single = graphics.DpiX
            Dim dpiY As Single = graphics.DpiY
            graphics.Dispose()
            Dim widthInPixels As Integer = CInt(h * dpiX)
            Dim heightInPixels As Integer = CInt(w * dpiY)
            PictureBox1.Size = New Size(widthInPixels, heightInPixels)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub PrintToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripMenuItem.Click
        Dim pc As New PrintDialog
        If pc.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Printerout.SendStringToPrinter(pc.PrinterSettings.PrinterName, RichTextBox1.Text)
        End If

    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        AddToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
        DeleteToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        SaveToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click
        Dim pc As New PrintDialog
        If pc.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Printerout.SendStringToPrinter(pc.PrinterSettings.PrinterName, RichTextBox1.Text)
        End If

    End Sub

    Sub DrawLabel()
        Try
            Dim mZPL As New ZPLConverter
            mZPL.ConvertZPLToImage(RichTextBox1.Text, TextBox1.Text & "x" & TextBox2.Text, PictureBox1, ComboBox1.SelectedItem)
            SizeBox()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged, TextBox2.TextChanged
        SizeBox()
    End Sub

    Private Sub RawFileEdit_ImeModeChanged(sender As Object, e As EventArgs) Handles Me.ImeModeChanged

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If boolStartup = False Then
            DrawLabel()
        Else
            boolStartup = False
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim originalImage As Bitmap = New Bitmap(PictureBox1.Image) ' Create a copy of the original image

        originalImage.RotateFlip(RotateFlipType.Rotate90FlipNone) ' Rotate 90 degrees clockwise

        PictureBox1.Image = originalImage ' Update the PictureBox with the rotated image

    End Sub
End Class
