This is a collection of code samples demonstrating the Data Grid Audit API using the .NET framework and IAuditLogManager. 

Setup:

The IAuditLogManager  .NET interface is used to interact with Data Grid audit records from Relativity application components such as agents, custom pages, and event handlers. The interface can be found in kCura.AuditUI2.Services.Interface.dll. Once the Data Grid for Audit application is installed in your environment you will be able to obtain this assembly by downloading the DLL from the Resource Files tab in Relativity.

The ConnectionHelper class contains all constants for the examples. If you'd like to modify any of the properties of the Audit Data queries it can be done here. 

In order to connect to your environment you must modify the following variables in the ConnectionHelper class:

• public static readonly string _userName = "YOURRELATIVITYUSERNAME"; - Replace YOURRELATIVITYUSERNAME with your Relativity username.

• public static readonly string _password = "YOURRELATIVITYPASSWORD"; - Replace YOURRELATIVITYPASSWORD with your Relativity password.

• public static readonly string BaseRelativityURL = "YOURRELATIVITYURL"; - Replace YOURRELATIVITYURL with your Relativity URL. EX: https://MySite.relativity.com/Relativity 

• public static int WorkspaceID = -1; - By default this targets the Admin/Instance level. You can replace this value with the ArtifactID of any workspace with Data Grid Audit installed.

• "Admin, Relativity" is hardcoded into the debug properties for FindAllActionsByUserName. You can modify the value using any valid Relativity username (LastName, FirstName) in the Debug Category in DataGridAuditAPISamples properties. found by right clicking DataGridAuditAPISamples in the Visual Studio solution explorer and selecting "Properties" and selecting the "Debug" category. 

Run:
Proceeds through 4 samples in the following order:

1. Find Last Login Date of All Users
2. Find Inactive Users (30 days)
3. Find All Errors in Audit
4. Find All Actions by Specific User
5. Find the 100 Most Recent Audit Items 