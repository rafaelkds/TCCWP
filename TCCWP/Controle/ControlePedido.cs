using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCCWP
{
    class ControlePedido : Controle<Pedido>
    {
        public List<Pedido> buscar(string busca)
        {
            string query = string.Format("select Pedido.* from Pedido, Cliente where Pedido.IdCliente = Cliente.Id and (Pedido.Numero like '{0}%' or Cliente.Nome like '{1}%' or Cliente.Cpf like '{2}%')", busca, busca, busca);
            List<Pedido> lista = BancoDeDados.Query<Pedido>(query);
            ControleCliente cc = new ControleCliente();
            foreach (Pedido pedido in lista)
                pedido.Cliente = cc.buscarPorId(pedido.IdCliente);
            return lista;
        }

        public Pedido buscarPorId(string id)
        {
            string query = string.Format("select * from Pedido where Id = '{0}'", id);
            List<Pedido> lista = BancoDeDados.Query<Pedido>(query);
            Pedido pedido = lista[0];

            query = string.Format("select * from ProdutoPedido where IdPedido = '{0}'", id);
            List<ProdutoPedido> listaProdutos = BancoDeDados.Query<ProdutoPedido>(query);
            pedido.Produtos = listaProdutos;

            query = string.Format("select * from Receber where IdPedido = '{0}'", id);
            List<Receber> listaReceber = BancoDeDados.Query<Receber>(query);
            pedido.Receber = listaReceber;

            return pedido;
        }
        
        public void gravar(Pedido objeto)
        {
            BancoDeDados.BeginTransaction();

            if (string.IsNullOrWhiteSpace(objeto.Id)) objeto.Id = BancoDeDados.GetIdPedido();

            decimal total = 0;
            foreach (ProdutoPedido item in objeto.Produtos)
            {
                item.IdPedido = objeto.Id;
                total += item.Quantidade * item.Valor;
            }
            objeto.Valor = total;

            foreach (Receber item in objeto.Receber)
            {
                item.IdPedido = objeto.Id;
            }

            string values = "("
                + "$$" + objeto.Id + "$$,"
                + "$$" + objeto.Numero + "$$,"
                + "$$" + objeto.IdVendedor + "$$,"
                + "$$" + objeto.IdCliente + "$$,"
                + "$$" + objeto.Valor + "$$,"
                + "$$" + objeto.DataEmissao.ToString("dd/MM/yyyy") + "$$,"
                + "$$" + objeto.DataPagamento.ToString("dd/MM/yyyy") + "$$,"
                + "$$" + objeto.Observacoes + "$$)";

            string sql = "insert into Pedido "
                + "(Id, Numero, Id_vendedor, Id_cliente, Valor, Data_emissao, Data_pagamento, Observacoes) "
                + "values " + values;
            Log log = new Log();
            log.Sql = sql;
            BancoDeDados.Insert(objeto, log);
            

            ControleProdutoPedido cpp = new ControleProdutoPedido();
            cpp.gravarLista(objeto.Produtos);

            ControleReceber cr = new ControleReceber();
            cr.gravarLista(objeto.Receber);

            BancoDeDados.CommitTransaction();
        }
        
    }
}
