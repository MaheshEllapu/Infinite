use assignments

select * from Emp

--1. Retrieve a list of MANAGERS. 
select * from Emp 
where E_Job = 'Manager'

--2. Find out the names and salaries of all employees earning more than 1000 per 
--month. 
select E_Name, Sal 
from Emp 
where Sal > 1000

--3. Display the names and salaries of all employees except JAMES.
select E_Name, Sal 
from Emp 
where E_Name <> 'James'

--4. Find out the details of employees whose names begin with ‘S’.
select * from Emp 
where E_Name like 'S%'

--5. Find out the names of all employees that have ‘A’ anywhere in their name. 
select * from Emp 
where E_Name like '%A%'

--6. Find out the names of all employees that have ‘L’ as their third character in their name.
select * from Emp 
where E_Name like '__L%'

--7. Compute daily salary of JONES. 
select E_Name, Sal/30 as Daily_Salary 
from Emp 
where E_Name = 'Jones'

--8. Calculate the total monthly salary of all employees. 
select sum(Sal) as Total_Monthly_Salary 
from Emp

--9. Print the average annual salary . 
select avg(Sal*12) as Average_Monthly_Salary 
from Emp

--10. Select the name, job, salary, department number of all employees except SALESMAN from department number 30. 
select E_NAME, E_Job,Sal,DeptNo 
from Emp 
where DeptNo = 30 and E_Job <> 'Salesman'

--11. List unique departments of the EMP table. 
select distinct DeptNo from Emp

--12. List the name and salary of employees who earn more than 1500 and are in department 10 or 30. Label the columns Employee and Monthly Salary respectively.
select E_Name as 'Employee', Sal as 'Monthly Salary' 
from Emp 
where Sal > 1500 and DeptNo in(10,30)

--13. Display the name, job, and salary of all the employees whose job is MANAGER or ANALYST and their salary is not equal to 1000, 3000, or 5000. 
select E_Name, E_Job, Sal 
from Emp
where E_Job in ('Manager','Analyst') and Sal not in (1000,3000,5000)

--14. Display the name, salary and commission for all employees whose commission amount is greater than their salary increased by 10%. 
select E_Name, Sal, Comm 
from Emp 
where Comm is not null and Comm > (Sal * 1.10)

--15. Display the name of all employees who have two Ls in their name and are in department 30 or their manager is 7782. 
select E_Name 
from Emp 
where (E_Name like '%L%L%' and DeptNo = 30) or Mgr_Id = 7782

--16. Display the names of employees with experience of over 30 years and under 40 yrs.Count the total number of employees. 
select E_Name 
from Emp 
where DATEDIFF(Year,Hire_Date,GetDate()) between 20 and 40
select count(*) as Total 
from Emp 
where DATEDIFF(Year,Hire_Date,GetDate()) between 30 and 40

--17. Retrieve the names of departments in ascending order and their employees in descending order. 
select d.D_Name,e.E_Name 
from Dept d join Emp e on d.DeptNo = E.DeptNo 
order by d.D_Name asc, e.E_Name desc

--18. Find out experience of MILLER. 
select E_Name, DATEDIFF(Day, Hire_Date, GetDate())/ 365.0 as Experience_Years 
from Emp 
where E_Name = 'Miller'