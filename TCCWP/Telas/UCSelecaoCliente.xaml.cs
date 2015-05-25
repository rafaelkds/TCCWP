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
    public partial class UCSelecaoCliente : UserControl
    {
        public UCSelecaoCliente()
        {
            InitializeComponent();
            listClientes.ItemsSource = BancoDeDados.ListAllCliente();
        }
    }
}
