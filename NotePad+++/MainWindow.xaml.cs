using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Runtime.CompilerServices;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using NotePad___;
using NotePad___.Resources;
using System.Windows.Media;

namespace Notepad___
{
    public partial class MainWindow : Window
    {
        private int tabIndex = 1;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnNewFile_Click(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = new TabItem();
            TextBlock txtFile = new TextBlock();
            txtFile.Text = "*New Document " + tabIndex + " ";
          
            tabIndex++;

            TextBox textBox = new TextBox();
            tabItem.Content = textBox;
            textBox.AcceptsTab = true;
            textBox.AcceptsReturn = true;

            string path= System.IO.Directory.GetCurrentDirectory();
        
            Image img = new Image();
            img.Source = new BitmapImage(new Uri(path + "/closeButton.ico"));
            StackPanel stackPnl = new StackPanel();
            stackPnl.Orientation = Orientation.Horizontal;
            stackPnl.Children.Add(img);

            Button tabCloseButton = new Button();
            tabCloseButton.Content = stackPnl;

            DockPanel grid = new DockPanel();
         
            grid.Children.Add(txtFile);
            grid.Children.Add(tabCloseButton);
            
            tabItem.Header = grid ;

            tabControl1.Items.Add(tabItem);
        }
       
        private void btnSaveFile_Click(object sender, RoutedEventArgs e)
        {
            if (tabControl1.Items.Count > 0)
            {
                try
                {
                    TabItem tabItem = (TabItem)tabControl1.SelectedItem;
                    Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();

                    saveFileDialog.DefaultExt = "txt";
                    saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

                    var data = (tabItem.Content as System.Windows.Controls.TextBox).Text;
                    if (saveFileDialog.ShowDialog() == true)
                        File.WriteAllText(saveFileDialog.FileName, data);
                }
                catch { }
            }
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            TabItem tabItem1= (TabItem)tabControl1.SelectedItem;
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            try
            {
                TabItem tabItem = (TabItem)tabControl1.SelectedItem;
                var data = (tabItem.Content as TextBox).Text;
                File.WriteAllText(saveFileDialog.FileName, data);
            }
            catch { }
        }
        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = new TabItem();
            System.Windows.Controls.TextBox textBox = new System.Windows.Controls.TextBox();
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                textBox.Text = File.ReadAllText(openFileDialog.FileName);
                tabItem.Header = Path.GetFileName(openFileDialog.FileName);
                tabItem.Content = textBox;
                tabControl1.Items.Add(tabItem);
            }
        }
        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            NotePad___.aboutWindow about = new aboutWindow();
            about.ShowDialog();
        }
        private void bntExit_Click(object sender, RoutedEventArgs e)
        {
            exitWindow exit=new exitWindow();
            exit.ShowDialog();
            if(exit.getStatus()==true)
            {
                this.Close();
            }
        }
        private void closeTab_Click(object sender, RoutedEventArgs e)
        {
            saveMessageWindow save=new saveMessageWindow();
            TabItem tabItem = (TabItem)tabControl1.SelectedItem;
            save.ShowDialog();
            if (save.getQuitResponse() == true)
            {
                tabControl1.Items.Remove((TabItem)tabControl1.SelectedItem);
                save.Close();
            } 
            if(save.getQuitResponse()==false)   
            {
                save.Close();
                btnSaveFile_Click(sender, e);
            }
        }
        private void ButtonDirectoryList_Click(object sender, RoutedEventArgs e)
        {
            treeViewDir.Visibility = Visibility.Visible;
            ListDirectory(treeViewDir, "C:\\Users\\Raluca\\Desktop");     
        }
        private void openDirectory_Click(object sender, RoutedEventArgs e)
        {
            openDir(treeViewDir);
        }
        private void ListDirectory(TreeView treeView, string path)
        {
            treeView.Items.Clear();
            var rootDirectiryInfo = new DirectoryInfo(path);
            treeView.Items.Add(CreateDirectoryNode(rootDirectiryInfo));
        }
        private static TreeViewItem CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
           
            TreeViewItem view= new TreeViewItem();
            view.Header=directoryInfo.Name;

            foreach (var directory in directoryInfo.GetDirectories())
            {
                view.Items.Add(CreateDirectoryNode(directory));
            }

            foreach (var file in directoryInfo.GetFiles())
            {
               TreeViewItem item= new TreeViewItem();
                item.Header = file.Name;
                view.Items.Add(item);

            }
            return view;
        }
        public void openDir(TreeView treeView)
        {
            String path = "C:\\Users\\Raluca\\Desktop";
            String viewItemName = treeView.SelectedItem.ToString().Replace("System.Windows.Controls.TreeViewItem Header:", String.Empty);
            viewItemName=viewItemName.Replace("Items.Count:",String.Empty);
            String aux=viewItemName.Remove(viewItemName.Length - 1,1);
            MessageBox.Show(path +"\\"+aux);
            System.Diagnostics.Process.Start(path + "\\" + aux);
        }

        private void showCalendar(object sender, RoutedEventArgs e)
        {
            NotePad___.Resources.calendarWindow calendar =new calendarWindow();
            calendar.ShowDialog();
        }

        private void darkModeWindow(object sender, RoutedEventArgs e)
        {
            tabControl1.Background = new SolidColorBrush(Colors.Black);
            toolBar.Background = new SolidColorBrush(Colors.Black);
            myDockPanel.Background= new SolidColorBrush(Colors.Black);
            TabItem tabItem = (TabItem)tabControl1.SelectedItem;
        }
    }
}
