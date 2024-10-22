using Exotisch_Nederland_Intratuin.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exotisch_Nederland_Intratuin.Model
{
    internal class Role
    {
        private int id;
        private string name;
        private string key;
        private List<User> users;

        public void EditRole(string name, string key)
        {
            this.name = name;
            this.key = key;
            SqlDal.EditRole(this);
        }

        public void DeleteRole()
        {
            SqlDal.DeleteRole(this);
        }
    }
}
