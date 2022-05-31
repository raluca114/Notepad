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
    /// Interaction logic for saveMessageWindow.xaml
    /// </summary>
    public partial class saveMessageWindow : Window
    {
        private bool quit=false;
        public saveMessageWindow()
        {
            InitializeComponent();
        }

        private void yesResponse(object sender, RoutedEventArgs e)
        {
            this.Close();
            quit = true;
        }
        private void saveAsResponse(object sender, RoutedEventArgs e)
        {
            this.Close();
            quit = false;
        }
        public bool getQuitResponse()
        {
            return quit;
        }
    }
}
