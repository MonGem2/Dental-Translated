using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
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

namespace Dental
{
    /// <summary>
    /// Interaction logic for Depth.xaml
    /// </summary>
    public partial class PatientDepth : Page
    {
        string id_patient;
        public PatientDepth(string id_pat)
        {
            InitializeComponent();
            ContextMenu contextMenu = new ContextMenu();
            MenuItem menuItem = new MenuItem();
            menuItem.Header = "Pay the debt off";
            menuItem.Click += Pay_the_debt_off;
            contextMenu.Items.Add(menuItem);
            View.ContextMenu = contextMenu;

            id_patient = id_pat;

            contextMenu = new ContextMenu();
            menuItem = new MenuItem();
            menuItem.Header = "Accept prepayment";
            menuItem.Click += Accept_prepayment;
            contextMenu.Items.Add(menuItem);
            View1.ContextMenu = contextMenu;
        }

        private void Page_Loaded(object sender, object e)
        {
            DataTable dt = DatabaseWorker.SelectDepthbyId(id_patient).Tables[0];
            dt.Columns["Id"].ColumnName = "ID";
            dt.Columns["id_Patient"].ColumnName = "Patient_ID";
            dt.Columns["Description"].ColumnName = "Description";
            dt.Columns["Date"].ColumnName = "Date";
            dt.Columns["Suma"].ColumnName = "Amount";
            View.ItemsSource = dt.DefaultView;
            dt = DatabaseWorker.SelectPeredbyId(id_patient).Tables[0];
            dt.Columns["Id"].ColumnName = "ID";
            dt.Columns["id_Patient"].ColumnName = "Patient_ID";
            dt.Columns["Description"].ColumnName = "Description";
            dt.Columns["Date"].ColumnName = "Date";
            dt.Columns["Suma"].ColumnName = "Amount";
            View1.ItemsSource = dt.DefaultView;
        }

        private void Button_Click(object sender, object e)
        {
            MainWindow.Pager.Items.Remove(MainWindow.tb2);
        }

        private void AddDepth(object sender, object e)
        {
            try
            {
                (new AddDepth(id_patient)).ShowDialog();
            }
            catch { }
            finally
            {
                DataTable dt = DatabaseWorker.SelectDepth().Tables[0];
                dt.Columns["Id"].ColumnName = "ID";
                dt.Columns["id_Patient"].ColumnName = "Patient_ID";
                dt.Columns["Description"].ColumnName = "Description";
                dt.Columns["Date"].ColumnName = "Date";
                dt.Columns["Suma"].ColumnName = "Amount";
                View.ItemsSource = dt.DefaultView;
            }
        }

        private void Pay_the_debt_off(object sender, object e)
        {

            string path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + @"\Base\Denta.db";
            if (View.SelectedItems.Count!=0)
            {
                //MessageBoxResult dialogResult = MessageBox.Show("Вы хотите полностью погасить этот долг?", "Подтверждение", MessageBoxButton.YesNoCancel);
                //if (dialogResult == MessageBoxResult.Yes)
                //{
                //    DataRowView row = (DataRowView)View.SelectedItems[0];
                //    DatabaseWorker.InsertTransaction(row["Suma"].ToString(), row["Description"].ToString(), row["id_Patient"].ToString(), row["Date"].ToString(), "Погашение долга");
                //    DatabaseWorker.DeleteDepth(row["id"].ToString());
                //    View.ItemsSource = DatabaseWorker.SelectDepth().Tables[0].DefaultView;
                //   
                //}
               // if (dialogResult == MessageBoxResult.No)
               // {
                    SQLiteConnection _con = new SQLiteConnection("Data Source=" + path + ";Version=3;");
                    try {
                        DataRowView row = (DataRowView)View.SelectedItems[0];
                        (new Depther(double.Parse(row["Amount"].ToString()),row["ID"].ToString(),int.Parse(row["Patient_ID"].ToString()))).ShowDialog();
                        }
                    catch { }
                    finally
                    {
                        try
                    {
                        DataTable dt = DatabaseWorker.SelectDepth().Tables[0];
                        dt.Columns["Id"].ColumnName = "ID";
                        dt.Columns["id_Patient"].ColumnName = "Patient_ID";
                        dt.Columns["Description"].ColumnName = "Description";
                        dt.Columns["Date"].ColumnName = "Date";
                        dt.Columns["Suma"].ColumnName = "Amount";
                        View.ItemsSource = dt.DefaultView;
                        dt = DatabaseWorker.SelectPered().Tables[0];
                        dt.Columns["Id"].ColumnName = "ID";
                        dt.Columns["id_Patient"].ColumnName = "Patient_ID";
                        dt.Columns["Description"].ColumnName = "Description";
                        dt.Columns["Date"].ColumnName = "Date";
                        dt.Columns["Suma"].ColumnName = "Amount";
                        View1.ItemsSource = dt.DefaultView;
                    }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    }
                //}
            }
        }

        private void Add_Pered(object sender, object e)
        {
            try
            {
                (new AddPered(id_patient)).ShowDialog();
            }
            catch { }
            finally
            {
                try
                {
                    DataTable dt = DatabaseWorker.SelectPered().Tables[0];
                    dt.Columns["Id"].ColumnName = "ID";
                    dt.Columns["id_Patient"].ColumnName = "Patient_ID";
                    dt.Columns["Description"].ColumnName = "Description";
                    dt.Columns["Date"].ColumnName = "Date";
                    dt.Columns["Suma"].ColumnName = "Amount";
                    View1.ItemsSource = dt.DefaultView;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Accept_prepayment(object sender, object e)
        {
            //DatabaseWorker.InsertTransaction("100", "dsfsdf", "1", "asfds", "sfdsdjkfsnk");
            if (View1.SelectedItems.Count != 0)
            {
                MessageBoxResult dialogResult = MessageBox.Show("Are you sure you want to accept this prepayment?", "Confirm", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    try
                    {
                        DataRowView row = (DataRowView)View1.SelectedItems[0];
                        
                        try
                        {
                            DatabaseWorker.InsertTransaction(row["Amount"].ToString(), row["Description"].ToString(), row["Patient_ID"].ToString(), row["Date"].ToString(), "Acceptance of prepayment");
                            DatabaseWorker.DeletePered(row["ID"].ToString());                            
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        finally
                        {
                            try
                            {
                                DataTable dt = DatabaseWorker.SelectPered().Tables[0];
                                dt.Columns["Id"].ColumnName = "ID";
                                dt.Columns["id_Patient"].ColumnName = "Patient_ID";
                                dt.Columns["Description"].ColumnName = "Description";
                                dt.Columns["Date"].ColumnName = "Date";
                                dt.Columns["Suma"].ColumnName = "Amount";
                                View1.ItemsSource = dt.DefaultView;
                             
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }

                        }
                    }
                    catch { }
                }
            }
        }

    }
}
