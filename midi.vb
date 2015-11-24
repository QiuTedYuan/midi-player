'Name:   QIU Yuan
'Stu ID: 20175552
'Email:  yqiuac@connect.ust.hk
'********************************************************************
'Documentation:
'1. Record and append can record different instruments and the trackbar will also move according to that.
'2. The instrument will be the last one played after a sequence is played.
'3. Chord type can't be selected unless chord mode is checked.
'4. Append don't count the time after the stop of recording and the beginning of append(i.e. it appends right after the sequence without delay).
'5. Keys will move down when playing.
'6. To load drum files, click on the menu.

Option Explicit On
Imports System.IO
Public Class frmMidiPiano
    Inherits System.Windows.Forms.Form

#Region "Windows Form Designer generated code "
    Public Sub New()

        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub
    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
        If Disposing Then
            If Not components Is Nothing Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(Disposing)
    End Sub
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Private WithEvents vsbVolume As System.Windows.Forms.VScrollBar
    Public WithEvents _key_15 As System.Windows.Forms.CheckBox
    Public WithEvents _key_13 As System.Windows.Forms.CheckBox
    Public WithEvents _key_10 As System.Windows.Forms.CheckBox
    Public WithEvents _key_8 As System.Windows.Forms.CheckBox
    Public WithEvents _key_6 As System.Windows.Forms.CheckBox
    Public WithEvents _key_3 As System.Windows.Forms.CheckBox
    Public WithEvents _key_1 As System.Windows.Forms.CheckBox
    Public WithEvents _key_16 As System.Windows.Forms.CheckBox
    Public WithEvents _key_14 As System.Windows.Forms.CheckBox
    Public WithEvents _key_12 As System.Windows.Forms.CheckBox
    Public WithEvents _key_11 As System.Windows.Forms.CheckBox
    Public WithEvents _key_9 As System.Windows.Forms.CheckBox
    Public WithEvents _key_7 As System.Windows.Forms.CheckBox
    Public WithEvents _key_5 As System.Windows.Forms.CheckBox
    Public WithEvents _key_4 As System.Windows.Forms.CheckBox
    Public WithEvents _key_2 As System.Windows.Forms.CheckBox
    Public WithEvents _key_0 As System.Windows.Forms.CheckBox
    Private WithEvents lblVolume As System.Windows.Forms.Label
    Public WithEvents mnuDevice0 As System.Windows.Forms.MenuItem
    Public WithEvents mnuDevice1 As System.Windows.Forms.MenuItem
    Public WithEvents mnuDevice2 As System.Windows.Forms.MenuItem
    Public WithEvents mnuDevice3 As System.Windows.Forms.MenuItem
    Public WithEvents mnuDevice4 As System.Windows.Forms.MenuItem
    Public WithEvents mnuDevice5 As System.Windows.Forms.MenuItem
    Public WithEvents mnuDevice6 As System.Windows.Forms.MenuItem
    Public WithEvents mnuDevice7 As System.Windows.Forms.MenuItem
    Public WithEvents mnuDevice8 As System.Windows.Forms.MenuItem
    Public WithEvents mnuDevice9 As System.Windows.Forms.MenuItem
    Public WithEvents mnuDevice10 As System.Windows.Forms.MenuItem
    Public WithEvents mnuDevice As System.Windows.Forms.MenuItem
    Public WithEvents mnuChannel0 As System.Windows.Forms.MenuItem
    Public WithEvents mnuChannel1 As System.Windows.Forms.MenuItem
    Public WithEvents mnuChannel2 As System.Windows.Forms.MenuItem
    Public WithEvents mnuChannel3 As System.Windows.Forms.MenuItem
    Public WithEvents mnuChannel4 As System.Windows.Forms.MenuItem
    Public WithEvents mnuChannel5 As System.Windows.Forms.MenuItem
    Public WithEvents mnuChannel6 As System.Windows.Forms.MenuItem
    Public WithEvents mnuChannel7 As System.Windows.Forms.MenuItem
    Public WithEvents mnuChannel8 As System.Windows.Forms.MenuItem
    Public WithEvents mnuChannel9 As System.Windows.Forms.MenuItem
    Public WithEvents mnuChannel10 As System.Windows.Forms.MenuItem
    Public WithEvents mnuChannel11 As System.Windows.Forms.MenuItem
    Public WithEvents mnuChannel12 As System.Windows.Forms.MenuItem
    Public WithEvents mnuChannel13 As System.Windows.Forms.MenuItem
    Public WithEvents mnuChannel14 As System.Windows.Forms.MenuItem
    Public WithEvents mnuChannel15 As System.Windows.Forms.MenuItem
    Public WithEvents mnuChannel As System.Windows.Forms.MenuItem
    Public WithEvents mnuBaseNote As System.Windows.Forms.MenuItem
    Public mnuMain As System.Windows.Forms.MainMenu
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Private WithEvents btnPlay As System.Windows.Forms.Button
    Private WithEvents btnStop As System.Windows.Forms.Button
    Private WithEvents btnRecord As System.Windows.Forms.Button
    Private WithEvents btnRemoveSilence As System.Windows.Forms.Button
    Friend WithEvents tbSpeed As System.Windows.Forms.TrackBar
    Friend WithEvents lblSeqSlow As System.Windows.Forms.Label
    Friend WithEvents lblSeqNormal As System.Windows.Forms.Label
    Friend WithEvents lblSeqFast As System.Windows.Forms.Label
    Friend WithEvents gbxInstrument As System.Windows.Forms.GroupBox
    Friend WithEvents tbBankMSB As System.Windows.Forms.TrackBar
    Friend WithEvents tbInstrument As System.Windows.Forms.TrackBar
    Friend WithEvents lblBankMSB As System.Windows.Forms.Label
    Friend WithEvents tmrSequencer As System.Windows.Forms.Timer
    Friend WithEvents tclMidiFunction As System.Windows.Forms.TabControl
    Friend WithEvents tabSequencer As System.Windows.Forms.TabPage
    Friend WithEvents tabDrumMachine As System.Windows.Forms.TabPage
    Friend WithEvents tabWhiteboard As System.Windows.Forms.TabPage
    Friend WithEvents gbxXAxis As System.Windows.Forms.GroupBox
    Friend WithEvents lblXValue As System.Windows.Forms.Label
    Friend WithEvents cboXTitle As System.Windows.Forms.ComboBox
    Friend WithEvents lblXTitle As System.Windows.Forms.Label
    Friend WithEvents lblXCaption As System.Windows.Forms.Label
    Friend WithEvents picWhiteboard As System.Windows.Forms.PictureBox
    Friend WithEvents gbxYAxis As System.Windows.Forms.GroupBox
    Friend WithEvents cboYTitle As System.Windows.Forms.ComboBox
    Friend WithEvents lblYTitle As System.Windows.Forms.Label
    Friend WithEvents lblYCaption As System.Windows.Forms.Label
    Friend WithEvents lblYValue As System.Windows.Forms.Label
    Public WithEvents btnDrumStop As System.Windows.Forms.Button
    Public WithEvents btnDrumStart As System.Windows.Forms.Button
    Public WithEvents picDrum As System.Windows.Forms.PictureBox
    Friend WithEvents mnuFile As System.Windows.Forms.MenuItem
    Friend WithEvents mnuOpen As System.Windows.Forms.MenuItem
    Public WithEvents tmrDrumPlayback As System.Windows.Forms.Timer
    Friend WithEvents ckChordMode As System.Windows.Forms.CheckBox
    Friend WithEvents cbChordMode As System.Windows.Forms.ComboBox
    Friend WithEvents tbBaseNote As System.Windows.Forms.TrackBar
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lbBaseNote As System.Windows.Forms.Label
    Friend WithEvents lbChannel As System.Windows.Forms.Label
    Friend WithEvents tbChannel As System.Windows.Forms.TrackBar
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents tbpanning As System.Windows.Forms.TrackBar
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnReset As System.Windows.Forms.Button
    Private WithEvents btnAppend As System.Windows.Forms.Button
    Friend WithEvents ckLoopMode As System.Windows.Forms.CheckBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents tbTranspose As System.Windows.Forms.TrackBar
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents nRows As System.Windows.Forms.NumericUpDown
    Friend WithEvents nColumns As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents btnRandomIns As System.Windows.Forms.Button
    Public WithEvents btnRandom As System.Windows.Forms.Button
    Public WithEvents btnInversion As System.Windows.Forms.Button
    Public WithEvents btnReversion As System.Windows.Forms.Button
    Friend WithEvents cbDrumNumber As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cbDrumInstrument As System.Windows.Forms.ComboBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents tbDrumSpeed As System.Windows.Forms.TrackBar
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Public WithEvents btnDrumReset As System.Windows.Forms.Button
    Friend WithEvents mnuDrumfile As System.Windows.Forms.MenuItem
    Friend WithEvents mnuDrumOpen As System.Windows.Forms.MenuItem
    Friend WithEvents lblInstrument As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.vsbVolume = New System.Windows.Forms.VScrollBar()
        Me._key_15 = New System.Windows.Forms.CheckBox()
        Me._key_13 = New System.Windows.Forms.CheckBox()
        Me._key_10 = New System.Windows.Forms.CheckBox()
        Me._key_8 = New System.Windows.Forms.CheckBox()
        Me._key_6 = New System.Windows.Forms.CheckBox()
        Me._key_3 = New System.Windows.Forms.CheckBox()
        Me._key_1 = New System.Windows.Forms.CheckBox()
        Me._key_16 = New System.Windows.Forms.CheckBox()
        Me._key_14 = New System.Windows.Forms.CheckBox()
        Me._key_12 = New System.Windows.Forms.CheckBox()
        Me._key_11 = New System.Windows.Forms.CheckBox()
        Me._key_9 = New System.Windows.Forms.CheckBox()
        Me._key_7 = New System.Windows.Forms.CheckBox()
        Me._key_5 = New System.Windows.Forms.CheckBox()
        Me._key_4 = New System.Windows.Forms.CheckBox()
        Me._key_2 = New System.Windows.Forms.CheckBox()
        Me._key_0 = New System.Windows.Forms.CheckBox()
        Me.lblVolume = New System.Windows.Forms.Label()
        Me.mnuChannel0 = New System.Windows.Forms.MenuItem()
        Me.mnuChannel1 = New System.Windows.Forms.MenuItem()
        Me.mnuChannel2 = New System.Windows.Forms.MenuItem()
        Me.mnuChannel3 = New System.Windows.Forms.MenuItem()
        Me.mnuChannel4 = New System.Windows.Forms.MenuItem()
        Me.mnuChannel5 = New System.Windows.Forms.MenuItem()
        Me.mnuChannel6 = New System.Windows.Forms.MenuItem()
        Me.mnuChannel7 = New System.Windows.Forms.MenuItem()
        Me.mnuChannel8 = New System.Windows.Forms.MenuItem()
        Me.mnuChannel9 = New System.Windows.Forms.MenuItem()
        Me.mnuChannel10 = New System.Windows.Forms.MenuItem()
        Me.mnuChannel11 = New System.Windows.Forms.MenuItem()
        Me.mnuChannel12 = New System.Windows.Forms.MenuItem()
        Me.mnuChannel13 = New System.Windows.Forms.MenuItem()
        Me.mnuChannel14 = New System.Windows.Forms.MenuItem()
        Me.mnuChannel15 = New System.Windows.Forms.MenuItem()
        Me.mnuDevice0 = New System.Windows.Forms.MenuItem()
        Me.mnuDevice1 = New System.Windows.Forms.MenuItem()
        Me.mnuDevice2 = New System.Windows.Forms.MenuItem()
        Me.mnuDevice3 = New System.Windows.Forms.MenuItem()
        Me.mnuDevice4 = New System.Windows.Forms.MenuItem()
        Me.mnuDevice5 = New System.Windows.Forms.MenuItem()
        Me.mnuDevice6 = New System.Windows.Forms.MenuItem()
        Me.mnuDevice7 = New System.Windows.Forms.MenuItem()
        Me.mnuDevice8 = New System.Windows.Forms.MenuItem()
        Me.mnuDevice9 = New System.Windows.Forms.MenuItem()
        Me.mnuDevice10 = New System.Windows.Forms.MenuItem()
        Me.mnuMain = New System.Windows.Forms.MainMenu(Me.components)
        Me.mnuFile = New System.Windows.Forms.MenuItem()
        Me.mnuOpen = New System.Windows.Forms.MenuItem()
        Me.mnuDevice = New System.Windows.Forms.MenuItem()
        Me.mnuChannel = New System.Windows.Forms.MenuItem()
        Me.mnuBaseNote = New System.Windows.Forms.MenuItem()
        Me.mnuDrumfile = New System.Windows.Forms.MenuItem()
        Me.mnuDrumOpen = New System.Windows.Forms.MenuItem()
        Me.lblSeqFast = New System.Windows.Forms.Label()
        Me.lblSeqNormal = New System.Windows.Forms.Label()
        Me.lblSeqSlow = New System.Windows.Forms.Label()
        Me.tbSpeed = New System.Windows.Forms.TrackBar()
        Me.btnRemoveSilence = New System.Windows.Forms.Button()
        Me.btnPlay = New System.Windows.Forms.Button()
        Me.btnStop = New System.Windows.Forms.Button()
        Me.btnRecord = New System.Windows.Forms.Button()
        Me.gbxInstrument = New System.Windows.Forms.GroupBox()
        Me.btnReset = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.tbpanning = New System.Windows.Forms.TrackBar()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lbChannel = New System.Windows.Forms.Label()
        Me.tbChannel = New System.Windows.Forms.TrackBar()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lbBaseNote = New System.Windows.Forms.Label()
        Me.tbBaseNote = New System.Windows.Forms.TrackBar()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cbChordMode = New System.Windows.Forms.ComboBox()
        Me.ckChordMode = New System.Windows.Forms.CheckBox()
        Me.tbBankMSB = New System.Windows.Forms.TrackBar()
        Me.tbInstrument = New System.Windows.Forms.TrackBar()
        Me.lblBankMSB = New System.Windows.Forms.Label()
        Me.lblInstrument = New System.Windows.Forms.Label()
        Me.tmrSequencer = New System.Windows.Forms.Timer(Me.components)
        Me.tclMidiFunction = New System.Windows.Forms.TabControl()
        Me.tabSequencer = New System.Windows.Forms.TabPage()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.tbTranspose = New System.Windows.Forms.TrackBar()
        Me.ckLoopMode = New System.Windows.Forms.CheckBox()
        Me.btnAppend = New System.Windows.Forms.Button()
        Me.tabWhiteboard = New System.Windows.Forms.TabPage()
        Me.gbxXAxis = New System.Windows.Forms.GroupBox()
        Me.lblXValue = New System.Windows.Forms.Label()
        Me.cboXTitle = New System.Windows.Forms.ComboBox()
        Me.lblXTitle = New System.Windows.Forms.Label()
        Me.lblXCaption = New System.Windows.Forms.Label()
        Me.picWhiteboard = New System.Windows.Forms.PictureBox()
        Me.gbxYAxis = New System.Windows.Forms.GroupBox()
        Me.cboYTitle = New System.Windows.Forms.ComboBox()
        Me.lblYTitle = New System.Windows.Forms.Label()
        Me.lblYCaption = New System.Windows.Forms.Label()
        Me.lblYValue = New System.Windows.Forms.Label()
        Me.tabDrumMachine = New System.Windows.Forms.TabPage()
        Me.btnDrumReset = New System.Windows.Forms.Button()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.tbDrumSpeed = New System.Windows.Forms.TrackBar()
        Me.cbDrumInstrument = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cbDrumNumber = New System.Windows.Forms.ComboBox()
        Me.btnReversion = New System.Windows.Forms.Button()
        Me.btnInversion = New System.Windows.Forms.Button()
        Me.btnRandom = New System.Windows.Forms.Button()
        Me.btnRandomIns = New System.Windows.Forms.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.nColumns = New System.Windows.Forms.NumericUpDown()
        Me.nRows = New System.Windows.Forms.NumericUpDown()
        Me.btnDrumStop = New System.Windows.Forms.Button()
        Me.btnDrumStart = New System.Windows.Forms.Button()
        Me.picDrum = New System.Windows.Forms.PictureBox()
        Me.tmrDrumPlayback = New System.Windows.Forms.Timer(Me.components)
        CType(Me.tbSpeed, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbxInstrument.SuspendLayout()
        CType(Me.tbpanning, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbChannel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbBaseNote, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbBankMSB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbInstrument, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tclMidiFunction.SuspendLayout()
        Me.tabSequencer.SuspendLayout()
        CType(Me.tbTranspose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabWhiteboard.SuspendLayout()
        Me.gbxXAxis.SuspendLayout()
        CType(Me.picWhiteboard, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbxYAxis.SuspendLayout()
        Me.tabDrumMachine.SuspendLayout()
        CType(Me.tbDrumSpeed, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nColumns, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nRows, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picDrum, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'vsbVolume
        '
        Me.vsbVolume.LargeChange = 1
        Me.vsbVolume.Location = New System.Drawing.Point(12, 25)
        Me.vsbVolume.Maximum = 127
        Me.vsbVolume.Name = "vsbVolume"
        Me.vsbVolume.Size = New System.Drawing.Size(50, 124)
        Me.vsbVolume.TabIndex = 17
        Me.vsbVolume.TabStop = True
        '
        '_key_15
        '
        Me._key_15.Appearance = System.Windows.Forms.Appearance.Button
        Me._key_15.BackColor = System.Drawing.Color.Black
        Me._key_15.ForeColor = System.Drawing.Color.White
        Me._key_15.Location = New System.Drawing.Point(354, 12)
        Me._key_15.Name = "_key_15"
        Me._key_15.Size = New System.Drawing.Size(17, 84)
        Me._key_15.TabIndex = 16
        Me._key_15.Text = ";"
        Me._key_15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me._key_15.UseVisualStyleBackColor = False
        '
        '_key_13
        '
        Me._key_13.Appearance = System.Windows.Forms.Appearance.Button
        Me._key_13.BackColor = System.Drawing.Color.Black
        Me._key_13.ForeColor = System.Drawing.Color.White
        Me._key_13.Location = New System.Drawing.Point(322, 12)
        Me._key_13.Name = "_key_13"
        Me._key_13.Size = New System.Drawing.Size(17, 84)
        Me._key_13.TabIndex = 15
        Me._key_13.Text = "L"
        Me._key_13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me._key_13.UseVisualStyleBackColor = False
        '
        '_key_10
        '
        Me._key_10.Appearance = System.Windows.Forms.Appearance.Button
        Me._key_10.BackColor = System.Drawing.Color.Black
        Me._key_10.ForeColor = System.Drawing.Color.White
        Me._key_10.Location = New System.Drawing.Point(258, 12)
        Me._key_10.Name = "_key_10"
        Me._key_10.Size = New System.Drawing.Size(17, 84)
        Me._key_10.TabIndex = 14
        Me._key_10.Text = "J"
        Me._key_10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me._key_10.UseVisualStyleBackColor = False
        '
        '_key_8
        '
        Me._key_8.Appearance = System.Windows.Forms.Appearance.Button
        Me._key_8.BackColor = System.Drawing.Color.Black
        Me._key_8.ForeColor = System.Drawing.Color.White
        Me._key_8.Location = New System.Drawing.Point(226, 12)
        Me._key_8.Name = "_key_8"
        Me._key_8.Size = New System.Drawing.Size(17, 84)
        Me._key_8.TabIndex = 13
        Me._key_8.Text = "H"
        Me._key_8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me._key_8.UseVisualStyleBackColor = False
        '
        '_key_6
        '
        Me._key_6.Appearance = System.Windows.Forms.Appearance.Button
        Me._key_6.BackColor = System.Drawing.Color.Black
        Me._key_6.ForeColor = System.Drawing.Color.White
        Me._key_6.Location = New System.Drawing.Point(194, 12)
        Me._key_6.Name = "_key_6"
        Me._key_6.Size = New System.Drawing.Size(17, 84)
        Me._key_6.TabIndex = 12
        Me._key_6.Text = "G"
        Me._key_6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me._key_6.UseVisualStyleBackColor = False
        '
        '_key_3
        '
        Me._key_3.Appearance = System.Windows.Forms.Appearance.Button
        Me._key_3.BackColor = System.Drawing.Color.Black
        Me._key_3.ForeColor = System.Drawing.Color.White
        Me._key_3.Location = New System.Drawing.Point(130, 12)
        Me._key_3.Name = "_key_3"
        Me._key_3.Size = New System.Drawing.Size(17, 84)
        Me._key_3.TabIndex = 11
        Me._key_3.Text = "D"
        Me._key_3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me._key_3.UseVisualStyleBackColor = False
        '
        '_key_1
        '
        Me._key_1.Appearance = System.Windows.Forms.Appearance.Button
        Me._key_1.BackColor = System.Drawing.Color.Black
        Me._key_1.ForeColor = System.Drawing.Color.White
        Me._key_1.Location = New System.Drawing.Point(98, 12)
        Me._key_1.Name = "_key_1"
        Me._key_1.Size = New System.Drawing.Size(17, 84)
        Me._key_1.TabIndex = 10
        Me._key_1.Text = "S"
        Me._key_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me._key_1.UseVisualStyleBackColor = False
        '
        '_key_16
        '
        Me._key_16.Appearance = System.Windows.Forms.Appearance.Button
        Me._key_16.BackColor = System.Drawing.Color.White
        Me._key_16.ForeColor = System.Drawing.Color.Black
        Me._key_16.Location = New System.Drawing.Point(362, 12)
        Me._key_16.Name = "_key_16"
        Me._key_16.Size = New System.Drawing.Size(33, 137)
        Me._key_16.TabIndex = 9
        Me._key_16.Text = "/"
        Me._key_16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me._key_16.UseVisualStyleBackColor = False
        '
        '_key_14
        '
        Me._key_14.Appearance = System.Windows.Forms.Appearance.Button
        Me._key_14.BackColor = System.Drawing.Color.White
        Me._key_14.ForeColor = System.Drawing.Color.Black
        Me._key_14.Location = New System.Drawing.Point(330, 12)
        Me._key_14.Name = "_key_14"
        Me._key_14.Size = New System.Drawing.Size(33, 137)
        Me._key_14.TabIndex = 8
        Me._key_14.Text = "."
        Me._key_14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me._key_14.UseVisualStyleBackColor = False
        '
        '_key_12
        '
        Me._key_12.Appearance = System.Windows.Forms.Appearance.Button
        Me._key_12.BackColor = System.Drawing.Color.White
        Me._key_12.ForeColor = System.Drawing.Color.Black
        Me._key_12.Location = New System.Drawing.Point(298, 12)
        Me._key_12.Name = "_key_12"
        Me._key_12.Size = New System.Drawing.Size(33, 137)
        Me._key_12.TabIndex = 7
        Me._key_12.Text = ","
        Me._key_12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me._key_12.UseVisualStyleBackColor = False
        '
        '_key_11
        '
        Me._key_11.Appearance = System.Windows.Forms.Appearance.Button
        Me._key_11.BackColor = System.Drawing.Color.White
        Me._key_11.ForeColor = System.Drawing.Color.Black
        Me._key_11.Location = New System.Drawing.Point(266, 12)
        Me._key_11.Name = "_key_11"
        Me._key_11.Size = New System.Drawing.Size(33, 137)
        Me._key_11.TabIndex = 6
        Me._key_11.Text = "M"
        Me._key_11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me._key_11.UseVisualStyleBackColor = False
        '
        '_key_9
        '
        Me._key_9.Appearance = System.Windows.Forms.Appearance.Button
        Me._key_9.BackColor = System.Drawing.Color.White
        Me._key_9.ForeColor = System.Drawing.Color.Black
        Me._key_9.Location = New System.Drawing.Point(234, 12)
        Me._key_9.Name = "_key_9"
        Me._key_9.Size = New System.Drawing.Size(33, 137)
        Me._key_9.TabIndex = 5
        Me._key_9.Text = "N"
        Me._key_9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me._key_9.UseVisualStyleBackColor = False
        '
        '_key_7
        '
        Me._key_7.Appearance = System.Windows.Forms.Appearance.Button
        Me._key_7.BackColor = System.Drawing.Color.White
        Me._key_7.ForeColor = System.Drawing.Color.Black
        Me._key_7.Location = New System.Drawing.Point(202, 12)
        Me._key_7.Name = "_key_7"
        Me._key_7.Size = New System.Drawing.Size(33, 137)
        Me._key_7.TabIndex = 4
        Me._key_7.Text = "B"
        Me._key_7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me._key_7.UseVisualStyleBackColor = False
        '
        '_key_5
        '
        Me._key_5.Appearance = System.Windows.Forms.Appearance.Button
        Me._key_5.BackColor = System.Drawing.Color.White
        Me._key_5.ForeColor = System.Drawing.Color.Black
        Me._key_5.Location = New System.Drawing.Point(170, 12)
        Me._key_5.Name = "_key_5"
        Me._key_5.Size = New System.Drawing.Size(33, 137)
        Me._key_5.TabIndex = 3
        Me._key_5.Text = "V"
        Me._key_5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me._key_5.UseVisualStyleBackColor = False
        '
        '_key_4
        '
        Me._key_4.Appearance = System.Windows.Forms.Appearance.Button
        Me._key_4.BackColor = System.Drawing.Color.White
        Me._key_4.ForeColor = System.Drawing.Color.Black
        Me._key_4.Location = New System.Drawing.Point(138, 12)
        Me._key_4.Name = "_key_4"
        Me._key_4.Size = New System.Drawing.Size(33, 137)
        Me._key_4.TabIndex = 2
        Me._key_4.Text = "C"
        Me._key_4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me._key_4.UseVisualStyleBackColor = False
        '
        '_key_2
        '
        Me._key_2.Appearance = System.Windows.Forms.Appearance.Button
        Me._key_2.BackColor = System.Drawing.Color.White
        Me._key_2.ForeColor = System.Drawing.Color.Black
        Me._key_2.Location = New System.Drawing.Point(106, 12)
        Me._key_2.Name = "_key_2"
        Me._key_2.Size = New System.Drawing.Size(33, 137)
        Me._key_2.TabIndex = 1
        Me._key_2.Text = "X"
        Me._key_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me._key_2.UseVisualStyleBackColor = False
        '
        '_key_0
        '
        Me._key_0.Appearance = System.Windows.Forms.Appearance.Button
        Me._key_0.BackColor = System.Drawing.Color.White
        Me._key_0.ForeColor = System.Drawing.Color.Black
        Me._key_0.Location = New System.Drawing.Point(74, 12)
        Me._key_0.Name = "_key_0"
        Me._key_0.Size = New System.Drawing.Size(33, 137)
        Me._key_0.TabIndex = 0
        Me._key_0.Text = "Z"
        Me._key_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me._key_0.UseVisualStyleBackColor = False
        '
        'lblVolume
        '
        Me.lblVolume.AutoSize = True
        Me.lblVolume.Location = New System.Drawing.Point(16, 12)
        Me.lblVolume.Name = "lblVolume"
        Me.lblVolume.Size = New System.Drawing.Size(42, 13)
        Me.lblVolume.TabIndex = 18
        Me.lblVolume.Text = "Volume"
        '
        'mnuChannel0
        '
        Me.mnuChannel0.Index = 0
        Me.mnuChannel0.Text = "1"
        '
        'mnuChannel1
        '
        Me.mnuChannel1.Index = 1
        Me.mnuChannel1.Text = "2"
        '
        'mnuChannel2
        '
        Me.mnuChannel2.Index = 2
        Me.mnuChannel2.Text = "3"
        '
        'mnuChannel3
        '
        Me.mnuChannel3.Index = 3
        Me.mnuChannel3.Text = "4"
        '
        'mnuChannel4
        '
        Me.mnuChannel4.Index = 4
        Me.mnuChannel4.Text = "5"
        '
        'mnuChannel5
        '
        Me.mnuChannel5.Index = 5
        Me.mnuChannel5.Text = "6"
        '
        'mnuChannel6
        '
        Me.mnuChannel6.Index = 6
        Me.mnuChannel6.Text = "7"
        '
        'mnuChannel7
        '
        Me.mnuChannel7.Index = 7
        Me.mnuChannel7.Text = "8"
        '
        'mnuChannel8
        '
        Me.mnuChannel8.Index = 8
        Me.mnuChannel8.Text = "9"
        '
        'mnuChannel9
        '
        Me.mnuChannel9.Index = 9
        Me.mnuChannel9.Text = "10"
        '
        'mnuChannel10
        '
        Me.mnuChannel10.Index = 10
        Me.mnuChannel10.Text = "11"
        '
        'mnuChannel11
        '
        Me.mnuChannel11.Index = 11
        Me.mnuChannel11.Text = "12"
        '
        'mnuChannel12
        '
        Me.mnuChannel12.Index = 12
        Me.mnuChannel12.Text = "13"
        '
        'mnuChannel13
        '
        Me.mnuChannel13.Index = 13
        Me.mnuChannel13.Text = "14"
        '
        'mnuChannel14
        '
        Me.mnuChannel14.Index = 14
        Me.mnuChannel14.Text = "15"
        '
        'mnuChannel15
        '
        Me.mnuChannel15.Index = 15
        Me.mnuChannel15.Text = "16"
        '
        'mnuDevice0
        '
        Me.mnuDevice0.Index = 0
        Me.mnuDevice0.Text = ""
        '
        'mnuDevice1
        '
        Me.mnuDevice1.Enabled = False
        Me.mnuDevice1.Index = 1
        Me.mnuDevice1.Text = ""
        Me.mnuDevice1.Visible = False
        '
        'mnuDevice2
        '
        Me.mnuDevice2.Enabled = False
        Me.mnuDevice2.Index = 2
        Me.mnuDevice2.Text = ""
        Me.mnuDevice2.Visible = False
        '
        'mnuDevice3
        '
        Me.mnuDevice3.Enabled = False
        Me.mnuDevice3.Index = 3
        Me.mnuDevice3.Text = ""
        Me.mnuDevice3.Visible = False
        '
        'mnuDevice4
        '
        Me.mnuDevice4.Enabled = False
        Me.mnuDevice4.Index = 4
        Me.mnuDevice4.Text = ""
        Me.mnuDevice4.Visible = False
        '
        'mnuDevice5
        '
        Me.mnuDevice5.Enabled = False
        Me.mnuDevice5.Index = 5
        Me.mnuDevice5.Text = ""
        Me.mnuDevice5.Visible = False
        '
        'mnuDevice6
        '
        Me.mnuDevice6.Enabled = False
        Me.mnuDevice6.Index = 6
        Me.mnuDevice6.Text = ""
        Me.mnuDevice6.Visible = False
        '
        'mnuDevice7
        '
        Me.mnuDevice7.Enabled = False
        Me.mnuDevice7.Index = 7
        Me.mnuDevice7.Text = ""
        Me.mnuDevice7.Visible = False
        '
        'mnuDevice8
        '
        Me.mnuDevice8.Enabled = False
        Me.mnuDevice8.Index = 8
        Me.mnuDevice8.Text = ""
        Me.mnuDevice8.Visible = False
        '
        'mnuDevice9
        '
        Me.mnuDevice9.Enabled = False
        Me.mnuDevice9.Index = 9
        Me.mnuDevice9.Text = ""
        Me.mnuDevice9.Visible = False
        '
        'mnuDevice10
        '
        Me.mnuDevice10.Enabled = False
        Me.mnuDevice10.Index = 10
        Me.mnuDevice10.Text = ""
        Me.mnuDevice10.Visible = False
        '
        'mnuMain
        '
        Me.mnuMain.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuFile, Me.mnuDevice, Me.mnuChannel, Me.mnuBaseNote, Me.mnuDrumfile})
        '
        'mnuFile
        '
        Me.mnuFile.Index = 0
        Me.mnuFile.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuOpen})
        Me.mnuFile.Text = "Midi &File"
        '
        'mnuOpen
        '
        Me.mnuOpen.Index = 0
        Me.mnuOpen.Text = "&Open"
        '
        'mnuDevice
        '
        Me.mnuDevice.Index = 1
        Me.mnuDevice.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuDevice0, Me.mnuDevice1, Me.mnuDevice2, Me.mnuDevice3, Me.mnuDevice4, Me.mnuDevice5, Me.mnuDevice6, Me.mnuDevice7, Me.mnuDevice8, Me.mnuDevice9, Me.mnuDevice10})
        Me.mnuDevice.Text = "Midi &Device"
        '
        'mnuChannel
        '
        Me.mnuChannel.Index = 2
        Me.mnuChannel.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuChannel0, Me.mnuChannel1, Me.mnuChannel2, Me.mnuChannel3, Me.mnuChannel4, Me.mnuChannel5, Me.mnuChannel6, Me.mnuChannel7, Me.mnuChannel8, Me.mnuChannel9, Me.mnuChannel10, Me.mnuChannel11, Me.mnuChannel12, Me.mnuChannel13, Me.mnuChannel14, Me.mnuChannel15})
        Me.mnuChannel.Text = "&Channel"
        '
        'mnuBaseNote
        '
        Me.mnuBaseNote.Index = 3
        Me.mnuBaseNote.Text = "&Base Note"
        '
        'mnuDrumfile
        '
        Me.mnuDrumfile.Index = 4
        Me.mnuDrumfile.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuDrumOpen})
        Me.mnuDrumfile.Text = "Dr&um File"
        '
        'mnuDrumOpen
        '
        Me.mnuDrumOpen.Index = 0
        Me.mnuDrumOpen.Text = "&Open"
        '
        'lblSeqFast
        '
        Me.lblSeqFast.Location = New System.Drawing.Point(712, 45)
        Me.lblSeqFast.Name = "lblSeqFast"
        Me.lblSeqFast.Size = New System.Drawing.Size(34, 23)
        Me.lblSeqFast.TabIndex = 27
        Me.lblSeqFast.Text = "Fast"
        Me.lblSeqFast.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblSeqNormal
        '
        Me.lblSeqNormal.Location = New System.Drawing.Point(622, 45)
        Me.lblSeqNormal.Name = "lblSeqNormal"
        Me.lblSeqNormal.Size = New System.Drawing.Size(50, 32)
        Me.lblSeqNormal.TabIndex = 26
        Me.lblSeqNormal.Text = "Normal Speed"
        Me.lblSeqNormal.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblSeqSlow
        '
        Me.lblSeqSlow.Location = New System.Drawing.Point(545, 45)
        Me.lblSeqSlow.Name = "lblSeqSlow"
        Me.lblSeqSlow.Size = New System.Drawing.Size(34, 23)
        Me.lblSeqSlow.TabIndex = 25
        Me.lblSeqSlow.Text = "Slow"
        '
        'tbSpeed
        '
        Me.tbSpeed.Location = New System.Drawing.Point(548, 12)
        Me.tbSpeed.Maximum = 9
        Me.tbSpeed.Minimum = -9
        Me.tbSpeed.Name = "tbSpeed"
        Me.tbSpeed.Size = New System.Drawing.Size(198, 45)
        Me.tbSpeed.TabIndex = 24
        '
        'btnRemoveSilence
        '
        Me.btnRemoveSilence.Location = New System.Drawing.Point(315, 12)
        Me.btnRemoveSilence.Name = "btnRemoveSilence"
        Me.btnRemoveSilence.Size = New System.Drawing.Size(95, 25)
        Me.btnRemoveSilence.TabIndex = 23
        Me.btnRemoveSilence.Text = "Remove Silence"
        '
        'btnPlay
        '
        Me.btnPlay.Location = New System.Drawing.Point(214, 12)
        Me.btnPlay.Name = "btnPlay"
        Me.btnPlay.Size = New System.Drawing.Size(95, 25)
        Me.btnPlay.TabIndex = 22
        Me.btnPlay.Text = "Play"
        '
        'btnStop
        '
        Me.btnStop.Enabled = False
        Me.btnStop.Location = New System.Drawing.Point(113, 12)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(95, 25)
        Me.btnStop.TabIndex = 21
        Me.btnStop.Text = "Stop"
        '
        'btnRecord
        '
        Me.btnRecord.Location = New System.Drawing.Point(12, 12)
        Me.btnRecord.Name = "btnRecord"
        Me.btnRecord.Size = New System.Drawing.Size(95, 25)
        Me.btnRecord.TabIndex = 20
        Me.btnRecord.Text = "Record"
        '
        'gbxInstrument
        '
        Me.gbxInstrument.Controls.Add(Me.btnReset)
        Me.gbxInstrument.Controls.Add(Me.Label5)
        Me.gbxInstrument.Controls.Add(Me.Label4)
        Me.gbxInstrument.Controls.Add(Me.tbpanning)
        Me.gbxInstrument.Controls.Add(Me.Label3)
        Me.gbxInstrument.Controls.Add(Me.lbChannel)
        Me.gbxInstrument.Controls.Add(Me.tbChannel)
        Me.gbxInstrument.Controls.Add(Me.Label2)
        Me.gbxInstrument.Controls.Add(Me.lbBaseNote)
        Me.gbxInstrument.Controls.Add(Me.tbBaseNote)
        Me.gbxInstrument.Controls.Add(Me.Label1)
        Me.gbxInstrument.Controls.Add(Me.cbChordMode)
        Me.gbxInstrument.Controls.Add(Me.ckChordMode)
        Me.gbxInstrument.Controls.Add(Me.tbBankMSB)
        Me.gbxInstrument.Controls.Add(Me.tbInstrument)
        Me.gbxInstrument.Controls.Add(Me.lblBankMSB)
        Me.gbxInstrument.Controls.Add(Me.lblInstrument)
        Me.gbxInstrument.Location = New System.Drawing.Point(407, 12)
        Me.gbxInstrument.Name = "gbxInstrument"
        Me.gbxInstrument.Size = New System.Drawing.Size(373, 254)
        Me.gbxInstrument.TabIndex = 22
        Me.gbxInstrument.TabStop = False
        Me.gbxInstrument.Text = "MIDI Instrument"
        '
        'btnReset
        '
        Me.btnReset.Location = New System.Drawing.Point(262, 222)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(102, 21)
        Me.btnReset.TabIndex = 16
        Me.btnReset.Text = "Reset"
        Me.btnReset.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(335, 199)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(32, 13)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "Right"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(72, 199)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(25, 13)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "Left"
        '
        'tbpanning
        '
        Me.tbpanning.LargeChange = 2
        Me.tbpanning.Location = New System.Drawing.Point(75, 167)
        Me.tbpanning.Maximum = 127
        Me.tbpanning.Name = "tbpanning"
        Me.tbpanning.Size = New System.Drawing.Size(290, 45)
        Me.tbpanning.TabIndex = 13
        Me.tbpanning.TickFrequency = 64
        Me.tbpanning.Value = 64
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(10, 174)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 13)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "Panning"
        '
        'lbChannel
        '
        Me.lbChannel.AutoSize = True
        Me.lbChannel.Location = New System.Drawing.Point(352, 136)
        Me.lbChannel.Name = "lbChannel"
        Me.lbChannel.Size = New System.Drawing.Size(13, 13)
        Me.lbChannel.TabIndex = 11
        Me.lbChannel.Text = "1"
        '
        'tbChannel
        '
        Me.tbChannel.LargeChange = 2
        Me.tbChannel.Location = New System.Drawing.Point(75, 129)
        Me.tbChannel.Maximum = 15
        Me.tbChannel.Name = "tbChannel"
        Me.tbChannel.Size = New System.Drawing.Size(272, 45)
        Me.tbChannel.TabIndex = 10
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(10, 136)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Channel:"
        '
        'lbBaseNote
        '
        Me.lbBaseNote.AutoSize = True
        Me.lbBaseNote.Location = New System.Drawing.Point(346, 99)
        Me.lbBaseNote.Name = "lbBaseNote"
        Me.lbBaseNote.Size = New System.Drawing.Size(19, 13)
        Me.lbBaseNote.TabIndex = 8
        Me.lbBaseNote.Text = "60"
        '
        'tbBaseNote
        '
        Me.tbBaseNote.LargeChange = 2
        Me.tbBaseNote.Location = New System.Drawing.Point(75, 92)
        Me.tbBaseNote.Maximum = 111
        Me.tbBaseNote.Name = "tbBaseNote"
        Me.tbBaseNote.Size = New System.Drawing.Size(272, 45)
        Me.tbBaseNote.TabIndex = 7
        Me.tbBaseNote.Value = 60
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 99)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Base Note:"
        '
        'cbChordMode
        '
        Me.cbChordMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbChordMode.Enabled = False
        Me.cbChordMode.FormattingEnabled = True
        Me.cbChordMode.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.cbChordMode.Items.AddRange(New Object() {"Major Chord", "Minor Chord", "Major 7th Chord", "Minor 7th Chord", "Dominant 7th Chord", "Augmented 7th Chord"})
        Me.cbChordMode.Location = New System.Drawing.Point(109, 222)
        Me.cbChordMode.Name = "cbChordMode"
        Me.cbChordMode.Size = New System.Drawing.Size(129, 21)
        Me.cbChordMode.TabIndex = 5
        '
        'ckChordMode
        '
        Me.ckChordMode.AutoSize = True
        Me.ckChordMode.Location = New System.Drawing.Point(12, 225)
        Me.ckChordMode.Name = "ckChordMode"
        Me.ckChordMode.Size = New System.Drawing.Size(84, 17)
        Me.ckChordMode.TabIndex = 4
        Me.ckChordMode.Text = "Chord Mode"
        Me.ckChordMode.UseVisualStyleBackColor = True
        '
        'tbBankMSB
        '
        Me.tbBankMSB.LargeChange = 2
        Me.tbBankMSB.Location = New System.Drawing.Point(75, 57)
        Me.tbBankMSB.Maximum = 8
        Me.tbBankMSB.Name = "tbBankMSB"
        Me.tbBankMSB.Size = New System.Drawing.Size(290, 45)
        Me.tbBankMSB.TabIndex = 3
        '
        'tbInstrument
        '
        Me.tbInstrument.Location = New System.Drawing.Point(75, 19)
        Me.tbInstrument.Maximum = 127
        Me.tbInstrument.Name = "tbInstrument"
        Me.tbInstrument.Size = New System.Drawing.Size(290, 45)
        Me.tbInstrument.TabIndex = 2
        '
        'lblBankMSB
        '
        Me.lblBankMSB.AutoSize = True
        Me.lblBankMSB.Location = New System.Drawing.Point(10, 64)
        Me.lblBankMSB.Name = "lblBankMSB"
        Me.lblBankMSB.Size = New System.Drawing.Size(61, 13)
        Me.lblBankMSB.TabIndex = 1
        Me.lblBankMSB.Text = "Bank MSB:"
        '
        'lblInstrument
        '
        Me.lblInstrument.AutoSize = True
        Me.lblInstrument.Location = New System.Drawing.Point(10, 25)
        Me.lblInstrument.Name = "lblInstrument"
        Me.lblInstrument.Size = New System.Drawing.Size(59, 13)
        Me.lblInstrument.TabIndex = 0
        Me.lblInstrument.Text = "Instrument:"
        '
        'tmrSequencer
        '
        '
        'tclMidiFunction
        '
        Me.tclMidiFunction.Controls.Add(Me.tabSequencer)
        Me.tclMidiFunction.Controls.Add(Me.tabWhiteboard)
        Me.tclMidiFunction.Controls.Add(Me.tabDrumMachine)
        Me.tclMidiFunction.Location = New System.Drawing.Point(12, 271)
        Me.tclMidiFunction.Name = "tclMidiFunction"
        Me.tclMidiFunction.SelectedIndex = 0
        Me.tclMidiFunction.Size = New System.Drawing.Size(768, 342)
        Me.tclMidiFunction.TabIndex = 23
        '
        'tabSequencer
        '
        Me.tabSequencer.Controls.Add(Me.Label10)
        Me.tabSequencer.Controls.Add(Me.Label9)
        Me.tabSequencer.Controls.Add(Me.Label6)
        Me.tabSequencer.Controls.Add(Me.Label7)
        Me.tabSequencer.Controls.Add(Me.Label8)
        Me.tabSequencer.Controls.Add(Me.tbTranspose)
        Me.tabSequencer.Controls.Add(Me.ckLoopMode)
        Me.tabSequencer.Controls.Add(Me.btnAppend)
        Me.tabSequencer.Controls.Add(Me.lblSeqFast)
        Me.tabSequencer.Controls.Add(Me.btnRecord)
        Me.tabSequencer.Controls.Add(Me.lblSeqNormal)
        Me.tabSequencer.Controls.Add(Me.btnStop)
        Me.tabSequencer.Controls.Add(Me.lblSeqSlow)
        Me.tabSequencer.Controls.Add(Me.btnPlay)
        Me.tabSequencer.Controls.Add(Me.tbSpeed)
        Me.tabSequencer.Controls.Add(Me.btnRemoveSilence)
        Me.tabSequencer.Location = New System.Drawing.Point(4, 22)
        Me.tabSequencer.Name = "tabSequencer"
        Me.tabSequencer.Padding = New System.Windows.Forms.Padding(3)
        Me.tabSequencer.Size = New System.Drawing.Size(760, 316)
        Me.tabSequencer.TabIndex = 0
        Me.tabSequencer.Text = "MIDI Sequencer"
        Me.tabSequencer.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(482, 97)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(60, 13)
        Me.Label10.TabIndex = 35
        Me.Label10.Text = "Transpose:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(497, 24)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(41, 13)
        Me.Label9.TabIndex = 34
        Me.Label9.Text = "Speed:"
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(712, 113)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(34, 23)
        Me.Label6.TabIndex = 33
        Me.Label6.Text = "up"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(622, 113)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(50, 32)
        Me.Label7.TabIndex = 32
        Me.Label7.Text = "0"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(545, 113)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(34, 23)
        Me.Label8.TabIndex = 31
        Me.Label8.Text = "down"
        '
        'tbTranspose
        '
        Me.tbTranspose.Location = New System.Drawing.Point(548, 80)
        Me.tbTranspose.Maximum = 60
        Me.tbTranspose.Minimum = -60
        Me.tbTranspose.Name = "tbTranspose"
        Me.tbTranspose.Size = New System.Drawing.Size(198, 45)
        Me.tbTranspose.TabIndex = 30
        '
        'ckLoopMode
        '
        Me.ckLoopMode.AutoSize = True
        Me.ckLoopMode.Location = New System.Drawing.Point(114, 57)
        Me.ckLoopMode.Name = "ckLoopMode"
        Me.ckLoopMode.Size = New System.Drawing.Size(119, 17)
        Me.ckLoopMode.TabIndex = 29
        Me.ckLoopMode.Text = "LoopMode(endless)"
        Me.ckLoopMode.UseVisualStyleBackColor = True
        '
        'btnAppend
        '
        Me.btnAppend.Location = New System.Drawing.Point(12, 52)
        Me.btnAppend.Name = "btnAppend"
        Me.btnAppend.Size = New System.Drawing.Size(95, 25)
        Me.btnAppend.TabIndex = 28
        Me.btnAppend.Text = "Append"
        '
        'tabWhiteboard
        '
        Me.tabWhiteboard.Controls.Add(Me.gbxXAxis)
        Me.tabWhiteboard.Controls.Add(Me.picWhiteboard)
        Me.tabWhiteboard.Controls.Add(Me.gbxYAxis)
        Me.tabWhiteboard.Location = New System.Drawing.Point(4, 22)
        Me.tabWhiteboard.Name = "tabWhiteboard"
        Me.tabWhiteboard.Size = New System.Drawing.Size(760, 316)
        Me.tabWhiteboard.TabIndex = 2
        Me.tabWhiteboard.Text = "MIDI Whiteboard"
        Me.tabWhiteboard.UseVisualStyleBackColor = True
        '
        'gbxXAxis
        '
        Me.gbxXAxis.Controls.Add(Me.lblXValue)
        Me.gbxXAxis.Controls.Add(Me.cboXTitle)
        Me.gbxXAxis.Controls.Add(Me.lblXTitle)
        Me.gbxXAxis.Controls.Add(Me.lblXCaption)
        Me.gbxXAxis.Location = New System.Drawing.Point(324, 12)
        Me.gbxXAxis.Name = "gbxXAxis"
        Me.gbxXAxis.Size = New System.Drawing.Size(145, 80)
        Me.gbxXAxis.TabIndex = 40
        Me.gbxXAxis.TabStop = False
        Me.gbxXAxis.Text = "X - axis"
        '
        'lblXValue
        '
        Me.lblXValue.Location = New System.Drawing.Point(48, 24)
        Me.lblXValue.Name = "lblXValue"
        Me.lblXValue.Size = New System.Drawing.Size(48, 16)
        Me.lblXValue.TabIndex = 38
        '
        'cboXTitle
        '
        Me.cboXTitle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboXTitle.Items.AddRange(New Object() {"(none)", "instrument", "velocity", "pitch", "stereo position", "pitch bend"})
        Me.cboXTitle.Location = New System.Drawing.Point(40, 48)
        Me.cboXTitle.Name = "cboXTitle"
        Me.cboXTitle.Size = New System.Drawing.Size(96, 21)
        Me.cboXTitle.TabIndex = 37
        '
        'lblXTitle
        '
        Me.lblXTitle.Location = New System.Drawing.Point(8, 51)
        Me.lblXTitle.Name = "lblXTitle"
        Me.lblXTitle.Size = New System.Drawing.Size(32, 16)
        Me.lblXTitle.TabIndex = 36
        Me.lblXTitle.Text = "Title:"
        '
        'lblXCaption
        '
        Me.lblXCaption.Location = New System.Drawing.Point(8, 24)
        Me.lblXCaption.Name = "lblXCaption"
        Me.lblXCaption.Size = New System.Drawing.Size(40, 16)
        Me.lblXCaption.TabIndex = 0
        Me.lblXCaption.Text = "Value:"
        '
        'picWhiteboard
        '
        Me.picWhiteboard.BackColor = System.Drawing.Color.White
        Me.picWhiteboard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picWhiteboard.Location = New System.Drawing.Point(12, 12)
        Me.picWhiteboard.Name = "picWhiteboard"
        Me.picWhiteboard.Size = New System.Drawing.Size(300, 300)
        Me.picWhiteboard.TabIndex = 39
        Me.picWhiteboard.TabStop = False
        '
        'gbxYAxis
        '
        Me.gbxYAxis.Controls.Add(Me.cboYTitle)
        Me.gbxYAxis.Controls.Add(Me.lblYTitle)
        Me.gbxYAxis.Controls.Add(Me.lblYCaption)
        Me.gbxYAxis.Controls.Add(Me.lblYValue)
        Me.gbxYAxis.Location = New System.Drawing.Point(324, 104)
        Me.gbxYAxis.Name = "gbxYAxis"
        Me.gbxYAxis.Size = New System.Drawing.Size(145, 80)
        Me.gbxYAxis.TabIndex = 41
        Me.gbxYAxis.TabStop = False
        Me.gbxYAxis.Text = "Y - axis"
        '
        'cboYTitle
        '
        Me.cboYTitle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboYTitle.Items.AddRange(New Object() {"(none)", "instrument", "velocity", "pitch", "stereo position", "pitch bend"})
        Me.cboYTitle.Location = New System.Drawing.Point(40, 48)
        Me.cboYTitle.Name = "cboYTitle"
        Me.cboYTitle.Size = New System.Drawing.Size(96, 21)
        Me.cboYTitle.TabIndex = 37
        '
        'lblYTitle
        '
        Me.lblYTitle.Location = New System.Drawing.Point(8, 51)
        Me.lblYTitle.Name = "lblYTitle"
        Me.lblYTitle.Size = New System.Drawing.Size(32, 16)
        Me.lblYTitle.TabIndex = 36
        Me.lblYTitle.Text = "Title:"
        '
        'lblYCaption
        '
        Me.lblYCaption.Location = New System.Drawing.Point(8, 24)
        Me.lblYCaption.Name = "lblYCaption"
        Me.lblYCaption.Size = New System.Drawing.Size(40, 16)
        Me.lblYCaption.TabIndex = 0
        Me.lblYCaption.Text = "Value:"
        '
        'lblYValue
        '
        Me.lblYValue.Location = New System.Drawing.Point(48, 24)
        Me.lblYValue.Name = "lblYValue"
        Me.lblYValue.Size = New System.Drawing.Size(48, 16)
        Me.lblYValue.TabIndex = 39
        '
        'tabDrumMachine
        '
        Me.tabDrumMachine.Controls.Add(Me.btnDrumReset)
        Me.tabDrumMachine.Controls.Add(Me.Label20)
        Me.tabDrumMachine.Controls.Add(Me.Label19)
        Me.tabDrumMachine.Controls.Add(Me.Label18)
        Me.tabDrumMachine.Controls.Add(Me.Label14)
        Me.tabDrumMachine.Controls.Add(Me.Label15)
        Me.tabDrumMachine.Controls.Add(Me.Label16)
        Me.tabDrumMachine.Controls.Add(Me.Label17)
        Me.tabDrumMachine.Controls.Add(Me.tbDrumSpeed)
        Me.tabDrumMachine.Controls.Add(Me.cbDrumInstrument)
        Me.tabDrumMachine.Controls.Add(Me.Label13)
        Me.tabDrumMachine.Controls.Add(Me.cbDrumNumber)
        Me.tabDrumMachine.Controls.Add(Me.btnReversion)
        Me.tabDrumMachine.Controls.Add(Me.btnInversion)
        Me.tabDrumMachine.Controls.Add(Me.btnRandom)
        Me.tabDrumMachine.Controls.Add(Me.btnRandomIns)
        Me.tabDrumMachine.Controls.Add(Me.Label12)
        Me.tabDrumMachine.Controls.Add(Me.Label11)
        Me.tabDrumMachine.Controls.Add(Me.nColumns)
        Me.tabDrumMachine.Controls.Add(Me.nRows)
        Me.tabDrumMachine.Controls.Add(Me.btnDrumStop)
        Me.tabDrumMachine.Controls.Add(Me.btnDrumStart)
        Me.tabDrumMachine.Controls.Add(Me.picDrum)
        Me.tabDrumMachine.Location = New System.Drawing.Point(4, 22)
        Me.tabDrumMachine.Name = "tabDrumMachine"
        Me.tabDrumMachine.Padding = New System.Windows.Forms.Padding(3)
        Me.tabDrumMachine.Size = New System.Drawing.Size(760, 316)
        Me.tabDrumMachine.TabIndex = 1
        Me.tabDrumMachine.Text = "Drum Machine"
        Me.tabDrumMachine.UseVisualStyleBackColor = True
        '
        'btnDrumReset
        '
        Me.btnDrumReset.Location = New System.Drawing.Point(210, 249)
        Me.btnDrumReset.Name = "btnDrumReset"
        Me.btnDrumReset.Size = New System.Drawing.Size(95, 25)
        Me.btnDrumReset.TabIndex = 48
        Me.btnDrumReset.Text = "Reset"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(599, 300)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(105, 13)
        Me.Label20.TabIndex = 47
        Me.Label20.Text = "(enabled during play)"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(662, 93)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(48, 13)
        Me.Label19.TabIndex = 46
        Me.Label19.Text = "(Max 12)"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(662, 54)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(48, 13)
        Me.Label18.TabIndex = 45
        Me.Label18.Text = "(Max 10)"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(500, 252)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(41, 13)
        Me.Label14.TabIndex = 44
        Me.Label14.Text = "Speed:"
        '
        'Label15
        '
        Me.Label15.Location = New System.Drawing.Point(715, 273)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(34, 23)
        Me.Label15.TabIndex = 43
        Me.Label15.Text = "Fast"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.Location = New System.Drawing.Point(625, 273)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(50, 32)
        Me.Label16.TabIndex = 42
        Me.Label16.Text = "Normal Speed"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label17
        '
        Me.Label17.Location = New System.Drawing.Point(548, 273)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(34, 23)
        Me.Label17.TabIndex = 41
        Me.Label17.Text = "Slow"
        '
        'tbDrumSpeed
        '
        Me.tbDrumSpeed.Location = New System.Drawing.Point(551, 240)
        Me.tbDrumSpeed.Maximum = 9
        Me.tbDrumSpeed.Minimum = -9
        Me.tbDrumSpeed.Name = "tbDrumSpeed"
        Me.tbDrumSpeed.Size = New System.Drawing.Size(198, 45)
        Me.tbDrumSpeed.TabIndex = 40
        '
        'cbDrumInstrument
        '
        Me.cbDrumInstrument.FormattingEnabled = True
        Me.cbDrumInstrument.Items.AddRange(New Object() {"35 Acoustic Bass Drum", "36 Bass Drum 1", "37 Side Stick", "38 Acoustic Snare", "39 Hand Clap", "40 Electric Snare ", "41 Low Floor Tom", "42 Closed Hi-Hat", "43 High Floor Tom", "44 Pedal Hi-Hat  ", "45 Low Tom ", "46 Open Hi-Hat ", "47 Low-Mid Tom ", "48 Hi-Mid Tom ", "49 Crash Cymbal 1 ", "50 High Tom ", "51 Ride Cymbal 1 ", "52 Chinese Cymbal ", "53 Ride Bell ", "54 Tambourine ", "55 Splash Cymbal ", "56 Cowbell ", "57 Crash Cymbal 2 ", "58 Vibraslap ", "59 Ride Cymbal 2 ", "60 Hi Bongo", "61 Low Bongo", "62 Mute Hi Conga", "63 Open Hi Conga ", "64 Low Conga ", "65 High Timbale ", "66 Low Timbale  ", "67 High Agogo ", "68 Low Agogo ", "69 Cabasa ", "70 Maracas ", "71 Short Whistle ", "72 Long Whistle ", "73 Short Guiro ", "74 Long Guiro ", "75 Claves ", "76 Hi Wood Block ", "77 Low Wood Block ", "78 Mute Cuica ", "79 Open Cuica ", "80 Mute Triangle ", "81 Open Triangle "})
        Me.cbDrumInstrument.Location = New System.Drawing.Point(599, 213)
        Me.cbDrumInstrument.Name = "cbDrumInstrument"
        Me.cbDrumInstrument.Size = New System.Drawing.Size(121, 21)
        Me.cbDrumInstrument.TabIndex = 39
        Me.cbDrumInstrument.Text = "60 Hi Bongo"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(548, 175)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(156, 13)
        Me.Label13.TabIndex = 37
        Me.Label13.Text = "Set instrument for drum number:"
        '
        'cbDrumNumber
        '
        Me.cbDrumNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbDrumNumber.FormattingEnabled = True
        Me.cbDrumNumber.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10"})
        Me.cbDrumNumber.Location = New System.Drawing.Point(711, 172)
        Me.cbDrumNumber.Name = "cbDrumNumber"
        Me.cbDrumNumber.Size = New System.Drawing.Size(44, 21)
        Me.cbDrumNumber.TabIndex = 36
        '
        'btnReversion
        '
        Me.btnReversion.Location = New System.Drawing.Point(210, 280)
        Me.btnReversion.Name = "btnReversion"
        Me.btnReversion.Size = New System.Drawing.Size(95, 25)
        Me.btnReversion.TabIndex = 35
        Me.btnReversion.Text = "Reversion"
        '
        'btnInversion
        '
        Me.btnInversion.Location = New System.Drawing.Point(109, 280)
        Me.btnInversion.Name = "btnInversion"
        Me.btnInversion.Size = New System.Drawing.Size(95, 25)
        Me.btnInversion.TabIndex = 34
        Me.btnInversion.Text = "Inversion"
        '
        'btnRandom
        '
        Me.btnRandom.Location = New System.Drawing.Point(8, 280)
        Me.btnRandom.Name = "btnRandom"
        Me.btnRandom.Size = New System.Drawing.Size(95, 25)
        Me.btnRandom.TabIndex = 33
        Me.btnRandom.Text = "Random"
        '
        'btnRandomIns
        '
        Me.btnRandomIns.Location = New System.Drawing.Point(609, 122)
        Me.btnRandomIns.Name = "btnRandomIns"
        Me.btnRandomIns.Size = New System.Drawing.Size(95, 45)
        Me.btnRandomIns.TabIndex = 32
        Me.btnRandomIns.Text = "Random Instruments"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(569, 72)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(59, 13)
        Me.Label12.TabIndex = 31
        Me.Label12.Text = "Drum slots:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(536, 34)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(92, 13)
        Me.Label11.TabIndex = 30
        Me.Label11.Text = "Drum Instruments:"
        '
        'nColumns
        '
        Me.nColumns.Location = New System.Drawing.Point(634, 70)
        Me.nColumns.Maximum = New Decimal(New Integer() {12, 0, 0, 0})
        Me.nColumns.Minimum = New Decimal(New Integer() {2, 0, 0, 0})
        Me.nColumns.Name = "nColumns"
        Me.nColumns.Size = New System.Drawing.Size(120, 20)
        Me.nColumns.TabIndex = 29
        Me.nColumns.Value = New Decimal(New Integer() {8, 0, 0, 0})
        '
        'nRows
        '
        Me.nRows.Location = New System.Drawing.Point(634, 32)
        Me.nRows.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.nRows.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nRows.Name = "nRows"
        Me.nRows.Size = New System.Drawing.Size(120, 20)
        Me.nRows.TabIndex = 28
        Me.nRows.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'btnDrumStop
        '
        Me.btnDrumStop.Location = New System.Drawing.Point(109, 249)
        Me.btnDrumStop.Name = "btnDrumStop"
        Me.btnDrumStop.Size = New System.Drawing.Size(95, 25)
        Me.btnDrumStop.TabIndex = 27
        Me.btnDrumStop.Text = "Stop Drum"
        '
        'btnDrumStart
        '
        Me.btnDrumStart.Location = New System.Drawing.Point(8, 249)
        Me.btnDrumStart.Name = "btnDrumStart"
        Me.btnDrumStart.Size = New System.Drawing.Size(95, 25)
        Me.btnDrumStart.TabIndex = 26
        Me.btnDrumStart.Text = "Start Drum"
        '
        'picDrum
        '
        Me.picDrum.BackColor = System.Drawing.SystemColors.Window
        Me.picDrum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picDrum.ForeColor = System.Drawing.SystemColors.WindowText
        Me.picDrum.Location = New System.Drawing.Point(12, 12)
        Me.picDrum.Name = "picDrum"
        Me.picDrum.Size = New System.Drawing.Size(300, 40)
        Me.picDrum.TabIndex = 25
        Me.picDrum.TabStop = False
        '
        'tmrDrumPlayback
        '
        Me.tmrDrumPlayback.Interval = 250
        '
        'frmMidiPiano
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(791, 625)
        Me.Controls.Add(Me.tclMidiFunction)
        Me.Controls.Add(Me.gbxInstrument)
        Me.Controls.Add(Me.vsbVolume)
        Me.Controls.Add(Me._key_15)
        Me.Controls.Add(Me._key_13)
        Me.Controls.Add(Me._key_10)
        Me.Controls.Add(Me._key_8)
        Me.Controls.Add(Me._key_6)
        Me.Controls.Add(Me._key_3)
        Me.Controls.Add(Me._key_1)
        Me.Controls.Add(Me._key_16)
        Me.Controls.Add(Me._key_14)
        Me.Controls.Add(Me._key_12)
        Me.Controls.Add(Me._key_11)
        Me.Controls.Add(Me._key_9)
        Me.Controls.Add(Me._key_7)
        Me.Controls.Add(Me._key_5)
        Me.Controls.Add(Me._key_4)
        Me.Controls.Add(Me._key_2)
        Me.Controls.Add(Me._key_0)
        Me.Controls.Add(Me.lblVolume)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(11, 49)
        Me.Menu = Me.mnuMain
        Me.Name = "frmMidiPiano"
        Me.Text = "VB Midi Piano"
        CType(Me.tbSpeed, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbxInstrument.ResumeLayout(False)
        Me.gbxInstrument.PerformLayout()
        CType(Me.tbpanning, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbChannel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbBaseNote, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbBankMSB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbInstrument, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tclMidiFunction.ResumeLayout(False)
        Me.tabSequencer.ResumeLayout(False)
        Me.tabSequencer.PerformLayout()
        CType(Me.tbTranspose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabWhiteboard.ResumeLayout(False)
        Me.gbxXAxis.ResumeLayout(False)
        CType(Me.picWhiteboard, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbxYAxis.ResumeLayout(False)
        Me.tabDrumMachine.ResumeLayout(False)
        Me.tabDrumMachine.PerformLayout()
        CType(Me.tbDrumSpeed, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nColumns, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nRows, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picDrum, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region

    Const INVALID_NOTE As Short = -1 ' Code for keyboard keys that we don't handle

    Dim numDevices As Integer ' number of midi output devices
    Dim curDevice As Integer ' current midi device
    Dim hmidi As Integer ' midi output handle
    Dim rc As Integer ' return code
    Dim midimsg As Integer ' midi output message buffer
    Dim channel As Short ' midi output channel
    Dim volume As Short ' midi volume
    Dim baseNote As Short ' the first note on our "piano"
    Dim playChord As Boolean
    Dim chordMode As Short
    Dim speed As Double = 1
    Dim drumspeed As Double = 1

    Dim instrument(0 To 15) As Short
    Dim panning(0 To 15) As Short

    Dim key As CheckBoxArray ' an array of check box for keys
    Dim chan As MenuItemArray ' an array of menu item for channel
    Dim device As MenuItemArray ' an array of menu item for midi device

    ' for recording
    Dim isRecording As Boolean ' recording status
    Dim startTime As System.DateTime ' the time of starting recording
    Dim endTime As System.DateTime
    Dim midiSequence As SequenceData ' store MIDI sequence
    Dim currentIndex As Integer ' store the current playing sequence index

    ' for MIDI whiteboard
    Dim lastMidiMessage As Integer = -1 ' previous MIDI message sent to the card
    Dim cbXselected As Short = 0 ' selection of the X axis
    Dim cbYselected As Short = 3 ' selection of the Y axis

    ' for drum machine
    Dim drum_instrument As Short = 2 ' Predefined number of drum instruments
    Dim drum_slot As Short = 8 ' Predefined number of slots across the drum machine
    Dim drumSlot(10, 16) As Boolean ' Slot On/Off for the drum machine
    Dim drumNumber(10) As Short ' The instrument for the drums
    Dim drumMessageSent(10) As Boolean ' True if a note-on midi message for a drum is sent

#Region "function initControlArray()"


    Public Sub initControlArray()
        ' initialize key checkboxarray (not generated by vb)
        key = New CheckBoxArray

        AddHandler _key_0.MouseDown, AddressOf key_MouseDown
        AddHandler _key_0.MouseUp, AddressOf key_MouseUp
        key.AddNewCheckBox(_key_0)

        AddHandler _key_1.MouseDown, AddressOf key_MouseDown
        AddHandler _key_1.MouseUp, AddressOf key_MouseUp
        key.AddNewCheckBox(_key_1)

        AddHandler _key_2.MouseDown, AddressOf key_MouseDown
        AddHandler _key_2.MouseUp, AddressOf key_MouseUp
        key.AddNewCheckBox(_key_2)

        AddHandler _key_3.MouseDown, AddressOf key_MouseDown
        AddHandler _key_3.MouseUp, AddressOf key_MouseUp
        key.AddNewCheckBox(_key_3)

        AddHandler _key_4.MouseDown, AddressOf key_MouseDown
        AddHandler _key_4.MouseUp, AddressOf key_MouseUp
        key.AddNewCheckBox(_key_4)

        AddHandler _key_5.MouseDown, AddressOf key_MouseDown
        AddHandler _key_5.MouseUp, AddressOf key_MouseUp
        key.AddNewCheckBox(_key_5)

        AddHandler _key_6.MouseDown, AddressOf key_MouseDown
        AddHandler _key_6.MouseUp, AddressOf key_MouseUp
        key.AddNewCheckBox(_key_6)

        AddHandler _key_7.MouseDown, AddressOf key_MouseDown
        AddHandler _key_7.MouseUp, AddressOf key_MouseUp
        key.AddNewCheckBox(_key_7)

        AddHandler _key_8.MouseDown, AddressOf key_MouseDown
        AddHandler _key_8.MouseUp, AddressOf key_MouseUp
        key.AddNewCheckBox(_key_8)

        AddHandler _key_9.MouseDown, AddressOf key_MouseDown
        AddHandler _key_9.MouseUp, AddressOf key_MouseUp
        key.AddNewCheckBox(_key_9)

        AddHandler _key_10.MouseDown, AddressOf key_MouseDown
        AddHandler _key_10.MouseUp, AddressOf key_MouseUp
        key.AddNewCheckBox(_key_10)

        AddHandler _key_11.MouseDown, AddressOf key_MouseDown
        AddHandler _key_11.MouseUp, AddressOf key_MouseUp
        key.AddNewCheckBox(_key_11)

        AddHandler _key_12.MouseDown, AddressOf key_MouseDown
        AddHandler _key_12.MouseUp, AddressOf key_MouseUp
        key.AddNewCheckBox(_key_12)

        AddHandler _key_13.MouseDown, AddressOf key_MouseDown
        AddHandler _key_13.MouseUp, AddressOf key_MouseUp
        key.AddNewCheckBox(_key_13)

        AddHandler _key_14.MouseDown, AddressOf key_MouseDown
        AddHandler _key_14.MouseUp, AddressOf key_MouseUp
        key.AddNewCheckBox(_key_14)

        AddHandler _key_15.MouseDown, AddressOf key_MouseDown
        AddHandler _key_15.MouseUp, AddressOf key_MouseUp
        key.AddNewCheckBox(_key_15)

        AddHandler _key_16.MouseDown, AddressOf key_MouseDown
        AddHandler _key_16.MouseUp, AddressOf key_MouseUp
        key.AddNewCheckBox(_key_16)

        ' initialize channel menuitemarray (not generated by vb)
        chan = New MenuItemArray
        AddHandler mnuChannel0.Click, AddressOf chan_Click
        chan.AddNewMenuItem(mnuChannel0)

        AddHandler mnuChannel1.Click, AddressOf chan_Click
        chan.AddNewMenuItem(mnuChannel1)

        AddHandler mnuChannel2.Click, AddressOf chan_Click
        chan.AddNewMenuItem(mnuChannel2)

        AddHandler mnuChannel3.Click, AddressOf chan_Click
        chan.AddNewMenuItem(mnuChannel3)

        AddHandler mnuChannel4.Click, AddressOf chan_Click
        chan.AddNewMenuItem(mnuChannel4)

        AddHandler mnuChannel5.Click, AddressOf chan_Click
        chan.AddNewMenuItem(mnuChannel5)

        AddHandler mnuChannel6.Click, AddressOf chan_Click
        chan.AddNewMenuItem(mnuChannel6)

        AddHandler mnuChannel7.Click, AddressOf chan_Click
        chan.AddNewMenuItem(mnuChannel7)

        AddHandler mnuChannel8.Click, AddressOf chan_Click
        chan.AddNewMenuItem(mnuChannel8)

        AddHandler mnuChannel9.Click, AddressOf chan_Click
        chan.AddNewMenuItem(mnuChannel9)

        AddHandler mnuChannel10.Click, AddressOf chan_Click
        chan.AddNewMenuItem(mnuChannel10)

        AddHandler mnuChannel11.Click, AddressOf chan_Click
        chan.AddNewMenuItem(mnuChannel11)

        AddHandler mnuChannel12.Click, AddressOf chan_Click
        chan.AddNewMenuItem(mnuChannel12)

        AddHandler mnuChannel13.Click, AddressOf chan_Click
        chan.AddNewMenuItem(mnuChannel13)

        AddHandler mnuChannel14.Click, AddressOf chan_Click
        chan.AddNewMenuItem(mnuChannel14)

        AddHandler mnuChannel15.Click, AddressOf chan_Click
        chan.AddNewMenuItem(mnuChannel15)

        ' initialize device menuitemarray (not generated by vb)
        device = New MenuItemArray

        AddHandler mnuDevice0.Click, AddressOf device_Click
        device.AddNewMenuItem(mnuDevice0)

        AddHandler mnuDevice1.Click, AddressOf device_Click
        device.AddNewMenuItem(mnuDevice1)

        AddHandler mnuDevice2.Click, AddressOf device_Click
        device.AddNewMenuItem(mnuDevice2)

        AddHandler mnuDevice3.Click, AddressOf device_Click
        device.AddNewMenuItem(mnuDevice3)

        AddHandler mnuDevice4.Click, AddressOf device_Click
        device.AddNewMenuItem(mnuDevice4)

        AddHandler mnuDevice5.Click, AddressOf device_Click
        device.AddNewMenuItem(mnuDevice5)

        AddHandler mnuDevice6.Click, AddressOf device_Click
        device.AddNewMenuItem(mnuDevice6)

        AddHandler mnuDevice7.Click, AddressOf device_Click
        device.AddNewMenuItem(mnuDevice7)

        AddHandler mnuDevice8.Click, AddressOf device_Click
        device.AddNewMenuItem(mnuDevice8)

        AddHandler mnuDevice9.Click, AddressOf device_Click
        device.AddNewMenuItem(mnuDevice9)

        AddHandler mnuDevice10.Click, AddressOf device_Click
        device.AddNewMenuItem(mnuDevice10)
    End Sub
#End Region

    ' Set the value for the starting note of the piano
    Public Sub base_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBaseNote.Click
        Dim s As String
        Dim i As Short
        s = InputBox("Enter the new base note for the keyboard (0 - 111)", "Base note", CStr(baseNote))
        If IsNumeric(s) Then
            i = CShort(s)
            If i >= 0 And i < 112 Then
                baseNote = i
                lbBaseNote.Text = baseNote
                tbBaseNote.Value = baseNote
            End If
        End If
    End Sub

    ' Select the midi output channel
    Public Sub chan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim index As Integer
        index = CType(sender, System.Windows.Forms.MenuItem).Index
        chan(channel).Checked = False
        channel = index
        chan(channel).Checked = True
        tbInstrument.Value = instrument(channel)
        tbpanning.Value = panning(channel)
        tbChannel.Value = channel
        lbChannel.Text = channel + 1
    End Sub

    ' Open the midi device selected in the menu. The menu index equals the midi device number + 1.
    Public Sub device_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim index As Integer
        index = CType(sender, System.Windows.Forms.MenuItem).Index
        device(curDevice + 1).Checked = False
        device(index).Checked = True
        curDevice = index - 1
        rc = midiOutClose(hmidi)
        rc = midiOutOpen(hmidi, curDevice, 0, 0, 0)
        If rc <> 0 Then
            MessageBox.Show("Couldn't open midi out, rc = " & rc, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    ' If user presses a keyboard key, start the corresponding midi note
    Private Sub frmMidiPiano_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = e.KeyCode

        StartNote(NoteFromKey(KeyCode))
    End Sub

    ' If user lifts a keyboard key, stop the corresponding midi note
    Private Sub frmMidiPiano_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = e.KeyCode

        StopNote(NoteFromKey(KeyCode))
    End Sub

    Private Sub frmMidiPiano_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim i As Integer
        Dim caps As MIDIOUTCAPS

        Try

            For i = 0 To 15
                panning(i) = 64
            Next

            initControlArray()

            ' Set the first device as midi mapper
            device(0).Text = "MIDI Mapper"
            device(0).Visible = True
            device(0).Enabled = True

            ' Get the rest of the midi devices
            numDevices = midiOutGetNumDevs()
            For i = 0 To (numDevices - 1)
                midiOutGetDevCaps(i, caps, Len(caps))
                device(i + 1).Text = caps.szPname
                device(i + 1).Visible = True
                device(i + 1).Enabled = True
            Next

            ' Select the MIDI Mapper as the default device
            device_Click(device.Item(0), New System.EventArgs)

            ' Set the default channel
            channel = 0
            chan(channel).Checked = True

            ' Set the base note
            baseNote = 60

            ' Set volume range
            volume = 127
            vsbVolume.Value = vsbVolume.Maximum - volume

            midiSequence = Nothing
            isRecording = False
            playChord = False
            chordMode = 0

            cboXTitle.SelectedIndex = cbXselected
            cboYTitle.SelectedIndex = cbYselected

            For i = 1 To 10
                drumNumber(i) = &H3C - 1 + i
            Next



        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmMidiPiano_Closed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        ' Close current midi device
        rc = midiOutClose(hmidi)
    End Sub

    ' Start a note when user click on it
    Public Sub key_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Dim index As Integer
        index = CType(sender, System.Windows.Forms.CheckBox).Tag
        StartNote(index)
    End Sub

    ' Stop the note when user lifts the mouse button
    Public Sub key_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Dim index As Integer
        index = CType(sender, System.Windows.Forms.CheckBox).Tag
        StopNote(index)
    End Sub

    ' Press the button and send midi start event
    Private Sub StartNote(ByRef Index As Short)
        If Index = INVALID_NOTE Then
            Exit Sub
        End If
        If key(Index).CheckState = 1 Then
            Exit Sub
        End If

        key(Index).CheckState = System.Windows.Forms.CheckState.Checked
        midimsg = &H90 + ((baseNote + Index) * &H100) + (volume * &H10000) + channel
        sendMidiMsg(hmidi, midimsg)

        If playChord = False Then
            Exit Sub
        End If

        Select Case chordMode
            Case 0
                midimsg = &H90 + channel + (baseNote + Index + 4) * &H100 + volume * &H10000
                sendMidiMsg(hmidi, midimsg)
                midimsg = &H90 + channel + (baseNote + Index + 7) * &H100 + volume * &H10000
                sendMidiMsg(hmidi, midimsg)
            Case 1
                midimsg = &H90 + channel + (baseNote + Index + 3) * &H100 + volume * &H10000
                sendMidiMsg(hmidi, midimsg)
                midimsg = &H90 + channel + (baseNote + Index + 7) * &H100 + volume * &H10000
                sendMidiMsg(hmidi, midimsg)
            Case 2
                midimsg = &H90 + channel + (baseNote + Index + 4) * &H100 + volume * &H10000
                sendMidiMsg(hmidi, midimsg)
                midimsg = &H90 + channel + (baseNote + Index + 7) * &H100 + volume * &H10000
                sendMidiMsg(hmidi, midimsg)
                midimsg = &H90 + channel + (baseNote + Index + 11) * &H100 + volume * &H10000
                sendMidiMsg(hmidi, midimsg)
            Case 3
                midimsg = &H90 + channel + (baseNote + Index + 3) * &H100 + volume * &H10000
                sendMidiMsg(hmidi, midimsg)
                midimsg = &H90 + channel + (baseNote + Index + 7) * &H100 + volume * &H10000
                sendMidiMsg(hmidi, midimsg)
                midimsg = &H90 + channel + (baseNote + Index + 10) * &H100 + volume * &H10000
                sendMidiMsg(hmidi, midimsg)
            Case 4
                midimsg = &H90 + channel + (baseNote + Index + 4) * &H100 + volume * &H10000
                sendMidiMsg(hmidi, midimsg)
                midimsg = &H90 + channel + (baseNote + Index + 7) * &H100 + volume * &H10000
                sendMidiMsg(hmidi, midimsg)
                midimsg = &H90 + channel + (baseNote + Index + 10) * &H100 + volume * &H10000
                sendMidiMsg(hmidi, midimsg)
            Case 5
                midimsg = &H90 + channel + (baseNote + Index + 4) * &H100 + volume * &H10000
                sendMidiMsg(hmidi, midimsg)
                midimsg = &H90 + channel + (baseNote + Index + 8) * &H100 + volume * &H10000
                sendMidiMsg(hmidi, midimsg)
                midimsg = &H90 + channel + (baseNote + Index + 11) * &H100 + volume * &H10000
                sendMidiMsg(hmidi, midimsg)
        End Select
    End Sub

    ' Raise the button and send midi stop event
    Private Sub StopNote(ByRef Index As Short)
        If Index = INVALID_NOTE Then
            Exit Sub
        End If

        key(Index).CheckState = System.Windows.Forms.CheckState.Unchecked
        midimsg = &H80 + ((baseNote + Index) * &H100) + (volume * &H10000) + channel
        sendMidiMsg(hmidi, midimsg)

        If playChord = False Then
            Exit Sub
        End If

        Select Case chordMode
            Case 0
                midimsg = &H80 + channel + (baseNote + Index + 4) * &H100 + volume * &H10000
                sendMidiMsg(hmidi, midimsg)
                midimsg = &H80 + channel + (baseNote + Index + 7) * &H100 + volume * &H10000
                sendMidiMsg(hmidi, midimsg)
            Case 1
                midimsg = &H80 + channel + (baseNote + Index + 3) * &H100 + volume * &H10000
                sendMidiMsg(hmidi, midimsg)
                midimsg = &H80 + channel + (baseNote + Index + 7) * &H100 + volume * &H10000
                sendMidiMsg(hmidi, midimsg)
            Case 2
                midimsg = &H80 + channel + (baseNote + Index + 4) * &H100 + volume * &H10000
                sendMidiMsg(hmidi, midimsg)
                midimsg = &H80 + channel + (baseNote + Index + 7) * &H100 + volume * &H10000
                sendMidiMsg(hmidi, midimsg)
                midimsg = &H80 + channel + (baseNote + Index + 11) * &H100 + volume * &H10000
                sendMidiMsg(hmidi, midimsg)
            Case 3
                midimsg = &H80 + channel + (baseNote + Index + 3) * &H100 + volume * &H10000
                sendMidiMsg(hmidi, midimsg)
                midimsg = &H80 + channel + (baseNote + Index + 7) * &H100 + volume * &H10000
                sendMidiMsg(hmidi, midimsg)
                midimsg = &H80 + channel + (baseNote + Index + 10) * &H100 + volume * &H10000
                sendMidiMsg(hmidi, midimsg)
            Case 4
                midimsg = &H80 + channel + (baseNote + Index + 4) * &H100 + volume * &H10000
                sendMidiMsg(hmidi, midimsg)
                midimsg = &H80 + channel + (baseNote + Index + 7) * &H100 + volume * &H10000
                sendMidiMsg(hmidi, midimsg)
                midimsg = &H80 + channel + (baseNote + Index + 10) * &H100 + volume * &H10000
                sendMidiMsg(hmidi, midimsg)
            Case 5
                midimsg = &H80 + channel + (baseNote + Index + 4) * &H100 + volume * &H10000
                sendMidiMsg(hmidi, midimsg)
                midimsg = &H80 + channel + (baseNote + Index + 8) * &H100 + volume * &H10000
                sendMidiMsg(hmidi, midimsg)
                midimsg = &H80 + channel + (baseNote + Index + 11) * &H100 + volume * &H10000
                sendMidiMsg(hmidi, midimsg)
        End Select
    End Sub

    ' Get the note corresponding to a keyboard key
    Private Function NoteFromKey(ByRef key As Short) As Short

        NoteFromKey = INVALID_NOTE

        Select Case key
            Case System.Windows.Forms.Keys.Z
                NoteFromKey = 0
            Case System.Windows.Forms.Keys.S
                NoteFromKey = 1
            Case System.Windows.Forms.Keys.X
                NoteFromKey = 2
            Case System.Windows.Forms.Keys.D
                NoteFromKey = 3
            Case System.Windows.Forms.Keys.C
                NoteFromKey = 4
            Case System.Windows.Forms.Keys.V
                NoteFromKey = 5
            Case System.Windows.Forms.Keys.G
                NoteFromKey = 6
            Case System.Windows.Forms.Keys.B
                NoteFromKey = 7
            Case System.Windows.Forms.Keys.H
                NoteFromKey = 8
            Case System.Windows.Forms.Keys.N
                NoteFromKey = 9
            Case System.Windows.Forms.Keys.J
                NoteFromKey = 10
            Case System.Windows.Forms.Keys.M
                NoteFromKey = 11
            Case 188 ' comma
                NoteFromKey = 12
            Case System.Windows.Forms.Keys.L
                NoteFromKey = 13
            Case 190 ' period
                NoteFromKey = 14
            Case 186 ' semicolon
                NoteFromKey = 15
            Case 191 ' forward slash
                NoteFromKey = 16
        End Select

    End Function

    ' Set the volume
    Private Sub vsbVolume_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles vsbVolume.Scroll
        Select Case e.Type
            Case System.Windows.Forms.ScrollEventType.EndScroll
                volume = vsbVolume.Maximum - e.NewValue
        End Select
    End Sub


    ' Start recording MIDI sequence
    Private Sub btnRecord_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles btnRecord.Click

        btnRecord.Enabled = False
        btnStop.Enabled = True
        btnPlay.Enabled = False
        btnRemoveSilence.Enabled = False
        btnAppend.Enabled = False
        ckLoopMode.Enabled = False
        tbSpeed.Enabled = False
        tbTranspose.Enabled = False

        ' 2.1 Start recording a MIDI sequence
        ' Initialize the sequence data
        isRecording = True
        startTime = DateTime.Now
        midiSequence = Nothing
        midiSequence = New SequenceData

        ' Send the messages for the instrument so that the playback will match
        midimsg = &HC0 + (tbInstrument.Value * &H100) + channel
        sendMidiMsg(hmidi, midimsg)

    End Sub

    ' Append the MIDI sequence
    Private Sub btnAppend_Click(sender As System.Object, e As System.EventArgs) Handles btnAppend.Click
        btnAppend.Enabled = False
        btnRecord.Enabled = False
        btnStop.Enabled = True
        btnPlay.Enabled = False
        btnRemoveSilence.Enabled = False
        ckLoopMode.Enabled = False
        tbSpeed.Enabled = False
        tbTranspose.Enabled = False

        isRecording = True
        Dim recordTime As TimeSpan
        recordTime = endTime.Subtract(startTime)
        startTime = DateTime.Now.Add(-recordTime)


        midimsg = &HC0 + (tbInstrument.Value * &H100) + channel
        sendMidiMsg(hmidi, midimsg)

    End Sub

    ' Stop recording MIDI sequence
    Private Sub btnStop_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles btnStop.Click

        btnRecord.Enabled = True
        btnStop.Enabled = False
        btnPlay.Enabled = True
        btnRemoveSilence.Enabled = True
        btnAppend.Enabled = True
        ckLoopMode.Enabled = True
        tbSpeed.Enabled = True
        tbTranspose.Enabled = True


        midimsg = &HB0 + channel + &H7B * &H100
        sendMidiMsg(hmidi, midimsg)

        If isRecording Then
            endTime = DateTime.Now
        End If
        ' 2.2 Stop recording a MIDI sequence
        ' Stop the recording
        isRecording = False

        ' 2.3 Play a MIDI sequence
        ' Stop the playback

        tmrSequencer.Enabled = False

    End Sub

    ' Play the MIDI sequence recorded
    Private Sub btnPlay_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles btnPlay.Click



        If midiSequence Is Nothing Then
            Exit Sub
        End If

        ' 2.3 Play a MIDI sequence
        ' Start the timer using the time of the first sequencer message data

        currentIndex = 0 ' Store the current index of the MIDI msg

        If midiSequence.dataLength = 0 Then
            Exit Sub
        End If

        btnRecord.Enabled = False
        btnStop.Enabled = True
        btnPlay.Enabled = False
        btnRemoveSilence.Enabled = False
        btnAppend.Enabled = False
        tbSpeed.Enabled = False
        ckLoopMode.Enabled = False
        tbTranspose.Enabled = False


        ' Start the timer using the time of the first sequencer message data
        tmrSequencer.Interval = CInt(midiSequence.data(0).time.TotalMilliseconds * speed) + 1
        tmrSequencer.Enabled = True

    End Sub

    Private Sub tmrSequencer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrSequencer.Tick
        ' 2.3 Play a MIDI sequence
        ' Send the MIDI message of the current message and schedule the next one


        ' Stop the timer from running
        tmrSequencer.Enabled = False

        If midiSequence Is Nothing Then
            Exit Sub
        End If

        Dim currentTime As TimeSpan
        ' Consume all sequence data which is on time
        currentTime = midiSequence.data(currentIndex).time


        While currentTime >= midiSequence.data(currentIndex).time

            If (midiSequence.data(currentIndex).midiMsg And &HF0) = &HC0 Then
                tbInstrument.Value = midiSequence.data(currentIndex).midiMsg / &H100 - 1
                midimsg = midiSequence.data(currentIndex).midiMsg
            ElseIf ((midiSequence.data(currentIndex).midiMsg And &HE0) = &H80) And (midiSequence.data(currentIndex).midiMsg And &HF) <> 9 Then
                Dim pitch As Integer
                pitch = (midiSequence.data(currentIndex).midiMsg And &HFF00) / &H100
                pitch = pitch + tbTranspose.Value
                If pitch > 127 Then
                    pitch = 127
                ElseIf pitch < 0 Then
                    pitch = 0
                End If

                If (pitch - baseNote >= 0) And (pitch - baseNote <= 16) Then
                    key(pitch - baseNote).Checked = Not key(pitch - baseNote).Checked
                End If

                midimsg = (midiSequence.data(currentIndex).midiMsg And &HFF00FF) + pitch * &H100
            Else
                midimsg = midiSequence.data(currentIndex).midiMsg
            End If

            sendMidiMsg(hmidi, midimsg)

            If currentIndex = midiSequence.dataLength - 1 Then
                If ckLoopMode.Checked = False Then
                    btnRecord.Enabled = True
                    btnStop.Enabled = False
                    btnPlay.Enabled = True
                    btnRemoveSilence.Enabled = True
                    btnAppend.Enabled = True
                    ckLoopMode.Enabled = True
                    tbSpeed.Enabled = True
                    tbTranspose.Enabled = True
                    Exit Sub
                Else
                    currentTime = currentTime.Subtract(midiSequence.data(currentIndex).time)
                    currentIndex = -1
                End If
            End If

            currentIndex = currentIndex + 1
        End While

        ' Schedule the timer for the next sequencer message
        tmrSequencer.Interval = CInt((midiSequence.data(currentIndex).time - currentTime).TotalMilliseconds * speed) + 1
        tmrSequencer.Enabled = True

    End Sub

    ' Send a MIDI message and store the message
    Private Sub sendMidiMsg(ByVal hMidiOut As Integer, ByVal dwMsg As Integer)
        ' Send a MIDI message
        midiOutShortMsg(hMidiOut, dwMsg)

        ' 2.1 Start recording a MIDI sequence
        ' Store the message

        If isRecording Then
            midiSequence.AddSequenceData(dwMsg, DateTime.Now.Subtract(startTime))
        End If

    End Sub

    ' Trackbar speed
    Private Sub tbSpeed_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbSpeed.Scroll
        If tbSpeed.Value > 0 Then
            speed = 1 - tbSpeed.Value / 10.0
        Else
            speed = 1 - tbSpeed.Value / 10.0
        End If
    End Sub

    ' Remove silence
    Private Sub btnRemoveSilence_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveSilence.Click
        If midiSequence Is Nothing Then
            Exit Sub
        End If
        If midiSequence.dataLength = 0 Then
            Exit Sub
        End If
        ' 3.1 Remove silence at the start
        Dim i As Integer
        Dim silencetime As TimeSpan
        silencetime = midiSequence.data(0).time
        For i = 0 To midiSequence.dataLength - 1
            midiSequence.data(i).time = midiSequence.data(i).time - silencetime
        Next i
    End Sub

    ' Change the instrument by sending a program change message
    Private Sub tbInstrument_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbInstrument.Scroll
        ' Send a program change message for the instrument
        midimsg = &HC0 + (tbInstrument.Value * &H100) + channel
        sendMidiMsg(hmidi, midimsg)
        instrument(channel) = tbInstrument.Value
    End Sub

    ' Bank MSB
    Private Sub tbBankMSB_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbBankMSB.ValueChanged
        ' Send a control change message to change the bank
        midimsg = &HB0 + channel + (&H0 * &H100) + (tbBankMSB.Value * &H10000)
        sendMidiMsg(hmidi, midimsg)

        ' Resend a program change message for the instrument so that it comes into effect immediately
        midimsg = &HC0 + (tbInstrument.Value * &H100) + channel
        sendMidiMsg(hmidi, midimsg)
    End Sub

    Private Function sendMsgForWhiteboard(ByVal x As Double, ByVal y As Double) As Integer

        Dim pitch As Integer


        ' Instrument is selected
        If cbXselected = 1 Or cbYselected = 1 Then
            tbInstrument.Value = IIf(cbXselected = 1, x * 127, y * 127)
            midimsg = &HC0 + (tbInstrument.Value * &H100) + channel
            sendMidiMsg(hmidi, midimsg)
        End If

        ' Velocity is selected
        If cbXselected = 2 Or cbYselected = 2 Then
            volume = IIf(cbXselected = 2, x * 127, y * 127)
            vsbVolume.Value = 127 - volume
        End If

        ' Stereo position is selected
        If cbXselected = 4 Or cbYselected = 4 Then
            tbpanning.Value = IIf(cbXselected = 4, x * 127, y * 127)
            midimsg = &HB0 + channel + 10 * &H100 + tbpanning.Value * &H10000
            sendMidiMsg(hmidi, midimsg)
        End If

        ' Pitch Bend is selected
        If cbXselected = 5 Or cbYselected = 5 Then
            Dim pitchBend As Integer
            pitchBend = IIf(cbXselected = 5, x * 16383, y * 16383)
            midimsg = &HE0 + channel + (pitchBend Mod 128) * &H100 + Math.Floor(pitchBend / 128) * &H10000
            sendMidiMsg(hmidi, midimsg)
        End If

        ' Pitch is selected
        If cbXselected = 3 Or cbYselected = 3 Then
            pitch = IIf(cbXselected = 3, x * 127, y * 127)
            If lastMidiMessage <> -1 Then
                midimsg = lastMidiMessage And &HFFFFEF
                sendMidiMsg(hmidi, midimsg)
            End If
            midimsg = &H90 + channel + pitch * &H100 + volume * &H10000
            sendMidiMsg(hmidi, midimsg)
            lastMidiMessage = midimsg
        End If



    End Function

    Private Sub picWhiteboard_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picWhiteboard.MouseMove

        Dim x As Double, y As Double


        ' Check the range of the x and y values
        If Not e.Button = MouseButtons.Left Or
            e.X < 0 Or e.X >= picWhiteboard.Width Or
            e.Y < 0 Or e.Y >= picWhiteboard.Height Then
            Exit Sub
        End If
        x = e.X / (picWhiteboard.Width - 1)
        y = e.Y / (picWhiteboard.Height - 1)
        sendMsgForWhiteboard(x, y)
        ' Send MIDI messages based on the x and y range
        ' Display the values in the x and y labels
    End Sub

    Private Sub cboXTitle_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboXTitle.SelectedIndexChanged
        If cboXTitle.SelectedIndex = cbYselected Then
            cboXTitle.SelectedIndex = cbXselected
        Else
            cbXselected = cboXTitle.SelectedIndex
        End If
    End Sub

    Private Sub cboYTitle_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboYTitle.SelectedIndexChanged
        If cboYTitle.SelectedIndex = cbXselected Then
            cboYTitle.SelectedIndex = cbYselected
        Else
            cbYselected = cboYTitle.SelectedIndex
        End If
    End Sub

    Private Sub picWhiteboard_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picWhiteboard.MouseDown
                picWhiteboard_MouseMove(sender, e)

    End Sub

    Private Sub picWhiteboard_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picWhiteboard.MouseUp
        If lastMidiMessage <> -1 Then
            midimsg = lastMidiMessage And &HFFFFEF
            midiOutShortMsg(hmidi, midimsg)
            lastMidiMessage = -1
        End If
    End Sub

    ' Draw the drum slots in a PictureBox (picDrum) in two rows
    Private Sub DrawDrumConfiguration(ByVal g As Graphics)
        Dim Slot, Drum As Short
        Dim X1, X2 As Single
        Dim Y1, Y2 As Single
        Dim Width, Height As Single

        ' The width and height of a slot in the drum machine
        Width = 300 / 8
        Height = 40 / 2


        picDrum.Height = Height * drum_instrument
        picDrum.Width = Width * drum_slot


        g.Clear(Color.White)

        'Create pens
        Dim blackBrush As New SolidBrush(Color.Black)
        Dim redPen As New Pen(Color.Red, 3)

        For Slot = 1 To drum_slot
            X1 = Width * (Slot - 1)
            X2 = Width * Slot

            For Drum = 1 To drum_instrument
                Y1 = Height * (Drum - 1)
                Y2 = Height * Drum

                ' Draw a black box if the slot is selected
                If drumSlot(Drum, Slot) Then
                    g.FillRectangle(blackBrush, X1, Y1, Width, Height)
                End If
            Next
            g.DrawLine(redPen, X1, 0, X1, (picDrum.ClientRectangle.Height))
        Next

        ' Draw the red separators between the slots
        For Drum = 1 To drum_instrument - 1
            g.DrawLine(redPen, 0, Height * Drum, (picDrum.ClientRectangle.Width), Height * Drum)
        Next
    End Sub

    Private Sub picDrum_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picDrum.MouseDown
        Dim X As Single = e.X
        Dim Y As Single = e.Y
        Dim Drum, Slot As Short
        Dim Width, Height As Single

        Width = picDrum.ClientRectangle.Width / drum_slot
        Height = picDrum.ClientRectangle.Height / drum_instrument

        ' Determine the slot where the user has selected
        Slot = Math.Floor(X / Width) + 1
        Drum = Math.Floor(Y / Height) + 1

        ' Set/unset the drum slot
        drumSlot(Drum, Slot) = Not drumSlot(Drum, Slot)

        ' Redraw the drum machine
        DrawDrumConfiguration(picDrum.CreateGraphics())
    End Sub

    Private Sub tmrDrumPlayback_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrDrumPlayback.Tick
        Static Slot As Short
        Static Slot_p As Short = 0
        Dim Drum As Short
        Dim X1, X2, Width As Single
        Dim bluePen As New Pen(Color.Blue, 3)
        Dim redPen As New Pen(Color.Red, 3)
        Dim g As Graphics = picDrum.CreateGraphics

        Width = picDrum.ClientRectangle.Width / drum_slot

        X1 = Width * (Slot_p - 1)
        X2 = Width * (Slot - 1)

        'Draw the red line to overwrite the blue line

        g.DrawLine(redPen, X1, 0, X1, (picDrum.ClientRectangle.Height))


        ' Initialize the slot number
        If Slot = 0 Then Slot = 1

        ' Start/Stop a drum for each row in the drum machine
        For Drum = 1 To drum_instrument
            If drumMessageSent(Drum) Then
                ' You need to stop any drum note already sent to
                ' the midi card by checking the variable DrumMessageSent

                midimsg = &H89 + drumNumber(Drum) * &H100 + &H7F * &H10000
                sendMidiMsg(hmidi, midimsg)
            End If

            If drumSlot(Drum, Slot) Then
                ' Here, a drum slot is selected that means you have to
                ' start a midi note with the drum sound

                midimsg = &H99 + drumNumber(Drum) * &H100 + &H7F * &H10000
                sendMidiMsg(hmidi, midimsg)


                drumMessageSent(Drum) = True
            Else
                drumMessageSent(Drum) = False
            End If
        Next

        'Draw the blue line

        g.DrawLine(bluePen, X2, 0, X2, (picDrum.ClientRectangle.Height))

        'Save the current position
        Slot_p = Slot

        ' Increase the number by 1
        Slot = (Slot Mod drum_slot) + 1
    End Sub

    ' Draw the drum machine
    Private Sub picDrum_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles picDrum.Paint
        DrawDrumConfiguration(e.Graphics())
    End Sub

    Private Sub btnDrumstart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDrumStart.Click
        ' start the drum timer
        tmrDrumPlayback.Interval = 250 * drumspeed
        tmrDrumPlayback.Enabled = True
        tbDrumSpeed.Enabled = False
    End Sub

    Private Sub btnDrumstop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDrumStop.Click
        Dim Drum As Short

        'ReDraw the drum machine
        DrawDrumConfiguration(picDrum.CreateGraphics())

        ' stop the drum timer
        tmrDrumPlayback.Enabled = False

        ' You need to stop any drum note already sent to the midi card

        For Drum = 1 To drum_instrument
            If drumMessageSent(Drum) Then
                midimsg = &H89 + drumNumber(Drum) * &H100 + &H7F * &H10000
                sendMidiMsg(hmidi, midimsg)
            End If
        Next
        tbDrumSpeed.Enabled = True
    End Sub

    Private Sub picDisplay_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim g As Graphics = e.Graphics()

        g.Clear(Color.White)

        ' This code simply draws three rectangles on the picture box
        g.FillRectangle(Brushes.Black, 10, 10, 10, 2)
        g.FillRectangle(Brushes.Black, 20, 20, 10, 2)
        g.FillRectangle(Brushes.Black, 30, 30, 10, 2)
    End Sub

    Private Sub mnuOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOpen.Click
        Dim dlg As New OpenFileDialog
        Dim midiFile As MIDIFile
        Dim midiSequences() As SequenceData

        ' Ask for the MIDI file
        dlg.DefaultExt = "mid"
        dlg.Filter = "MIDI files (*.mid)|*.mid"
        dlg.Multiselect = False

        If dlg.ShowDialog() = DialogResult.OK Then
            ' Load the file into the MIDIFile structure
            midiFile = New MIDIFile(dlg.FileName)

            ' Convert the MIDI file into the SequenceData memory structure
            midiSequences = midiFile.ConvertToSequence()
            If midiSequences.Length > 0 Then
                ' Here the first track of the MIDI file is set into the sequencer memory
                midiSequence = midiSequences(0)
            End If
        End If
    End Sub



    Private Sub ckChordMode_CheckedChanged(sender As Object, e As EventArgs) Handles ckChordMode.CheckedChanged
        playChord = ckChordMode.Checked
        cbChordMode.Enabled = ckChordMode.Checked
        cbChordMode.SelectedIndex = chordMode
    End Sub

    Private Sub cbChordMode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbChordMode.SelectedIndexChanged
        chordMode = cbChordMode.SelectedIndex
    End Sub

    Private Sub tbBaseNote_Scroll(sender As System.Object, e As System.EventArgs) Handles tbBaseNote.Scroll
        baseNote = tbBaseNote.Value
        lbBaseNote.Text = baseNote
    End Sub

    Private Sub tbChannel_Scroll(sender As System.Object, e As System.EventArgs) Handles tbChannel.Scroll
        chan(channel).Checked = False
        channel = tbChannel.Value
        lbChannel.Text = channel + 1
        chan(channel).Checked = True
        tbInstrument.Value = instrument(channel)
        tbpanning.Value = panning(channel)
    End Sub

    Private Sub tbpanning_ValueChanged(sender As System.Object, e As System.EventArgs) Handles tbpanning.ValueChanged
        midimsg = &HB0 + channel + 10 * &H100 + tbpanning.Value * &H10000
        sendMidiMsg(hmidi, midimsg)
        panning(channel) = tbpanning.Value
    End Sub

    Private Sub btnReset_Click(sender As System.Object, e As System.EventArgs) Handles btnReset.Click

        tbSpeed.Value = 0
        speed = 1
        tbTranspose.Value = 0

        Dim i As Integer
        For i = 0 To 15
            midimsg = &HB0 + i + &H7B * &H100
            sendMidiMsg(hmidi, midimsg)

            midimsg = &HB0 + i + 10 * &H100 + 64 * &H10000
            sendMidiMsg(hmidi, midimsg)
            panning(i) = 64
            tbpanning.Value = 64


            midimsg = &HC0 + i + (0 * &H100)
            sendMidiMsg(hmidi, midimsg)
            instrument(i) = tbInstrument.Value
            tbInstrument.Value = 0


            ' Send a control change message to change the bank  
            midimsg = &HB0 + i + (&H0 * &H100) + (0 * &H10000)
            sendMidiMsg(hmidi, midimsg)
            tbBankMSB.Value = 0

        Next

        chan(channel).Checked = False
        channel = 0
        tbChannel.Value = 0
        lbChannel.Text = 1
        chan(0).Checked = True

        baseNote = 60
        tbBaseNote.Value = 60
        lbBaseNote.Text = 60

    End Sub


    Private Sub nColumns_ValueChanged(sender As System.Object, e As System.EventArgs) Handles nColumns.ValueChanged
        drum_slot = nColumns.Value
        DrawDrumConfiguration(picDrum.CreateGraphics())
    End Sub

    Private Sub nRows_ValueChanged(sender As System.Object, e As System.EventArgs) Handles nRows.ValueChanged
        drum_instrument = nRows.Value
        DrawDrumConfiguration(picDrum.CreateGraphics())
    End Sub


    Private Sub btnRandomIns_Click(sender As System.Object, e As System.EventArgs) Handles btnRandomIns.Click
        Randomize()
        Dim i As Integer
        For i = 1 To drum_instrument
            drumNumber(i) = CInt(Rnd() * 47) + 35
        Next
        cbDrumNumber.SelectedIndex = 0
        cbDrumInstrument.SelectedIndex = drumNumber(1) - 35
    End Sub

    Private Sub btnRandom_Click(sender As System.Object, e As System.EventArgs) Handles btnRandom.Click
        Randomize()
        Dim i As Integer
        Dim j As Integer
        For i = 1 To drum_instrument
            For j = 1 To drum_slot
                drumSlot(i, j) = (Rnd() < 0.5)
            Next j
        Next i
        DrawDrumConfiguration(picDrum.CreateGraphics())
    End Sub

    Private Sub btnInversion_Click(sender As System.Object, e As System.EventArgs) Handles btnInversion.Click
        Dim i As Integer
        Dim j As Integer
        For i = 1 To drum_instrument
            For j = 1 To drum_slot
                drumSlot(i, j) = Not drumSlot(i, j)
            Next j
        Next i
        DrawDrumConfiguration(picDrum.CreateGraphics())
    End Sub

    Private Sub btnReversion_Click(sender As System.Object, e As System.EventArgs) Handles btnReversion.Click
        Dim i As Integer
        Dim j As Integer
        For i = 1 To drum_instrument
            For j = 1 To CInt(drum_slot / 2) + 1
                Dim temp As Integer
                temp = drumSlot(i, j)
                drumSlot(i, j) = drumSlot(i, drum_slot + 1 - j)
                drumSlot(i, drum_slot + 1 - j) = temp
            Next j
        Next i
        DrawDrumConfiguration(picDrum.CreateGraphics())
    End Sub

    Private Sub cbDrumInstrument_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbDrumInstrument.SelectedIndexChanged
        drumNumber(cbDrumNumber.SelectedIndex + 1) = cbDrumInstrument.SelectedIndex + 35
    End Sub

    Private Sub cbDrumNumber_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbDrumNumber.SelectedIndexChanged
        cbDrumInstrument.SelectedIndex = drumNumber(cbDrumNumber.SelectedIndex + 1) - 35
    End Sub

    Private Sub tbDrumSpeed_Scroll(sender As System.Object, e As System.EventArgs) Handles tbDrumSpeed.Scroll
        drumspeed = 1 - tbDrumSpeed.Value / 10.0

    End Sub


    Private Sub btnDrumReset_Click(sender As System.Object, e As System.EventArgs) Handles btnDrumReset.Click
        tbDrumSpeed.Value = 0
        speed = 1
        Dim i As Integer
        Dim j As Integer

        midimsg = &HB0 + 10 + &H7B * &H100
        sendMidiMsg(hmidi, midimsg)

        For i = 0 To 10
            For j = 0 To 16
                drumSlot(i, j) = False
            Next
        Next
        DrawDrumConfiguration(picDrum.CreateGraphics())

        tmrDrumPlayback.Enabled = False

        For i = 1 To 10
            drumNumber(i) = &H3C - 1 + i
        Next

        cbDrumNumber.SelectedIndex = 0
        cbDrumInstrument.SelectedIndex = drumNumber(1) - 35

    End Sub

    Private Sub mnuDrumOpen_Click(sender As System.Object, e As System.EventArgs) Handles mnuDrumOpen.Click

        Dim dlg As New OpenFileDialog
        Dim fileReader As System.IO.StreamReader
        Dim currentLine As String

        ' Ask for the text file
        dlg.DefaultExt = "txt"
        dlg.Filter = "Text Documents (*.txt)|*.txt"
        dlg.Multiselect = False

        If dlg.ShowDialog() = DialogResult.OK Then
            Try
                fileReader = My.Computer.FileSystem.OpenTextFileReader(dlg.FileName)
                currentLine = fileReader.ReadLine()
                Dim firstArray() As String = Split(currentLine)
                drum_instrument = CInt(firstArray(0))
                drum_slot = CInt(firstArray(1))
                fileReader.ReadLine()

                If drum_instrument < 1 Or drum_instrument > 10 Or drum_slot < 2 Or drum_slot > 12 Then
                    Throw New System.Exception()
                End If

                Dim i As Integer
                Dim j As Integer

                For i = 1 To drum_instrument

                    currentLine = fileReader.ReadLine()
                    Dim currentArray() As String = Split(currentLine)
     
                    For j = 1 To drum_slot
                        If currentArray(j - 1) = 1 Then
                            drumSlot(i, j) = True
                        Else
                            drumSlot(i, j) = False
                        End If
                    Next j
                    drumNumber(i) = currentArray(drum_slot)
                    fileReader.ReadLine()
                Next i


                nRows.Value = drum_instrument
                nColumns.Value = drum_slot
                DrawDrumConfiguration(picDrum.CreateGraphics())
                cbDrumNumber.SelectedIndex = 0
                cbDrumInstrument.SelectedIndex = drumNumber(1) - 35

            Catch ex As Exception

            End Try


        End If

    End Sub


End Class
