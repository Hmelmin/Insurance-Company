using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coursev1
{
    public class Type
    {
        int id;
        string type;
        public Type()
        {

        }
        public Type(int id, string name)
        {
            this.id = id;
            this.type = name;
        }
        public void SetId(int value)
        {
            id = value;
        }
        public int GetId()
        {
            return id;
        }
        public void SetType(string value)
        {
            type = value;
        }
        public string GetName()
        {
            return type;
        }
        public override string ToString()
        {
            return type;
        }
    }
}
