USE [master]
GO
/****** Object:  Database [ByteBarn]    Script Date: 3/20/2025 2:34:57 PM ******/
CREATE DATABASE [ByteBarn]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ByteBarn', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\ByteBarn.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ByteBarn_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\ByteBarn_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [ByteBarn] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ByteBarn].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ByteBarn] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ByteBarn] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ByteBarn] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ByteBarn] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ByteBarn] SET ARITHABORT OFF 
GO
ALTER DATABASE [ByteBarn] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ByteBarn] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ByteBarn] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ByteBarn] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ByteBarn] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ByteBarn] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ByteBarn] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ByteBarn] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ByteBarn] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ByteBarn] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ByteBarn] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ByteBarn] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ByteBarn] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ByteBarn] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ByteBarn] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ByteBarn] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ByteBarn] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ByteBarn] SET RECOVERY FULL 
GO
ALTER DATABASE [ByteBarn] SET  MULTI_USER 
GO
ALTER DATABASE [ByteBarn] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ByteBarn] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ByteBarn] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ByteBarn] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ByteBarn] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ByteBarn] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'ByteBarn', N'ON'
GO
ALTER DATABASE [ByteBarn] SET QUERY_STORE = ON
GO
ALTER DATABASE [ByteBarn] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [ByteBarn]
GO
/****** Object:  Table [dbo].[CustomerData]    Script Date: 3/20/2025 2:34:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerData](
	[customer_id] [int] NOT NULL,
	[customer_name] [nvarchar](50) NULL,
	[customer_email] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[customer_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductCustomer]    Script Date: 3/20/2025 2:34:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductCustomer](
	[customer_id] [int] NOT NULL,
	[product_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[customer_id] ASC,
	[product_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductInformation]    Script Date: 3/20/2025 2:34:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductInformation](
	[product_id] [int] NOT NULL,
	[product_name] [nvarchar](50) NULL,
	[product_quantity] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[product_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ProductCustomer]  WITH CHECK ADD FOREIGN KEY([customer_id])
REFERENCES [dbo].[CustomerData] ([customer_id])
GO
ALTER TABLE [dbo].[ProductCustomer]  WITH CHECK ADD FOREIGN KEY([product_id])
REFERENCES [dbo].[ProductInformation] ([product_id])
GO
/****** Object:  StoredProcedure [dbo].[AddProduct]    Script Date: 3/20/2025 2:34:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddProduct] 
	-- Add the parameters for the stored procedure here
	@ProductId int,
	@ProductName nvarchar(50),
	@ProductQuantity int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO ProductInformation (product_name, product_quantity, product_id)
	VALUES (@ProductName, @ProductQuantity, @ProductId);
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteProduct]    Script Date: 3/20/2025 2:34:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteProduct]
	@ProductId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE FROM ProductInformation
		WHERE product_id = @ProductId
END
GO
/****** Object:  StoredProcedure [dbo].[DisplayProductCustomer]    Script Date: 3/20/2025 2:34:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DisplayProductCustomer]
	-- Add the parameters for the stored procedure here
	--@customer_id int,
	--@product_id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    SELECT 
        CustomerData.customer_id AS CustomerId,
        CustomerData.customer_name AS CustomerName,
        ProductInformation.product_id AS ProductId,
        ProductInformation.product_name AS ProductName
    FROM ProductCustomer pc
    JOIN CustomerData ON pc.customer_id = CustomerData.customer_id
    JOIN ProductInformation ON pc.product_id = ProductInformation.product_id
END
GO
/****** Object:  StoredProcedure [dbo].[DisplayProducts]    Script Date: 3/20/2025 2:34:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DisplayProducts]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT
        ProductInformation.product_quantity AS ProductQuantity,
        ProductInformation.product_id AS ProductId,
        ProductInformation.product_name AS ProductName
	FROM ProductInformation
END
GO
/****** Object:  StoredProcedure [dbo].[GetProductByID]    Script Date: 3/20/2025 2:34:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetProductByID]
	-- Add the parameters for the stored procedure here
	@ProductId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM ProductInformation WHERE product_id = @ProductId
END
GO
/****** Object:  StoredProcedure [dbo].[GetTotalItems]    Script Date: 3/20/2025 2:34:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetTotalItems]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT
		COUNT(*) as Total_Items
	FROM ProductInformation
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateProduct]    Script Date: 3/20/2025 2:34:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateProduct]
	-- Add the parameters for the stored procedure here
	@ProductId int,
	@ProductName nvarchar(50),
	@ProductQuantity int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE ProductInformation 
		SET product_name = @ProductName, product_quantity = @ProductQuantity
		WHERE product_id = @ProductId

END
GO
USE [master]
GO
ALTER DATABASE [ByteBarn] SET  READ_WRITE 
GO
