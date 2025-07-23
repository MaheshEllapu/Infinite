-use Assessments

--1.	Write a query to display your birthday( day of week)
 select DATENAME(Weekday,'2003-06-06') as Birthday_DayOfWeek
 

--2.	Write a query to display your age in days
select DATEDIFF(Day,'2003-06-06',GetDate()) as AgeInDays
 

--3.	Write a query to display all employees information those who joined before 5 years in the current month
 
--(Hint : If required update some HireDates in your EMP table of the assignment)

create table Emp(
EmpNo int primary key,
E_Name varchar(20),
E_Job varchar(20),
Mgr_Id int,
Hire_Date date,
Sal decimal(10, 2),
Comm decimal(10, 2),
DeptNo int)

insert into Emp values
(7369, 'Smith', 'Clerk', 7902, '1980-12-17', 800, null, 20),
(7499, 'Allen', 'Salesman', 7698, '1981-02-20', 1600, 300, 30),
(7521, 'Ward', 'Salesman', 7698, '1981-02-22', 1250, 500, 30),
(7566, 'Jones', 'Manager', 7839, '1981-04-02', 2975, null, 20),
(7654, 'Martin', 'Salesman', 7698, '1981-09-28', 1250, 1400, 30),
(7698, 'Blake', 'Manager', 7839, '1981-05-01', 2850, null, 30),
(7782, 'Clark', 'Manager', 7839, '1981-06-09', 2450, null, 10),
(7788, 'Scott', 'Analyst', 7566, '1987-04-19', 3000, null, 20),
(7839, 'King', 'President', null, '1981-11-17', 5000, null, 10),
(7844, 'Turner', 'Salesman', 7698, '1981-09-08', 1500, 0, 30),
(7876, 'Adams', 'Clerk', 7788, '1987-05-23', 1100, null, 20),
(7900, 'James', 'Clerk', 7698, '1981-12-03', 950, null, 30),
(7902, 'Ford', 'Analyst', 7566, '1981-12-03', 3000, null, 20),
(7934, 'Miller', 'Clerk', 7782, '1982-01-23', 1300, null, 10)

select * from Emp

select * from Emp where Hire_Date <= DATEADD(Year,-5,GetDate())
 
 
--4.	Create table Employee with empno, ename, sal, doj columns or use your emp table and perform the following operations in a single transaction

create table Employee (
empno int primary key,
ename varchar(50),
sal decimal(10,2),
doj date)
  
begin transaction
 
declare @DeletedRows table (
empno int,
ename varchar(50),
sal decimal(10,2),
doj date)
 
 --	a. First insert 3 rows

insert into Employee (empno, ename, sal, doj) values
(201, 'Krishna', 30000, '2020-01-01'),
(202, 'Yeshu', 40000, '2021-02-15'),
(203, 'Mahi', 50000, '2022-03-20')
 
select * from Employee

--	b. Update the second row sal with 15% increment 

update Employee
set sal = sal + sal * 0.15
where empno = 202

select * from Employee
--  c. Delete first row.

delete from Employee
output DELETED.* into @DeletedRows
where empno = 201
 
select * from Employee
--After completing above all actions, recall the deleted row without losing increment of second row.

insert into Employee
select * from @DeletedRows
 
commit transaction

select * from Employee

 
--5.      Create a user defined function calculate Bonus for all employees of a  given dept using 	following conditions
--	a.     For Deptno 10 employees 15% of sal as bonus.
--	b.     For Deptno 20 employees  20% of sal as bonus
--	c      For Others employees 5%of sal as bonus

create or alter function fn_Calculate_Bonus(@deptno int)
returns @bonustable table(
empno int,
ename varchar(30), 
salary int, 
bonus_amount float, 
deptno int)
as
begin
	if(@deptno = 10)
	begin
		insert into @bonustable
		select empno, E_Name, Sal, (Sal * 0.15) 'Bonus amount', deptno from emp where deptno = @deptno;
	end
	else if(@deptno = 20)
		begin
		insert into @bonustable
		select empno, E_Name, Sal, (Sal * 0.2) 'Bonus amount', deptno from emp where deptno = @deptno;
	end
	else
		begin
		insert into @bonustable
		select empno, E_Name, Sal, (Sal * 0.05) 'Bonus amount', deptno from emp where deptno = @deptno;
	end
	return
end
 
select * from dbo.fn_Calculate_Bonus(10)
select * from dbo.fn_Calculate_Bonus(20)
select * from dbo.fn_Calculate_Bonus(30)


--6. Create a procedure to update the salary of employee by 500 whose dept name is Sales and current salary is below 1500 (use emp table)


create table Dept(
DeptNo int primary key,
D_Name varchar(20),
Loc varchar(20)
)

insert into Dept values
(10, 'accounting', 'new york'),
(20, 'research', 'dallas'),
(30, 'sales', 'chicago'),
(40, 'operations', 'boston')

select * from Dept

create procedure Update_Salary_Sales_Employees
as
begin
	update E
	set E.sal = E.sal + 500
	from emp E
	join dept d on E.DeptNo = d.DeptNo
	where d.D_Name = 'Sales' and E.Sal < 1500
end

exec Update_Salary_Sales_Employees

select * from Emp
