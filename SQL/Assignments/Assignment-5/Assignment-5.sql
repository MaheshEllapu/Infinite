use Assignments

--1. Write a T-Sql based procedure to generate complete payslip of a given employee with respect to the following condition

--   a) HRA as 10% of Salary
--   b) DA as 20% of Salary
--   c) PF as 8% of Salary
--   d) IT as 5% of Salary
--   e) Deductions as sum of PF and IT
--   f) Gross Salary as sum of Salary, HRA, DA
--   g) Net Salary as Gross Salary - Deductions

--Print the payslip neatly with all details

create table Employee (
EmpId int primary key,
EName varchar(50),
Salary decimal(10,2))

insert into Employee values
(101, 'Mahesh', 60000.00),
(102, 'Yasaswini', 50000.00),
(103, 'Krishna Sai', 55000.00)

create or alter procedure GeneratePaySlip
@EmpId int
as
begin
declare @Name varchar(50)
declare @Salary decimal(10,2)
declare @HRA decimal(10,2)
declare @DA decimal(10,2)
declare @PF decimal(10,2)
declare @IT decimal(10,2)
declare @Deductions decimal(10,2)
declare @GrossSalary decimal(10,2)
declare @NetSalary decimal(10,2)

-- details
select @Name = EName, @Salary = Salary
from Employee
where EmpId = @EmpId

if @Name is null
begin
print 'Employee Not Found.'
return
end

-- components
set @HRA = 0.10 * @salary
set @DA = 0.20 * @salary
set @PF = 0.08 * @salary
set @IT = 0.05 * @salary
set @Deductions = @pf + @it
set @GrossSalary = @Salary + @HRA + @DA
set @NetSalary = @GrossSalary - @Deductions

print '         Employee Payslip       '
print 'Employee Id     : ' + cast(@EmpId as varchar)
print 'Employee Name   : ' + @Name
print 'Base Salary     : ' + cast(@Salary as varchar)
print 'HRA (10%)       : ' + cast(@HRA as varchar)
print 'DA (20%)        : ' + cast(@DA as varchar)
print 'PF (8%)         : ' + cast(@PF as varchar)
print 'IT (5%)         : ' + cast(@IT as varchar)
print 'Gross Salary    : ' + cast(@GrossSalary as varchar)
print 'Deductions      : ' + cast(@Deductions as varchar)
print 'Net Salary      : ' + cast(@NetSalary as varchar)
end

-- run:
exec generatepayslip @empid = 101

--2.  Create a trigger to restrict data manipulation on EMP table during General holidays.
--Display the error message like “Due to Independence day you cannot manipulate data” or "Due To Diwali, you cannot manipulate" etc.

--Note: Create holiday table with (holiday_date,Holiday_name). Store at least 4 holiday details.
--Try to match and stop manipulation 

create table Holidays (
Holiday_Date date primary key,
Holiday_Name varchar(100))

insert into Holidays values
('2025-01-26', 'Republic Day'),
('2025-08-15', 'Independence Day'),
('2025-10-02', 'Gandhi Jayanti'),
('2025-11-12', 'Diwali')

create or alter trigger Trg_BlockOnHoliday
on Emp
instead of insert, update, delete
as
begin
	--declare @today date =cast(getdate() as date);
	declare @Today date='2025-01-26'
    declare @Holiday_Name varchar(50);
 
    select @Holiday_Name = Holiday_Name 
    from Holidays 
    where Holiday_Date = @Today;

    if @Holiday_Name is not null
    begin
        raiserror('Due to %s, you cannot manipulate data', 16, 1, @Holiday_Name);
        rollback transaction;
    end
end

select * from Emp
--Testing
insert into Emp values(0498,'Mahesh','Software Engineer',7566,'2003-06-06',8700,null,10)

