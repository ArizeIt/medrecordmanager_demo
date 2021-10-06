using CucmsCommon.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PVAMCommon
{
    public class SqlHelper
    {
        private string ConnectionString { get; set; }

        public SqlHelper(string connectionStr)
        {
            ConnectionString = connectionStr;
        }

        private bool ExecuteNonQuery(string commandStr, List<SqlParameter> paramList)
        {
            bool result = false;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                     conn.Open();
                }

                using (SqlCommand command = new SqlCommand(commandStr, conn))
                {
                    command.Parameters.AddRange(paramList.ToArray());
                    int count = command.ExecuteNonQuery();
                    result = count > 0;
                }
            }
            return result;
        }

        public bool InsertLog(MrmLog log)
        {
            string command = $@"INSERT INTO [dbo].[Log4Net] ([Date],[Thread],[Level],[Logger],[Message],[Exception]) VALUES (@Date, @Thread, @Level, @Logger, @Message, @Exception)";
            List<SqlParameter> paramList = new List<SqlParameter>();
            //paramList.Add(new SqlParameter("Id", log.EventId));
            paramList.Add(new SqlParameter("Date", log.Date));
            paramList.Add(new SqlParameter("Thread", log.Thread));
            paramList.Add(new SqlParameter("Level", log.Level));
            paramList.Add(new SqlParameter("Logger", log.Logger));
            paramList.Add(new SqlParameter("Message", log.Message));
            paramList.Add(new SqlParameter("Exception", log.Exception));
            return ExecuteNonQuery(command, paramList);
        }
    }
}
