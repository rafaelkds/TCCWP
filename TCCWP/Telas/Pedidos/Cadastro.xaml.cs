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
    public partial class Cadastro : PhoneApplicationPage
    {
        private Pedido novoPedido;

        public Cadastro()
        {
            InitializeComponent();
            novoPedido = new Pedido();
            novoPedido.Produtos = new List<ProdutoPedido>();
            novoPedido.Receber = new List<Receber>();
        }

        private void btSelecionarCliente_Click(object sender, RoutedEventArgs e)
        {
            UCSelecaoCliente ucsc = new UCSelecaoCliente();
            CustomMessageBox cmb = new CustomMessageBox()
            {
                Content = ucsc,
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

        private void btAdicionarProduto_Click(object sender, RoutedEventArgs e)
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
                            novoPedido.Produtos.Add(novoProdutoPedido);

                            listProdutos.ItemsSource = null;
                            listProdutos.ItemsSource = novoPedido.Produtos;

                            decimal tQtde = 0, tValor = 0;
                            foreach (ProdutoPedido item in novoPedido.Produtos)
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

        private void btRemoverProduto_Click(object sender, RoutedEventArgs e)
        {
            novoPedido.Produtos.Remove(listProdutos.SelectedItem as ProdutoPedido);
            listProdutos.ItemsSource = null;
            listProdutos.ItemsSource = novoPedido.Produtos;
        }

        private void btAdicionarVencimento_Click(object sender, RoutedEventArgs e)
        {
            Receber novoReceber = new Receber();
            novoReceber.Vencimento = dpData.Value ?? new DateTime();
            novoReceber.Valor = Convert.ToDecimal(tbValor.Text);
            novoPedido.Receber.Add(novoReceber);

            listVencimentos.ItemsSource = null;
            listVencimentos.ItemsSource = novoPedido.Receber;
        }

        private void btRemoverVencimento_Click(object sender, RoutedEventArgs e)
        {
            novoPedido.Receber.Remove(listVencimentos.SelectedItem as Receber);
            listVencimentos.ItemsSource = null;
            listVencimentos.ItemsSource = novoPedido.Receber;
        }

        private void tbGravar_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbNumero.Text) && dpEmissao.Value != null)
            {
                novoPedido.DataEmissao = dpEmissao.Value ?? new DateTime();
                novoPedido.Numero = tbNumero.Text;

                novoPedido.Id = BancoDeDados.GetIdPedido();
                novoPedido.IdVendedor = 1;
                novoPedido.Observacoes = tbObservacoes.Text;
                decimal total = 0;
                foreach (ProdutoPedido item in novoPedido.Produtos)
                {
                    item.Id = BancoDeDados.GetIdProdutoPedido();
                    item.IdPedido = novoPedido.Id;
                    total += item.Quantidade * item.Valor;
                }
                novoPedido.Valor = total;

                foreach (Receber item in novoPedido.Receber)
                {
                    item.Id = BancoDeDados.GetIdReceber();
                    item.IdPedido = novoPedido.Id;
                }

                ControlePedido cp = new ControlePedido();
                cp.gravar(novoPedido);

                ControleProdutoPedido cpp = new ControleProdutoPedido();
                cpp.gravarLista(novoPedido.Produtos);

                ControleReceber cr = new ControleReceber();
                cr.gravarLista(novoPedido.Receber);
            }
        }
    }
}