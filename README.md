# AccessPointClient

## Prerequisites and dependencies:
-	.NET Framework 4.5.1 https://support.microsoft.com/tr-tr/kb/2858728 
-	Entity Framework 6 Tools for Visual Studio 2012 & 2013 http://www.microsoft.com/en-us/download/details.aspx?id=40762 
-	Visual Studio 2012 or later.

## When project will be opened the packages would be downloaded by Visual Studio
- bootstrap.3.3.5
- 	EntityFramework.6.1.3
-	jQuery.2.1.4
-	jquery.mobile.1.4.5
-	jQuery.Validation.1.14.0
-	Microsoft.AspNet.Mvc.5.2.3
-	Microsoft.AspNet.Razor.3.2.3
-	Microsoft.AspNet.WebApi.5.2.3
-	Newtonsoft.Json.7.0.1

## Database
-	Database generation script is at “\Database\accessControlManagement.sql” for creating the script
-	Database backup file is at “\Database \accessControlManagement.bak” for demo data

## Build & Run
Open Solution file “\Source\AccessPointClient.sln” with Visual Studio 2012
Change Following config files with new generated database connection string
-	\Source\AccessPointAPI\Web.Config
-	\Source\ManagementPanel\Web.Config
-	\Source\ ReportingTool \App.Config


# Access Control System Software Architecture

## Technology

Since the client has multiple facilities and no regulation against usage of the cloud a Microsoft Azure Based solution would be suitable, affordable, scalable and maintainable. 

# Server:

Microsoft Azure Cloud Based Technology gives the opportunity of using PaaS (Platform as a Service) Architecture. 
Platform as a service (PaaS) is a cloud computing model that delivers applications over the Internet. In a PaaS model, a cloud provider delivers hardware and software tools -- usually those needed for application development -- to its users as a service. A PaaS provider hosts the hardware and software on its own infrastructure. As a result, PaaS frees users from having to install in-house hardware and software to develop or run a new application.
All the database and web applications would be stored in Azure Cloud Services. Accessibility of the web applications and load balancing will be managed by Over Azure Virtual Network.

# Database:

Microsoft SQL should be used over Microsoft Azure Premium Layer Sql Server Machines. Those are PaaS based virtual database server and specialized for database servicing machines.
-	Code will be connected to database through Entity Frame work.
-	For Performance there would be some Stored Procedures implemented in the database.

# Code:

Web applications will be developed with MVC5 – HTML5 combined with J-Query Mobile. All the codes will be written in Visual Studio and Stored in the Azure Team Project (Cloud based Team Foundation Server). Also the Azure Visual Studio Online could be used for mobile working and for decrease the licensing costs. 
Visual Studio Online provides you and your team a set of cloud-powered collaboration tools that work with your existing IDE or editor, so your team can work effectively on software projects of all shapes and sizes. Visual Studio Online includes everything from hosted code repos, project tracking tools and agile portfolio management tools, to continuous integration, test case management and cloud based load testing. Visual Studio Online scales to meet any team size and provides the capabilities to be the core of your ALM and DevOps practices.
-	So the web applications would be hosted in the azure web site service.  
-	Usage of .NET C# and JavaScript is common by developer communities and there are great numbers of developers.
-	Also MVC Framework of .NET some services could be developed as WEB API and would be communicated with Basic HTTP Post and Get Requests. So mobile and light-weight devices also could communicate with the servers. 
-	With J-Query Mobile all the web applications would be developed with responsive UI and could be available from tablets and phones.

# Software

## On Site Access Point Software:
First off all there are independent sites. There should be a software that connects the access points and access controls to the Server.
-	Server could be also welcome messages over TCP-IP and Http Request from access points. 
-	All the Business Intelligence should be developed server side as a Web API solution. 
-	This WEB API solution gain access to the Server Side Access Control Management Database.
-	Also this application is responsible for alerting the manager via Email and SMS.
Access-Point Client Software only checks the proximity sensor data of the biometric data and sends usage data to the server. Server communication would be encrypted. 
Client software could work offline when internet is not available. At that mode client software will be checked biometric sensors according to the local cache and stores the log data locally until the internet would be online. When internet becomes available the data would be sent to the server and deleted from the local cache.
Server Side Access Control Management Web Application:
Since the Authentication is stored in the LDAP directory of the company. Azure Active Directory and Multi-Factor Authentication should be integrated with the Company LDAP Directory from Microsoft Azure Management Panel. 

## Application Flow
-	User will be logged on to the system
-	If it is a manager following screen access will be provided
-	Add / Remove User, User Group and Roles
-	Edit Roles and User Groups
-	Roles to Access Point Binding 
-	View Access Point Action Log. (Successful and failed accesses)
-	If it is not a manager following screen access will be provided
-	Change User Details
-	View Access Point Logs only for him/herself.
Server Side Reporting Application:
This would be a Windows Service or a Console Application that also runs over Microsoft Azure Cloud Virtual Machine and will be managed by System Administrator. This application also has to gain access to the Server Side Access Control Management Web Application’s Database. 
-	It is responsible to create daily department wise attendance report as a CSV file for HR department. File would be available over Azure Storage Service and Accessed according to the Active Directory Permissions.
-	It is responsible for department-wise activity log data and it would be mailed to the department managers.
