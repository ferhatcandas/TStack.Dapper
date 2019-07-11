# Overview
Dapper is a micro orm tool for .Net ado providers. The purpose of this library is to provide a tool that can be used in an **easy and generic** structure.

# Covarage
Include providers
 - SQLite
 - SQL CE
 - Firebird
 - Oracle
 - MySQL
 - PostgreSQL
 - SQL Server

# Installation
[Nuget Package](https://www.nuget.org/packages/TStack.Dapper/)
#### Package Manager
```PM
Install-Package TStack.Dapper -Version 1.0.0
```
#### .NET CLI
```PM
dotnet add package TStack.Dapper --version 1.0.0
```
#### PackageReference
```PM
<PackageReference Include="TStack.Dapper" Version="1.0.0" />
```
#### Paket CLI
```PM
paket add TStack.Dapper --version 1.0.0
```

# Usage
To use database classes must be inherited from the "IDapperEntity" class before
```csharp
    [TableName("Employee")] //TableName mapping
    public class Employee : IDapperEntity<int>
    {
        [Primary]//primary key
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [Excluded]//excluded field not updating on insert or update 
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public double Salary { get; set; }
    }
```

For connection class
```csharp
public class TestConnection : DapperConnection
{
	public TestConnection() : base(@"Server=.\SQLEXPRESS;Database=TESTDB;Trusted_Connection=True;", 30)
	{
	}
}
```

For repository
```csharp
public class EmployeeRepository : DapperRepository<Employee, int, TestConnection>
{
}
```


That's it, now repository access to usable methods.


# Author

Ferhat Candaş - Software Developer
 - Mail : candasferhat61@gmail.com
 - LinkedIn : https://www.linkedin.com/in/ferhatcandas