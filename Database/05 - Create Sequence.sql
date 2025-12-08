use TechnicalInterviewDb;
go

CREATE SEQUENCE SeqReference
    START WITH 10000
    INCREMENT BY 1
    MINVALUE 10000
    NO CYCLE 
    CACHE 10;

GO