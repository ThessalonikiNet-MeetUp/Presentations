USE master;
GO
USE learnchef;
GO
-- Allow the Windows user 'IIS APPPOOL\Products' to login.
CREATE LOGIN [IIS APPPOOL\Products] FROM WINDOWS
GO
-- Create the corresponding user.
CREATE USER [IIS APPPOOL\Products]
GO
-- Grant query access to the user.
GRANT SELECT ON customers TO [IIS APPPOOL\Products]
GO
