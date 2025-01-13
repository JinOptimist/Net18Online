using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.IO;

namespace WebPortalEverthing.E2E.LoadTest.DataBase
{
    public class DbHelper
    {
        string connectionString;

        /// <summary>
        /// Откройте Tools > NuGet Package Manager > Manage NuGet Packages for Solution.
        ///  Найдите и установите:
        /// Microsoft.Extensions.Configuration
        ///  Microsoft.Extensions.Configuration.Json
        ///  Microsoft.Extensions.Configuration.FileExtensions
        /// </summary>
        public DbHelper()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Проверяем, существует ли параметр "connectionStringMsSQL"
            var connectionStringValue = configuration.GetConnectionString("connectionStringMsSQL");

            if (string.IsNullOrEmpty(connectionStringValue))
            {
                throw new InvalidOperationException("Параметр 'connectionStringMsSQL' не найден в конфигурации или пуст.");
            }

            connectionString = configuration.GetConnectionString("connectionStringMsSQL");
            Console.WriteLine($"Connection String: {connectionString}");
        }

        /// <summary>
        /// @Username: Используется параметризированный запрос для предотвращения SQL-инъекций.
        ///SqlCommand.ExecuteNonQuery(): Выполняет запрос на изменение данных(в данном случае DELETE).
        /// </summary>
        /// <param name="username"></param>
        public int DeleteUserByUsername(string username)
        {
            string query = "DELETE FROM Users WHERE Username = @Username";

            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@Username", SqlDbType.NVarChar).Value = username;
                        rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"{rowsAffected} пользователь(ей) удалено.");
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Ошибка при удалении пользователя: {ex.Message}");
            }
            return rowsAffected;
        }
    }
}
