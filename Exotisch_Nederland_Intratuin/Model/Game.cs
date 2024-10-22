using Exotisch_Nederland_Intratuin.DAL;
using System.Collections.Generic;

namespace Exotisch_Nederland_Intratuin.Model {
    internal class Game {
        private static SQLDAL SqlDal = SQLDAL.Instance;

        private int id;
        private string name;
        private string location; // verwijderen?
        private string description;
        private Route route;
        private List<Question> questions;


        //Constructors

        /// <summary>
        /// Constructor for creating a <see cref="Game"/> from database
        /// </summary>
        /// <param name="id">ID of the game</param>
        /// <param name="name">Name of the game</param>
        /// <param name="location">Location of the game</param>
        /// <param name="description">Description of the game</param>
        /// <param name="route"><see cref="Route"/> the game is located on</param>
        /// <param name="questions"><see langword="List"/> of <see cref="Question"/>s in the game (use empty list if there are none)</param>
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

        /// <summary>
        /// Constructor for creating a <see cref="Game"/> from scratch<para/>
        /// Automatically adds it to the database
        /// </summary>
        /// <param name="name">Name of the game</param>
        /// <param name="location">Location of the game</param>
        /// <param name="description">Description of the game</param>
        /// <param name="route"><see cref="Route"/> the game is located on</param>
        /// <param name="questions"><see langword="List"/> of <see cref="Question"/>s in the game (use empty list if there are none)</param>
        public Game(string name, string location, string description, Route route, List<Question> questions) {
            this.name = name;
            this.location = location;
            this.description = description;
            this.route = route;
            foreach (Question question in questions) { AddQuestion(question); }

            //Tell route this game is on it
            this.route.AddGame(this);
            SqlDal.AddGame(this);
        }


        //Methods

        /// <returns><see langword="List"/> of all <see cref="Game"/>s currently in the database</returns>
        public static List<Game> GetAllGames() {
            return SqlDal.GetAllGames();
        }

        /// <summary>Adds a <see cref="Question"/> to <see cref="Game"/>'s list of questions it contains</summary>
        /// <param name="question"><see cref="Question"/> to be added to <see langword="this"/> <see cref="Game"/></param>
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

        public void SetID(int id) { this.id = id; }

        public void EditGame(string name, string description, string location)
        {
            this.name = name;
            this.description = description;
            this.location = location;
            SqlDal.EditGame (this);
        }

        public void DeleteGame()
        {
            SqlDal.DeleteGame(this);
        }
    }
}
