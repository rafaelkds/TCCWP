#define DEBUG_AGENT
using System.Diagnostics;
using System.Windows;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;

namespace TCCWPTaskAgent
{
    public class ScheduledAgent : ScheduledTaskAgent
    {
        /// <remarks>
        /// ScheduledAgent constructor, initializes the UnhandledException handler
        /// </remarks>
        static ScheduledAgent()
        {
            // Subscribe to the managed exception handler
            Deployment.Current.Dispatcher.BeginInvoke(delegate
            {
                Application.Current.UnhandledException += UnhandledException;
            });
        }

        /// Code to execute on Unhandled Exceptions
        private static void UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                Debugger.Break();
            }
        }

        /// <summary>
        /// Agent that runs a scheduled task
        /// </summary>
        /// <param name="task">
        /// The invoked task
        /// </param>
        /// <remarks>
        /// This method is called when a periodic or resource intensive task is invoked
        /// </remarks>
        protected override void OnInvoke(ScheduledTask task)
        {

            Sincronizacao.Sincronizacao sinc = new Sincronizacao.Sincronizacao();
            sinc.Sincronizar();
            while (sinc.concluiu == false) { }
            

            ShellToast toast = new ShellToast();
            toast.Title = "TCCWP";
            toast.Content = "Sincronizou";
            toast.Show();
            
#if DEBUG_AGENT
  ScheduledActionService.LaunchForTest(task.Name, System.TimeSpan.FromSeconds(60));
#endif

            
            NotifyComplete();
        }


    }
}