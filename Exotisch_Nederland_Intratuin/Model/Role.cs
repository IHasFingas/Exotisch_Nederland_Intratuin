using Exotisch_Nederland_Intratuin.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exotisch_Nederland_Intratuin.Model {
    internal class Role {
        private SQLDAL SqlDal = SQLDAL.Instance;

        private int id;
        private string name;
        private string key;
        private List<User> users;

        public Role(int id, string name, string key) {
            this.id = id;
            this.name = name;
            this.key = key;
            this.users = new List<User>();
        }

        public Role(string name, string key) {
            this.name = name;
            this.key = key;
            this.users = new List<User>();

            SqlDal.AddRole(this);
        }

        public void AddUser(User user) {
            if (!users.Contains(user)) {
                users.Add(user);
            }
        }

        //Getters and Setters (veranderen we private attributen naar public incl { get; set; }?
        public string GetName() { return name; }

        public string GetKey() { return key; }

        public void SetID(int id) { this.id = id; }
    }
}
