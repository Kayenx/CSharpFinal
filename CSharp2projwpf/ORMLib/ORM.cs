using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ORMLib
{
    public class ORM
    {
        public string connectionString = "Data Source=../../database.db;";

        public  T Select<T>(int id) where T : new()
        {
            var type = typeof(T);
            T instance = (T)Activator.CreateInstance(type);
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand($"SELECT * FROM {type.Name} WHERE ID=@ID", connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                var property = type.GetProperty(reader.GetName(i));
                                if (property != null)
                                {
                                    var value = reader.GetValue(i);
                                    if (value != DBNull.Value)
                                    {
                                        property.SetValue(instance, Convert.ChangeType(value, property.PropertyType));
                                    }
                                }
                            }
                        }
                        return instance;
                    }
                }
            }
        }
        public List<T> Select_All<T>() where T : new()
        {
            var type = typeof(T);
            var result = new List<T>();
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand($"SELECT * FROM {type.Name}", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            T instance = (T)Activator.CreateInstance(type);
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                var property = type.GetProperty(reader.GetName(i));
                                if (property != null)
                                {
                                    var value = reader.GetValue(i);
                                    if (value != DBNull.Value)
                                    {
                                        property.SetValue(instance, Convert.ChangeType(value, property.PropertyType));
                                    }
                                }
                            }
                            result.Add(instance);
                        }
                        return result;
                    }
                }
            }
        }
        public async void Insert<T>(T obj) where T : new()
        {
            var type = typeof(T);
            using (var connection = new SqliteConnection(connectionString))
            {
                await connection.OpenAsync();

                string[] columnNames = type.GetProperties()
                                            .Where(p => p.Name != "ID")
                                            .Select(p => p.Name)
                                            .ToArray();
                string[] parameterNames = columnNames.Select(name => $"@{name}").ToArray();

                var command = new SqliteCommand($"INSERT INTO {typeof(T).Name} ({string.Join(", ", columnNames)}) VALUES ({string.Join(", ", parameterNames)}); ", connection);
                var properties = type.GetProperties();
                foreach (var property in properties)
                {
                    if (property.Name != "ID")
                    {
                        var value = property.GetValue(obj);
                        var parameter = new SqliteParameter($"@{property.Name}", value);
                        command.Parameters.Add(parameter);
                    }
                }

                await command.ExecuteNonQueryAsync();
               
            }
        }
        public async void Update<T>(T obj) where T : new()
        {
            var type = typeof(T);
            using (var connection = new SqliteConnection(connectionString))
            {
                
                await connection.OpenAsync();
                var properties = type.GetProperties();
                var columnNames = string.Join(", ", properties.Where(p => p.Name != "ID").Select(p => p.Name + " = @" + p.Name));
                var command = new SqliteCommand($"UPDATE {type.Name} SET {columnNames} WHERE ID = @ID", connection);

                foreach (var property in properties)
                {
                    var value = property.GetValue(obj);
                    var parameter = new SqliteParameter("@" + property.Name, value);
                    command.Parameters.Add(parameter);
                }

                await command.ExecuteNonQueryAsync();
                
            }
        }
        public async void Delete<T>(T obj) where T : new()
        {
            var type = typeof(T);
            using (var connection = new SqliteConnection(connectionString))
            {
                await connection.OpenAsync();

                var id = type.GetProperty("ID").GetValue(obj);
                var command = new SqliteCommand($"DELETE FROM {type.Name} WHERE ID = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                await command.ExecuteNonQueryAsync();
               
            }
        }
    }
}
