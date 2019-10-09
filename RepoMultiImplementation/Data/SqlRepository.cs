using RepoMultiImplementation.Interfaces;
using RepoMultiImplementation.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace RepoMultiImplementation.Data
{
    public abstract class SqlRepository<T> : IAsyncRepository<T> where T:BaseEntity
    {
        private string _connectionString;

        public SqlRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection GetOpenConnection()
        {
            var conn = new SqlConnection(_connectionString);
            conn.Open();
            return conn;
        }

        public abstract Task DeleteAsync(T entity);
        public abstract Task<IReadOnlyList<T>> ListAllAsync();
        public abstract Task<T> GetByIdAsync(int id);
        public abstract Task<T> AddAsync(T entity);
        public abstract Task UpdateAsync(T entityToUpdate);
    }
}
