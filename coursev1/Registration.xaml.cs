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
using System.Windows.Shapes;

namespace coursev1
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(login.Text) && !String.IsNullOrEmpty(password.Text) && !String.IsNullOrEmpty(name.Text) && !String.IsNullOrEmpty(lastname.Text) && !String.IsNullOrEmpty(phone.Text) && !String.IsNullOrEmpty(birth.Text))
            {
                if (!DatabaseManager.CheckUser(login.Text, password.Text))
                {
                    DatabaseManager.InsertClient(login.Text, password.Text);
                    DatabaseManager.InsertClientInfo(lastname.Text, name.Text, phone.Text, birth.Text);
                    MessageBox.Show("Client has been added");
                }
                else
                {
                    MessageBox.Show("Choose another login or password");
                }
            }
            else
            {
                MessageBox.Show("You have to fill all fields!");
            }
        }
    }
}
