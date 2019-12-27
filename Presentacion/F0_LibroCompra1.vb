Imports Logica.AccesoLogica
Imports DevComponents.Editors
Imports DevComponents.DotNetBar.SuperGrid
Imports System.IO
Imports System.Drawing.Printing
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Imports Janus.Windows.GridEX

Public Class F0_LibroCompra1

#Region "Variables Globales"

    Dim _DuracionSms As Integer = 5
    Dim _DsLV As DataTable
    Public _modulo As SideNavItem
    Public _nameButton As String
    Public _tab As SuperTabItem
    Private inDuracion As Byte = 5

    Dim codReporte As String = "LibCom"


#End Region

#Region "Eventos"

    Private Sub P_LibroVentas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        P_Inicio()
    End Sub

#End Region


#Region "Metodos"
    Private Sub P_Inicio()

        Me.WindowState = FormWindowState.Maximized
        Me.Text = "L I B R O   D E   C O M P R A S"

        btnNuevo.Visible = False
        btnModificar.Visible = False
        btnEliminar.Visible = False
        btnGrabar.Visible = False
        btnSalir.Visible = False
        btnImprimir.Visible = False

        btnPrimero.Visible = False
        btnAnterior.Visible = False
        btnSiguiente.Visible = False
        btnUltimo.Visible = False

        LblPaginacion.Visible = False
        BubbleBarUsuario.Visible = False


        CpExportarExcel.Visible = False

        P_prArmarCombos()


        P_prArmarGrillas()

        P_ArmarGrilla()
    End Sub

    Private Sub P_prArmarCombos()
        P_prArmarComboAno()
        P_prArmarComboMes()
    End Sub

    Private Sub P_prArmarGrillas()
        P_prArmarGrillaLibroCompra("-1", "-1")
    End Sub

    Private Sub P_prArmarComboAno()
        Dim dt As New DataTable
        dt = L_prCompraComprobanteGeneralAnios()

        With cbAno.DropDownList
            .Columns.Clear()

            .Columns.Add(dt.Columns("anho").ToString).Width = 100
            .Columns(0).Caption = "Año"
            .Columns(0).Visible = True

            .ValueMember = dt.Columns("anho").ToString
            .DisplayMember = dt.Columns("anho").ToString
            .DataSource = dt
            .Refresh()
        End With

        cbAno.SelectedIndex = dt.Rows.Count - 1
    End Sub


    Private Sub P_prArmarComboMes()
        Dim dt As New DataTable

        dt.Columns.Add("nro", Type.GetType("System.Int32"))
        dt.Columns.Add("mes", Type.GetType("System.String"))

        Dim fil As DataRow
        For i = 1 To 12
            fil = dt.NewRow
            fil(0) = i
            fil(1) = MonthName(i)
            dt.Rows.Add(fil)
        Next

        With cbMes.DropDownList
            .Columns.Clear()

            .Columns.Add(dt.Columns("nro").ToString).Width = 60
            .Columns(0).Caption = "Nro."
            .Columns(0).Visible = True

            .Columns.Add(dt.Columns("mes").ToString).Width = 140
            .Columns(1).Caption = "Mes"
            .Columns(1).Visible = True

            .ValueMember = dt.Columns("nro").ToString
            .DisplayMember = dt.Columns("mes").ToString
            .DataSource = dt
            .Refresh()
        End With

        cbMes.SelectedIndex = Month(Now.Date) - 1
    End Sub

    Private Sub P_prArmarGrillaLibroCompra(mes As String, ano As String)
        Dim dt As New DataTable
        dt = L_prCompraComprobanteGeneralLibroCompra(ano, mes, gi_empresaNumi)

        dgjLibroCompra.BoundMode = Janus.Data.BoundMode.Bound
        dgjLibroCompra.DataSource = dt
        dgjLibroCompra.RetrieveStructure()

        'dar formato a las columnas
        With dgjLibroCompra.RootTable.Columns("esp")
            .Caption = "ESP"
            .Width = 40
            ''.HeaderStyle.Font = ftTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            ''.CellStyle.Font = ftNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
        End With

        With dgjLibroCompra.RootTable.Columns("row")
            .Caption = "Nro"
            .Width = 80
            '.HeaderStyle.Font = ftTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            '.CellStyle.Font = ftNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
        End With

        With dgjLibroCompra.RootTable.Columns("fcanumi")
            .Visible = False
        End With

        With dgjLibroCompra.RootTable.Columns("fcafdoc")
            .Caption = "Fecha"
            .Width = 100
            '.HeaderStyle.Font = ftTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            '.CellStyle.Font = ftNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
        End With

        With dgjLibroCompra.RootTable.Columns("fcanit")
            .Caption = "NIT"
            .Width = 120
            '.HeaderStyle.Font = ftTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            '.CellStyle.Font = ftNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
        End With

        With dgjLibroCompra.RootTable.Columns("fcarsocial")
            .Caption = "Razon Social"
            .Width = 200
            '.HeaderStyle.Font = ftTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            '.CellStyle.Font = ftNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
        End With

        With dgjLibroCompra.RootTable.Columns("fcanfac")
            .Caption = "Nro. Factura"
            .Width = 100
            '.HeaderStyle.Font = ftTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            '.CellStyle.Font = ftNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
        End With

        With dgjLibroCompra.RootTable.Columns("fcandui")
            .Caption = "Nro. DUI"
            .Width = 120
            '.HeaderStyle.Font = ftTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            '.CellStyle.Font = ftNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
        End With

        With dgjLibroCompra.RootTable.Columns("fcaautoriz")
            .Caption = "Nro. Autorización"
            .Width = 120
            '.HeaderStyle.Font = ftTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            '.CellStyle.Font = ftNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
        End With
        With dgjLibroCompra.RootTable.Columns("fcaitc")
            .Caption = "Importe Total de la Compra"
            .Width = 120
            '.HeaderStyle.Font = ftTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            '.CellStyle.Font = ftNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
            .FormatString = "0.00"
        End With

        With dgjLibroCompra.RootTable.Columns("fcanscf")
            .Caption = "No Sujeto a Crédito Fiscal"
            .Width = 120
            '.HeaderStyle.Font = ftTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            '.CellStyle.Font = ftNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
            .FormatString = "0.00"
        End With

        With dgjLibroCompra.RootTable.Columns("fcasubtotal")
            .Caption = "Sub Total"
            .Width = 120
            '.HeaderStyle.Font = ftTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            '.CellStyle.Font = ftNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
            .FormatString = "0.00"
        End With

        With dgjLibroCompra.RootTable.Columns("fcadesc")
            .Caption = "Descuento, Bonificaciónes, Rebajas Obtenidas"
            .Width = 120
            '.HeaderStyle.Font = ftTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            '.CellStyle.Font = ftNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
            .FormatString = "0.00"
        End With

        With dgjLibroCompra.RootTable.Columns("fcaibcf")
            .Caption = "Importe Base para Crédito Fiscal"
            .Width = 120
            '.HeaderStyle.Font = ftTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            '.CellStyle.Font = ftNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
            .FormatString = "0.00"
        End With

        With dgjLibroCompra.RootTable.Columns("fcacfiscal")
            .Caption = "Crédito Fiscal"
            .Width = 120
            '.HeaderStyle.Font = ftTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            '.CellStyle.Font = ftNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
            .FormatString = "0.00"
        End With

        With dgjLibroCompra.RootTable.Columns("fcaccont")
            .Caption = "Código de Control"
            .Width = 120
            '.HeaderStyle.Font = ftTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            '.CellStyle.Font = ftNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
        End With

        With dgjLibroCompra.RootTable.Columns("fcatcom")
            .Caption = "Tipo de Compra"
            .Width = 100
            '.HeaderStyle.Font = ftTitulo
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            '.CellStyle.Font = ftNormal
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            '.CellStyle.BackColor = Color.AliceBlue
        End With

        'Habilitar Filtradores
        With dgjLibroCompra
            .GroupByBoxVisible = False
            '.FilterRowFormatStyle.BackColor = Color.Blue
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            'Diseño de la tabla
            .VisualStyle = VisualStyle.Office2007
            .SelectionMode = SelectionMode.MultipleSelection
            .AlternatingColors = True
            .RecordNavigator = True
        End With
    End Sub

    Private Function P_prExportarExcel() As Boolean
        Dim rutaArchivo As String
        'Dim _directorio As New FolderBrowserDialog

        If (1 = 1) Then 'If(_directorio.ShowDialog = Windows.Forms.DialogResult.OK) Then
            '_ubicacion = _directorio.SelectedPath
            rutaArchivo = gs_RutaArchivos + "\Compras\Libro de Compras"
            If (Not Directory.Exists(gs_RutaArchivos + "\Compras\Libro de Compras")) Then
                Directory.CreateDirectory(gs_RutaArchivos + "\Compras\Libro de Compras")
            End If
            Try
                Dim _stream As Stream
                Dim _escritor As StreamWriter
                Dim _fila As Integer = dgjLibroCompra.RowCount
                Dim _columna As Integer = dgjLibroCompra.RootTable.Columns.Count
                Dim _archivo As String = rutaArchivo & "\Libro_Compra_" & "_" & Now.Date.Day &
                    "." & Now.Date.Month & "." & Now.Date.Year & "_" & Now.Hour & "." & Now.Minute & "." & Now.Second & ".csv"
                Dim _linea As String = ""
                Dim _filadata = 0, columndata As Int32 = 0
                File.Delete(_archivo)
                _stream = File.OpenWrite(_archivo)
                _escritor = New StreamWriter(_stream, System.Text.Encoding.UTF8)

                For Each _col As GridEXColumn In dgjLibroCompra.RootTable.Columns
                    If (_col.Visible) Then
                        _linea = _linea & _col.Caption & ";"
                    End If
                Next
                _linea = Mid(CStr(_linea), 1, _linea.Length - 1)
                _escritor.WriteLine(_linea)
                _linea = Nothing

                CpExportarExcel.Visible = True
                CpExportarExcel.Minimum = 1
                CpExportarExcel.Maximum = dgjLibroCompra.RowCount
                CpExportarExcel.Value = 1

                For Each _fil As GridEXRow In dgjLibroCompra.GetRows
                    For Each _col As GridEXColumn In dgjLibroCompra.RootTable.Columns
                        If (_col.Visible) Then
                            _linea = _linea & CStr(_fil.Cells(_col.Key).Value) & ";"
                        End If
                    Next
                    _linea = Mid(CStr(_linea), 1, _linea.Length - 1)
                    _escritor.WriteLine(_linea)
                    _linea = Nothing
                    CpExportarExcel.Value += 1
                Next
                _escritor.Close()
                CpExportarExcel.Visible = False
                Try
                    Dim info As New TaskDialogInfo("¿desea abrir el libro de compra?".ToUpper,
                                                   eTaskDialogIcon.Exclamation,
                                                   "pregunta".ToUpper,
                                                   "Desea continuar?".ToUpper,
                                                   eTaskDialogButton.Yes Or eTaskDialogButton.Cancel,
                                                   eTaskDialogBackgroundColor.Blue)
                    Dim result As eTaskDialogResult = TaskDialog.Show(info)
                    If result = eTaskDialogResult.Yes Then
                        Process.Start(_archivo)
                    End If
                    Return True
                Catch ex As Exception
                    MsgBox(ex.Message)
                    Return False
                End Try
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        End If
        Return False
    End Function


    Private Sub _prImprimir()
        Dim objrep As New R_LibroCompra
        Dim dt As New DataTable
        If IsNothing(dgjLibroCompra.DataSource) = False Then
            dt = CType(dgjLibroCompra.DataSource, DataTable)
            If dt.Rows.Count > 0 Then

                'ahora lo mando al visualizador
                Dim dtTitulos As DataTable = L_prTitulosAll(codReporte)

                P_Global.Visualizador = New Visualizador
                objrep.SetDataSource(dt)

                objrep.SetParameterValue("periodo", cbMes.Value.ToString + "/" + cbAno.Text)
                objrep.SetParameterValue("ci", dtTitulos.Rows(0).Item("yedesc").ToString)
                objrep.SetParameterValue("nombre", dtTitulos.Rows(1).Item("yedesc").ToString)

                'objrep.SetParameterValue("empresaDesc", gs_empresaDescSistema)
                objrep.SetParameterValue("empresaDesc", "CFDISTRIBUCIÓN S.R.L." + gs_empresaDesc.ToUpper)

                objrep.SetParameterValue("empresaNit", gs_empresaNit)
                objrep.SetParameterValue("empresaDirec", gs_empresaDireccion)

                P_Global.Visualizador.CRV1.ReportSource = objrep 'Comentar
                P_Global.Visualizador.Show() 'Comentar
                P_Global.Visualizador.BringToFront() 'Comentar
            End If
        End If

    End Sub
    Private Function P_ExportarTxt(TipoLibroVenta As String, Separador As String) As Boolean
        Dim _ubicacion As String
        _ubicacion = gs_CarpetaRaiz
        Try
            'DgdLC.PrimaryGrid.Rows.Clear()
            'DgdLC.PrimaryGrid.DataSource = L_prCompraComprobanteGeneralLibroCompra2(cbAno.Value.ToString, cbMes.Value.ToString, gi_empresaNumi)
            'DgdLC.PrimaryGrid.SetActiveRow(CType(DgdLC.PrimaryGrid.ActiveRow, GridRow))
            Dim _stream As Stream
            Dim _escritor As StreamWriter
            Dim _fila As Integer = DgdLC.PrimaryGrid.Rows.Count
            Dim _columna As Integer = DgdLC.PrimaryGrid.Columns.Count
            Dim _archivo As String = _ubicacion & "\LCV_" & Now.Date.Day &
                "." & Now.Date.Month & "." & Now.Date.Year & "_" & Now.Hour & "." & Now.Minute & "." & Now.Second & ".txt"
            Dim _linea As String = "" 'TipoLibroVenta + "|"
            Dim _filadata = 0, columndata As Int32 = 0
            File.Delete(_archivo)
            _stream = File.OpenWrite(_archivo)
            _escritor = New StreamWriter(_stream, System.Text.Encoding.UTF8)

            Dim nro As Integer = 1
            For Each _fil As GridRow In DgdLC.PrimaryGrid.Rows
                _linea = TipoLibroVenta + Separador + nro.ToString + Separador
                '_linea = nro.ToString + Separador
                For Each _col As GridColumn In DgdLC.PrimaryGrid.Columns
                    If (_col.Visible And Not _col.Name.Equals("factura")) Then
                        _linea = _linea & CStr(_fil.Cells(_col.Name).Value).Trim & Separador
                    End If
                Next
                _linea = Mid(CStr(_linea), 1, _linea.Length - 1)
                _escritor.WriteLine(_linea)
                _linea = Nothing
                nro += 1
            Next
            _escritor.Close()
            Try
                If (MessageBox.Show("DESEA ABRIR EL ARCHIVO?", "PREGUNTA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes) Then
                    Process.Start(_archivo)
                End If
                Return True
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
        Return False
    End Function
    Private Sub P_ArmarGrilla()

        DgdLC.PrimaryGrid.Columns.Clear()
        'Alto de la Fila de Nombres de Columnas
        DgdLC.PrimaryGrid.ColumnHeader.RowHeight = 25

        'Mostrar u Ocultar la Fila de Filtrado
        DgdLC.PrimaryGrid.EnableColumnFiltering = True
        DgdLC.PrimaryGrid.EnableFiltering = True
        DgdLC.PrimaryGrid.EnableRowFiltering = True
        DgdLC.PrimaryGrid.Filter.Visible = True

        'Para Mostrar u Ocultar la Columna de Cabecera de las Filas
        DgdLC.PrimaryGrid.ShowRowHeaders = True

        'Para Mostrar el Indice de la Grilla
        DgdLC.PrimaryGrid.RowHeaderIndexOffset = 1
        DgdLC.PrimaryGrid.ShowRowGridIndex = True

        'Alto de las Filas
        DgdLC.PrimaryGrid.DefaultRowHeight = 22

        'Alternar Colores de las Filas
        DgdLC.PrimaryGrid.UseAlternateRowStyle = True

        'Para permitir o denegar el cambio de tamaño de la Filas
        DgdLC.PrimaryGrid.AllowRowResize = False

        'Para que el Tamaño de las Columnas se pongan automaticamente
        'DgdLCV.PrimaryGrid.ColumnAutoSizeMode = ColumnAutoSizeMode.DisplayedCells

        DgdLC.PrimaryGrid.SelectionGranularity = SelectionGranularity.RowWithCellHighlight

        Dim col As GridColumn

        ''Nro
        'col = New GridColumn("Nro")
        'col.HeaderText = "Nro"
        'col.EditorType = GetType(GridTextBoxXEditControl)
        'col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        'col.ReadOnly = True
        'col.Visible = True
        'col.Width = 60
        'DgdLCV.PrimaryGrid.Columns.Add(col)

        'Codigo
        col = New GridColumn("fcanumi")
        col.HeaderText = "Codigo"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = False
        col.Width = 80
        DgdLC.PrimaryGrid.Columns.Add(col)

        'Fecha
        col = New GridColumn("fcafdoc")
        col.HeaderText = "Fecha"
        col.EditorType = GetType(GridDateTimePickerEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleCenter
        col.ReadOnly = True
        col.Visible = True
        col.Width = 120
        DgdLC.PrimaryGrid.Columns.Add(col)

        'Nro de Factura
        col = New GridColumn("fcanit")
        col.HeaderText = "NIT"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 120
        DgdLC.PrimaryGrid.Columns.Add(col)

        'Nro Autorizacion
        col = New GridColumn("fcarsocial")
        col.HeaderText = "RAZON SOCIAL"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleLeft
        col.ReadOnly = True
        col.Visible = True
        col.Width = 200
        DgdLC.PrimaryGrid.Columns.Add(col)

        'Nit
        col = New GridColumn("fcanfac")
        col.HeaderText = "Nro. Factura"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 120
        DgdLC.PrimaryGrid.Columns.Add(col)

        'Razon Social
        col = New GridColumn("fcandui")
        col.HeaderText = "Nro. DUI"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 120
        DgdLC.PrimaryGrid.Columns.Add(col)

        'A
        col = New GridColumn("fcaautoriz")
        col.HeaderText = "Nro. Autorización"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 120
        DgdLC.PrimaryGrid.Columns.Add(col)

        'B
        col = New GridColumn("fcaitc")
        col.HeaderText = "Importe Total de la Compra"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 120
        DgdLC.PrimaryGrid.Columns.Add(col)

        'C
        col = New GridColumn("fcanscf")
        col.HeaderText = "No Sujeto a Crédito Fiscal"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 120
        DgdLC.PrimaryGrid.Columns.Add(col)

        'D
        col = New GridColumn("fcasubtotal")
        col.HeaderText = "Sub Total"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 120
        DgdLC.PrimaryGrid.Columns.Add(col)

        'E
        col = New GridColumn("fcadesc")
        col.HeaderText = "Descuento, Bonificaciónes, Rebajas Obtenidas"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 120
        DgdLC.PrimaryGrid.Columns.Add(col)

        'F
        col = New GridColumn("fcaibcf")
        col.HeaderText = "Importe Base para credito fiscal"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 120
        DgdLC.PrimaryGrid.Columns.Add(col)

        'G
        col = New GridColumn("fcacfiscal")
        col.HeaderText = "Crédito Fiscal"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 120
        DgdLC.PrimaryGrid.Columns.Add(col)

        'H
        col = New GridColumn("fcaccont")
        col.HeaderText = "Código de control"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 120
        DgdLC.PrimaryGrid.Columns.Add(col)

        'Codigo de Control
        col = New GridColumn("fcatcom")
        col.HeaderText = "Tipo de Compra"
        col.EditorType = GetType(GridTextBoxXEditControl)
        col.CellStyles.Default.Alignment = Style.Alignment.MiddleRight
        col.ReadOnly = True
        col.Visible = True
        col.Width = 120
        DgdLC.PrimaryGrid.Columns.Add(col)
    End Sub
#End Region

    Private Sub _prSalir()
        _modulo.Select()
        _tab.Close()
    End Sub
    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _prSalir()
    End Sub

    Private Sub btGenerar_Click(sender As Object, e As EventArgs) Handles btGenerar.Click
        P_prArmarGrillaLibroCompra(cbMes.Value.ToString, cbAno.Value.ToString)
        DgdLC.PrimaryGrid.Rows.Clear()
        DgdLC.PrimaryGrid.DataSource = L_prCompraComprobanteGeneralLibroCompra2(cbAno.Value.ToString, cbMes.Value.ToString, gi_empresaNumi)
        'DgdLC.PrimaryGrid.DataSource = CType(dgjLibroCompra.DataSource, DataTable)
        DgdLC.PrimaryGrid.SetActiveRow(CType(DgdLC.PrimaryGrid.ActiveRow, GridRow))
        If (dgjLibroCompra.GetRows.Count = 0) Then
            ToastNotification.Show(Me,
                                   "No hay compras para el año y mes seleccionados.".ToUpper,
                                   My.Resources.INFORMATION, inDuracion * 1000,
                                   eToastGlowColor.Blue,
                                   eToastPosition.TopCenter)
        End If
    End Sub

    Private Sub btExcel_Click(sender As Object, e As EventArgs) Handles btExcel.Click
        If (P_prExportarExcel()) Then
            ToastNotification.Show(Me, "Exportación de libro de compras exitosa.".ToUpper,
                                       My.Resources.GRABACION_EXITOSA, inDuracion * 1000,
                                       eToastGlowColor.Green,
                                       eToastPosition.TopCenter)
        Else
            ToastNotification.Show(Me, "Fallo la esporatación de el libro de compras.".ToUpper,
                                       My.Resources.WARNING, inDuracion * 1000,
                                       eToastGlowColor.Red,
                                       eToastPosition.TopCenter)
        End If
    End Sub

    Private Sub btReporte_Click(sender As Object, e As EventArgs) Handles btReporte.Click
        _prImprimir()
    End Sub
    Private Sub btTxt_Click_1(sender As Object, e As EventArgs) Handles btTxt.Click
        If (P_ExportarTxt("3", "|")) Then
            ToastNotification.Show(Me, "EXPORTACIÓN DE LISTA DE PRECIOS EXITOSA..!!!",
                                       My.Resources.OK1, _DuracionSms * 1000,
                                       eToastGlowColor.Green,
                                       eToastPosition.BottomLeft)
        Else
            ToastNotification.Show(Me, "FALLO AL EXPORTACIÓN DE LISTA DE PRECIOS..!!!",
                                       My.Resources.WARNING, _DuracionSms * 1000,
                                       eToastGlowColor.Red,
                                       eToastPosition.BottomLeft)
        End If
    End Sub

    Private Sub btSalir_Click(sender As Object, e As EventArgs) Handles btSalir.Click
        _prSalir()
    End Sub
End Class