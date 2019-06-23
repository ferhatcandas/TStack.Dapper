using System;
using System.Data;
using System.Threading.Tasks;

namespace TStack.Dapper.Repository
{
    public abstract class DapperBaseRepository
    {
        public IDbConnection _dbContext { get; internal set; }
        public IDbTransaction _dbTransaction { get; internal set; }
        public int _commandTimeOut { get; internal set; }
        public IDbTransaction DbTransaction => _dbTransaction;
        public IDbConnection DbConnection => _dbContext;

        public void CommitTranscation()
        {
            _dbTransaction.Commit();
        }
        internal void Open()
        {
            if (_dbContext.State != ConnectionState.Open)
                _dbContext.Open();
        }
        internal void Close()
        {
            if (_dbContext.State != ConnectionState.Closed)
                _dbContext.Close();
        }
        internal void OpenTranscation()
        {
            _dbTransaction = _dbContext.BeginTransaction();
        }
        internal T DapperProcess<T>(Func<T> func)
        {
            try
            {
                Open();
                OpenTranscation();
                var response = func();
                CommitTranscation();
                Close();
                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        internal async Task<T> DapperProcessAsync<T>(Func<Task<T>> func)
        {
            return await Task.Run<T>(() =>
            {
                return DapperProcess(func);
            });

        }

    }
}
