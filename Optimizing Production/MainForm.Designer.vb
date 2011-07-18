<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.TRow = New System.Windows.Forms.NumericUpDown()
        Me.TCol = New System.Windows.Forms.NumericUpDown()
        Me.PartMinTime = New System.Windows.Forms.NumericUpDown()
        Me.PartMaxTime = New System.Windows.Forms.NumericUpDown()
        Me.TTime = New System.Windows.Forms.NumericUpDown()
        Me.JStatus = New System.Windows.Forms.StatusStrip()
        Me.Programmer = New System.Windows.Forms.ToolStripStatusLabel()
        Me.List1 = New System.Windows.Forms.ListView()
        Me.Tree1 = New System.Windows.Forms.TreeView()
        Me.BtnRefresh = New System.Windows.Forms.Button()
        Me.LblRow = New System.Windows.Forms.Label()
        Me.LblCol = New System.Windows.Forms.Label()
        Me.LblpartMinTime = New System.Windows.Forms.Label()
        Me.LblPartMaxTime = New System.Windows.Forms.Label()
        Me.lblTTime = New System.Windows.Forms.Label()
        CType(Me.TRow, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TCol, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PartMinTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PartMaxTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TTime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.JStatus.SuspendLayout()
        Me.SuspendLayout()
        '
        'TRow
        '
        resources.ApplyResources(Me.TRow, "TRow")
        Me.TRow.Minimum = New Decimal(New Integer() {2, 0, 0, 0})
        Me.TRow.Name = "TRow"
        Me.TRow.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'TCol
        '
        resources.ApplyResources(Me.TCol, "TCol")
        Me.TCol.Minimum = New Decimal(New Integer() {2, 0, 0, 0})
        Me.TCol.Name = "TCol"
        Me.TCol.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'PartMinTime
        '
        resources.ApplyResources(Me.PartMinTime, "PartMinTime")
        Me.PartMinTime.Maximum = New Decimal(New Integer() {14, 0, 0, 0})
        Me.PartMinTime.Minimum = New Decimal(New Integer() {3, 0, 0, 0})
        Me.PartMinTime.Name = "PartMinTime"
        Me.PartMinTime.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'PartMaxTime
        '
        resources.ApplyResources(Me.PartMaxTime, "PartMaxTime")
        Me.PartMaxTime.Maximum = New Decimal(New Integer() {30, 0, 0, 0})
        Me.PartMaxTime.Minimum = New Decimal(New Integer() {15, 0, 0, 0})
        Me.PartMaxTime.Name = "PartMaxTime"
        Me.PartMaxTime.Value = New Decimal(New Integer() {15, 0, 0, 0})
        '
        'TTime
        '
        resources.ApplyResources(Me.TTime, "TTime")
        Me.TTime.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.TTime.Minimum = New Decimal(New Integer() {2, 0, 0, 0})
        Me.TTime.Name = "TTime"
        Me.TTime.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'JStatus
        '
        Me.JStatus.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Programmer})
        resources.ApplyResources(Me.JStatus, "JStatus")
        Me.JStatus.Name = "JStatus"
        Me.JStatus.SizingGrip = False
        '
        'Programmer
        '
        Me.Programmer.Name = "Programmer"
        resources.ApplyResources(Me.Programmer, "Programmer")
        '
        'List1
        '
        Me.List1.FullRowSelect = True
        Me.List1.GridLines = True
        resources.ApplyResources(Me.List1, "List1")
        Me.List1.Name = "List1"
        Me.List1.UseCompatibleStateImageBehavior = False
        '
        'Tree1
        '
        resources.ApplyResources(Me.Tree1, "Tree1")
        Me.Tree1.Name = "Tree1"
        '
        'BtnRefresh
        '
        resources.ApplyResources(Me.BtnRefresh, "BtnRefresh")
        Me.BtnRefresh.Name = "BtnRefresh"
        Me.BtnRefresh.UseVisualStyleBackColor = True
        '
        'LblRow
        '
        resources.ApplyResources(Me.LblRow, "LblRow")
        Me.LblRow.Name = "LblRow"
        '
        'LblCol
        '
        resources.ApplyResources(Me.LblCol, "LblCol")
        Me.LblCol.Name = "LblCol"
        '
        'LblpartMinTime
        '
        resources.ApplyResources(Me.LblpartMinTime, "LblpartMinTime")
        Me.LblpartMinTime.Name = "LblpartMinTime"
        '
        'LblPartMaxTime
        '
        resources.ApplyResources(Me.LblPartMaxTime, "LblPartMaxTime")
        Me.LblPartMaxTime.Name = "LblPartMaxTime"
        '
        'lblTTime
        '
        resources.ApplyResources(Me.lblTTime, "lblTTime")
        Me.lblTTime.Name = "lblTTime"
        '
        'MainForm
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.lblTTime)
        Me.Controls.Add(Me.LblPartMaxTime)
        Me.Controls.Add(Me.LblpartMinTime)
        Me.Controls.Add(Me.LblCol)
        Me.Controls.Add(Me.LblRow)
        Me.Controls.Add(Me.BtnRefresh)
        Me.Controls.Add(Me.Tree1)
        Me.Controls.Add(Me.List1)
        Me.Controls.Add(Me.JStatus)
        Me.Controls.Add(Me.TTime)
        Me.Controls.Add(Me.PartMaxTime)
        Me.Controls.Add(Me.PartMinTime)
        Me.Controls.Add(Me.TCol)
        Me.Controls.Add(Me.TRow)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "MainForm"
        Me.Opacity = 0.97R
        CType(Me.TRow, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TCol, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PartMinTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PartMaxTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TTime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.JStatus.ResumeLayout(False)
        Me.JStatus.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TRow As System.Windows.Forms.NumericUpDown
    Friend WithEvents TCol As System.Windows.Forms.NumericUpDown
    Friend WithEvents PartMinTime As System.Windows.Forms.NumericUpDown
    Friend WithEvents PartMaxTime As System.Windows.Forms.NumericUpDown
    Friend WithEvents TTime As System.Windows.Forms.NumericUpDown
    Friend WithEvents JStatus As System.Windows.Forms.StatusStrip
    Friend WithEvents Programmer As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents List1 As System.Windows.Forms.ListView
    Friend WithEvents Tree1 As System.Windows.Forms.TreeView
    Friend WithEvents BtnRefresh As System.Windows.Forms.Button
    Friend WithEvents LblRow As System.Windows.Forms.Label
    Friend WithEvents LblCol As System.Windows.Forms.Label
    Friend WithEvents LblpartMinTime As System.Windows.Forms.Label
    Friend WithEvents LblPartMaxTime As System.Windows.Forms.Label
    Friend WithEvents lblTTime As System.Windows.Forms.Label

End Class
