using Exotisch_Nederland_Intratuin.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exotisch_Nederland_Intratuin.Model
{
    internal class User
    {
            private int id;
            private string name;
            private string email;
            private string currentLocation;
            private Route currentRoute;
            private List<Role> roles;
            private List<Observation> observations;

        public void EditUser(string name, string email)
        {
            this.name = name;
            this.email = email;
             SqlDal.EditUser(this);
        }

        public void RemoveUser()
        {
            SQLDAL.DeleteUser(this);
        }


    }
       
}
