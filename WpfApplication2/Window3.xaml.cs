using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
using System.Windows.Threading;

namespace WpfApplication2
{
    /// <summary>
    /// Логика взаимодействия для Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        DispatcherTimer time1 = new DispatcherTimer();
        string pathdb;
        public Window3(string nme, string im)
        {
            InitializeComponent();
            pathdb = System.IO.Directory.GetCurrentDirectory();
            n.Content = nme;
           
            time1.Interval = TimeSpan.FromSeconds(0.1);
            time1.Tick += time1_tick;
            time1.IsEnabled = true;
        }
       
       
        private void time1_tick(object sender, EventArgs e)
        {
            dat.Content = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
        }
        OpenFileDialog op = new OpenFileDialog();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
              
                op.ShowDialog();
                file.Content = op.FileName;
            }
            catch
            {

            }
        }

        static string CalculateMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                string hash = CalculateMD5(op.FileName);
                /*
               string old_hash=хэш предыдущего или файл заявки
               hash=CreateMD5(old_hash+";"+hash);
                */
                file.Content = hash; 
                MessageBox.Show("Успешно!");
            }
            catch
            {

            }
        }
    }
}
