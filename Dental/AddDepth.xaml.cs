using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Dental
{
    /// <summary>
    /// Interaction logic for AddTreatment.xaml
    /// </summary>
    public partial class AddDepth : Window
    {
        public AddDepth()
        {
            InitializeComponent();
            Date.SelectedDate = DateTime.Today;
        }
        string id_Patient;
        public AddDepth(string id_Patient)
        {
            InitializeComponent();
            Date.SelectedDate = DateTime.Today;
            this.id_Patient = id_Patient;
            Id_Pat.Text = id_Patient;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Price.Text == string.Empty || Id_Pat.Text == string.Empty)
            {
                MessageBox.Show("Fill in the fields!!!");
            }
            else
            {
                Price.Text.Replace('.', ',');
                try
                {
                    DatabaseWorker.InsertDepth(Price.Text, Descr.Text, Id_Pat.Text, Date.Text);
                    DatabaseWorker.InsertTransaction(Price.Text, Descr.Text, Id_Pat.Text, Date.Text);                
                    
                    this.Close();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }
        private void Find(object sender, RoutedEventArgs e)
        {
            try
            {
                Patient p = DatabaseWorker.GetPatient(Name.Text, Surname.Text, FatherName.Text);
                Id_Pat.Text = p.Id;
                Name.Text = p.Name;
                Surname.Text = p.Surname;
                FatherName.Text = p.FatherName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
