create database Raceday;
use Raceday;

 create Table event (event_id int primary key,
						event_type varchar(250),
						event_name varchar(250) not null,
						Location varchar(250),
						event_description varchar (255),
						weather varchar(250),
						organiser_id int,
						category_id int,
						foreign key (organiser_id) references organiser (organiser_id),
						foreign key (category_id) references category (category_id));

create table organiser (organiser_id int primary key,
						organiser_name varchar(250) not null);


create table category (category_id int primary key,
						category_name varchar(255),
						distance decimal ,
						Age int);

create table participant(participant_id int identity(1,1) primary key,
						fullname varchar(255) not null,
						email varchar(255) not null
						);

create table enrolment ( enrolment_id int primary key,
						participant_id int,
						event_id int,
						foreign key (participant_id) references participant (participant_id),
						foreign key (event_id) references event (event_id));
select * from result;
Create table result (results_id int primary key,
					participant_id int,
					organiser_id int,
					performance_history varchar(255),
					foreign key (participant_id) references participant (participant_id),
					foreign key (organiser_id) references organiser (organiser_id));

create table authentication (authentication_id int primary key,
							username varchar(255) unique not null,
							password varchar(200) not null);

alter table category 
 drop column distance;

 alter table enrolment
add category_id int
foreign key (category_id) references category(category_id);

alter table category
add organiser_id int,
foreign key (organiser_id) references organiser(organiser_id);



alter table category
add event_id int,
foreign key (event_id) references event(event_id);

ALTER TABLE event
ADD event_date DATE,
    distance VARCHAR(100);

alter table authentication 
add  participant_id INT NULL,
    organiser_id INT NULL;
select * from authentication;
ALTER TABLE authentication
ADD CONSTRAINT FK_Auth_Organiser
FOREIGN KEY (organiser_id)
REFERENCES organiser(organiser_id);

ALTER TABLE authentication
ADD CONSTRAINT FK_Auth_Participant
FOREIGN KEY (participant_id)
REFERENCES participant(participant_id);



INSERT INTO Event (Event_id, Event_type, Event_name,Location,  event_description, organiser_id, category_id)
VALUES (1, 'Marathon', 'Johannesburg City Marathon', 'Johannesburg', 'A road running event open to participants of different experience levels.', 1, 1),
(2, 'Cycling', 'Cape Town Cycle Challenge', 'Cape Town', 'A cycling event that allows participants to compete on a scenic road route.', 2, 2),
(3, 'Running', 'Pretoria Spring Run', 'Pretoria', 'A community running event designed for both recreational and competitive runners.', 2, 3);

insert into participant (fullname,email)
values ('keletso Mphago', 'kele@gmail.com'),
		('Remofilwe Moloi' ,'remofilweM@gmail.com');

insert into organiser values (1,'Kefilwe Marang'),
								(2,'Connie Kgang');

insert into enrolment values(1,2,1),
							(2,1,1);
ALTER TABLE category
ALTER COLUMN age VARCHAR(100);

ALTER TABLE category
ALTER COLUMN distance VARCHAR(100);

insert into category 
				values(1,'Fun run','5km','4 years or older'),
				      (2,'run','10km','12 years or older'),
					  (3,'half_marathon','21,1km','16years or older'); 

select * from category;
select * from event;
select* from organiser;
select * from participant;
select * from enrolment;
select * from authentication;
SELECT
    IDENT_SEED('participant') AS IdentitySeed,
    IDENT_INCR('participant') AS IdentityIncrement;

	SELECT participant_id, fullname, email
FROM participant
ORDER BY participant_id;