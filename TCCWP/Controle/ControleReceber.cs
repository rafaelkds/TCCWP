﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCCWP
{
    class ControleReceber : Controle<Receber>
    {
        public List<Receber> buscar(string busca)
        {
            throw new NotImplementedException();
        }

        public void gravar(Receber objeto)
        {
            if (string.IsNullOrWhiteSpace(objeto.Id)) objeto.Id = BancoDeDados.GetIdReceber();
                string values = "("
                    + "$$" + objeto.Id + "$$,"
                    + "$$" + objeto.IdPedido + "$$,"
                    + "$$" + objeto.Ordem + "$$,"
                    + "$$" + objeto.Valor + "$$,"
                    + "$$" + objeto.Vencimento.ToString("dd/M/yyyy") + "$$,"
                    + "$$" + objeto.Pagamento.ToString("dd/M/yyyy") + "$$)";
            
            string sql = "insert into Receber "
                + "(Id, Id_pedido, Ordem, Valor, Vencimento, Pagamento) "
                + "values " + values;
            Log log = new Log();
            log.Sql = sql;
            BancoDeDados.InsertRIT(objeto, log);
        }

        public void deletar(Receber objeto)
        {
            throw new NotImplementedException();
        }

        public void gravarLista(List<Receber> lista)
        {
            string values = "";
            foreach (Receber receber in lista)
            {
                if (string.IsNullOrWhiteSpace(receber.Id)) receber.Id = BancoDeDados.GetIdReceber();
                if (values.Length > 0) values += ", ";
                values += "("
                    + "$$" + receber.Id + "$$,"
                    + "$$" + receber.IdPedido + "$$,"
                    + "$$" + receber.Ordem + "$$,"
                    + "$$" + receber.Valor + "$$,"
                    + "$$" + receber.Vencimento.ToString("dd/M/yyyy") + "$$,"
                    + "$$" + receber.Pagamento.ToString("dd/M/yyyy") + "$$)";
            }
            string sql = "insert into Receber "
                + "(Id, Id_pedido, Ordem, Valor, Vencimento, Pagamento) "
                + "values " + values;
            Log log = new Log();
            log.Sql = sql;
            BancoDeDados.InsertList(lista, log);
        }
    }
}