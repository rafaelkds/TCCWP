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
        {
            listClientes.ItemsSource = BancoDeDados.ListAllCliente();
        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cliente c = new Cliente();
                c.Nome = tbNome.Text;
                c.Cpf = tbCpf.Text;
                c.Rua = tbRua.Text;
                c.Numero = tbNumero.Text;
                c.Bairro = tbBairro.Text;
                c.Cidade = Convert.ToInt32(tbCidade.Text);
                c.Cep = tbCep.Text;

                ControleCliente cc = new ControleCliente();
                cc.gravar(c);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        

    }
}