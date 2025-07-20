use Assignments

--1.	Write a T-SQL Program to find the factorial of a given number.

create or alter procedure sp_Factorial
    @Num int
as
begin
    declare @Fact bigint = 1;
    declare @I int = 1;
 
    while @I <= @Num
    begin
        set @Fact = @Fact * @I;
        set @I = @I + 1;
    end
 
    print 'Factorial of ' + cast(@Num as varchar) + ' is ' + cast(@Fact as varchar);
end
go

-- input:
exec sp_Factorial @Num = 5
exec sp_Factorial @Num = 7


--2.	Create a stored procedure to generate multiplication table that accepts a number and generates up to a given number. 

create or alter procedure sp_Multiplicationtable
    @Number int,
    @Upto int
as
begin
    declare @I int = 1;
 
    print 'Multiplication table for ' + cast(@Number as varchar) + ' up to ' + cast(@Upto as varchar);
 
    while @I <= @Upto
    begin
        print cast(@Number as varchar) + ' x ' + cast(@I as varchar) + ' = ' + cast(@Number * @I as varchar);
        set @I = @I + 1;
    end
end
go
 
-- input:
exec sp_Multiplicationtable @Number = 6, @Upto = 10;

--3. Create a function to calculate the status of the student. If student score >=50 then pass, else fail. Display the data neatly

--student table

--Sid       Sname   
--1         Jack
--2         Rithvik
--3         Jaspreeth
--4         Praveen
--5         Bisa
--6         Suraj

--Marks table

--Mid      Sid     Score
--1        1        23
--2        6        95
--3        4        98
--4        2        17
--5        3        53
--6        5        13

create table Student (
S_Id int,
Sname varchar(50))
 
insert into Student values
(1, 'Jack'),
(2, 'Rithvik'),
(3, 'Jaspreeth'),
(4, 'Praveen'),
(5, 'Bisa'),
(6, 'Suraj')
 
create table Marks
(Mid int,
S_Id int,
Score int)
 
insert into Marks values
(1, 1, 23),
(2, 6, 95),
(3, 4, 98),
(4, 2, 17),
(5, 3, 53),
(6, 5, 13)

create or alter function fn_StudentStatus(@Score int)
returns varchar(10)
as
begin
    return case
               when @Score >= 50 then 'Pass'
               else 'Fail'
           end
end
go

select s.S_Id, s.Sname, m.Score, dbo.fn_StudentStatus(m.Score) as Status 
from Student s
join Marks m on s.S_Id = m.S_Id
order by s.S_Id
