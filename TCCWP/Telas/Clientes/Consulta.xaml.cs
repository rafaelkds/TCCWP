using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace TCCWP.Telas.Clientes
{
    public partial class Consulta : PhoneApplicationPage
    {
        public Consulta()
        {
            InitializeComponent();
            listClientes.ItemsSource = BancoDeDados.ListAllCliente();
        }

        private void tbBusca_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(tbBusca.Text))
            {
                listClientes.ItemsSource = null;
            }
            else
            {
                ControleCliente cc = new ControleCliente();
                listClientes.ItemsSource = null;
                listClientes.ItemsSource = cc.buscar(tbBusca.Text);
            }
        }

        private void btVisualizar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Telas/Clientes/Cadastro.xaml?id="+(listClientes.SelectedItem as Cliente).Id, UriKind.Relative));
        }
    }
}