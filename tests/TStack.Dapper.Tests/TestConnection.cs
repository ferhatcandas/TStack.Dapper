using System;
using System.Collections.Generic;
using System.Text;
using TStack.Dapper.Connection;

namespace TStack.Dapper.Tests
{
    public class TestConnection : DapperConnection
    {
        public TestConnection() : base(@"Server=.\SQLEXPRESS;Database=TESTDB;Trusted_Connection=True;", 30)
        {
        }
    }
}
