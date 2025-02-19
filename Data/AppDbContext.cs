using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Financials.Data;

public class AppDbContext 
{
    private readonly IConfiguration _config;
    public AppDbContext(IConfiguration config)
    {
        _config = config;
    }

    public IEnumerable<T> LoadData<T>(string sql, object? param = null)
    {
        IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        return dbConnection.Query<T>(sql, param);
    }
    public T LoadDataSingle<T>(string sql, object? param = null)
    {
        IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        return dbConnection.QuerySingle<T>(sql, param);
    }

    public bool ExecuteSql(string sql, object? param = null)
    {
        IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        return (dbConnection.Execute(sql, param)) > 0;
    }
    public int ExecuteSqlWithRowCount(string sql, object? param = null)
    {
        IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        return dbConnection.Execute(sql, param);
    }
}