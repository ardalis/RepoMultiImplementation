using RepoMultiImplementation.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace RepoMultiImplementation.Data
{
    public class DapperProductRepository : SqlRepository<Product>
    {
        public DapperProductRepository(string connectionString) : base(connectionString)
        {
        }

        public override async Task<Product> AddAsync(Product entity)
        {
            using (var conn = GetOpenConnection())
            {
                var sql = "INSERT INTO Product(Name) "
                    + "VALUES(@Name)";

                var parameters = new DynamicParameters();
                parameters.Add("@Name", entity.Name, System.Data.DbType.String);

                await conn.QueryAsync(sql, parameters);

                return await Task.FromResult<Product>(null); // not implemented
            }
        }

        public override async Task DeleteAsync(Product entity)
        {
            using (var conn = GetOpenConnection())
            {
                var sql = "DELETE FROM Product WHERE Id = @Id";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", entity.Id, System.Data.DbType.Int32);
                await conn.QueryFirstOrDefaultAsync<Product>(sql, parameters);
            }
        }

        public override async Task<Product> GetByIdAsync(int id)
        {
            using (var conn = GetOpenConnection())
            {
                var sql = "SELECT * FROM Product WHERE Id = @Id";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id, System.Data.DbType.Int32);
                return await conn.QueryFirstOrDefaultAsync<Product>(sql, parameters);
            }
        }

        public override async Task<IReadOnlyList<Product>> ListAllAsync()
        {
            using (var conn = GetOpenConnection())
            {
                var sql = "SELECT * FROM Product";
                return (await conn.QueryAsync<Product>(sql)).AsList();
            }
        }

        public override async Task UpdateAsync(Product entityToUpdate)
        {
            using (var conn = GetOpenConnection())
            {
                var existingEntity = await GetByIdAsync(entityToUpdate.Id);

                var sql = "UPDATE Product "
                    + "SET ";

                var parameters = new DynamicParameters();
                if (existingEntity.Name != entityToUpdate.Name)
                {
                    sql += "Name=@Name,";
                    parameters.Add("@Name", entityToUpdate.Name, DbType.String);
                }

                sql = sql.TrimEnd(',');

                sql += " WHERE Id=@Id";
                parameters.Add("@Id", entityToUpdate.Id, DbType.Int32);

                await conn.QueryAsync(sql, parameters);
            }
        }
    }
}
