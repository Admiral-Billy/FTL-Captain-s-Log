using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using System.IO;
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
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : UserControl
    {
        public Menu()
        {
            InitializeComponent();
            menu.Visibility = Visibility.Hidden;
            options.Visibility = Visibility.Hidden;
        }


        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            if (menu.Visibility == Visibility.Visible)
            {
                menu.Visibility = Visibility.Hidden;
            }
            else if (menu.Visibility == Visibility.Hidden)
            {
                if (options.Visibility == Visibility.Visible)
                {
                    options.Visibility = Visibility.Hidden;
                }
                menu.Visibility = Visibility.Visible;
            }
        }

        private void Options_Click(object sender, RoutedEventArgs e)
        {
            if (options.Visibility == Visibility.Visible)
            {
                options.Visibility = Visibility.Hidden;
            }
            else if (options.Visibility == Visibility.Hidden)
            {
                if (menu.Visibility == Visibility.Visible)
                {
                    menu.Visibility = Visibility.Hidden;
                }
                options.Visibility = Visibility.Visible;
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Rescan_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Please select your FTL.dat (within the FTL folder).", "FTL: Captain's Log");
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "Data files (*.dat)|*.dat|All(*.*)|*"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                File.WriteAllLines(Directory.GetCurrentDirectory() + "\\settings.txt", new string[] { "ftlDatLocation=\"" + openFileDialog.FileName + "\"" });
                File.WriteAllLines(Directory.GetCurrentDirectory() + "\\unpacker\\modman.cfg", new string[] { "ftl_dats_path=" + openFileDialog.FileName.Replace("\\", "\\\\").Replace("\\ftl.dat", "") });

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
        }
    }

}
