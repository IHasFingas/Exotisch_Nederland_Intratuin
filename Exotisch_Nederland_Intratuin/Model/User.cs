using Exotisch_Nederland_Intratuin.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exotisch_Nederland_Intratuin.Model {
    internal class User {
        private SQLDAL SqlDal = SQLDAL.Instance;

        private int id;
        private string name;
        private string email;
        private string currentLocation;
        private Route currentRoute;
        private List<Role> roles;
        private List<Observation> observations;

        public User(string name, string email, string currentLocation, Route currentRoute, List<Role> roles) {
            this.name = name;
            this.email = email;
            this.currentLocation = currentLocation;
            this.currentRoute = currentRoute;
            foreach (Role role in roles) { AddRole(role); }
            this.observations = new List<Observation>();

            currentRoute.AddUser(this);
            SqlDal.AddUser(this);
        }

        public void AddRole(Role role) {
            if (!roles.Contains(role)) {
                roles.Add(role);
                role.AddUser(this);
            }
        }

        public void AddObservation(Observation observation) {
            if (!observations.Contains(observation)) {
                observations.Add(observation);
            }
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

        //Getters and Setters (veranderen we private attributen naar public incl { get; set; }?
        public int GetID() { return id; }

        public string GetName() { return name; }

        public string GetEmail() { return email; }

        public string GetCurrentLocation() { return currentLocation; }

        public Route GetRoute() { return currentRoute; }

        public void SetID(int id) { this.id = id; }
    }
}
