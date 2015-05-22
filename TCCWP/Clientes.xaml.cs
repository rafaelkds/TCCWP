using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TCCWP.ServiceReference1;
using SQLite;
using TCCWP.Resources;
using System.IO;

namespace TCCWP
{
    public partial class Clientes : PhoneApplicationPage
    {
        public Clientes()
        {
            InitializeComponent();
        }

        

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {/*
            Service1Client client = new Service1Client();
            //client.Endpoint.Binding.ReceiveTimeout = TimeSpan.FromMinutes(1);
            client.GetClientesCompleted += GetClientesCompleted;
            client.GetClientesAsync();*/
            /*try
            {*/
            /*SQLiteConnection dbConn = new SQLiteConnection(Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "test.sqlite"));
            dbConn.CreateTable<Cliente>();
                listClientes.ItemsSource = dbConn.Table<Cliente>();
            /*}
            catch (Exception ex)
            { }*/
            listClientes.ItemsSource = BancoDeDados.ListAllCliente();
        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {/*
                ServiceReference1.Cliente c = new ServiceReference1.Cliente();
                c.Nome = tbNome.Text;
                c.Cpf = tbCpf.Text;
                c.Rua = tbRua.Text;
                c.Numero = tbNumero.Text;
                c.Bairro = tbBairro.Text;
                c.Cidade = Convert.ToInt32(tbCidade.Text);
                c.Cep = tbCep.Text;
                //c.Complemento = tbComplemento.Text;
                //c.Telefone = tbTelefone.Text;
                //c.Email = tbEmail.Text;

                Service1Client client = new Service1Client();
                client.InsertClienteCompleted += InsertClienteCompleted;
                client.InsertClienteAsync(c);*/

                
                Cliente c = new Cliente();
                c.Nome = tbNome.Text;
                c.Cpf = tbCpf.Text;
                c.Rua = tbRua.Text;
                c.Numero = tbNumero.Text;
                c.Bairro = tbBairro.Text;
                c.Cidade = Convert.ToInt32(tbCidade.Text);
                c.Cep = tbCep.Text;
/*
                SQLiteConnection dbConn = new SQLiteConnection(Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "test.sqlite"));
                dbConn.CreateTable<Cliente>();
                dbConn.Insert(c);
                dbConn.Close();
                */
                ControleCliente cc = new ControleCliente();
                List<Cliente> lista = new List<Cliente>();
                lista.Add(c);
                cc.gravar(lista);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        

    }
}