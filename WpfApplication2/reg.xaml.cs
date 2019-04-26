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
using System.Data;
using System.Data.OleDb;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApplication2
{
    /// <summary>
    /// Логика взаимодействия для reg.xaml
    /// </summary>
    /// 
    class Vector<T>
    {
        T[] array;
        public Vector()
        {
            array = new T[0];
        }
        public Vector(int count)
        {
            array = new T[count];
        }
        public T At(int index)
        {

            return array[index];
        }

        public T back
        {
            get
            {
                return array[array.Length - 1];
            }
            set
            {
                array[array.Length - 1] = value;
            }
        }
        public void isat(int index, T value)
        {

            array[index] = value;

        }
        public T Front
        {
            get
            {
                return array[0];
            }
            set
            {
                array[0] = value;
            }
        }
        public void Clear()
        {
            array = new T[0];
        }

        public void Insert(int pos, T item)
        {
            Array.Resize(ref array, array.Length + 1);
            for (int i = array.Length - 1; i > pos; i--)
                array[i] = array[i - 1];
            array[pos] = item;
        }
        public void push_back(T item)
        {
            Array.Resize(ref array, array.Length + 1);
            array[array.Length - 1] = item;
        }
        public void pop_back()
        {
            Array.Resize(ref array, array.Length - 1);

        }
        public void push_front(T item)
        {
            Insert(0, item);
        }
        public int Count
        {
            get
            {
                return array.Length;
            }
        }
        public T[] ToArray()
        {
            return array;
        }
    }
    public partial class reg : Window
    {
        public reg()
        {
            InitializeComponent();
            
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (pas.Password != pas1.Password)
                {
                    MessageBox.Show("Пароли не совпадают!");
                }
                else
                {
                    bool l = true;
                    var wr = new System.IO.StreamReader("logins.txt");
                    Vector<string> st = new Vector<string>();
                    while (wr.EndOfStream != true)
                    {
                        string k = wr.ReadLine();
                        if ( k== login.Text)
                        {
                            l = false;
                           
                        }
                        st.push_back(k);
                    }
                    wr.Close();
                    if (l == false)
                    {
                        MessageBox.Show("Данный логин уже используется!");
                    }
                    else
                    {
                        string[] labels = new string[] { "Логин", "Пароль", "Email"};
                      
                        string[] values = new string[] { login.Text, pas.Password, email.Text };
                     
                    
                        string table = "m";
                        try
                        {



                            OleDbConnection myOleDbConnection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + System.IO.Directory.GetCurrentDirectory() + @"\db.accdb");

                            myOleDbConnection.Open();
                            OleDbCommand myOleDbCommand;
                            // создаем объект OleDbCommand
                            try
                            {


                                myOleDbCommand = new OleDbCommand("SELECT*" + " FROM " + table, myOleDbConnection);



                                //создаем OleDB-адаптер
                                OleDbDataAdapter MyOleDbAdapter = new OleDbDataAdapter();
                                MyOleDbAdapter.SelectCommand = myOleDbCommand;
                                OleDbCommandBuilder ds = new OleDbCommandBuilder(MyOleDbAdapter);
                                //заполняем

                                MyOleDbAdapter.TableMappings.Add(table, "MainTable");
                                DataSet r1 = new DataSet();
                                MyOleDbAdapter.Fill(r1, "MainTable");
                               DataRow row=r1.Tables[0].NewRow();

                                row["Логин"] = login.Text;
                                row["Пароль"] = pas.Password;
                                
                                row["Email"] = email.Text;
                              
                                r1.Tables[0].Rows.Add(row);
                                MyOleDbAdapter.Update(r1, "MainTable");
                                myOleDbConnection.Close();
                                var ww = new System.IO.StreamWriter("logins.txt");
                                ww.WriteLine(login.Text);
                                for (int i = 0; i < st.Count; i++)
                                {
                                    ww.WriteLine(st.At(i));
                                }
                                ww.Close();
                                MessageBox.Show("Успешно!");
                                this.Close();
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
                }
            }
            catch (Exception r)
            {
                MessageBox.Show(r.Message);

            }
        }
    }
}
