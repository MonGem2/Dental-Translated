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
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using System.IO;

namespace Dental
{
    /// <summary>
    /// Interaction logic for Patients.xaml
    /// </summary>
    public partial class Patients : Page
    {
        public Patients()
        {
            InitializeComponent();
            ContextMenu contextMenu = new ContextMenu();
            MenuItem menuItem = new MenuItem();
            menuItem.Header = "Open";
            menuItem.Click +=OpenPatient;
            contextMenu.Items.Add(menuItem);
            menuItem = new MenuItem();
            menuItem.Header = "New treatment";
            menuItem.Click += NewTreatment;
            contextMenu.Items.Add(menuItem);
            menuItem = new MenuItem();
            menuItem.Header = "Delete";
            menuItem.Click += DeletePatient;
            contextMenu.Items.Add(menuItem);
            View.ContextMenu = contextMenu;

        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
            DataTable dt = DatabaseWorker.SelectPatients().Tables[0];
            dt.Columns["Id"].ColumnName = "ID";
            dt.Columns["Name"].ColumnName = "Name";
            dt.Columns["Surname"].ColumnName = "Surname";
            dt.Columns["FatherName"].ColumnName = "Patronymic";
            dt.Columns["Mobile_Phone"].ColumnName = "Phone_1";
            dt.Columns["Work_Phone"].ColumnName = "Phone_2";
            dt.Columns["Home_Phone"].ColumnName = "Phone_3";
            dt.Columns["Date_Birth"].ColumnName = "Date-of-Birth";
            dt.Columns["Gender"].ColumnName = "Gender";
            dt.Columns["Card_Num"].ColumnName = "Card-number";
            dt.Columns["Description"].ColumnName = "Description";
            dt.Columns["Date"].ColumnName = "Date-of-create";
            View.ItemsSource = dt.DefaultView;
            
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Pager.Items.Remove(MainWindow.tb);
        }

        private void DeletePatient(object sender, object e)
        {
            if (View.SelectedItems.Count != 0)
            {
                MessageBoxResult dialogResult = MessageBox.Show("Are you sure you want to delete this patient and everything related to him?", "Confirm", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    DataRowView row = (DataRowView)View.SelectedItems[0];
                    DatabaseWorker.DeletePatient(row["ID"].ToString());
                    View.ItemsSource = DatabaseWorker.SelectPatients().Tables[0].DefaultView;                  
                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
             try
             {
                 
                 View.ItemsSource = DatabaseWorker.FindPatient(textb.Text).DefaultView;
             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.Message);
             }
        }

        private void OpenPatient(object sender, RoutedEventArgs e)
        {   
            try
            {
                TabItem tb = new TabItem() { Header = "Card: " + ((DataRowView)View.SelectedItems[0])["Name"].ToString() + " " + ((DataRowView)View.SelectedItems[0])["Surname"].ToString() + " " + ((DataRowView)View.SelectedItems[0])["Patronymic"].ToString(), Content = new Frame() { Content = new Card(((DataRowView)View.SelectedItems[0])["Name"].ToString(), ((DataRowView)View.SelectedItems[0])["Surname"].ToString(), ((DataRowView)View.SelectedItems[0])["Patronymic"].ToString(), ((DataRowView)View.SelectedItems[0])["ID"].ToString()) } };
                MainWindow.Pager.Items.Add(tb);
                MainWindow.Pager.SelectedItem = tb;
            }
            catch { }
        }

        private void NewTreatment(object sender, RoutedEventArgs e)
        {
            try
            {
                (new AddTreatment(((DataRowView)View.SelectedItems[0])["ID"].ToString())).ShowDialog();
            }
            catch { }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            textb.Text = string.Empty;
        }

        private void View_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                TabItem tb = new TabItem() { Header= "Card: " + ((DataRowView)View.SelectedItems[0])["Name"].ToString() + " " + ((DataRowView)View.SelectedItems[0])["Surname"].ToString() + " " + ((DataRowView)View.SelectedItems[0])["Patronymic"].ToString(), Content = new Frame() { Content = new Card(((DataRowView)View.SelectedItems[0])["Name"].ToString(), ((DataRowView)View.SelectedItems[0])["Surname"].ToString(), ((DataRowView)View.SelectedItems[0])["Patronymic"].ToString(), ((DataRowView)View.SelectedItems[0])["ID"].ToString()) } };
                MainWindow.Pager.Items.Add(tb);
                tb.IsSelected = true;
            }
            catch { }
        }

