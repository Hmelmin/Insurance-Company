using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace coursev1
{
    
    interface IUserInterface
    {
         void ShowUserInterface(int id);
         
    }
    class User 
    {
        private int id;
        protected IUserInterface UI;
       
        public User(IUserInterface ui, int id)
        {
            this.UI = ui;
            this.id = id;
            
        }
        public void ShowUserInterface(int id)
        {
            if (UI != null)
            { UI.ShowUserInterface(id); }
        }
        public int GetId()
        {
            return id;
        }
    }
    class ClientInterface : IUserInterface
    {
        public  void ShowUserInterface(int id)
        {
            ObservableCollection<insurance> insurances = new ObservableCollection<insurance>();
            List<insurance> tmp = DatabaseManager.GetInsurancesById(id);

            foreach (insurance x in tmp)
            {
                insurances.Add(x);
            }
            ClientWindow CW = new ClientWindow(id, insurances);
            CW.Show();
        }
        
    }
    class AnalystInterface : IUserInterface
    {
        public void ShowUserInterface(int id)
        {
           ObservableCollection<insurance> insurances = new ObservableCollection<insurance>();
           List<insurance> tmp = DatabaseManager.GetRequiredInsurances(1);

            foreach (insurance x in tmp)
            {
                insurances.Add(x);
            }
            AnalystWindow AW = new AnalystWindow(id, insurances);
            AW.Show();
            


        }
    }
    class FinancialInterface : IUserInterface
    {
        public void ShowUserInterface(int id)
        {
            ObservableCollection<insurance> insurances = new ObservableCollection<insurance>();
            List<insurance> tmp = DatabaseManager.GetRequiredInsurances(4);

            foreach (insurance x in tmp)
            {
                insurances.Add(x);
            }
            FinancialWindow FW = new FinancialWindow(id, insurances);
            FW.Show();
        }
    }
    class PoliceInterface : IUserInterface
    {
        public void ShowUserInterface(int id)
        {
            ObservableCollection<insurance> insurances = new ObservableCollection<insurance>();
            List<insurance> tmp = DatabaseManager.GetInsurancesToCheck(1);

            foreach (insurance x in tmp)
            {
                insurances.Add(x);
            }
            MediatorWindow MW = new MediatorWindow(id, insurances);
            MW.Show();
        }
    }
    class HospitalInterface : IUserInterface
    {
        public void ShowUserInterface(int id)
        {
            ObservableCollection<insurance> insurances = new ObservableCollection<insurance>();
            List<insurance> tmp = DatabaseManager.GetInsurancesToCheck(2);

            foreach (insurance x in tmp)
            {
                insurances.Add(x);
            }
            MediatorWindow MW = new MediatorWindow(id, insurances);
            MW.Show();
        }
    }
}
