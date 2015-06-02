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
        private static string idU = "1";
        private static string x = "";



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
            conn.BeginTransaction();
        }

        public static void CommitTransaction()
        {
            conn.Commit();
        }

        public static void RollbackTransaction()
        {
            conn.Rollback();
        }
        #endregion


        #region Normal
        public static void Insert<T>(T objeto, Log log)
        {
            conn.CreateTable<T>();
            conn.Insert(objeto);
            conn.CreateTable<Log>();
            conn.Insert(log);
        }

        public static void InsertList<T>(List<T> lista, Log log)
        {
            conn.CreateTable<T>();
            conn.InsertAll(lista);
            conn.CreateTable<Log>();
            conn.Insert(log);
        }

        public static void Delete(object objeto)
        {
            conn.Delete(objeto);
        }

        public static void DeleteList<T>(List<T> lista)
        {
            foreach (Object item in lista)
                conn.Delete<T>(item);
        }

        public static void Update(object objeto, Log log)
        {
            conn.Update(objeto);
            conn.Insert(log);
        }

        public static void Atualiza<T>(List<T> lista)
        {
            conn.CreateTable<T>();

            foreach (T objeto in lista)
                conn.Delete(objeto);
            conn.InsertAll(lista);
        }

        public static void UltSinc(Sinc ultSinc)
        {
            conn.DropTable<Sinc>();
            conn.CreateTable<Sinc>();
            conn.Insert(ultSinc);
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


        #region GetId
        public static string GetIdCliente()
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
                    id = idU + "/" + idAux;
                });
            }
            return id;
        }

        public static string GetIdPedido()
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
                    id = idU + "/" + idAux;
                });
            }
            return id;
        }

        public static string GetIdProdutoPedido()
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
                    id = idU + "/" + idAux;
                });
            }
            return id;
        }

        public static string GetIdReceber()
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
                        dbConn.Execute("Update Id set Receber = ?", idAux);
                    }
                    id = idU + "/" + idAux;
                });
            }
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
                });
            }
            //System.Windows.MessageBox.Show(storage.FileExists("teste.sqlite").ToString());        
        }

    }
}
