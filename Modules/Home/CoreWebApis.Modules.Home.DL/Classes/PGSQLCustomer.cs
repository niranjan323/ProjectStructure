using CoreWebApis.Modules.Home.DL.Interfaces;
using CoreWebApis.Modules.Home.Model.Classes;
using CoreWebApis.Modules.Home.Model.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CoreWebApis.Modules.Home.DL.Classes
{
    public class PGSQLCustomer : IPGSQLCustomer
    {
        private readonly string _connectionString;
        public PGSQLCustomer(string Connectionstring)
        {
            _connectionString = Connectionstring;
        }
        public async Task<List<ICustomer>> GetEmployees()
        {
            try
            {
                List<ICustomer> employees = new List<ICustomer>();
                using (var npgSqlConnecction = new NpgsqlConnection(_connectionString))
                {
                    await npgSqlConnecction.OpenAsync();
                    var sql = @"Select * from ""Employees""";
                    using (var cmd = new NpgsqlCommand(sql, npgSqlConnecction))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (await reader.ReadAsync())
                            {
                                var emp = new Customer();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    string fieldName = reader.GetName(i);
                                    PropertyInfo property = typeof(Customer).GetProperty(fieldName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
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
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
