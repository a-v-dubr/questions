using Microsoft.Data.SqlClient;

namespace Infrastructure
{
    public static class DbHelper
    {
        public static string ConnectionString { get; private set; } = @"Data Source=.\SQLEXPRESS;Database=master;Trusted_Connection=True;TrustServerCertificate=True;";

        /// <summary>
        /// Returns true if a database connection using the current connection string can be established correctly
        /// </summary>
        /// <returns></returns>
        public static bool TryEstablishDbConnection()
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if the connection can be established and sets a new value for connection string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool TrySetValidConnectionString(string input)
        {
            try
            {
                using (var connection = new SqlConnection(input))
                {
                    connection.Open();
                }
                ConnectionString = input;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
