using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace coursev1
{
    class FormChecker: AbstractInsurance
    {
        insurance current;
        string message_in;
        string description;
        string amount;
        int type;
        int client_id;

        public FormChecker(string message_in, string description, string amount, int type, int id)
        {
            this.message_in = message_in;
            this.description = description;
            this.amount = amount;
            this.type = type;
            this.client_id = id;
            current = new insurance();
        }
        public override insurance AddNewInsurance()
        {
            int sum = 0;
            if(String.IsNullOrEmpty(message_in))
            { MessageBox.Show("Write a message to the company!"); }
            else if (String.IsNullOrEmpty(description))
            { MessageBox.Show("Write a description!"); }
            else
            {
                try
                {
                    sum = Convert.ToInt32(amount);
                }
                catch
                {
                    MessageBox.Show("Incorrect amount!");
                }

            }
            if (sum > 100 && sum < 1000001)
            {

                current.AddNewInsurance();
                current.SetMessageIn(message_in);
                current.SetDescription(description);
                current.SetPayout(sum);
                current.SetType(type);
                current.SetClient(client_id);
                current.SetMounthlyCalc();
                current.SetInsuranceInstance();
                current.SetMounthlyPay();
                
                current.SendToApply();
                return current;
            }
            else
            {
                MessageBox.Show("amount must be between 100 and 1000000");
            }
            return null;
        }
    }
}
