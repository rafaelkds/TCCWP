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

        public static void Delete(object objeto)
        {
            using (var dbConn = new SQLiteConnection(caminhoDB))
            {
                dbConn.RunInTransaction(() =>
                {
                    dbConn.Delete(objeto);
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

        public static List<Produto> ListAllProduto()
        {
            using (var dbConn = new SQLiteConnection(caminhoDB))
            {
                dbConn.CreateTable<Produto>();
                return dbConn.Table<Produto>().ToList();
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

        public static void AtualizaI<T>(T objeto)
        {
            try
            {
                using (var dbConn = new SQLiteConnection(caminhoDB))
                {
                    dbConn.RunInTransaction(() =>
                    {
                        Cliente c = objeto as Cliente;
                        x += "\n" + (c.Id + " // " + c.Nome + " // " + c.Numero + " // " + c.Rua + " // " + c.Telefone + " // " 
                            + c.Email + " // " + c.Cpf + " // " + c.Complemento + " // " + c.Cidade + " // " + c.Cep + " // " + c.Bairro);
                        dbConn.CreateTable<T>();
                        dbConn.InsertOrReplace(objeto);
                    });
                }
            }catch(Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
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
