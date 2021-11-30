﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FTL_Captain_s_Log
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            if (!File.Exists(Directory.GetCurrentDirectory() + "\\settings.txt")) // if the settings file doesn't exist, run first-time setup
            {
                MessageBox.Show("Please select your FTL.dat (within the FTL folder).", "FTL: Captain's Log");
                OpenFileDialog openFileDialog = new OpenFileDialog()
                {
                    Filter = "Data files (*.dat)|*.dat|All(*.*)|*"
                };
                if (openFileDialog.ShowDialog() == true)
                {
                    File.WriteAllLines(Directory.GetCurrentDirectory() + "\\settings.txt", new string[] { "ftlDatLocation=\"" + openFileDialog.FileName + "\"" });
                    File.WriteAllLines(Directory.GetCurrentDirectory() + "\\unpacker\\modman.cfg", new string[] { "ftl_dats_path=" + openFileDialog.FileName.Replace("\\", "\\\\").Replace("\\ftl.dat", "")});

                    LoadingScreen load = new LoadingScreen();
                    load.Show();
                    ProcessStartInfo start = new ProcessStartInfo();
                    start.Arguments = "--extract-dats .\\unpackedFiles";
                    start.FileName = ".\\unpacker\\modman.exe";
                    start.WindowStyle = ProcessWindowStyle.Hidden;
                    start.CreateNoWindow = true;
                    using (Process proc = Process.Start(start))
                    {
                        proc.WaitForExit();
                        Database.Parse();
                        load.Close();
                    }
                }
                else
                {
                    System.Windows.Application.Current.Shutdown();
                }
            }
            InitializeComponent();
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Options_Click(object sender, RoutedEventArgs e) // options button
        {
            LoadingScreen load = new LoadingScreen();
            this.Hide();
            load.Show();
            ProcessStartInfo start = new ProcessStartInfo();
            start.Arguments = "--extract-dats .\\unpackedFiles";
            start.FileName = ".\\unpacker\\modman.exe";
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;
            using (Process proc = Process.Start(start))
            {
                proc.WaitForExit();
                Database.Parse();
                load.Close();
                this.Show();
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
