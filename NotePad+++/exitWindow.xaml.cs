using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace NotePad___
{
    /// <summary>
    /// Interaction logic for exitWindow.xaml
    /// </summary>
    public partial class exitWindow : Window
    {
        private bool exit = false;
        public exitWindow()
        {
            InitializeComponent();
        }

        private void exitYes(object sender, RoutedEventArgs e)
        {
            this.Close();
            exit = true;
        }

        private void exitNo(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public bool getStatus()
        {
            return exit;
        }
    }
}
