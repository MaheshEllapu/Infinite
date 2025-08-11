-- Create Database
create database Mini_Project_RRS
--Use Database
use Mini_Project_RRS

-- 1. Users table
create table Users
(
UserID int identity(1,1) primary key,
Username varchar(50) not null unique,
Email varchar(100) not null unique,
PhoneNumber varchar(15) not null,
Password varchar(50) not null,
Role varchar(10) check (Role in ('Admin', 'User')) not null,
IsDeleted bit default 0
)

-- Insert hardcoded admin
insert into Users (Username, Email, PhoneNumber, Password, Role)
values ('admin', 'admin@railway.com', '9999999999', 'admin123', 'Admin')

-- 2. Traindetails table
create table TrainDetails
(
TrainNumber int primary key,
TrainName varchar(100) not null,
Source varchar(50) not null,
Destination varchar(50) not null,
Duration varchar(20) not null,
Departure time not null,
Seats_1AC int not null,
Cost_1AC decimal(10,2) not null,
Seats_2AC int not null,
Cost_2AC decimal(10,2) not null,
Seats_3AC int not null,
Cost_3AC decimal(10,2) not null,
Seats_3E int not null,
Cost_3E decimal(10,2) not null,
Seats_Sleeper int not null,
Cost_Sleeper decimal(10,2) not null,
IsDeleted bit default 0
)

-- 3. Reservation table
create table Reservation
(
ReservationID int identity(1,1) primary key,
PNR varchar(20) unique not null,
UserID int not null foreign key references Users(UserID),
TrainNumber int not null foreign key references TrainDetails(TrainNumber),
ClassType varchar(10) check (ClassType in ('1AC', '2AC', '3AC', '3E', 'Sleeper')),
DateOfTravel date not null,
DateOfBooking date not null default getdate(),
TotalCost decimal(10,2) not null,
BerthAllotment varchar(20),
CustomerName varchar(100) not null,
IsCancelled bit default 0,
IsDeleted bit default 0
)

-- 4. Cancellation table
create table Cancellation
(
CancellationID int identity(1,1) primary key,
ReservationID int not null foreign key references Reservation(ReservationID),
DateOfCancellation date not null default getdate(),
RefundAmount decimal(10,2) not null,
IsDeleted bit default 0
)

select * from Users
select * from TrainDetails
select * from Reservation
select * from Cancellation


-- Inserting some train details
INSERT INTO TrainDetails VALUES
(12805, 'Janmabhoomi Express', 'Visakhapatnam', 'Lingampalli', '12h 30m', '06:20:00', 10, 2200.00, 20, 1500.00, 30, 1000.00, 40, 900.00, 100, 500.00, 0),
(12806, 'Janmabhoomi Express', 'Lingampalli', 'Visakhapatnam', '12h 30m', '19:45:00', 10, 2200.00, 20, 1500.00, 30, 1000.00, 40, 900.00, 100, 500.00, 0),

(12728, 'Godavari Express', 'Hyderabad', 'Visakhapatnam', '13h 00m', '17:20:00', 10, 2500.00, 20, 1800.00, 30, 1200.00, 40, 1000.00, 100, 550.00, 0),
(12727, 'Godavari Express', 'Visakhapatnam', 'Hyderabad', '13h 00m', '05:55:00', 10, 2500.00, 20, 1800.00, 30, 1200.00, 40, 1000.00, 100, 550.00, 0),

(20833, 'Vande Bharat Express', 'Visakhapatnam', 'Secunderabad', '8h 00m', '05:45:00', 0, 0.00, 0, 0.00, 0, 0.00, 0, 0.00, 100, 1600.00, 0),
(20708, 'Vande Bharat Express', 'Secunderabad', 'Visakhapatnam', '8h 00m', '14:30:00', 0, 0.00, 0, 0.00, 0, 0.00, 0, 0.00, 100, 1600.00, 0),

(18464, 'Prashanthi Express', 'KSR Bengaluru', 'Bhubaneswar', '29h 10m', '13:40:00', 10, 2800.00, 20, 1900.00, 30, 1300.00, 40, 1100.00, 100, 600.00, 0),
(18463, 'Prashanthi Express', 'Bhubaneswar', 'KSR Bengaluru', '30h 05m', '05:40:00', 10, 2800.00, 20, 1900.00, 30, 1300.00, 40, 1100.00, 100, 600.00, 0),

(17488, 'Tirumala Express', 'Visakhapatnam', 'Tirupati', '15h 20m', '14:00:00', 10, 2400.00, 20, 1600.00, 30, 1100.00, 40, 950.00, 100, 500.00, 0),
(17487, 'Tirumala Express', 'Tirupati', 'Visakhapatnam', '15h 00m', '20:30:00', 10, 2400.00, 20, 1600.00, 30, 1100.00, 40, 950.00, 100, 500.00, 0);

select * from Users
select * from TrainDetails
select * from Reservation
select * from Cancellation