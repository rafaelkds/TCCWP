﻿using System;
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
    public partial class UCSelecaoProduto : UserControl
    {
        public UCSelecaoProduto()
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

        private void tbValor_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            tbValor.Text = tbValor.Text.Replace(".", ",");
            tbValor.SelectionStart = tbValor.Text.Length;
        }

        private void tbValor_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Unknown && tbValor.Text.Contains(","))
            {
                e.Handled = true;
            }
        }

        private void tbQuantidade_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            tbQuantidade.Text = tbQuantidade.Text.Replace(".", ",");
            tbQuantidade.SelectionStart = tbQuantidade.Text.Length;
        }

        private void tbQuantidade_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Unknown && tbQuantidade.Text.Contains(","))
            {
                e.Handled = true;
            }
        }
    }
}
