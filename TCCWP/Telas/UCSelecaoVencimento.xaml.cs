using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace TCCWP
{
    public partial class UCSelecaoVencimento : UserControl
    {
        public CustomMessageBox Cmb { get; set; }
        public UCSelecaoVencimento()
        {
            InitializeComponent();
            /*dpData.ManipulationCompleted += (s1, e2) =>
            {
                Cmb.Show();
            };*/
            dpData.ValueChanged += (s1, e2) =>
            {
                ((CustomMessageBox)this.Parent).Show();
            };
            
        }
    }
}
