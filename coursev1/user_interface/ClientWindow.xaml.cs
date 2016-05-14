using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
    /// Interaction logic for ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        
        int id_user;
        ObservableCollection<insurance> insurances;
        
        public ClientWindow()
        {
            InitializeComponent();
            
        }

       
        internal ClientWindow(int id, ObservableCollection<insurance> insurances)
        {
            this.id_user = id;
            this.insurances = insurances;
            InitializeComponent();
            listBox1.ItemsSource = insurances;
            
        }

        public void Clear()
        {
            status.Text = "";
            ballance.Text = "";
            mounthly.Text = "";
            message_out.Text = "";
            description.Text = "";
            message_in.Text = "";
            message_out.Text = "";
            amount.Text = "";
            listBox1.SelectedItem = null;
        }
        private void details_Click(object sender, RoutedEventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                insurance current = (insurance)listBox1.SelectedItem;
                status.Text = Convert.ToString(current.GetCurrentStatus());
                ballance.Text = Convert.ToString(current.GetBallance());
                mounthly.Text = Convert.ToString(current.GetMounthlyPay());
                message_out.Text = current.GetMessageOut();
                description.Text = current.GetDescription();
            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void pay_Click(object sender, RoutedEventArgs e)
        {
            int sum = 0;
            if (listBox1.SelectedItem != null)
            {
                insurance current = (insurance)listBox1.SelectedItem;
                try
                {
                    sum = Convert.ToInt32(amount.Text);
                }
                catch
                {
                    MessageBox.Show("incorrect input");
                }
                if (sum > 0 && sum <= 3 * current.GetMounthlyPay())
                {
                    current.UpdateBallance(sum);
                    
                }
                else
                {
                    MessageBox.Show("too large amount");
                }
                Clear();
            }
            else
            {
                MessageBox.Show("Select an insurance!");
            }
        }

        private void request_Click(object sender, RoutedEventArgs e)
        {
            insurance tmp = (insurance)listBox1.SelectedItem;
            if (tmp != null && !(String.IsNullOrEmpty(message_out.Text)))
            {
                tmp.SetMessageIn(message_in.Text);
                DatabaseManager.SetMessage_In(tmp.GetId(), message_in.Text);
                tmp.SendForPayment();
                
                Clear();
            }
            else
            {
                MessageBox.Show("You haven't chosen insurance or haven't written message to client");
            }
        }

        private void create_new_Click(object sender, RoutedEventArgs e)
        {
            NewInsurance NI = new NewInsurance(this.id_user, insurances);
            NI.Show();
        }
    }
}
