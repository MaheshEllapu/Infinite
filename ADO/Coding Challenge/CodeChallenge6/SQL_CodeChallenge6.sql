use Assessments

create table Employee_Details (
EmpId int identity(1001,1) primary key,
EmpName varchar(100),
Gender varchar(10),
Salary decimal(10, 2),
NetSalary as (Salary - (Salary * 0.10))
)

--1. Write a stored Procedure that inserts records in the Employee_Details table
 
--The procedure should generate the EmpId automatically to insert and should return the generated value to the user
 
--Also the Salary Column is a calculated column (Salary is givenSalary - 10%)
 
--Table : Employee_Details (Empid, Name, Salary, Gender)
--Hint(User should not give the EmpId)
 
--Test the Procedure using ADO classes and show the generated Empid and Salary

create or alter procedure InsertEmployee
@GeneratedEmpid int output,
@Name varchar(100),
@Gender varchar(10),
@Salary decimal(10,2),
@NetSalary decimal(10,2) output
as
begin
insert into Employee_Details (EmpName, Gender, Salary)
values (@Name, @Gender, @Salary)
select @GeneratedEmpid = Scope_Identity()
select @NetSalary = Salary - (Salary * 0.10)
from Employee_Details
where Empid = @GeneratedEmpid
end

select * from Employee_Details


--2. Write a procedure that takes empid as input and outputs the updated salary as current salary + 100 for the give employee.
 
--  Test the procedure using ADO classes and display the Employee details of that employee whose salary has been updated

create or alter procedure UpdateEmployeeSalary
@EmpId int,
@Name varchar(100) output,
@Gender char(1) output,
@Updated_Salary int output,
@Net_Salary int output
as
begin
update Employee_Details
set Salary = Salary + 100
where EmpId = @EmpId;
 
select 
@Name = EmpName,
@Gender = Gender,
@Updated_Salary = salary,
@Net_Salary = Salary - (Salary / 10)
from Employee_Details
where empid = @empid;
end