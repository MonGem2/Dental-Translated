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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dental
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static TabControl Pager = new TabControl() { Name="Pager", TabStripPlacement = Dock.Bottom, Margin = new Thickness(0,0,0,3) };
        public static TabItem tb = new TabItem() { Name = "Patients", Header = "Patients", Content = new Frame() { Content = new Patients() } };
        public static TabItem tb1 = new TabItem() { Name = "NewCard", Header = "New card", Content = new Frame() { Content = new New_Card() } };
        public static TabItem tb2 = new TabItem() { Name = "Depth", Header = "Debts/Prepayments", Content = new Frame() { Content = new Depth() } };
        public static TabItem tb3 = new TabItem() { Name = "Transactions", Header = "Transactions", Content = new Frame() { Content = new Transactions() } };
        public MainWindow()
        {
            InitializeComponent();
            grid.Children.Add(Pager);
            Grid.SetColumn(Pager, 1);
            this.Closing += MainWindow_Closing;
            //DatabaseWorker.InsertTransaction("100", "dsdad", "1", "addsas", "asdadsk");
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DatabaseWorker.Close();
        
        }

        private void Patients_Click(object sender, RoutedEventArgs e)
        {
            if (!Pager.Items.Contains(tb))
            {
                Pager.Items.Add((tb));
            }
            Pager.SelectedItem = tb;
        }
        private void AddCard_Click(object sender, RoutedEventArgs e)
        {
            if (!Pager.Items.Contains(tb1))
            {
                Pager.Items.Add((tb1));
            }
            Pager.SelectedItem = tb1;
        }
        private void Depth_Click(object sender, RoutedEventArgs e)
        {
            if (!Pager.Items.Contains(tb2))
            {
                Pager.Items.Add((tb2));
            }
            Pager.SelectedItem = tb2;
        }
        private void Transaction_Click(object sender, RoutedEventArgs e)
        {
            if (!Pager.Items.Contains(tb3))
            {
                Pager.Items.Add((tb3));
            }
            Pager.SelectedItem = tb3;
        }
    }
}
