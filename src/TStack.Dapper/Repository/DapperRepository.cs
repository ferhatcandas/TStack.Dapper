using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TStack.Dapper.Connection;
using TStack.Dapper.Entity;

namespace TStack.Dapper.Repository
{

    public abstract class DapperRepository<TDocument, TPrimary> : DapperBaseRepository, IDapperRepository<TDocument, TPrimary> where
        TDocument : class, IDapperEntity<TPrimary>, new()
      where TPrimary : struct
    {

        private readonly DapperConnection _dapperConnection;
        public DapperRepository(DapperConnection connection)
        {
            _dapperConnection = connection;
            if (string.IsNullOrEmpty(_dapperConnection.ConnectionString))
                throw new ArgumentNullException(nameof(_dapperConnection));
            _dbContext = new SqlConnection(_dapperConnection.ConnectionString);
            _commandTimeOut = _dapperConnection.CommantTimeOut;
        }
        public DapperRepository()
        {
            if (_dapperConnection == null)
                throw new ArgumentNullException(nameof(_dapperConnection));
        }
        public int AddOne(TDocument model) => DapperProcess(() => (int)_dbContext.Insert(model, _dbTransaction, _commandTimeOut));

        public async Task<int> AddOneAsync(TDocument model) => await DapperProcessAsync(async () => await _dbContext.InsertAsync(model, _dbTransaction, _commandTimeOut));
        public bool Delete(TDocument model) => DapperProcess(() => _dbContext.Delete(model, _dbTransaction, _commandTimeOut));

        public async Task<bool> DeleteAsync(TDocument model) => await DapperProcessAsync(async () => await _dbContext.DeleteAsync(model, _dbTransaction, _commandTimeOut));

        public bool DeleteById(TPrimary id) => DapperProcess(() => _dbContext.Delete(new TDocument { Id = id }, _dbTransaction, _commandTimeOut));

        public async Task<bool> DeleteByIdAsync(TPrimary id) => await DapperProcessAsync(async () => await _dbContext.DeleteAsync(new TDocument { Id = id }, _dbTransaction, _commandTimeOut));

        public IEnumerable<TDocument> GetAll() => DapperProcess(() => _dbContext.GetAll<TDocument>(_dbTransaction, _commandTimeOut));

        public async Task<IEnumerable<TDocument>> GetAllAsync() => await DapperProcessAsync(async () => await _dbContext.GetAllAsync<TDocument>(_dbTransaction, _commandTimeOut));

        public TDocument GetById(TPrimary id) => DapperProcess(() => _dbContext.Get<TDocument>(id, _dbTransaction, _commandTimeOut));

        public async Task<TDocument> GetByIdAsync(TPrimary id) => await DapperProcessAsync(async () => await _dbContext.GetAsync<TDocument>(id, _dbTransaction, _commandTimeOut));

        public bool UpdateOne(TDocument model) => DapperProcess(() => _dbContext.Update(model, _dbTransaction, _commandTimeOut));

        public async Task<bool> UpdateOneAsync(TDocument model) => await DapperProcessAsync(async () => await _dbContext.UpdateAsync(model, _dbTransaction, _commandTimeOut));
    }
    public abstract class DapperRepository<TDocument, TPrimary, TContext> : DapperRepository<TDocument, TPrimary> where
      TDocument : class, IDapperEntity<TPrimary>, new()
    where TPrimary : struct
    where TContext : DapperConnection, new()
    {
        private readonly DapperConnection _dapperConnection;
        public DapperRepository()
        {
            _dapperConnection = new TContext();
            if (string.IsNullOrEmpty(_dapperConnection.ConnectionString))
                throw new ArgumentNullException();
            _dbContext = new SqlConnection(_dapperConnection.ConnectionString);
            _commandTimeOut = _dapperConnection.CommantTimeOut;
        }

    }
}
