using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCCWP.ServiceReference1;

namespace TCCWP
{
    class Sinconizacao
    {
        private bool concluiu;
        private List<Log> atualizacoes;
        public async void Sincronizar()
        {
            concluiu = false;
            List<Sinc> ls = BancoDeDados.Query<Sinc>("select * from Sinc");
            Sinc ultSinc = ls.Count > 0 ? ls[0] : new Sinc();
            atualizacoes = BancoDeDados.Query<Log>("select * from Log order by Id");
            List<string> lista = new List<string>();
            foreach(Log log in atualizacoes)
            {
                lista.Add(log.Sql);
            }

            

            Service1Client client = new Service1Client();
            client.SincronizarCompleted += SincronizarCompleted;
            //client.SincronizarAsync(new System.Collections.ObjectModel.ObservableCollection<string>(lista), ultSinc.getUltimaSinc().AddDays(-5));
            client.SincronizarAsync(new System.Collections.ObjectModel.ObservableCollection<string>(lista), ultSinc.getUltimaSinc());
            
            while (!concluiu)
            {
                await Task.Delay(TimeSpan.FromSeconds(5));
            }
        }

        void SincronizarCompleted(object sender, SincronizarCompletedEventArgs e)
        {
            Atualizacao a = e.Result;
            BancoDeDados.BeginTransaction();
            BancoDeDados.DeleteList<Log>(atualizacoes);

            #region Cliente
            List<Cliente> clientes = new List<Cliente>(a.clientes.Count);
            foreach (ClienteWS item in a.clientes)
            {
                clientes.Add(new Cliente()
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Cpf = item.Cpf,
                    Rua = item.Rua,
                    Numero = item.Numero,
                    Bairro = item.Bairro,
                    Cidade = item.Cidade,
                    Cep = item.Cep,
                    Complemento = item.Complemento,
                    Telefone = item.Telefone,
                    Email = item.Email
                });
            }

            BancoDeDados.Atualiza<Cliente>(clientes);
            #endregion

            #region Produto
            List<Produto> produtos = new List<Produto>(a.produtos.Count);
            foreach (ProdutoWS item in a.produtos)
            {
                produtos.Add(new Produto()
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Estoque = item.Estoque
                });
            }

            BancoDeDados.Atualiza<Produto>(produtos);
            #endregion

            #region Pedido
            List<Pedido> pedidos = new List<Pedido>(a.pedidos.Count);
            foreach (PedidoWS item in a.pedidos)
            {
                pedidos.Add(new Pedido()
                {
                    Id = item.Id,
                    Numero = item.Numero,
                    IdCliente = item.IdCliente,
                    IdVendedor = item.IdVendedor,
                    Valor = item.Valor,
                    DataEmissao = item.DataEmissao,
                    DataPago = item.DataPago,
                    Observacoes = item.Observacoes
                });
            }

            BancoDeDados.Atualiza<Pedido>(pedidos);
            #endregion

            #region Produtos Pedido
            List<ProdutoPedido> produtospedido = new List<ProdutoPedido>(a.produtospedido.Count);
            foreach (ProdutoPedidoWS item in a.produtospedido)
            {
                produtospedido.Add(new ProdutoPedido()
                {
                    Id = item.Id,
                    IdPedido = item.IdPedido,
                    IdProduto = item.IdProduto,
                    Valor = item.Valor,
                    Quantidade = item.Quantidade
                });
            }

            BancoDeDados.Atualiza<ProdutoPedido>(produtospedido);
            #endregion

            #region Receber
            List<Receber> receber = new List<Receber>(a.receber.Count);
            foreach (ReceberWS item in a.receber)
            {
                receber.Add(new Receber()
                {
                    Id = item.Id,
                    IdPedido = item.IdPedido,
                    Ordem = item.Ordem,
                    Valor = item.Valor,
                    Vencimento = item.Vencimento,
                    Pagamento = item.Pagamento
                });
            }

            BancoDeDados.Atualiza<Receber>(receber);
            #endregion

            Sinc s = new Sinc();
            List<Sinc> ls = BancoDeDados.Query<Sinc>("select * from Sinc");
            if (ls.Count > 0)
                s.UltimaSinc = a.dtAtualizado.Ticks > ls[0].UltimaSinc ? a.dtAtualizado.Ticks : ls[0].UltimaSinc;
            else
                s.UltimaSinc = a.dtAtualizado.Ticks;

            BancoDeDados.UltSinc(s);
            BancoDeDados.CommitTransaction();
            concluiu = true;
            System.Windows.MessageBox.Show("Sincronizado");
        }
    }
}
