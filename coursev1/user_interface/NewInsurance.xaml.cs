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
    /// Interaction logic for NewInsurance.xaml
    /// </summary>
    public partial class NewInsurance : Window
    {
        int id;
        ObservableCollection<insurance> insurances;
        public NewInsurance()
        {
            InitializeComponent();
        }
        internal NewInsurance(int id, ObservableCollection<insurance> insurances)
        {
            this.id = id;
            this.insurances = insurances;
            Type type1 = new Type(1, "house");
            Type type2 = new Type(2, "vehicle");
            Type type3 = new Type(3, "life");
            Type type4 = new Type(4, "medical");
            ObservableCollection<Type> types = new ObservableCollection<Type>();
            types.Add(type1);
            types.Add(type2);
            types.Add(type3);
            types.Add(type4);
            InitializeComponent();
            insurance_types.ItemsSource = types;
        }

        private void apply_Click(object sender, RoutedEventArgs e)
        {
            Type CurrentType = (Type)insurance_types.SelectedItem;
            if (CurrentType != null)
            {
                FormChecker FC = new FormChecker(message_in.Text, description.Text, payout.Text, CurrentType.GetId(), id);
                insurance created = FC.AddNewInsurance();
                if (created != null)
                {
                    MessageBox.Show("Insurance has been added");
                    insurances.Add(created);
                }

            }
            else
            {
                MessageBox.Show("You have to select a type");
            }
        }
    }
}
