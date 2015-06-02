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
            throw new NotImplementedException();
        }
        
        /*public void gravar(Pedido objeto)
        {
            if (string.IsNullOrWhiteSpace(objeto.Id)) objeto.Id = BancoDeDados.GetIdPedido();
            string values = "("
                + "$$" + objeto.Id + "$$,"
                + "$$" + objeto.Numero + "$$,"
                + "$$" + objeto.IdVendedor + "$$,"
                + "$$" + objeto.IdCliente + "$$,"
                + "$$" + objeto.Valor + "$$,"
                + "$$" + objeto.DataEmissao.ToString("dd/M/yyyy") + "$$,"
                + "$$" + objeto.DataPago.ToString("dd/M/yyyy") + "$$,"
                + "$$" + objeto.Observacoes + "$$)";
            
            string sql = "insert into Pedido "
                + "(Id, Numero, Id_vendedor, Id_cliente, Valor, Data_emissao, Data_pago, Observacoes) "
                + "values " + values;
            Log log = new Log();
            log.Sql = sql;
            BancoDeDados.Insert(objeto, log);
        }*/
        public void gravar(Pedido objeto)
        {
            BancoDeDados.BeginTransaction();

            objeto.IdVendedor = 1;
            decimal total = 0;
            foreach (ProdutoPedido item in objeto.Produtos)
            {
                item.Id = BancoDeDados.GetIdProdutoPedido();
                item.IdPedido = objeto.Id;
                total += item.Quantidade * item.Valor;
            }
            objeto.Valor = total;

            foreach (Receber item in objeto.Receber)
            {
                item.Id = BancoDeDados.GetIdReceber();
                item.IdPedido = objeto.Id;
            }

            ControleProdutoPedido cpp = new ControleProdutoPedido();
            cpp.gravarLista(objeto.Produtos);

            ControleReceber cr = new ControleReceber();
            cr.gravarLista(objeto.Receber);


            if (string.IsNullOrWhiteSpace(objeto.Id)) objeto.Id = BancoDeDados.GetIdPedido();
            string values = "("
                + "$$" + objeto.Id + "$$,"
                + "$$" + objeto.Numero + "$$,"
                + "$$" + objeto.IdVendedor + "$$,"
                + "$$" + objeto.IdCliente + "$$,"
                + "$$" + objeto.Valor + "$$,"
                + "$$" + objeto.DataEmissao.ToString("dd/M/yyyy") + "$$,"
                + "$$" + objeto.DataPago.ToString("dd/M/yyyy") + "$$,"
                + "$$" + objeto.Observacoes + "$$)";

            string sql = "insert into Pedido "
                + "(Id, Numero, Id_vendedor, Id_cliente, Valor, Data_emissao, Data_pago, Observacoes) "
                + "values " + values;
            Log log = new Log();
            log.Sql = sql;
            BancoDeDados.Insert(objeto, log);

            BancoDeDados.CommitTransaction();
        }

        public void deletar(Pedido objeto)
        {
            throw new NotImplementedException();
        }

        public void gravarLista(List<Pedido> lista)
        {
            string values = "";
            foreach (Pedido pedido in lista)
            {
                if (string.IsNullOrWhiteSpace(pedido.Id)) pedido.Id = BancoDeDados.GetIdPedido();
                if (values.Length > 0) values += ", ";
                values += "("
                    + "$$" + pedido.Id + "$$,"
                    + "$$" + pedido.Numero + "$$,"
                    + "$$" + pedido.IdVendedor + "$$,"
                    + "$$" + pedido.IdCliente + "$$,"
                    + "$$" + pedido.Valor + "$$,"
                    + "$$" + pedido.DataEmissao.ToString("dd/M/yyyy") + "$$,"
                    + "$$" + pedido.DataPago.ToString("dd/M/yyyy") + "$$,"
                    + "$$" + pedido.Observacoes + "$$)";
            }
            string sql = "insert into Pedido "
                + "(Id, Numero, Id_vendedor, Id_cliente, Valor, Data_emissao, Data_pago, Observacoes) "
                + "values " + values;
            Log log = new Log();
            log.Sql = sql;
            BancoDeDados.InsertList(lista, log);
        }
    }
}
