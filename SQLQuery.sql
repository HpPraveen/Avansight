CREATE DATABASE PatientDB;

USE PatientDB
GO

-------------------------------Tables----------------------
CREATE TABLE Patient (
    PatientId int PRIMARY KEY,
    Age int,
    Gender varchar(10)   
);

CREATE TABLE TreatmentReading (
    TreatmentReadingId int PRIMARY KEY,
    VisitWeek varchar(10),
    Reading decimal(6,2),
    PatientId int FOREIGN KEY REFERENCES Patient(PatientId)
);
------------------------------------------------------------

---------------------User Defined Tables---------------------
CREATE TYPE PatientTableType AS TABLE
(
	PatientId int PRIMARY KEY,
    Age int,
    Gender varchar(10)   
);

CREATE TYPE TreatmentReadingTableType AS TABLE
(
	TreatmentReadingId int PRIMARY KEY,
    VisitWeek varchar(10),
    Reading decimal(6,2),
    PatientId int
);
-------------------------------------------------------------

-------------------SP----------------------
CREATE PROC PatientSet
@Patients AS PatientTableType READONLY
AS
BEGIN	
	INSERT INTO Patient
	SELECT * FROM @Patients		
END

CREATE PROC PatientGet
@Patients AS PatientTableType READONLY
AS
BEGIN 
	SELECT * FROM Patient
END


CREATE PROC TreatmentReadingSet
@TreatmentReadings AS TreatmentReadingTableType READONLY
AS
BEGIN 
	INSERT INTO TreatmentReading
	SELECT * FROM @TreatmentReadings
END

CREATE PROC TreatmentReadingsGet
@TreatmentReadings AS TreatmentReadingTableType READONLY
AS
BEGIN 
	SELECT * FROM TreatmentReading
END
---------------------------------------------------------