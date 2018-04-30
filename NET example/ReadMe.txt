How to use
----------
Before running the executable, fill out the appSettings section in your app.config file:

  <appSettings>
    <add key="RelativityBaseUrl" value="https://relativity-instance" />
    <add key="RelativityUserName" value="" />
    <add key="RelativityPassword" value="" />
    <add key="WorkspaceId" value=""/>
  </appSettings>

Note: it is crucial that your DLL versions match your Relativity instance version.

Dependencies
------------
•	kCura.AuditUI2.Services.Interface
•	Relativity.Services.Interfaces
•	Relativity.Services.Interfaces.Private
