using Npgsql;
using CoreWebApis.Models;
using System.Reflection;
using CoreWebApis.Modules.Login.Model.Classes;
public class PostgresConnector
{
    private readonly string _connectionString;

    public PostgresConnector(string connectionString)
    {
        _connectionString = connectionString;
    }
    public async Task<List<User>> GetUsers()
    {
        List<User> users = new List<User>();

        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        string sql = "SELECT username FROM users";

        using var command = new NpgsqlCommand(sql, connection);
        using var reader = command.ExecuteReader();

        while (await reader.ReadAsync())
        {
            var user = new User();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                string fieldName = reader.GetName(i);
                PropertyInfo property = typeof(User).GetProperty(fieldName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (property != null && !reader.IsDBNull(i))
                {
                    var value = reader.GetValue(i);
                    property.SetValue(user, Convert.ChangeType(value, property.PropertyType));
                }
            }
            users.Add(user);
        }
        return users;
    }
}

