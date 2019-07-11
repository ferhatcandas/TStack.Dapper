using System.Collections.Generic;
using System.Threading.Tasks;
using TStack.Dapper.Connection;
using TStack.Dapper.Entity;

namespace TStack.Dapper.Repository
{
    public interface IDapperRepository<TDocument, TPrimary> where
        TDocument : class, IDapperEntity<TPrimary>, new()
        where TPrimary : struct
    {
        int AddOne(TDocument model);
        Task<int> AddOneAsync(TDocument model);
        //bool Exist(Expression<Func<TDocument, bool>> predicate);
        //Task<bool> ExistsAsync(Expression<Func<TDocument, bool>> predicate);
        bool UpdateOne(TDocument model);
        Task<bool> UpdateOneAsync(TDocument model);
        bool Delete(TDocument model);
        Task<bool> DeleteAsync(TDocument model);
        bool DeleteById(TPrimary id);
        Task<bool> DeleteByIdAsync(TPrimary id);
        TDocument GetById(TPrimary id);
        Task<TDocument> GetByIdAsync(TPrimary id);
        IEnumerable<TDocument> GetAll();
        Task<IEnumerable<TDocument>> GetAllAsync();
    }
}
