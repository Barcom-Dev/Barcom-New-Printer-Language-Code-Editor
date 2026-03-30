Imports System.ComponentModel
Imports System.IO
Imports System.Net
Imports System.Text

Public Class RawFileEdit
    Inherits WeifenLuo.WinFormsUI.DockContent
    Private _ZplConverter As New ZPLConverter()
    Private _ProgramOptionsTable As DataTable
    Private _ProgramOption As Int32
    Private _AvailableTask As New Specialized.StringCollection
    Private _AvailableOptions As Specialized.StringCollection

    'Private _connectionstring As String = My.Settings.ConnectionStirng
    Dim deletingCurrentLabel As Boolean = False
    Dim _currentLabel As String = ""
    Dim _currentparent As String = ""
    Private _UserCode As String = "bhamer"
    Private _ConnectionString As String = "User ID=sa;Password=waves428&Blanket;Initial Catalog=barcomdemo;Data Source=SQL.EBARCOM.COM,9876"
    Dim boolStartup As Boolean = True
    Dim boolLabelChange As Boolean = False
    Private _syncingLabelFromDatabase As Boolean = False
    Dim _lastHitContextMenuNode As TreeNode = Nothing
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
        DensityComboBox.SelectedIndex = 1
        ImagingModeComboBox.SelectedIndex = 0
        UnitComboBox.SelectedIndex = 0
    End Sub
    Sub LoadFiles()
        Try

            Dim Conn As New SqlClient.SqlConnection(_ConnectionString)
            Dim cmd As New SqlClient.SqlCommand

            cmd.Connection = Conn
            cmd.Connection.Open()

            Dim i As Int32 = 0
            For i = 0 To TreeView1.Nodes.Count - 1
                cmd.CommandText = "Select ReportDescription from Reports where ReportType = '" & TreeView1.Nodes(i).Text & "' and ReportProgram = 'RAWTEXT'"
                Dim dr As SqlClient.SqlDataReader = cmd.ExecuteReader
                While dr.Read
                    'do stuff here
                    TreeView1.Nodes(i).Nodes.Add(dr("ReportDescription"), dr("ReportDescription"), "FILE", "FILE")
                    pnlZPL.Visible = True
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
        pnlZPL.Visible = False
        cmd.Connection = Conn
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "Select ReportType from Reports where ReportProgram = 'RAWTEXT' and isnull(ReportTYPE,'') <> '' Group by ReportType "
        Try

            cmd.Connection.Open()
            Dim dr As SqlClient.SqlDataReader = cmd.ExecuteReader
            While dr.Read
                TreeView1.Nodes.Add(dr("REPORTTYPE"), dr("REPORTTYPE"), "Folder", "Folder")
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

    Private Sub SaveCurrentLabel(Optional labelName As String = Nothing)
        Dim labelToSave As String = If(String.IsNullOrEmpty(labelName), _currentLabel, labelName)
        Dim densityDb As String = _ZplConverter.GetStringDPMValue(
            If(DensityComboBox.SelectedItem IsNot Nothing, DensityComboBox.SelectedItem.ToString(), ""))
        Dim colorMode As String = If(ImagingModeComboBox.SelectedItem IsNot Nothing,
            ImagingModeComboBox.SelectedItem.ToString(), "Grayscale")
        Dim labelW As Single
        Dim labelH As Single
        If Not Single.TryParse(LabelWidthBox.Text, labelW) Then
            labelW = 4
        End If
        If Not Single.TryParse(LabelHeightBox.Text, labelH) Then
            labelH = 6
        End If
        Dim labelUnits As String = If(UnitComboBox.SelectedItem IsNot Nothing,
            UnitComboBox.SelectedItem.ToString().ToLower(), "inches")

        Dim Conn As New SqlClient.SqlConnection(_ConnectionString)
        Dim cmd As SqlClient.SqlCommand = Nothing
        Try
            Conn.Open()
            cmd = New SqlClient.SqlCommand
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "Update Reports Set ReportFile = @LabelContent, Density = @Density, ColorMode = @ColorMode, " &
                "LabelWidth = @LabelWidth, LabelHeight = @LabelHeight, LabelUnits = @LabelUnits " &
                "Where REPORTDESCRIPTION = @ReportDesc"

            Dim b As Byte() = Encoding.Unicode.GetBytes(RawZPLText.Text)
            cmd.Parameters.AddWithValue("@LabelContent", b)
            cmd.Parameters.AddWithValue("@Density", densityDb)
            cmd.Parameters.AddWithValue("@ColorMode", colorMode)
            cmd.Parameters.AddWithValue("@LabelWidth", labelW)
            cmd.Parameters.AddWithValue("@LabelHeight", labelH)
            cmd.Parameters.AddWithValue("@LabelUnits", labelUnits)
            cmd.Parameters.AddWithValue("@ReportDesc", labelToSave)

            cmd.Connection = Conn
            cmd.ExecuteNonQuery()
        Catch ex As SqlClient.SqlException
            Console.WriteLine(ex.ToString())
        Catch ex As Exception
            Console.WriteLine(ex.ToString())
        Finally
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
                Conn.Dispose()
                Conn = Nothing
            End If
        End Try
    End Sub

    Private Sub OnClickSaveToolStripMenuItem(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem.Click
        If TreeView1.SelectedNode Is Nothing OrElse TreeView1.SelectedNode.Level = 0 Then
            MessageBox.Show("Please select a label to save")
            Exit Sub
        End If
        Dim selectedLabel As String = TreeView1.SelectedNode.Text
        If MessageBox.Show("Are you Sure you want to Save the Label: " & selectedLabel, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        If selectedLabel = _currentLabel Then
            _currentLabel = ""
            boolLabelChange = False
            SaveCurrentLabel(selectedLabel)
            UnmarkNodeAsChanged(TreeView1.SelectedNode)
            Exit Sub
        End If
        SaveCurrentLabel(selectedLabel)
    End Sub

    Sub Loaditup()
        FillTYpe()
    End Sub

    Private Sub OnClickDeleteToolStripMenuItem(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If _lastHitContextMenuNode Is Nothing Then
            Exit Sub
        End If

        Dim Conn As New SqlClient.SqlConnection(_ConnectionString)
        Dim cmd As SqlClient.SqlCommand = Nothing
        Try
            If _lastHitContextMenuNode.Level = 0 Then
                Exit Sub
            End If
            Dim selectedLabel As String = _lastHitContextMenuNode.Text
            Dim selectedParent As String = _lastHitContextMenuNode.Parent.Text
            If MessageBox.Show("Are you Sure you want to Delete the label: " & selectedLabel, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Conn.Open()
                cmd = New SqlClient.SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandText = "Delete From Reports where ReportDescription = '" & selectedLabel & "'"
                cmd.Connection = Conn
                cmd.ExecuteNonQuery()
                If selectedLabel = _currentLabel Then
                    _currentLabel = ""
                    boolLabelChange = False
                    pnlZPL.Visible = False
                    boolStartup = True
                    RawZPLText.Text = ""
                End If
                TreeView1.Nodes(selectedParent).Nodes(selectedLabel).Remove()
                CheckParentDelete(selectedParent)
            End If
        Catch ex As SqlClient.SqlException
            Console.WriteLine(ex.ToString())
        Catch ex As Exception
            Console.WriteLine(ex.ToString())
        Finally
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
                Conn.Dispose()
                Conn = Nothing
            End If
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
            TreeView1.Nodes.Add(strNode, strNode, "Folder", "Folder")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub OnClickAddToolStripMenuItem(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddToolStripMenuItem.Click
        If boolLabelChange Then
            Dim choice As DialogResult = TreeView1_BeforeSelect(Nothing, Nothing)
            If choice = DialogResult.Cancel Then
                Return
            End If
            boolLabelChange = False
            boolStartup = True
            pnlZPL.Visible = False
        End If

        Dim FO As New OpenFileDialog
        Dim Result As DialogResult = FO.ShowDialog()
        If Result = Windows.Forms.DialogResult.OK Then
            Dim fs As New frmSave
            fs.ConnectionString = _ConnectionString
            If TreeView1.Nodes.Count > 0 Then
                If TreeView1.SelectedNode Is Nothing Then
                    fs.cboReportCategory.Text = TreeView1.Nodes(0).Text
                ElseIf TreeView1.SelectedNode.Level = 0 Then
                    fs.cboReportCategory.Text = TreeView1.SelectedNode.Text
                Else
                    fs.cboReportCategory.Text = TreeView1.Nodes(0).Text
                End If
            End If
            fs.ShowDialog()

            If fs.DialogResult = Windows.Forms.DialogResult.Cancel Then
                _currentLabel = ""
                _currentparent = ""
                _lastHitContextMenuNode = Nothing
                TreeView1.SelectedNode = Nothing
                Exit Sub
            End If
            Dim strLabelName As String = fs.txtReportName.Text
            Dim strLabelType As String = fs.cboReportCategory.Text
            If doesExist(strLabelName) Then
                MessageBox.Show("Report " & strLabelName & "already exist. Please Choose another Name.")
                fs.ShowDialog()
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
                Dim strLABELCONTENTS As String = lc.ReadToEnd()
                lc.Close()
                lc.Dispose()
                lc = Nothing
                Dim b As Byte() = Encoding.Unicode.GetBytes(strLABELCONTENTS)
                cmd.Parameters.AddWithValue("@LabelContent", b)
                cmd.Connection = Conn
                cmd.ExecuteNonQuery()
                CheckforNodeExistence(strLabelType)
                TreeView1.Nodes(strLabelType).Nodes.Add(strLabelName, strLabelName, "FILE", "FILE")
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
        ElseIf Result = Windows.Forms.DialogResult.Cancel Then
            _currentLabel = ""
            _currentparent = ""
            boolLabelChange = False
            boolStartup = True
            pnlZPL.Visible = False
            _lastHitContextMenuNode = Nothing
            TreeView1.SelectedNode = Nothing
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

    ' Add a variable to track the current rotation angle
    Private _currentRotationAngle As Integer = 0

    Sub SetDensity(ByVal density As String)
        If density = "6dpmm" Then
            DensityComboBox.SelectedIndex = 0
            Exit Sub
        End If
        If density = "12dpmm" Then
            DensityComboBox.SelectedIndex = 2
            Exit Sub
        End If
        If density = "24dpmm" Then
            DensityComboBox.SelectedIndex = 3
            Exit Sub
        End If
        ' Default to 8 dpmm (203 dpi)
        DensityComboBox.SelectedIndex = 1
    End Sub

    Sub SetColorMode(ByVal colorMode As String)
        If colorMode = "Bitonal" Then
            ImagingModeComboBox.SelectedIndex = 1
            Exit Sub
        End If
        ImagingModeComboBox.SelectedIndex = 0
    End Sub

    Sub SetLabelWidth(ByVal labelWidth As Single)
        If String.IsNullOrEmpty(labelWidth) Then
            LabelWidthBox.Text = "4"
            Exit Sub
        End If
        LabelWidthBox.Text = labelWidth.ToString()
    End Sub

    Sub SetLabelHeight(ByVal labelHeight As Single)
        If String.IsNullOrEmpty(labelHeight) Then
            LabelHeightBox.Text = "6"
            Exit Sub
        End If
        LabelHeightBox.Text = labelHeight.ToString()
    End Sub

    Sub SetLabelUnits(ByVal labelUnits As String)
        If String.IsNullOrEmpty(labelUnits) Then
            UnitComboBox.SelectedIndex = 0
            Exit Sub
        End If
        If labelUnits = "cm" Then
            UnitComboBox.SelectedIndex = 2
            Exit Sub
        End If
        If labelUnits = "mm" Then
            UnitComboBox.SelectedIndex = 1
            Exit Sub
        End If
        ' Default to inches
        UnitComboBox.SelectedIndex = 0
    End Sub

    Sub DrawZPLLabel()
        Dim mn As TreeNode = TreeView1.SelectedNode
        If mn.Level = 0 Then
            Exit Sub
        End If
        ' Reset the rotation angle when a new label is selected
        _currentRotationAngle = 0
        PreviewPictureBox.Image = Nothing ' Clear the previous image
        Dim Conn As New SqlClient.SqlConnection(_ConnectionString)
        Dim cmd As SqlClient.SqlCommand = Nothing
        Try
            _currentLabel = mn.Text
            _currentparent = mn.Parent.Text
            Conn.Open()
            cmd = New SqlClient.SqlCommand
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "Select Density, ColorMode, LabelWidth, LabelHeight, LabelUnits, ReportFile  from Reports Where ReportDescription = '" & mn.Text & "'"
            cmd.Connection = Conn
            Dim strModified As String = String.Empty
            Dim dr As SqlClient.SqlDataReader = Nothing
            _syncingLabelFromDatabase = True
            Try
                dr = cmd.ExecuteReader()
                While dr.Read
                    strModified = System.Text.Encoding.Unicode.GetString(dr("ReportFile"))
                    SetDensity(If(dr("Density") Is DBNull.Value, "8dpmm", dr("Density")))
                    SetColorMode(If(dr("ColorMode") Is DBNull.Value, "Grayscale", dr("ColorMode")))
                    SetLabelWidth(If(dr("LabelWidth") Is DBNull.Value, 4, dr("LabelWidth")))
                    SetLabelHeight(If(dr("LabelHeight") Is DBNull.Value, 6, dr("LabelHeight")))
                    SetLabelUnits(If(dr("LabelUnits") Is DBNull.Value, "inches", dr("LabelUnits")))
                End While
                dr.Close()
                dr = Nothing
                boolLabelChange = False
                boolStartup = True
                RawZPLText.Text = strModified
                boolStartup = False
                If RawZPLText.Text.StartsWith("^XA") Then
                    DrawLabel()
                    SizeBox()
                    ' RawZPLText.Dock = DockStyle.Left
                Else
                    ' RawZPLText.Dock = DockStyle.Fill
                End If
            Finally
                _syncingLabelFromDatabase = False
                If dr IsNot Nothing Then
                    dr.Close()
                End If
            End Try

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
    End Sub


    Private Sub TreeView1_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect
        lblSelected.Text = e.Node.Text
        Dim mn As TreeNode = TreeView1.SelectedNode
        If mn.Level <> 0 Then
            pnlZPL.Visible = True
            DrawZPLLabel()
        Else
            boolLabelChange = False
            boolStartup = True
            RawZPLText.Text = ""
            pnlZPL.Visible = False
        End If
    End Sub

    Private Sub MarkNodeAsChanged(node As TreeNode)
        Dim nodeText As String = node.Text.Clone()
        Dim nodeFont As Font = node.NodeFont
        If nodeFont Is Nothing OrElse nodeFont.Style <> FontStyle.Bold Then
            nodeFont = New Font(TreeView1.Font, FontStyle.Bold)
        End If
        node.NodeFont = nodeFont
        node.Text = nodeText
    End Sub

    Private Sub UnmarkNodeAsChanged(node As TreeNode)
        node.NodeFont = Nothing
    End Sub

    Private Sub UnmarkDirtyLabelTreeNode()
        Dim dirty As TreeNode = Nothing
        If Not String.IsNullOrEmpty(_currentparent) AndAlso Not String.IsNullOrEmpty(_currentLabel) AndAlso TreeView1.Nodes.ContainsKey(_currentparent) Then
            Dim parentNode As TreeNode = TreeView1.Nodes(_currentparent)
            If parentNode.Nodes.ContainsKey(_currentLabel) Then
                dirty = parentNode.Nodes(_currentLabel)
            End If
        End If
        If dirty Is Nothing AndAlso TreeView1.SelectedNode IsNot Nothing AndAlso TreeView1.SelectedNode.Level <> 0 Then
            dirty = TreeView1.SelectedNode
        End If
        If dirty IsNot Nothing Then
            UnmarkNodeAsChanged(dirty)
        End If
    End Sub

    Private Function TreeView1_BeforeSelect(sender As Object, e As TreeViewCancelEventArgs) As DialogResult Handles TreeView1.BeforeSelect
        If Not boolLabelChange OrElse _currentLabel = "" Then
            Return Nothing
        End If
        Dim choice As DialogResult = MessageBox.Show($"{_currentLabel} has unsaved changes. Do you want to Save before proceeding?", "Save Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
        Select Case choice
            Case DialogResult.Yes
                SaveCurrentLabel()
                boolLabelChange = False
                UnmarkDirtyLabelTreeNode()
            Case DialogResult.No
                boolLabelChange = False
                UnmarkDirtyLabelTreeNode()
            Case DialogResult.Cancel
                If e IsNot Nothing Then
                    e.Cancel = True
                End If
                Return DialogResult.Cancel
        End Select
        Return choice
    End Function

    Sub SizeBox()
        Try
            Dim widthInches As Single, heightInches As Single
            If Not GetLabelSizeInInches(widthInches, heightInches) Then
                Exit Sub
            End If

            ' Get screen DPI
            Dim graphics As Graphics = Me.CreateGraphics()
            Dim dpiX As Single = graphics.DpiX
            Dim dpiY As Single = graphics.DpiY
            graphics.Dispose()

            ' Convert inches to pixels - consider the current rotation
            Dim widthInPixels As Integer
            Dim heightInPixels As Integer

            If _currentRotationAngle = 90 OrElse _currentRotationAngle = 270 Then
                widthInPixels = CInt(heightInches * dpiX)
                heightInPixels = CInt(widthInches * dpiY)
            Else
                widthInPixels = CInt(widthInches * dpiX)
                heightInPixels = CInt(heightInches * dpiY)
            End If

            ' Set the PictureBox size (used when no image yet; ConvertZPLToImage2/RotateLabel set final size)
            PreviewPictureBox.Size = New Size(widthInPixels, heightInPixels)
            PreviewPictureBox.SizeMode = PictureBoxSizeMode.Normal
        Catch ex As Exception
            Debug.WriteLine($"Error in SizeBox: {ex.Message}")
        End Try
    End Sub

    Private Sub OnClickAddFromFile(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddFromFileToolStripButton.Click
        OnClickAddToolStripMenuItem(Nothing, Nothing)
    End Sub

    Private Sub OnClickDelete(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripButton.Click
        If TreeView1.SelectedNode Is Nothing OrElse TreeView1.SelectedNode.Level = 0 Then
            MessageBox.Show("Please select a label to delete")
            Exit Sub
        End If
        _lastHitContextMenuNode = TreeView1.SelectedNode
        OnClickDeleteToolStripMenuItem(Nothing, Nothing)
    End Sub

    Private Sub OnClickSave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        If TreeView1.SelectedNode Is Nothing OrElse TreeView1.SelectedNode.Level = 0 Then
            MessageBox.Show("Please select a label to save")
            Exit Sub
        End If
        If MessageBox.Show("Are you Sure you want to Save the Label: " & _currentLabel, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        Dim selectedLabel As String = TreeView1.SelectedNode.Text
        SaveCurrentLabel(selectedLabel)
        If selectedLabel = _currentLabel Then
            boolLabelChange = False
            UnmarkNodeAsChanged(TreeView1.SelectedNode)
        End If
    End Sub

    Private Sub OnClickPrint(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Dim pc As New PrintDialog
        If _currentLabel = "" Then
            MessageBox.Show("Please select a label to print")
            Exit Sub
        End If
        If RawZPLText.Text = "" Then
            MessageBox.Show("Please enter ZPL code")
            Exit Sub
        End If
        If pc.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Printerout.SendStringToPrinter(pc.PrinterSettings.PrinterName, RawZPLText.Text)
        End If
    End Sub

    Sub DrawLabel()
        Try
            Dim widthInches As Single, heightInches As Single
            If Not GetLabelSizeInInches(widthInches, heightInches) Then
                MessageBox.Show("Please enter valid width and height values")
                Exit Sub
            End If

            ' Size the PictureBox first (for initial layout before image loads)
            SizeBox()

            If String.IsNullOrWhiteSpace(RawZPLText.Text) Then
                MessageBox.Show("Please enter ZPL code")
                Exit Sub
            End If

            Dim selectedDPM As String = If(DensityComboBox.SelectedItem IsNot Nothing, DensityComboBox.SelectedItem.ToString(), "8 dpmm (203 dpi)")
            Dim sizeInInches As String = widthInches.ToString() & "x" & heightInches.ToString()

            _ZplConverter.ConvertZPLToImage2(RawZPLText.Text, sizeInInches, PreviewPictureBox, selectedDPM)

            If _currentRotationAngle > 0 Then
                RotateLabel(_currentRotationAngle)
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub RotateLabel(ByVal angle As Integer)
        If PreviewPictureBox.Image Is Nothing Then Return

        Dim originalImage As Bitmap = New Bitmap(PreviewPictureBox.Image)
        Dim oldImage As Image = PreviewPictureBox.Image

        Select Case angle
            Case 90
                originalImage.RotateFlip(RotateFlipType.Rotate90FlipNone)
            Case 180
                originalImage.RotateFlip(RotateFlipType.Rotate180FlipNone)
            Case 270
                originalImage.RotateFlip(RotateFlipType.Rotate270FlipNone)
        End Select

        PreviewPictureBox.Image = originalImage

        ' Swap PictureBox dimensions for 90/270 so display matches rotated image
        If angle = 90 OrElse angle = 270 Then
            PreviewPictureBox.Size = New Size(PreviewPictureBox.Height, PreviewPictureBox.Width)
        End If

        If oldImage IsNot Nothing AndAlso oldImage IsNot originalImage Then
            oldImage.Dispose()
        End If
    End Sub


    Private Sub OnClickRotateToolStripButton(sender As Object, e As EventArgs) Handles RotateToolStripButton.Click
        Try
            ' Check if there's an image to rotate
            If PreviewPictureBox.Image Is Nothing Then
                Return
            End If

            If TreeView1.SelectedNode Is Nothing OrElse TreeView1.SelectedNode.Level = 0 Then
                Return
            End If

            ' Update the rotation angle
            _currentRotationAngle = (_currentRotationAngle + 90) Mod 360

            ' Store the current ZPL code
            Dim currentZPL As String = RawZPLText.Text

            ' Redraw the label with the current ZPL code
            DrawLabel()

            ' Refresh the PictureBox
            PreviewPictureBox.Refresh()
        Catch ex As Exception
            MessageBox.Show($"Error rotating image: {ex.Message}")
        End Try
    End Sub

    Private Sub OnClickFillVariablesToolStripButton(sender As Object, e As EventArgs) Handles FillVariablesToolStripButton.Click
        ' Check if there's ZPL code to process
        If String.IsNullOrWhiteSpace(RawZPLText.Text) Then
            MessageBox.Show("Please enter ZPL code first.")
            Return
        End If

        ' Find all XB variables in the ZPL code
        Dim zplCode As String = RawZPLText.Text
        Dim regex As New System.Text.RegularExpressions.Regex("XB[A-Z0-9]+")
        Dim matches As System.Text.RegularExpressions.MatchCollection = regex.Matches(zplCode)

        ' Extract unique variable names
        Dim variables As New HashSet(Of String)()
        For Each match As System.Text.RegularExpressions.Match In matches
            variables.Add(match.Value)
        Next

        ' If no variables found, inform the user
        If variables.Count = 0 Then
            MessageBox.Show("No XB variables found in ZPL code.")
            Return
        End If

        ' Create a new form to collect variable values
        Dim inputForm As New Form()
        inputForm.Text = "Enter Values for Variables"
        inputForm.Width = 400
        inputForm.Height = 100 + (variables.Count * 30)
        inputForm.FormBorderStyle = FormBorderStyle.FixedDialog
        inputForm.StartPosition = FormStartPosition.CenterParent
        inputForm.MaximizeBox = False
        inputForm.MinimizeBox = False

        ' Create a table layout panel to organize the form controls
        Dim tablePanel As New TableLayoutPanel()
        tablePanel.RowCount = variables.Count + 1 ' +1 for the button row
        tablePanel.ColumnCount = 2
        tablePanel.Dock = DockStyle.Fill
        tablePanel.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 40))
        tablePanel.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 60))

        ' Dictionary to store the textboxes by variable name
        Dim textBoxes As New Dictionary(Of String, TextBox)()

        ' Add labels and textboxes for each variable
        Dim rowIndex As Integer = 0
        For Each variable As String In variables
            ' Label for variable name
            Dim lbl As New Label()
            lbl.Text = variable & ":"
            lbl.Anchor = AnchorStyles.Left
            lbl.AutoSize = True
            tablePanel.Controls.Add(lbl, 0, rowIndex)

            ' Textbox for variable value
            Dim txt As New TextBox()
            txt.Width = 200
            txt.Anchor = AnchorStyles.Left Or AnchorStyles.Right
            tablePanel.Controls.Add(txt, 1, rowIndex)

            ' Store the textbox in dictionary
            textBoxes.Add(variable, txt)

            rowIndex += 1
        Next

        ' Create Button panel for OK/Cancel
        Dim buttonPanel As New Panel()
        buttonPanel.Height = 40
        buttonPanel.Dock = DockStyle.Bottom

        ' OK Button
        Dim btnOK As New Button()
        btnOK.Text = "OK"
        btnOK.DialogResult = DialogResult.OK
        btnOK.Left = inputForm.Width - 160
        btnOK.Top = 5
        btnOK.Width = 75

        ' Cancel Button
        Dim btnCancel As New Button()
        btnCancel.Text = "Cancel"
        btnCancel.DialogResult = DialogResult.Cancel
        btnCancel.Left = inputForm.Width - 80
        btnCancel.Top = 5
        btnCancel.Width = 75

        ' Add buttons to panel
        buttonPanel.Controls.Add(btnOK)
        buttonPanel.Controls.Add(btnCancel)

        ' Add panels to form
        inputForm.Controls.Add(tablePanel)
        inputForm.Controls.Add(buttonPanel)
        inputForm.AcceptButton = btnOK
        inputForm.CancelButton = btnCancel

        ' Show the form
        If inputForm.ShowDialog() = DialogResult.OK Then
            ' Replace variables in ZPL code with user-entered values
            Dim newZPL As String = zplCode

            For Each variable As String In variables
                Dim value As String = textBoxes(variable).Text
                ' Replace all instances of this variable with its value
                newZPL = newZPL.Replace(variable, value)
            Next

            ' Update the ZPL code
            RawZPLText.Text = newZPL

            ' Redraw the label with the new ZPL, which will also apply existing rotation
            DrawLabel()
        End If
    End Sub

    Private Sub OnLabelChanged(sender As Object, e As EventArgs) Handles DensityComboBox.SelectedIndexChanged, ImagingModeComboBox.SelectedIndexChanged, LabelWidthBox.TextChanged, LabelHeightBox.TextChanged, UnitComboBox.SelectedIndexChanged, RawZPLText.TextChanged
        If boolStartup OrElse _syncingLabelFromDatabase OrElse TreeView1.SelectedNode Is Nothing OrElse TreeView1.SelectedNode.Level = 0 Then
            Return
        End If
        boolLabelChange = True
        MarkNodeAsChanged(TreeView1.SelectedNode)
        If sender IsNot RawZPLText AndAlso LabelHeightBox.TextLength > 0 AndAlso LabelWidthBox.TextLength > 0 AndAlso _ZplConverter.IsValidZPL(RawZPLText.Text) Then
            DrawLabel()
        End If
    End Sub

    Private Sub OnClickNewLabel(sender As Object, e As EventArgs) Handles NewLabelToolStripButton.Click
        If boolLabelChange Then
            Dim choice As DialogResult = TreeView1_BeforeSelect(Nothing, Nothing)
            If choice = DialogResult.Cancel Then
                Return
            End If
            boolLabelChange = False
            boolStartup = True
            pnlZPL.Visible = False
        End If

        Try
            RawZPLText.Text = ""
            Dim fs As New frmSave
            fs.ConnectionString = _ConnectionString
            If TreeView1.Nodes.Count > 0 Then
                If TreeView1.SelectedNode Is Nothing Then
                    fs.cboReportCategory.Text = TreeView1.Nodes(0).Text
                ElseIf TreeView1.SelectedNode.Level = 0 Then
                    fs.cboReportCategory.Text = TreeView1.SelectedNode.Text
                Else
                    fs.cboReportCategory.Text = TreeView1.Nodes(0).Text
                End If
            End If
            fs.ShowDialog()

            If fs.DialogResult = Windows.Forms.DialogResult.Cancel Then
                _currentLabel = ""
                _currentparent = ""
                _lastHitContextMenuNode = Nothing
                TreeView1.SelectedNode = Nothing
                Exit Sub
            End If
            Dim strLabelName As String = fs.txtReportName.Text
            Dim strLabelType As String = fs.cboReportCategory.Text
            If doesExist(strLabelName) Then
                MessageBox.Show("Report " & strLabelName & "already exist. Please Choose another Name.")
                fs.ShowDialog()
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
                Dim strLABELCONTENTS As String = RawZPLText.Text
                Dim b As Byte() = Encoding.Unicode.GetBytes(strLABELCONTENTS)
                cmd.Parameters.AddWithValue("@LabelContent", b)
                cmd.Connection = Conn
                cmd.ExecuteNonQuery()
                CheckforNodeExistence(strLabelType)
                TreeView1.Nodes(strLabelType).Nodes.Add(strLabelName, strLabelName, "FILE", "FILE")
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

            'Dim strFileName As String = InputBox("What do you want to call this file?", "File Name", "ZPL")
            'If strFileName = "" Then
            '    Exit Sub
            'End If

        Catch ex As Exception

        End Try
    End Sub

    Private Function GetErrorMessage() As String
        Dim width As Integer = -1
        Dim height As Integer = -1

        Integer.TryParse(LabelWidthBox.Text, width)
        Integer.TryParse(LabelHeightBox.Text, height)

        If width <= 0 Then
            Return "Invalid width. Please enter a valid width."
        End If
        If height <= 0 Then
            Return "Invalid height. Please enter a valid height."
        End If

        If Not _ZplConverter.IsValidZPL(RawZPLText.Text) Then
            Return "Invalid ZPL code. Please enter a valid ZPL code."
        End If
        Return ""
    End Function

    Private Function ConvertToInches(value As String, currentUnit As String) As Single
        Dim result As Single = -1
        Single.TryParse(value, result)
        If currentUnit = "cm" Then
            result = result / 2.54
        ElseIf currentUnit = "mm" Then
            result = result / 25.4
        End If
        Return result
    End Function

    Private Function GetLabelSizeInInches(ByRef widthInches As Single, ByRef heightInches As Single) As Boolean
        Dim units As String = If(UnitComboBox.SelectedItem IsNot Nothing, UnitComboBox.SelectedItem.ToString().ToLower(), "inches")
        widthInches = ConvertToInches(LabelWidthBox.Text, units)
        heightInches = ConvertToInches(LabelHeightBox.Text, units)
        Return widthInches > 0 AndAlso heightInches > 0
    End Function

    Private Sub SaveFileLocally(fileName As String, fileData As Byte(), extension As String)
        Using saveFileDialog As New SaveFileDialog()
            saveFileDialog.Filter = extension.ToUpper() & "|*." & extension
            saveFileDialog.Title = "Save " & fileName
            saveFileDialog.FileName = fileName
            saveFileDialog.RestoreDirectory = True
            If (saveFileDialog.ShowDialog() = DialogResult.OK) Then
                Dim filePath As String = saveFileDialog.FileName
                File.WriteAllBytes(filePath, fileData)
                MessageBox.Show("File saved to " & filePath)
            End If
        End Using
    End Sub

    Private Function GetImageStream(contentTypeHeaderValue As String) As MemoryStream
        Dim errorMessage As String = GetErrorMessage()
        If errorMessage <> "" Then
            MessageBox.Show(errorMessage)
            Return Nothing
        End If
        Dim dpm As String = _ZplConverter.GetStringDPMValue(DensityComboBox.SelectedItem.ToString())
        Dim quality As String = ImagingModeComboBox.SelectedItem

        Dim units As String = If(UnitComboBox.SelectedItem IsNot Nothing, UnitComboBox.SelectedItem.ToString().ToLower(), "inches")
        Dim width As Single = ConvertToInches(LabelWidthBox.Text, units)
        Dim height As Single = ConvertToInches(LabelHeightBox.Text, units)

        Dim size As String = width & "x" & height

        Dim imageStream As MemoryStream = _ZplConverter.FetchImageDataFromAPI(RawZPLText.Text, size, dpm, contentTypeHeaderValue, quality)
        Return imageStream
    End Function

    Private Sub OnClickDownloadZPL(sender As Object, e As EventArgs) Handles DownloadButtonZPL.Click
        Dim errorMessage As String = GetErrorMessage()
        If errorMessage <> "" Then
            MessageBox.Show(errorMessage)
            Return
        End If
        Dim fileName As String = If(_currentLabel <> "", _currentLabel, "label.zpl")
        SaveFileLocally(fileName, Encoding.Unicode.GetBytes(RawZPLText.Text), "zpl")
    End Sub

    Private Sub OnClickDownloadPNG(sender As Object, e As EventArgs) Handles DownloadButtonPNG.Click
        Dim imageStream As MemoryStream = GetImageStream("image/png")
        If imageStream Is Nothing Then
            Return
        End If

        Dim fileName As String = If(_currentLabel <> "", _currentLabel, "label.png")
        SaveFileLocally(fileName, imageStream.ToArray(), "png")
        imageStream.Dispose()
    End Sub

    Private Sub OnClickDownloadPDF(sender As Object, e As EventArgs) Handles DownloadButtonPDF.Click
        Dim imageStream As MemoryStream = GetImageStream("application/pdf")
        If imageStream Is Nothing Then
            Return
        End If

        Dim fileName As String = If(_currentLabel <> "", _currentLabel, "label.pdf")
        SaveFileLocally(fileName, imageStream.ToArray(), "pdf")
        imageStream.Dispose()
    End Sub

    Private Sub OnClickDownloadEPL(sender As Object, e As EventArgs) Handles DownloadButtonEPL.Click
        Dim imageStream As MemoryStream = GetImageStream("application/epl")
        If imageStream Is Nothing Then
            Return
        End If

        Dim fileName As String = If(_currentLabel <> "", _currentLabel, "label.epl")
        SaveFileLocally(fileName, imageStream.ToArray(), "epl")
        imageStream.Dispose()
    End Sub

    Private Sub OnClickDownloadMultiPDF(sender As Object, e As EventArgs) Handles DownloadButtonMultiPDF.Click
        Dim imageStream As MemoryStream = GetImageStream("application/multi-page-pdf")
        If imageStream Is Nothing Then
            Return
        End If

        Dim fileName As String = If(_currentLabel <> "", _currentLabel, "label.pdf")
        SaveFileLocally(fileName, imageStream.ToArray(), "pdf")
        imageStream.Dispose()
    End Sub

    Private Sub OnClickRedraw(sender As Object, e As EventArgs) Handles RedrawButton.Click
        Dim widthInches As Single, heightInches As Single
        If Not GetLabelSizeInInches(widthInches, heightInches) Then Return
        Dim dpm As String = If(DensityComboBox.SelectedItem IsNot Nothing, DensityComboBox.SelectedItem.ToString(), "8 dpmm (203 dpi)")
        Me._ZplConverter.ConvertZPLToImage2(RawZPLText.Text, widthInches.ToString() & "x" & heightInches.ToString(), PreviewPictureBox, dpm)
        If _currentRotationAngle > 0 Then RotateLabel(_currentRotationAngle)
    End Sub

    Private Sub OnClickRotate(sender As Object, e As EventArgs) Handles RotateButton.Click
        _currentRotationAngle = (_currentRotationAngle + 90) Mod 360
        DrawLabel()
    End Sub

    Private Sub OnClickOpenFile(sender As Object, e As EventArgs) Handles OpenFileButton.Click
        Using openFileDialog As New OpenFileDialog()
            openFileDialog.Filter = "ZPL Files (*.zpl)|*.zpl|Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
            openFileDialog.Title = "Pick a ZPL File"
            openFileDialog.RestoreDirectory = True
            If (openFileDialog.ShowDialog() = DialogResult.OK) Then
                Try
                    Dim filePath As String = openFileDialog.FileName
                    Dim zplCode As String = File.ReadAllText(filePath)
                    RawZPLText.Text = zplCode
                Catch ex As Exception
                    MessageBox.Show("Error reading file: " & ex.Message)
                End Try
            End If
        End Using
    End Sub

    Private Sub OnClickPermalink(sender As Object, e As EventArgs) Handles PermalinkButton.Click
        If String.IsNullOrWhiteSpace(RawZPLText.Text) OrElse RawZPLText.Text.Length = 0 Then
            MessageBox.Show("Please enter ZPL code first.")
            Return
        End If

        Dim width As Integer = -1
        Dim height As Integer = -1

        Try
            Integer.TryParse(LabelWidthBox.Text, width)
        Catch ex As Exception
            MessageBox.Show("Invalid width value " & LabelWidthBox.Text)
        End Try

        Try
            Integer.TryParse(LabelHeightBox.Text, height)
        Catch ex As Exception
            MessageBox.Show("Invalid height value " & LabelHeightBox.Text)
        End Try

        Dim density As Integer = Me._ZplConverter.GetIntegerDPMValue(DensityComboBox.SelectedItem.ToString())
        Dim zpl As String = Uri.EscapeDataString(RawZPLText.Text)
        Dim quality As String = If(ImagingModeComboBox.SelectedItem IsNot Nothing, ImagingModeComboBox.SelectedItem.ToString().ToLower(), "grayscale")
        Dim permalink As String = "https://labelary.com/viewer.html?density=" & density & "&quality=" & quality
        If (width > -1) Then
            permalink = permalink & "&width=" & width
        End If
        If (height > -1) Then
            permalink = permalink & "&height=" & height
        End If

        Dim units As String = If(UnitComboBox.SelectedItem IsNot Nothing, UnitComboBox.SelectedItem.ToString().ToLower(), "inches")
        permalink = permalink & "&units=" & units

        permalink = permalink & "&zpl=" & zpl
        Clipboard.SetText(permalink)
        MessageBox.Show("Permalink copied to clipboard")
    End Sub

    Private Function IsNumericKey(e As KeyEventArgs) As Boolean
        Return (e.KeyCode >= 48 AndAlso e.KeyCode <= 57) OrElse (e.KeyCode >= 96 AndAlso e.KeyCode <= 105)
    End Function

    Private Function ShouldSuppressKey(e As KeyEventArgs) As Boolean
        Return Not (Me.IsNumericKey(e) OrElse e.KeyData = Keys.Back OrElse e.KeyData = Keys.Delete)
    End Function

    Private Sub OnKeydownDimensionBox(sender As Object, e As KeyEventArgs) Handles LabelWidthBox.KeyDown, LabelHeightBox.KeyDown
        If (Me.ShouldSuppressKey(e)) Then
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub OnLeaveLabelWidthBox(sender As Object, e As EventArgs) Handles LabelWidthBox.Leave
        If LabelWidthBox.Text = "" Then
            LabelWidthBox.Text = "4"
        End If
    End Sub

    Private Sub OnLeaveLabelHeightBox(sender As Object, e As EventArgs) Handles LabelHeightBox.Leave
        If LabelHeightBox.Text = "" Then
            LabelHeightBox.Text = "6"
        End If
    End Sub

    Private Sub OnOpeningContextMenu(sender As Object, e As CancelEventArgs) Handles ContextMenuStrip6.Opening
        Dim node As TreeNode = TreeView1.SelectedNode
        Dim pt As Point = TreeView1.PointToClient(Control.MousePosition)
        Dim hit As TreeNode = TreeView1.GetNodeAt(pt)
        _lastHitContextMenuNode = hit
        If hit Is Nothing Then
            e.Cancel = True
            Exit Sub
        End If
        node = hit
        Dim newFileButton As ToolStripMenuItem = ContextMenuStrip6.Items(0)
        Dim addButton As ToolStripMenuItem = ContextMenuStrip6.Items(1)
        Dim deleteButton As ToolStripMenuItem = ContextMenuStrip6.Items(2)
        Dim saveButton As ToolStripMenuItem = ContextMenuStrip6.Items(3)
        Dim printButton As ToolStripMenuItem = ContextMenuStrip6.Items(4)
        If node.Level = 0 Then
            ' For now no new file button
            newFileButton.Visible = True
            addButton.Visible = True
            deleteButton.Visible = False
            saveButton.Visible = False
            printButton.Visible = False
        Else
            newFileButton.Visible = False
            addButton.Visible = False
            deleteButton.Visible = True
            printButton.Visible = False
            saveButton.Visible = True
            ' If not current label, hide save button.
            If node.Text <> _currentLabel Then
                saveButton.Visible = False
            End If
        End If
    End Sub

    Private Sub OnClickNewToolStripMenuItem(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewFileToolStripMenuItem.Click
        If boolLabelChange Then
            Dim choice As DialogResult = TreeView1_BeforeSelect(Nothing, Nothing)
            If choice = DialogResult.Cancel Then
                Return
            End If
            boolLabelChange = False
            boolStartup = True
            pnlZPL.Visible = False
        End If

        Dim fs As New frmSave
        fs.ConnectionString = _ConnectionString
        If TreeView1.Nodes.Count > 0 Then
            If _lastHitContextMenuNode IsNot Nothing AndAlso _lastHitContextMenuNode.Level = 0 Then
                fs.cboReportCategory.Text = _lastHitContextMenuNode.Text
            End If
        End If
        fs.ShowDialog()

        If fs.DialogResult = Windows.Forms.DialogResult.Cancel Then
            _currentLabel = ""
            _currentparent = ""
            _lastHitContextMenuNode = Nothing
            TreeView1.SelectedNode = Nothing
            Exit Sub
        End If
        Dim strLabelName As String = fs.txtReportName.Text
        Dim strLabelType As String = fs.cboReportCategory.Text
        If doesExist(strLabelName) Then
            MessageBox.Show("Report " & strLabelName & "already exist. Please Choose another Name.")
            fs.ShowDialog()
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
            Dim strLABELCONTENTS As String = ""
            Dim b As Byte() = Encoding.Unicode.GetBytes(strLABELCONTENTS)
            cmd.Parameters.AddWithValue("@LabelContent", b)
            cmd.Connection = Conn
            cmd.ExecuteNonQuery()
            CheckforNodeExistence(strLabelType)
            TreeView1.Nodes(strLabelType).Nodes.Add(strLabelName, strLabelName, "FILE", "FILE")
            TreeView1.SelectedNode = TreeView1.Nodes(strLabelType).Nodes(strLabelName)
            _lastHitContextMenuNode = TreeView1.Nodes(strLabelType).Nodes(strLabelName)
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

    End Sub

End Class