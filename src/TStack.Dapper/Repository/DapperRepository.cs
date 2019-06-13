using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TStack.Dapper.Connection;
using TStack.Dapper.Entity;

namespace TStack.Dapper.Repository
{
    public abstract class DapperRepository<TDocument, TPrimary, TContext> : DapperBaseRepository, IDapperRepository<TDocument, TPrimary, TContext> where
          TDocument : class, IDapperEntity<TPrimary>, new()
        where TPrimary : struct
        where TContext : DapperConnection, new()
    {
        private  readonly DapperConnection _dapperConnection;
        public DapperRepository()
        {
            _dapperConnection = new TContext();
            if (string.IsNullOrEmpty(_dapperConnection.ConnectionString))
                throw new ArgumentNullException();
            _dbContext = new SqlConnection(_dapperConnection.ConnectionString);
            _commandTimeOut = _dapperConnection.CommantTimeOut;
        }
        public int AddOne(TDocument model)
        {
            return DapperProcess(() => (int)_dbContext.Insert(model, _dbTransaction, _commandTimeOut));
        }

        public async Task<int> AddOneAsync(TDocument model)
        {
            return await DapperProcessAsync<int>(async () => (int)(await _dbContext.InsertAsync(model, _dbTransaction, _commandTimeOut)));
        }
        public bool Delete(TDocument model)
        {
            return DapperProcess(() => _dbContext.Delete(model, _dbTransaction, _commandTimeOut));
        }

        public async Task<bool> DeleteAsync(TDocument model)
        {
            return await DapperProcessAsync<bool>(async () => (bool)(await _dbContext.DeleteAsync(model, _dbTransaction, _commandTimeOut)));
        }

        public bool DeleteById(TPrimary id)
        {
            return DapperProcess(() => _dbContext.Delete(new TDocument { Id = id }, _dbTransaction, _commandTimeOut));
        }

        public async Task<bool> DeleteByIdAsync(TPrimary id)
        {
            return await DapperProcessAsync<bool>(async () => (bool)(await _dbContext.DeleteAsync(new TDocument { Id = id }, _dbTransaction, _commandTimeOut)));
        }

        public IEnumerable<TDocument> GetAll()
        {
            return DapperProcess(() => _dbContext.GetAll<TDocument>(_dbTransaction, _commandTimeOut));
        }

        public async Task<IEnumerable<TDocument>> GetAllAsync()
        {
            return await DapperProcessAsync<IEnumerable<TDocument>>(async () => (IEnumerable<TDocument>)(await _dbContext.GetAllAsync<TDocument>(_dbTransaction, _commandTimeOut)));

        }

        public TDocument GetById(TPrimary id)
        {
            return DapperProcess(() => _dbContext.Get<TDocument>(id, _dbTransaction, _commandTimeOut));
        }

        public async Task<TDocument> GetByIdAsync(TPrimary id)
        {
            return await DapperProcessAsync<TDocument>(async () => (TDocument)(await _dbContext.GetAsync<TDocument>(id, _dbTransaction, _commandTimeOut)));
        }

        public bool UpdateOne(TDocument model)
        {
            return DapperProcess(() => _dbContext.Update(model, _dbTransaction, _commandTimeOut));
        }

        public async Task<bool> UpdateOneAsync(TDocument model)
        {
            return await DapperProcessAsync<bool>(async () => (bool)(await _dbContext.UpdateAsync(model, _dbTransaction, _commandTimeOut)));
        }
    }
}
