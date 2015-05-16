using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.IO;


namespace TCCWP
{
    class BancoDeDados
    {
        private static string caminhoDB = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "test.sqlite");
        
        public static void Insert<T>(T objeto)
        {
            using (var dbConn = new SQLiteConnection(caminhoDB))
            {
                dbConn.RunInTransaction(() =>
                {
                    dbConn.CreateTable<T>();
                    dbConn.Insert(objeto);
                });
            }
        }

        public static void Update(object objeto)
        {
            using (var dbConn = new SQLiteConnection(caminhoDB))
            {
                dbConn.RunInTransaction(() =>
                {
                    dbConn.Update(objeto);
                });
            }
        }

        public static List<Cliente> ListAllCliente()
        {
            using (var dbConn = new SQLiteConnection(caminhoDB))
            {
                dbConn.CreateTable<Cliente>();
                return dbConn.Table<Cliente>().ToList();
            }
        }
        /*
        public static List<T> Query<T>()
        {
            using (var dbConn = new SQLiteConnection(caminhoDB))
            {
                return dbConn.Query<T>("");
            }
        }*/
    }
}
