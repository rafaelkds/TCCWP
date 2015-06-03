using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace TCCWP.Telas.Pedidos
{
    public partial class Consulta : PhoneApplicationPage
    {
        public Consulta()
        {
            InitializeComponent();
        }

        private void btVisualizar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Telas/Pedidos/Cadastro.xaml?id=" + (listPedidos.SelectedItem as Pedido).Id, UriKind.Relative));
        }

        private void tbBusca_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbBusca.Text))
            {
                listPedidos.ItemsSource = null;
            }
            else
            {
                ControlePedido cc = new ControlePedido();
                listPedidos.ItemsSource = null;
                listPedidos.ItemsSource = cc.buscar(tbBusca.Text);
            }
        }
    }
}