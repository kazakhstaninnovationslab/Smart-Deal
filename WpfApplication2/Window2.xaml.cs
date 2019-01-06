using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
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

namespace WpfApplication2
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        string log;
        string mail;
        string pathdb; 
        public Window2(string login)
        {
            InitializeComponent();
            try
            {
                if (!login.Equals("none"))
                {
                    log = login;
                    string table = "m";
                    OleDbConnection myOleDbConnection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + System.IO.Directory.GetCurrentDirectory() + @"\db.accdb");

                    myOleDbConnection.Open();
                    OleDbCommand myOleDbCommand;
                    // создаем объект OleDbCommand
                    try
                    {


                        myOleDbCommand = new OleDbCommand("SELECT*" + " FROM " + table + " WHERE Логин='" + login + "'", myOleDbConnection);



                        //создаем OleDB-адаптер
                        OleDbDataAdapter MyOleDbAdapter = new OleDbDataAdapter();
                        MyOleDbAdapter.SelectCommand = myOleDbCommand;
                        OleDbCommandBuilder ds = new OleDbCommandBuilder(MyOleDbAdapter);
                        //заполняем

                        MyOleDbAdapter.TableMappings.Add(table, "MainTable");
                        DataSet r1 = new DataSet();
                        MyOleDbAdapter.Fill(r1, "MainTable");
                        pathdb = System.IO.Directory.GetCurrentDirectory();


                        fio.Content = r1.Tables[0].Rows[0]["Логин"];
                        mail = r1.Tables[0].Rows[0]["Email"].ToString();
                        poisk();

                    }
                    catch
                    {

                        myOleDbConnection.Close();
                    }
                }
                else
                {
                    mail = "none";
                    poisk();
                }
            }
            catch
            {
                poisk();
                mail = "none";
            }
        }
        Vector<string> inds = new Vector<string>();
    void poisk()
    {
        try
        {
                inds.Clear();
                p.Items.Clear();
            string table = "cards";

            OleDbConnection myOleDbConnection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + System.IO.Directory.GetCurrentDirectory() + @"\db.accdb");

            myOleDbConnection.Open();
            OleDbCommand myOleDbCommand;
            // создаем объект OleDbCommand
            try
            {


                myOleDbCommand = new OleDbCommand("SELECT*" + " FROM " + table + " WHERE Holders='none'", myOleDbConnection);



                //создаем OleDB-адаптер
                OleDbDataAdapter MyOleDbAdapter = new OleDbDataAdapter();
                MyOleDbAdapter.SelectCommand = myOleDbCommand;
                OleDbCommandBuilder ds = new OleDbCommandBuilder(MyOleDbAdapter);
                //заполняем

                MyOleDbAdapter.TableMappings.Add(table, "MainTable");
                DataSet r1 = new DataSet();
                MyOleDbAdapter.Fill(r1, "MainTable");
                for (int i = 0; i < r1.Tables[0].Rows.Count; i++)
                {
                    string card = r1.Tables[0].Rows[i]["Card"].ToString();
                    string club = r1.Tables[0].Rows[i]["Club"].ToString();
                    string cost = r1.Tables[0].Rows[i]["Cost"].ToString();
                    string ind = r1.Tables[0].Rows[i]["Ind"].ToString();
                        inds.push_back(ind);
                    p.Items.Add(club + " " + card + " [" + ind + "] " + cost);
                }

            }
            catch (Exception r)
            {
                MessageBox.Show(r.Message);
                myOleDbConnection.Close();
            }
        }
        catch (Exception r)
        {
            MessageBox.Show(r.Message);
        }
    }

        //участвовать
    private void asks_Copy1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!mail.Equals("none"))
                {

                    string table = "cards";

                    OleDbConnection myOleDbConnection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + System.IO.Directory.GetCurrentDirectory() + @"\db.accdb");

                    myOleDbConnection.Open();
                    OleDbCommand myOleDbCommand;
                    // создаем объект OleDbCommand
                    try
                    {


                        //myOleDbCommand = new OleDbCommand("SELECT*" + " FROM " + table + " WHERE Ind='"+inds.At(p.SelectedIndex)+"'", myOleDbConnection);


                        myOleDbCommand = new OleDbCommand("Update " + table + " SET Holders='" + mail + "' WHERE Ind='" + inds.At(p.SelectedIndex) + "'", myOleDbConnection);

                        myOleDbCommand.ExecuteNonQuery();
                        Window4 w4 = new Window4();
                        w4.ShowDialog();
                        poisk();

                    }
                    catch (Exception r)
                    {
                        MessageBox.Show(r.Message);
                        myOleDbConnection.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Войдите в аккаунт");
                }
            }
            catch (Exception r)
            {
                MessageBox.Show(r.Message);
            }
        }

       
    }
}
