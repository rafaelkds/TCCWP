﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCCWP
{
    class ControleCliente : Controle<Cliente>
    {
        public List<Cliente> buscar(string busca)
        {
            string query = string.Format("select * from Cliente where Nome like '{0}%' or Cpf like '{1}%'", busca, busca);
            List<Cliente> lista = BancoDeDados.Query<Cliente>(query);
            return lista;
        }

        public Cliente buscarPorId(string id)
        {
            string query = string.Format("select * from Cliente where Id = '{0}'", id);
            List<Cliente> lista = BancoDeDados.Query<Cliente>(query);
            return lista[0];
        }

        public void gravar(Cliente objeto)
        {
            if (string.IsNullOrWhiteSpace(objeto.Id))
            {
                objeto.Id = BancoDeDados.GetIdCliente();

                string values = "("
                    + "$$" + objeto.Id + "$$,"
                    + "$$" + objeto.Nome + "$$,"
                    + "$$" + objeto.Cpf + "$$,"
                    + "$$" + objeto.Rua + "$$,"
                    + "$$" + objeto.Numero + "$$,"
                    + "$$" + objeto.Bairro + "$$,"
                    + "$$" + objeto.Cidade + "$$,"
                    + "$$" + objeto.Cep + "$$,"
                    + "$$" + objeto.Complemento + "$$,"
                    + "$$" + objeto.Telefone + "$$,"
                    + "$$" + objeto.Email + "$$)";

                string sql = "insert into Cliente "
                    + "(Id, Nome, Cpf, Rua, Numero, Bairro, Cidade, Cep, Complemento, Telefone, Email) "
                    + "values " + values;
                Log log = new Log();
                log.Sql = sql;

                BancoDeDados.Insert(objeto, log);
            }
            else
            {
                string sql = "update Cliente set "
                    + "Nome = $$" + objeto.Nome + "$$,"
                    + "Cpf = $$" + objeto.Cpf + "$$,"
                    + "Rua = $$" + objeto.Rua + "$$,"
                    + "Numero = $$" + objeto.Numero + "$$,"
                    + "Bairro = $$" + objeto.Bairro + "$$,"
                    + "Cidade = $$" + objeto.Cidade + "$$,"
                    + "Cep = $$" + objeto.Cep + "$$,"
                    + "Complemento = $$" + objeto.Complemento + "$$,"
                    + "Telefone = $$" + objeto.Telefone + "$$,"
                    + "Email = $$" + objeto.Email + "$$"
                    + " where Id = $$" + objeto.Id + "$$";

                Log log = new Log();
                log.Sql = sql;

                BancoDeDados.Update(objeto, log);
            }
        }

        public void deletar(Cliente objeto)
        {
            throw new NotImplementedException();
        }

        public void gravarLista(List<Cliente> lista)
        {
            string values = "";
            foreach (Cliente cliente in lista)
            {
                if (string.IsNullOrWhiteSpace(cliente.Id)) cliente.Id = BancoDeDados.GetIdCliente();
                if (values.Length > 0) values += ", ";
                values += "("
                    + "$$" + cliente.Id + "$$,"
                    + "$$" + cliente.Nome + "$$,"
                    + "$$" + cliente.Cpf + "$$,"
                    + "$$" + cliente.Rua + "$$,"
                    + "$$" + cliente.Numero + "$$,"
                    + "$$" + cliente.Bairro + "$$,"
                    + "$$" + cliente.Cidade + "$$,"
                    + "$$" + cliente.Cep + "$$,"
                    + "$$" + cliente.Complemento + "$$,"
                    + "$$" + cliente.Telefone + "$$,"
                    + "$$" + cliente.Email + "$$)";
            }
            string sql = "insert into Cliente "
                + "(Id, Nome, Cpf, Rua, Numero, Bairro, Cidade, Cep, Complemento, Telefone, Email) "
                + "values " + values;
            Log log = new Log();
            log.Sql = sql;
            BancoDeDados.InsertList(lista, log);
        }
    }
}