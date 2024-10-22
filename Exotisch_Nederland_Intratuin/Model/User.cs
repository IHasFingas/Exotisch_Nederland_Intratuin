using Exotisch_Nederland_Intratuin.DAL;
using System;
using System.Collections.Generic;

namespace Exotisch_Nederland_Intratuin.Model {
    internal class User {
        private static SQLDAL SqlDal = SQLDAL.Instance;

        private int id;
        private string name;
        private string email;
        private string currentLocation;
        private Route currentRoute;
        private List<Observation> observations;
        private List<Role> roles;


        //Constructors

        /// <summary>
        /// Constructor for creating a <see cref="User"/> from database
        /// </summary>
        /// <param name="id">ID of the user</param>
        /// <param name="name">Name of the user</param>
        /// <param name="email">Email of the game</param>
        /// <param name="currentLocation">Current location of the user</param>
        /// <param name="currentRoute"><see cref="Route"/> the user is on</param>
        /// <param name="roles"><see langword="List"/> of <see cref="Role"/>s the user has (use empty list if there are none)</param>
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

        /// <summary>
        /// Constructor for creating a <see cref="User"/> from database
        /// </summary>
        /// <param name="name">Name of the user</param>
        /// <param name="email">Email of the game</param>
        /// <param name="currentLocation">Current location of the user</param>
        /// <param name="currentRoute"><see cref="Route"/> the user is on</param>
        /// <param name="roles"><see langword="List"/> of <see cref="Role"/>s the user has (use empty list if there are none)</param>
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

        /// <returns><see langword="List"/> of all <see cref="User"/>s currently in the database</returns>
        public static List<User> GetAllUsers() {
            return SqlDal.GetAllUsers();
        }

        /// <summary>Adds a <see cref="Role"/> to <see cref="User"/>'s list of roles it contains</summary>
        /// <param name="role"><see cref="Role"/> to be added to <see langword="this"/> <see cref="User"/></param>
        public void AddRole(Role role) {
            if (!roles.Contains(role)) {
                roles.Add(role);

                //Tell role this user was given the role
                role.AddUser(this);
            }
        }

        /// <summary>Adds a <see cref="Observation"/> to <see cref="User"/>'s list of observations it has made</summary>
        /// <param name="observation"><see cref="Observation"/> to be added to <see langword="this"/> <see cref="User"/></param>
        public void AddObservation(Observation observation) {
            if (!observations.Contains(observation)) {
                observations.Add(observation);
            }
        }

        public override string ToString() {
            string roleNames = string.Empty;
            foreach (Role role in roles) {
                roleNames += " " + role.GetName();
            }
            return $"User {id}: {name}, {email}, Roles ={roleNames}";
        }

        //public void EditObservation(Observation observation) {
        //    //Check of deze observation in user's list zit
        //    //zo ja, edit dit observation object aan de hand van setters op observation's attributes
        //    //update deze observation in de tabel (UpdateObservation() in SQLDAL)
        //}

        //public void RemoveObservation(Observation observation) {
        //    //Check of deze observation in user's list zit
        //    //zo ja, verwijder deze uit de lijst
        //    //verwijder deze observation in de tabel (DeleteObservation() in SQLDAL)
        //}

        //Getters and Setters
        public int GetID() { return id; }

        public string GetName() { return name; }

        public string GetEmail() { return email; }

        public string GetCurrentLocation() { return currentLocation; }

        public Route GetRoute() { return currentRoute; }

        public void SetID(int id) { this.id = id; }
    }
}
