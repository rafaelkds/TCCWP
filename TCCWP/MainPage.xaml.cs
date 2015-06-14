using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TCCWP.Resources;
using SQLite;

namespace TCCWP
{
    public partial class MainPage : PhoneApplicationPage
    {
        
        public MainPage()
        {
            InitializeComponent();
            BancoDeDados.teste();
        }

        private void btClientes_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Telas/Clientes/Menu.xaml", UriKind.Relative));
        }





        
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Pdf.Pdf p = new Pdf.Pdf();
            List<Produto> prod = BancoDeDados.Query<Produto>("Select * from Produto where Ativo = 1");
            List<string> tit = new List<string>();
            tit.Add("Produto");
            tit.Add("Quantidade");
            tit.Add("Preco");
            tit.Add("Preco");
            tit.Add("Preco");
            tit.Add("Preco");

            List<List<string>> col = new List<List<string>>(2);
            List<string> a = new List<string>(prod.Count);
            List<string> b = new List<string>(prod.Count);
            List<string> c = new List<string>(prod.Count);
            foreach(Produto pr in prod)
            {
                a.Add(pr.Nome);
                b.Add(pr.Estoque.ToString());
                c.Add(pr.Valor.ToString());
            }
            col.Add(a);
            col.Add(b);
            col.Add(c);
            col.Add(c); col.Add(c); col.Add(c); 

            p.criar("Estoque de Produtos", tit, col);
        }

        private void btRelatorios_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Telas/Relatorios/Menu.xaml", UriKind.Relative));
        }

        private void btEstoque_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Telas/Estoque/Consulta.xaml", UriKind.Relative));
        }

        private void btPedidos_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Telas/Pedidos/Menu.xaml", UriKind.Relative));
        }

        private void btConfigurar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Telas/Configurar/Configurar.xaml", UriKind.Relative));
        }

        private void btSincronizar_Click(object sender, RoutedEventArgs e)
        {
            this.IsEnabled = false;
            Controle.Sincronizacao sinc = new Controle.Sincronizacao();
            sinc.Sincronizar(this);
        }

        public void mensagemSincronizacao(string mensagem)
        {
            this.IsEnabled = true;
            MessageBox.Show(mensagem);
        }
    }
}