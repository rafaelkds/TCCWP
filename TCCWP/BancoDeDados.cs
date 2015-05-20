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
        
        public static void Insert<T>(List<T> lista, Log log)
        {
            using (var dbConn = new SQLiteConnection(caminhoDB))
            {
                dbConn.RunInTransaction(() =>
                {
                    dbConn.CreateTable<T>();
                    dbConn.InsertAll(lista);
                    dbConn.CreateTable<Log>();
                    dbConn.Insert(log);
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
        
        public static List<T> Query<T>(string query)
        {
            using (var dbConn = new SQLiteConnection(caminhoDB))
            {
                TableMapping tm = new TableMapping(typeof(T));
                dbConn.CreateTable<T>();
                return new List<T>(dbConn.Query(tm, query).Cast<T>());
            }
        }

        public static void Atualiza<T>(List<T> lista)
        {
            using (var dbConn = new SQLiteConnection(caminhoDB))
            {
                dbConn.RunInTransaction(() =>
                {
                    dbConn.CreateTable<T>();
                    foreach (T objeto in lista)
                        dbConn.InsertOrReplace(objeto);
                });
            }
        }

        public static void UltSinc(Sinc ultSinc)
        {
            using (var dbConn = new SQLiteConnection(caminhoDB))
            {
                dbConn.RunInTransaction(() =>
                {
                    dbConn.DropTable<Sinc>();
                    dbConn.CreateTable<Sinc>();
                    dbConn.Insert(ultSinc);
                });
            }
        }
        
        /*
        public static void NonQuer(string query)
        {
            using (var dbConn = new SQLiteConnection(caminhoDB))
            {
                dbConn.RunInTransaction(() =>
                {
                    dbConn.Execute(query);
                });
            }
        }*/
    }
}
