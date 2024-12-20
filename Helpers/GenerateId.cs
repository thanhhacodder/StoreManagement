using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;


namespace StoreManagement.Helpers
{
    public class GenerateId
    {
        DatabaseConnection dbConnection = new DatabaseConnection();
        public int Generate(string table) 
        {
            string column = "";
            if (table.Substring(table.Length - 3) == "ies")
            {
                column = $"{table.Substring(0, table.Length - 3)}yID";
            }
            else
            {
                column = $"{table.Substring(0, table.Length - 1)}ID";
            }

            string query = $"SELECT MAX({column}) AS MaxID FROM {table}";
            DataTable dataTable = dbConnection.getData(query);
            int maxId = 0;

            if (dataTable.Rows.Count > 0 && dataTable.Rows[0]["MaxID"] != DBNull.Value)
            {
                maxId = Convert.ToInt32(dataTable.Rows[0]["MaxID"]);
            }

            int newId = maxId + 1;

            return newId;
        }
    }
}
