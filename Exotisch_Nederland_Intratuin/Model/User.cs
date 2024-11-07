using Exotisch_Nederland_Intratuin.DAL;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Exotisch_Nederland_Intratuin.Model {
    internal class User {
        private static SQLDAL SqlDal = SQLDAL.Instance;

        private int id;
        private string name;
        private string email;
        private string password;
        private string currentLocation;
        private Route currentRoute;
        private Role activeRole;
        private List<Role> roles;
        private List<Observation> observations;
        private List<(Question, Answer)> answeredQuestions;


        //Constructor for creating an User from database
        public User(int id, string name, string email, string password, string currentLocation, Route currentRoute, List<Role> roles, List<(Question question, Answer answer)> answeredQuestions) {
            this.id = id;
            this.name = name;
            this.email = email;
            this.password = password;
            this.currentLocation = currentLocation;
            this.currentRoute = currentRoute;
            this.activeRole = null;
            this.observations = new List<Observation>();

            //Filling roles list
            this.roles = new List<Role>();
            foreach (Role role in roles) { AddRole(role, role.GetKey(), false); }

            //Filling questions list
            this.answeredQuestions = new List<(Question, Answer)>();
            foreach ((Question question, Answer answer) answeredQuestion in answeredQuestions) { AnswerQuestion(answeredQuestion, false); };

            //Tell Route this user is on it
            currentRoute.AddUser(this);
        }

        //Constructor for creating an User from scratch (automatically adds it to the database)
        public User(string name, string email, string password, string currentLocation, Route currentRoute) {
            this.name = name;
            this.email = email;
            this.password = password;
            this.currentLocation = currentLocation;
            this.currentRoute = currentRoute;
            this.activeRole = null;
            this.roles = new List<Role>();
            this.observations = new List<Observation>();
            this.answeredQuestions = new List<(Question, Answer)>();

            this.id = SqlDal.AddUser(this);

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

        public void Edit(string name, string email, string password, string currentLocation, Route currentRoute, List<Role> newRoles) {
            this.name = name;
            this.email = email;
            this.password = password;
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

        public void AddRole(Role role, string key, bool addToDB) {
            if (role.GetKey() != key) {
                return;
            }

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

            if(activeRole == role) {
                activeRole = null;
            }
        }

        public void SetActiveRole(Role role) {
            if (roles.Contains(role)) {
                activeRole = role;
            }
        }

        public void AddObservation(Observation observation) {
            if (!observations.Contains(observation)) {
                observations.Add(observation);
            }
        }

        public void EditObservation(Observation observation, string name, string location, string description, byte[] picture, Specie specie, Area area) {
            if (!observation.GetValidated() && observations.Contains(observation)) {
                observation.Edit(name, location, description, picture, specie, area, this, observation.GetSubmittedByVolunteer(), observation.GetValidated());
            }
        }

        public void ValidateObservation(Observation observation) {
            if (activeRole.GetName() == "Validator") {
                observation.Edit(observation.GetName(), observation.GetLocation(), observation.GetDescription(), observation.GetPicture(), observation.GetSpecie(), observation.GetArea(), observation.GetUser(), observation.GetSubmittedByVolunteer(), true);
            }
        }

        public void DisplayObservations() {
            foreach (Observation observation in observations) {
                Console.WriteLine(observation);
            }
        }

        public void ChangeRoute(Route route) {
            Edit(name, email, password, currentLocation, route, roles);
        }

        public void PlayGame(Game game) {
            if (!currentRoute.GetGames().Contains(game)) {
                return;
            }

            Random rng = new Random();
            List<Question> questions = game.GetQuestions().OrderBy(q => rng.Next()).ToList();

            int score = 0;

            Console.WriteLine($"Currently playing game {game.GetName()}: {game.GetDescription()}");

            for (int i = 0; i < questions.Count; i++) {
                Question question = questions[i];
                List<Answer> answers = question.GetAnswers().OrderBy(a => rng.Next()).ToList();

                Console.WriteLine($"\nQuestion {i + 1}: {question.GetQuestionText()}");

                for (int j = 0; j < answers.Count; j++) {
                    Answer answer = answers[j];

                    Console.WriteLine($"Answer {j + 1}: {answer.GetAnswerText()}");
                }

                int number;
                while (true) {
                    Console.Write("Your answer: ");
                    string input = Console.ReadLine();

                    if (int.TryParse(input, out number) && number > 0 && number <= answers.Count()) {
                        break;
                    } else {
                        Console.WriteLine("Invalid input");
                    }
                }

                Answer givenAnswer = answers[number - 1];

                if (givenAnswer.GetCorrectness()) {
                    Console.WriteLine("Correct :D");
                    score++;
                } else {
                    Console.WriteLine("Incorrect :(");

                    Answer correctAnswer = answers.Single(a => a.GetCorrectness());
                    Console.WriteLine($"Correct answer: {correctAnswer.GetAnswerText()}");
                }

                AnswerQuestion((question, givenAnswer), true);
            }

            Console.WriteLine($"You have scored {score} point(s)!");
        }

        private void AnswerQuestion((Question question, Answer answer) answeredQuestion, bool addToDB) {
            if (!answeredQuestions.Contains(answeredQuestion)) {
                answeredQuestions.Add((answeredQuestion.question, answeredQuestion.answer));

                //Tell question this user has answered it
                answeredQuestion.question.AddAnsweredBy(this);

                //Add new entry to linking table
                //Only add if user is from scratch, otherwise these entries are already in DB
                if (addToDB) {
                    SqlDal.AddUserQuestion(this, answeredQuestion);
                }
            }
        }

        public override string ToString() {
            string roleNames = string.Empty;
            foreach (Role role in roles) {
                roleNames += " " + role.GetName();
            }
            return $"User {id}: {name}, {email}, {password}, Roles ={roleNames}";
        }


        //Getters and Setters

        public int GetID() { return id; }

        public string GetName() { return name; }

        public string GetEmail() { return email; }

        public string GetPassword() { return password; }

        public string GetCurrentLocation() { return currentLocation; }

        public Route GetRoute() { return currentRoute; }

        public Role GetActiveRole() { return activeRole; }

        public List<Role> GetRoles() { return roles; }

        public List<Observation> GetObservations() { return observations; }

        public List<(Question, Answer)> GetAnsweredQuestions() { return answeredQuestions; }
    }
}