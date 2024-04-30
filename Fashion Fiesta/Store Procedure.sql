select * from [dbo].[Brands]
select * from [dbo].[DressCategories]
select * from [dbo].[Dresses]
select * from [dbo].[Stocks]
go

--insert dress

create proc spInsertDress @Name nvarchar(50),
						  @LaunchDate datetime,
						  @SaleStatus bit,
						  @Picture nvarchar(max),
						  @DressCategoryID int,
						  @BrandID int 
as
begin
  insert into Dresses(Name,LaunchDate,SaleStatus,Picture,DressCategoryID,BrandID)
  values(@Name,@LaunchDate,@SaleStatus,@Picture,@DressCategoryID,@BrandID)
  select SCOPE_IDENTITY() as DressID
  return
end
go
--test spinsertdress
exec spInsertDress 'Pink Kamiz','11-11-2023',1,'1.jpg',1,1
go

--for insert stocks
create proc spInsertStock @Size int,
						  @Price decimal(18,0),
						  @Quantity int,
						  @DressID int
as
begin
  insert into Stocks(Size,Price,Quantity,DressID)
  values(@Size,@Price,@Quantity,@DressID)
  select SCOPE_IDENTITY() as StockID
  return
end
go

create proc spDeleteStock @DressID int
as
begin
 delete from Stocks
 where DressID=@DressID
 return
end
go