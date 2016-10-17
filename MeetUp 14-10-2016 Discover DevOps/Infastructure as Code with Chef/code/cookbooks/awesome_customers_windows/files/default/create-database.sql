USE master;
GO
-- Create the learnchef database.
CREATE DATABASE learnchef;
GO
USE learnchef;
GO
-- Create the customers table.
CREATE TABLE customers(
  id uniqueidentifier NOT NULL DEFAULT newid(),
  PRIMARY KEY(id),
  first_name VARCHAR(64),
  last_name VARCHAR(64),
  email VARCHAR(64)
);
GO
-- Add sample data to the table.
INSERT INTO customers(id, first_name, last_name, email) VALUES(newid(), 'Jane', 'Smith', 'jane.smith@example.com');
INSERT INTO customers(id, first_name, last_name, email) VALUES(newid(), 'Dave', 'Richards', 'dave.richards@example.com');
GO
