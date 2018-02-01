This is a collection of code samples demonstrating the Data Grid Audit API using the .NET framework and IAuditLogManager. 

Setup:

Modify the following variables in the ConnectionHelper class:

• public static readonly string _userName = "YOURRELATIVITYUSERNAME"; - Replace YOURRELATIVITYUSERNAME with your Relativity username.

• public static readonly string _password = "YOURRELATIVITYPASSWORD"; - Replace YOURRELATIVITYPASSWORD with your Relativity password.

• public static readonly string BaseRelativityURL = "YOURRELATIVITYURL"; - Replace YOURRELATIVITYURL with your Relativity URL. EX: https://MySite.relativity.com/Relativity 

• public static int WorkspaceID = -1; - By default this targets the Admin/Instance level. You can replace this value with the ArtifactID of any workspace with Data Grid Audit installed.

• "Admin, Relativity" is hardcoded into the debug properties for FindAllActionsByUserName. You can modify the value using any valid Relativity username (LastName, FirstName) in the Debug Category in DataGridAuditAPISamples properties. 

Run:
Proceeds through 4 samples in the following order:

1. Find Last Login Date of All Users
2. Find All Errors in Audit
3. Find All Actions by Specific User
4. Find the 100 Most Recent Audits 