create database Assessments
use Assessments
-------------------------------------- QUESTION 1 --------------------------------------
create table Books
(ID int primary key,
Title varchar(50),
Author varchar(50),
ISBN varchar(30) unique,
Published_Date datetime) 

insert into books (ID,Title,Author,ISBN,Published_Date) values
(1,'My First SQL Book','Mary Parker',981483029127,'2012-02-22 12:08:17'),
(2,'My Second SQL Book','John Mayer',857300923713,'1972-07-03 09:22:45'),
(3,'My Third SQL Book','Cary Flint',523120967812,'2015-10-18 14:05:44')

--Write a query to fetch the details of the books written by author whose name ends with er.
select * from books where author like '%er'

create table reviews
(ID int,
Book_ID int,
Reviewer_Name varchar(50),
Content varchar(50),
Rating int,
Published_Date datetime)

insert into reviews (ID,Book_ID,Reviewer_Name,Content,Rating,Published_Date) values
(1,1,'John Smith','My First Review',4,'2017-12-05 05:50:11'),
(2,2,'John Smith','My Second Review',5,'2017-10-13 15:05:12'),
(3,2,'Alice Walker','Another Review',1,'2017-10-22 23:47:10')

--Display the Title ,Author and ReviewerName for all the books from the above table.
select b.Title, b.Author, r.Reviewer_Name from Books b join reviews r On b.id=r.Book_ID

--Display the reviewer name who reviewed more than one book.
select Reviewer_Name from reviews group by Reviewer_Name having count(distinct book_id)>1



-------------------------------------- QUESTION 2 --------------------------------------

create table Customers_table
(ID int primary key,
C_Name varchar(50),
C_Age int,
C_Address varchar(100),
C_Salary decimal(10,2))

select * from customers_table

insert into Customers_table (ID,C_Name,C_Age,C_Address,C_Salary) values
(1,'Ramesh',32,'Ahmedabad',2000.00),
(2,'Khilan',25,'Delhi',1500.00),
(3,'Kaushik',23,'Kota',2000.00),
(4,'Chaitali',25,'Mumbai',6500.00),
(5,'Hardik',27,'Bhopal',8500.00),
(6,'Komal',22,'MP',4500.00),
(7,'Muffy',24,'Indore',10000.00)

--Display the Name for the customer from above customer table who live in same address which has character o anywhere in address
select C_Name from Customers_table where C_Address like '%o%'

create table Orders_table
(OID int primary key,
Order_Date datetime,
Customer_ID int,
Amount decimal(10,2),
Foreign key(Customer_ID) references customers_table(ID))

 insert into Orders_table(OID,Order_Date,Customer_ID,Amount) values
 (102,'2009-10-08 00:00:00',3,3000),
 (100,'2009-10-08 00:00:00',3,1500),
 (101,'2009-11-20 00:00:00',2,1560),
 (103,'2008-05-20 00:00:00',4,2060)

 --Write a query to display the Date,Total no of customer placed order on same Date
 select Order_Date, count(Customer_ID) as Total_Customers from Orders_table group by Order_Date

-------------------------------------- QUESTION 3 --------------------------------------

create table Employee_Table
(ID int primary key,
E_Name varchar(30),
E_Age int,
E_Address varchar(50),
Salary decimal(10,2))

insert into Employee_Table(ID,E_Name,E_Age,E_Address,Salary) values
(1,'Ramesh',32,'Ahmedabad',2000.00),
(2,'Khilan',25,'Delhi',1500.00),
(3,'Kaushik',23,'Kota',2000.00),
(4,'Chaitali',25,'Mumbai',6500.00),
(5,'Hardik',27,'Bhopal',8500.00),
(6,'Komal',22,'MP',null),
(7,'Muffy',24,'Indore',null)

--Display the Names of the Employee in lower case, whose salary is null
select lower(E_Name) as EmployeeName from Employee_Table where Salary is null

-------------------------------------- QUESTION 4 --------------------------------------

create table StudentDetails
(RegisterNo int primary key,
S_Name varchar(30),
S_Age int,
S_Qualification varchar(20),
S_MobileNo varchar(15),
S_MailID varchar(50),
S_Location varchar(50),
S_Gender char(1))

insert into StudentDetails(RegisterNo,S_Name,S_Age,S_Qualification,S_MobileNo,S_MailID,S_Location,S_Gender) values
(2,'Sai',22,'B.E',9952836777,'Sai@gmail.com','Chennai','M'),
(3,'Kumar',20,'BSC',7890125648,'Kumar@gmail.com','Madhurai','M'),
(4,'Selvi',22,'B.Tech',8904567342,'Selvi@gmail.com','Selam','F'),
(5,'Nisha',25,'M.E',7834672310,'Nisha@gmail.com','Theni','F'),
(6,'SaiSaran',21,'B.A',7890345678,'Saran@gmail.com','Madhurai','F'),
(7,'Tom',23,'BCA',8901234675,'Tom@gmail.com','Pune','M')

--Write a sql server query to display the Gender,Total no of male and female from the above relation
Select S_Gender,count(*) as Total from StudentDetails group by S_Gender
