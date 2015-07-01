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

            tbNumero.Text = novoPedido.Id;
            dpEmissao.Value = novoPedido.DataEmissao;
            listVendedores.SelectedIndex = (new List<Vendedor>(listVendedores.ItemsSource.Cast<Vendedor>())).FindIndex(x => x.Id == novoPedido.IdVendedor);

            ControleCliente cc = new ControleCliente();
            Cliente cliente = cc.buscarPorId(novoPedido.IdCliente);
            btSelecionarCliente.Content = cliente.Nome;

            listProdutos.ItemsSource = novoPedido.Produtos;
            listVencimentos.ItemsSource = novoPedido.Receber;

            decimal tValor = 0;
            foreach (ProdutoPedido item in novoPedido.Produtos)
            {
                tValor += item.Valor * item.Quantidade;
            }
            tbTotalValor.Text = tValor.ToString("0.00");
            tbTotalRestante.Text = (tValor - (string.IsNullOrWhiteSpace(tbTotalReceber.Text)
                ? 0 : Convert.ToDecimal(tbTotalReceber.Text))).ToString("0.00");

            tValor = 0;
            foreach (Receber item in novoPedido.Receber)
            {
                tValor += item.Valor;
            }
            tbTotalReceber.Text = tValor.ToString("0.00");
            tbTotalRestante.Text = ((string.IsNullOrWhiteSpace(tbTotalValor.Text)
                ? 0 : Convert.ToDecimal(tbTotalValor.Text)) - tValor).ToString("0.00");

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
                        if (ucsc.listClientes.SelectedItem == null)
                            e1.Cancel = true;
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
                        if (ucsp.listProdutos.SelectedItem == null || string.IsNullOrWhiteSpace(ucsp.tbQuantidade.Text) || string.IsNullOrWhiteSpace(ucsp.tbValor.Text))
                            e1.Cancel = true;
                        if (ucsp.listProdutos.SelectedItem != null && !string.IsNullOrWhiteSpace(ucsp.tbQuantidade.Text) && !string.IsNullOrWhiteSpace(ucsp.tbValor.Text))
                        {
                            ProdutoPedido novoProdutoPedido = new ProdutoPedido();
                            novoProdutoPedido.IdProduto = (ucsp.listProdutos.SelectedItem as Produto).Id;
                            novoProdutoPedido.Quantidade = Convert.ToDecimal(ucsp.tbQuantidade.Text);
                            novoProdutoPedido.Valor = Convert.ToDecimal(ucsp.tbValor.Text);
                            novoProdutoPedido.Produto = ucsp.listProdutos.SelectedItem as Produto;
                            novoPedido.Produtos.Add(novoProdutoPedido);

                            listProdutos.ItemsSource = null;
                            listProdutos.ItemsSource = novoPedido.Produtos;

                            decimal tValor = 0;
                            foreach (ProdutoPedido item in novoPedido.Produtos)
                            {
                                tValor += item.Valor * item.Quantidade;
                            }
                            tbTotalValor.Text = tValor.ToString("0.00");
                            tbTotalRestante.Text = (tValor - (string.IsNullOrWhiteSpace(tbTotalReceber.Text)
                                ? 0 : Convert.ToDecimal(tbTotalReceber.Text))).ToString("0.00");
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
            decimal tValor = 0;
            foreach (ProdutoPedido item in novoPedido.Produtos)
            {
                tValor += item.Valor * item.Quantidade;
            }
            tbTotalValor.Text = tValor.ToString("0.00");
            tbTotalRestante.Text = (tValor - (string.IsNullOrWhiteSpace(tbTotalReceber.Text)
                ? 0 : Convert.ToDecimal(tbTotalReceber.Text))).ToString("0.00");
        }

        private void btAdicionarVencimento_ValueChanged(object sender, DateTimeValueChangedEventArgs e)
        {
            if (btAdicionarVencimento.Value != null)
            {
                StackPanel sp = new StackPanel();
                sp.Margin = new Thickness(0, 0, 10, 0);
                TextBlock text = new TextBlock();
                text.Text = "Valor:";
                TextBox tb = new TextBox();

                System.Windows.Input.InputScope scope = new System.Windows.Input.InputScope();
                System.Windows.Input.InputScopeName scopeName = new System.Windows.Input.InputScopeName();
                scopeName.NameValue = System.Windows.Input.InputScopeNameValue.Number;
                scope.Names.Add(scopeName);
                tb.InputScope = scope;

                sp.Children.Add(text);
                sp.Children.Add(tb);
                
                CustomMessageBox cmb = new CustomMessageBox()
                {
                    Content = sp,
                    LeftButtonContent = "Adicionar",
                    RightButtonContent = "Cancelar"
                };
                cmb.Dismissing += (s1, e1) =>
                {
                    switch (e1.Result)
                    {
                        case CustomMessageBoxResult.LeftButton:
                            if (string.IsNullOrWhiteSpace(tb.Text))
                                e1.Cancel = true;
                            if (!string.IsNullOrWhiteSpace(tb.Text))
                            {
                                Receber novoReceber = new Receber();
                                novoReceber.Vencimento = btAdicionarVencimento.Value ?? new DateTime();
                                novoReceber.Valor = Convert.ToDecimal(tb.Text);
                                novoPedido.Receber.Add(novoReceber);

                                listVencimentos.ItemsSource = null;
                                listVencimentos.ItemsSource = novoPedido.Receber;

                                decimal tValor = 0;
                                foreach (Receber item in novoPedido.Receber)
                                {
                                    tValor += item.Valor;
                                }
                                tbTotalReceber.Text = tValor.ToString("0.00");
                                tbTotalRestante.Text = ((string.IsNullOrWhiteSpace(tbTotalValor.Text)
                                    ? 0 : Convert.ToDecimal(tbTotalValor.Text)) - tValor).ToString("0.00");
                            }
                            break;
                    }
                };

                cmb.Show();
            }
        }

        private void btAdicionarVencimento_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            btAdicionarVencimento.Value = null;
        }

        private void btRemoverVencimento_Click(object sender, RoutedEventArgs e)
        {
            novoPedido.Receber.Remove(listVencimentos.SelectedItem as Receber);
            listVencimentos.ItemsSource = null;
            listVencimentos.ItemsSource = novoPedido.Receber;
            decimal tValor = 0;
            foreach (Receber item in novoPedido.Receber)
            {
                tValor += item.Valor;
            }
            tbTotalReceber.Text = tValor.ToString("0.00");
            tbTotalRestante.Text = ((string.IsNullOrWhiteSpace(tbTotalValor.Text)
                ? 0 : Convert.ToDecimal(tbTotalValor.Text)) - tValor).ToString("0.00");
        }

        private void btGravar_Click(object sender, RoutedEventArgs e)
        {
            string retorno = verificaCampos();
            if (!string.IsNullOrWhiteSpace(retorno))
            {
                MessageBox.Show(retorno);
                return;
            }
            novoPedido.DataEmissao = dpEmissao.Value ?? new DateTime();
            novoPedido.Observacoes = tbObservacoes.Text;
            novoPedido.IdVendedor = (listVendedores.SelectedItem as Vendedor).Id;

            ControlePedido cp = new ControlePedido();
            cp.gravar(novoPedido);
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

                btAdicionarVencimento.IsEnabled = false;
                btRemoverVencimento.IsEnabled = false;
                listVencimentos.IsEnabled = false;

                tbObservacoes.IsEnabled = false;

                carregarPedido(id);
            }
        }

        private void btAnotacoes_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Telas/Pedidos/Anotacoes/Consulta.xaml?id=" + novoPedido.Id, UriKind.Relative));
        }

        private string verificaCampos()
        {
            if (string.IsNullOrWhiteSpace(novoPedido.IdCliente))
                return "Campo \"Cliente\" é obrigatório";
            if (novoPedido.Produtos.Count == 0)
                return "Adicione produtos";
            if (novoPedido.Receber.Count == 0)
                return "Adicione vencimentos";
            if (tbTotalRestante.Text != "0,00")
                return "Valor total dos vencimentos está diferente do total dos produtos";
            return "";
        }
    }
}