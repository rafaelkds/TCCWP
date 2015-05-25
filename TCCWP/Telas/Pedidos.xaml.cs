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
    public partial class Pedidos : PhoneApplicationPage
    {
        private Pedido novoPedido;
        public Pedidos()
        {
            InitializeComponent();
            novoPedido = new Pedido();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UCSelecaoCliente ucsc = new UCSelecaoCliente();
            CustomMessageBox cmb = new CustomMessageBox()
            {
                Content = ucsc,
                //Height = 500,
                //Opacity = 0.7,
                LeftButtonContent = "Selecionar",
                RightButtonContent = "Cancelar"
            };
            cmb.Dismissing += (s1, e1) =>
            {
                switch (e1.Result)
                {
                    case CustomMessageBoxResult.LeftButton:
                        if (ucsc.listClientes.SelectedItem != null)
                        {
                            novoPedido.IdCliente = (ucsc.listClientes.SelectedItem as Cliente).Id;
                            tbCliente.Text = (ucsc.listClientes.SelectedItem as Cliente).Nome;
                        }
                        break;
                }
            };
            cmb.Show();
        }
    }
}