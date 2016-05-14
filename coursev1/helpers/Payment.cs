using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coursev1
{
    class Payment
    {
        int id;
        string date;
        int amount;
        public Payment()
        {

        }
        public Payment(int id, string date, int amount)
        {
            this.id = id;
            this.date = date;
            this.amount = amount;
        }


        public void SetId(int value)
        {
            id = value;
        }
        public int GetId()
        {
            return id;
        }
        public void SetAmount(int value)
        {
            amount = value;
        }
        public int GetAmount()
        {
            return amount;
        }
        public void SetDate(string value)
        {
            date = value;
        }
        public string  GetDate()
        {
            return date;
        }
        public override string ToString()
        {
            return "date: " + date + " amount" + amount;
        }

    }
}
