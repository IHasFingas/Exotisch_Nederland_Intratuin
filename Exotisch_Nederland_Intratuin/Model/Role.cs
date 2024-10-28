using Exotisch_Nederland_Intratuin.DAL;
using System.Collections.Generic;

namespace Exotisch_Nederland_Intratuin.Model {
    internal class Role {
        private static SQLDAL SqlDal = SQLDAL.Instance;

        private int id;
        private string name;
        private string key;
        private List<User> users;


        //Constructor for creating a Role from database
        public Role(int id, string name, string key) {
            this.id = id;
            this.name = name;
            this.key = key;
            this.users = new List<User>();
        }

        //Constructor for creating a Role from scratch (automatically adds it to the database)
        public Role(string name, string key) {
            this.name = name;
            this.key = key;
            this.users = new List<User>();

            SqlDal.AddRole(this);
        }


        //Methods

        public static List<Role> GetALlRoles() {
            return SqlDal.GetAllRoles();
        }

        public static Role GetRoleByID(int id) {
            return SqlDal.GetRoleByID(id);
        }

        public void EditRole(string name, string key) {
            this.name = name;
            this.key = key;
            SqlDal.EditRole(this);
        }

        public void DeleteRole() {
            SqlDal.DeleteRole(this);
        }

        public void AddUser(User user) {
            if (!users.Contains(user)) {
                users.Add(user);
            }
        }

        public override string ToString() {
            return $"Role {id}: {name}, Key => {key}";
        }


        //Getters and Setters

        public int GetID() { return id; }

        public string GetName() { return name; }

        public string GetKey() { return key; }

        public List<User> GetUsers() { return users; }

        public void SetID(int id) { this.id = id; }
    }
}