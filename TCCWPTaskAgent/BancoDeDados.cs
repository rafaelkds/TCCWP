using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.IO;


namespace TCCWPTaskAgent
{
    class BancoDeDados
    {
        private static string caminhoDB = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "teste.sqlite");
        private static int identificacao;
        private static int Identificacao
        {
            get
            {
                if(identificacao == 0)
                {
                    List<Sinc> lista = Query<Sinc>("select * from Sinc");
                    if (lista.Count > 0)
                        identificacao = lista[0].IdCelular;
                }
                return identificacao;
            }
        }


        #region Connection Transaction
        private static SQLiteConnection conn;
        private static SQLiteConnection Conn
        {
            get
            {
                if (conn == null)
                {
                    conn = new SQLiteConnection(caminhoDB);
                }
                return conn;
            }
        }

        public static void BeginTransaction()
        {
            Conn.BeginTransaction();
        }

        public static void CommitTransaction()
        {
            Conn.Commit();
        }

        public static void RollbackTransaction()
        {
            Conn.Rollback();
        }
        #endregion


        #region Normal
        public static void Insert<T>(T objeto)
        {
            Conn.CreateTable<T>();
            Conn.Insert(objeto);
        }

        public static void DeleteAll<T>()
        {
            Conn.DeleteAll<T>();
        }

        public static void Atualiza<T>(List<T> lista)
        {
            Conn.CreateTable<T>();

            foreach (T objeto in lista)
            {
                Conn.Delete(objeto);
                Conn.Insert(objeto);
            }
        }

        public static void UltSinc(Sinc ultSinc)///////////remover
        {
            Conn.CreateTable<Sinc>();
            Conn.DeleteAll<Sinc>();
            Conn.Insert(ultSinc);
        }
        #endregion



        public static List<T> Query<T>(string query)
        {
            using (var dbConn = new SQLiteConnection(caminhoDB))
            {
                TableMapping tm = new TableMapping(typeof(T));
                dbConn.CreateTable<T>();
                return new List<T>(dbConn.Query(tm, query).Cast<T>());
            }
        }
    }
}
