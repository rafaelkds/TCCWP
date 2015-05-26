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
    public partial class Pedidos : PhoneApplicationPage
    {
        private Pedido novoPedido;
        private List<ProdutoPedido> produtos;
        public Pedidos()
        {
            InitializeComponent();
            novoPedido = new Pedido();
            produtos = new List<ProdutoPedido>();
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
                            
                            int i = 1;
                            while(i < 30)
                            {
                                produtos.Add(new ProdutoPedido() 
                                { 
                                    IdProduto = produtos[i-1].IdProduto,
                                    Quantidade = produtos[i-1].Quantidade+1,
                                    Valor = produtos[i-1].Valor+1
                                });
                                i++;
                            }
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
    }
}