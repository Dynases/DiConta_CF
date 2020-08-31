Option Strict Off
Imports Logica.AccesoLogica
Imports Janus.Windows.GridEX
Imports DevComponents.DotNetBar
Imports System.IO
Imports DevComponents.DotNetBar.SuperGrid
Imports System.Drawing
Imports DevComponents.DotNetBar.Controls
Imports System.Threading
Imports System.Drawing.Text
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Drawing.Printing
Imports System.Math
Public Class F1_AsientoContableBanco
#Region "Variables Globales"
    Dim RutaGlobal As String = gs_CarpetaRaiz
    Dim RutaTemporal As String = "C:\Temporal"
    Dim nameImg As String = "Default.jpg"
    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem

    Dim tBancos As DataTable = New DataTable
#End Region
#Region "Metodos SobreEscritos"
    Private Sub MostrarMensajeError(mensaje As String)
        ToastNotification.Show(Me,
                               mensaje.ToUpper,
                               My.Resources.WARNING,
                               5000,
                               eToastGlowColor.Red,
                               eToastPosition.TopCenter)

    End Sub
    Private Sub _prTraerBancos(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Try
            Dim dt As New DataTable
            dt = L_prCargarComboBanco()
            With mCombo
                .DropDownList.Columns.Clear()
                .DropDownList.Columns.Add("yccod3").Width = 70
                .DropDownList.Columns("yccod3").Caption = "COD"
                .DropDownList.Columns.Add("ycdes3").Width = 200
                .DropDownList.Columns("ycdes3").Caption = "DESCRIPCION"
                .ValueMember = "ycdes3"
                .DisplayMember = "ycdes3"
                .DataSource = dt
                .Refresh()
            End With
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try

    End Sub
    Private Sub _prCargarBancos()
        Try
            Dim dt As New DataTable
            dt = L_prIntegracionBancos()
            grbanco.DataSource = dt
            grbanco.RetrieveStructure()
            grbanco.AlternatingColors = True

            With grbanco.RootTable.Columns("Id")
                .Width = 100
                .Caption = "Id"
                .Visible = False
            End With
            With grbanco.RootTable.Columns("canumi")
                .Width = 100
                .Caption = "CODIGO"
                .Visible = False

            End With
            With grbanco.RootTable.Columns("img")
                .Width = 100
                .Caption = "Imagen"
                .Visible = False
            End With
            With grbanco.RootTable.Columns("canombre")
                .Width = 300
                .EditType = EditType.MultiColumnDropDown
                .DropDown = cbbanco.DropDownList
                .Visible = True
                .Caption = "Bancos"
            End With
            With grbanco.RootTable.Columns("caimage")
                .Width = 110
                .Visible = False
                .Caption = "TIPO DE CAMBIO"
            End With

            With grbanco.RootTable.Columns("camonto")
                .Width = 150
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = True
                .FormatString = "0.00"
                .Caption = "Monto"
            End With
            With grbanco.RootTable.Columns("caestado")
                .Width = 100
                .Caption = "estado"
                .Visible = False

            End With
            With grbanco.RootTable.Columns("ctanumi")
                .Width = 10
                .Caption = "numibanco"
                .Visible = False
            End With
            With grbanco
                .DefaultFilterRowComparison = FilterConditionOperator.Equal
                .FilterMode = FilterMode.Automatic
                .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
                .GroupByBoxVisible = False
                'diseño de la grilla
                .VisualStyle = VisualStyle.Office2007
            End With

            _prDibujarImagenes()
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try

    End Sub
    Public Sub _prDibujarImagenes()
        Dim length As Integer = CType(grbanco.DataSource, DataTable).Rows.Count
        For i As Integer = 0 To length - 1 Step 1
            Dim nameImagen As String = CType(grbanco.DataSource, DataTable).Rows(i).Item("caimage")
            If (nameImagen.Equals("Default.jpg")) Then
                Dim Bin As New MemoryStream
                Dim img As New Bitmap(My.Resources.imageDefault, 100, 80)
                img.Save(Bin, Imaging.ImageFormat.Jpeg)
                Bin.Dispose()

                CType(grbanco.DataSource, DataTable).Rows(i).Item("img") = Bin.GetBuffer
            Else
                Dim Bin As New MemoryStream
                If (File.Exists(RutaGlobal + "\Imagenes\Imagenes Faubrica" + nameImagen)) Then
                    Dim img As New Bitmap(New Bitmap(RutaGlobal + "\Imagenes\Imagenes Faubrica" + nameImagen), 90, 40)
                    img.Save(Bin, Imaging.ImageFormat.Jpeg)
                    CType(grbanco.DataSource, DataTable).Rows(i).Item("img") = Bin.GetBuffer
                    Bin.Dispose()
                Else

                    Dim img As New Bitmap(My.Resources.imageDefault, 100, 80)
                    img.Save(Bin, Imaging.ImageFormat.Jpeg)
                    CType(grbanco.DataSource, DataTable).Rows(i).Item("img") = Bin.GetBuffer
                End If
            End If


        Next
    End Sub
    Private Sub _prCargarMovimiento()
        Dim dt As New DataTable
        dt = L_prIntegracionGeneralBanco()
        grmovimientos.DataSource = dt
        grmovimientos.RetrieveStructure()
        grmovimientos.AlternatingColors = True
        With grmovimientos.RootTable.Columns("ifnumi")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = True
        End With

        With grmovimientos.RootTable.Columns("ifto001numi")
            .Width = 150
            .Visible = True
            .Caption = "COD COMPROBANTE"
        End With
        With grmovimientos.RootTable.Columns("oanumdoc")
            .Width = 150
            .Visible = True
            .Caption = "NRO DOCUMENTO"
        End With

        With grmovimientos.RootTable.Columns("iftc")
            .Width = 150
            .Visible = True
            .Caption = "TIPO DE CAMBIO"
            .FormatString = "0.00"
        End With
        With grmovimientos.RootTable.Columns("iffechai")
            .Width = 150
            .Visible = True
            .Caption = "FECHA I".ToUpper
            .FormatString = "dd/MM/yyyy"
        End With
        With grmovimientos.RootTable.Columns("iffechaf")
            .Width = 110
            .Visible = False
            .Caption = "FECHA F".ToUpper
            .FormatString = "dd/MM/yyyy"
        End With

        With grmovimientos.RootTable.Columns("ifest")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With

        Dim dtSuc As DataTable
        dtSuc = L_fnListarAlmacenDosificacion()

        With grmovimientos.RootTable.Columns("ifsuc")
            .Width = 200
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Caption = "SUCURSAL"

            .HasValueList = True
            .EditType = EditType.DropDownList
            .ValueList.PopulateValueList(dtSuc.DefaultView, "cod", "desc")
            .CompareTarget = ColumnCompareTarget.Text
            .DefaultGroupInterval = GroupInterval.Text
            .AllowSort = False
            .Visible = False
        End With
        With grmovimientos.RootTable.Columns("ifto001numibanco")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grmovimientos
            .DefaultFilterRowComparison = FilterConditionOperator.Equal
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
        End With

        If (dt.Rows.Count <= 0) Then
            L_prIntegracionDetalle(-1)
        End If
    End Sub

#End Region
#Region "METODOS PRIVADOS"
    Private Sub _IniciarTodo()
        _prCargarComboModulos(cbSucursal)
        MSuperTabControl.SelectedTabIndex = 0
        Me.WindowState = FormWindowState.Maximized
        Me.Text = "INTEGRACION DE BANCOS"
        Dim blah As New Bitmap(New Bitmap(My.Resources.compra), 20, 20)
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico
        btnModificar.Visible = False
        _prAsignarPermisos()
        _prCargarMovimiento()
        _prInhabiliitar()
        _prTraerBancos(cbbanco)
        If grmovimientos.RowCount = 0 Then
            _prCargarBancos()
        End If

    End Sub

    Private Sub _prCargarComboModulos(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnListarPlantillas()
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("cod").Width = 60
            .DropDownList.Columns("cod").Caption = "COD"
            .DropDownList.Columns.Add("desc").Width = 500
            .DropDownList.Columns("desc").Caption = "Modulo"
            .ValueMember = "cod"
            .DisplayMember = "desc"
            .DataSource = dt
            .Refresh()
        End With
        If (gb_userTodasSuc = False And CType(mCombo.DataSource, DataTable).Rows.Count > 0) Then


            mCombo.SelectedIndex = _fnObtenerPosSucursal(gi_userNumiSucursal)
            mCombo.ReadOnly = True
        Else
            mCombo.ReadOnly = False
        End If
    End Sub
    Public Function _fnObtenerPosSucursal(numi As Integer)
        Dim length As Integer = CType(cbSucursal.DataSource, DataTable).Rows.Count - 1
        For i As Integer = 0 To length Step 1
            If (CType(cbSucursal.DataSource, DataTable).Rows(i).Item("cod") = numi) Then
                Return i
            End If
        Next
        Return -1
    End Function
    Public Sub _prAplicarCondiccionJanusBanco()
        Dim fc As GridEXFormatCondition
        fc = New GridEXFormatCondition(grAsientoBanco.RootTable.Columns("tc"), ConditionOperator.Equal, DBNull.Value)

        fc.FormatStyle.FontBold = TriState.True
        fc.FormatStyle.FontSize = 9
        fc.FormatStyle.FontUnderline = TriState.True
        grAsientoBanco.RootTable.FormatConditions.Add(fc)
    End Sub
    Private Sub _prAsignarPermisos()

        Dim dtRolUsu As DataTable = L_prRolDetalleGeneral(gi_userRol, _nameButton)

        Dim show As Boolean = dtRolUsu.Rows(0).Item("ycshow")
        Dim add As Boolean = dtRolUsu.Rows(0).Item("ycadd")
        Dim modif As Boolean = dtRolUsu.Rows(0).Item("ycmod")
        Dim del As Boolean = dtRolUsu.Rows(0).Item("ycdel")

        If add = False Then
            btnNuevo.Visible = False
        End If
        If modif = False Then
            btnModificar.Visible = False
        End If
        If del = False Then
            btnEliminar.Visible = False
        End If
    End Sub
    Private Sub _prInhabiliitar()
        tbNumi.ReadOnly = True
        btActualizar.Visible = False
        tbTipoCambio.IsInputReadOnly = True
        btnModificar.Enabled = True
        btnGrabar.Enabled = False
        btnNuevo.Enabled = True
        btnEliminar.Enabled = True
        PanelNavegacion.Enabled = True
        btnNuevoTipoCambio.Visible = False
        cbSucursal.ReadOnly = True
        tbFechaI.Enabled = False
        tbFechaF.Enabled = False
    End Sub
    Private Sub _prhabilitar()
        cbSucursal.ReadOnly = False
        tbNumi.ReadOnly = True
        tbFechaI.Enabled = True
        tbFechaF.Enabled = True
        btActualizar.Visible = True
        btnNuevoTipoCambio.Visible = True
        tbTipoCambio.IsInputReadOnly = False
        tbNumi.Clear()
    End Sub
    Private Sub F1_ServicioCuentas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _IniciarTodo()
    End Sub
    Public Function _fnVisualizarRegistros() As Boolean
        Return btnGrabar.Enabled = True
    End Function
    Public Function _ValidarCampos() As Boolean
        If (tbTipoCambio.Value <= 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Coloque un Tipo de Cambio Valido".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            tbTipoCambio.Focus()
            Return False

        End If
        If (Lb_Saldo.Text <> 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Inserte Monto Correctos en los bancos", img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            grbanco.Focus()
            Return False

        End If
        Return True
    End Function
    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click

        If _ValidarCampos() = False Then
            Exit Sub
        End If
        If (tbNumi.Text = String.Empty) Then
            _prGuardar()
            'Lb_efec.Text = 0
            'Lb_Saldo.Text = 0
            'Lb_Banco.Text = 0
        End If
    End Sub
    Private Function to3Decimales(num As Double) As Double
        Dim res As Double = 0
        Dim numeroString As String = num.ToString()
        Dim posicionPuntoDecimal As Integer = numeroString.IndexOf(".")

        If posicionPuntoDecimal > 0 Then
            Dim cantidadDecimales As Integer = numeroString.Substring(posicionPuntoDecimal).Count - 1
            If cantidadDecimales >= 3 Then
                numeroString = numeroString.Substring(0, posicionPuntoDecimal + 4)
                res = Convert.ToDouble(numeroString)
            Else
                res = num
            End If

        Else
            res = num
        End If
        Return res
    End Function
    Public Sub _prMostrarRegistro(_N As Integer)
        Try
            grmovimientos.Row = _N
            With grmovimientos
                tbNumi.Text = .GetValue("ifnumi")
                tbFechaI.Value = .GetValue("iffechai")
                tbFechaF.Value = .GetValue("iffechaf")
                tbTipoCambio.Value = .GetValue("iftc")
                cbSucursal.Value = .GetValue("ifto001numibanco")
            End With
            _prCargarBancosRegistrados(tbNumi.Text)
            '_prCargarDetalleMovimiento(tbNumi.Text)
            _prCargarDetalleMovimientoBanco(tbNumi.Text)
            _prMostrarbancos(tbNumi.Text)
            LblPaginacion.Text = Str(grmovimientos.Row + 1) + "/" + grmovimientos.RowCount.ToString
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try

    End Sub
    Public Sub _prArmarDetalleDt(ByRef dt As DataTable)
        Dim tabla As DataTable = dt.Copy
        tabla.Rows.Clear()
        Dim cuenta As Integer = 0
        For i As Integer = 0 To dt.Rows.Count - 1 Step 1
            Dim aux As Integer = dt.Rows(i).Item("canumi")
            If (aux <> cuenta) Then
                Linea = Linea + 1
                Dim dtObtenerCuenta As DataTable = L_prCuentaDiferencia(aux)  ''
                tabla.Rows.Add(dtObtenerCuenta.Rows(0).Item("canumi"), dtObtenerCuenta.Rows(0).Item("cacta"), dtObtenerCuenta.Rows(0).Item("cadesc"), DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, Administracion, 0)

                tabla.Rows.Add(dtObtenerCuenta.Rows(1).Item("canumi"), dtObtenerCuenta.Rows(1).Item("cacta"), dtObtenerCuenta.Rows(1).Item("cadesc"), DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, Administracion, 0)

                Dim debe As Double = IIf(IsDBNull(dt.Rows(i).Item("debe")), 0, dt.Rows(i).Item("debe"))
                Dim haber As Double = IIf(IsDBNull(dt.Rows(i).Item("haber")), 0, dt.Rows(i).Item("haber"))
                Dim debeSus As Double = IIf(IsDBNull(dt.Rows(i).Item("debesus")), 0, dt.Rows(i).Item("debesus"))
                Dim haberSus As Double = IIf(IsDBNull(dt.Rows(i).Item("habersus")), 0, dt.Rows(i).Item("habersus"))
                If (debe = 0 And haber = 0 And debeSus = 0 And haberSus = 0) Then
                    tabla.Rows.Add(dtObtenerCuenta.Rows(1).Item("canumi"), DBNull.Value, dt.Rows(i).Item("cadesc"), DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, 0, 0)

                Else
                    tabla.Rows.Add(dt.Rows(i).Item("canumi"), DBNull.Value, dt.Rows(i).Item("cadesc"), DBNull.Value, DBNull.Value, DBNull.Value, dt.Rows(i).Item("tc"), dt.Rows(i).Item("debe"), dt.Rows(i).Item("haber"), dt.Rows(i).Item("debesus"), dt.Rows(i).Item("habersus"), dt.Rows(i).Item("variable"), Linea)
                End If
            Else
                Linea = Linea + 1
                Dim debe As Integer = IIf(IsDBNull(dt.Rows(i).Item("debe")), 0, dt.Rows(i).Item("debe"))
                Dim haber As Integer = IIf(IsDBNull(dt.Rows(i).Item("haber")), 0, dt.Rows(i).Item("haber"))

                If (debe = 0 And haber = 0) Then
                    tabla.Rows.Add(dt.Rows(i).Item("canumi"), DBNull.Value, dt.Rows(i).Item("cadesc"), DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, 0, 0)

                Else
                    tabla.Rows.Add(dt.Rows(i).Item("canumi"), DBNull.Value, dt.Rows(i).Item("cadesc"), DBNull.Value, DBNull.Value, DBNull.Value, dt.Rows(i).Item("tc"), dt.Rows(i).Item("debe"), dt.Rows(i).Item("haber"), dt.Rows(i).Item("debesus"), dt.Rows(i).Item("habersus"), dt.Rows(i).Item("variable"), Linea)
                End If

            End If
            cuenta = aux
        Next
        dt = tabla
    End Sub
    Private Sub _prCargarDetalleMovimientoBanco(_numi As String)
        Dim dt As New DataTable
        dt = L_prIntegracionDetalleBanco(_numi)
        _prArmarDetalleDt(dt)
        grAsientoBanco.DataSource = dt
        grAsientoBanco.RetrieveStructure()
        grAsientoBanco.AlternatingColors = True
        With grAsientoBanco.RootTable.Columns("canumi")
            .Width = 100

            .Visible = False
        End With
        With grAsientoBanco.RootTable.Columns("variable")
            .Width = 100

            .Visible = False
        End With
        With grAsientoBanco.RootTable.Columns("linea")
            .Width = 100

            .Visible = False
        End With
        With grAsientoBanco.RootTable.Columns("nro")
            .Width = 120
            .Caption = "NRO CUENTA"
            .Visible = True
        End With
        With grAsientoBanco.RootTable.Columns("cadesc")
            .Width = 500
            .Caption = "DESCRIPCION"
            .Visible = True
        End With
        With grAsientoBanco.RootTable.Columns("chporcen")
            .Width = 100

            .Visible = False
        End With
        With grAsientoBanco.RootTable.Columns("chdebe")
            .Width = 180
            .Caption = "DEBE"
            .Visible = False
        End With
        With grAsientoBanco.RootTable.Columns("chhaber")
            .Width = 180
            .Caption = "HABER"
            .Visible = False
        End With
        With grAsientoBanco.RootTable.Columns("tc")
            .Width = 70
            .Caption = "TC"
            .Visible = True
            .FormatString = "0.00"
        End With
        With grAsientoBanco.RootTable.Columns("debe")
            .Width = 100
            .Caption = "DEBE BS"
            .Visible = True
            .TextAlignment = TextAlignment.Far
            .FormatString = "0.00"
            .TotalFormatString = "0.00"
            .AggregateFunction = AggregateFunction.Sum

        End With
        With grAsientoBanco.RootTable.Columns("haber")
            .Width = 100
            .Caption = "HABER BS"
            .Visible = True
            .FormatString = "0.00"
            .TotalFormatString = "0.00"
            .TextAlignment = TextAlignment.Far
            .AggregateFunction = AggregateFunction.Sum

        End With

        With grAsientoBanco.RootTable.Columns("debesus")
            .Width = 100
            .Caption = "DEBE SUS"
            .Visible = True
            .TextAlignment = TextAlignment.Far
            .FormatString = "0.00"
            .TotalFormatString = "0.00"
            .AggregateFunction = AggregateFunction.Sum

        End With
        With grAsientoBanco.RootTable.Columns("habersus")
            .Width = 100
            .Caption = "HABER SUS"
            .Visible = True
            .FormatString = "0.00"
            .TotalFormatString = "0.00"
            .TextAlignment = TextAlignment.Far
            .AggregateFunction = AggregateFunction.Sum

        End With
        With grAsientoBanco
            .TotalRowFormatStyle.BackColor = Color.Gold
            .TotalRowPosition = TotalRowPosition.BottomFixed
            .TotalRow = InheritableBoolean.True

            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
        End With

        _prAplicarCondiccionJanusBanco()
    End Sub
    Private Sub _prCargarBancosRegistrados(ifnumi As String)
        Dim dt As New DataTable
        dt = L_prIntegracionBancosRegistrados(ifnumi)
        grbanco.DataSource = dt
        grbanco.RetrieveStructure()
        grbanco.AlternatingColors = True

        With grbanco.RootTable.Columns("Id")
            .Width = 100
            .Caption = "Id"
            .Visible = False

        End With
        With grbanco.RootTable.Columns("canumi")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = False

        End With
        With grbanco.RootTable.Columns("img")
            .Width = 100
            .Caption = "Imagen"
            .Visible = False

        End With
        With grbanco.RootTable.Columns("canombre")
            .Width = 300
            .EditType = EditType.MultiColumnDropDown
            .DropDown = cbbanco.DropDownList
            .Visible = True
            .Caption = "Bancos"
        End With
        With grbanco.RootTable.Columns("caimage")
            .Width = 110
            .Visible = False
            .Caption = "TIPO DE CAMBIO"
        End With

        With grbanco.RootTable.Columns("camonto")
            .Width = 90
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Monto"

        End With
        With grbanco.RootTable.Columns("caestado")
            .Width = 100
            .Caption = "estado"
            .Visible = False

        End With
        With grbanco.RootTable.Columns("ctanumi")
            .Width = 100
            .Caption = "ctanumi"
            .Visible = False

        End With
        With grbanco
            .DefaultFilterRowComparison = FilterConditionOperator.Equal
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
        End With

        _prDibujarImagenes()
    End Sub
    Private Sub _prMostrarbancos(_Numi As String)
        Dim dt As New DataTable
        'Dim fila As DataRow
        dt = L_prIntegracionBanco(_Numi)
        Lb_Banco.Text = 0
        Lb_efec.Text = 0
        Lb_Saldo.Text = 0
        If dt.Rows.Count > 0 Then
            For Each fila As DataRow In dt.Rows
                Lb_Banco.Text = Lb_Banco.Text + fila.Item("ikmonto")
            Next
            Lb_efec.Text = Lb_Banco.Text
        End If
    End Sub
    Private Sub _prSalir()
        If btnGrabar.Enabled = False Then
            _prInhabiliitar()
            If grmovimientos.RowCount > 0 Then
                _prMostrarRegistro(0)
            End If
        Else
            _modulo.Select()
            _tab.Close()
        End If
    End Sub
    Sub _prLimpiar()
        Lb_efec.Text = 0
        Lb_Saldo.Text = 0
        Lb_Banco.Text = 0
        tbTipoCambio.Value = 0
        tbFechaI.Value = Now.Date
        tbFechaF.Value = Now.Date
        If (Not IsDBNull(grAsientoBanco)) Then
            If (grAsientoBanco.RowCount > 0) Then
                CType(grAsientoBanco.DataSource, DataTable).Rows.Clear()
            End If
        End If
        If (Not IsDBNull(grbanco)) Then
            If (grbanco.RowCount > 0) Then
                CType(grbanco.DataSource, DataTable).Rows.Clear()
            End If
        End If
        _prAddFilaBanco()
    End Sub


    Private Sub _prAñadirTipoCambio()
        Dim dtRolUsu As DataTable = L_prRolDetalleGeneral(gi_userRol, "btConfTipoCambio")

        Dim add As Boolean = dtRolUsu.Rows(0).Item("ycadd")

        If add = True Then
            Dim frm As New F0_TipoCambio_Nuevo
            frm.tbFecha.Value = tbFechaI.Value
            frm.ShowDialog()
            tbFechaI.Value = DateAdd(DateInterval.Day, -1, tbFechaI.Value)
            tbFechaI.Value = DateAdd(DateInterval.Day, 1, tbFechaI.Value)
        Else
            ToastNotification.Show(Me, "el usario no cuenta con los permisos para adicionar tipo de cambio".ToUpper, My.Resources.WARNING, 3000, eToastGlowColor.Blue, eToastPosition.TopCenter)
        End If

    End Sub
    Private Sub _prImprimirBanco()
        Dim objrep As New R_ComprobanteIntegracion
        Dim dt As New DataTable
        dt = CType(grAsientoBanco.DataSource, DataTable)
        'ahora lo mando al visualizador
        P_Global.Visualizador = New Visualizador
        objrep.SetDataSource(dt)
        objrep.SetParameterValue("fecha", tbFechaI.Value.ToString("dd/MM/yyyy"))
        objrep.SetParameterValue("tc", tbTipoCambio.Value)
        objrep.SetParameterValue("titulo", "COMPROBANTE DE TRASPASO")
        objrep.SetParameterValue("titulo2", "CFDISTRIBUCIÓN S.R.L.")
        objrep.SetParameterValue("Direccion", gs_empresaDireccion)
        objrep.SetParameterValue("glosa", "COMPROBANTE POR TRASPASO A BANCO")
        objrep.SetParameterValue("autor", "")
        'cargar el numero de comprobante
        Dim dtNum As DataTable = L_prObtenerNumFacturaGeneral(1, tbFechaI.Value.Year, tbFechaI.Value.Month, 1)
        If dtNum.Rows.Count > 0 Then
            objrep.SetParameterValue("numero", dtNum.Rows(0).Item("oanumdoc").ToString)
        Else
            objrep.SetParameterValue("numero", "")

        End If
        objrep.SetParameterValue("nit", gs_empresaNit.ToUpper)
        P_Global.Visualizador.CRV1.ReportSource = objrep 'Comentar
        P_Global.Visualizador.Show() 'Comentar
        P_Global.Visualizador.BringToFront() 'Comentar
    End Sub

    Public Sub _PrimerRegistro()
        Dim _MPos As Integer
        If grmovimientos.RowCount > 0 Then
            _MPos = 0
            grmovimientos.Row = _MPos
        End If
    End Sub


    Public Sub _prEliminarFila()
        If (grbanco.Row >= 0) Then
            If (grbanco.RowCount >= 2) Then
                Dim estado As Integer = grbanco.GetValue("caestado")
                Dim pos As Integer = -1
                Dim lin As Integer = grbanco.GetValue("Id")
                _fnObtenerFilaDetalle(pos, lin)
                If (estado = 0) Then
                    CType(grbanco.DataSource, DataTable).Rows(pos).Item("caestado") = -2

                End If
                If (estado = 1) Then
                    CType(grbanco.DataSource, DataTable).Rows(pos).Item("caestado") = -1
                End If
                grbanco.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(grbanco.RootTable.Columns("caestado"), Janus.Windows.GridEX.ConditionOperator.GreaterThanOrEqualTo, 0))

                grbanco.Select()
                grbanco.Col = 4
                grbanco.Row = grbanco.RowCount - 1
                Pr_sumarbanco()
            End If
        End If
    End Sub
    Public Sub _fnObtenerFilaDetalle(ByRef pos As Integer, numi As Integer)
        For i As Integer = 0 To CType(grbanco.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _numi As Integer = CType(grbanco.DataSource, DataTable).Rows(i).Item("Id")
            If (_numi = numi) Then
                pos = i
                Return
            End If
        Next

    End Sub
    Private Sub _prAddFilaBanco()
        Try
            Dim Bin As New MemoryStream
            Dim img As New Bitmap(My.Resources.imageDefault, 28, 28)
            img.Save(Bin, Imaging.ImageFormat.Png)
            cbbanco.SelectedIndex = 0
            CType(grbanco.DataSource, DataTable).Rows.Add(_fnSiguienteNumi() + 1, 0, Bin.GetBuffer, cbbanco.Text, "Default.jpg", 0, 0, 0)
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try

    End Sub
    Public Function _fnSiguienteNumi()
        Dim dt As DataTable = CType(grbanco.DataSource, DataTable)
        Dim mayor As Integer = 0
        For i As Integer = 0 To dt.Rows.Count - 1 Step 1
            Dim data As Integer = IIf(IsDBNull(CType(grbanco.DataSource, DataTable).Rows(i).Item("Id")), 0, CType(grbanco.DataSource, DataTable).Rows(i).Item("Id"))
            If (data > mayor) Then
                mayor = data
            End If
        Next
        Return mayor
    End Function
    Private Sub _prCargarCodigoBanco()
        Try
            Dim tGrBancos = L_prCargarBanco()
            tBancos = CType(grbanco.DataSource, DataTable)
            For Each rBanco As DataRow In tBancos.Rows
                For Each rgrBanco As DataRow In tGrBancos.Rows
                    If rBanco.Item("canombre") = rgrBanco.Item("canombre") Then
                        rBanco.Item("canumi") = rgrBanco.Item("canumi")
                        rBanco.Item("ctanumi") = rgrBanco.Item("ctanumi")
                    End If
                Next
            Next
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub
    Private Sub Pr_sumarbanco()
        Try
            'Dim dt As DataTable
            'dt = grbanco.DataSource
            grbanco.UpdateData()
            Lb_Banco.Text = grbanco.GetTotal(grbanco.RootTable.Columns("camonto"), AggregateFunction.Sum)
            'Lb_Banco.Text = 0
            'Dim i As Integer
            'For i = 0 To dt.Rows.Count - 1
            '    Lb_Banco.Text = Lb_Banco.Text + dt.Rows(i).Item("camonto")
            'Next
            Lb_Saldo.Text = Lb_efec.Text - Lb_Banco.Text
            If Lb_Saldo.Text <> 0 Then
                Lb_Saldo.ForeColor = Color.Red
                LabelX7.ForeColor = Color.Red
                btnGrabar.Enabled = False
            Else
                Lb_Saldo.ForeColor = Color.Black
                LabelX7.ForeColor = Color.Black
                btnGrabar.Enabled = True
            End If

        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub

#End Region

#Region "Eventos"
    Private Sub _prGuardar()
        Try
            'tBancos.Rows.Add()
            Dim numiComprobante As String = ""
            _prCargarCodigoBanco()
            Dim res As Boolean = L_prComprobanteGrabarIntegracionBanco(numiComprobante, "", tbFechaI.Value.Year.ToString, tbFechaI.Value.Month.ToString, "", tbFechaI.Value.Date.ToString("yyyy/MM/dd"),
                                                                       tbTipoCambio.Value.ToString, gi_empresaNumi, tBancos, "", 0, tbTipoCambio.Value, tbFechaI.Value.ToString("yyyy/MM/dd"),
                                                                       tbFechaF.Value.ToString("yyyy/MM/dd"), 1, tbFechaI.Value.ToString("yyyy/MM/dd"),
                                                                       tbFechaF.Value.ToString("yyyy/MM/dd"))
            If res Then
                Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
                ToastNotification.Show(Me, "El Asiento Contable fue generado Exitosamente".ToUpper,
                                          img, 2000,
                                          eToastGlowColor.Green,
                                          eToastPosition.TopCenter
                                          )
                Filtrar(2)
                _prInhabiliitar()
                _prImprimirBanco()
            Else
                Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                ToastNotification.Show(Me, "Los codigos no pudieron ser modificados".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try

    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        btnNuevo.Enabled = False
        btnModificar.Enabled = False
        btnGrabar.Enabled = True
        btnEliminar.Enabled = False

    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _modulo.Select()
        _tab.Close()
    End Sub
    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        _prhabilitar()
        _prLimpiar()
        btnNuevo.Enabled = False
        btnModificar.Enabled = False
        btnGrabar.Enabled = True
        btnEliminar.Enabled = False
        Dim dtTipoCambio As DataTable = L_prTipoCambioGeneralPorFecha(Now.ToString("yyyy/MM/dd"))
        If dtTipoCambio.Rows.Count = 0 Then
            _existTipoCambio = False
            tbTipoCambio.Value = 0
            tbTipoCambio.BackgroundStyle.BackColor = Color.Red
            btnNuevoTipoCambio.Visible = True
        Else
            _existTipoCambio = True
            tbTipoCambio.Value = dtTipoCambio.Rows(0).Item("cbdol")
            tbTipoCambio.BackgroundStyle.BackColor = Color.White
            MEP.SetError(tbTipoCambio, "")
            btnNuevoTipoCambio.Visible = False
        End If
        If (gb_userTodasSuc = False And CType(cbSucursal.DataSource, DataTable).Rows.Count > 0) Then
            cbSucursal.SelectedIndex = _fnObtenerPosSucursal(gi_userNumiSucursal)
            cbSucursal.ReadOnly = True
        Else
            cbSucursal.ReadOnly = False
        End If
    End Sub

    Private Sub btActualizar_Click(sender As Object, e As EventArgs) Handles btActualizar.Click
        Try
            If (IsNothing(tbTipoCambio.Value) Or tbTipoCambio.ToString = String.Empty Or tbTipoCambio.Value = 0) Then
                Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
                ToastNotification.Show(Me, "NO HAY TIPO DE CAMBIO REGISTRADO. POR FAVOR REGISTRE EL TIPO DE CAMBIO".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                cbSucursal.Focus()
                Return
            End If
            'If (cbSucursal.SelectedIndex < 0) Then
            '    Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
            '    ToastNotification.Show(Me, "POR FAVOR SELECCIONE UN MODULO".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            '    cbSucursal.Focus()
            '    Return
            'End If
            If L_fnExisteComprobanteBanco(tbFechaI.Value, 2) Then
                Throw New Exception("EXISTE UN INTEGRACIÓN CON LA FECHA ESPECÍFICADA")
            End If
            Dim dtTotales = L_prObtenerTotalesContadoIntegracionBanco(tbFechaI.Value.ToString("yyyy/MM/dd"))
            Dim totalGeneral As Double = 0
            For Each Cantidad As DataRow In dtTotales.Rows
                totalGeneral = totalGeneral + Cantidad.Item("TotalEfectivo")
            Next
            If (totalGeneral > 0) Then
                Lb_efec.Text = totalGeneral.ToString
            Else
                Lb_efec.Text = 0
                Throw New Exception("NO SE ENCONTRO REGISTROS CON LA FECHA ESPECÍFICADA")
            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try

    End Sub
    Private Sub grbanco_EditingCell(sender As Object, e As EditingCellEventArgs) Handles grbanco.EditingCell
        Try
            If tbFechaI.Enabled = True Then
                If (e.Column.Index = grbanco.RootTable.Columns("canombre").Index Or
                    e.Column.Index = grbanco.RootTable.Columns("camonto").Index) Then
                    e.Cancel = False
                Else
                    e.Cancel = True
                End If
            Else
                e.Cancel = True
            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub
    Private Sub btnNuevoTipoCambio_Click(sender As Object, e As EventArgs)
        _prAñadirTipoCambio()
    End Sub
    Private Sub grmovimientos_SelectionChanged(sender As Object, e As EventArgs) Handles grmovimientos.SelectionChanged
        If (grmovimientos.RowCount >= 0 And grmovimientos.Row >= 0) Then

            _prMostrarRegistro(grmovimientos.Row)
        End If
    
    End Sub

    Private Sub btnSiguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click
        Dim _pos As Integer = grmovimientos.Row
        If _pos < grmovimientos.RowCount - 1 Then
            _pos = grmovimientos.Row + 1
            grmovimientos.Row = _pos
        End If
    End Sub

    Private Sub btnPrimero_Click(sender As Object, e As EventArgs) Handles btnPrimero.Click
        _PrimerRegistro()
    End Sub

    Private Sub btnUltimo_Click(sender As Object, e As EventArgs) Handles btnUltimo.Click
        Dim _pos As Integer = grmovimientos.Row
        If grmovimientos.RowCount > 0 Then
            _pos = grmovimientos.RowCount - 1
            grmovimientos.Row = _pos
        End If
    End Sub

    Private Sub btnAnterior_Click(sender As Object, e As EventArgs) Handles btnAnterior.Click
        Dim _MPos As Integer = grmovimientos.Row
        If _MPos > 0 And grmovimientos.RowCount > 0 Then
            _MPos = _MPos - 1
            grmovimientos.Row = _MPos
        End If
    End Sub

    Private Sub btnNuevoTipoCambio_Click_1(sender As Object, e As EventArgs) Handles btnNuevoTipoCambio.Click
        _prAñadirTipoCambio()
    End Sub

    Private Sub tbFechaI_ValueChanged(sender As Object, e As EventArgs) Handles tbFechaI.ValueChanged
        tbFechaF.Value = tbFechaI.Value
        'verifico el tipo de cambio de la fecha elegida
        Dim dtTipoCambio As DataTable = L_prTipoCambioGeneralPorFecha(tbFechaI.Value.ToString("yyyy/MM/dd"))
        If dtTipoCambio.Rows.Count = 0 Then
            '_existTipoCambio = False
            tbTipoCambio.Value = Nothing
            tbTipoCambio.Text = ""
            tbTipoCambio.BackgroundStyle.BackColor = Color.Red
            btnNuevoTipoCambio.Visible = True
        Else
            '_existTipoCambio = True
            tbTipoCambio.Value = dtTipoCambio.Rows(0).Item("cbdol")
            tbTipoCambio.BackgroundStyle.BackColor = Color.White
            btnNuevoTipoCambio.Visible = False
            MEP.SetError(tbTipoCambio, "")
        End If
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        Dim ef = New Efecto
        ef.tipo = 2
        ef.Context = "¿esta seguro de eliminar el asiento contable?".ToUpper
        ef.Header = "mensaje principal".ToUpper
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        If (bandera = True) Then
            Dim mensajeError As String = ""
            Dim res As Boolean = L_fnEliminarAsientoContableBanco(tbNumi.Text, mensajeError)
            If res Then
                Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
                ToastNotification.Show(Me, "Código de Venta ".ToUpper + tbNumi.Text + " eliminado con Exito.".ToUpper,
                                          img, 2000,
                                          eToastGlowColor.Green,
                                          eToastPosition.TopCenter)

                Filtrar(1)
                _prInhabiliitar()
            Else
                Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                ToastNotification.Show(Me, mensajeError, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            End If
        End If

    End Sub
    Private Sub Filtrar(tipo As Integer)
        _prCargarMovimiento()
        If grmovimientos.RowCount > 0 Then
            _MPos = 0
            _prMostrarRegistro(IIf(tipo = 1, _MPos, grmovimientos.RowCount - 1))
        Else
            _prLimpiar()
            LblPaginacion.Text = "0/0"
        End If
    End Sub
    Private Sub grbanco_KeyDown(sender As Object, e As KeyEventArgs) Handles grbanco.KeyDown
        Try
            If (tbFechaI.Enabled) Then
                If e.KeyData = Keys.Enter Then
                    If (grbanco.Col = grbanco.RootTable.Columns("canombre").Index) Then
                        grbanco.UpdateData()
                        ListaDeposito = grbanco.DataSource
                        _prAddFilaBanco()
                        '_prCargarBancos()
                    End If
                End If
                If e.KeyData = Keys.Escape Then
                    If grbanco.RowCount > 0 Then
                        _prEliminarFila()
                    Else
                        Throw New Exception("Detalle de deposito no puede estar vacio")
                    End If
                End If
            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub

    Private Sub btnbanco_Click(sender As Object, e As EventArgs) Handles btnbanco.Click
        _prImprimirBanco()
    End Sub

    Private Sub grbanco_CellValueChanged(sender As Object, e As ColumnActionEventArgs) Handles grbanco.CellValueChanged

    End Sub

    Private Sub grbanco_CellEdited(sender As Object, e As ColumnActionEventArgs) Handles grbanco.CellEdited
        Pr_sumarbanco()
    End Sub
#End Region


End Class