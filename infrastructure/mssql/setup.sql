Create Login akkadotnet with password = 'q1w2e3r4t5Y^U&I*O(P)';
Create DATABASE Akka;

GO

Use Akka;  
CREATE USER akkadotnet FOR LOGIN akkadotnet;
GRANT CREATE TABLE TO akkadotnet;
GRANT ALTER, SELECT, INSERT ON SCHEMA::dbo TO akkadotnet;

GO