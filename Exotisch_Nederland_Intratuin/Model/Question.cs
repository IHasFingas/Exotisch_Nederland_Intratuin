using Exotisch_Nederland_Intratuin.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exotisch_Nederland_Intratuin.Model {
    internal class Question {
        private SQLDAL SqlDal = SQLDAL.Instance;

        private int id;
        private string questionText;
        private List<Answer> answers;
        private Game game;

        public Question(string questionText, Game game) {
            this.questionText = questionText;
            this.game = game;

            game.AddQuestion(this);
            SqlDal.AddQuestion(this);
        }

        public void AddAnswer(Answer answer) {
            answers.Add(answer);
        }

        //Getters and Setters (veranderen we private attributen naar public incl { get; set; }?
        public int GetID() { return id; }

        public string GetQuestionText() { return questionText; }

        public Game GetGame() { return game; }

        public void SetID(int id) { this.id = id; }
    }
}
