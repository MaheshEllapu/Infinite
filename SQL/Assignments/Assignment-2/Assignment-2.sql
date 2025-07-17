use Assignments

create table Dept(
DeptNo int primary key,
D_Name varchar(20),
Loc varchar(20)
);

create table Emp(
EmpNo int primary key,
E_Name varchar(20),
E_Job varchar(20),
Mgr_Id int,
Hire_Date date,
Sal decimal(10, 2),
Comm decimal(10, 2),
DeptNo int,
foreign key (deptno) references dept(deptno))

insert into Dept values
(10, 'accounting', 'new york'),
(20, 'research', 'dallas'),
(30, 'sales', 'chicago'),
(40, 'operations', 'boston')

select * from Dept

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

--1. List all employees whose name begins with 'A'. 
select * from Emp where E_Name like 'A%'


--2. Select all those employees who don't have a manager. 
select * from Emp where Mgr_Id is null


--3. List employee name, number and salary for those employees who earn in the range 1200 to 1400.
select E_Name, EmpNo, Sal from Emp where Sal between 1200 and 1400


--4. Give all the employees in the RESEARCH department a 10% pay rise. Verify that this has been done by listing all their details before and after the rise.
select * from Emp where DeptNo = (select DeptNo from Dept where D_Name = 'Research')
update Emp
set Sal = Sal * 1.10
where DeptNo = (select DeptNo from Dept where D_Name = 'Research');
select * from Emp where DeptNo = (select DeptNo from Dept where D_Name = 'Research')


--5. Find the number of CLERKS employed. Give it a descriptive heading.
select count(*) as 'Total Clerks' from Emp where E_Job = 'Clerk';


--6. Find the average salary for each job type and the number of people employed in each job. 
select E_Job, count(*) as "NumEmployees", avg(Sal) as "Avg_Salary"
from Emp
group by E_Job


--7. List the employees with the lowest and highest salary. 
select *
from Emp
where Sal = (select min(Sal) from Emp)
or Sal = (select max(Sal) from Emp)


--8. List full details of departments that don't have any employees. 
select * from Dept
where DeptNo not in (select distinct DeptNo from Emp);


--9. Get the names and salaries of all the analysts earning more than 1200 who are based in department 20. Sort the answer by ascending order of name. 
select E_Name, Sal from Emp
where E_Job = 'Analyst' and Sal > 1200 and DeptNo = 20
order by E_Name;


--10. For each department, list its name and number together with the total salary paid to employees in that department. 
select d.D_Name, d.DeptNo, sum(e.Sal) as "Total_Salary"
from Dept d
join Emp e on d.DeptNo = e.DeptNo
group by d.D_Name, d.DeptNo


--11. Find out salary of both MILLER and SMITH.
select E_Name, Sal from Emp
where E_Name in ('Miller', 'Smith')


--12. Find out the names of the employees whose name begin with ‘A’ or ‘M’. 
select * from Emp
where E_Name like 'A%' or E_Name like 'M%'


--13. Compute yearly salary of SMITH. 
select E_Name, Sal * 12 as "Yearly_Salary"
from Emp
where E_Name = 'Smith';


--14. List the name and salary for all employees whose salary is not in the range of 1500 and 2850. 
select E_Name, Sal from Emp
where Sal not between 1500 and 2850;


--15. Find all managers who have more than 2 employees reporting to them.
select e.Mgr_Id, m.E_Name as "Manager_Name", count(*) as "Reportees"
from Emp e
join Emp m on e.Mgr_Id = m.EmpNo
group by e.Mgr_Id, m.E_Name
having count(*) > 2;
