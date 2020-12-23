using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dental
{
    public static class DatabaseWorker
    {
        static string path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + @"\Base\Denta.db";
        static SQLiteConnection con = new SQLiteConnection("Data Source=" + path + ";Version=3;");
        public static void ReduceDepth(string ID, string diference)
        {
            SQLiteCommand cmd = new SQLiteCommand(con);
            cmd.CommandText = $"update Depth set Suma=Suma-{diference} where Id='{ID}'";
            cmd.ExecuteNonQuery();
        }
        static DatabaseWorker()
        {
            con.Open();
        }
        public static DataTable FindTransactions(string pattern)
        {

            string query = "select * from [Transactions]";
            if (pattern != "") // Note: txt_Search is the TextBox..
            {
                query += $" where Description Like '@{pattern}@' or where id_Patient Like '%{pattern}%' or where Suma Like '%{pattern}%' or where Type Like '%{pattern}%'";
            }
            SQLiteCommand _cmd = new SQLiteCommand(query, con);
            _cmd.ExecuteNonQuery();

            SQLiteDataAdapter _adp = new SQLiteDataAdapter(_cmd);
            DataTable _dt = new DataTable();
            _adp.Fill(_dt);
            _adp.Update(_dt);
            return _dt;




        }
        public static DataSet SelectTransactions()
        {
            string text = "Select * From [Transactions]";
            try
            {

                DataSet ds = new DataSet();
                var da = new SQLiteDataAdapter(text, con);
                da.AcceptChangesDuringUpdate = true;
                da.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public static DataSet SelectPatients()
        {
            string text = "Select * From [Patients]";
            try
            {

                DataSet ds = new DataSet();
                var da = new SQLiteDataAdapter(text, con);
                da.AcceptChangesDuringUpdate = true;
                da.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public static DataSet SelectDepth()
        {
            string text = "Select * From [Depth]";
            try
            {

                DataSet ds = new DataSet();
                var da = new SQLiteDataAdapter(text, con);
                da.AcceptChangesDuringUpdate = true;
                da.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public static DataSet SelectDepthbyId(string Id)
        {
            string text = $"Select * From [Depth] where id_patient={Id}";
            try
            {

                DataSet ds = new DataSet();
                var da = new SQLiteDataAdapter(text, con);
                da.AcceptChangesDuringUpdate = true;
                da.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public static DataSet SelectPered()
        {
            string text = "Select * From [Pered]";
            try
            {

                DataSet ds = new DataSet();
                var da = new SQLiteDataAdapter(text, con);
                da.AcceptChangesDuringUpdate = true;
                da.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public static DataSet SelectPeredbyId(string Id)
        {
            string text = $"Select * From [Pered] where id_patient={Id}";
            try
            {

                DataSet ds = new DataSet();
                var da = new SQLiteDataAdapter(text, con);
                da.AcceptChangesDuringUpdate = true;
                da.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public static void DeletePatient(string id)
        {
            try
            {
                
                
                SQLiteCommand _cmd = new SQLiteCommand($"Delete from [Patients] where Id='{id}'", con);
                _cmd.ExecuteNonQuery();
                _cmd.CommandText = $"Delete from [Treatment] where id_Patient='{ id}'";
                _cmd.ExecuteNonQuery();
                _cmd.CommandText = $"Delete from [Depth] where id_Patient='{ id}'";
                _cmd.ExecuteNonQuery();
                _cmd.CommandText = $"Delete from [Transactions] where id_Patient='{ id}'";
                _cmd.ExecuteNonQuery();

                
            }
            catch
            { 
            }

        }
        public static void DeleteDepth(string id)
        {
            try
            {
                SQLiteCommand _cmd = new SQLiteCommand($"Delete from [Patients] where Id='{id}'", con);
                _cmd.CommandText = $"Delete from [Depth] where Id='{ id}'";
                _cmd.ExecuteNonQuery();
            }
            catch
            {
            }

        }
        public static void DeletePered(string id)
        {
            try
            {
                SQLiteCommand _cmd = new SQLiteCommand($"Delete from [Patients] where Id='{id}'", con);
                _cmd.CommandText = $"Delete from [Pered] where Id='{ id}'";
                _cmd.ExecuteNonQuery();
            }
            catch
            {
            }

        }
        public static DataTable FindPatient(string pattern)
        {
            string query = "select * from [Patients]";
            if (pattern != "") // Note: txt_Search is the TextBox..
            {
                query += $" where Description Like '@{pattern}@' or Mobile_Phone Like '%{pattern}%' or Home_Phone Like '%{pattern}%' or  Work_Phone Like '%{pattern}%' or FatherName Like '%{pattern}%' or Surname Like '%{pattern}%' or Name Like '%{pattern}%' or Id Like '%{pattern}%'";
            }
            SQLiteCommand _cmd = new SQLiteCommand(query, con);
            _cmd.ExecuteNonQuery();

            SQLiteDataAdapter _adp = new SQLiteDataAdapter(_cmd);
            DataTable _dt = new DataTable();
            _adp.Fill(_dt);
            _adp.Update(_dt);
            return _dt;
        }
        public static void NewCard(string name, string surname, string fathername,string gender, string mobphone, string homephone, string workphone, string birth, string descr)
        {
            string query = $"insert into [Patients] (Name,Surname,FatherName,Gender,Mobile_Phone,Home_Phone,Work_Phone,Date_Birth,Description,Date) values ('{name}','{surname}','{fathername}','{gender}','{mobphone}','{homephone}','{workphone}','{birth}','{descr}','{DateTime.Today.ToShortDateString()}')";
            SQLiteCommand _cmd = new SQLiteCommand(query, con);
            _cmd.ExecuteNonQuery();
        }
        public static void Close()
        {
            con.Close();
        }
        public static void InsertTransaction(string Price, string Descr, string Id_Pat, string Date, string Type= "Добавлен долг")
        {

            string query1 = $"insert into [Transactions] (Suma,Description,id_Patient,Date,Type) values ('{Price}','{Descr}','{Id_Pat}','{Date}','{Type}')";
            SQLiteCommand _cmd1 = new SQLiteCommand(query1, con);
            _cmd1.ExecuteNonQuery();

        }
        public static void InsertDepth(string Price, string Descr, string Id_Pat, string Date)
        {

            string query = $"insert into [Depth] (Suma,Description,id_Patient,Date) values ('{Price}','{Descr}','{Id_Pat}','{Date}')";
            SQLiteCommand _cmd = new SQLiteCommand(query, con);
            _cmd.ExecuteNonQuery();

        }
        public static void InsertPered(string Price, string Descr, string Id_Pat, string Date)
        {

            string query = $"insert into [Pered] (Suma,Description,id_Patient,Date) values ('{Price}','{Descr}','{Id_Pat}','{Date}')";
            SQLiteCommand _cmd = new SQLiteCommand(query, con);
            _cmd.ExecuteNonQuery();

        }
        public static void InsertTreatment(string Price, string Descr, string Id_Pat, string Date)
        {

            string query = $"insert into [Treatment] (Date,Description,id_Patient,Price) values ('{Date}','{Descr}','{Id_Pat}','{Price}')";
            SQLiteCommand _cmd = new SQLiteCommand(query, con);
            _cmd.ExecuteNonQuery();

        }
        public static Patient getPatient(string Id)
        {
            Patient patient = new Patient();
            patient.Id = Id;
            
            string text = $"Select [Date],[Date_Birth], [Mobile_Phone], [Home_Phone], [Work_Phone], [Description], [Name],[Surname],[FatherName],[Gender] From [Patients] where Id='{Id}'";
            SQLiteCommand comand = new SQLiteCommand(text, con);
            SQLiteDataReader dataReader = comand.ExecuteReader();
            while (dataReader.Read())
            {
                patient.Date = dataReader.GetString(0);
                patient.Date_Birth = dataReader.GetString(1);
                patient.Mobile_Phone = dataReader.GetString(2);

                patient.Home_Phone = dataReader.GetString(3);
                patient.Work_Phone = dataReader.GetString(4);
                patient.Description = dataReader.GetString(5);
                patient.Name = dataReader.GetString(6);
                
                patient.Surname = dataReader.GetString(7);
                patient.FatherName = dataReader.GetString(8);
                patient.Gender = dataReader.GetString(9);
                
            }
           
            return patient;
        }
        public static string GetTreatmentString(string id)
        {
            string Rez = string.Empty;
            try
            {
                string text = $"Select [Date] From [Treatment] where id_Patient='{id}'";
                string tmp = string.Empty;
                string tmp1 = string.Empty;
                
                SQLiteCommand comand = new SQLiteCommand(text, con);
                SQLiteDataReader dataReader = comand.ExecuteReader();
                while (dataReader.Read())
                {
                    if (dataReader.GetString(0) != tmp)
                    {
                        
                        Rez += "Дата: " + dataReader.GetString(0) + "\n";
                        text = $"Select [Description] From [Treatment] where id_Patient='{id}' and Date='{dataReader.GetString(0)}'";

                        try
                        {
                         
                            SQLiteCommand comand1 = new SQLiteCommand(text, con);
                            SQLiteDataReader dataReader1 = comand1.ExecuteReader();
                            while (dataReader1.Read())
                            {
                               
                                Rez += "Описание: " + dataReader1.GetString(0) + "\n";
                                text = $"Select [Price] From [Treatment] where id_Patient='{id}' and Date='{dataReader.GetString(0)}' and Description='{dataReader1.GetString(0)}'";
                                try
                                {                                                                       
                                    SQLiteCommand comand2 = new SQLiteCommand(text, con);
                                    SQLiteDataReader dataReader2 = comand2.ExecuteReader();
                                    while (dataReader2.Read())
                                    {
                                        Rez += "Цена: " + dataReader2.GetDouble(0).ToString() + "\n";
                                    }
                                    Rez += "\n";
                                }
                                catch (Exception e)
                                {
                                }
                            }
                            

                        }
                        catch (Exception e)
                        {

                        }
                    }
                    tmp = dataReader.GetString(0);
                    
                }
                
            }
            catch (Exception e)
            {

            }
            return Rez;
        }
        public static Patient GetPatient(string Name, string Surname, string FatherName)
        {
            string query = "select * from [Patients]";
            query += $" where FatherName Like '%{FatherName}%' and Surname Like '%{Surname}%' and Name Like '%{Name}%'";
            
            SQLiteCommand _cmd = new SQLiteCommand(query, con);
            _cmd.ExecuteNonQuery();
            SQLiteDataAdapter _adp = new SQLiteDataAdapter(_cmd);
            DataTable _dt = new DataTable();
            _adp.Fill(_dt);
            _adp.Update(_dt);
            try
            {

                Patient p = new Patient();
                p.Date = (_dt.Rows[0])["Date"].ToString();
                p.Date_Birth = (_dt.Rows[0])["Date_Birth"].ToString();
                p.Name= (_dt.Rows[0])["Name"].ToString();
                p.Surname= (_dt.Rows[0])["Surname"].ToString(); 
                p.FatherName= (_dt.Rows[0])["FatherName"].ToString();
                p.Description= (_dt.Rows[0])["Description"].ToString();
                p.Home_Phone= (_dt.Rows[0])["Home_Phone"].ToString();
                p.Id= (_dt.Rows[0])["Id"].ToString();
                p.Mobile_Phone= (_dt.Rows[0])["Mobile_Phone"].ToString();
                p.Work_Phone= (_dt.Rows[0])["Work_Phone"].ToString();
                p.Gender = (_dt.Rows[0])["Gender"].ToString();

                return p;

            }
            catch (Exception)
            {

                
            }

            return null;
        }
        public static void UpdatePatient(string Name, string Surname, string FatherName, string Mobile_Phone, string Home_Phone, string Work_Phone, string Date_Birth, string Gender, string Card_Num,string Description,string Date, string id)
        {
            using (SQLiteCommand command = con.CreateCommand())
            {

                command.CommandText =
                    $"update Patients set Name = '{Name}', Surname = '{Surname}'," +
                    $" FatherName = '{FatherName}', Mobile_Phone = '{Mobile_Phone}'," +
                    $" Home_Phone = '{Home_Phone}', Work_Phone = '{Work_Phone}', " +
                    $"Date_Birth = '{Date_Birth}', Gender = '{Gender}', Card_Num = '{Card_Num}', Description = '{Description}', Date = '{Date}' where Id = '{id}'";
                
                command.ExecuteNonQuery();

            }
        }
        public static List<string> getPatientsTransactionString(string Id)
        {
            List<string> res = new List<string>();
            string text = $"Select [Date],[Type],[Suma],[Description] From [Transactions] where id_Patient='{Id}'";
            SQLiteCommand comand = new SQLiteCommand(text, con);
            SQLiteDataReader dataReader = comand.ExecuteReader();
            while (dataReader.Read())
            {
                res.Add("Дата:"+dataReader.GetString(0));
                res.Add("Тип:"+dataReader.GetString(1));
                res.Add("Сума:"+dataReader.GetValue(2).ToString());
                res.Add("Описание:"+dataReader.GetString(3)+"\n");
            }
            return res;
        }
    }
}
