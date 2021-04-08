using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace RSA
{
    public class DataBase
    {
        public static List<EncryptedModel> GetAllEncrypted()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<EncryptedModel>("select * from Data",new DynamicParameters());
                return output.ToList();
            }
        }

        public static void SaveEncrypted(EncryptedModel model)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Data (y, n, e) values (@y, @n, @e)", model);
            }
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
