use Assignments

create table Clients(
Client_Id int primary key,
C_Name varchar(40) not null,
C_Address varchar(30),
C_Email varchar(30) unique,
C_Phone varchar(10),
C_Business varchar(20) not null)

create table Departments(
DeptNo int primary key,
D_Name varchar(15) not null,
Loc varchar(20))

create table Employees(
EmpNo int primary key,
E_Name varchar(20) not null,
E_Job varchar(15),
E_Salary varchar(7) check (E_Salary > 0),
DeptNo int,
foreign key (DeptNo) references departments(DeptNo))

create table Projects(
Project_Id int primary key,
Descr varchar(30) not null,
PStart_Date date,
Planned_End_Date date,
Actual_End_Date date,
Budget varchar(10) check (budget > 0),
Client_Id int,
foreign key (client_id) references clients(client_id))

create table EmpProjectTasks(
Project_Id int,
EmpNo int,
EStart_Date date,
End_Date date,
Task varchar(25) not null,
EPT_Status varchar(15) not null,
primary key (project_id, empno),
foreign key (project_id) references projects(project_id),
foreign key (empno) references employees(empno))


insert into clients values
(1001, 'ACME Utilities', 'Noida', 'contact@acmeutil.com', 9567880032, 'Manufacturing'),
(1002, 'Trackon Consultants', 'Mumbai', 'consult@trackon.com', 8734210090, 'Consultant'),
(1003, 'Moneysaver Distributors', 'Kolkata', 'save@moneysaver.com', 7799886655, 'Reseller'),
(1004, 'Lawful Corp', 'Chennai', 'justice@lawful.com', 9210342219, 'Professional')

insert into departments values
(10, 'Design', 'Pune'),
(20, 'Development', 'Pune'),
(30, 'Testing', 'Mumbai'),
(40, 'Document', 'Mumbai')

insert into employees values
(7001, 'Sandeep', 'Analyst', 25000, 10),
(7002, 'Rajesh', 'Designer', 30000, 10),
(7003, 'Madhav', 'Developer', 40000, 20),
(7004, 'Manoj', 'Developer', 40000, 20),
(7005, 'Abhay', 'Designer', 35000, 10),
(7006, 'Uma', 'Tester', 30000, 30),
(7007, 'Gita', 'Tech. Writer', 30000, 40),
(7008, 'Priya', 'Tester', 35000, 30),
(7009, 'Nutan', 'Developer', 45000, 20),
(7010, 'Smita', 'Analyst', 20000, 10),
(7011, 'Anand', 'Project Mgr', 65000, 10)

insert into Projects values
(401, 'Inventory','01-Apr-2011','01-Oct-2011','31-Oct-2011', 150000, 1001),
(402, 'Accounting','01-Aug-2011','01-Jan-2012', null, 500000, 1002),
(403, 'Payroll','01-Oct-2011','31-Dec-2011', null, 75000, 1003),
(404, 'Contact Mgmt','01-Nov-2011','31-Dec-2011', null, 50000, 1004)

insert into empprojecttasks values
(401, 7001,'01-Apr-2011','20-Apr-2011', 'System Analysis', 'Completed'),
(401, 7002,'21-Apr-2011','30-May-2011', 'System Design', 'Completed'),
(401, 7003,'01-Jun-2011','15-Jul-2011', 'Coding', 'Completed'),
(401, 7004,'18-Jul-2011','01-Sep-2011', 'Coding', 'Completed'),
(401, 7006,'03-Sep-2011','15-Sep-2011', 'Testing', 'Completed'),
(401, 7009,'18-Sep-2011','05-Oct-2011', 'Code Change', 'Completed'),
(401, 7008,'06-Oct-2011','16-Oct-2011', 'Testing', 'Completed'),
(401, 7007,'06-Oct-2011','22-Oct-2011', 'Documentation', 'Completed'),
(401, 7011,'22-Oct-2011','31-Oct-2011', 'Sign Off', 'Completed'),
(402, 7010,'01-Aug-2011','20-Aug-2011', 'System Analysis', 'Completed'),
(402, 7002,'22-Aug-2011','30-Sep-2011', 'System Design', 'Completed'),
(402, 7004,'01-Oct-2011', null, 'Coding', 'In Progress')

select * from Clients
select * from Departments
select * from Employees
select * from Projects
select *from EmpProjectTasks

select * from Employees where DeptNo = 20

select * from EmpProjectTasks where Project_Id = 401
