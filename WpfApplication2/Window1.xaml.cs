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
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace WpfApplication2
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        string log;
        string mail;
        public Window1(string login)
        {
            InitializeComponent();
            try
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
                    image(login);
                 
                    fio.Content = r1.Tables[0].Rows[0]["Логин"];
                    mail = r1.Tables[0].Rows[0]["Email"].ToString();
                    poisk();

                }
                catch
                {
                  
                    myOleDbConnection.Close();
                }
            }
            catch
            {

            }
        }
        Vector<string> inds = new Vector<string>();
        Vector<string> im = new Vector<string>();
        void poisk()
        {
            try
            {
                im.Clear();
                inds.Clear();
                p.Items.Clear();
                string table = "cards";
               
                OleDbConnection myOleDbConnection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + System.IO.Directory.GetCurrentDirectory() + @"\db.accdb");

                myOleDbConnection.Open();
                OleDbCommand myOleDbCommand;
                // создаем объект OleDbCommand
                try
                {


                    myOleDbCommand = new OleDbCommand("SELECT*" + " FROM " + table + " WHERE Holders='" + mail + "'", myOleDbConnection);



                    //создаем OleDB-адаптер
                    OleDbDataAdapter MyOleDbAdapter = new OleDbDataAdapter();
                    MyOleDbAdapter.SelectCommand = myOleDbCommand;
                    OleDbCommandBuilder ds = new OleDbCommandBuilder(MyOleDbAdapter);
                    //заполняем

                    MyOleDbAdapter.TableMappings.Add(table, "MainTable");
                    DataSet r1 = new DataSet();
                    MyOleDbAdapter.Fill(r1, "MainTable");
                    for(int i = 0; i < r1.Tables[0].Rows.Count; i++)
                    {
                        string card = r1.Tables[0].Rows[i]["Card"].ToString();
                        string club = r1.Tables[0].Rows[i]["Club"].ToString();
                        string cost = r1.Tables[0].Rows[i]["Cost"].ToString();
                        string ind = r1.Tables[0].Rows[i]["Ind"].ToString();
                        string img = r1.Tables[0].Rows[i]["Image"].ToString();
                        im.push_back(img);
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
       
        string pathdb;
        void image(string background)
        {
            try
            {
                try
                {
                    string path1 = System.IO.Path.Combine(pathdb + @"\фото\", background + ".png");
                    ImageSourceConverter imgs = new ImageSourceConverter();
                    image1.SetValue(Image.SourceProperty, imgs.ConvertFromString(path1));

                }
                catch
                {
                    try
                    {
                        string path1 = System.IO.Path.Combine(pathdb + @"\фото\", background + ".jpg");
                        ImageSourceConverter imgs = new ImageSourceConverter();
                        image1.SetValue(Image.SourceProperty, imgs.ConvertFromString(path1));
                    }
                    catch
                    {
                        string path1 = System.IO.Path.Combine(pathdb + @"\фото\", background + ".bmp");
                        ImageSourceConverter imgs = new ImageSourceConverter();
                        image1.SetValue(Image.SourceProperty, imgs.ConvertFromString(path1));
                    }
                }
              
            }
            catch
            {

            }
        }
        private void button_Copy4_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_Copy7_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Win32.OpenFileDialog openFileDialog2 = new Microsoft.Win32.OpenFileDialog();
                openFileDialog2.Title = "Выберите фото";
                openFileDialog2.FileName = "";
                openFileDialog2.Filter = "Photos (*.png)|*.png|JPG (*.jpg)|*.jpg";
                openFileDialog2.ShowDialog();
                DirectoryInfo newFolder = new DirectoryInfo(pathdb + @"\фото");
                if (!newFolder.Exists)
                    newFolder.Create();
                if (openFileDialog2.FilterIndex == 1)
                {
                    File.Copy(openFileDialog2.FileName, pathdb + @"\фото\" + log+ ".png");

                }
                if (openFileDialog2.FilterIndex == 2)
                {
                    File.Copy(openFileDialog2.FileName, pathdb + @"\фото\" + log + ".jpg");

                }
                image(log);
            }
            catch
            {

            }
        }

        private void bf2_Copy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Web w2 = new Web(pathdb + @"\фото\" + log + ".jpg");
                w2.ShowDialog();
                image(log);
            }
            catch
            {

            }
        }

        private void asks_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Window2 w2 = new Window2(log);
                w2.ShowDialog();
                poisk();
            }
            catch
            {

            }
        }

        private void bf2_Copy1_Click(object sender, RoutedEventArgs e)
        {
            poisk();
        }

      

        private void p_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string img = im.At(p.SelectedIndex);
            string nme = p.SelectedValue.ToString();
            Window3 w3 = new Window3(nme,img);
            w3.ShowDialog();
        }
    }
}
