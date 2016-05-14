using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;


namespace coursev1
{
    abstract class AbstractInsurance
    {
        public abstract insurance AddNewInsurance();
    }
    abstract class abstractState
    {
        protected insurance current;
        public abstract void UpdateBallance(int value);
        //public abstract void SendToApply();
        public abstract void SendForPayment();
        public abstract void DenyPayment();
        public abstract void AcceptPayment(int percents);
        public abstract void DenyDeclaration();
        public abstract void AcceptDeclaration();
        public abstractState(insurance x)
        {
            current = x;
        }
    }
    class DeniedState : abstractState
    {
        public DeniedState(insurance x) : base(x) { }
        public override void UpdateBallance(int value)
        {
            MessageBox.Show("Your insurance has been denied");
        }
        public override void SendForPayment()
        {
            MessageBox.Show("Your insurance has been denied");
        }
        public override void AcceptDeclaration()
        {
            MessageBox.Show("Your insurance has been denied");
        }
        public override void AcceptPayment(int percents)
        {
            MessageBox.Show("Your insurance has been denied");
        }
        public override void DenyDeclaration()
        {
            MessageBox.Show("Your insurance has been denied");
        }
        public override void DenyPayment()
        {
            MessageBox.Show("Your insurance has been denied");
        }

    }

    class SentToApplyState : abstractState
    {
        public SentToApplyState(insurance x) : base(x) { }
        public override void UpdateBallance(int value)
        {
            MessageBox.Show("Your insurance has been not applied yet");
        }
        public override void SendForPayment()
        {
            MessageBox.Show("Your insurance has been noot applied yet");
        }
        public override void AcceptDeclaration()
        {
            current.SetState(new ConfirmedState(current));
            current.SetStatus(3);
            DatabaseManager.UpdateStatus(current, 3, 0);
            MessageBox.Show("Declaration has been applied");
        }
        public override void AcceptPayment(int percents)
        {
            MessageBox.Show("Your insurance has been not applied yet");
        }
        public override void DenyDeclaration()
        {
            current.SetState(new DeniedState(current));
            current.SetStatus(2);
            DatabaseManager.UpdateStatus(current, 2, 0);
            MessageBox.Show("Declaration has been denied");
        }
        public override void DenyPayment()
        {
            MessageBox.Show("Your insurance has been not applied yet");
        }

    }

    class ConfirmedState : abstractState
    {
        public ConfirmedState(insurance x) : base(x) { }
        public override void UpdateBallance(int value)
        {
            current.SetBallance(current.GetBallance() + value);
            DatabaseManager.UpdateBallance(current, value);
            MessageBox.Show("ballance has been updated");
        }
        public override void SendForPayment()
        {
            current.SetStatus(4);
            current.GetInsuranceInstance().SendToApply(current);
            current.SetState(new InsuranceCaseState(current));
            MessageBox.Show("Your insurance case has been registrated");
        }
        public override void AcceptDeclaration()
        {
            MessageBox.Show("Your insurance has been already registrated");
        }
        public override void AcceptPayment(int percents)
        {
            MessageBox.Show("Your insurance case has not been  already registrated");
        }
        public override void DenyDeclaration()
        {
            MessageBox.Show("Your insurance has been already registrated");
        }
        public override void DenyPayment()
        {
            MessageBox.Show("Your insurance case  has been not already registrated");
        }

    }


    class InsuranceCaseState : abstractState
    {
        public InsuranceCaseState(insurance x) : base(x) { }
        public override void UpdateBallance(int value)
        {
            current.SetBallance(current.GetBallance() + value);
            DatabaseManager.UpdateBallance(current, value);
            MessageBox.Show("ballance has been updated");
        }
        public override void SendForPayment()
        {
            MessageBox.Show("Your insurance case has been already registrated");
        }
        public override void AcceptDeclaration()
        {
            MessageBox.Show("Your insurance has been already denied");
        }
        public override void AcceptPayment(int percents)
        {
            current.SetStatus(3);
            current.SetState(new ConfirmedState(current));
            DatabaseManager.UpdateStatus(current, 3, 0);
            DatabaseManager.UpdateAcct(current.GetClient(), current.GetPayout() * percents / 100);
            MessageBox.Show("payment accepted");
        }
        public override void DenyDeclaration()
        {
            MessageBox.Show("Your insurance has been already registrated");   
        }
        public override void DenyPayment()
        {
            current.SetStatus(3);
            current.SetState(new ConfirmedState(current));
            DatabaseManager.UpdateStatus(current, 3, 0);
            MessageBox.Show("payment denied");
        }

    }


    interface IMounthlyPayCount
    {
        int CalculateMounthlyPay(int amount);
    }

    class HousePayCount : IMounthlyPayCount
    {
        public HousePayCount()
        { }
        public int CalculateMounthlyPay(int amount)
        {
            return amount / 50;
        }
    }
    class VehiclePayCount : IMounthlyPayCount
    {
        public VehiclePayCount()
        { }
        public int CalculateMounthlyPay(int amount)
        {
            return amount / 25;
        }
    }
    class LifePayCount : IMounthlyPayCount
    {
        public LifePayCount()
        { }
        public int CalculateMounthlyPay(int amount)
        {
            return amount / 100;
        }
    }
    class MedicalPayCount : IMounthlyPayCount
    {
        public MedicalPayCount()
        { }
        public int CalculateMounthlyPay(int amount)
        {
            return 25;
        }
    }

    interface IInsuranceCase 
    {
        void SendToApply(insurance x);
    }

