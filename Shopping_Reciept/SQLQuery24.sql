USE [Pr_Inventory]
GO
/****** Object:  StoredProcedure [dbo].[sp_updateproduct]    Script Date: 8/19/2024 6:06:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[sp_updateproduct]
	-- Add the parameters for the stored procedure here
	@id int,
	@name varchar(max),
	@detail varchar(max),
	@price int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	update Products SET name=@name, detail=@detail, price=@price where id like @id
END
