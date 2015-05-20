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
        private Sinconizacao sinc = new Sinconizacao();
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            
            
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Clientes.xaml", UriKind.Relative));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            sinc.Sincronizar();
            /*List<Log> atualizacoes = BancoDeDados.Query<Log>("select * from Log order by Id");
            List<string> lista = new List<string>();
            foreach (Log log in atualizacoes)
            {
                lista.Add(log.Sql);
            }

            TCCWP.ServiceReference1.Service1Client client = new TCCWP.ServiceReference1.Service1Client();
            client.SincronizarCompleted += SincronizarCompleted;
            client.SincronizarAsync(new System.Collections.ObjectModel.ObservableCollection<string>(lista), DateTime.Now.AddYears(-5));*/
        }
        /*
        void SincronizarCompleted(object sender, TCCWP.ServiceReference1.SincronizarCompletedEventArgs e)
        {
            System.Windows.MessageBox.Show("1");
            TCCWP.ServiceReference1.Atualizacao a = e.Result;
            List<Cliente> lista = new List<Cliente>(a.clientes.Count);
            foreach (TCCWP.ServiceReference1.ClienteWS item in a.clientes)
            {
                lista.Add(new Cliente()
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
            BancoDeDados.Atualiza<Cliente>(lista);
            Sinc s = new Sinc();
            s.UltimaSinc = a.dtAtualizado;
            BancoDeDados.UltSinc(s);
            System.Windows.MessageBox.Show("2");
        }*/

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}