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
        public Game(int id, string name, string location, string description, Route route, List<Question> questions) {
            this.id = id;
            this.name = name;
            this.location = location;
            this.description = description;
            this.route = route;

            this.questions = new List<Question>();
            foreach (Question question in questions) { AddQuestion(question); }

            //Tell route this game is on it
            this.route.AddGame(this);
        }

        //Constructor for creating a Game from scratch (automatically adds it to the database)
        public Game(string name, string location, string description, Route route, List<Question> questions) {
            this.name = name;
            this.location = location;
            this.description = description;
            this.route = route;
            foreach (Question question in questions) { AddQuestion(question); }

            //Tell route this game is on it
            this.route.AddGame(this);

            this.id = SqlDal.AddGame(this);
        }


        //Methods

        public static List<Game> GetAllGames() {
            return SqlDal.GetAllGames();
        }

        public static Game GetGameByID(int id) {
            return SqlDal.GetGameByID(id);
        }

        public void EditGame(string name, string location, string description) {
            this.name = name;
            this.location = location;
            this.description = description;
            SqlDal.EditGame(this);
        }

        public void DeleteGame() {
            SqlDal.DeleteGame(this);
        }

        public void AddQuestion(Question question) {
            if (!questions.Contains(question)) {
                questions.Add(question);
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