﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F1_AsientoContableBanco
    Inherits Modelos.ModeloF00

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F1_AsientoContableBanco))
        Dim cbbanco_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim cbSucursal_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.GroupPanelBanco = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.grbanco = New Janus.Windows.GridEX.GridEX()
        Me.GrDatos = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.cbbanco = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.Lb_Saldo = New System.Windows.Forms.Label()
        Me.Lb_Banco = New System.Windows.Forms.Label()
        Me.Lb_efec = New System.Windows.Forms.Label()
        Me.LabelX7 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX6 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX17 = New DevComponents.DotNetBar.LabelX()
        Me.cbSucursal = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.btnNuevoTipoCambio = New DevComponents.DotNetBar.ButtonX()
        Me.btActualizar = New DevComponents.DotNetBar.ButtonX()
        Me.LabelX5 = New DevComponents.DotNetBar.LabelX()
        Me.tbFechaF = New System.Windows.Forms.DateTimePicker()
        Me.tbNumi = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.tbTipoCambio = New DevComponents.Editors.DoubleInput()
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.tbFechaI = New System.Windows.Forms.DateTimePicker()
        Me.SuperTabGeneral = New DevComponents.DotNetBar.SuperTabControl()
        Me.SuperTabControlPanel3 = New DevComponents.DotNetBar.SuperTabControlPanel()
        Me.grAsientoBanco = New Janus.Windows.GridEX.GridEX()
        Me.SuperTabItem3 = New DevComponents.DotNetBar.SuperTabItem()
        Me.GroupPanel2 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.grmovimientos = New Janus.Windows.GridEX.GridEX()
        Me.btnbanco = New DevComponents.DotNetBar.ButtonX()
        Me.PanelSuperior.SuspendLayout()
        Me.PanelInferior.SuspendLayout()
        CType(Me.BubbleBarUsuario, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelToolBar1.SuspendLayout()
        Me.PanelToolBar2.SuspendLayout()
        Me.PanelPrincipal.SuspendLayout()
        Me.PanelUsuario.SuspendLayout()
        Me.PanelNavegacion.SuspendLayout()
        Me.MPanelUserAct.SuspendLayout()
        CType(Me.MEP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelContent.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.MSuperTabControlPanel1.SuspendLayout()
        CType(Me.MSuperTabControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MSuperTabControl.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelBuscador.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.GroupPanelBanco.SuspendLayout()
        CType(Me.grbanco, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrDatos.SuspendLayout()
        CType(Me.cbbanco, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbSucursal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbTipoCambio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SuperTabGeneral, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuperTabGeneral.SuspendLayout()
        Me.SuperTabControlPanel3.SuspendLayout()
        CType(Me.grAsientoBanco, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupPanel2.SuspendLayout()
        CType(Me.grmovimientos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelSuperior
        '
        Me.PanelSuperior.Controls.Add(Me.btnbanco)
        Me.PanelSuperior.Size = New System.Drawing.Size(1123, 72)
        Me.PanelSuperior.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelSuperior.Style.BackColor1.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.PanelSuperior.Style.BackColor2.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.PanelSuperior.Style.BackgroundImage = CType(resources.GetObject("PanelSuperior.Style.BackgroundImage"), System.Drawing.Image)
        Me.PanelSuperior.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelSuperior.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelSuperior.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelSuperior.Style.GradientAngle = 90
        Me.PanelSuperior.StyleMouseDown.BackColor1.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.PanelSuperior.StyleMouseDown.BackColor2.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.PanelSuperior.StyleMouseOver.BackColor1.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.PanelSuperior.StyleMouseOver.BackColor2.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.PanelSuperior.StyleMouseOver.BackgroundImage = CType(resources.GetObject("PanelSuperior.StyleMouseOver.BackgroundImage"), System.Drawing.Image)
        Me.PanelSuperior.Controls.SetChildIndex(Me.PanelToolBar1, 0)
        Me.PanelSuperior.Controls.SetChildIndex(Me.PanelToolBar2, 0)
        Me.PanelSuperior.Controls.SetChildIndex(Me.MRlAccion, 0)
        Me.PanelSuperior.Controls.SetChildIndex(Me.PictureBox1, 0)
        Me.PanelSuperior.Controls.SetChildIndex(Me.btnbanco, 0)
        '
        'PanelInferior
        '
        Me.PanelInferior.Location = New System.Drawing.Point(0, 659)
        Me.PanelInferior.Size = New System.Drawing.Size(1123, 39)
        Me.PanelInferior.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelInferior.Style.BackColor1.Color = System.Drawing.Color.Transparent
        Me.PanelInferior.Style.BackColor2.Color = System.Drawing.Color.Transparent
        Me.PanelInferior.Style.BackgroundImage = CType(resources.GetObject("PanelInferior.Style.BackgroundImage"), System.Drawing.Image)
        Me.PanelInferior.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelInferior.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelInferior.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelInferior.Style.GradientAngle = 90
        '
        'BubbleBarUsuario
        '
        '
        '
        '
        Me.BubbleBarUsuario.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.BubbleBarUsuario.ButtonBackAreaStyle.BackColor = System.Drawing.Color.Transparent
        Me.BubbleBarUsuario.ButtonBackAreaStyle.BorderBottomWidth = 1
        Me.BubbleBarUsuario.ButtonBackAreaStyle.BorderColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.BubbleBarUsuario.ButtonBackAreaStyle.BorderLeftWidth = 1
        Me.BubbleBarUsuario.ButtonBackAreaStyle.BorderRightWidth = 1
        Me.BubbleBarUsuario.ButtonBackAreaStyle.BorderTopWidth = 1
        Me.BubbleBarUsuario.ButtonBackAreaStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.BubbleBarUsuario.ButtonBackAreaStyle.PaddingBottom = 3
        Me.BubbleBarUsuario.ButtonBackAreaStyle.PaddingLeft = 3
        Me.BubbleBarUsuario.ButtonBackAreaStyle.PaddingRight = 3
        Me.BubbleBarUsuario.ButtonBackAreaStyle.PaddingTop = 3
        Me.BubbleBarUsuario.MouseOverTabColors.BorderColor = System.Drawing.SystemColors.Highlight
        Me.BubbleBarUsuario.SelectedTabColors.BorderColor = System.Drawing.Color.Black
        '
        'TxtNombreUsu
        '
        Me.TxtNombreUsu.ReadOnly = True
        Me.TxtNombreUsu.Text = "DEFAULT"
        '
        'btnSalir
        '
        '
        'btnGrabar
        '
        '
        'btnEliminar
        '
        '
        'btnModificar
        '
        '
        'btnNuevo
        '
        '
        'PanelToolBar2
        '
        Me.PanelToolBar2.Location = New System.Drawing.Point(1043, 0)
        '
        'PanelPrincipal
        '
        Me.PanelPrincipal.Size = New System.Drawing.Size(1123, 698)
        Me.PanelPrincipal.Controls.SetChildIndex(Me.PanelInferior, 0)
        Me.PanelPrincipal.Controls.SetChildIndex(Me.PanelUsuario, 0)
        Me.PanelPrincipal.Controls.SetChildIndex(Me.PanelSuperior, 0)
        Me.PanelPrincipal.Controls.SetChildIndex(Me.Panel1, 0)
        '
        'btnImprimir
        '
        Me.btnImprimir.Visible = False
        '
        'btnUltimo
        '
        '
        'btnSiguiente
        '
        '
        'btnAnterior
        '
        '
        'btnPrimero
        '
        '
        'MPanelUserAct
        '
        Me.MPanelUserAct.Location = New System.Drawing.Point(923, 0)
        '
        'MRlAccion
        '
        '
        '
        '
        Me.MRlAccion.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.MRlAccion.Size = New System.Drawing.Size(667, 72)
        '
        'PanelContent
        '
        Me.PanelContent.Controls.Add(Me.SuperTabGeneral)
        Me.PanelContent.Controls.Add(Me.Panel2)
        Me.PanelContent.Size = New System.Drawing.Size(1090, 587)
        '
        'Panel1
        '
        Me.Panel1.Size = New System.Drawing.Size(1123, 587)
        '
        'MSuperTabControlPanel1
        '
        Me.MSuperTabControlPanel1.Size = New System.Drawing.Size(1090, 587)
        '
        'MSuperTabControl
        '
        '
        '
        '
        '
        '
        '
        Me.MSuperTabControl.ControlBox.CloseBox.Name = ""
        '
        '
        '
        Me.MSuperTabControl.ControlBox.MenuBox.Name = ""
        Me.MSuperTabControl.ControlBox.Name = ""
        Me.MSuperTabControl.ControlBox.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.MSuperTabControl.ControlBox.MenuBox, Me.MSuperTabControl.ControlBox.CloseBox})
        Me.MSuperTabControl.SelectedTabIndex = 1
        Me.MSuperTabControl.Size = New System.Drawing.Size(1123, 587)
        Me.MSuperTabControl.Controls.SetChildIndex(Me.MSuperTabControlPanel1, 0)
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(779, 0)
        '
        'PanelBuscador
        '
        Me.PanelBuscador.Controls.Add(Me.GroupPanel2)
        Me.PanelBuscador.Size = New System.Drawing.Size(951, 450)
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.GroupPanelBanco)
        Me.Panel2.Controls.Add(Me.GrDatos)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1090, 228)
        Me.Panel2.TabIndex = 244
        '
        'GroupPanelBanco
        '
        Me.GroupPanelBanco.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanelBanco.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanelBanco.Controls.Add(Me.grbanco)
        Me.GroupPanelBanco.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanelBanco.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupPanelBanco.Location = New System.Drawing.Point(675, 0)
        Me.GroupPanelBanco.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupPanelBanco.Name = "GroupPanelBanco"
        Me.GroupPanelBanco.Size = New System.Drawing.Size(415, 228)
        '
        '
        '
        Me.GroupPanelBanco.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.GroupPanelBanco.Style.BackColorGradientAngle = 90
        Me.GroupPanelBanco.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.GroupPanelBanco.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanelBanco.Style.BorderBottomWidth = 1
        Me.GroupPanelBanco.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.GroupPanelBanco.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanelBanco.Style.BorderLeftWidth = 1
        Me.GroupPanelBanco.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanelBanco.Style.BorderRightWidth = 1
        Me.GroupPanelBanco.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanelBanco.Style.BorderTopWidth = 1
        Me.GroupPanelBanco.Style.CornerDiameter = 4
        Me.GroupPanelBanco.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanelBanco.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanelBanco.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.GroupPanelBanco.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanelBanco.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanelBanco.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupPanelBanco.TabIndex = 2
        Me.GroupPanelBanco.Text = "BANCOS"
        '
        'grbanco
        '
        Me.grbanco.BackColor = System.Drawing.Color.WhiteSmoke
        Me.grbanco.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grbanco.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grbanco.HeaderFormatStyle.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grbanco.Location = New System.Drawing.Point(0, 0)
        Me.grbanco.Margin = New System.Windows.Forms.Padding(2)
        Me.grbanco.Name = "grbanco"
        Me.grbanco.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.grbanco.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.grbanco.RowFormatStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grbanco.Size = New System.Drawing.Size(409, 205)
        Me.grbanco.TabIndex = 0
        Me.grbanco.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'GrDatos
        '
        Me.GrDatos.CanvasColor = System.Drawing.SystemColors.Control
        Me.GrDatos.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GrDatos.Controls.Add(Me.cbbanco)
        Me.GrDatos.Controls.Add(Me.Lb_Saldo)
        Me.GrDatos.Controls.Add(Me.Lb_Banco)
        Me.GrDatos.Controls.Add(Me.Lb_efec)
        Me.GrDatos.Controls.Add(Me.LabelX7)
        Me.GrDatos.Controls.Add(Me.LabelX6)
        Me.GrDatos.Controls.Add(Me.LabelX2)
        Me.GrDatos.Controls.Add(Me.LabelX17)
        Me.GrDatos.Controls.Add(Me.cbSucursal)
        Me.GrDatos.Controls.Add(Me.btnNuevoTipoCambio)
        Me.GrDatos.Controls.Add(Me.btActualizar)
        Me.GrDatos.Controls.Add(Me.LabelX5)
        Me.GrDatos.Controls.Add(Me.tbFechaF)
        Me.GrDatos.Controls.Add(Me.tbNumi)
        Me.GrDatos.Controls.Add(Me.tbTipoCambio)
        Me.GrDatos.Controls.Add(Me.LabelX4)
        Me.GrDatos.Controls.Add(Me.LabelX1)
        Me.GrDatos.Controls.Add(Me.LabelX3)
        Me.GrDatos.Controls.Add(Me.tbFechaI)
        Me.GrDatos.DisabledBackColor = System.Drawing.Color.Empty
        Me.GrDatos.Dock = System.Windows.Forms.DockStyle.Left
        Me.GrDatos.Location = New System.Drawing.Point(0, 0)
        Me.GrDatos.Margin = New System.Windows.Forms.Padding(2)
        Me.GrDatos.Name = "GrDatos"
        Me.GrDatos.Size = New System.Drawing.Size(675, 228)
        '
        '
        '
        Me.GrDatos.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.GrDatos.Style.BackColorGradientAngle = 90
        Me.GrDatos.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.GrDatos.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GrDatos.Style.BorderBottomWidth = 1
        Me.GrDatos.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.GrDatos.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GrDatos.Style.BorderLeftWidth = 1
        Me.GrDatos.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GrDatos.Style.BorderRightWidth = 1
        Me.GrDatos.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GrDatos.Style.BorderTopWidth = 1
        Me.GrDatos.Style.CornerDiameter = 4
        Me.GrDatos.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GrDatos.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GrDatos.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.GrDatos.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GrDatos.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GrDatos.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GrDatos.TabIndex = 0
        Me.GrDatos.Text = "DATOS"
        '
        'cbbanco
        '
        cbbanco_DesignTimeLayout.LayoutString = resources.GetString("cbbanco_DesignTimeLayout.LayoutString")
        Me.cbbanco.DesignTimeLayout = cbbanco_DesignTimeLayout
        Me.cbbanco.Location = New System.Drawing.Point(339, 34)
        Me.cbbanco.Margin = New System.Windows.Forms.Padding(2)
        Me.cbbanco.Name = "cbbanco"
        Me.cbbanco.SelectedIndex = -1
        Me.cbbanco.SelectedItem = Nothing
        Me.cbbanco.Size = New System.Drawing.Size(142, 22)
        Me.cbbanco.TabIndex = 246
        Me.cbbanco.Visible = False
        '
        'Lb_Saldo
        '
        Me.Lb_Saldo.BackColor = System.Drawing.Color.Transparent
        Me.Lb_Saldo.Font = New System.Drawing.Font("Georgia", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lb_Saldo.Location = New System.Drawing.Point(336, 169)
        Me.Lb_Saldo.Name = "Lb_Saldo"
        Me.Lb_Saldo.Size = New System.Drawing.Size(98, 20)
        Me.Lb_Saldo.TabIndex = 244
        Me.Lb_Saldo.Text = "0"
        Me.Lb_Saldo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Lb_Banco
        '
        Me.Lb_Banco.BackColor = System.Drawing.Color.Transparent
        Me.Lb_Banco.Font = New System.Drawing.Font("Georgia", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lb_Banco.Location = New System.Drawing.Point(336, 138)
        Me.Lb_Banco.Name = "Lb_Banco"
        Me.Lb_Banco.Size = New System.Drawing.Size(98, 20)
        Me.Lb_Banco.TabIndex = 244
        Me.Lb_Banco.Text = "0"
        Me.Lb_Banco.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Lb_efec
        '
        Me.Lb_efec.BackColor = System.Drawing.Color.Transparent
        Me.Lb_efec.Font = New System.Drawing.Font("Georgia", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lb_efec.Location = New System.Drawing.Point(336, 110)
        Me.Lb_efec.Name = "Lb_efec"
        Me.Lb_efec.Size = New System.Drawing.Size(98, 20)
        Me.Lb_efec.TabIndex = 244
        Me.Lb_efec.Text = "0"
        Me.Lb_efec.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LabelX7
        '
        Me.LabelX7.AutoSize = True
        Me.LabelX7.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX7.Font = New System.Drawing.Font("Century", 14.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX7.Location = New System.Drawing.Point(226, 166)
        Me.LabelX7.Name = "LabelX7"
        Me.LabelX7.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX7.Size = New System.Drawing.Size(59, 27)
        Me.LabelX7.TabIndex = 243
        Me.LabelX7.Text = "Saldo"
        '
        'LabelX6
        '
        Me.LabelX6.AutoSize = True
        Me.LabelX6.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX6.Font = New System.Drawing.Font("Century", 14.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX6.Location = New System.Drawing.Point(226, 135)
        Me.LabelX6.Name = "LabelX6"
        Me.LabelX6.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX6.Size = New System.Drawing.Size(73, 27)
        Me.LabelX6.TabIndex = 243
        Me.LabelX6.Text = "Bancos"
        '
        'LabelX2
        '
        Me.LabelX2.AutoSize = True
        Me.LabelX2.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.Font = New System.Drawing.Font("Century", 14.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX2.Location = New System.Drawing.Point(226, 110)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX2.Size = New System.Drawing.Size(82, 27)
        Me.LabelX2.TabIndex = 243
        Me.LabelX2.Text = "Efectivo"
        '
        'LabelX17
        '
        Me.LabelX17.AutoSize = True
        Me.LabelX17.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX17.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX17.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX17.Location = New System.Drawing.Point(268, 6)
        Me.LabelX17.Name = "LabelX17"
        Me.LabelX17.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX17.Size = New System.Drawing.Size(55, 16)
        Me.LabelX17.TabIndex = 242
        Me.LabelX17.Text = "Modulo:"
        Me.LabelX17.Visible = False
        '
        'cbSucursal
        '
        cbSucursal_DesignTimeLayout.LayoutString = resources.GetString("cbSucursal_DesignTimeLayout.LayoutString")
        Me.cbSucursal.DesignTimeLayout = cbSucursal_DesignTimeLayout
        Me.cbSucursal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbSucursal.Location = New System.Drawing.Point(340, 3)
        Me.cbSucursal.Name = "cbSucursal"
        Me.cbSucursal.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.cbSucursal.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.cbSucursal.SelectedIndex = -1
        Me.cbSucursal.SelectedItem = Nothing
        Me.cbSucursal.Size = New System.Drawing.Size(225, 22)
        Me.cbSucursal.TabIndex = 241
        Me.cbSucursal.Visible = False
        Me.cbSucursal.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'btnNuevoTipoCambio
        '
        Me.btnNuevoTipoCambio.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnNuevoTipoCambio.BackColor = System.Drawing.Color.Transparent
        Me.btnNuevoTipoCambio.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat
        Me.btnNuevoTipoCambio.Image = Global.Presentacion.My.Resources.Resources.anadir
        Me.btnNuevoTipoCambio.ImageFixedSize = New System.Drawing.Size(28, 28)
        Me.btnNuevoTipoCambio.Location = New System.Drawing.Point(154, 27)
        Me.btnNuevoTipoCambio.Name = "btnNuevoTipoCambio"
        Me.btnNuevoTipoCambio.Size = New System.Drawing.Size(34, 29)
        Me.btnNuevoTipoCambio.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnNuevoTipoCambio.TabIndex = 240
        Me.btnNuevoTipoCambio.Visible = False
        '
        'btActualizar
        '
        Me.btActualizar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btActualizar.BackColor = System.Drawing.Color.SkyBlue
        Me.btActualizar.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground
        Me.btActualizar.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btActualizar.Image = Global.Presentacion.My.Resources.Resources.reload_5
        Me.btActualizar.ImageFixedSize = New System.Drawing.Size(40, 40)
        Me.btActualizar.Location = New System.Drawing.Point(100, 95)
        Me.btActualizar.Name = "btActualizar"
        Me.btActualizar.Shape = New DevComponents.DotNetBar.RoundRectangleShapeDescriptor(4)
        Me.btActualizar.Size = New System.Drawing.Size(106, 49)
        Me.btActualizar.SubItemsExpandWidth = 10
        Me.btActualizar.TabIndex = 239
        Me.btActualizar.Text = "CARGAR DATOS"
        Me.btActualizar.TextColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        '
        'LabelX5
        '
        Me.LabelX5.AutoSize = True
        Me.LabelX5.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX5.Location = New System.Drawing.Point(203, 60)
        Me.LabelX5.Name = "LabelX5"
        Me.LabelX5.Size = New System.Drawing.Size(19, 15)
        Me.LabelX5.TabIndex = 130
        Me.LabelX5.Text = "AL:"
        Me.LabelX5.Visible = False
        '
        'tbFechaF
        '
        Me.tbFechaF.CalendarMonthBackground = System.Drawing.Color.White
        Me.tbFechaF.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbFechaF.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.tbFechaF.Location = New System.Drawing.Point(226, 58)
        Me.tbFechaF.Name = "tbFechaF"
        Me.tbFechaF.Size = New System.Drawing.Size(100, 20)
        Me.tbFechaF.TabIndex = 129
        Me.tbFechaF.Visible = False
        '
        'tbNumi
        '
        '
        '
        '
        Me.tbNumi.Border.Class = "TextBoxBorder"
        Me.tbNumi.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbNumi.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbNumi.Location = New System.Drawing.Point(100, 3)
        Me.tbNumi.Name = "tbNumi"
        Me.tbNumi.PreventEnterBeep = True
        Me.tbNumi.Size = New System.Drawing.Size(72, 20)
        Me.tbNumi.TabIndex = 123
        Me.tbNumi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbTipoCambio
        '
        '
        '
        '
        Me.tbTipoCambio.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbTipoCambio.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbTipoCambio.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.tbTipoCambio.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbTipoCambio.Increment = 1.0R
        Me.tbTipoCambio.Location = New System.Drawing.Point(100, 32)
        Me.tbTipoCambio.Name = "tbTipoCambio"
        Me.tbTipoCambio.Size = New System.Drawing.Size(57, 20)
        Me.tbTipoCambio.TabIndex = 122
        '
        'LabelX4
        '
        Me.LabelX4.AutoSize = True
        Me.LabelX4.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX4.Location = New System.Drawing.Point(18, 32)
        Me.LabelX4.Name = "LabelX4"
        Me.LabelX4.Size = New System.Drawing.Size(78, 15)
        Me.LabelX4.TabIndex = 128
        Me.LabelX4.Text = "TIPO CAMBIO:"
        '
        'LabelX1
        '
        Me.LabelX1.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX1.Location = New System.Drawing.Point(18, 3)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.Size = New System.Drawing.Size(75, 23)
        Me.LabelX1.TabIndex = 124
        Me.LabelX1.Text = "ID:"
        '
        'LabelX3
        '
        Me.LabelX3.AutoSize = True
        Me.LabelX3.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX3.Location = New System.Drawing.Point(18, 62)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.Size = New System.Drawing.Size(43, 15)
        Me.LabelX3.TabIndex = 127
        Me.LabelX3.Text = "FECHA:"
        '
        'tbFechaI
        '
        Me.tbFechaI.CalendarMonthBackground = System.Drawing.Color.White
        Me.tbFechaI.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbFechaI.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.tbFechaI.Location = New System.Drawing.Point(100, 58)
        Me.tbFechaI.Name = "tbFechaI"
        Me.tbFechaI.Size = New System.Drawing.Size(100, 20)
        Me.tbFechaI.TabIndex = 121
        '
        'SuperTabGeneral
        '
        '
        '
        '
        '
        '
        '
        Me.SuperTabGeneral.ControlBox.CloseBox.Name = ""
        '
        '
        '
        Me.SuperTabGeneral.ControlBox.MenuBox.Name = ""
        Me.SuperTabGeneral.ControlBox.Name = ""
        Me.SuperTabGeneral.ControlBox.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.SuperTabGeneral.ControlBox.MenuBox, Me.SuperTabGeneral.ControlBox.CloseBox})
        Me.SuperTabGeneral.Controls.Add(Me.SuperTabControlPanel3)
        Me.SuperTabGeneral.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SuperTabGeneral.Location = New System.Drawing.Point(0, 228)
        Me.SuperTabGeneral.Margin = New System.Windows.Forms.Padding(2)
        Me.SuperTabGeneral.Name = "SuperTabGeneral"
        Me.SuperTabGeneral.ReorderTabsEnabled = True
        Me.SuperTabGeneral.SelectedTabFont = New System.Drawing.Font("Georgia", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SuperTabGeneral.SelectedTabIndex = 0
        Me.SuperTabGeneral.Size = New System.Drawing.Size(1090, 359)
        Me.SuperTabGeneral.TabFont = New System.Drawing.Font("Georgia", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SuperTabGeneral.TabIndex = 245
        Me.SuperTabGeneral.Tabs.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.SuperTabItem3})
        Me.SuperTabGeneral.TabStyle = DevComponents.DotNetBar.eSuperTabStyle.Office2010BackstageBlue
        Me.SuperTabGeneral.Text = "SuperTabControl1"
        '
        'SuperTabControlPanel3
        '
        Me.SuperTabControlPanel3.Controls.Add(Me.grAsientoBanco)
        Me.SuperTabControlPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SuperTabControlPanel3.Location = New System.Drawing.Point(0, 25)
        Me.SuperTabControlPanel3.Margin = New System.Windows.Forms.Padding(2)
        Me.SuperTabControlPanel3.Name = "SuperTabControlPanel3"
        Me.SuperTabControlPanel3.Size = New System.Drawing.Size(1090, 334)
        Me.SuperTabControlPanel3.TabIndex = 0
        Me.SuperTabControlPanel3.TabItem = Me.SuperTabItem3
        '
        'grAsientoBanco
        '
        Me.grAsientoBanco.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grAsientoBanco.BackColor = System.Drawing.Color.WhiteSmoke
        Me.grAsientoBanco.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grAsientoBanco.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grAsientoBanco.HeaderFormatStyle.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grAsientoBanco.Location = New System.Drawing.Point(0, 0)
        Me.grAsientoBanco.Margin = New System.Windows.Forms.Padding(2)
        Me.grAsientoBanco.Name = "grAsientoBanco"
        Me.grAsientoBanco.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.grAsientoBanco.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.grAsientoBanco.RowFormatStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grAsientoBanco.Size = New System.Drawing.Size(1090, 334)
        Me.grAsientoBanco.TabIndex = 1
        Me.grAsientoBanco.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'SuperTabItem3
        '
        Me.SuperTabItem3.AttachedControl = Me.SuperTabControlPanel3
        Me.SuperTabItem3.GlobalItem = False
        Me.SuperTabItem3.Name = "SuperTabItem3"
        Me.SuperTabItem3.Text = "Detalle Asiento Banco"
        '
        'GroupPanel2
        '
        Me.GroupPanel2.BackColor = System.Drawing.Color.White
        Me.GroupPanel2.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel2.Controls.Add(Me.grmovimientos)
        Me.GroupPanel2.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupPanel2.Font = New System.Drawing.Font("Georgia", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupPanel2.Location = New System.Drawing.Point(0, 0)
        Me.GroupPanel2.Name = "GroupPanel2"
        Me.GroupPanel2.Size = New System.Drawing.Size(951, 450)
        '
        '
        '
        Me.GroupPanel2.Style.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.GroupPanel2.Style.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.GroupPanel2.Style.BackColorGradientAngle = 90
        Me.GroupPanel2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel2.Style.BorderBottomWidth = 1
        Me.GroupPanel2.Style.BorderColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.GroupPanel2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel2.Style.BorderLeftWidth = 1
        Me.GroupPanel2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel2.Style.BorderRightWidth = 1
        Me.GroupPanel2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel2.Style.BorderTopWidth = 1
        Me.GroupPanel2.Style.CornerDiameter = 4
        Me.GroupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanel2.Style.Font = New System.Drawing.Font("Georgia", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupPanel2.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanel2.Style.TextColor = System.Drawing.Color.White
        Me.GroupPanel2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupPanel2.TabIndex = 4
        Me.GroupPanel2.Text = "BUSCADOR"
        '
        'grmovimientos
        '
        Me.grmovimientos.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grmovimientos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grmovimientos.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None
        Me.grmovimientos.FlatBorderColor = System.Drawing.Color.DodgerBlue
        Me.grmovimientos.FocusStyle = Janus.Windows.GridEX.FocusStyle.Solid
        Me.grmovimientos.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grmovimientos.GridLineColor = System.Drawing.Color.DodgerBlue
        Me.grmovimientos.GridLineStyle = Janus.Windows.GridEX.GridLineStyle.Solid
        Me.grmovimientos.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.UseRowStyle
        Me.grmovimientos.HeaderFormatStyle.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grmovimientos.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight
        Me.grmovimientos.Location = New System.Drawing.Point(0, 0)
        Me.grmovimientos.Name = "grmovimientos"
        Me.grmovimientos.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.grmovimientos.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.grmovimientos.SelectedFormatStyle.BackColor = System.Drawing.Color.DodgerBlue
        Me.grmovimientos.SelectedFormatStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grmovimientos.SelectedFormatStyle.ForeColor = System.Drawing.Color.White
        Me.grmovimientos.Size = New System.Drawing.Size(945, 427)
        Me.grmovimientos.TabIndex = 0
        Me.grmovimientos.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'btnbanco
        '
        Me.btnbanco.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnbanco.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange
        Me.btnbanco.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnbanco.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnbanco.Image = Global.Presentacion.My.Resources.Resources.print
        Me.btnbanco.ImageFixedSize = New System.Drawing.Size(48, 48)
        Me.btnbanco.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top
        Me.btnbanco.Location = New System.Drawing.Point(667, 0)
        Me.btnbanco.Name = "btnbanco"
        Me.btnbanco.Size = New System.Drawing.Size(112, 72)
        Me.btnbanco.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnbanco.TabIndex = 13
        Me.btnbanco.Text = "Asiento Banco"
        Me.btnbanco.TextColor = System.Drawing.Color.White
        '
        'F1_AsientoContableBanco
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1123, 698)
        Me.Name = "F1_AsientoContableBanco"
        Me.Text = "AsientoContableBanco"
        Me.Controls.SetChildIndex(Me.PanelPrincipal, 0)
        Me.PanelSuperior.ResumeLayout(False)
        Me.PanelInferior.ResumeLayout(False)
        CType(Me.BubbleBarUsuario, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelToolBar1.ResumeLayout(False)
        Me.PanelToolBar2.ResumeLayout(False)
        Me.PanelPrincipal.ResumeLayout(False)
        Me.PanelUsuario.ResumeLayout(False)
        Me.PanelUsuario.PerformLayout()
        Me.PanelNavegacion.ResumeLayout(False)
        Me.MPanelUserAct.ResumeLayout(False)
        Me.MPanelUserAct.PerformLayout()
        CType(Me.MEP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelContent.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.MSuperTabControlPanel1.ResumeLayout(False)
        CType(Me.MSuperTabControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MSuperTabControl.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelBuscador.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.GroupPanelBanco.ResumeLayout(False)
        CType(Me.grbanco, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrDatos.ResumeLayout(False)
        Me.GrDatos.PerformLayout()
        CType(Me.cbbanco, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbSucursal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbTipoCambio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SuperTabGeneral, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SuperTabGeneral.ResumeLayout(False)
        Me.SuperTabControlPanel3.ResumeLayout(False)
        CType(Me.grAsientoBanco, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupPanel2.ResumeLayout(False)
        CType(Me.grmovimientos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel2 As Panel
    Friend WithEvents GroupPanelBanco As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents grbanco As Janus.Windows.GridEX.GridEX
    Friend WithEvents GrDatos As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents Lb_Saldo As Label
    Friend WithEvents Lb_Banco As Label
    Friend WithEvents Lb_efec As Label
    Friend WithEvents LabelX7 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX6 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX17 As DevComponents.DotNetBar.LabelX
    Friend WithEvents cbSucursal As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents btnNuevoTipoCambio As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btActualizar As DevComponents.DotNetBar.ButtonX
    Friend WithEvents LabelX5 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbFechaF As DateTimePicker
    Friend WithEvents tbNumi As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents tbTipoCambio As DevComponents.Editors.DoubleInput
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbFechaI As DateTimePicker
    Friend WithEvents SuperTabGeneral As DevComponents.DotNetBar.SuperTabControl
    Friend WithEvents SuperTabControlPanel3 As DevComponents.DotNetBar.SuperTabControlPanel
    Friend WithEvents grAsientoBanco As Janus.Windows.GridEX.GridEX
    Friend WithEvents SuperTabItem3 As DevComponents.DotNetBar.SuperTabItem
    Friend WithEvents GroupPanel2 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents grmovimientos As Janus.Windows.GridEX.GridEX
    Protected WithEvents btnbanco As DevComponents.DotNetBar.ButtonX
    Friend WithEvents cbbanco As Janus.Windows.GridEX.EditControls.MultiColumnCombo
End Class
