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

        public User(string name, string email, string currentLocation, Route currentRoute, List<Role> roles, List<Observation> observations) {
            this.name = name;
            this.email = email;
            this.currentLocation = currentLocation;
            this.currentRoute = currentRoute;
            foreach (Role role in roles) { this.roles.Add(role); }
            foreach (Observation observation in observations) { this.observations.Add(observation); }

            currentRoute.AddUser(this);
            SqlDal.AddUser(this);
        }

        public void AddObservation(Observation observation) {
            observations.Add(observation);
        }

        public void EditObservation(Observation observation) { }

        public void RemoveObservation(Observation observation) { }

        //Getters and Setters (veranderen we private attributen naar public incl { get; set; }?
        public int GetID() { return id; }

        public string GetName() { return name; }

        public string GetEmail() { return email; }

        public string GetCurrentLocation() { return currentLocation; }

        public Route GetRoute() { return currentRoute; }

        public void SetID(int id) { this.id = id; }
    }
}
