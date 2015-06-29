using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace TCCWP.Telas.Relatorios
{
    public partial class Menu : PhoneApplicationPage
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void btReceber_Click(object sender, RoutedEventArgs e)
        {
            Pdf.Pdf pdf = new Pdf.Pdf();
            List<Receber> rece = BancoDeDados.Query<Receber>("Select Receber.* from Receber, Pedido, Cliente "
                                                           + "where Receber.IdPedido = Pedido.Id and Pedido.IdCliente = Cliente.Id and Receber.Pagamento = '0001-01-01 00:00:00'  "
                                                           + "order by Cliente.nome, Pedido.Id, Receber.Ordem");
            List<string> tit = new List<string>();
            tit.Add("Nome");
            tit.Add("Numero");
            tit.Add("Emissao");
            tit.Add("Vencimento");
            tit.Add("Valor");

            List<List<string>> col = new List<List<string>>(5);
            List<string> nomes = new List<string>(rece.Count);
            List<string> numeros = new List<string>(rece.Count);
            List<string> emissoes = new List<string>(rece.Count);
            List<string> vencimentos = new List<string>(rece.Count);
            List<string> valores = new List<string>(rece.Count);
            ControleCliente cc = new ControleCliente();
            ControlePedido cp = new ControlePedido();
            Pedido ped = null;
            string nomeCliente = "";
            foreach (Receber re in rece)
            {
                if(ped == null || re.IdPedido != ped.Id)
                {
                    ped = cp.buscarPorId(re.IdPedido);
                    nomeCliente = cc.buscarPorId(ped.IdCliente).Nome;
                }
                nomes.Add(nomeCliente);
                numeros.Add(re.IdPedido + "-" + re.Ordem);
                emissoes.Add(ped.DataEmissaoFormatado);
                vencimentos.Add(re.VencimentoFormatado);
                valores.Add(re.ValorFormatado);
            }
            col.Add(nomes);
            col.Add(numeros);
            col.Add(emissoes);
            col.Add(vencimentos);
            col.Add(valores);

            pdf.criar("Contas a Receber", tit, col);
        }

        private void btEstoque_Click(object sender, RoutedEventArgs e)
        {
            Pdf.Pdf pdf = new Pdf.Pdf();
            List<Produto> prod = BancoDeDados.Query<Produto>("Select * from Produto where Ativo = 1 order by nome");
            List<string> tit = new List<string>();
            tit.Add("Produto");
            tit.Add("Quantidade");

            List<List<string>> col = new List<List<string>>(2);
            List<string> nomes = new List<string>(prod.Count);
            List<string> quantidades = new List<string>(prod.Count);
            foreach (Produto pr in prod)
            {
                nomes.Add(pr.Nome);
                quantidades.Add(pr.EstoqueFormatado.ToString());
            }
            col.Add(nomes);
            col.Add(quantidades);

            pdf.criar("Estoque", tit, col);
        }

        private void btPrecos_Click(object sender, RoutedEventArgs e)
        {
            Pdf.Pdf pdf = new Pdf.Pdf();
            List<Produto> prod = BancoDeDados.Query<Produto>("Select * from Produto where Ativo = 1 order by nome");
            List<string> tit = new List<string>();
            tit.Add("Produto");
            tit.Add("Preco");

            List<List<string>> col = new List<List<string>>(2);
            List<string> nomes = new List<string>(prod.Count);
            List<string> valores = new List<string>(prod.Count);
            foreach (Produto pr in prod)
            {
                nomes.Add(pr.Nome);
                valores.Add(pr.ValorFormatado.ToString());
            }
            col.Add(nomes);
            col.Add(valores);

            pdf.criar("Tabela de precos", tit, col);
        }

        private void btEntrega_Click(object sender, RoutedEventArgs e)
        {
            Pdf.Pdf pdf = new Pdf.Pdf();
            List<ProdutoPedido> proped = BancoDeDados.Query<ProdutoPedido>("Select ProdutoPedido.* from ProdutoPedido, Pedido, Cliente, Produto "
                                                           + "where ProdutoPedido.IdPedido = Pedido.Id and Pedido.IdCliente = Cliente.Id and ProdutoPedido.IdProduto = Produto.Id "
                                                           + "and ProdutoPedido.Quantidade > ProdutoPedido.QuantidadeEntregue "
                                                           + "order by Cliente.nome, Pedido.Id, Produto.Nome");
            List<string> tit = new List<string>();
            tit.Add("Cliente");
            tit.Add("Numero");
            tit.Add("Emissao");
            tit.Add("Produto");
            tit.Add("Quantidade");
            tit.Add("Entregue");

            List<List<string>> col = new List<List<string>>(6);
            List<string> clientes = new List<string>(proped.Count);
            List<string> numeros = new List<string>(proped.Count);
            List<string> emissoes = new List<string>(proped.Count);
            List<string> produtos = new List<string>(proped.Count);
            List<string> quantidades = new List<string>(proped.Count);
            List<string> entregues = new List<string>(proped.Count);
            ControleCliente cc = new ControleCliente();
            ControlePedido cp = new ControlePedido();
            ControleProduto cpr = new ControleProduto();
            Pedido ped = null;
            string nomeCliente = "";
            foreach (ProdutoPedido pp in proped)
            {
                if (ped == null || pp.IdPedido != ped.Id)
                {
                    ped = cp.buscarPorId(pp.IdPedido);
                    nomeCliente = cc.buscarPorId(ped.IdCliente).Nome;
                }
                clientes.Add(nomeCliente);
                numeros.Add(pp.IdPedido);
                emissoes.Add(ped.DataEmissaoFormatado);
                produtos.Add(cpr.buscarPorId(pp.IdProduto.ToString()).Nome);
                quantidades.Add(pp.QuantidadeFormatada);
                entregues.Add(pp.QuantidadeEntregueFormatada);
            }
            col.Add(clientes);
            col.Add(numeros);
            col.Add(emissoes);
            col.Add(produtos);
            col.Add(quantidades);
            col.Add(entregues);

            pdf.criar("Situacao de Entrega", tit, col);
        }
    }
}