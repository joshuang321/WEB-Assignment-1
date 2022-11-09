using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace web_asignment1_Y2S1_2022.Models
{
    public class SQL
    {
        public static readonly SqlConnection conn = SQL.Initialize();

        private static SqlConnection Initialize()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
            IConfiguration configuration = builder.Build();
            string strConn = configuration.GetConnectionString(
            "ZZFashionConnectionString");
            //Instantiate a SqlConnection object with the
            //Connection String read.
            return new SqlConnection(strConn);
        }
    }
}