        private void AddCard_Click(object sender, RoutedEventArgs e)
        { 
            TabItem tb = new TabItem() { Header = "New card", Content = new Frame() { Content = new New_Card() } };
            MainWindow.Pager.Items.Add(tb);
            MainWindow.Pager.SelectedItem = tb;
        }



        private void View_SourceUpdated(object sender, EventArgs e)
        {
            
            try
            {
                DatabaseWorker.UpdatePatient(((DataRowView)View.SelectedItems[0])["Name"].ToString(), ((DataRowView)View.SelectedItems[0])["Surname"].ToString(), ((DataRowView)View.SelectedItems[0])["Patronymic"].ToString(),
                    ((DataRowView)View.SelectedItems[0])["Phone_1"].ToString(), ((DataRowView)View.SelectedItems[0])["Phone_2"].ToString(), ((DataRowView)View.SelectedItems[0])["Phone_3"].ToString(),
                    ((DataRowView)View.SelectedItems[0])["Date-of-Birth"].ToString(), ((DataRowView)View.SelectedItems[0])["Gender"].ToString(), ((DataRowView)View.SelectedItems[0])["Card-number"].ToString(), ((DataRowView)View.SelectedItems[0])["Description"].ToString(),
                    ((DataRowView)View.SelectedItems[0])["Date-of-create"].ToString(), ((DataRowView)View.SelectedItems[0])["ID"].ToString());
                
            }
            catch (Exception ex)
            {
                
            }
        }

        private void View_KeyDown(object sender, KeyEventArgs e)
        {
            //MessageBox.Show(e.Key.ToString());
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.F5)
            {
                try
                {
                    try
                    {
                        DatabaseWorker.UpdatePatient(((DataRowView)View.SelectedItems[0])["Name"].ToString(), ((DataRowView)View.SelectedItems[0])["Surname"].ToString(), ((DataRowView)View.SelectedItems[0])["Patronymic"].ToString(),
                            ((DataRowView)View.SelectedItems[0])["Phone_1"].ToString(), ((DataRowView)View.SelectedItems[0])["Phone_2"].ToString(), ((DataRowView)View.SelectedItems[0])["Phone_3"].ToString(),
                            ((DataRowView)View.SelectedItems[0])["Date-of-Birth"].ToString(), ((DataRowView)View.SelectedItems[0])["Gender"].ToString(), ((DataRowView)View.SelectedItems[0])["Card-number"].ToString(), ((DataRowView)View.SelectedItems[0])["Description"].ToString(),
                            ((DataRowView)View.SelectedItems[0])["Date-of-create"].ToString(), ((DataRowView)View.SelectedItems[0])["ID"].ToString());

                    }
                    catch (Exception ex)
                    {

                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
            else if(e.Key == Key.F5)
            {
                DataTable dt = DatabaseWorker.SelectPatients().Tables[0];
                dt.Columns["Id"].ColumnName = "ID";
                dt.Columns["Name"].ColumnName = "Name";
                dt.Columns["Surname"].ColumnName = "Surname";
                dt.Columns["FatherName"].ColumnName = "Patronymic";
                dt.Columns["Mobile_Phone"].ColumnName = "Phone_1";
                dt.Columns["Work_Phone"].ColumnName = "Phone_2";
                dt.Columns["Home_Phone"].ColumnName = "Phone_3";
                dt.Columns["Date_Birth"].ColumnName = "Date-of-Birth";
                dt.Columns["Gender"].ColumnName = "Gender";
                dt.Columns["Card_Num"].ColumnName = "Card-number";
                dt.Columns["Description"].ColumnName = "Description";
                dt.Columns["Date"].ColumnName = "Date-of-create";
                View.ItemsSource = dt.DefaultView;
            }
        }
    }
}
