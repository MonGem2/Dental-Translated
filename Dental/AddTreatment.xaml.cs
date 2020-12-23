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
    public partial class AddTreatment : Window
    {
        public AddTreatment()
        {
            InitializeComponent();
            Date.SelectedDate = DateTime.Today;
        }
        string id_Patient;
        public AddTreatment(string id_Patient)
        {
            InitializeComponent();
            Date.Text = DateTime.Today.ToShortDateString();
            this.id_Patient = id_Patient;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Descr.Text == string.Empty || Date.Text == string.Empty)
            {
                MessageBox.Show("Fill in the fields!!!");
            }
            else
            {
                Price.Text.Replace('.', ',');
                DatabaseWorker.InsertTreatment(Price.Text, Descr.Text, id_Patient, Date.Text);
                DatabaseWorker.InsertDepth(Price.Text, Descr.Text, id_Patient, Date.Text);
                DatabaseWorker.InsertTransaction(Price.Text, Descr.Text, id_Patient, Date.Text);
                this.Close();
            }
        }
    }
}
