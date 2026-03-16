<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class RawFileEdit

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RawFileEdit))
        Me.RawZPLText = New System.Windows.Forms.RichTextBox()
        Me.ContextMenuStrip6 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AddToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrintToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TreeView1 = New System.Windows.Forms.TreeView()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.pnlZPL = New System.Windows.Forms.Panel()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.GroupBoxEditor = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.PermalinkButton = New System.Windows.Forms.Button()
        Me.OpenFileButton = New System.Windows.Forms.Button()
        Me.RotateButton = New System.Windows.Forms.Button()
        Me.RedrawButton = New System.Windows.Forms.Button()
        Me.PrintOptionsGroupBox = New System.Windows.Forms.GroupBox()
        Me.UnitComboBox = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LabelHeightBox = New System.Windows.Forms.TextBox()
        Me.LabelWidthBox = New System.Windows.Forms.TextBox()
        Me.ImagingModeLabel = New System.Windows.Forms.Label()
        Me.ImagingModeComboBox = New System.Windows.Forms.ComboBox()
        Me.DensityLabel = New System.Windows.Forms.Label()
        Me.DensityComboBox = New System.Windows.Forms.ComboBox()
        Me.WidthHeightLabel = New System.Windows.Forms.Label()
        Me.LinterGroupBox = New System.Windows.Forms.GroupBox()
        Me.LinterWarningsLabel = New System.Windows.Forms.Label()
        Me.GroupBoxPreview = New System.Windows.Forms.GroupBox()
        Me.PreviewScrollPanel = New System.Windows.Forms.Panel()
        Me.PreviewPictureBox = New System.Windows.Forms.PictureBox()
        Me.DownloadsGroupBox = New System.Windows.Forms.GroupBox()
        Me.DownloadButtonMultiPDF = New System.Windows.Forms.Button()
        Me.DownloadButtonEPL = New System.Windows.Forms.Button()
        Me.DownloadButtonPDF = New System.Windows.Forms.Button()
        Me.DownloadButtonPNG = New System.Windows.Forms.Button()
        Me.DownloadButtonZPL = New System.Windows.Forms.Button()
        Me.lblSelected = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.NewFileToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.AddFromFileToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.DeleteToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.SaveToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.PrintToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.RotateToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.FillVariablesToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ContextMenuStrip6.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.pnlZPL.SuspendLayout()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.GroupBoxEditor.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.PrintOptionsGroupBox.SuspendLayout()
        Me.LinterGroupBox.SuspendLayout()
        Me.GroupBoxPreview.SuspendLayout()
        Me.PreviewScrollPanel.SuspendLayout()
        CType(Me.PreviewPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DownloadsGroupBox.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'RawZPLText
        '
        Me.RawZPLText.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RawZPLText.Location = New System.Drawing.Point(0, 161)
        Me.RawZPLText.Name = "RawZPLText"
        Me.RawZPLText.Size = New System.Drawing.Size(450, 239)
        Me.RawZPLText.TabIndex = 5
        Me.RawZPLText.Text = ""
        '
        'ContextMenuStrip6
        '
        Me.ContextMenuStrip6.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ContextMenuStrip6.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddToolStripMenuItem, Me.DeleteToolStripMenuItem, Me.SaveToolStripMenuItem, Me.PrintToolStripMenuItem})
        Me.ContextMenuStrip6.Name = "ContextMenuStrip6"
        Me.ContextMenuStrip6.Size = New System.Drawing.Size(143, 132)
        '
        'AddToolStripMenuItem
        '
        Me.AddToolStripMenuItem.Image = CType(resources.GetObject("AddToolStripMenuItem.Image"), System.Drawing.Image)
        Me.AddToolStripMenuItem.Name = "AddToolStripMenuItem"
        Me.AddToolStripMenuItem.Size = New System.Drawing.Size(142, 32)
        Me.AddToolStripMenuItem.Text = "Add"
        '
        'DeleteToolStripMenuItem
        '
        Me.DeleteToolStripMenuItem.Image = CType(resources.GetObject("DeleteToolStripMenuItem.Image"), System.Drawing.Image)
        Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        Me.DeleteToolStripMenuItem.Size = New System.Drawing.Size(142, 32)
        Me.DeleteToolStripMenuItem.Text = "Delete"
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Image = CType(resources.GetObject("SaveToolStripMenuItem.Image"), System.Drawing.Image)
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(142, 32)
        Me.SaveToolStripMenuItem.Text = "Save"
        '
        'PrintToolStripMenuItem
        '
        Me.PrintToolStripMenuItem.Image = CType(resources.GetObject("PrintToolStripMenuItem.Image"), System.Drawing.Image)
        Me.PrintToolStripMenuItem.Name = "PrintToolStripMenuItem"
        Me.PrintToolStripMenuItem.Size = New System.Drawing.Size(142, 32)
        Me.PrintToolStripMenuItem.Text = "Print"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 70)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.TreeView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.pnlZPL)
        Me.SplitContainer1.Size = New System.Drawing.Size(1348, 586)
        Me.SplitContainer1.SplitterDistance = 300
        Me.SplitContainer1.SplitterWidth = 5
        Me.SplitContainer1.TabIndex = 7
        '
        'TreeView1
        '
        Me.TreeView1.ContextMenuStrip = Me.ContextMenuStrip6
        Me.TreeView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeView1.ImageIndex = 0
        Me.TreeView1.ImageList = Me.ImageList1
        Me.TreeView1.Location = New System.Drawing.Point(0, 0)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.SelectedImageIndex = 0
        Me.TreeView1.Size = New System.Drawing.Size(300, 586)
        Me.TreeView1.TabIndex = 0
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "FILE")
        Me.ImageList1.Images.SetKeyName(1, "Folder")
        Me.ImageList1.Images.SetKeyName(2, "source_code-query.ico")
        Me.ImageList1.Images.SetKeyName(3, "zebra.jpg")
        '
        'pnlZPL
        '
        Me.pnlZPL.Controls.Add(Me.FlowLayoutPanel1)
        Me.pnlZPL.Controls.Add(Me.lblSelected)
        Me.pnlZPL.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlZPL.Location = New System.Drawing.Point(0, 0)
        Me.pnlZPL.Name = "pnlZPL"
        Me.pnlZPL.Size = New System.Drawing.Size(1043, 586)
        Me.pnlZPL.TabIndex = 7
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.AutoScroll = True
        Me.FlowLayoutPanel1.Controls.Add(Me.GroupBoxEditor)
        Me.FlowLayoutPanel1.Controls.Add(Me.GroupBoxPreview)
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(0, 34)
        Me.FlowLayoutPanel1.Margin = New System.Windows.Forms.Padding(3, 8, 3, 3)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Padding = New System.Windows.Forms.Padding(5)
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(1043, 552)
        Me.FlowLayoutPanel1.TabIndex = 20
        '
        'GroupBoxEditor
        '
        Me.GroupBoxEditor.Controls.Add(Me.RawZPLText)
        Me.GroupBoxEditor.Controls.Add(Me.GroupBox1)
        Me.GroupBoxEditor.Controls.Add(Me.PrintOptionsGroupBox)
        Me.GroupBoxEditor.Controls.Add(Me.LinterGroupBox)
        Me.GroupBoxEditor.Location = New System.Drawing.Point(8, 8)
        Me.GroupBoxEditor.MaximumSize = New System.Drawing.Size(800, 0)
        Me.GroupBoxEditor.MinimumSize = New System.Drawing.Size(300, 400)
        Me.GroupBoxEditor.Name = "GroupBoxEditor"
        Me.GroupBoxEditor.Size = New System.Drawing.Size(450, 400)
        Me.GroupBoxEditor.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.PermalinkButton)
        Me.GroupBox1.Controls.Add(Me.OpenFileButton)
        Me.GroupBox1.Controls.Add(Me.RotateButton)
        Me.GroupBox1.Controls.Add(Me.RedrawButton)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(0, 96)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(450, 65)
        Me.GroupBox1.TabIndex = 19
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Controls"
        '
        'PermalinkButton
        '
        Me.PermalinkButton.AutoSize = True
        Me.PermalinkButton.Font = New System.Drawing.Font("Baskerville Old Face", 9.75!)
        Me.PermalinkButton.Location = New System.Drawing.Point(245, 20)
        Me.PermalinkButton.Name = "PermalinkButton"
        Me.PermalinkButton.Size = New System.Drawing.Size(103, 33)
        Me.PermalinkButton.TabIndex = 18
        Me.PermalinkButton.Text = "Permalink"
        Me.PermalinkButton.UseVisualStyleBackColor = True
        '
        'OpenFileButton
        '
        Me.OpenFileButton.AutoSize = True
        Me.OpenFileButton.Font = New System.Drawing.Font("Baskerville Old Face", 9.75!)
        Me.OpenFileButton.Location = New System.Drawing.Point(156, 20)
        Me.OpenFileButton.Name = "OpenFileButton"
        Me.OpenFileButton.Size = New System.Drawing.Size(95, 33)
        Me.OpenFileButton.TabIndex = 17
        Me.OpenFileButton.Text = "Open file"
        Me.OpenFileButton.UseVisualStyleBackColor = True
        '
        'RotateButton
        '
        Me.RotateButton.AutoSize = True
        Me.RotateButton.Font = New System.Drawing.Font("Baskerville Old Face", 9.75!)
        Me.RotateButton.Location = New System.Drawing.Point(78, 20)
        Me.RotateButton.Name = "RotateButton"
        Me.RotateButton.Size = New System.Drawing.Size(71, 33)
        Me.RotateButton.TabIndex = 3
        Me.RotateButton.Text = "Rotate"
        Me.RotateButton.UseVisualStyleBackColor = True
        '
        'RedrawButton
        '
        Me.RedrawButton.AutoSize = True
        Me.RedrawButton.Font = New System.Drawing.Font("Baskerville Old Face", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RedrawButton.Location = New System.Drawing.Point(7, 20)
        Me.RedrawButton.Name = "RedrawButton"
        Me.RedrawButton.Size = New System.Drawing.Size(80, 33)
        Me.RedrawButton.TabIndex = 1
        Me.RedrawButton.Text = "Redraw"
        Me.RedrawButton.UseVisualStyleBackColor = True
        '
        'PrintOptionsGroupBox
        '
        Me.PrintOptionsGroupBox.Controls.Add(Me.UnitComboBox)
        Me.PrintOptionsGroupBox.Controls.Add(Me.Label2)
        Me.PrintOptionsGroupBox.Controls.Add(Me.LabelHeightBox)
        Me.PrintOptionsGroupBox.Controls.Add(Me.LabelWidthBox)
        Me.PrintOptionsGroupBox.Controls.Add(Me.ImagingModeLabel)
        Me.PrintOptionsGroupBox.Controls.Add(Me.ImagingModeComboBox)
        Me.PrintOptionsGroupBox.Controls.Add(Me.DensityLabel)
        Me.PrintOptionsGroupBox.Controls.Add(Me.DensityComboBox)
        Me.PrintOptionsGroupBox.Controls.Add(Me.WidthHeightLabel)
        Me.PrintOptionsGroupBox.Dock = System.Windows.Forms.DockStyle.Top
        Me.PrintOptionsGroupBox.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PrintOptionsGroupBox.Location = New System.Drawing.Point(0, 0)
        Me.PrintOptionsGroupBox.Name = "PrintOptionsGroupBox"
        Me.PrintOptionsGroupBox.Size = New System.Drawing.Size(450, 96)
        Me.PrintOptionsGroupBox.TabIndex = 16
        Me.PrintOptionsGroupBox.TabStop = False
        Me.PrintOptionsGroupBox.Text = "Print Options"
        '
        'UnitComboBox
        '
        Me.UnitComboBox.FormattingEnabled = True
        Me.UnitComboBox.Items.AddRange(New Object() {"inches", "mm", "cm"})
        Me.UnitComboBox.Location = New System.Drawing.Point(263, 58)
        Me.UnitComboBox.Name = "UnitComboBox"
        Me.UnitComboBox.Size = New System.Drawing.Size(107, 28)
        Me.UnitComboBox.TabIndex = 19
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(149, 61)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(20, 20)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "x"
        '
        'LabelHeightBox
        '
        Me.LabelHeightBox.Location = New System.Drawing.Point(177, 58)
        Me.LabelHeightBox.Name = "LabelHeightBox"
        Me.LabelHeightBox.Size = New System.Drawing.Size(66, 28)
        Me.LabelHeightBox.TabIndex = 17
        Me.LabelHeightBox.Text = "6"
        Me.LabelHeightBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LabelWidthBox
        '
        Me.LabelWidthBox.Location = New System.Drawing.Point(74, 57)
        Me.LabelWidthBox.Name = "LabelWidthBox"
        Me.LabelWidthBox.Size = New System.Drawing.Size(66, 28)
        Me.LabelWidthBox.TabIndex = 16
        Me.LabelWidthBox.Text = "4"
        Me.LabelWidthBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'ImagingModeLabel
        '
        Me.ImagingModeLabel.AutoSize = True
        Me.ImagingModeLabel.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ImagingModeLabel.Location = New System.Drawing.Point(232, 26)
        Me.ImagingModeLabel.Name = "ImagingModeLabel"
        Me.ImagingModeLabel.Size = New System.Drawing.Size(104, 20)
        Me.ImagingModeLabel.TabIndex = 14
        Me.ImagingModeLabel.Text = "Color Mode"
        '
        'ImagingModeComboBox
        '
        Me.ImagingModeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ImagingModeComboBox.FormattingEnabled = True
        Me.ImagingModeComboBox.Items.AddRange(New Object() {"Grayscale", "Bitonal"})
        Me.ImagingModeComboBox.Location = New System.Drawing.Point(311, 23)
        Me.ImagingModeComboBox.Name = "ImagingModeComboBox"
        Me.ImagingModeComboBox.Size = New System.Drawing.Size(107, 28)
        Me.ImagingModeComboBox.TabIndex = 15
        '
        'DensityLabel
        '
        Me.DensityLabel.AutoSize = True
        Me.DensityLabel.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DensityLabel.Location = New System.Drawing.Point(8, 26)
        Me.DensityLabel.Name = "DensityLabel"
        Me.DensityLabel.Size = New System.Drawing.Size(74, 20)
        Me.DensityLabel.TabIndex = 0
        Me.DensityLabel.Text = "Density"
        '
        'DensityComboBox
        '
        Me.DensityComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.DensityComboBox.FormattingEnabled = True
        Me.DensityComboBox.Items.AddRange(New Object() {"6 dpmm (152 dpi)", "8 dpmm (203 dpi)", "12 dpmm (300 dpi)", "24 dpmm (600 dpi)"})
        Me.DensityComboBox.Location = New System.Drawing.Point(64, 23)
        Me.DensityComboBox.Name = "DensityComboBox"
        Me.DensityComboBox.Size = New System.Drawing.Size(153, 28)
        Me.DensityComboBox.TabIndex = 13
        '
        'WidthHeightLabel
        '
        Me.WidthHeightLabel.AutoSize = True
        Me.WidthHeightLabel.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.WidthHeightLabel.Location = New System.Drawing.Point(7, 61)
        Me.WidthHeightLabel.Name = "WidthHeightLabel"
        Me.WidthHeightLabel.Size = New System.Drawing.Size(96, 20)
        Me.WidthHeightLabel.TabIndex = 10
        Me.WidthHeightLabel.Text = "Label Size"
        '
        'LinterGroupBox
        '
        Me.LinterGroupBox.Controls.Add(Me.LinterWarningsLabel)
        Me.LinterGroupBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LinterGroupBox.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinterGroupBox.Location = New System.Drawing.Point(0, 0)
        Me.LinterGroupBox.Name = "LinterGroupBox"
        Me.LinterGroupBox.Size = New System.Drawing.Size(450, 400)
        Me.LinterGroupBox.TabIndex = 17
        Me.LinterGroupBox.TabStop = False
        Me.LinterGroupBox.Text = "Linter Warnings"
        '
        'LinterWarningsLabel
        '
        Me.LinterWarningsLabel.AutoSize = True
        Me.LinterWarningsLabel.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinterWarningsLabel.Location = New System.Drawing.Point(7, 26)
        Me.LinterWarningsLabel.Name = "LinterWarningsLabel"
        Me.LinterWarningsLabel.Size = New System.Drawing.Size(53, 20)
        Me.LinterWarningsLabel.TabIndex = 0
        Me.LinterWarningsLabel.Text = "None"
        '
        'GroupBoxPreview
        '
        Me.GroupBoxPreview.Controls.Add(Me.PreviewScrollPanel)
        Me.GroupBoxPreview.Controls.Add(Me.DownloadsGroupBox)
        Me.GroupBoxPreview.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBoxPreview.Location = New System.Drawing.Point(464, 8)
        Me.GroupBoxPreview.MaximumSize = New System.Drawing.Size(800, 0)
        Me.GroupBoxPreview.MinimumSize = New System.Drawing.Size(300, 400)
        Me.GroupBoxPreview.Name = "GroupBoxPreview"
        Me.GroupBoxPreview.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.GroupBoxPreview.Size = New System.Drawing.Size(450, 400)
        Me.GroupBoxPreview.TabIndex = 1
        Me.GroupBoxPreview.TabStop = False
        Me.GroupBoxPreview.Text = "Preview"
        '
        'PreviewScrollPanel
        '
        Me.PreviewScrollPanel.AutoScroll = True
        Me.PreviewScrollPanel.Controls.Add(Me.PreviewPictureBox)
        Me.PreviewScrollPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PreviewScrollPanel.Location = New System.Drawing.Point(0, 26)
        Me.PreviewScrollPanel.Name = "PreviewScrollPanel"
        Me.PreviewScrollPanel.Padding = New System.Windows.Forms.Padding(3)
        Me.PreviewScrollPanel.Size = New System.Drawing.Size(450, 292)
        Me.PreviewScrollPanel.TabIndex = 19
        '
        'PreviewPictureBox
        '
        Me.PreviewPictureBox.BackColor = System.Drawing.Color.White
        Me.PreviewPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PreviewPictureBox.Location = New System.Drawing.Point(3, 3)
        Me.PreviewPictureBox.Name = "PreviewPictureBox"
        Me.PreviewPictureBox.Size = New System.Drawing.Size(444, 300)
        Me.PreviewPictureBox.TabIndex = 6
        Me.PreviewPictureBox.TabStop = False
        '
        'DownloadsGroupBox
        '
        Me.DownloadsGroupBox.AutoSize = True
        Me.DownloadsGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.DownloadsGroupBox.Controls.Add(Me.DownloadButtonMultiPDF)
        Me.DownloadsGroupBox.Controls.Add(Me.DownloadButtonEPL)
        Me.DownloadsGroupBox.Controls.Add(Me.DownloadButtonPDF)
        Me.DownloadsGroupBox.Controls.Add(Me.DownloadButtonPNG)
        Me.DownloadsGroupBox.Controls.Add(Me.DownloadButtonZPL)
        Me.DownloadsGroupBox.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.DownloadsGroupBox.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DownloadsGroupBox.Location = New System.Drawing.Point(0, 318)
        Me.DownloadsGroupBox.Margin = New System.Windows.Forms.Padding(6, 3, 3, 3)
        Me.DownloadsGroupBox.Name = "DownloadsGroupBox"
        Me.DownloadsGroupBox.Padding = New System.Windows.Forms.Padding(9, 3, 3, 3)
        Me.DownloadsGroupBox.Size = New System.Drawing.Size(450, 82)
        Me.DownloadsGroupBox.TabIndex = 18
        Me.DownloadsGroupBox.TabStop = False
        Me.DownloadsGroupBox.Text = "Download"
        '
        'DownloadButtonMultiPDF
        '
        Me.DownloadButtonMultiPDF.AutoSize = True
        Me.DownloadButtonMultiPDF.Font = New System.Drawing.Font("Baskerville Old Face", 9.75!)
        Me.DownloadButtonMultiPDF.Location = New System.Drawing.Point(265, 22)
        Me.DownloadButtonMultiPDF.Name = "DownloadButtonMultiPDF"
        Me.DownloadButtonMultiPDF.Size = New System.Drawing.Size(156, 33)
        Me.DownloadButtonMultiPDF.TabIndex = 22
        Me.DownloadButtonMultiPDF.Text = "Multi-Label PDF"
        Me.DownloadButtonMultiPDF.UseVisualStyleBackColor = True
        '
        'DownloadButtonEPL
        '
        Me.DownloadButtonEPL.AutoSize = True
        Me.DownloadButtonEPL.Font = New System.Drawing.Font("Baskerville Old Face", 9.75!)
        Me.DownloadButtonEPL.Location = New System.Drawing.Point(203, 22)
        Me.DownloadButtonEPL.Name = "DownloadButtonEPL"
        Me.DownloadButtonEPL.Size = New System.Drawing.Size(56, 33)
        Me.DownloadButtonEPL.TabIndex = 21
        Me.DownloadButtonEPL.Text = "EPL"
        Me.DownloadButtonEPL.UseVisualStyleBackColor = True
        '
        'DownloadButtonPDF
        '
        Me.DownloadButtonPDF.AutoSize = True
        Me.DownloadButtonPDF.Font = New System.Drawing.Font("Baskerville Old Face", 9.75!)
        Me.DownloadButtonPDF.Location = New System.Drawing.Point(139, 22)
        Me.DownloadButtonPDF.Name = "DownloadButtonPDF"
        Me.DownloadButtonPDF.Size = New System.Drawing.Size(58, 33)
        Me.DownloadButtonPDF.TabIndex = 20
        Me.DownloadButtonPDF.Text = "PDF"
        Me.DownloadButtonPDF.UseVisualStyleBackColor = True
        '
        'DownloadButtonPNG
        '
        Me.DownloadButtonPNG.AutoSize = True
        Me.DownloadButtonPNG.Font = New System.Drawing.Font("Baskerville Old Face", 9.75!)
        Me.DownloadButtonPNG.Location = New System.Drawing.Point(71, 22)
        Me.DownloadButtonPNG.Name = "DownloadButtonPNG"
        Me.DownloadButtonPNG.Size = New System.Drawing.Size(62, 33)
        Me.DownloadButtonPNG.TabIndex = 19
        Me.DownloadButtonPNG.Text = "PNG"
        Me.DownloadButtonPNG.UseVisualStyleBackColor = True
        '
        'DownloadButtonZPL
        '
        Me.DownloadButtonZPL.AutoSize = True
        Me.DownloadButtonZPL.Font = New System.Drawing.Font("Baskerville Old Face", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DownloadButtonZPL.Location = New System.Drawing.Point(9, 22)
        Me.DownloadButtonZPL.Name = "DownloadButtonZPL"
        Me.DownloadButtonZPL.Size = New System.Drawing.Size(56, 33)
        Me.DownloadButtonZPL.TabIndex = 18
        Me.DownloadButtonZPL.Text = "ZPL"
        Me.DownloadButtonZPL.UseVisualStyleBackColor = True
        '
        'lblSelected
        '
        Me.lblSelected.AutoSize = True
        Me.lblSelected.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblSelected.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelected.ForeColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(82, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.lblSelected.Location = New System.Drawing.Point(0, 0)
        Me.lblSelected.Name = "lblSelected"
        Me.lblSelected.Padding = New System.Windows.Forms.Padding(5, 5, 0, 0)
        Me.lblSelected.Size = New System.Drawing.Size(257, 34)
        Me.lblSelected.TabIndex = 14
        Me.lblSelected.Text = "No Label Selected"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(82, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.ToolStrip1.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewFileToolStripButton, Me.AddFromFileToolStripButton, Me.DeleteToolStripButton, Me.SaveToolStripButton, Me.PrintToolStripButton, Me.RotateToolStripButton, Me.FillVariablesToolStripButton})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1348, 70)
        Me.ToolStrip1.TabIndex = 8
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'NewFileToolStripButton
        '
        Me.NewFileToolStripButton.ForeColor = System.Drawing.Color.White
        Me.NewFileToolStripButton.Image = CType(resources.GetObject("NewFileToolStripButton.Image"), System.Drawing.Image)
        Me.NewFileToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.NewFileToolStripButton.Name = "NewFileToolStripButton"
        Me.NewFileToolStripButton.Size = New System.Drawing.Size(69, 65)
        Me.NewFileToolStripButton.Text = "New"
        Me.NewFileToolStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'AddFromFileToolStripButton
        '
        Me.AddFromFileToolStripButton.ForeColor = System.Drawing.Color.White
        Me.AddFromFileToolStripButton.Image = CType(resources.GetObject("AddFromFileToolStripButton.Image"), System.Drawing.Image)
        Me.AddFromFileToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.AddFromFileToolStripButton.Name = "AddFromFileToolStripButton"
        Me.AddFromFileToolStripButton.Size = New System.Drawing.Size(175, 65)
        Me.AddFromFileToolStripButton.Text = "Add from File"
        Me.AddFromFileToolStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'DeleteToolStripButton
        '
        Me.DeleteToolStripButton.ForeColor = System.Drawing.Color.White
        Me.DeleteToolStripButton.Image = CType(resources.GetObject("DeleteToolStripButton.Image"), System.Drawing.Image)
        Me.DeleteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.DeleteToolStripButton.Name = "DeleteToolStripButton"
        Me.DeleteToolStripButton.Size = New System.Drawing.Size(93, 65)
        Me.DeleteToolStripButton.Text = "Delete"
        Me.DeleteToolStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'SaveToolStripButton
        '
        Me.SaveToolStripButton.ForeColor = System.Drawing.Color.White
        Me.SaveToolStripButton.Image = CType(resources.GetObject("SaveToolStripButton.Image"), System.Drawing.Image)
        Me.SaveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.SaveToolStripButton.Name = "SaveToolStripButton"
        Me.SaveToolStripButton.Size = New System.Drawing.Size(75, 65)
        Me.SaveToolStripButton.Text = "Save"
        Me.SaveToolStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'PrintToolStripButton
        '
        Me.PrintToolStripButton.ForeColor = System.Drawing.Color.White
        Me.PrintToolStripButton.Image = CType(resources.GetObject("PrintToolStripButton.Image"), System.Drawing.Image)
        Me.PrintToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintToolStripButton.Name = "PrintToolStripButton"
        Me.PrintToolStripButton.Size = New System.Drawing.Size(71, 65)
        Me.PrintToolStripButton.Text = "Print"
        Me.PrintToolStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'RotateToolStripButton
        '
        Me.RotateToolStripButton.ForeColor = System.Drawing.Color.White
        Me.RotateToolStripButton.Image = CType(resources.GetObject("RotateToolStripButton.Image"), System.Drawing.Image)
        Me.RotateToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.RotateToolStripButton.Name = "RotateToolStripButton"
        Me.RotateToolStripButton.Size = New System.Drawing.Size(94, 65)
        Me.RotateToolStripButton.Text = "Rotate"
        Me.RotateToolStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'FillVariablesToolStripButton
        '
        Me.FillVariablesToolStripButton.ForeColor = System.Drawing.Color.White
        Me.FillVariablesToolStripButton.Image = CType(resources.GetObject("FillVariablesToolStripButton.Image"), System.Drawing.Image)
        Me.FillVariablesToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.FillVariablesToolStripButton.Name = "FillVariablesToolStripButton"
        Me.FillVariablesToolStripButton.Size = New System.Drawing.Size(164, 65)
        Me.FillVariablesToolStripButton.Text = "Fill Variables"
        Me.FillVariablesToolStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'RawFileEdit
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1348, 656)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "RawFileEdit"
        Me.TabText = "Raw File Editor"
        Me.Text = "Raw File Editor"
        Me.ContextMenuStrip6.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.pnlZPL.ResumeLayout(False)
        Me.pnlZPL.PerformLayout()
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.GroupBoxEditor.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.PrintOptionsGroupBox.ResumeLayout(False)
        Me.PrintOptionsGroupBox.PerformLayout()
        Me.LinterGroupBox.ResumeLayout(False)
        Me.LinterGroupBox.PerformLayout()
        Me.GroupBoxPreview.ResumeLayout(False)
        Me.GroupBoxPreview.PerformLayout()
        Me.PreviewScrollPanel.ResumeLayout(False)
        CType(Me.PreviewPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DownloadsGroupBox.ResumeLayout(False)
        Me.DownloadsGroupBox.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RawZPLText As System.Windows.Forms.RichTextBox
    Friend WithEvents ContextMenuStrip6 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AddToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents TreeView1 As System.Windows.Forms.TreeView
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents PrintToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents AddFromFileToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents DeleteToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents SaveToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents PrintToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents PreviewPictureBox As PictureBox
    Friend WithEvents pnlZPL As Panel
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents GroupBoxEditor As Panel
    Friend WithEvents GroupBoxPreview As GroupBox
    Friend WithEvents PreviewScrollPanel As Panel
    Friend WithEvents WidthHeightLabel As Label
    Friend WithEvents DensityComboBox As ComboBox
    Friend WithEvents RotateToolStripButton As ToolStripButton
    Friend WithEvents FillVariablesToolStripButton As ToolStripButton
    Friend WithEvents lblSelected As Label
    Friend WithEvents NewFileToolStripButton As ToolStripButton
    Friend WithEvents PrintOptionsGroupBox As GroupBox
    Friend WithEvents DensityLabel As Label
    Friend WithEvents ImagingModeLabel As Label
    Friend WithEvents ImagingModeComboBox As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents LabelHeightBox As TextBox
    Friend WithEvents LabelWidthBox As TextBox
    Friend WithEvents UnitComboBox As ComboBox
    Friend WithEvents LinterGroupBox As GroupBox
    Friend WithEvents LinterWarningsLabel As Label
    Friend WithEvents DownloadsGroupBox As GroupBox
    Friend WithEvents DownloadButtonMultiPDF As Button
    Friend WithEvents DownloadButtonEPL As Button
    Friend WithEvents DownloadButtonPDF As Button
    Friend WithEvents DownloadButtonPNG As Button
    Friend WithEvents DownloadButtonZPL As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents PermalinkButton As Button
    Friend WithEvents OpenFileButton As Button
    Friend WithEvents RotateButton As Button
    Friend WithEvents RedrawButton As Button
End Class
