using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCCWP.TCCWS;

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

            TCCWSClient client = new TCCWSClient();
            client.SincronizarCompleted += SincronizarCompleted;
            //client.SincronizarAsync(new System.Collections.ObjectModel.ObservableCollection<string>(lista), ultSinc.getUltimaSinc().AddDays(-5));
            client.SincronizarAsync(new System.Collections.ObjectModel.ObservableCollection<string>(lista), ultSinc.getUltimaSinc(), Windows.Phone.System.Analytics.HostInformation.PublisherHostId);
            
            while (!concluiu)
            {
                await Task.Delay(TimeSpan.FromSeconds(5));
            }
        }

        void SincronizarCompleted(object sender, SincronizarCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                Atualizacao a = e.Result;
                BancoDeDados.BeginTransaction();
                BancoDeDados.DeleteAll<Log>();

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
                        Uf = item.Uf,
                        Cep = item.Cep,
                        Complemento = item.Complemento,
                        Telefone = item.Telefone,
                        Email = item.Email,
                        Ativo = item.Ativo
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
                        Estoque = item.Estoque,
                        Valor = item.Valor,
                        Ativo = item.Ativo
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
                        DataPagamento = item.DataPagamento,
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
                        Quantidade = item.Quantidade,
                        QuantidadeEntregue = item.QuantidadeEntregue
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

                #region Anotacao
                List<Anotacao> anotacoes = new List<Anotacao>(a.anotacoes.Count);
                foreach (AnotacaoWS item in a.anotacoes)
                {
                    anotacoes.Add(new Anotacao()
                    {
                        Id = item.Id,
                        IdPedido = item.IdPedido,
                        Data = item.Data,
                        DataUltimaAlteracao = item.DataUltimaAlteracao,
                        Texto = item.Texto
                    });
                }

                BancoDeDados.Atualiza<Anotacao>(anotacoes);
                #endregion

                #region Vendedor
                List<Vendedor> vendedores = new List<Vendedor>(a.vendedores.Count);
                foreach (VendedorWS item in a.vendedores)
                {
                    vendedores.Add(new Vendedor()
                    {
                        Id = item.Id,
                        Nome = item.Nome
                    });
                }

                BancoDeDados.Atualiza<Vendedor>(vendedores);
                #endregion

                if (a.maxIdAnotacao != null || a.maxIdCliente != null || a.maxIdPedido != null || a.maxIdProdutoPedido != null || a.maxIdReceber != null)
                {
                    Id id = new Id()
                    {
                        Anotacao = a.maxIdAnotacao ?? 0,
                        Cliente = a.maxIdCliente ?? 0,
                        Pedido = a.maxIdPedido ?? 0,
                        ProdutoPedido = a.maxIdProdutoPedido ?? 0,
                        Receber = a.maxIdReceber ?? 0
                    };
                    BancoDeDados.DeleteAll<Id>();
                    BancoDeDados.Insert<Id>(id);
                }

                Sinc s = new Sinc();
                List<Sinc> ls = BancoDeDados.Query<Sinc>("select * from Sinc");
                if (ls.Count > 0)
                    s.UltimaSinc = a.dtAtualizado.Ticks > ls[0].UltimaSinc ? a.dtAtualizado.Ticks : ls[0].UltimaSinc;
                else
                    s.UltimaSinc = a.dtAtualizado.Ticks;
                s.IdCelular = a.idCelular;

                BancoDeDados.UltSinc(s);
                BancoDeDados.CommitTransaction();
                concluiu = true;
                System.Windows.MessageBox.Show("Sincronizado");
            }
        }
    }
}
