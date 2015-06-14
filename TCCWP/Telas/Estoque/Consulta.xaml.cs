using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace TCCWP.Telas.Estoque
{
    public partial class Consulta : PhoneApplicationPage
    {
        public Consulta()
        {
            InitializeComponent();
        }

        private void tbBusca_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbBusca.Text))
            {
                listProdutos.ItemsSource = null;
            }
            else
            {
                ControleProduto cpr = new ControleProduto();
                listProdutos.ItemsSource = null;
                listProdutos.ItemsSource = cpr.buscar(tbBusca.Text);
            }
        }
    }
}