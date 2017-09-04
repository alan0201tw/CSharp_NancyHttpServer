using MySql.Data.MySqlClient;
using Nancy;

namespace NancyExample
{
    public class MySQLConnectionManager : NancyModule
    {
        // Global variables
        public static MySQLConnectionManager Instance { get; private set; }

        public MySqlConnection databaseConnection;

        private string host = "140.113.67.132";
        private string id = "AlanShih";
        private string pwd = "pwd";
        private string database = "testing";
        private string result = "";

        public MySQLConnectionManager()
        {
            if (Instance == null)
                Instance = this;

            Start();
        }

        private void Start()
        {
            string connectionString = string.Format("Server = {0}; Database = {1}; UserID = {2}; Password = {3}; ", host, database, id, pwd);
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