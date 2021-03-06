USE [BDDiconDinoEco]
GO
/****** Object:  StoredProcedure [dbo].[sp_Mam_Asiento]    Script Date: 30/09/2019 5:33:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--drop procedure sp_Mam_TS006
ALTER PROCEDURE [dbo].[sp_Mam_Asiento] (@tipo int,@seuact nvarchar(10)='',@categoria int=-1,@canumi int=-1,
@cuenta nvarchar(20)='',@descripcion nvarchar(200)='',@empresa int=-1,@sector int=-1,@vcnumi int=-1,@servicio int=-1,@fechaI date=null,
@fechaF date=null,@sucursal int=-1,@Estado int=-1, @tventa int=-1,@Id int=-1)
AS
BEGIN
	DECLARE @newHora nvarchar(5)
	set @newHora=CONCAT(DATEPART(HOUR,GETDATE()),':',DATEPART(MINUTE,GETDATE()))

	DECLARE @newFecha date
	set @newFecha=GETDATE()
IF @tipo=10 --MOSTRAR CUENTAS
	BEGIN
		BEGIN TRY	
  select cuenta .canumi ,cuenta .cacta as nro,cuenta .cadesc ,b.Porcentaje  as chporcen,b.Debe as  chdebe ,b.Haber  as chhaber,cast(null as decimal (18,2)) as tc
   ,cast(null as decimal (18,2)) as debe,cast(null as decimal (18,2)) as haber,cast(null as decimal (18,2)) as debesus
   ,cast(null as decimal (18,2)) as habersus,cast(null as int) as variable,cast(null as int) as linea
  from Plantilla  as a 
  inner join DetallePlantilla  as b on a.Id  =b.PlantillaId   
  inner join TC001 as cuenta on cuenta.canumi =b.CuentaId 
  where a.Id =@Id 
 

		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@seuact)
		END CATCH

END
IF @tipo=12 --BUSCAR PADRE
	BEGIN
		BEGIN TRY	
select padre.canumi ,padre .cacta as nro ,padre .cadesc ,0 as chporcen,0 as chdebe ,0 as chhaber ,cast(null as decimal (18,2)) as
 debe,cast(null as decimal (18,2)) as haber,cast(null as int) as variable,cast(null as int) as linea
  from TC001 as cuenta,TC001 as padre,TC0071 as b where cuenta.canumi =b.chnumitc1 
  and cuenta .capadre =padre.canumi  
  and cuenta.canumi =@canumi 
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@seuact)
		END CATCH

END

IF @tipo=11 --SUMAR TOTAL DE VENTAS POR CATEGORIA
	BEGIN
		BEGIN TRY	
  select  Isnull(Sum(b.vdtotdesc),0) as total
  from TV002 as a inner join TV0021 as b on a.vcnumi =b.vdvc2numi 
  and a.vcsector =@categoria and a.vcest =0
  and a.vcfdoc >=@fechaI and a.vcfdoc <=@fechaF 
  and a.vcidcore <=0
  and a.vcalm =@sucursal
  and (ISNULL((select top 1 sefactu from TS006 where senumiserv=b.vdserv),1))=1 --aumentado por mi
  inner join TFV001 as factura on factura.fvanumi =a.vcnumi and factura.fvaest in (1,4)
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@seuact)
		END CATCH

END

IF @tipo=110101 --SUMAR TOTAL DE VENTAS POR CATEGORIA para totales de ventas y tome en cuenta las ventas por recibo
	BEGIN
		BEGIN TRY	
  select  Isnull(Sum(b.vdtotdesc),0) as total
  from TV002 as a inner join TV0021 as b on a.vcnumi =b.vdvc2numi 
  and a.vcsector =@categoria and a.vcest =0
  and a.vcfdoc >=@fechaI and a.vcfdoc <=@fechaF 
  and a.vcidcore <=0
  and a.vcalm =@sucursal and
  IIF(exists(select x.fvanumi from TFV001 x where a.vcnumi=x.fvanumi) ,(select top 1 y.fvaest from TFV001 y where a.vcnumi=y.fvanumi),1) in (1,4)
  
  -- and (ISNULL((select top 1 sefactu from TS006 where senumiserv=b.vdserv),1))=1 --aumentado por mi
  --inner join TFV001 as factura on factura.fvanumi =a.vcnumi and factura.fvaest =1


 ---- UNION
 ---- select  Isnull(Sum(b.vdtotdesc),0) as total
 ---- from TV002 as a inner join TV0021 as b on a.vcnumi =b.vdvc2numi 
 ---- and a.vcsector =@categoria and a.vcest =0
 ---- and a.vcfdoc >=@fechaI and a.vcfdoc <=@fechaF 
 ---- and a.vcidcore <=0
 ---- and a.vcalm =@sucursal and
 ---- a.vcnumi not in(select x.fvanumi from TFV001 as x )
 ------ and (ISNULL((select top 1 sefactu from TS006 where senumiserv=b.vdserv),1))=1 --aumentado por mi
 ------ inner join TFV001 as factura on factura.fvanumi =a.vcnumi and factura.fvaest =1
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@seuact)
		END CATCH

END

IF @tipo=13  -------Lavadero  Total Servicio
	BEGIN
		BEGIN TRY	
 select   Isnull(Sum(b.vdtotdesc),0)  as total
  from TV002 as a inner join TV0021 as b on a.vcnumi =b.vdvc2numi 
  and a.vcsector =@sector and b.vdserv >0
    and a.vcfdoc >=@fechaI and a.vcfdoc <=@fechaF 
  and a.vcidcore <=0 and a.vcalm =@sucursal 
  --inner join TFV001 as factura on factura.fvanumi =a.vcnumi and factura.fvaest =1 --esto para que tome en cuenta a los recibos
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@seuact)
		END CATCH

END
IF @tipo=14  -------Lavadero  Total Productos
	BEGIN
		BEGIN TRY	
  select   Isnull(Sum(b.vdtotdesc),0) as total 
  from TV002 as a inner join TV0021 as b on a.vcnumi =b.vdvc2numi 
  and a.vcsector =3 and b.vdprod >0 
 inner join TFV001 as factura on factura.fvanumi =a.vcnumi and factura.fvaest in (1,4)
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@seuact)
		END CATCH

END
IF @tipo=15  -------Listar Cuentas Servicios Lavadero
	BEGIN
		BEGIN TRY	
    select distinct hijo.canumi , a.senrocuenta ,a.seref ,a.seest 
from TS006 as a inner join DBDies.dbo.TCE004 as servicios on a.senumiserv =servicios .ednumi 
and servicios .edtipo =3
inner join TC001 as hijo on hijo .cacta =a.senrocuenta 
inner join TC001 as padre on padre.canumi =hijo.capadre 
and padre.cacta =@cuenta 
and padre.caemp =@empresa   ---Emrpresa
union

select distinct hijo.canumi,a.senrocuenta ,a.seref ,a.seest 
from TS006 as a inner join DBDies.dbo.TCL003  as servicios on a.senumiserv  =servicios .ldnumi  
and a.seest =2
inner join TC001 as hijo on hijo .cacta =a.senrocuenta 
inner join TC001 as padre on padre.canumi =hijo.capadre 
and padre .cacta =@cuenta 
and padre.caemp =@empresa ---Empresa
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@seuact)
		END CATCH

END

IF @tipo=16  -------Total Por Una Cuenta
	BEGIN
		BEGIN TRY	
    select isnull(sum (b.vdtotdesc ),0) as total,isnull(sum(b.vdcmin),0) as cantidad
   from TV002 as a inner join TV0021 as b on a.vcnumi =b.vdvc2numi and b.vdserv >0 
   inner join TS006 as cuenta on cuenta .senumiserv =b.vdserv 
   and cuenta.senrocuenta =@cuenta and cuenta.seref =@descripcion and cuenta .seest =1
   and a.vcfdoc >=@fechaI and a.vcfdoc <=@fechaF 
    and a.vcidcore <=0 and a.vcalm =@sucursal and a.vcsector >0
	inner join TFV001 as factura on factura.fvanumi =a.vcnumi and factura.fvaest in (1,4)
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@seuact)
		END CATCH

END

IF @tipo=17 
	BEGIN
		BEGIN TRY	
   select distinct padre.canumi,padre.caemp ,padre.cacta ,padre.cadesc ,padre.caniv ,padre .camon ,padre .catipo ,padre .capadre 
from TS006 as a inner join DBDies.dbo.TCE004 as servicios on a.senumiserv =servicios .ednumi 
and servicios .edtipo =3
inner join TC001 as hijo on hijo .cacta =a.senrocuenta 
inner join TC001 as padre on padre.canumi =hijo.capadre 
and padre.caemp =@empresa  ---Emrpresa
union

select distinct padre.canumi,padre.caemp ,padre.cacta ,padre.cadesc ,padre.caniv ,padre .camon ,padre .catipo ,padre .capadre 
from TS006 as a inner join DBDies.dbo.TCL003  as servicios on a.senumiserv  =servicios .ldnumi  
and a.seest =2
inner join TC001 as hijo on hijo .cacta =a.senrocuenta 
inner join TC001 as padre on padre.canumi =hijo.capadre 
and padre.caemp =@empresa ---Empresa

order by padre.canumi asc

 
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@seuact)
		END CATCH

END

IF @tipo=18  -------Total Producto
	BEGIN
		BEGIN TRY	
  select   Isnull(Sum(b.vdtotdesc),0) as total 
  from TV002 as a inner join TV0021 as b on a.vcnumi =b.vdvc2numi and b.vdprod >0
  inner join DBDies .dbo.TCL003 as producto on producto.ldnumi =b.vdprod 
  inner join TS006 as cuenta on cuenta.seest =2 and cuenta.senumiserv =producto .ldgr1  
  and cuenta.senrocuenta =@cuenta and cuenta.seref =@descripcion  and b.vdprod >0 
  and cuenta.seest =2
 and a.vcfdoc >=@fechaI and a.vcfdoc <=@fechaF 
 and a.vcalm =@sucursal 
  and a.vcidcore <=0
  inner join TFV001 as factura on factura.fvanumi =a.vcnumi and factura.fvaest in (1,4)
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@seuact)
		END CATCH

END
IF @tipo=19  -------Obtener Nombre de la Cuenta
	BEGIN
		BEGIN TRY	
  select   a.cadesc 
  from TC001 as a where a.cacta =@cuenta
 
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@seuact)
		END CATCH

END


IF @tipo=20  -------Obtener Nombre de la Cuenta
	BEGIN
		BEGIN TRY	
select distinct padre.canumi,padre.caemp ,padre.cacta ,padre.cadesc ,padre.caniv ,padre .camon ,padre .catipo ,padre .capadre 
from TS006 as a inner join DBDies.dbo.TCE004 as servicios on a.senumiserv =servicios .ednumi 
and servicios .edtipo =@sector 
inner join TC001 as hijo on hijo .cacta =a.senrocuenta 
inner join TC001 as padre on padre.canumi =hijo.capadre 
and a.seest =1
and padre.caemp =@empresa   ---Emrpresa
and servicios .edsuc =@sucursal 
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@seuact)
		END CATCH

END

IF @tipo=21  -------Obtener Servicios Cuentas
	BEGIN
		BEGIN TRY	
    select distinct hijo.canumi, a.senrocuenta ,a.seref ,a.seest
from TS006 as a inner join DBDies.dbo.TCE004 as servicios on a.senumiserv =servicios .ednumi 
and servicios .edtipo =@sector
inner join TC001 as hijo on hijo .cacta =a.senrocuenta 
inner join TC001 as padre on padre.canumi =hijo.capadre 
and padre.cacta =@cuenta 
and padre.caemp =@empresa   ---Emrpresa 
and servicios .edsuc =@sucursal 
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@seuact)
		END CATCH

END

IF @tipo=22  -------Obtener VENTAS DE PAGOS DE CUOTAS DE LOS SOCIOS de
	BEGIN
		BEGIN TRY	
select a.vcnumi,Concat(socio.cfapat ,' ',socio .cfamat ,' ',socio .cfnom )as socio,
(select sum(b.vdtotdesc )   from TV0021 as b where b.vdserv =nrocuen .senumiserv  and b.vdvc2numi  =a.vcnumi )as total
from TV002 as a ,DBDies .dbo.TCS01 as socio,TS006 as nrocuen ,TFV001 as factura where 
 a.vcsector =2 and a.vcnumi in (select b.vdvc2numi   from TV0021 as b where b.vdserv =senumiserv  and b.vdvc2numi  =a.vcnumi )
 and socio.cfnumi =a.vcclie 
 and nrocuen.senrocuenta =@cuenta 
 and a.vcfdoc >=@fechaI and a.vcfdoc <=@fechaF 
  and a.vcidcore <=0
  and a.vcalm =@sucursal 
  and factura.fvanumi =a.vcnumi and factura.fvaest in (1,4)
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@seuact)
		END CATCH

END

IF @tipo=23  -------Obtener DETALLE DE PAGOS DE CUOTAS SOCIOS
	BEGIN
		BEGIN TRY	
 select cuenta.senumi,UPPER(DateName( month , DateAdd( month , cuenta.semes , -1 ) ))as mes ,cuenta .seano 
 from DBDies .dbo .TCS014 as cuenta
 inner join TV0021 as detalle on detalle .vdprod =cuenta.selin 
 and detalle .vdserv =1
 inner join TV002 as a on a.vcnumi =detalle .vdvc2numi 
 and vcnumi =@vcnumi and a.vcfdoc >=@fechaI and a.vcfdoc <=@fechaF 
  and a.vcidcore <=0
  and a.vcalm =@sucursal 
 order by semes ,seano asc
 
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@seuact)
		END CATCH

END

IF @tipo=24  -------Obtener DETALLE DE PAGOS DE CUOTAS SOCIOS
	BEGIN
		BEGIN TRY	
 select cuenta.sfnumi ,cuenta .sfgestion  
 from DBDies .dbo .TCS015 as cuenta
 inner join TV0021 as detalle on detalle .vdprod =cuenta.sflin 
 and detalle .vdserv =2
 inner join TV002 as a on a.vcnumi =detalle .vdvc2numi 
 and vcnumi =@vcnumi
 and a.vcfdoc >=@fechaI and a.vcfdoc <=@fechaF 
  and a.vcidcore <=0
  and a.vcalm =@sucursal 
 order by sfgestion  asc
 
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@seuact)
		END CATCH

END
IF @tipo=25  -------Obtener Servicios Cuentas
	BEGIN
		BEGIN TRY	
    select distinct hijo.canumi,a.senrocuenta ,a.seref ,a.seest ,a.senumiserv,ISNULL(a.sefactu,1) as sefactu
from TS006 as a inner join DBDies.dbo.TCE004 as servicios on a.senumiserv =servicios .ednumi 
and servicios .edtipo =@sector
inner join TC001 as hijo on hijo .cacta =a.senrocuenta 
inner join TC001 as padre on padre.canumi =hijo.capadre 
and padre.cacta =@cuenta 
and padre.caemp =@empresa   ---Emrpresa
and servicios .edsuc =@sucursal 
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@seuact)
		END CATCH

END

IF @tipo=26  -------Obtener Cuenta Diferencia  de Cambio
	BEGIN
		BEGIN TRY	
    select hijo.canumi ,hijo.cacta,hijo.cadesc 
	from TC001 as hijo where hijo.canumi =@cuenta 
 union 
 select padre.canumi ,padre.cacta ,padre .cadesc 
 from TC001 as padre inner join TC001 as hijo 
 on hijo.capadre =padre.canumi 
 and hijo.canumi =@cuenta
 order by canumi asc
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@seuact)
		END CATCH

END

IF @tipo=27 --SUMAR TOTAL DE VENTAS POR CATEGORIA
	BEGIN
		BEGIN TRY	
  select distinct  a.vcnumi 
  from TV002 as a inner join TV0021 as b on a.vcnumi =b.vdvc2numi 
  and a.vcsector =@categoria and a.vcest =0
  and a.vcfdoc >=@fechaI and a.vcfdoc <=@fechaF 
   and a.vcidcore <=0
   and a.vcalm =@sucursal 
   inner join TFV001 as factura on factura.fvanumi =a.vcnumi and factura.fvaest in (1, 4)
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@seuact)
		END CATCH

END
IF @tipo=28
	BEGIN
		BEGIN TRY	

			select Concat(cliente.hanom ,' ALQ. CAB ',cabana.hbnumi  ,' DEL ',
Day(venta.hdfcin) ,' AL ',day(venta.hdfcou),' ',Datename(month,venta.hdfcou) ) as descripcion   ,vdtotdesc  as total
			
		from TV002 as a inner join DBDies .dbo.TCH001    as cliente on cliente .hanumi    =a.vcclie 
		 and a.vcsector =-10
		 inner join TV0021 as detalle on detalle .vdvc2numi =a.vcnumi
		 inner join DBDies .dbo.TCH003 as venta on venta.hdnumi =detalle.vdprod --a.vcSecNumi 
		 inner join DBDies .dbo.TCH002 as cabana on cabana .hbnumi =venta.hdtc2cab 
		 and a.vcidcore <=0
		 and a.vcfdoc >=@fechaI and a.vcfdoc <=@fechaF 
		 and a.vcalm =@sucursal 
		 
		 inner join TFV001 as factura on factura.fvanumi =a.vcnumi and factura.fvaest in (1,4)
		where a.vcest >=0
		--AUMENTADO PARA QUE PUEDAN TOMAR EN CUENTA VENTAS QUE NO VENGAN DE CABAÑAS SI NO SEAN SOLAMENTE SERVICIOS
		union
select 'ALQUILER CABAÑA' as descripcion   ,vdtotdesc  as total
			
		from TV002 as a 
		 inner join TV0021 as detalle on detalle .vdvc2numi =a.vcnumi 
		 inner join TFV001 as factura on factura.fvanumi =a.vcnumi and factura.fvaest in (1,4)
		where a.vcest >=0 and a.vcsector =-10 and a.vcidcore <=0
		 and a.vcfdoc >=@fechaI  and a.vcfdoc <=@fechaF  
		 and a.vcalm =@sucursal
		 and a.vcalm > 1  -- Cambio Carlos 30 05 18
	

		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@seuact)
		END CATCH

END
IF @tipo=29  -------Total Por Una Cuenta
	BEGIN
		BEGIN TRY	
    select isnull(sum (b.vdtotdesc ),0) as total,isnull(sum(b.vdcmin),0) as cantidad
   from TV002 as a inner join TV0021 as b on a.vcnumi =b.vdvc2numi and b.vdserv >0 and a.vcfactanul=1
   inner join TS006 as cuenta on cuenta .senumiserv =b.vdserv 
   and cuenta.senrocuenta =@cuenta and cuenta.seref =@descripcion and cuenta .seest =1
   and a.vcfdoc >=@fechaI and a.vcfdoc <=@fechaF 
    and a.vcidcore <=0 and a.vcalm =@sucursal and a.vcsector >0
	and a.vcsector=@sector
	--inner join TFV001 as factura on factura.fvanumi =a.vcnumi and factura.fvaest =1--comentado para que tome en cuenta las venta con recibo solamente
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@seuact)
		END CATCH

END



IF @tipo=30 --SUMAR TOTAL DE VENTAS POR CATEGORIA CLIENTES POR COOBRAR
	BEGIN
		BEGIN TRY	
  select  Isnull(Sum(b.vdtotdesc),0) as total 
  from TV002 as a inner join TV0021 as b on a.vcnumi =b.vdvc2numi 
  and a.vcsector =@categoria and a.vcest =0
  and a.vcfdoc >=@fechaI and a.vcfdoc <=@fechaF 
  and a.vcidcore <=0
  and a.vcalm =@sucursal 
  and a.vctipo =0
  inner join TFV001 as factura on factura.fvanumi =a.vcnumi and factura.fvaest in (1,4)
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@seuact)
		END CATCH

END

IF @tipo=31 --SUMAR TOTAL DE VENTAS POR CATEGORIA CLIENTES POR COOBRAR
	BEGIN
		BEGIN TRY	
  select clienteCobrar .cjnumi ,clienteCobrar .cjnombre , Isnull(Sum(b.vdtotdesc),0) as total 
  from TV002 as a inner join TV0021 as b on a.vcnumi =b.vdvc2numi 
  and a.vcsector =@categoria and a.vcest =0
  and a.vcfdoc >=@fechaI and a.vcfdoc <=@fechaF 
  and a.vcidcore <=0
  and a.vcalm =@sucursal 
  and a.vctipo =0
  inner join TC009 as clienteCobrar on clienteCobrar .cjnumi=a.vcclietc9 
  inner join TFV001 as factura on factura.fvanumi =a.vcnumi and factura.fvaest in (1,4)
  group  by clienteCobrar .cjnumi ,clienteCobrar .cjnombre 
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@seuact)
		END CATCH

END
IF @tipo=32 --SUMAR TOTAL DE VENTAS POR CATEGORIA 
	BEGIN
		BEGIN TRY	
  select  Isnull(Sum(b.vdtotdesc),0) as total,isnull(sum(b.vdcmin),0) as cantidad
  from TV002 as a inner join TV0021 as b on a.vcnumi =b.vdvc2numi 
  and a.vcsector =@categoria and a.vcest =0
  and a.vcfdoc >=@fechaI and a.vcfdoc <=@fechaF 
  and a.vcidcore <=0
  and a.vcalm =@sucursal 
  and a.vctipo = @tventa  --Tipo de Venta
  and IIF(exists(select x.fvanumi from TFV001 x where a.vcnumi=x.fvanumi) ,(select top 1 y.fvaest from TFV001 y where a.vcnumi=y.fvanumi),1) in (1,4)

  --inner join TFV001 as factura on factura.fvanumi =a.vcnumi and factura.fvaest =1 --comentado para que tome en cuenta recibos
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@seuact)
		END CATCH

END

IF @tipo=322 --SUMAR TOTAL DE VENTAS POR CATEGORIA 
	BEGIN
		BEGIN TRY	
    select  d.canumi, c.cacuenta, c.canombre,  Isnull(Sum(b.vdtotdesc),0) as total,isnull(sum(b.vdcmin),0) as cantidad
  from TV002 as a inner join TV0021 as b on a.vcnumi =b.vdvc2numi 
  inner join dbdies.dbo.ba001 c on a.vcbanco = c.canumi
  inner join TC001 d on c.cacuenta = d.cacta
  and a.vcsector =@categoria and a.vcest =0
  and a.vcfdoc >=@fechai and a.vcfdoc <= @fechaf 
  and a.vcidcore <=0
  and a.vcalm = @sucursal
  and a.vctipo = @tventa  --Tipo de Venta
  and 1=IIF(exists(select x.fvanumi from TFV001 x where a.vcnumi=x.fvanumi) ,(select top 1 y.fvaest from TFV001 y where a.vcnumi=y.fvanumi),1)
  group by d.canumi, c.cacuenta, c.canombre
  --inner join TFV001 as factura on factura.fvanumi =a.vcnumi and factura.fvaest =1 --comentado para que tome en cuenta recibos
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@seuact)
		END CATCH

END

IF @tipo=33 --NumiCuentaPorCobrar
	BEGIN
		BEGIN TRY	
select a.NumiCuenta as cuenta,cuenta .cacta as nro,cuenta .cadesc as descripcion,
padre.canumi as cuentapadre,padre.cacta as nropadre,padre.cadesc as descripcionpadre
from SY000 as a 
inner join TC001 as cuenta on cuenta.canumi =a.NumiCuenta 
inner join TC001 as padre on padre.canumi =cuenta .capadre 
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@seuact)
		END CATCH

END

	IF @tipo=34 --preguntar si es un servicio
	BEGIN
		BEGIN TRY	
			select a.esnumi,a.esnumitce4,a.esnumitc1,b.cacta,b.cadesc,a.esmonto
			from DBDies.dbo.TCE0043 a,TC001 b
			where a.esnumitc1=b.canumi
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
					VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@seuact)
		END CATCH		

	END

End






