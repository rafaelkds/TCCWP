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
                        identificacao = lista[0].Identificacao;
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
            Conn.DeleteAll<T>();
        }

        public static void Update(object objeto, Log log)
        {
            Conn.Update(objeto);
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

        public static void UltSinc(Sinc ultSinc)
        {
            Conn.CreateTable<Sinc>();
            Conn.DeleteAll<Sinc>();
            Conn.Insert(ultSinc);
        }
        #endregion


        #region RunInTransaction
        public static void InsertRIT<T>(T objeto, Log log)
        {
            using (var dbConn = new SQLiteConnection(caminhoDB))
            {
                dbConn.RunInTransaction(() =>
                {
                    dbConn.CreateTable<T>();
                    dbConn.Insert(objeto);
                    dbConn.CreateTable<Log>();
                    dbConn.Insert(log);
                });
            }
        }

        public static void InsertListRIT<T>(List<T> lista, Log log)
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

        public static void DeleteRIT(object objeto)
        {
            using (var dbConn = new SQLiteConnection(caminhoDB))
            {
                dbConn.RunInTransaction(() =>
                {
                    dbConn.Delete(objeto);
                });
            }
        }

        public static void DeleteListRIT(List<object> lista)
        {
            using (var dbConn = new SQLiteConnection(caminhoDB))
            {
                dbConn.RunInTransaction(() =>
                {
                    foreach(Object item in lista)
                        dbConn.Delete(item);
                });
            }
        }

        public static void UpdateRIT(object objeto, Log log)
        {
            using (var dbConn = new SQLiteConnection(caminhoDB))
            {
                dbConn.RunInTransaction(() =>
                {
                    dbConn.Update(objeto);
                    dbConn.Insert(log);
                });
            }
        }

        public static void AtualizaRIT<T>(List<T> lista)
        {
            using (var dbConn = new SQLiteConnection(caminhoDB))
            {
                dbConn.RunInTransaction(() =>
                {
                    dbConn.CreateTable<T>();

                    foreach (T objeto in lista)
                        dbConn.Delete(objeto);
                    dbConn.InsertAll(lista);
                });
            }
        }

        public static void UltSincRIT(Sinc ultSinc)
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

        public static List<Cliente> ListAllCliente()
        {
            using (var dbConn = new SQLiteConnection(caminhoDB))
            {
                dbConn.CreateTable<Cliente>();
                return dbConn.Table<Cliente>().ToList();
            }
        }

        public static List<Produto> ListAllProduto()
        {
            using (var dbConn = new SQLiteConnection(caminhoDB))
            {
                dbConn.CreateTable<Produto>();
                return dbConn.Table<Produto>().ToList();
            }
        }


        #region GetIdRIT
        public static string GetIdClienteRIT()
        {
            string id = "";
            using (var dbConn = new SQLiteConnection(caminhoDB))
            {
                dbConn.RunInTransaction(() =>
                {
                    dbConn.CreateTable<Id>();
                    List<Id> ls = dbConn.Query<Id>("select * from Id");
                    int idAux;
                    if (ls.Count == 0)
                    {
                        idAux = 1;
                        dbConn.Execute("Insert into Id (Cliente) values (?)", idAux);
                    }
                    else
                    {
                        idAux = ls[0].Cliente + 1;
                        dbConn.Execute("Update Id set Cliente = ?", idAux);
                    }
                    id = Identificacao + "/" + idAux;
                });
            }
            return id;
        }

        public static string GetIdPedidoRIT()
        {
            string id = "";
            using (var dbConn = new SQLiteConnection(caminhoDB))
            {
                dbConn.RunInTransaction(() =>
                {
                    dbConn.CreateTable<Id>();
                    List<Id> ls = dbConn.Query<Id>("select * from Id");
                    int idAux;
                    if (ls.Count == 0)
                    {
                        idAux = 1;
                        dbConn.Execute("Insert into Id (Pedido) values (?)", idAux);
                    }
                    else
                    {
                        idAux = ls[0].Pedido + 1;
                        dbConn.Execute("Update Id set Pedido = ?", idAux);
                    }
                    id = Identificacao + "/" + idAux;
                });
            }
            return id;
        }

        public static string GetIdProdutoPedidoRIT()
        {
            string id = "";
            using (var dbConn = new SQLiteConnection(caminhoDB))
            {
                dbConn.RunInTransaction(() =>
                {
                    dbConn.CreateTable<Id>();
                    List<Id> ls = dbConn.Query<Id>("select * from Id");
                    int idAux;
                    if (ls.Count == 0)
                    {
                        idAux = 1;
                        dbConn.Execute("Insert into Id (ProdutoPedido) values (?)", idAux);
                    }
                    else
                    {
                        idAux = ls[0].ProdutoPedido + 1;
                        dbConn.Execute("Update Id set ProdutoPedido = ?", idAux);
                    }
                    id = Identificacao + "/" + idAux;
                });
            }
            return id;
        }

        public static string GetIdReceberRIT()
        {
            string id = "";
            using (var dbConn = new SQLiteConnection(caminhoDB))
            {
                dbConn.RunInTransaction(() =>
                {
                    dbConn.CreateTable<Id>();
                    List<Id> ls = dbConn.Query<Id>("select * from Id");
                    int idAux;
                    if (ls.Count == 0)
                    {
                        idAux = 1;
                        dbConn.Execute("Insert into Id (Receber) values (?)", idAux);
                    }
                    else
                    {
                        idAux = ls[0].Receber + 1;
                        dbConn.Execute("Update Id set Receber = " + idAux);
                    }
                    id = Identificacao + "/" + idAux;
                });
            }
            return id;
        }
        #endregion

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

        

        public static void teste()
        {
            //System.IO.IsolatedStorage.IsolatedStorageFile storage = System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication();
            //System.Windows.MessageBox.Show(storage.FileExists("teste.sqlite").ToString());
            //storage.DeleteFile("teste.sqlite");
            //System.Windows.MessageBox.Show(storage.FileExists("teste.sqlite").ToString());
            using (var dbConn = new SQLiteConnection(caminhoDB))
            {
                dbConn.RunInTransaction(() =>
                {
                    dbConn.CreateTable<Id>();
                    dbConn.CreateTable<Log>();
                    dbConn.CreateTable<Sinc>();
                    dbConn.CreateTable<Cliente>();
                    dbConn.CreateTable<Pedido>();
                    dbConn.CreateTable<ProdutoPedido>();
                    dbConn.CreateTable<Receber>();
                    dbConn.CreateTable<Produto>();
                    dbConn.CreateTable<Anotacao>();
                });
            }
            //System.Windows.MessageBox.Show(storage.FileExists("teste.sqlite").ToString());        
        }

    }
}
