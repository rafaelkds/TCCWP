using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace TCCWP.Telas.Pedidos.Anotacoes
{
    public partial class Consulta : PhoneApplicationPage
    {
        private string idPedido;

        public Consulta()
        {
            InitializeComponent();
        }

        private void btNova_Click(object sender, RoutedEventArgs e)
        {
            UCTexto uct = new UCTexto();
            CustomMessageBox cmb = new CustomMessageBox()
            {
                Content = uct,
                LeftButtonContent = "Gravar",
                RightButtonContent = "Cancelar"
            };
            cmb.Dismissing += (s1, e1) =>
            {
                switch (e1.Result)
                {
                    case CustomMessageBoxResult.LeftButton:
                        try
                        {
                            if (!string.IsNullOrWhiteSpace(uct.tbTexto.Text))
                            {
                                Anotacao anotacao = new Anotacao();
                                anotacao.IdPedido = idPedido;
                                anotacao.Data = anotacao.DataUltimaAlteracao = DateTime.Now;
                                anotacao.Texto = uct.tbTexto.Text;

                                ControleAnotacao ca = new ControleAnotacao();
                                ca.gravar(anotacao);
                                listAnotacoes.ItemsSource = null;
                                listAnotacoes.ItemsSource = ca.buscar(idPedido);
                            }
                        }
                        catch(Exception ex)
                        {
                            e1.Cancel = true;
                            MessageBox.Show(ex.Message);
                        }
                        break;
                }
            };
            cmb.Show();
        }

        private void btVisualizar_Click(object sender, RoutedEventArgs e)
        {
            Anotacao anotacao = listAnotacoes.SelectedItem as Anotacao;
            UCTexto uct = new UCTexto();
            uct.tbTexto.Text = anotacao.Texto;
            CustomMessageBox cmb = new CustomMessageBox()
            {
                Content = uct,
                LeftButtonContent = "Gravar",
                RightButtonContent = "Cancelar"
            };
            cmb.Dismissing += (s1, e1) =>
            {
                switch (e1.Result)
                {
                    case CustomMessageBoxResult.LeftButton:
                        try
                        {
                            if (!string.IsNullOrWhiteSpace(uct.tbTexto.Text))
                            {
                                anotacao.DataUltimaAlteracao = DateTime.Now;
                                anotacao.Texto = uct.tbTexto.Text;

                                ControleAnotacao ca = new ControleAnotacao();
                                ca.gravar(anotacao);
                                listAnotacoes.ItemsSource = null;
                                listAnotacoes.ItemsSource = ca.buscar(idPedido);
                            }
                        }
                        catch (Exception ex)
                        {
                            e1.Cancel = true;
                            MessageBox.Show(ex.Message);
                        }
                        break;
                }
            };
            cmb.Show();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            NavigationContext.QueryString.TryGetValue("id", out idPedido);
            ControleAnotacao ca = new ControleAnotacao();
            listAnotacoes.ItemsSource = ca.buscar(idPedido);
        }
    }
}