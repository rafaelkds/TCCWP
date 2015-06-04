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
        ResourceIntensiveTask resourceIntensiveTask;
        string resourceIntensiveTaskName = "ResourceIntensiveAgent";
        public bool agentsAreEnabled = true;

        public long UltimaSinc { get; set; }
        private Sinconizacao sinc = new Sinconizacao();
        // Constructor
        public MainPage()
        {
            InitializeComponent();

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


        private void StartResourceIntensiveAgent()
        {
            // Variable for tracking enabled status of background agents for this app.
            agentsAreEnabled = true;

            resourceIntensiveTask = ScheduledActionService.Find(resourceIntensiveTaskName) as ResourceIntensiveTask;

            // If the task already exists and background agents are enabled for the
            // application, you must remove the task and then add it again to update 
            // the schedule.
            if (resourceIntensiveTask != null)
            {
                ScheduledActionService.Remove(resourceIntensiveTaskName);
            }

            resourceIntensiveTask = new ResourceIntensiveTask(resourceIntensiveTaskName);

            // The description is required for periodic agents. This is the string that the user
            // will see in the background services Settings page on the device.
            resourceIntensiveTask.Description = "Atualização automática do TCCWP.";

            // Place the call to Add in a try block in case the user has disabled agents.
            try
            {
                ScheduledActionService.Add(resourceIntensiveTask);

                // If debugging is enabled, use LaunchForTest to launch the agent in one minute.
/*#if(DEBUG_AGENT)
    ScheduledActionService.LaunchForTest(resourceIntensiveTaskName, TimeSpan.FromSeconds(60));
#endif*/
            }
            catch (InvalidOperationException exception)
            {
                if (exception.Message.Contains("BNS Error: The action is disabled"))
                {
                    agentsAreEnabled = false;

                }
            }
            catch (SchedulerServiceException)
            {
                // No user action required.
            }


        }


    }
}