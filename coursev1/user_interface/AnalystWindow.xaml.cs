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
    /// Interaction logic for AnalystWindow.xaml
    /// </summary>
    public partial class AnalystWindow : Window
    {
        
        
        private int id_user;
        ObservableCollection<insurance> insurances;
        public AnalystWindow()
        {
            InitializeComponent();
        }
        
        internal AnalystWindow(int id, ObservableCollection<insurance> insurances)
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
            client.Text = "";
            phone.Text = "";
            description.Text = "";
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            insurance tmp = (insurance)listBox1.SelectedItem;
            if (tmp != null && !(String.IsNullOrEmpty(message_out.Text)))
            {
                tmp.SetMessageOut(message_out.Text);
                DatabaseManager.SetMessage_Out(tmp.GetId(), message_out.Text);
                tmp.ApplyDeclaration();
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
                DatabaseManager.SetMessage_Out(tmp.GetId(), tmp.GetMessageOut());
                tmp.DenyDeclaration();
                insurances.Remove(tmp);
                
                Clear();
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
                message_in.Text = current.GetMessageIn();
            }
            
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            Clear();
            listBox1.SelectedItem = null;
        }
    }
}
