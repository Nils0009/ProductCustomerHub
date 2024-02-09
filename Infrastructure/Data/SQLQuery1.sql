CREATE TABLE Categories (
	Id int not null identity primary key,
	CategoryName nvarchar(100) not null unique
)

CREATE TABLE Manufacturer(
	Id int not null identity primary key,
	Manufacturer nvarchar(200) not null unique
)

CREATE TABLE Prices(
	Id int not null identity primary key,
	UnitPrice money not null,
)

CREATE TABLE Products (
	ArticleNumber nvarchar(20) not null primary key,
	Title nvarchar(250) not null,
	Description nvarchar(max) not null,
	CategoryId int not null references Categories(Id),
	ManufacturerId int not null references Manufacturer(Id),
	PriceId int not null references Prices(Id),

)

