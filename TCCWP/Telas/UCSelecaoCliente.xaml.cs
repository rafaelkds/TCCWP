using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace TCCWP
{
    public partial class UCSelecaoCliente : UserControl
    {
        public UCSelecaoCliente()
        {
            InitializeComponent();
        }

        private void tbBusca_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbBusca.Text))
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
    }
}
