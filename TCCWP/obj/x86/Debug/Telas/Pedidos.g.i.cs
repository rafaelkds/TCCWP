﻿#pragma checksum "D:\Visual Studio 2013\Projects\TCCWP\TCCWP\Telas\Pedidos.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "19611C94A63D257721C49955EE2C15D8"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace TCCWP {
    
    
    public partial class Pedidos : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.TextBox tbNumero;
        
        internal Microsoft.Phone.Controls.DatePicker dpEmissao;
        
        internal System.Windows.Controls.TextBlock tbCliente;
        
        internal Microsoft.Phone.Controls.LongListSelector listProdutos;
        
        internal System.Windows.Controls.TextBlock tbTotalQuantidade;
        
        internal System.Windows.Controls.TextBlock tbTotalValor;
        
        internal Microsoft.Phone.Controls.DatePicker dpData;
        
        internal System.Windows.Controls.TextBox tbValor;
        
        internal Microsoft.Phone.Controls.LongListSelector listVencimentos;
        
        internal System.Windows.Controls.TextBlock tbTotalReceber;
        
        internal System.Windows.Controls.TextBlock tbTotalRestante;
        
        internal System.Windows.Controls.TextBox tbObservacoes;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/TCCWP;component/Telas/Pedidos.xaml", System.UriKind.Relative));
            this.tbNumero = ((System.Windows.Controls.TextBox)(this.FindName("tbNumero")));
            this.dpEmissao = ((Microsoft.Phone.Controls.DatePicker)(this.FindName("dpEmissao")));
            this.tbCliente = ((System.Windows.Controls.TextBlock)(this.FindName("tbCliente")));
            this.listProdutos = ((Microsoft.Phone.Controls.LongListSelector)(this.FindName("listProdutos")));
            this.tbTotalQuantidade = ((System.Windows.Controls.TextBlock)(this.FindName("tbTotalQuantidade")));
            this.tbTotalValor = ((System.Windows.Controls.TextBlock)(this.FindName("tbTotalValor")));
            this.dpData = ((Microsoft.Phone.Controls.DatePicker)(this.FindName("dpData")));
            this.tbValor = ((System.Windows.Controls.TextBox)(this.FindName("tbValor")));
            this.listVencimentos = ((Microsoft.Phone.Controls.LongListSelector)(this.FindName("listVencimentos")));
            this.tbTotalReceber = ((System.Windows.Controls.TextBlock)(this.FindName("tbTotalReceber")));
            this.tbTotalRestante = ((System.Windows.Controls.TextBlock)(this.FindName("tbTotalRestante")));
            this.tbObservacoes = ((System.Windows.Controls.TextBox)(this.FindName("tbObservacoes")));
        }
    }
}

