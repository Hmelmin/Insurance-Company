using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
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
    /// Interaction logic for FinancialWindow.xaml
    /// </summary>
    public partial class FinancialWindow : Window
    {
        private int id_user;
        private ObservableCollection<insurance> insurances;
        
        public FinancialWindow()
        {
            InitializeComponent();
        }
       
        internal FinancialWindow(int id, ObservableCollection<insurance> insurances)
        {
            this.id_user = id;
            this.insurances = insurances;
            InitializeComponent();
            listBox1.ItemsSource = insurances;
        }

        
        private void Clear()
        {
            message_in.Text = "";
            message_out.Text = "";
            message_between.Text = "";
            client.Text = "";
            phone.Text = "";
            description.Text = "";
            ballance.Text = "";
            history.ItemsSource = null;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            

            insurance tmp = (insurance)listBox1.SelectedItem;
            if (tmp != null && !(String.IsNullOrEmpty(message_out.Text)))
            {
                tmp.SetMessageOut(message_out.Text);
                DatabaseManager.SetMessage_Out(tmp.GetId(), message_out.Text);
                DatabaseManager.SetPayment(tmp.GetId(), 0);
                tmp.DenyPayment();
                insurances.Remove(tmp);

                
                Clear();
                
            }
            else
            {
                MessageBox.Show("You haven't chosen insurance or haven't written message to client");
            }
            
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {

            insurance tmp = (insurance)listBox1.SelectedItem;
            if (tmp != null && !(String.IsNullOrEmpty(message_out.Text)))
            {
                tmp.SetMessageOut(message_out.Text);
                DatabaseManager.SetMessage_Out(tmp.GetId(), message_out.Text);
                DatabaseManager.SetPayment(tmp.GetId(),tmp.GetPayout());
                tmp.AcceptPayment(100);
                insurances.Remove(tmp);
                
                Clear();

            }
            else
            {
                MessageBox.Show("You haven't chosen insurance or haven't written message to client");
            }

        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            int percents = 100;
            insurance tmp = (insurance)listBox1.SelectedItem;
            if (tmp != null && !(String.IsNullOrEmpty(message_out.Text)))
            {
                try
                {
                    percents = Convert.ToInt32(Percents.Text);
                }
                catch
                {
                    MessageBox.Show("Incorrect Input!");
                }

                if (percents > 0 && percents < 100)
                {
                    tmp.SetMessageOut(message_out.Text);
                    DatabaseManager.SetMessage_Out(tmp.GetId(), message_out.Text);
                    DatabaseManager.SetPayment(tmp.GetId(), tmp.GetPayout()*percents/100);
                    tmp.AcceptPayment(percents);
                    insurances.Remove(tmp);
                    Percents.Text = "100";
                    
                    Clear();

                }
                else
                { MessageBox.Show("Incorrect Input!"); }
                
            }
            else
            {
                MessageBox.Show("You haven't chosen insurance or haven't written message to client");
            }
        }
        private void details_Click(object sender, RoutedEventArgs e)
        {

            if (listBox1.SelectedItem != null)
            {
                insurance current = (insurance)listBox1.SelectedItem;
                client.Text = DatabaseManager.GetClientInfo(current.GetClient());
                phone.Text = DatabaseManager.GetClientPhone(current.GetClient());
                description.Text = current.GetDescription();
                ballance.Text = Convert.ToString(current.GetBallance());
                List<Payment> tmp = DatabaseManager.GetHistoryById(current.GetId());
                ObservableCollection<Payment> payments = new ObservableCollection<Payment>();
                foreach (Payment x in tmp)
                {
                    payments.Add(x);
                }
                history.ItemsSource = payments;
                message_in.Text = current.GetMessageIn();
                message_between.Text = current.GetMessageBetween();
            }

        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            Clear();
            listBox1.SelectedItem = null;
        }

        
    }
}
