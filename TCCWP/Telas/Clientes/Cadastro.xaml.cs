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

        private void carregarPedido()
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
            tbCidade.Text = cliente.Cidade.ToString();
            tbCep.Text = string.IsNullOrWhiteSpace(cliente.Cep) ? "" : cliente.Cep;
            tbComplemento.Text = string.IsNullOrWhiteSpace(cliente.Complemento) ? "" : cliente.Complemento;
        }

        private void btGravar_Click(object sender, RoutedEventArgs e)
        {
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
                c.Cidade = Convert.ToInt32(tbCidade.Text);
                c.Cep = tbCep.Text;

                c.Complemento = tbComplemento.Text;

                ControleCliente cc = new ControleCliente();
                cc.gravar(c);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                tbNome.Text = tbCpf.Text = tbTelefone.Text = tbEmail.Text = tbRua.Text = tbNumero.Text = tbBairro.Text = tbCidade.Text = tbCep.Text = tbComplemento.Text = "";
                id = "";
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (NavigationContext.QueryString.TryGetValue("id", out id))
                carregarPedido();
        }
    }
}