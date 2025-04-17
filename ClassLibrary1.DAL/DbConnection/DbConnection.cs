using System.Data;
using MySql.Data.MySqlClient;

namespace ClassLibrary1.DAL.DbConnection;

public class DbConnection
{
    private readonly string _connectionString;
    
    public DbConnection(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
        => new MySqlConnection(_connectionString);
    
    public async Task<T> QueryWithConnectionAsync<T>(Func<IDbConnection, Task<T>> query)
    {
        using var connection = CreateConnection();
        connection.Open();
        return await query(connection);
    }
    
    public T QueryWithConnection<T>(Func<IDbConnection, T> query)
    {
        using var connection = CreateConnection();
        connection.Open();
        return query(connection);
    }
}