using Exotisch_Nederland_Intratuin.DAL;
using System.Collections.Generic;

namespace Exotisch_Nederland_Intratuin.Model {
    internal class Game {
        private static SQLDAL SqlDal = SQLDAL.Instance;

        private int id;
        private string name;
        private string location;
        private string description;
        private Route route;
        private List<Question> questions;


        //Constructor for creating a Game from database
        public Game(int id, string name, string location, string description, Route route) {
            this.id = id;
            this.name = name;
            this.location = location;
            this.description = description;
            this.route = route;

            this.questions = new List<Question>();

            //Tell route this game is on it
            this.route.AddGame(this);
        }

        //Constructor for creating a Game from scratch (automatically adds it to the database)
        public Game(string name, string location, string description, Route route) {
            this.name = name;
            this.location = location;
            this.description = description;
            this.route = route;

            this.id = SqlDal.AddGame(this);

            this.questions = new List<Question>();

            //Tell route this game is on it
            this.route.AddGame(this);
        }


        //Methods

        public static List<Game> GetAll() {
            return SqlDal.GetAllGames();
        }

        public static Game GetByID(int id) {
            return SqlDal.GetGameByID(id);
        }

        public void Edit(string name, string location, string description, Route route) {
            this.name = name;
            this.location = location;
            this.description = description;

            if (this.route != route) {
                this.route.RemoveGame(this);
                this.route = route;
                this.route.AddGame(this);
            }

            SqlDal.EditGame(this);
        }

        public void Delete() {
            SqlDal.DeleteGame(this);
        }

        public void AddQuestion(Question question) {
            if (!questions.Contains(question)) {
                questions.Add(question);
            }
        }

        public void RemoveQuestion(Question question) {
            if (questions.Contains(question)) {
                questions.Remove(question);
            }
        }

        public override string ToString() {
            return $"Game {id}: {name}, {description}, Route {route.GetID()}";
        }


        //Getters and Setters

        public int GetID() { return id; }

        public string GetName() { return name; }

        public string GetLocation() { return location; }

        public string GetDescription() { return description; }

        public Route GetRoute() { return route; }
    }
}