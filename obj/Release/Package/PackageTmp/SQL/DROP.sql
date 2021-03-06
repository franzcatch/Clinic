﻿--select 'drop table ', table_name, 'cascade constraints;' from user_tables;
--select 'drop sequence ', sequence_name, ';' from user_sequences;

drop table 	ENTITY	cascade constraints;
drop table 	ROLES	cascade constraints;
drop table 	USERS	cascade constraints;
drop table 	HOUSEHOLD	cascade constraints;
drop table 	RELATIONSHIP	cascade constraints;
drop table 	HOUSEHOLD_PERSON	cascade constraints;
drop table 	CLINIC	cascade constraints;
drop table 	ROOM	cascade constraints;
drop table 	SERVICE	cascade constraints;
drop table 	SERVICE_CLINIC	cascade constraints;
drop table 	PROVIDER	cascade constraints;
drop table 	PROVIDER_QUALIFICATION	cascade constraints;
drop table 	APPOINTMENT	cascade constraints;
drop table 	APPOINTMENT_SERVICE	cascade constraints;
drop table 	PAYMENT	cascade constraints;

drop sequence 	APPOINTMENT_S	;
drop sequence 	APPOINTMENT_SERVICE_S	;
drop sequence 	CLINIC_S	;
drop sequence 	ENTITY_S	;
drop sequence 	HOUSEHOLD_PERSON_S	;
drop sequence 	HOUSEHOLD_S	;
drop sequence 	PAYMENT_S	;
drop sequence 	PROVIDER_QUALIFICATION_S	;
drop sequence 	PROVIDER_S	;
drop sequence 	ROOM_S	;
drop sequence 	SERVICE_CLINIC_S	;
drop sequence 	SERVICE_S	;
drop sequence 	USERS_S	;