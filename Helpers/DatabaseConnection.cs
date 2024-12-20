using System;
using System.Data;
using System.Data.SqlClient;

namespace StoreManagement.Helpers
{
    public class DatabaseConnection
    {
        private const string connectionString = "Data Source=LENOVO\\SQLEXPRESS;Initial Catalog=StoreManagementProject;Integrated Security=True;TrustServerCertificate=True";


        public DataTable getData(string query)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        dataTable.Load(reader);
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
            return dataTable;
        }

        public bool isExecuteSuccess(string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        return false;
                    }
                }
            }
        }
    }
}
