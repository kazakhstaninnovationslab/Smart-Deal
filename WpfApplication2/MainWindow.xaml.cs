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
using System.Data;
using System.Data.OleDb;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                reg rg = new reg();
                rg.ShowDialog();
            }
            catch
            {

            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try {
                string table = "m";
            OleDbConnection myOleDbConnection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + System.IO.Directory.GetCurrentDirectory() + @"\db.accdb");

            myOleDbConnection.Open();
            OleDbCommand myOleDbCommand;
            // создаем объект OleDbCommand
            try
            {


                myOleDbCommand = new OleDbCommand("SELECT*" + " FROM " + table+" WHERE Логин='" + log.Text + "'", myOleDbConnection);



                //создаем OleDB-адаптер
                OleDbDataAdapter MyOleDbAdapter = new OleDbDataAdapter();
                MyOleDbAdapter.SelectCommand = myOleDbCommand;
                OleDbCommandBuilder ds = new OleDbCommandBuilder(MyOleDbAdapter);
                //заполняем

                MyOleDbAdapter.TableMappings.Add(table, "MainTable");
                DataSet r1 = new DataSet();
                MyOleDbAdapter.Fill(r1, "MainTable");


                    if (pas.Password == r1.Tables[0].Rows[0]["Пароль"].ToString())
                    {
                        Window1 w1 = new Window1(log.Text);
                        w1.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Неверные данные!");
                    }
              
            }
            catch 
            {
                    MessageBox.Show("Неверные данные!");
                    myOleDbConnection.Close();
            }
        }
                        catch (Exception r)
                        {
                            MessageBox.Show(r.Message);
                           
                        }
}

        private void button_Copy1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Window2 w2 = new Window2("none");
                w2.ShowDialog();
            }
            catch
            {

            }
        }
    }
}
