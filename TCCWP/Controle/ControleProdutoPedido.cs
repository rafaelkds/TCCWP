using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCCWP
{
    class ControleProdutoPedido : Controle<ProdutoPedido>
    {
        public List<ProdutoPedido> buscar(string busca)
        {
            throw new NotImplementedException();
        }

        public void gravar(ProdutoPedido objeto)
        {
            if (string.IsNullOrWhiteSpace(objeto.Id)) objeto.Id = BancoDeDados.GetIdProdutoPedido();
            objeto.QuantidadeEntregue = 0;
            string values = "("
                + "$$" + objeto.Id + "$$,"
                + "$$" + objeto.IdProduto + "$$,"
                + "$$" + objeto.IdPedido + "$$,"
                + "$$" + objeto.Quantidade + "$$,"
                + "$$" + objeto.QuantidadeEntregue + "$$,"
                + "$$" + objeto.Valor + "$$)";
            
            string sql = "insert into Produto_pedido "
                + "(Id, Id_produto, Id_pedido, Quantidade, Quantidade_entregue, Valor) "
                + "values " + values;
            Log log = new Log();
            log.Sql = sql;
            BancoDeDados.Insert(objeto, log);
        }

        public void gravarLista(List<ProdutoPedido> lista)
        {
            string values = "";
            foreach (ProdutoPedido produtopedido in lista)
            {
                if (string.IsNullOrWhiteSpace(produtopedido.Id)) produtopedido.Id = BancoDeDados.GetIdProdutoPedido();
                produtopedido.QuantidadeEntregue = 0;
                if (values.Length > 0) values += ", ";
                values += "("
                    + "$$" + produtopedido.Id + "$$,"
                    + "$$" + produtopedido.IdProduto + "$$,"
                    + "$$" + produtopedido.IdPedido + "$$,"
                    + "$$" + produtopedido.Quantidade + "$$,"
                    + "$$" + produtopedido.QuantidadeEntregue + "$$,"
                    + "$$" + produtopedido.Valor + "$$)";
            }
            string sql = "insert into Produto_pedido "
                + "(Id, Id_produto, Id_pedido, Quantidade, Quantidade_entregue, Valor) "
                + "values " + values;
            Log log = new Log();
            log.Sql = sql;
            BancoDeDados.InsertList(lista, log);
        }
    }
}
