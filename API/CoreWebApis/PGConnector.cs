using CoreWebApis.Models;
using Npgsql;
using System.Reflection;

namespace CoreWebApis
{
    public class PGConnector
    {
        private readonly string ConnectionString;
        public PGConnector(string Connectionstring)
        {
            ConnectionString = Connectionstring;
        }

        public async Task<List<IEmployees>> GetEmployees()
        {
            try
            {
                List<IEmployees> employees = new List<IEmployees>(); 
                using(var npgSqlConnecction = new NpgsqlConnection(ConnectionString))
                {
                    await npgSqlConnecction.OpenAsync();
                    var sql = @"Select * from ""Employees""";
                    using (var cmd = new NpgsqlCommand(sql, npgSqlConnecction))
                    {
                        using(var reader =  cmd.ExecuteReader())
                        {
                            while (await reader.ReadAsync())
                            {
                                var emp = new Employees();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    string fieldName = reader.GetName(i);
                                    PropertyInfo property = typeof(Employees).GetProperty(fieldName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                                    if (property != null && !reader.IsDBNull(i))
                                    {
                                        var value = reader.GetValue(i);
                                        property.SetValue(emp, Convert.ChangeType(value, property.PropertyType));
                                    }
                                }
                                employees.Add(emp);
                            }
                        }
                    }
                    await npgSqlConnecction.CloseAsync();
                }
                return employees;
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
