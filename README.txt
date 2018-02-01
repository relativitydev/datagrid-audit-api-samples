DataGridConsole
--------------- 

Summary
-------
This C# console application demonstrates how to use the DataGrid Audit API to query and filter on records.
It does two things:
First, it queries for all 


How to use
----------
When running the solution, supply one command line argument that specifies the path to your credentials text file, 
which should be formatted like so:
(instance URL)
(email address of user)
(password)

e.g.
https://my-relativity-instance.com
my_username@my-relativity-instance.com
MySecretPassword!1234


Dependencies
------------
 - Newtonsoft.JSON (for constructing JSON objects)