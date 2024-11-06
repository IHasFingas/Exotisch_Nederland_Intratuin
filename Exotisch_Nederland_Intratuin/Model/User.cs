using Exotisch_Nederland_Intratuin.DAL;
using System.Collections.Generic;
using System.Linq;

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
        private List<Question> answeredQuestions;

        //Constructor for creating an User from database
        public User(int id, string name, string email, string currentLocation, Route currentRoute, List<Role> roles) {
            this.id = id;
            this.name = name;
            this.email = email;
            this.currentLocation = currentLocation;
            this.currentRoute = currentRoute;
            this.observations = new List<Observation>();
            this.answeredQuestions = new List<Question>();

            //Filling roles list
            this.roles = new List<Role>();
            foreach (Role role in roles) { AddRole(role, false); }

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
            this.answeredQuestions = new List<Question>();

            this.id = SqlDal.AddUser(this); //Get an ID for User so we can enter roles in UserRole linking table

            //Filling roles list
            this.roles = new List<Role>();
            foreach (Role role in roles) { AddRole(role, true); }

            //Tell Route this user is on it
            currentRoute.AddUser(this);
        }


        //Methods

        public static List<User> GetAll() {
            return SqlDal.GetAllUsers();
        }

        public static User GetByID(int id) {
            return SqlDal.GetUserByID(id);
        }

        public void Edit(string name, string email, string currentLocation, Route currentRoute, List<Role> newRoles) {
            this.name = name;
            this.email = email;
            this.currentLocation = currentLocation;

            if (this.currentRoute != currentRoute) {
                this.currentRoute.RemoveUser(this);
                this.currentRoute = currentRoute;
                this.currentRoute.AddUser(this);
            }

            foreach (Role currentRole in roles) {
                if (!newRoles.Contains(currentRole)) {
                    currentRole.RemoveUser(this);
                }
            }

            SqlDal.EditUser(this);

            this.roles = newRoles;
        }

        public void Delete() {
            SqlDal.DeleteUser(this);
        }

        public void AddRole(Role role, bool addToDB) {
            if (!roles.Contains(role)) {
                roles.Add(role);

                //Tell role this user was given the role
                role.AddUser(this);

                //Add new entry to linking table
                //Only add if user is from scratch, otherwise these entries are already in DB
                if (addToDB) {
                    SqlDal.AddUserRole(this, role);
                }
            }
        }

        public void RemoveRole(Role role) {
            if (roles.Contains(role)) {
                roles.Remove(role);
            }
        }

        public void AddObservation(Observation observation) {
            if (!observations.Contains(observation)) {
                observations.Add(observation);
            }
        }

        public void AddAnsweredQuestion(Question question) {
            if (!answeredQuestions.Contains(question)) {
                answeredQuestions.Add(question);

                //Tell question this user has answered it
                question.AddAnsweredBy(this);

                //Add new entry to linking table
                SqlDal.AddUserQuestion(this, question);
            }
        }

        public void ValidateObservation(Observation observation)
        {
            if (roles.Any(role => role.GetName() == "Validator"))
            {
                observation.Edit(observation.GetName(), observation.GetLocation(), observation.GetDescription(), observation.GetPicture(), observation.GetSpecie(), observation.GetArea(), observation.GetUser(), true);
            }
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

        public List<Question> GetAnsweredQuestions() { return answeredQuestions; }
    }
}