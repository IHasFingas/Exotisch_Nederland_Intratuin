using Exotisch_Nederland_Intratuin.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Exotisch_Nederland_Intratuin.Model {
    internal class Game {
        private SQLDAL SqlDal = SQLDAL.Instance;

        private int id;
        private string name;
        private string location;
        private string description;
        private Route route;
        private List<Question> questions;

        public Game(int id, string name, string location, string description, Route route, List<Question> questions) {
            this.id = id;
            this.name = name;
            this.location = location;
            this.description = description;
            this.route = route;

            this.questions = new List<Question>();
            foreach (Question question in questions) { AddQuestion(question); }

            route.AddGame(this);
        }

        public Game(string name, string location, string description, Route route, List<Question> questions) {
            this.name = name;
            this.location = location;
            this.description = description;
            this.route = route;
            foreach (Question question in questions) { AddQuestion(question); }

            route.AddGame(this);
            SqlDal.AddGame(this);
        }

        public void AddQuestion(Question question) {
            if (!questions.Contains(question)) {
                questions.Add(question);
            }
        }

        //Getters and Setters (veranderen we private attributen naar public incl { get; set; }?
        public int GetID() { return id; }

        public string GetName() { return name; }

        public string GetLocation() { return location; }

        public string GetDescription() { return description; }

        public Route GetRoute() { return route; }

        public void SetID(int id) { this.id = id; }
    }
}
