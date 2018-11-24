create extension "uuid-ossp";
drop table if exists Task;
drop table if exists Account;
create table Account (
  email varchar(30) Unique Primary Key,
  timeZone integer,
  encryptedPasswordHash varchar(30),
  firstName varchar(100),
  lastName varchar(100),
  middleName varchar(100)
);

create table Task(
  taskId UUID Unique Primary key,
  taskName varchar(20),
  timeSpent real,
	taskDate timestamp,
  email varchar(30) REFERENCES Account(email)
);

insert into Account values('guest@abc.com', 4, 'abc', 'John', 'M.', 'Doe');
insert into Task values(uuid_generate_v4(), 'running', 1, TIMESTAMP '2018-01-01 6:00:00', 'guest@abc.com');
insert into Task values(uuid_generate_v4(), 'reading', 1, TIMESTAMP '2018-01-11 7:00:00', 'guest@abc.com');
insert into Task values(uuid_generate_v4(), 'writing', 1, TIMESTAMP '2018-01-01 8:00:00', 'guest@abc.com');
insert into Task values(uuid_generate_v4(), 'eating', 0.5, TIMESTAMP '2018-01-01 9:00:00', 'guest@abc.com');
insert into Task values(uuid_generate_v4(), 'biking', 1, TIMESTAMP '2018-01-01 11:00:00', 'guest@abc.com');