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
            
            ControleVendedor cv = new ControleVendedor();
            listVendedores.ItemsSource = cv.buscar("");
        }

        private void carregarPedido(string id)
        {
            ControlePedido cp = new ControlePedido();
            novoPedido = cp.buscarPorId(id);

            tbNumero.Text = novoPedido.Numero;
            dpEmissao.Value = novoPedido.DataEmissao;
            listVendedores.SelectedIndex = (new List<Vendedor>(listVendedores.ItemsSource.Cast<Vendedor>())).FindIndex(x => x.Id == novoPedido.IdVendedor);

            ControleCliente cc = new ControleCliente();
            Cliente cliente = cc.buscarPorId(novoPedido.IdCliente);
            btSelecionarCliente.Content = cliente.Nome;

            listProdutos.ItemsSource = novoPedido.Produtos;
            listVencimentos.ItemsSource = novoPedido.Receber;

            tbObservacoes.Text = novoPedido.Observacoes;
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
                            btSelecionarCliente.Content = (ucsc.listClientes.SelectedItem as Cliente).Nome;
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

        private void btGravar_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbNumero.Text) && dpEmissao.Value != null)
            {
                novoPedido.Numero = tbNumero.Text;
                novoPedido.DataEmissao = dpEmissao.Value ?? new DateTime();
                novoPedido.Observacoes = tbObservacoes.Text;
                novoPedido.IdVendedor = (listVendedores.SelectedItem as Vendedor).Id;

                ControlePedido cp = new ControlePedido();
                cp.gravar(novoPedido);
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            string id;
            if (NavigationContext.QueryString.TryGetValue("id", out id))
            {
                btAnotacoes.Visibility = System.Windows.Visibility.Visible;
                btGravar.IsEnabled = false;

                tbNumero.IsEnabled = false;
                dpEmissao.IsEnabled = false;
                btSelecionarCliente.IsEnabled = false;
                listVendedores.IsEnabled = false;

                btAdicionarProduto.IsEnabled = false;
                btRemoverProduto.IsEnabled = false;
                listProdutos.IsEnabled = false;

                dpData.IsEnabled = false;
                tbValor.IsEnabled = false;
                btAdicionarVencimento.IsEnabled = false;
                btRemoverVencimento.IsEnabled = false;

                tbObservacoes.IsEnabled = false;

                carregarPedido(id);
            }
        }

        private void btAnotacoes_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Telas/Pedidos/Anotacoes/Consulta.xaml?id=" + novoPedido.Id, UriKind.Relative));
        }
    }
}