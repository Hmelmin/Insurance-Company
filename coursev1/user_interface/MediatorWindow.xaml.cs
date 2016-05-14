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
    /// Interaction logic for MediatorWindow.xaml
    /// </summary>
    public partial class MediatorWindow : Window
    {
        private int id_user;
        private ObservableCollection<insurance> insurances;
        public MediatorWindow()
        {
            InitializeComponent();
        }
        internal MediatorWindow(int id, ObservableCollection<insurance> insurances)
        {
            this.id_user = id;
            this.insurances = insurances;
            InitializeComponent();
            listBox1.ItemsSource = insurances;
        }
        public void Clear()
        {
            message_in.Text = "";
            phone.Text = "";
            client.Text = "";
            message_between.Text = "";
            listBox1.SelectedItem = null;

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            insurance current= (insurance)listBox1.SelectedItem;
            if(current!=null)
            {
                message_in.Text = current.GetMessageIn();
                client.Text = DatabaseManager.GetClientInfo(current.GetClient());
                phone.Text = DatabaseManager.GetClientPhone(current.GetClient()); 
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            insurance current = (insurance)listBox1.SelectedItem;
            if (current != null)
            {
                if (!String.IsNullOrEmpty(message_between.Text))
                {
                    current.SetMessageBetween(message_between.Text);
                    DatabaseManager.SetMessage_Between(current.GetId(), current.GetMessageBetween());
                    DatabaseManager.UpdateStatus(current, 4, 0);
                    insurances.Remove(current);
                    MessageBox.Show("Report has been sent");
                    Clear();
                }
                else
                {
                    MessageBox.Show("Write a report to company!");
                }
            }
            else
            {
                MessageBox.Show("Select an insurance!");
            }
        }
    }
}
