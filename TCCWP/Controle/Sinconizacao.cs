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
            client.SincronizarAsync(new System.Collections.ObjectModel.ObservableCollection<string>(lista), ultSinc.getUltimaSinc());
            while (!concluiu)
                await Task.Delay(TimeSpan.FromSeconds(5));
        }

        void SincronizarCompleted(object sender, SincronizarCompletedEventArgs e)
        {
            foreach (Log log in atualizacoes)
            {
                BancoDeDados.Delete(log);
            }

            Atualizacao a = e.Result;

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

            Sinc s = new Sinc();
            List<Sinc> ls = BancoDeDados.Query<Sinc>("select * from Sinc");
            if (ls.Count > 0)
                s.UltimaSinc = a.dtAtualizado.Ticks > ls[0].UltimaSinc ? a.dtAtualizado.Ticks : ls[0].UltimaSinc;
            else
                s.UltimaSinc = a.dtAtualizado.Ticks;

            BancoDeDados.UltSinc(s);
            concluiu = true;
        }
    }
}
