Option Strict Off
Imports Logica.AccesoLogica
Imports Janus.Windows.GridEX
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Imports System.Math
Public Class F1_AsientosContables2
#Region "VARIABLES"
    Dim Modificado As Boolean = False
    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem
    Dim NumiCertificacion As Integer = 6
    Public _LisTransacciones As DataTable
    Dim NumiAdministracion As Integer = 7
    Dim conRedondeo As Boolean = False
    Dim dtTC009 As DataTable = New DataTable
    Dim dtTO00111 As DataTable
#End Region
#Region "METODOS"
    Private Sub _IniciarTodo()
        Try
            _prCargarComboModulos(cbPlantilla)
            MSuperTabControl.SelectedTabIndex = 0
            Me.WindowState = FormWindowState.Maximized
            Me.Text = "COMPROBANTES DE SERVICIOS"
            Dim blah As New Bitmap(New Bitmap(My.Resources.compra), 20, 20)
            Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
            Me.Icon = ico
            _prAsignarPermisos()
            _prCargarMovimiento()
            _prInhabiliitar()
            cbPlantilla.Value = 3
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub
    Private Sub _prCargarComboModulos(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Try
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
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub
    Public Function _fnObtenerPosSucursal(numi As Integer)
        Try
            Dim length As Integer = CType(cbPlantilla.DataSource, DataTable).Rows.Count - 1
            For i As Integer = 0 To length Step 1
                If (CType(cbPlantilla.DataSource, DataTable).Rows(i).Item("cod") = numi) Then
                    Return i
                End If
            Next
            Return -1
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Function
    Private Sub _prAsignarPermisos()
        Try
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
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub
    Private Sub _prCargarMovimiento()
        Try
            Dim dt As New DataTable
            dt = L_prIntegracionGeneral()
            Dgv_Buscador.DataSource = dt
            Dgv_Buscador.RetrieveStructure()
            Dgv_Buscador.AlternatingColors = True

            'a.ifnumi ,a.ifto001numi,comprobante .oanumdoc  ,a.iftc ,a.iffechai ,a.iffechaf ,a.ifest 
            With Dgv_Buscador.RootTable.Columns("ifnumi")
                .Width = 100
                .Caption = "CODIGO"
                .Visible = True

            End With

            With Dgv_Buscador.RootTable.Columns("ifto001numi")
                .Width = 100
                .Visible = True
                .Caption = "COD COMPROBANTE"
            End With
            With Dgv_Buscador.RootTable.Columns("oanumdoc")
                .Width = 110
                .Visible = True
                .Caption = "NRO DOCUMENTO"
            End With

            With Dgv_Buscador.RootTable.Columns("iftc")
                .Width = 110
                .Visible = True
                .Caption = "TIPO DE CAMBIO"
            End With
            With Dgv_Buscador.RootTable.Columns("iffechai")
                .Width = 110
                .Visible = True
                .Caption = "FECHA I".ToUpper
                .FormatString = "dd/MM/yyyy"
            End With
            With Dgv_Buscador.RootTable.Columns("iffechaf")
                .Width = 110
                .Visible = True
                .Caption = "FECHA F".ToUpper
                .FormatString = "dd/MM/yyyy"
            End With

            With Dgv_Buscador.RootTable.Columns("ifest")
                .Width = 50
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = False
            End With

            Dim dtSuc As DataTable
            dtSuc = L_fnListarAlmacenDosificacion()

            With Dgv_Buscador.RootTable.Columns("ifsuc")
                .Width = 200
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Caption = "SUCURSAL"

                .HasValueList = True
                .EditType = EditType.DropDownList
                .ValueList.PopulateValueList(dtSuc.DefaultView, "cod", "desc")
                .CompareTarget = ColumnCompareTarget.Text
                .DefaultGroupInterval = GroupInterval.Text
                .AllowSort = False

            End With
            With Dgv_Buscador
                .DefaultFilterRowComparison = FilterConditionOperator.Equal
                .FilterMode = FilterMode.Automatic
                .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
                .GroupByBoxVisible = False
                .VisualStyle = VisualStyle.Office2007
            End With

            If (dt.Rows.Count <= 0) Then
                L_prIntegracionDetalle(-1)
            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try

    End Sub
    Private Sub _prInhabiliitar()
        Try
            tbNumi.ReadOnly = True
            btActualizar.Visible = False
            tbTipoCambio.IsInputReadOnly = True
            btnModificar.Enabled = True
            btnGrabar.Enabled = False
            btnNuevo.Enabled = True
            btnEliminar.Enabled = True
            PanelNavegacion.Enabled = True
            btnNuevoTipoCambio.Visible = False
            cbPlantilla.ReadOnly = True
            conRedondeo = False
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try

    End Sub
    Private Sub _prhabilitar()
        Try
            cbPlantilla.ReadOnly = False
            tbNumi.ReadOnly = True
            btActualizar.Visible = True
            btnNuevoTipoCambio.Visible = True
            tbTipoCambio.IsInputReadOnly = False
            tbNumi.Clear()
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try

    End Sub
    Sub _prLimpiar()
        Try
            conRedondeo = False
            tbTipoCambio.Value = 0
            tbFechaI.Value = Now.Date
            If (Not IsDBNull(Dgv_Detalle)) Then
                If (Dgv_Detalle.RowCount > 0) Then
                    CType(Dgv_Detalle.DataSource, DataTable).Rows.Clear()
                End If

            End If
            _prCrearColumns()
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try

    End Sub
    Sub _prCrearColumns()
        Try
            If (Not IsNothing(_LisTransacciones)) Then
                _LisTransacciones.Columns.Clear()
            End If
            _LisTransacciones = New DataTable
            _LisTransacciones.Columns.Add("id", Type.GetType("System.Int32"))
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try


    End Sub
    Private Sub MostrarMensajeError(mensaje As String)
        ToastNotification.Show(Me,
                               mensaje.ToUpper,
                               My.Resources.WARNING,
                               5000,
                               eToastGlowColor.Red,
                               eToastPosition.TopCenter)

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
    Public Function _fnBuscarIDTC009(ci As String, _dt As DataTable) As Integer
        For i As Integer = 0 To _dt.Rows.Count - 1 Step 1
            If (ci.Equals(_dt.Rows(i).Item("cjci"))) Then
                Return _dt.Rows(i).Item("cjnumi")
            End If

        Next
        Return 0

    End Function

    Private Sub _prCargarTablaComprobantes()
        Dim k As Integer
        Dim dt As New DataTable
        dt = L_prServicioListarCuentas(cbPlantilla.Value)  ''Ok
        Dim tabla As DataTable = dt.Copy
        tabla.Rows.Clear()
        Dim BanderaCuentaPorCobrar As Boolean = False
        Dim TotalTransaccion As Double
        For i As Integer = 0 To dt.Rows.Count - 1
            Dim dtDetalle As DataTable = L_prObtenerDetallePlantilla(dt.Rows(i).Item("canumi"), cbPlantilla.Value)
            Dim tipo As Integer = dtDetalle.Rows(0).Item("tipo")
            'Dim dtTotales = L_prObtenerTotalesTransaccionCaja(tbFechaI.Value.ToString("yyyy/MM/dd"))
            'If (dtTotales.Rows.Count > 0) Then
            '    TotalTransaccion = IIf(IsDBNull(dtTotales.Rows(0).Item("TotalConciliacion")), 0, dtTotales.Rows(0).Item("TotalConciliacion"))
            'Else
            '    TotalTransaccion = 0
            'End If
            If (tipo = 1 Or tipo = 0 Or tipo = 2 Or tipo = 4 Or tipo = 5) Then ''''Venta Contado
                If (cbPlantilla.Value = 1 Or cbPlantilla.Value = 2) Then
                    TotalTransaccion = ObtenerTotales()
                Else
                    TotalTransaccion = ObtenerTotalVentasCreditoOContado(tipo)
                End If

            Else
                TotalTransaccion = ObtenerTotales()
            End If

            ''canumi , nro,cadesc ,chporcen,chdebe ,chhaber 
            Dim porcentaje As Double = dt.Rows(i).Item("chporcen")

            Dim numiCuenta As Integer = dt.Rows(i).Item("canumi")

            Dim numicuentaatc As Integer = 21 'ATC
            Dim diferencia As Double = 0
            diferencia = IIf(tipo = 5, TotalTransaccion, 0)

            Dim total As Double = TotalTransaccion
            If (total <> 0) Then
                Dim dtObtenerCuenta As DataTable = L_prCuentaDiferencia(numiCuenta)
                tabla.Rows.Add(dtObtenerCuenta.Rows(0).Item("canumi"), dtObtenerCuenta.Rows(0).Item("cacta"), dtObtenerCuenta.Rows(0).Item("cadesc"), DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, Escuela, 0) '''' Aqui agrego al padre
                tabla.ImportRow(dt.Rows(i))  '''Aqui agrego al hijo
                If (tipo = 0) Then

                    If (cbPlantilla.Value = 3) Then  '' Si es Cuenta de Credito y Ademas estamos en ventas a credito desglosamos los clientes
                        Dim dtListado As DataTable = L_prListarClienteCredito2(tbFechaI.Value.ToString("dd/MM/yyyy"), tbFechaI.Value.ToString("dd/MM/yyyy"))
                        dtTC009 = L_prListarProveedorCreditoParaTC009ParaVentas(tbFechaI.Value.ToString("dd/MM/yyyy"), tbFechaI.Value.ToString("dd/MM/yyyy"))
                        '''''''Relleno el numi de la cuenta en la TC009
                        For p As Integer = 0 To dtTC009.Rows.Count - 1 Step 1
                            dtTC009.Rows(p).Item("cjnumiTc001") = numiCuenta
                        Next
                        '''''''''''Insertamos a la TC009'''''''''''''''
                        Dim _dtTC009Inserted = L_prInsertarTC009(dtTC009)

                        '''''''''''''''''''''''''''''''''''''''''''''''

                        For j As Integer = 0 To dtListado.Rows.Count - 1 Step 1
                            Dim Glosa As String = dt.Rows(i).Item("cadesc")

                            Dim conversion As Double = dtListado.Rows(j).Item("monto")
                            conversion = to3Decimales(conversion)
                            Dim totales As Double = Round(conversion, 2)
                            Dim TotalSus As Double = Round(to3Decimales(totales / (tbTipoCambio.Value)), 2)
                            Linea = Linea + 1

                            ''''''''''''''''''
                            Dim IdTC009 As Integer = _fnBuscarIDTC009(dtListado.Rows(j).Item("ccnumi"), _dtTC009Inserted)
                            dtTO00111.Rows.Add(0, Linea, IdTC009, 1)
                            '''''''''''''''''''''''''
                            If (dt.Rows(i).Item("chdebe") > 0) Then
                                tabla.Rows.Add(numiCuenta, DBNull.Value, dtListado.Rows(j).Item("cliente") + " con Factura Nro #" + dtListado.Rows(j).Item("nroDocumento"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, totales, DBNull.Value, TotalSus, DBNull.Value, Escuela, Linea)
                            Else
                                tabla.Rows.Add(numiCuenta, DBNull.Value, dtListado.Rows(j).Item("cliente") + " con Factura Nro #" + dtListado.Rows(j).Item("nroDocumento"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, DBNull.Value, totales, DBNull.Value, TotalSus, Escuela, Linea)
                            End If
                        Next
                    End If

                Else
                    If tipo = 4 Then
                        Dim totalBanco = L_prObtenerTotalesBanco(tbFechaI.Value.ToString("yyyy/MM/dd"))
                        For Each totalB As DataRow In totalBanco.Rows
                            Dim Glosa As String = dt.Rows(i).Item("cadesc")
                            Dim conversion As Double = (totalB.Item("TotalDeposito") * (porcentaje / 100))
                            conversion = to3Decimales(conversion)
                            Dim totales As Double = Round(conversion, 2)
                            Dim TotalSus As Double = Round(to3Decimales(totales / (tbTipoCambio.Value)), 2)
                            Linea = Linea + 1
                            If (dt.Rows(i).Item("chdebe") > 0) Then
                                tabla.Rows.Add(numiCuenta, DBNull.Value, Glosa + " DEL " + tbFechaI.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, totales, DBNull.Value, TotalSus, DBNull.Value, Escuela, Linea)
                            Else
                                tabla.Rows.Add(numiCuenta, DBNull.Value, Glosa + " DEL " + tbFechaI.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, DBNull.Value, totales, DBNull.Value, TotalSus, Escuela, Linea)
                            End If
                        Next
                    Else

                        Dim Glosa As String = dt.Rows(i).Item("cadesc")
                        Dim conversion As Double = (total * (porcentaje / 100))
                        conversion = to3Decimales(conversion)
                        Dim totales As Double = Round(conversion, 2)
                        Dim TotalSus As Double = Round(to3Decimales(totales / (tbTipoCambio.Value)), 2)
                        Linea = Linea + 1

                        If tipo = 5 Then ' Existencia de diferencia
                            If (diferencia < 0) Then
                                diferencia = diferencia * -1
                                tabla.Rows.Add(numiCuenta, DBNull.Value, Glosa + " DEL " + tbFechaI.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, totales * -1, DBNull.Value, TotalSus * -1, DBNull.Value, Escuela, Linea)
                            Else
                                tabla.Rows.Add(numiCuenta, DBNull.Value, Glosa + " DEL " + tbFechaI.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, DBNull.Value, totales, DBNull.Value, TotalSus, Escuela, Linea)
                            End If
                        Else
                            If (dt.Rows(i).Item("chdebe") > 0) Then
                                tabla.Rows.Add(numiCuenta, DBNull.Value, Glosa + " DEL " + tbFechaI.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, totales, DBNull.Value, TotalSus, DBNull.Value, Escuela, Linea)
                            Else
                                tabla.Rows.Add(numiCuenta, DBNull.Value, Glosa + " DEL " + tbFechaI.Value.ToString("dd/MM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, tbTipoCambio.Value, DBNull.Value, totales, DBNull.Value, TotalSus, Escuela, Linea)
                            End If
                        End If
                    End If
                End If
            End If
            'End If
        Next
        If tabla.Rows.Count = 0 Then
            Throw New Exception("No se encontraron registros")
        End If
        _prArmarCuadre(tabla)

        ''canumi , nro, cadesc, chporcen, chdebe, chhaber 
        Dgv_Detalle.DataSource = tabla
        Dgv_Detalle.RetrieveStructure()


        Dim dtt As DataTable = _LisTransacciones

        With Dgv_Detalle.RootTable.Columns("canumi")
            .Width = 100

            .Visible = False
        End With
        With Dgv_Detalle.RootTable.Columns("variable")
            .Width = 100

            .Visible = False
        End With
        With Dgv_Detalle.RootTable.Columns("linea")
            .Width = 100

            .Visible = False
        End With
        With Dgv_Detalle.RootTable.Columns("nro")
            .Width = 120
            .Caption = "NRO CUENTA"
            .Visible = True
        End With
        With Dgv_Detalle.RootTable.Columns("cadesc")
            .Width = 580
            .Caption = "DESCRIPCION"
            .Visible = True
        End With
        With Dgv_Detalle.RootTable.Columns("chporcen")
            .Width = 100

            .Visible = False
        End With
        With Dgv_Detalle.RootTable.Columns("chdebe")
            .Width = 180
            .Caption = "DEBE"
            .Visible = False
        End With
        With Dgv_Detalle.RootTable.Columns("chhaber")
            .Width = 180
            .Caption = "HABER"
            .Visible = False
        End With
        With Dgv_Detalle.RootTable.Columns("tc")
            .Width = 70
            .Caption = "TC"
            .Visible = True
            .FormatString = "0.00"
        End With
        With Dgv_Detalle.RootTable.Columns("debe")
            .Width = 100
            .Caption = "DEBE BS"
            .Visible = True
            .TextAlignment = TextAlignment.Far
            .FormatString = "0.00"
            .TotalFormatString = "0.00"
            .AggregateFunction = AggregateFunction.Sum

        End With
        With Dgv_Detalle.RootTable.Columns("haber")
            .Width = 100
            .Caption = "HABER BS"
            .Visible = True
            .FormatString = "0.00"
            .TotalFormatString = "0.00"
            .TextAlignment = TextAlignment.Far
            .AggregateFunction = AggregateFunction.Sum

        End With

        With Dgv_Detalle.RootTable.Columns("debesus")
            .Width = 100
            .Caption = "DEBE SUS"
            .Visible = True
            .TextAlignment = TextAlignment.Far
            .FormatString = "0.00"
            .TotalFormatString = "0.00"
            .AggregateFunction = AggregateFunction.Sum

        End With
        With Dgv_Detalle.RootTable.Columns("habersus")
            .Width = 100
            .Caption = "HABER SUS"
            .Visible = True
            .FormatString = "0.00"
            .TotalFormatString = "0.00"
            .TextAlignment = TextAlignment.Far
            .AggregateFunction = AggregateFunction.Sum

        End With
        With Dgv_Detalle
            .TotalRowFormatStyle.BackColor = Color.Gold
            .TotalRowPosition = TotalRowPosition.BottomFixed
            .TotalRow = InheritableBoolean.True

            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
        End With
        Dim aux As DataTable = CType(Dgv_Detalle.DataSource, DataTable)
        _prAplicarCondiccionJanus()
    End Sub
    Public Sub _prAplicarCondiccionJanus()
        Dim fc As GridEXFormatCondition
        fc = New GridEXFormatCondition(Dgv_Detalle.RootTable.Columns("tc"), ConditionOperator.Equal, DBNull.Value)
        fc.FormatStyle.FontBold = TriState.True
        fc.FormatStyle.FontSize = 9
        fc.FormatStyle.FontUnderline = TriState.True
        Dgv_Detalle.RootTable.FormatConditions.Add(fc)

    End Sub
    Public Function ObtenerTotales() As Double
        If (cbPlantilla.Value >= 1) Then
            Dim dt As DataTable = L_prObtenerPlantila(cbPlantilla.Value)
            If (dt.Rows.Count > 0) Then
                Dim dtTotales = L_prObtenerTotalesConciliacion(tbFechaI.Value.ToString("yyyy/MM/dd"))
                Dim totalGeneral As Double = 0
                For Each Cantidad As DataRow In dtTotales.Rows
                    totalGeneral = totalGeneral + Cantidad.Item("TotalConciliacion")
                Next
                If (totalGeneral > 0) Then
                    Return totalGeneral
                Else
                    Return 0
                End If
            Else
                Return 0
            End If
        Else
            Return 0
        End If

    End Function
    Public Sub _prArmarCuadre(ByRef dt As DataTable)
        Try
            If dt.Rows.Count > 0 Then
                Dim totaldebe As Double = dt.Compute("Sum(debe)", "")
                Dim totalhaber As Double = dt.Compute("Sum(haber)", "")
                Dim totaldebesus As Double = dt.Compute("Sum(debesus)", "")
                Dim totalhabersus As Double = dt.Compute("Sum(habersus)", "")
                Dim restantedebe As Double = 0
                Dim restanteHaber As Double = 0
                Dim RestanteDebeSus As Double = 0
                Dim RestanteHaberSus As Double = 0
                If (totaldebe > totalhaber) Then
                    restanteHaber = totaldebe - totalhaber
                Else
                    restantedebe = totalhaber - totaldebe
                End If
                If (totaldebesus > totalhabersus) Then
                    RestanteHaberSus = totaldebesus - totalhabersus
                Else
                    RestanteDebeSus = totalhabersus - totaldebesus
                End If
                If (restantedebe > 0 Or restanteHaber > 0 Or RestanteDebeSus > 0 Or RestanteHaberSus > 0) Then
                    Dim dtObtenerCuenta As DataTable = L_prCuentaDiferencia(652)  '''3=Lavadero
                    If dtObtenerCuenta.Rows.Count > 0 Then
                        dt.Rows.Add(dtObtenerCuenta.Rows(0).Item("canumi"), dtObtenerCuenta.Rows(0).Item("cacta"), dtObtenerCuenta.Rows(0).Item("cadesc"), DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, Administracion, 0)
                        dt.Rows.Add(dtObtenerCuenta.Rows(1).Item("canumi"), dtObtenerCuenta.Rows(1).Item("cacta"), dtObtenerCuenta.Rows(1).Item("cadesc"), DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, Administracion, 0)
                        Linea = Linea + 1
                        dt.Rows.Add(dtObtenerCuenta.Rows(1).Item("canumi"), DBNull.Value, "Ajuste de Cambio", DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, IIf(restantedebe = 0, DBNull.Value, restantedebe), IIf(restanteHaber = 0, DBNull.Value, restanteHaber), IIf(RestanteDebeSus = 0, DBNull.Value, RestanteDebeSus), IIf(RestanteHaberSus = 0, DBNull.Value, RestanteHaberSus), Administracion, Linea)
                        conRedondeo = True
                    Else
                        conRedondeo = False
                    End If
                Else
                    conRedondeo = False
                End If
            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub
    Public Function _ValidarCampos() As Boolean
        Try
            If (tbTipoCambio.Value <= 0) Then
                Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
                ToastNotification.Show(Me, "Por Favor Coloque un Tipo de Cambio Valido".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                tbTipoCambio.Focus()
                Return False

            End If

            If (Dgv_Detalle.RowCount <= 0) Then
                Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
                ToastNotification.Show(Me, "No Existen Datos Para Guardar".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                Dgv_Detalle.Focus()
                Return False
            End If
            If (cbPlantilla.SelectedIndex < 0) Then
                Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
                ToastNotification.Show(Me, "POR FAVOR SELECCIONE UNA SUCURSAL".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                cbPlantilla.Focus()
                Return False

            End If

            If conRedondeo = True Then
                Dgv_Detalle.MoveLast()
                Dim redonDebeBs As Double = IIf(IsDBNull(Dgv_Detalle.GetValue("debe")) = True, 0, Dgv_Detalle.GetValue("debe"))
                Dim redonDebeSus As Double = IIf(IsDBNull(Dgv_Detalle.GetValue("debesus")) = True, 0, Dgv_Detalle.GetValue("debesus"))
                Dim redonHaberBs As Double = IIf(IsDBNull(Dgv_Detalle.GetValue("haber")) = True, 0, Dgv_Detalle.GetValue("haber"))
                Dim redonHaberSus As Double = IIf(IsDBNull(Dgv_Detalle.GetValue("habersus")) = True, 0, Dgv_Detalle.GetValue("habersus"))
                If redonDebeBs > 0 Or redonDebeSus > 0 Or redonHaberBs > 0 Or redonHaberSus > 0 Then
                    Dim dtGlobal As DataTable = L_prConfigGeneralEmpresa(1)
                    Dim _difMaximaAjuste As Double
                    If dtGlobal.Rows.Count > 0 Then
                        _difMaximaAjuste = dtGlobal.Rows(0).Item("cfdifmax")
                    End If
                    Dim diferencia As Double = redonDebeSus - redonHaberSus
                    'verifico si la diferencia es para el debe
                    If diferencia < 0 Then 'si es negativo la diferencia es para el haber
                        diferencia = diferencia * -1
                    End If
                    If diferencia > _difMaximaAjuste Then 'si es verdadero,pregunto sin desea hacer el ajuste automatico
                        Dim info As New TaskDialogInfo("advertencia".ToUpper, eTaskDialogIcon.Exclamation, "ajuste de cambio mayor al permitido, ¿desea grabar de todos modos?".ToUpper, "".ToUpper, eTaskDialogButton.Yes Or eTaskDialogButton.No, eTaskDialogBackgroundColor.Blue)
                        Dim result As eTaskDialogResult = TaskDialog.Show(info)
                        If result = eTaskDialogResult.Yes Then
                        Else
                            Return False
                        End If
                    End If
                End If
            End If
            Return True
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
            Return False
        End Try
    End Function
    Private Sub _prGuardarModificado()
        Try
            Dim dtDetalle As DataTable = CType(Dgv_Detalle.DataSource, DataTable)
            Dim Reg As DataRow
            For Each fila As DataRow In dtDetalle.Rows
                If IsDBNull(fila.Item("debe")) = True Then
                    fila.Item("debe") = 0
                End If
                If IsDBNull(fila.Item("haber")) = True Then
                    fila.Item("haber") = 0
                End If
                If IsDBNull(fila.Item("debesus")) = True Then
                    fila.Item("debesus") = 0
                End If
                If IsDBNull(fila.Item("habersus")) = True Then
                    fila.Item("habersus") = 0
                End If
            Next
            '******************************************
            Dim numiComprobante As String = ""

            Dim dt As DataTable = L_prObtenerPlantila(cbPlantilla.Value)
            Dim tipo As Integer = dt.Rows(0).Item("Tipo")
            Dim factura As Integer = dt.Rows(0).Item("Factura")
            Dim TipoTransacion As Integer = 0

            If (cbPlantilla.Value = 1 Or cbPlantilla.Value = 2 Or cbPlantilla.Value = 1004 Or cbPlantilla.Value = 1005) Then ''' Si es compra o es asiento contable de cuentas por cobrar
                TipoTransacion = 3   ''''se asigna 3= traspaso
            Else
                If (cbPlantilla.Value = 3) Then  '''' si es ventas al contado o credito
                    TipoTransacion = 1 ''' se asigna 1=ingreso

                Else ''' si no es ninguna de las demas y entonces es pagos de credito es un egreso

                    TipoTransacion = 2  '''' se asigna 2= egreso
                End If

            End If

            Dim res As Boolean = L_prComprobanteGrabarIntegracion(numiComprobante, "", 1, tbFechaI.Value.Year.ToString, tbFechaI.Value.Month.ToString, "", tbFechaI.Value.Date.ToString("yyyy/MM/dd"), tbTipoCambio.Value.ToString, "", "", gi_empresaNumi, dtDetalle, dtDetalle2, "", 0, tbTipoCambio.Value, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaI.Value.ToString("yyyy/MM/dd"), 1, _LisTransacciones, cbPlantilla.Value,
                                                                  tipo, factura, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaI.Value.ToString("yyyy/MM/dd"), TipoTransacion, dtTO00111)

            If res Then
                'camabiar de estado a Caja en DiSOFT
                Dim idCaja = L_prObtenerIdCaja(tbFechaI.Value.ToString("yyyy/MM/dd"))
                For Each Id As DataRow In idCaja.Rows
                    L_prCajaCambiarEstado(Id.Item("olnumi"))
                Next

                Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
                ToastNotification.Show(Me, "El Asiento Contable fue generado Exitosamente".ToUpper,
                                          img, 2000,
                                          eToastGlowColor.Green,
                                          eToastPosition.TopCenter
                                          )
                _prImprimirComprobante(numiComprobante)
                _prCargarMovimiento()
                _prInhabiliitar()
                If Dgv_Buscador.RowCount > 0 Then
                    _prMostrarRegistro(0)
                End If
            Else
                Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                ToastNotification.Show(Me, "Los codigos no pudieron ser modificados".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub
    Private Sub _prImprimirComprobante(numiComprobante)
        Try
            Dim objrep As New R_Comprobante
            Dim dt As New DataTable
            dt = L_prComprobanteReporteComprobante(numiComprobante)

            'ahora lo mando al visualizador
            P_Global.Visualizador = New Visualizador
            objrep.SetDataSource(dt)
            objrep.SetParameterValue("fechaDesde", "")
            objrep.SetParameterValue("fechaHasta", "")
            objrep.SetParameterValue("titulo", "CFDISTRIBUCIÓN S.R.L." + gs_empresaDesc.ToUpper)
            objrep.SetParameterValue("nit", gs_empresaNit.ToUpper)
            objrep.SetParameterValue("ultimoRegistro", 0)
            objrep.SetParameterValue("Direccion", gs_empresaDireccion)
            objrep.SetParameterValue("fecha", tbFechaI.Value.ToString("dd/MM/yyyy"))
            objrep.SetParameterValue("Autor", gs_user)

            P_Global.Visualizador.CRV1.ReportSource = objrep 'Comentar
            P_Global.Visualizador.Show() 'Comentar
            P_Global.Visualizador.BringToFront() 'Comentar
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub
    Public Sub _prMostrarRegistro(_N As Integer)
        Try
            With Dgv_Buscador
                tbNumi.Text = .GetValue("ifnumi")
                tbFechaI.Value = .GetValue("iffechai")
                tbTipoCambio.Value = .GetValue("iftc")
                cbPlantilla.Value = .GetValue("ifto001numibanco")
            End With
            _prCargarDetalleMovimiento(tbNumi.Text)
            LblPaginacion.Text = Str(Dgv_Buscador.Row + 1) + "/" + Dgv_Buscador.RowCount.ToString
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub
    Private Sub _prCargarDetalleMovimiento(_numi As String)
        Try
            Dim dt As New DataTable
            dt = L_prIntegracionDetalle(_numi)
            _prArmarDetalleDt(dt)
            Dgv_Detalle.DataSource = dt
            Dgv_Detalle.RetrieveStructure()
            Dgv_Detalle.AlternatingColors = True


            With Dgv_Detalle.RootTable.Columns("canumi")
                .Width = 100

                .Visible = False
            End With
            With Dgv_Detalle.RootTable.Columns("variable")
                .Width = 100

                .Visible = False
            End With
            With Dgv_Detalle.RootTable.Columns("linea")
                .Width = 100

                .Visible = False
            End With
            With Dgv_Detalle.RootTable.Columns("nro")
                .Width = 120
                .Caption = "NRO CUENTA"
                .Visible = True
            End With
            With Dgv_Detalle.RootTable.Columns("cadesc")
                .Width = 500
                .Caption = "DESCRIPCION"
                .Visible = True
            End With
            With Dgv_Detalle.RootTable.Columns("chporcen")
                .Width = 100

                .Visible = False
            End With
            With Dgv_Detalle.RootTable.Columns("chdebe")
                .Width = 180
                .Caption = "DEBE"
                .Visible = False
            End With
            With Dgv_Detalle.RootTable.Columns("chhaber")
                .Width = 180
                .Caption = "HABER"
                .Visible = False
            End With
            With Dgv_Detalle.RootTable.Columns("tc")
                .Width = 70
                .Caption = "TC"
                .Visible = True
                .FormatString = "0.00"
            End With
            With Dgv_Detalle.RootTable.Columns("debe")
                .Width = 100
                .Caption = "DEBE BS"
                .Visible = True
                .TextAlignment = TextAlignment.Far
                .FormatString = "0.00"
                .TotalFormatString = "0.00"
                .AggregateFunction = AggregateFunction.Sum

            End With
            With Dgv_Detalle.RootTable.Columns("haber")
                .Width = 100
                .Caption = "HABER BS"
                .Visible = True
                .FormatString = "0.00"
                .TotalFormatString = "0.00"
                .TextAlignment = TextAlignment.Far
                .AggregateFunction = AggregateFunction.Sum

            End With

            With Dgv_Detalle.RootTable.Columns("debesus")
                .Width = 100
                .Caption = "DEBE SUS"
                .Visible = True
                .TextAlignment = TextAlignment.Far
                .FormatString = "0.00"
                .TotalFormatString = "0.00"
                .AggregateFunction = AggregateFunction.Sum

            End With
            With Dgv_Detalle.RootTable.Columns("habersus")
                .Width = 100
                .Caption = "HABER SUS"
                .Visible = True
                .FormatString = "0.00"
                .TotalFormatString = "0.00"
                .TextAlignment = TextAlignment.Far
                .AggregateFunction = AggregateFunction.Sum

            End With
            With Dgv_Detalle
                .TotalRowFormatStyle.BackColor = Color.Gold
                .TotalRowPosition = TotalRowPosition.BottomFixed
                .TotalRow = InheritableBoolean.True

                .GroupByBoxVisible = False
                'diseño de la grilla
                .VisualStyle = VisualStyle.Office2007
            End With

            _prAplicarCondiccionJanus()
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try

    End Sub
    Public Sub _prArmarDetalleDt(ByRef dt As DataTable)
        Try
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
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try

    End Sub
    Public Function ObtenerTotalVentasCreditoOContado(TipoVenta As Integer) As Double

        If (cbPlantilla.Value >= 1) Then
            Dim dt As DataTable = L_prObtenerPlantila(cbPlantilla.Value)
            If (dt.Rows.Count > 0) Then
                Dim tipo As Integer = dt.Rows(0).Item("Tipo")
                Dim factura As Integer = dt.Rows(0).Item("Factura")

                If (TipoVenta = 1) Then
                    Dim dtTotales = L_prObtenerTotalesContado(tbFechaI.Value.ToString("yyyy/MM/dd"))
                    Dim totalGeneral As Double = 0
                    For Each Cantidad As DataRow In dtTotales.Rows
                        totalGeneral = totalGeneral + Cantidad.Item("TotalEfectivo")
                    Next
                    If (totalGeneral > 0) Then
                        Return totalGeneral
                    Else
                        Return 0
                    End If
                End If
                If (TipoVenta = 0) Then
                    Dim dtTotales = L_prObtenerTotalesCredito(tbFechaI.Value.ToString("yyyy/MM/dd"))
                    If (dtTotales.Rows.Count > 0) Then
                        Return IIf(IsDBNull(dtTotales.Rows(0).Item("TotalCredito")), 0, dtTotales.Rows(0).Item("TotalCredito"))
                    Else
                        Return 0
                    End If
                End If
                If (TipoVenta = 2) Then
                    Dim dtTotales = L_prObtenerTotalesTransaccionVentaPrecioCosto(tipo, factura, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaI.Value.ToString("yyyy/MM/dd"))
                    If (dtTotales.Rows.Count > 0) Then
                        Return IIf(IsDBNull(dtTotales.Rows(0).Item("Total")), 0, dtTotales.Rows(0).Item("Total"))
                    Else
                        Return 0
                    End If
                End If
                If (TipoVenta = 4) Then
                    Dim dtTotales = L_prObtenerTotalesBanco(tbFechaI.Value.ToString("yyyy/MM/dd"))
                    Dim totalGeneral As Double = 0
                    For Each Cantidad As DataRow In dtTotales.Rows
                        totalGeneral = totalGeneral + Cantidad.Item("TotalDeposito")
                    Next
                    If (totalGeneral > 0) Then
                        Return totalGeneral
                    Else
                        Return 0
                    End If
                End If
                If (TipoVenta = 5) Then
                    Dim dtTotales = L_prObtenerTotalesDiferencia(tbFechaI.Value.ToString("yyyy/MM/dd"))
                    Dim totalGeneral As Double = 0
                    For Each Cantidad As DataRow In dtTotales.Rows
                        totalGeneral = totalGeneral + Cantidad.Item("Diferencia")
                    Next
                    Return totalGeneral
                End If
            Else
                Return 0
            End If
        Else
            Return 0
        End If
        Return 0
    End Function

    Private Sub _prImprimir()
        Try
            Dim objrep As New R_ComprobanteIntegracion
            Dim dt As New DataTable
            dt = CType(Dgv_Detalle.DataSource, DataTable)
            P_Global.Visualizador = New Visualizador
            objrep.SetDataSource(dt)
            objrep.SetParameterValue("fecha", tbFechaI.Value.ToString("dd/MM/yyyy"))
            objrep.SetParameterValue("tc", tbTipoCambio.Value)
            objrep.SetParameterValue("titulo", "COMPROBANTE DE INGRESO")
            objrep.SetParameterValue("titulo2", "CFDISTRIBUCIÓN S.R.L.")
            objrep.SetParameterValue("Direccion", gs_empresaDireccion)
            objrep.SetParameterValue("glosa", cbPlantilla.Text)
            objrep.SetParameterValue("Autor", gs_user)
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
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub
#End Region
#Region "EVENTOS"
    Private Sub F1_AsientosContables2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            _IniciarTodo()
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub

    Private Sub btActualizar_Click(sender As Object, e As EventArgs) Handles btActualizar.Click
        Try
            If (IsNothing(tbTipoCambio.Value) Or tbTipoCambio.ToString = String.Empty Or tbTipoCambio.Value = 0) Then
                Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
                ToastNotification.Show(Me, "NO HAY TIPO DE CAMBIO REGISTRADO. POR FAVOR REGISTRE EL TIPO DE CAMBIO".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                cbPlantilla.Focus()
                Return
            End If
            If (cbPlantilla.SelectedIndex < 0) Then
                Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
                ToastNotification.Show(Me, "POR FAVOR SELECCIONE UN MODULO".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                cbPlantilla.Focus()
                Return

            End If
            dtTC009.Rows.Clear()

            _prCrearColumns()
            dtTO00111 = L_prComprobanteDetalleDetalleGeneral(-1)
            _prCargarTablaComprobantes()
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
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

        If (gb_userTodasSuc = False And CType(cbPlantilla.DataSource, DataTable).Rows.Count > 0) Then
            cbPlantilla.SelectedIndex = _fnObtenerPosSucursal(gi_userNumiSucursal)
            cbPlantilla.ReadOnly = True
        Else
            cbPlantilla.ReadOnly = False
        End If
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        btnNuevo.Enabled = False
        btnModificar.Enabled = False
        btnGrabar.Enabled = True
        btnEliminar.Enabled = False
    End Sub

    Private Sub btnNuevoTipoCambio_Click(sender As Object, e As EventArgs) Handles btnNuevoTipoCambio.Click
        _prAñadirTipoCambio()
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _modulo.Select()
        _tab.Close()
    End Sub

    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        Try
            If _ValidarCampos() = False Then
                Exit Sub
            End If

            If (tbNumi.Text = String.Empty) Then
                _prGuardarModificado()
            Else
            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub


    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        Try
            _prImprimir()
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub


    Private Sub Dgv_Buscador_SelectionChanged(sender As Object, e As EventArgs) Handles Dgv_Buscador.SelectionChanged
        If (Dgv_Buscador.RowCount >= 0 And Dgv_Buscador.Row >= 0) Then
            _prMostrarRegistro(Dgv_Buscador.Row)
        End If
    End Sub

    Private Sub btnPrimero_Click(sender As Object, e As EventArgs) Handles btnPrimero.Click
        Dim _MPos As Integer
        If Dgv_Buscador.RowCount > 0 Then
            _MPos = 0
            ''   _prMostrarRegistro(_MPos)
            Dgv_Buscador.Row = _MPos
        End If
    End Sub

    Private Sub btnAnterior_Click(sender As Object, e As EventArgs) Handles btnAnterior.Click
        Dim _MPos As Integer = Dgv_Buscador.Row
        If _MPos > 0 And Dgv_Buscador.RowCount > 0 Then
            _MPos = _MPos - 1
            ''  _prMostrarRegistro(_MPos)
            Dgv_Buscador.Row = _MPos
        End If
    End Sub
    Private Sub btnSiguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click
        Dim _pos As Integer = Dgv_Buscador.Row
        If _pos < Dgv_Buscador.RowCount - 1 Then
            _pos = Dgv_Buscador.Row + 1
            '' _prMostrarRegistro(_pos)
            Dgv_Buscador.Row = _pos
        End If
    End Sub
    Private Sub btnUltimo_Click(sender As Object, e As EventArgs) Handles btnUltimo.Click
        Dim _pos As Integer = Dgv_Buscador.Row
        If Dgv_Buscador.RowCount > 0 Then
            _pos = Dgv_Buscador.RowCount - 1
            ''  _prMostrarRegistro(_pos)
            Dgv_Buscador.Row = _pos
        End If
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        Try
            Dim ef = New Efecto
            ef.tipo = 2
            ef.Context = "¿esta seguro de eliminar el asiento contable?".ToUpper
            ef.Header = "mensaje principal".ToUpper
            ef.ShowDialog()
            Dim bandera As Boolean = False
            bandera = ef.band
            If (bandera = True) Then
                Dim mensajeError As String = ""
                Dim res As Boolean = L_fnEliminarAsientoContable(tbNumi.Text, mensajeError)
                If res Then
                    Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)

                    ToastNotification.Show(Me, "Código de Venta ".ToUpper + tbNumi.Text + " eliminado con Exito.".ToUpper,
                                              img, 2000,
                                              eToastGlowColor.Green,
                                              eToastPosition.TopCenter)
                    _prCargarMovimiento()
                    _prInhabiliitar()
                    If Dgv_Buscador.RowCount > 0 Then
                        _prMostrarRegistro(0)
                    End If
                Else
                    Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                    ToastNotification.Show(Me, mensajeError, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                End If
            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub
#End Region
End Class