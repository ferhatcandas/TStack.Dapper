using System;
using System.Collections.Generic;
using System.Text;
using TStack.Dapper.Entity;
using TStack.Dapper.Helper;

namespace TStack.Dapper.Tests
{
    [TableName("Employee")]
    public class Employee : IDapperEntity<int>
    {
        [Primary]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [Excluded]
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public double Salary { get; set; }
    }
}
