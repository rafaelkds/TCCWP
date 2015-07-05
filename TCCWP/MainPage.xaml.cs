using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TCCWP.Resources;
using SQLite;

namespace TCCWP
{
    public partial class MainPage : PhoneApplicationPage
    {
        
        public MainPage()
        {
            InitializeComponent();
        }

        private void btClientes_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Telas/Clientes/Menu.xaml", UriKind.Relative));
        }

        private void btRelatorios_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Telas/Relatorios/Menu.xaml", UriKind.Relative));
        }

        private void btEstoque_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Telas/Estoque/Consulta.xaml", UriKind.Relative));
        }

        private void btPedidos_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Telas/Pedidos/Menu.xaml", UriKind.Relative));
        }

        private void btConfigurar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Telas/Configurar/Configurar.xaml", UriKind.Relative));
        }

        private void btSincronizar_Click(object sender, RoutedEventArgs e)
        {
            this.IsEnabled = false;
            btSincronizar.Content = "Sincronizando";
            Controle.Sincronizacao sinc = new Controle.Sincronizacao();
            sinc.Sincronizar(this);
        }

        public void mensagemSincronizacao(string mensagem)
        {
            btSincronizar.Content = "Sincronizar";
            this.IsEnabled = true;
            MessageBox.Show(mensagem);
        }
    }
}