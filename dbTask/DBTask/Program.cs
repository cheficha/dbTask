using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using DBTask.Core;

namespace DBTask
{
    class Program
    {

        static void Main(string[] args)
        {
            var connection = new SqlConnection();
            var command = new SqlCommand();
            var engine = new DbConnectedModelEngine(connection, command);
            engine.Run();
        }
    }
}
