using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBTask.Core
{
    public class DbConnectedModelEngine
    {
        public DbConnectedModelEngine(IDbConnection connection, IDbCommand command)
        {
            this.Connection = connection;
            this.Command = command;
        }

        public IDbConnection Connection { get; private set; }
        public IDbCommand Command { get; set; }

        public void ConfigureCommand(string text, CommandType type, IDbConnection connection)
        {
            using (this.Command.Connection)
            {
                this.Command.Connection = connection;
                this.Command.Connection.Open();
                this.Command.CommandText = text;
                this.Command.CommandType = type;
                this.Command.ExecuteNonQuery();

            };
        }
        private void ConfigConnectionString()
        {
            this.Connection.ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog = ChefichaDB;Integrated Security=SSPI;";

        }
        private void AddParametersToACommand(int id, string nameS, string dateOfStart, bool isActive)
        {
            SqlParameter eventId = new SqlParameter("@ID", SqlDbType.Int, 255, "EventiD");
            eventId.Value = id;
            SqlParameter name = new SqlParameter("@Name", SqlDbType.NVarChar, 50, "Name");
            name.Value = nameS;
            SqlParameter date = new SqlParameter("@StartingDate", SqlDbType.DateTime, 50, "StartingDate");
            date.Value = dateOfStart;
            SqlParameter IsActive = new SqlParameter("@IsActive", SqlDbType.Bit, 1, "IsActive");
            IsActive.Value = isActive;
            this.Command.Parameters.Add(eventId);
            this.Command.Parameters.Add(name);
            this.Command.Parameters.Add(date);
            this.Command.Parameters.Add(IsActive);
        }
        public void Run()
        {

        }

       
    }
}
