using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace TCCWP.Telas.Clientes
{
    public partial class Cadastro : PhoneApplicationPage
    {
        private string id;

        public Cadastro()
        {
            InitializeComponent();
            
        }

        private void carregarCliente()
        {
            ControleCliente cc = new ControleCliente();
            Cliente cliente = cc.buscarPorId(id);
            tbNome.Text = cliente.Nome;
            tbCpf.Text = cliente.Cpf;
            tbTelefone.Text = string.IsNullOrWhiteSpace(cliente.Telefone) ? "" : cliente.Telefone;
            tbEmail.Text = string.IsNullOrWhiteSpace(cliente.Email) ? "" : cliente.Email;
            tbRua.Text = cliente.Rua;
            tbNumero.Text = cliente.Numero;
            tbBairro.Text = cliente.Bairro;
            tbCidade.Text = cliente.Cidade;
            tbUf.Text = cliente.Uf;
            tbCep.Text = string.IsNullOrWhiteSpace(cliente.Cep) ? "" : cliente.Cep;
            tbComplemento.Text = string.IsNullOrWhiteSpace(cliente.Complemento) ? "" : cliente.Complemento;
        }

        private void btGravar_Click(object sender, RoutedEventArgs e)
        {
            string retorno = verificaCampos();
            if(!string.IsNullOrWhiteSpace(retorno))
            {
                MessageBox.Show(retorno);
                return;
            }
            try
            {
                Cliente c = new Cliente();
                c.Id = string.IsNullOrWhiteSpace(id) ? "" : id;
                c.Nome = tbNome.Text;
                c.Cpf = tbCpf.Text;
                c.Telefone = tbTelefone.Text;
                c.Email = tbEmail.Text;

                c.Rua = tbRua.Text;
                c.Numero = tbNumero.Text;
                c.Bairro = tbBairro.Text;
                c.Cidade = tbCidade.Text;
                c.Uf = tbUf.Text;
                c.Cep = tbCep.Text;

                c.Complemento = tbComplemento.Text;

                ControleCliente cc = new ControleCliente();
                cc.gravar(c);
                MessageBox.Show("Cliente gravado");

                limpar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                tbNome.Text = tbCpf.Text = tbTelefone.Text = tbEmail.Text = tbRua.Text = tbNumero.Text = tbBairro.Text = tbCidade.Text = tbUf.Text = tbCep.Text = tbComplemento.Text = "";
                id = "";
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (NavigationContext.QueryString.TryGetValue("id", out id))
                carregarCliente();
        }

        private string verificaCampos()
        {
            if (string.IsNullOrWhiteSpace(tbNome.Text))
                return "Campo \"Nome\" é obrigatório";
            if (string.IsNullOrWhiteSpace(tbCpf.Text))
                return "Campo \"CPF\" é obrigatório";
            if (string.IsNullOrWhiteSpace(tbRua.Text))
                return "Campo \"Rua\" é obrigatório";
            if (string.IsNullOrWhiteSpace(tbNumero.Text))
                return "Campo \"Numero\" é obrigatório";
            if (string.IsNullOrWhiteSpace(tbBairro.Text))
                return "Campo \"Bairro\" é obrigatório";
            if (string.IsNullOrWhiteSpace(tbCidade.Text))
                return "Campo \"Cidade\" é obrigatório";
            if (string.IsNullOrWhiteSpace(tbUf.Text) || tbUf.Text.Length < 2)
                return "Campo \"UF\" é obrigatório";
            if (string.IsNullOrWhiteSpace(tbCep.Text))
                return "Campo \"CEP\" é obrigatório";
            return "";
        }

        private void limpar()
        {
            id = "";
            tbNome.Text = "";
            tbCpf.Text = "";
            tbTelefone.Text = "";
            tbEmail.Text = "";
            tbRua.Text = "";
            tbNumero.Text = "";
            tbBairro.Text = "";
            tbCidade.Text = "";
            tbUf.Text = "";
            tbCep.Text = "";
            tbComplemento.Text = "";
        }

        private void tbNome_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            tbNome.Text = Util.Texto.removerAcentuacao(tbNome.Text).ToUpper();
            tbNome.SelectionStart = tbNome.Text.Length;
        }

        private void tbEmail_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            tbEmail.Text = Util.Texto.removerAcentuacao(tbEmail.Text).ToLower();
            tbEmail.SelectionStart = tbEmail.Text.Length;
        }

        private void tbRua_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            tbRua.Text = Util.Texto.removerAcentuacao(tbRua.Text).ToUpper();
            tbRua.SelectionStart = tbRua.Text.Length;
        }

        private void tbNumero_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            tbNumero.Text = Util.Texto.removerAcentuacao(tbNumero.Text).ToUpper();
            tbNumero.SelectionStart = tbNumero.Text.Length;
        }

        private void tbBairro_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            tbBairro.Text = Util.Texto.removerAcentuacao(tbBairro.Text).ToUpper();
            tbBairro.SelectionStart = tbBairro.Text.Length;
        }

        private void tbCidade_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            tbCidade.Text = Util.Texto.removerAcentuacao(tbCidade.Text).ToUpper();
            tbCidade.SelectionStart = tbCidade.Text.Length;
        }

        private void tbUf_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            tbUf.Text = Util.Texto.removerAcentuacao(tbUf.Text).ToUpper();
            tbUf.SelectionStart = tbUf.Text.Length;
        }
    }
}