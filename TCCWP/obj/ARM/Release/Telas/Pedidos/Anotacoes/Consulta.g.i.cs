﻿#pragma checksum "D:\Visual Studio 2013\Projects\TCCWP\TCCWP\Telas\Pedidos\Anotacoes\Consulta.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "10395AC898788414FB15853EFCFFE1B6"
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


namespace TCCWP.Telas.Pedidos.Anotacoes {
    
    
    public partial class Consulta : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Button btNova;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.ListBox listAnotacoes;
        
        internal System.Windows.Controls.Button btVisualizar;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/TCCWP;component/Telas/Pedidos/Anotacoes/Consulta.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.btNova = ((System.Windows.Controls.Button)(this.FindName("btNova")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.listAnotacoes = ((System.Windows.Controls.ListBox)(this.FindName("listAnotacoes")));
            this.btVisualizar = ((System.Windows.Controls.Button)(this.FindName("btVisualizar")));
        }
    }
}