    class HospitalInsurance :IInsuranceCase
    {
        public HospitalInsurance()
        { }
        public void SendToApply(insurance x)
        {
            DatabaseManager.UpdateStatus(x,4,2);
        }
    }

    class PoliceInsurance : IInsuranceCase
    {
        public PoliceInsurance()
        { }
        public void SendToApply(insurance x)
        {
            DatabaseManager.UpdateStatus(x, 4, 1);
        }
    }
    class insurance : AbstractInsurance
    {
        private int id;
        private int client_id;
        private int payout;
        private int mounthly;
        private int ballance;
        private int status;
        private string description;
        
        private int type;
        private string message_in;
        private string message_out;
        abstractState CurrentState;
        private string message_between;
        IMounthlyPayCount MounthlyCalc;
        IInsuranceCase InsuranceInstance;
        public override insurance AddNewInsurance()
        {
            ballance = 0;
            status = 1;
            SetState(new SentToApplyState(this));
            return this;
        }
        public insurance()
        {
            
            
        }
        public insurance(int id, int client, int payout, int mounthly, int ballance,  string description, int status,int type)
        {
            this.id = id;
            this.client_id = client;
            this.payout = payout;
            this.mounthly = mounthly;
            this.status = status;
            this.description = description;
            this.ballance = ballance;
            this.type = type;
            SetMounthlyCalc();
            SetInsuranceInstance();
            switch (status)
            {
                case 1:
                    SetState(new SentToApplyState(this));
                    break;
                case 2:
                    SetState(new DeniedState(this));
                    break;
                case 3:
                    SetState(new ConfirmedState(this));
                    break;
                case 4:
                    SetState(new InsuranceCaseState(this));
                    break;
                default:
                    SetState(null);
                    break;


            }
           
       }
        public int GetId()
        {
            return id;
        }
        public void SetId(int value)
        {
            this.id = value;
        }
        public int GetClient()
        {
            return client_id;
        }
        public void SetClient(int value)
        {
            this.client_id = value;
        }
        public int GetPayout()
        {
            return payout;
        }
        public void SetPayout(int value)
        {
            this.payout = value;
        }
        public int GetMounthlyPay()
        {
            return mounthly;
        }
        public void SetMounthlyPay()
        {
            if (MounthlyCalc != null)
            {
                mounthly = MounthlyCalc.CalculateMounthlyPay(payout);
            }
            else
            {
                throw new Exception("Type is undefined");
            }
        }
        public int GetStatus()
        {
            return status;
        }
        public void SetStatus(int value)
        {
            this.status = value;
        }
        public string GetDescription()
        {
            return description;
        }
        public void SetDescription(string value)
        {
            this.description = value;
        }
        public int GetBallance()
        {
            return ballance;
        }
        public void SetBallance(int ballance)
        {
            this.ballance = ballance;
        }
        public void SetMessageIn(string message)
        {
            message_in = message;
        }
        public string GetMessageIn()
        {
            return message_in;
        }
        public void SetMessageOut(string message)
        {
            message_out = message;
        }
        public string GetMessageOut()
        {
            return message_out;
        }
        public void SetMessageBetween(string message)
        {
            message_between = message;
        }
        public string GetMessageBetween()
        {
            return message_between;
        }
        public void SetType(int value)
        {
            type = value;
        }
        public int GetThisType()
        {
            return type;
        }
        public void SetMounthlyCalc()
        {
            switch (type)
            {
                case 1:
                    MounthlyCalc = new HousePayCount();
                    break;
                case 2:
                    MounthlyCalc = new VehiclePayCount();
                    break;
                case 3:
                    MounthlyCalc = new LifePayCount();
                    break;
                case 4:
                    MounthlyCalc = new MedicalPayCount();
                    break;
                default:
                    MounthlyCalc = null;
                    break;
            }
        }
        public void SetInsuranceInstance()
        {
            switch (type)
            {
                case 1:
                    InsuranceInstance = new PoliceInsurance();
                    break;
                case 2:
                    InsuranceInstance = new PoliceInsurance();
                    break;
                case 3:
                    InsuranceInstance = new PoliceInsurance();
                    break;
                case 4:
                    InsuranceInstance = new HospitalInsurance();
                    break;
                default:
                    InsuranceInstance = null;
                    break;

            }

        }
        public IInsuranceCase GetInsuranceInstance()
        {
            return InsuranceInstance;
        }

        public void SetState(abstractState x)
        {
            this.CurrentState = x;
        }
        public void UpdateBallance( int value)
        {
            CurrentState.UpdateBallance(value);
        }

        public void SendToApply()
        {
            DatabaseManager.InsertInsurance(this); 
        }
        public void SendForPayment()
        {
            CurrentState.SendForPayment();
        }

        public void DenyPayment()
        {
            CurrentState.DenyPayment();
        }

        public void AcceptPayment(int percents)
        {
            CurrentState.AcceptPayment(percents);
        }
        public void DenyDeclaration()
        {
            CurrentState.DenyDeclaration();
        }
        public void ApplyDeclaration()
        {
            CurrentState.AcceptDeclaration();
        }
        public string GetCurrentStatus()
        {
            string result = "";
            switch (status)
            {
                case 1:
                    result = "sent to apply";
                    break;
                case 2:
                    result = "denied";
                    break;
                case 3:
                    result = "confirmed";
                    break;
                case 4 :
                    result = "insurance case";
                    break;
                default:
                    result = "unknown";
                    break;

            }
            return result;
        }
        public override string ToString()
        {
            return Convert.ToString(payout)+ " " + DatabaseManager.GetType(type) ;
        }


    }
}
