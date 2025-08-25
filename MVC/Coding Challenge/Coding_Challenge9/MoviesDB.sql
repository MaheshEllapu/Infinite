create database MoviesDB
use MoviesDB

create table Movies(
Mid varchar(10),
Moviename varchar(10),
DirectorName varchar(10),
DateofRelease varchar(10))

insert into Movies (mid, moviename, directorname, dateofrelease) values 
('M001', 'Inception', 'Nolan', '2010-07-16'), 
('M002', 'Avatar', 'Cameron', '2009-12-18'), 
('M003', 'Titanic', 'Cameron', '1997-12-19'), 
('M004', 'Dangal', 'Tiwari', '2016-12-23'), 
('M005', 'Rrr', 'Rajamouli', '2022-03-25')

select * from Movies
