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

            this.id = SqlDal.AddRole(this);
        }


        //Methods

        public static List<Role> GetAll() {
            return SqlDal.GetAllRoles();
        }

        public static Role GetByID(int id) {
            return SqlDal.GetRoleByID(id);
        }

        public void Edit(string name, string key) {
            this.name = name;
            this.key = key;

            SqlDal.EditRole(this);
        }

        public void Delete() {
            SqlDal.DeleteRole(this);
        }

        public void AddUser(User user) {
            if (!users.Contains(user)) {
                users.Add(user);
            }
        }

        public void RemoveUser(User user) {
            if (users.Contains(user)) {
                users.Remove(user);
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
    }
}