#define DEBUG_AGENT
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
using Microsoft.Phone.Scheduler;

namespace TCCWP
{
    public partial class MainPage : PhoneApplicationPage
    {
        PeriodicTask periodicTask;

        string periodicTaskName = "PeriodicAgent";


        public long UltimaSinc { get; set; }
        private Sinconizacao sinc = new Sinconizacao();
        // Constructor
        public MainPage()
        {
            InitializeComponent();
           // StartPeriodicAgent();
            BancoDeDados.teste();
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            sinc.Sincronizar();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Telas/Produtos.xaml", UriKind.Relative));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Telas/Pedidos/Menu.xaml", UriKind.Relative));
        }

        private void btClientes_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Telas/Clientes/Menu.xaml", UriKind.Relative));
        }





        
        private void StartPeriodicAgent()
        {
            periodicTask = ScheduledActionService.Find(periodicTaskName) as PeriodicTask;

            if (periodicTask != null)
            {
                RemoveAgent(periodicTaskName);
            }

            periodicTask = new PeriodicTask(periodicTaskName);

            periodicTask.Description = "This demonstrates a periodic task.";

            try
            {
                ScheduledActionService.Add(periodicTask);

#if(DEBUG_AGENT)
    ScheduledActionService.LaunchForTest(periodicTaskName, TimeSpan.FromSeconds(60));
#endif
            }
            catch (InvalidOperationException exception)
            {
                if (exception.Message.Contains("BNS Error: The action is disabled"))
                {
                    MessageBox.Show("Background agents for this application have been disabled by the user.");
                }

                if (exception.Message.Contains("BNS Error: The maximum number of ScheduledActions of this type have already been added."))
                {
                    // No user action required. The system prompts the user when the hard limit of periodic tasks has been reached.
                }
            }
            catch (SchedulerServiceException)
            {
                // No user action required.
            }
        }

        private void RemoveAgent(string name)
        {
            try
            {
                ScheduledActionService.Remove(name);
            }
            catch (Exception)
            {
            }
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
            col.Add(c);

            p.criar(tit, col);
        }
        
    }
}