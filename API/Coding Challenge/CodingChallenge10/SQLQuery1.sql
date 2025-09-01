use northwind

CREATE PROCEDURE GetCustomersByCountry
    @Country NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
 
    SELECT CustomerID,
           CompanyName,
           ContactName,
           City,
           Country
    FROM Customers
    WHERE Country = @Country;
END
 
