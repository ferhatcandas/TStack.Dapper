using System;
using System.Collections.Generic;
using System.Text;
using TStack.Dapper.Repository;

namespace TStack.Dapper.Tests
{
    public class EmployeeRepository : DapperRepository<Employee, int, TestConnection>
    {
    }
}
