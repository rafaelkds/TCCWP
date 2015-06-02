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
    public partial class Pedidos : PhoneApplicationPage
    {
        private Pedido novoPedido;
        private List<ProdutoPedido> produtos;
        private List<Receber> vencimentos;
        public Pedidos()
        {
            InitializeComponent();
            novoPedido = new Pedido();
            produtos = new List<ProdutoPedido>();
            vencimentos = new List<Receber>();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UCSelecaoCliente ucsc = new UCSelecaoCliente();
            CustomMessageBox cmb = new CustomMessageBox()
            {
                Content = ucsc,
                //Height = 500,
                //Opacity = 0.7,
                LeftButtonContent = "Selecionar",
                RightButtonContent = "Cancelar"
            };
            cmb.Dismissing += (s1, e1) =>
            {
                switch (e1.Result)
                {
                    case CustomMessageBoxResult.LeftButton:
                        if (ucsc.listClientes.SelectedItem != null)
                        {
                            novoPedido.IdCliente = (ucsc.listClientes.SelectedItem as Cliente).Id;
                            tbCliente.Text = (ucsc.listClientes.SelectedItem as Cliente).Nome;
                        }
                        break;
                }
            };
            cmb.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            UCSelecaoProduto ucsp = new UCSelecaoProduto();
            CustomMessageBox cmb = new CustomMessageBox()
            {
                Content = ucsp,
                LeftButtonContent = "Adicionar",
                RightButtonContent = "Cancelar"
            };
            cmb.Dismissing += (s1, e1) =>
            {
                switch (e1.Result)
                {
                    case CustomMessageBoxResult.LeftButton:
                        if (ucsp.listProdutos.SelectedItem != null)
                        {
                            ProdutoPedido novoProdutoPedido = new ProdutoPedido();
                            novoProdutoPedido.IdProduto = (ucsp.listProdutos.SelectedItem as Produto).Id;
                            novoProdutoPedido.Quantidade = Convert.ToDecimal(ucsp.tbQuantidade.Text);
                            novoProdutoPedido.Valor = Convert.ToDecimal(ucsp.tbValor.Text);
                            produtos.Add(novoProdutoPedido);
                            
                            listProdutos.ItemsSource = null;
                            listProdutos.ItemsSource = produtos;

                            decimal tQtde = 0, tValor = 0;
                            foreach(ProdutoPedido item in produtos)
                            {
                                tQtde += item.Quantidade;
                                tValor += item.Valor;
                            }
                            tbTotalQuantidade.Text = tQtde.ToString("0.00");
                            tbTotalValor.Text = tValor.ToString("0.00");
                        }
                        break;
                }
            };
            cmb.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            produtos.Remove(listProdutos.SelectedItem as ProdutoPedido);
            listProdutos.ItemsSource = null;
            listProdutos.ItemsSource = produtos;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Receber novoReceber = new Receber();
            novoReceber.Vencimento = dpData.Value ?? new DateTime();
            novoReceber.Valor = Convert.ToDecimal(tbValor.Text);
            vencimentos.Add(novoReceber);

            listVencimentos.ItemsSource = null;
            listVencimentos.ItemsSource = vencimentos;
                        
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            vencimentos.Remove(listVencimentos.SelectedItem as Receber);
            listVencimentos.ItemsSource = null;
            listVencimentos.ItemsSource = vencimentos;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(tbNumero.Text) && dpEmissao.Value != null)
            {
                novoPedido.DataEmissao = dpEmissao.Value ?? new DateTime();
                novoPedido.Numero = tbNumero.Text;

                novoPedido.Id = BancoDeDados.GetIdPedido();
                novoPedido.IdVendedor = 1;
                novoPedido.Observacoes = tbObservacoes.Text;
                decimal total = 0;
                foreach(ProdutoPedido item in produtos)
                {
                    item.Id = BancoDeDados.GetIdProdutoPedido();
                    item.IdPedido = novoPedido.Id;
                    total += item.Quantidade * item.Valor;
                }
                novoPedido.Valor = total;

                foreach(Receber item in vencimentos)
                {
                    item.Id = BancoDeDados.GetIdReceber();
                    item.IdPedido = novoPedido.Id;
                }

                ControlePedido cp = new ControlePedido();
                cp.gravar(novoPedido);

                ControleProdutoPedido cpp = new ControleProdutoPedido();
                cpp.gravarLista(produtos);

                ControleReceber cr = new ControleReceber();
                cr.gravarLista(vencimentos);
            }
        }
    }
}