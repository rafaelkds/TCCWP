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
        private static string caminhoDB = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "bd.sqlite");
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
        public static void Insert<T>(T objeto, Log log)
        {
            Conn.CreateTable<T>();
            Conn.Insert(objeto);
            Conn.CreateTable<Log>();
            Conn.Insert(log);
        }

        public static void Insert<T>(T objeto)
        {
            Conn.CreateTable<T>();
            Conn.Insert(objeto);
        }

        public static void InsertList<T>(List<T> lista, Log log)
        {
            Conn.CreateTable<T>();
            foreach(T item in lista)
                Conn.Insert(item);
            Conn.CreateTable<Log>();
            Conn.Insert(log);
        }

        public static void DeleteAll<T>()
        {
            Conn.CreateTable<T>();
            Conn.DeleteAll<T>();
        }

        public static void Update(object objeto, Log log)
        {
            Conn.Update(objeto);
            Conn.CreateTable<Log>();
            Conn.Insert(log);
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


        #region GetId
        public static string GetIdCliente()
        {
            string id = "";
            Conn.CreateTable<Id>();
            List<Id> ls = Conn.Query<Id>("select * from Id");
            int idAux;
            if (ls.Count == 0)
            {
                idAux = 1;
                Conn.Execute("Insert into Id (Cliente) values (?)", idAux);
            }
            else
            {
                idAux = ls[0].Cliente + 1;
                Conn.Execute("Update Id set Cliente = ?", idAux);
            }
            id = Identificacao + "/" + idAux;
            return id;
        }

        public static string GetIdPedido()
        {
            string id = "";
            Conn.CreateTable<Id>();
            List<Id> ls = Conn.Query<Id>("select * from Id");
            int idAux;
            if (ls.Count == 0)
            {
                idAux = 1;
                Conn.Execute("Insert into Id (Pedido) values (?)", idAux);
            }
            else
            {
                idAux = ls[0].Pedido + 1;
                Conn.Execute("Update Id set Pedido = ?", idAux);
            }
            id = Identificacao + "/" + idAux;
            return id;
        }

        public static string GetIdProdutoPedido()
        {
            string id = "";
            Conn.CreateTable<Id>();
            List<Id> ls = Conn.Query<Id>("select * from Id");
            int idAux;
            if (ls.Count == 0)
            {
                idAux = 1;
                Conn.Execute("Insert into Id (ProdutoPedido) values (?)", idAux);
            }
            else
            {
                idAux = ls[0].ProdutoPedido + 1;
                Conn.Execute("Update Id set ProdutoPedido = ?", idAux);
            }
            id = Identificacao + "/" + idAux;
            return id;
        }

        public static string GetIdReceber()
        {
            string id = "";
            Conn.CreateTable<Id>();
            List<Id> ls = Conn.Query<Id>("select * from Id");
            int idAux;
            if (ls.Count == 0)
            {
                idAux = 1;
                Conn.Execute("Insert into Id (Receber) values (?)", idAux);
            }
            else
            {
                idAux = ls[0].Receber + 1;
                Conn.Execute("Update Id set Receber = " + idAux);
            }
            id = Identificacao + "/" + idAux;
            return id;
        }

        public static string GetIdAnotacao()
        {
            string id = "";
            Conn.CreateTable<Id>();
            List<Id> ls = Conn.Query<Id>("select * from Id");
            int idAux;
            if (ls.Count == 0)
            {
                idAux = 1;
                Conn.Execute("Insert into Id (Anotacao) values (?)", idAux);
            }
            else
            {
                idAux = ls[0].Anotacao + 1;
                Conn.Execute("Update Id set Anotacao = " + idAux);
            }
            id = Identificacao + "/" + idAux;
            return id;
        }
        #endregion
    }
}
