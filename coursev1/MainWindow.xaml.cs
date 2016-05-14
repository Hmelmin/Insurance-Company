using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace coursev1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void authorize_Click(object sender, RoutedEventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Password;
            int type = DatabaseManager.GetTypeOfUser(login, password);
            int id = DatabaseManager.GetIdOfUser(login, password);
            Checker checker = new Checker();
            User current = checker.Authorize(id,type);
            if (current != null)
            {
                current.ShowUserInterface(current.GetId());

            }
           
            
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Registration R = new Registration();
            R.Show();
        }
    }
}
