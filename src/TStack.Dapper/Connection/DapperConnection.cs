using System;
using System.Data;

namespace TStack.Dapper.Connection
{
    public abstract class DapperConnection
    {
        public DapperConnection(string connStr, int commandTimeout = 30)
        {
            if (string.IsNullOrEmpty(connStr))
                throw new ArgumentNullException("Connection string must not be null or empty.");
            if (commandTimeout < 1)
                throw new ArgumentException("Command time out must be greather than zero");
            ConnectionString = connStr;
            CommantTimeOut = commandTimeout;
        }
        internal string ConnectionString { get; private set; }
        internal int CommantTimeOut { get; private set; } = 30;
    }
}
