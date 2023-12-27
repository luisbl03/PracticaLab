﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

namespace PracticaLab
{
    public partial class Gestor : Window
    {
        public Usuario usuario { get; set; }
        private void Gestor_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            
            // Ajuste dinamico parte superior
            double nh_gridSup = e.NewSize.Height * 0.15;
            //gridPrincipal.RowDefinitions[0].Height = new GridLength(nh_gridSup);
            //Ajuste dinámico el ancho y alto del perfil
            double nuevoAncho = e.NewSize.Width * 0.32;
            double nuevaAltura = e.NewSize.Height * 0.14;

            perfilRectangle.Width = nuevoAncho;
            perfilRectangle.Height = nuevaAltura;

        }
       

       
        public Gestor(Usuario u)
        {
            InitializeComponent();
            usuario = u;

            //Usamos el usuariio para poner sus datos
            this.DataContext = usuario;
            mainFrame.Navigate(new Page2());

        }

        private void txtCodigoPostaly_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private Page1 paginaPersona;
        private void Button_Click_Personal(object sender, RoutedEventArgs e)
        {
            if (!(mainFrame.Content is Page1) && paginaPersona == null)
            {
                // inicializa una pagina de personal
                paginaPersona = new Page1();
                mainFrame.Navigate(paginaPersona);
            }
            else if (!(mainFrame.Content is Page1) && !(paginaPersona == null))
            {
                mainFrame.Navigate(paginaPersona);
            }
        }

        private void Button_Click_Pacientes(object sender, RoutedEventArgs e)
        {
            if (mainFrame.Content is Page1)
            {
                // Regresa a la pagina de pacientes
                mainFrame.GoBack();
            }
            
        }

    }
}