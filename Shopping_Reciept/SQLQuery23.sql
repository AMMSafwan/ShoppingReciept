USE [Pr_Inventory]
GO
/****** Object:  StoredProcedure [dbo].[sp_insertproduct]    Script Date: 8/19/2024 6:06:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[sp_insertproduct] 
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
	insert into Products (id, name, detail, price)
	values (@id, @name, @detail, @price)
END
