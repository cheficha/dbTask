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
        public DbConnectedModelEngine(SqlConnection connection, SqlCommand command)
        {
            this.Connection = connection;
            this.Command = command;
        }

        public SqlConnection Connection { get; private set; }
        public SqlCommand Command { get; set; }

        private void ConfigureCommand(string text, CommandType type)
        {
            ConfigConnectionString();
            this.Command.Connection = this.Connection;

            this.Command.CommandText = text;
            this.Command.CommandType = type;



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
        public void RunStoredProcedure(SqlCommand Command)
        {
            this.Command.Connection.Open();
            this.Command.ExecuteNonQuery();
            this.Command.Connection.Close();
        }
        public void Run()
        {
            var input = string.Empty;
            while (input != "exit")
            {
                input = Console.ReadLine();
                var inputLine = input.Split().ToList();
                var spName = inputLine[0];
                switch (spName)
                {
                    case "AddEvent":
                        this.ConfigureCommand("AddEvent",CommandType.StoredProcedure);
                        this.AddParametersToACommand(int.Parse(inputLine[1]), inputLine[2], inputLine[3], bool.Parse(inputLine[4]));
                        RunStoredProcedure(this.Command);
                        Console.WriteLine("good");


                        break;
                    default:
                        break;
                }
            }
        }


    }
}
