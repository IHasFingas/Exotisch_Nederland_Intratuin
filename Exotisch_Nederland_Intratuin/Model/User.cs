using Exotisch_Nederland_Intratuin.DAL;
using System.Collections.Generic;

namespace Exotisch_Nederland_Intratuin.Model {
    internal class User {
        private static SQLDAL SqlDal = SQLDAL.Instance;

        private int id;
        private string name;
        private string email;
        private string currentLocation;
        private Route currentRoute;
        private List<Role> roles;
        private List<Observation> observations;


        //Constructor for creating an User from database
        public User(int id, string name, string email, string currentLocation, Route currentRoute, List<Role> roles) {
            this.id = id;
            this.name = name;
            this.email = email;
            this.currentLocation = currentLocation;
            this.currentRoute = currentRoute;
            this.observations = new List<Observation>();

            this.roles = new List<Role>();
            foreach (Role role in roles) { AddRole(role); }

            //Tell Route this user is on it
            currentRoute.AddUser(this);
        }

        //Constructor for creating an User from scratch (automatically adds it to the database)
        public User(string name, string email, string currentLocation, Route currentRoute, List<Role> roles) {
            this.name = name;
            this.email = email;
            this.currentLocation = currentLocation;
            this.currentRoute = currentRoute;
            this.observations = new List<Observation>();

            this.roles = new List<Role>();
            foreach (Role role in roles) { AddRole(role); }

            //Tell Route this user is on it
            currentRoute.AddUser(this);
            SqlDal.AddUser(this);
        }


        //Methods

        public static List<User> GetAllUsers() {
            return SqlDal.GetAllUsers();
        }

        public void AddRole(Role role) {
            if (!roles.Contains(role)) {
                roles.Add(role);

                //Tell role this user was given the role
                role.AddUser(this);

                //Add new entry to linking table
                SqlDal.AddUserRole(this, role);
            }
        }

        public void AddObservation(Observation observation) {
            if (!observations.Contains(observation)) {
                observations.Add(observation);
            }
        }

        public void EditUser(string name, string email, string currentLocation, Route currentRoute, List<Role> roles) {
            this.name = name;
            this.email = email;
            this.currentLocation = currentLocation;
            this.currentRoute = currentRoute;
            this.roles = roles;
            SqlDal.EditUser(this);
        }

        public void RemoveUser() {
            SqlDal.DeleteUser(this);
        }

        public override string ToString() {
            string roleNames = string.Empty;
            foreach (Role role in roles) {
                roleNames += " " + role.GetName();
            }
            return $"User {id}: {name}, {email}, Roles ={roleNames}";
        }


        //Getters and Setters

        public int GetID() { return id; }

        public string GetName() { return name; }

        public string GetEmail() { return email; }

        public string GetCurrentLocation() { return currentLocation; }

        public Route GetRoute() { return currentRoute; }

        public List<Role> GetRoles() { return roles; }

        public void SetID(int id) { this.id = id; }
    }
}