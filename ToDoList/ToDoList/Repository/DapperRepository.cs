using Dapper;
using Microsoft.Data.SqlClient;

namespace ToDoList.Repository
{
    public class DapperRepository<T> : IRepository<T>
    {
        private readonly string _dbConnection;
        private readonly string _tableName;
        public DapperRepository(IConfiguration Configuration)
        {
            _dbConnection = Configuration.GetSQLServerConnectionString();
            _tableName = $"{typeof(T).Name}";
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using var connection = new SqlConnection(_dbConnection);
            var sql = $"SELECT * FROM {_tableName}";
            return await connection.QueryAsync<T>(sql);
        }
        public async Task<T> GetByIdAsync(int id)
        {
            using var connection = new SqlConnection(_dbConnection);
            var sql = $"SELECT * FROM {_tableName} WHERE Id = @Id";
            return await connection.QuerySingleOrDefaultAsync<T>(sql, new { Id = id });
        }
        public async Task AddAsync(T entity)
        {
            using var connection = new SqlConnection(_dbConnection);

            var entityType = typeof(T);
            var properties = entityType.GetProperties()
                .Where(p => p.Name != "Id");

            var columns = string.Join(", ", properties.Select(p => p.Name));
            var parameters = string.Join(", ", properties.Select(p => "@" + p.Name));

            var sql = $"INSERT INTO {_tableName} ({columns}) VALUES ({parameters})";

            var parameterValues = new DynamicParameters();
            foreach (var property in properties)
                parameterValues.Add("@" + property.Name, property.GetValue(entity));
            
            await connection.ExecuteAsync(sql, parameterValues);
        }
        public async Task UpdateAsync(T entity)
        {
            using var connection = new SqlConnection(_dbConnection);
            var properties = typeof(T).GetProperties()
                                       .Where(p => p.Name != "Id")
                                       .Select(p => $"{p.Name} = @{p.Name}");

            var setClause = string.Join(", ", properties);
            var sql = $"UPDATE {_tableName} SET {setClause} WHERE Id = @Id";

            await connection.ExecuteAsync(sql, entity);
        }
        public async Task DeleteAsync(int id)
        {
            using var connection = new SqlConnection(_dbConnection);
            var sql = $"DELETE FROM {_tableName} WHERE Id = @Id";
            await connection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
