Create DATABASE RaceDay;
use RaceDay;

CREATE TABLE User_Role
(
User_Role_ID VARCHAR(50) NOT NULL PRIMARY KEY,
User_Role_Name VARCHAR(50) NOT NULL,
User_FirstName VARCHAR(50) NOT NULL,
User_LastName VARCHAR(50) NOT NULL
);

CREATE TABLE User_Profile
(
User_Profile_ID VARCHAR(50) NOT NULL PRIMARY KEY,
 User_Role_ID VARCHAR(50) NOT NULL,
User_Name VARCHAR(50) UNIQUE NOT NULL,
User_Password VARCHAR(50) NOT NULL,
User_Age VARCHAR(50) NOT NULL,
Email VARCHAR(50) UNIQUE NOT NULL,
Phone_Number VARCHAR(50) NOT NULL,
Registration_Date DATE NOT NULL,

FOREIGN KEY (User_Role_ID) REFERENCES User_Role(User_Role_ID)
);


CREATE TABLE Events
(
Events_ID VARCHAR(50) NOT NULL PRIMARY KEY,
Event_Name VARCHAR(50) NOT NULL,
Event_Description VARCHAR(50) NOT NULL,
Event_Date DATE NOT NULL,
Event_Location VARCHAR(50) NOT NULL,
Event_Type VARCHAR(50) NOT NULL,
Event_Distance VARCHAR(50) NOT NULL
);



CREATE TABLE Category
(
Category_ID VARCHAR(50) NOT NULL PRIMARY KEY,
Events_ID VARCHAR(50) NOT NULL,
Age_Category VARCHAR(50) NOT NULL,
Distance_Category VARCHAR(50) NOT NULL,

FOREIGN KEY (Events_ID) REFERENCES Events(Events_ID)
);



CREATE TABLE Event_Enrolment
(
 Event_Enrolment_ID VARCHAR(50) NOT NULL PRIMARY KEY,
Events_ID VARCHAR(50) NOT NULL,
Category_ID VARCHAR(50) NOT NULL,
User_Profile_ID VARCHAR(50) NOT NULL,

FOREIGN KEY (Events_ID) REFERENCES Events(Events_ID),
FOREIGN KEY (Category_ID) REFERENCES Category(Category_ID),
FOREIGN KEY (User_Profile_ID) REFERENCES User_Profile(User_Profile_ID)
);



CREATE TABLE Results
(
Results_ID VARCHAR(50) NOT NULL PRIMARY KEY,
Finish_Times VARCHAR(50) NOT NULL,
Finishing_Positions VARCHAR(50) NOT NULL,
Event_Enrolment_ID VARCHAR(50) NOT NULL,

FOREIGN KEY
(Event_Enrolment_ID) REFERENCES Event_Enrolment(Event_Enrolment_ID)
);


INSERT INTO User_Role
(User_Role_ID, User_Role_Name, User_FirstName, User_LastName)
VALUES
	('O_001','Organiser', 'Dave', 'Roberts'),
	('O_002','Organiser','Mike','Afton'),
	('P_001','Participant','Ronald','Johnson'),
	('P_002','Participant','Adrian','Alan');

	SELECT * FROM User_Role;

INSERT INTO Events
(Events_ID, Event_Name, Event_Description, Event_Date, Event_Location, Event_Type, Event_Distance)
VALUES
	('E_001','Champ Walk', 'Jogging around the Standmore Area','2018-9-20','Stanmore Secondary','Walk', '5km'),
	('E_002','Sunnyday Sprint', 'A race across the beachside','2021-9-10','Blue Lagoon','Run', '10km'),
	('E_003','Charity Cycle', 'Bicycle race for charity','2024-12-5','Durban North','Cycling', '150km');

	SELECT * FROM Events;


		INSERT INTO Category
(Category_ID, Events_ID, Age_Category, Distance_Category)
VALUES
	('C_001', 'E_001' ,'12-16', '5km'),
	('C_002', 'E_002' ,'12-18', '10km'),
	('C_003', 'E_003' ,'15-19', '150km');

	SELECT * FROM Category;


		INSERT INTO User_Profile
(User_Profile_ID, User_Role_ID, User_Name, User_Password, User_Age, Email, Phone_Number, Registration_Date)
VALUES
	('UP_001','O_001', 'useername','password','25','.com', '0123456789','20'),
	('UP_002','O_002', 'useername','password','27','.com', '0123456789','20'),
	('UP_003','P_001', 'useername','password','14','.com', '0123456789','20'),
	('UP_004','P_002', 'useername','password','15','.com', '0123456789','20');

	SELECT * FROM User_Profile;


	INSERT INTO Event_Enrolment
(Event_Enrolment_ID, Events_ID, Category_ID, User_Profile_ID)
VALUES
	('Erol_001','E_001', 'C_001','UP_003'),
	('Erol_002','E_002', 'C_002','UP_004');

	SELECT * FROM Event_Enrolment;


DROP TABLE Results;
DROP TABLE Event_Enrolment;
DROP TABLE Events;
DROP TABLE Category;
DROP TABLE User_Profile;
DROP TABLE User_Role;