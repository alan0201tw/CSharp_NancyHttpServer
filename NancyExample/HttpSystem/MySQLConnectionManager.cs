using MySql.Data.MySqlClient;
using Nancy;
using System.IO;
using System.Web;

namespace NancyExample
{
    public class MySQLConnectionManager : NancyModule
    {
        // Global variables
        public static MySQLConnectionManager Instance { get; private set; }

        public MySqlConnection databaseConnection;
        
        private MySQLConfig configuration;

        private string result;

        public MySQLConnectionManager()
        {
            if (Instance == null)
                Instance = this;

            if (MySQLConfig.Load(Path.Combine(HttpRuntime.AppDomainAppPath, "DatabaseConfig.xml"), out configuration))
            {
                Start(configuration);
            }
        }

        private void Start(MySQLConfig configuration)
        {
            string connectionString = string.Format("Server = {0}; Database = {1}; UserID = {2}; Password = {3}; ", configuration.DatabaseHostAddress, configuration.Database, configuration.DatabaseUsername, configuration.DatabasePassword);
            openSqlConnection(connectionString);
        }

        // On quit
        public void OnApplicationQuit()
        {
            closeSqlConnection();
        }

        // Connect to database
        private void openSqlConnection(string connectionString)
        {
            databaseConnection = new MySqlConnection(connectionString);
            databaseConnection.Open();
            result = databaseConnection.ServerVersion;
        }

        // Disconnect from database
        private void closeSqlConnection()
        {
            databaseConnection.Close();
            databaseConnection = null;
        }

    }
}