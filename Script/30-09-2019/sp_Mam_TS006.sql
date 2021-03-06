USE [BDDiconDinoEco]
GO
/****** Object:  StoredProcedure [dbo].[sp_Mam_TS006]    Script Date: 24/09/2019 15:14:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--drop procedure sp_Mam_TS006
ALTER PROCEDURE [dbo].[sp_Mam_TS006] (@tipo int,@senumi int=-1,@senumiserv int=1,@senrocuenta nvarchar(20)='',@seest int=-1,@seuact nvarchar(10)='',
@categoria int=-1,@TS006 Mam_TS006TypeV2 Readonly)
AS
BEGIN
	DECLARE @newHora nvarchar(5)
	set @newHora=CONCAT(DATEPART(HOUR,GETDATE()),':',DATEPART(MINUTE,GETDATE()))

	DECLARE @newFecha date
	set @newFecha=GETDATE()

	IF @tipo=2--MODIFICACION
	BEGIN
		BEGIN TRY 
		
			INSERT INTO TS006 (senumiserv ,senrocuenta ,seest,seref ,sefactu)
			SELECT td.senumiserv ,LTRIM (RTRIM (td.senrocuenta)) ,td.seest,LTRIM (RTRIM (td.seref)),td.sefactu  FROM @TS006 AS td
			where td.estado =0 and td.senumiserv >0 

					--MODIFICO LOS REGISTROS
			UPDATE TS006
			SET senrocuenta =LTRIM (RTRIM (td.senrocuenta)) ,seref=td.seref,sefactu=td.sefactu
			FROM TS006  INNER JOIN @TS006  AS td
			ON TS006 .senumi      = td.senumi    and td.estado=2;

			--ELIMINO LOS REGISTROS
			DELETE FROM TS006 WHERE senumi    in (SELECT td.senumi   FROM @TS006 AS td WHERE td.estado=-1)
   

			select @senumi as newNumi
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),2,@newFecha,@newHora,@seuact)
		END CATCH
	END

	IF @tipo=3 --MOSTRAR TODOS
	BEGIN
		BEGIN TRY
	select a.senumi ,a.senumiserv ,servicio .yfcdprod1 as servicio,a.senrocuenta ,a.seref,a.seest,CAST(a.sefactu as bit) as  sefactu,1 as estado,'Sucursal Principal' as sucursal,
		   IIF(EXISTS(select * from tc001 where cacta=senrocuenta),1,0) as existe
	from TS006 as a inner join DBDinoMEco.dbo.TY005  as servicio 
	on a.senumiserv =servicio.yfnumi  
	and servicio .yfgr1  =@categoria   and a.seest =1

	union

		select 0 as senumi ,servicio.yfnumi as senumiserv ,servicio .yfcdprod1 as servicio,0 as senrocuenta ,'' as seref,0 as seest,CAST(0 as bit) as  sefactu,1 as estado,'Sucursal Principal' as sucursal,
		   0 as existe
	from  DBDinoMEco.dbo.TY005  as servicio 
	where  servicio .yfgr1  =@categoria  and 
	servicio.yfnumi not in (select a.senumiserv from TS006 as a where a.seest =1 )

	 

		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@seuact)
		END CATCH

END

IF @tipo=4 --MOSTRAR CATEGORIAS
	BEGIN
		BEGIN TRY
select yccod3 as cenum,ycdes3 as cedesc1 from 
DBDinoMEco .dbo.TY0031 as gr1 where gr1.yccod1 =1 and gr1.yccod2 =1 

		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@seuact)
		END CATCH

END

IF @tipo=5 --MOSTRAR CATEGORIAS VENTA
	BEGIN
		BEGIN TRY
	SELECT  cenum, cedesc1
FROM DBDies.dbo.TC0051
WHERE  (cecod1 = 14) AND (cecod2 = 3)
order by cenum asc
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@seuact)
		END CATCH

END


End






