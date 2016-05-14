using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coursev1
{
    class Creator
    {
        public Creator()
        {

        }
        public User CreateUser(int type, int id)
        {
            switch (type)
            {
                case 1:
                    return new User(new ClientInterface(), id);
                case 2:
                    return new User(new AnalystInterface(),id);
                case 3:
                    return new User(new FinancialInterface(), id);
                case 4: 
                    return new User(new PoliceInterface(),id);
                case 5:
                    return new User(new HospitalInterface(), id);
                default:
                    return null;
                    
            }

        }
    }
}
