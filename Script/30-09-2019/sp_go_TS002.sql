USE [BDDiconDinoEco]
GO
/****** Object:  StoredProcedure [dbo].[sp_go_TS002]    Script Date: 30/09/2019 5:08:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


----------------------------------------------------------------------------------------------------------------------------------------------
ALTER PROCEDURE [dbo].[sp_go_TS002](@tipo int, @numi int=-1, @cia int=-1, @alm int=-1, @sfc int=-1, @autoriz decimal(18,0)=0,
									 @nfac int=-1, @key nvarchar(255)='', @fdel date=null, @fal date=null, @nota nvarchar(255)='',
									 @nota2 nvarchar(255)='', @est bit=0,  
									 @uact nvarchar(10)='',
									 @filtro INT=-1,@sbtipo int=-1,@inicio int=-1,@fin int=-1)
AS
BEGIN
	DECLARE @newHora nvarchar(5)
	set @newHora=CONCAT(DATEPART(HOUR,GETDATE()),':',DATEPART(MINUTE,GETDATE()))

	DECLARE @newFecha date
	set @newFecha=GETDATE()
	
	IF @tipo=-1 --ELIMINAR REGISTRO
	BEGIN
		BEGIN TRY 
			DELETE FROM TS002 WHERE sbnumi=@numi
			SELECT @numi AS newNumi
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum, baproc, balinea ,bamensaje, batipo, bafact, bahact, bauact)
				   VALUES(ERROR_NUMBER(), ERROR_PROCEDURE(), ERROR_LINE(), ERROR_MESSAGE(), -1, @newFecha, @newHora, @uact)
		END CATCH
	END

	IF @tipo=1 --NUEVO REGISTRO
	BEGIN
		BEGIN TRAN INSERTAR
		BEGIN TRY 
			set @numi=IIF((select COUNT(sbnumi) from TS002)=0, 0, (select MAX(sbnumi) from TS002))+1
			
			INSERT INTO TS002 VALUES(@numi, @cia, @alm, @sfc, @autoriz, @nfac, @key, @fdel, @fal, @nota, @nota2, @est,@sbtipo,@inicio ,@fin )

			-- DEVUELVO VALORES DE CONFIRMACION
			SELECT @numi AS newNumi
			COMMIT TRAN
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum, baproc, balinea, bamensaje, batipo, bafact, bahact, bauact)
				   VALUES(ERROR_NUMBER(), ERROR_PROCEDURE(), ERROR_LINE(), ERROR_MESSAGE(), 1, @newFecha, @newHora,@uact)
			ROLLBACK TRAN
		END CATCH
	END
	
	IF @tipo=2--MODIFICACION
	BEGIN
		BEGIN TRAN MODIFICACION
		BEGIN TRY

			UPDATE TS002 SET sbcia=@cia, sbalm=@alm, sbsfc=@sfc, sbautoriz=@autoriz, sbnfac=@nfac, sbkey=@key,
							 sbfdel=@fdel, sbfal=@fal, sbnota=@nota, sbnota2=@nota2, sbest=@est,
							 sbtipo =@sbtipo ,sbinicio =@inicio ,sbfinal =@fin 
					 Where sbnumi = @numi

			--DEVUELVO VALORES DE CONFIRMACION
			select @numi as newNumi
			COMMIT TRAN
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum, baproc, balinea, bamensaje, batipo, bafact, bahact, bauact)
				   VALUES(ERROR_NUMBER(), ERROR_PROCEDURE(), ERROR_LINE(), ERROR_MESSAGE(), 2, @newFecha, @newHora, @uact)
			ROLLBACK TRAN
		END CATCH
	END

	IF @tipo=3 --MOSTRAR TODOS
	BEGIN
		BEGIN TRY
			SELECT	a.sbnumi as numi, a.sbcia as cia,a.sbalm as alm, b.cadesc  as ALMACEN, a.sbsfc as sfc, a.sbautoriz as autoriz, a.sbnfac as nfac, 
					a.sbkey as [key], a.sbfdel as fdel, a.sbfal as fal, a.sbnota as nota, a.sbnota2 as nota2, a.sbest as est,
					isnull(a.sbtipo,1) as tipo ,isnull(a.sbinicio,0) as inicio,isnull(a.sbfinal,0) as final
			FROM 
				TS002 a inner join Sucursal as b on b.canumi =a.sbalm 
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum, baproc, balinea, bamensaje, batipo, bafact, bahact, bauact)
				   VALUES(ERROR_NUMBER(), ERROR_PROCEDURE(), ERROR_LINE(), ERROR_MESSAGE(), 3, @newFecha, @newHora, @uact)
		END CATCH
	END

	IF @tipo=4 --Listar compañias
	BEGIN
		BEGIN TRY
			SELECT	1 as cod, 'CIA PRINCIPAL' as [desc]
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum, baproc, balinea, bamensaje, batipo, bafact, bahact, bauact)
				   VALUES(ERROR_NUMBER(), ERROR_PROCEDURE(), ERROR_LINE(), ERROR_MESSAGE(), 4, @newFecha, @newHora, @uact)
		END CATCH
	END

	IF @tipo=5 --Listar almacenes
	BEGIN
		BEGIN TRY
			SELECT	a.canumi as cod, a.cadesc  as [desc]
			from Sucursal as a
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum, baproc, balinea, bamensaje, batipo, bafact, bahact, bauact)
				   VALUES(ERROR_NUMBER(), ERROR_PROCEDURE(), ERROR_LINE(), ERROR_MESSAGE(), 5, @newFecha, @newHora, @uact)
		END CATCH
	END


	IF @tipo=6 --Listar almacenes
	BEGIN
		BEGIN TRY
			SELECT	a.canumi as cod, a.cadesc  as [desc]
			from Sucursal a where a.canumi in (select b.sbalm  from TS002 as b )
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum, baproc, balinea, bamensaje, batipo, bafact, bahact, bauact)
				   VALUES(ERROR_NUMBER(), ERROR_PROCEDURE(), ERROR_LINE(), ERROR_MESSAGE(), 5, @newFecha, @newHora, @uact)
		END CATCH
	END

	IF @tipo=7 --Listar almacenes completo
	BEGIN
		BEGIN TRY
			SELECT	a.canumi , a.cadesc,a.caconcep1,a.caconcep2,a.caconcep3,a.caconcep4
			from Sucursal as a
			where canumi=@numi
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum, baproc, balinea, bamensaje, batipo, bafact, bahact, bauact)
				   VALUES(ERROR_NUMBER(), ERROR_PROCEDURE(), ERROR_LINE(), ERROR_MESSAGE(), 5, @newFecha, @newHora, @uact)
		END CATCH
	END

		IF @tipo=8 --Listar almacenes
	BEGIN
		BEGIN TRY
			SELECT	a.Id   as cod, a.Descripcion  as [desc]
			from Plantilla  as a
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum, baproc, balinea, bamensaje, batipo, bafact, bahact, bauact)
				   VALUES(ERROR_NUMBER(), ERROR_PROCEDURE(), ERROR_LINE(), ERROR_MESSAGE(), 5, @newFecha, @newHora, @uact)
		END CATCH
	END
END





