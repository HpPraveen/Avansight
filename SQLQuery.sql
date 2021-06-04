CREATE DATABASE PatientDB;

USE PatientDB
GO

-------------------------------Tables----------------------
CREATE TABLE Patient (
    PatientId int identity(1,1) PRIMARY KEY,
    Age int,
    Gender varchar(10)   
);

CREATE TABLE TreatmentReading (
    TreatmentReadingId int identity(1,1) PRIMARY KEY,
    VisitWeek varchar(10),
    Reading decimal(6,2),
    PatientId int FOREIGN KEY REFERENCES Patient(PatientId)
);
------------------------------------------------------------

---------------------User Defined Tables---------------------
CREATE TYPE PatientTableType AS TABLE
( 
    Age int,
    Gender varchar(10)   
);

CREATE TYPE TreatmentReadingTableType AS TABLE
(  	
    VisitWeek varchar(10),
    Reading decimal(6,2),
    PatientId int
);
-------------------------------------------------------------

--------------------------SP----------------------
---------------------------------------
CREATE PROC PatientSet
@Patients AS PatientTableType READONLY
AS
BEGIN	
	INSERT INTO Patient	OUTPUT INSERTED.PatientId,INSERTED.Age,INSERTED.Gender
	SELECT * FROM @Patients		
END
----------------------------------------
CREATE PROC PatientGet
AS
BEGIN 
	SELECT * FROM Patient
END
--------------------------------------------------------
CREATE PROC TreatmentReadingSet
@TreatmentReadings AS TreatmentReadingTableType READONLY
AS
BEGIN 
	INSERT INTO TreatmentReading
	SELECT * FROM @TreatmentReadings
END
-----------------------------------------------------
CREATE PROC TreatmentReadingsGet
AS
BEGIN 
	SELECT * FROM TreatmentReading
END
------------------------------------------------------
--------------------------------------------------------------------