create database ElectricityBillDB
use ElectricityBillDB


create table ElectricityBill 
(
  ID int identity(1,1) primary key,        
  Consumer_Number varchar(20) not null,
  Consumer_Name   varchar(50) not null,
  Units_Consumed  int not null,
  Bill_Amount     float not null
)

select * from ElectricityBill
drop table ElectricityBill