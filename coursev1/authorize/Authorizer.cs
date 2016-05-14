using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;


namespace coursev1
{
    abstract class AbstractAuthorizer
    {
        public abstract User Authorize(int type, int id);
    }
    class Authorizer : AbstractAuthorizer
    {
        Creator UserCreator = new Creator();
        
        public override User Authorize(int type,int id)
        {
            
            return UserCreator.CreateUser(type, id);
        }
    }
    // proxy
    class Checker : AbstractAuthorizer
    {
        Authorizer authorizer = new Authorizer();
        public  override User Authorize(int id,int type)
        {
            
            if ((type >= 1 && type <= 5 ) && (id!= -1))
            {
                
                return authorizer.Authorize(type,id);
            }
            else
            {
                MessageBox.Show("unknown user");
                return null;
            }
        }
    }
}
