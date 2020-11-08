using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Uwp_Sqlite.Models;
using Windows.Storage;

namespace Uwp_Sqlite.Data
{
    #region Configuration and Properties
    public static class SqliteContext
    {
        private static string _dbpath { get; set; }

        public static async void UseSQLite(string databaseName = "sqlite.db")
        {
            await ApplicationData.Current.LocalFolder.CreateFileAsync(databaseName, CreationCollisionOption.OpenIfExists);
            _dbpath = $"FileName={Path.Combine(ApplicationData.Current.LocalFolder.Path, databaseName)}";

            using (var db = new SqliteConnection(_dbpath))
            {
                db.Open();
                var query = "CREATE TABLE IF NOT EXISTS Todos (Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, Customer TEXT NOT NULL, Title TEXT NOT NULL, Description TEXT NOT NULL, Status TEXT NOT NULL, Created DATETIME NOT NULL);";
                var cmd = new SqliteCommand(query, db);

                await cmd.ExecuteNonQueryAsync();
                db.Close();
            }
        }
        #endregion

        #region Create Methods

        public static async Task<long> CreateTodoAsync(Todo todo)
        {
            long id = 0;

            using (var db = new SqliteConnection(_dbpath))
            {
                db.Open();

                var query = "INSERT INTO Todos VALUES(null,@Customer,@Title,@Description,@Status,@Created);";
                var cmd = new SqliteCommand(query, db);

                cmd.Parameters.AddWithValue("@Customer", todo.Customer);
                cmd.Parameters.AddWithValue("@Title", todo.Title);
                cmd.Parameters.AddWithValue("@Description", todo.Description);
                cmd.Parameters.AddWithValue("@Status", todo.Status);
                cmd.Parameters.AddWithValue("@Created", DateTime.Now);
                await cmd.ExecuteNonQueryAsync();

                cmd.CommandText = "SELECT last_insert_rowid()";
                id = (long)await cmd.ExecuteScalarAsync();

                db.Close();
            }
            return id;
        }

        #endregion

        #region Get Methods
        public static async Task<IEnumerable<Todo>> GetTodos()
        {
            var todos = new List<Todo>();

            using (var db = new SqliteConnection(_dbpath))
            {
                db.Open();

                var query = "SELECT * FROM Todos";
                var cmd = new SqliteCommand(query, db);

                var result = await cmd.ExecuteReaderAsync();

                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        var todo = new Todo(result.GetInt64(0), result.GetString(1), result.GetString(2), result.GetString(3), result.GetString(4), result.GetDateTime(5));


                        todos.Add(todo);
                    }
                }

                db.Close();
            }

            return todos;

            #endregion
        }

        public static async Task UpdateTodoStatusAsync(long id, string status)
        {
            using (var db = new SqliteConnection(_dbpath))
            {
                db.Open();

                var query = "UPDATE Todos SET Status = @Status WHERE Id = @Id";
                var cmd = new SqliteCommand(query, db);

                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@Id", id);

                await cmd.ExecuteNonQueryAsync();

                db.Close();
            }
        }
    }
}
