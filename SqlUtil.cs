using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MFramewoek.EF.Tests
{
    public static class SqlUtil
    {
        public const string DatabaseName = "EFContribTest";

        public const string MasterConnectionString = "Data Source=.;Initial Catalog=master;Integrated Security=True";

        public const string TestConnectionString = "Data Source=.;Initial Catalog=EFContribTest;Integrated Security=True";

		public const string TestDefaultsConnectionString = "Data Source=.;Initial Catalog=EFDefaultsContribTest;Integrated Security=True";

        public static bool DatatabaseExists(string databaseName)
        {
            return (int)ExecuteScalar(databaseName, "Select Count(1) from sys.databases where name = '" + databaseName + "'", true) > 0;
        }

        public static bool ColumnExists(string databaseName, string tableName, string columnName)
        {
            return (int)ExecuteScalar(databaseName,
                string.Format("SELECT COUNT(1) FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME='{1}' AND COLUMN_NAME='{2}'", databaseName, tableName, columnName), false) > 0;
        }

        public static void DropDatabase(string databaseName)
        {
            if (DatatabaseExists(databaseName))
            {
                var sql =
                            "ALTER DATABASE {0} SET OFFLINE WITH ROLLBACK IMMEDIATE  " +
                            "ALTER DATABASE {0} SET ONLINE " +
                            "Drop Database {0} ";
                sql = string.Format(sql, databaseName);
                ExecuteNonQuery(databaseName, sql, true);
            }
        }

        #region Query methods

        public static object ExecuteScalar(string databaseName, string sqlStatement, bool useMaster)
        {
            object returnValue;
            using (var connection = new SqlConnection(useMaster ? MasterConnectionString : TestConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = sqlStatement;
                    returnValue = command.ExecuteScalar();
                }
            }
            return returnValue;
        }

		public static object ExecuteScalar(string coonectionString, string sqlStatement)
		{
			object returnValue;
			using (var connection = new SqlConnection(coonectionString))
			{
				connection.Open();
				using (var command = connection.CreateCommand())
				{
					command.CommandType = CommandType.Text;
					command.CommandText = sqlStatement;
					returnValue = command.ExecuteScalar();
				}
			}
			return returnValue;
		}

		public static List<object[]> ExecuteQuery(string coonectionString, string sqlStatement)
		{
			List<object[]> returnValue = new List<object[]>();
			using (var connection = new SqlConnection(coonectionString))
			{
				connection.Open();
				using (var command = connection.CreateCommand())
				{
					command.CommandType = CommandType.Text;
					command.CommandText = sqlStatement;
					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							var data = new object[reader.GetSchemaTable().Rows.Count];
							reader.GetValues(data);
							returnValue.Add(data);
						}
					}
				}
			}
			return returnValue;
		}

        public static void ExecuteNonQuery(string databaseName, string sqlStatement, bool useMaster)
        {
            using (var connection = new SqlConnection(useMaster ? MasterConnectionString : TestConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = sqlStatement;
                    command.ExecuteNonQuery();
                }
            }
        }

        #endregion
    }
}
