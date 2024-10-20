using Exotisch_Nederland_Intratuin.DAL;
using System.Collections.Generic;

namespace Exotisch_Nederland_Intratuin.Model {
    internal class Role {
        private static SQLDAL SqlDal = SQLDAL.Instance;

        private int id;
        private string name;
        private string key;
        private List<User> users;


        //Constructors

        /// <summary>
        /// Constructor for creating a <see cref="Role"/> from database
        /// </summary>
        /// <param name="id">ID of the role</param>
        /// <param name="name">Name of the role</param>
        /// <param name="key">Key of the role</param>
        public Role(int id, string name, string key) {
            this.id = id;
            this.name = name;
            this.key = key;
            this.users = new List<User>();
        }

        /// <summary>
        /// Constructor for creating a <see cref="Role"/> from scratch<para/>
        /// Automatically adds it to the database
        /// </summary>
        /// <param name="name">Name of the role</param>
        /// <param name="key">Key of the role</param>
        public Role(string name, string key) {
            this.name = name;
            this.key = key;
            this.users = new List<User>();

            SqlDal.AddRole(this);
        }


        //Methods

        /// <returns><see langword="List"/> of all <see cref="Role"/>s currently in the database</returns>
        public static List<Role> GetALlRoles() {
            return SqlDal.GetAllRoles();
        }

        /// <summary>Adds a <see cref="User"/> to <see cref="Role"/>'s list of users</summary>
        /// <param name="user"><see cref="User"/> to be added to <see langword="this"/> <see cref="Role"/></param>
        public void AddUser(User user) {
            if (!users.Contains(user)) {
                users.Add(user);
            }
        }


        //Getters and Setters

        public string GetName() { return name; }

        public string GetKey() { return key; }

        public void SetID(int id) { this.id = id; }
    }
}
