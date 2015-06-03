﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCCWP
{
    class ControleAnotacao : Controle<Anotacao>
    {
        public List<Anotacao> buscar(string busca)
        {
            string query = string.Format("select * from Anotacao where IdPedido = '{0}'", busca);
            List<Anotacao> lista = BancoDeDados.Query<Anotacao>(query);
            return lista;
        }

        public void gravar(Anotacao objeto)
        {
            BancoDeDados.BeginTransaction();

            if (string.IsNullOrWhiteSpace(objeto.Id))
            {
                objeto.Id = BancoDeDados.GetIdAnotacao();
                string values = "("
                    + "$$" + objeto.Id + "$$,"
                    + "$$" + objeto.IdPedido + "$$,"
                    + "$$" + objeto.Data.ToString("dd/M/yyyy") + "$$,"
                    + "$$" + objeto.DataUltimaAlteracao.ToString("dd/M/yyyy") + "$$,"
                    + "$$" + objeto.Texto + "$$)";

                string sql = "insert into Anotacao "
                    + "(Id, Id_pedido, Data, Data_ultima_alteracao, Texto) "
                    + "values " + values;
                Log log = new Log();
                log.Sql = sql;
                BancoDeDados.Insert(objeto, log);
            }
            else
            {
                string sql = "update Anotacao set "
                    + "Data_ultima_alteracao = $$" + objeto.DataUltimaAlteracao.ToString("dd/M/yyyy") + "$$,"
                    + "Texto = $$" + objeto.Texto + "$$"
                    + " where Id = $$" + objeto.Id + "$$";
                Log log = new Log();
                log.Sql = sql;
                BancoDeDados.Update(objeto, log);
            }

            BancoDeDados.CommitTransaction();
        }
    }
}
