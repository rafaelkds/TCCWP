using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCCWP.TCCWS;

namespace TCCWP.Controle
{
    class Sincronizacao
    {
        private bool concluiu;
        private bool erro;
        
        public async void Sincronizar(MainPage pagina)
        {
            concluiu = false;
            List<Sinc> ls = BancoDeDados.Query<Sinc>("select * from Sinc");
            Sinc ultSinc = ls.Count > 0 ? ls[0] : new Sinc();
            List<Log> atualizacoes = BancoDeDados.Query<Log>("select * from Log order by Id");
            List<string> lista = new List<string>();
            foreach(Log log in atualizacoes)
            {
                lista.Add(log.Sql);
            }

            TCCWSClient client = new TCCWSClient();
            client.SincronizarCompleted += SincronizarCompleted;
            client.SincronizarAsync(lista, ultSinc.getUltimaSinc(), Windows.Phone.System.Analytics.HostInformation.PublisherHostId);

            while (!concluiu)
                await Task.Delay(TimeSpan.FromSeconds(1));
            
            pagina.mensagemSincronizacao(erro ? "Erro na sincronização" : "Sincronizado com sucesso");
        }

        void SincronizarCompleted(object sender, SincronizarCompletedEventArgs e)
        {
            try
            {
                if (e.Result != null)
                {
                    Atualizacao atualizacao = e.Result;
                    BancoDeDados.BeginTransaction();
                    BancoDeDados.DeleteAll<Log>();

                    #region Cliente
                    List<Cliente> clientes = new List<Cliente>(atualizacao.clientes.Count);
                    foreach (ClienteWS item in atualizacao.clientes)
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
                    List<Produto> produtos = new List<Produto>(atualizacao.produtos.Count);
                    foreach (ProdutoWS item in atualizacao.produtos)
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
                    List<Pedido> pedidos = new List<Pedido>(atualizacao.pedidos.Count);
                    foreach (PedidoWS item in atualizacao.pedidos)
                    {
                        pedidos.Add(new Pedido()
                        {
                            Id = item.Id,
//                            Numero = item.Numero,
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
                    List<ProdutoPedido> produtospedido = new List<ProdutoPedido>(atualizacao.produtospedido.Count);
                    foreach (ProdutoPedidoWS item in atualizacao.produtospedido)
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
                    List<Receber> receber = new List<Receber>(atualizacao.receber.Count);
                    foreach (ReceberWS item in atualizacao.receber)
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
                    List<Anotacao> anotacoes = new List<Anotacao>(atualizacao.anotacoes.Count);
                    foreach (AnotacaoWS item in atualizacao.anotacoes)
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
                    List<Vendedor> vendedores = new List<Vendedor>(atualizacao.vendedores.Count);
                    foreach (VendedorWS item in atualizacao.vendedores)
                    {
                        vendedores.Add(new Vendedor()
                        {
                            Id = item.Id,
                            Nome = item.Nome
                        });
                    }

                    BancoDeDados.Atualiza<Vendedor>(vendedores);
                    #endregion

                    if (atualizacao.maxIdAnotacao != null || atualizacao.maxIdCliente != null || atualizacao.maxIdPedido != null || atualizacao.maxIdProdutoPedido != null || atualizacao.maxIdReceber != null)
                    {
                        Id id = new Id()
                        {
                            Anotacao = atualizacao.maxIdAnotacao ?? 0,
                            Cliente = atualizacao.maxIdCliente ?? 0,
                            Pedido = atualizacao.maxIdPedido ?? 0,
                            ProdutoPedido = atualizacao.maxIdProdutoPedido ?? 0,
                            Receber = atualizacao.maxIdReceber ?? 0
                        };
                        BancoDeDados.DeleteAll<Id>();
                        BancoDeDados.Insert<Id>(id);
                    }

                    Sinc sinc = new Sinc();
                    List<Sinc> ls = BancoDeDados.Query<Sinc>("select * from Sinc");
                    if (ls.Count > 0)
                    {
                        sinc.UltimaSinc = atualizacao.dtAtualizado.Ticks > ls[0].UltimaSinc ? atualizacao.dtAtualizado.Ticks : ls[0].UltimaSinc;
                    }
                    else
                    {
                        sinc.UltimaSinc = atualizacao.dtAtualizado.Ticks;
                    }
                    sinc.IdCelular = atualizacao.idCelular;

                    BancoDeDados.DeleteAll<Sinc>();
                    BancoDeDados.Insert<Sinc>(sinc);
                    BancoDeDados.CommitTransaction();
                    erro = false;
                }
                else
                { erro = true; }
            }
            catch (Exception)
            {
                erro = true;
                BancoDeDados.RollbackTransaction();
            }
            concluiu = true;
        }
    }
}
