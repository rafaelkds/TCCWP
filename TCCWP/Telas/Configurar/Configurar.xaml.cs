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
using Microsoft.Phone.Scheduler;

namespace TCCWP.Telas.Configurar
{
    public partial class Configurar : PhoneApplicationPage
    {
        private PeriodicTask periodicTask;
        private string periodicTaskName = "TCCWPAgent";
        public Configurar()
        {
            InitializeComponent();
            periodicTask = ScheduledActionService.Find(periodicTaskName) as PeriodicTask;
            PeriodicStackPanel.DataContext = periodicTask;
        }

        private void PeriodicCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            StartPeriodicAgent();
        }
        private void PeriodicCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            RemoveAgent();
        }

        private void StartPeriodicAgent()
        {
            periodicTask = ScheduledActionService.Find(periodicTaskName) as PeriodicTask;

            if (periodicTask != null)
            {
                RemoveAgent();
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

        private void RemoveAgent()
        {
            try
            {
                ScheduledActionService.Remove(periodicTaskName);
            }
            catch (Exception)
            {
            }
        }
    }
}