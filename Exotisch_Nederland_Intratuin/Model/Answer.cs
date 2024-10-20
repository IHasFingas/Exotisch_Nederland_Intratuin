using Exotisch_Nederland_Intratuin.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exotisch_Nederland_Intratuin.Model {
    internal class Answer {
        private SQLDAL SqlDal = SQLDAL.Instance;

        private int id;
        private string answerText;
        private Question question;

        public Answer(string answerText, Question question) {
            this.answerText = answerText;
            this.question = question;

            question.AddAnswer(this);
            SqlDal.AddAnswer(this);
        }

        //Getters and Setters (veranderen we private attributen naar public incl { get; set; }?
        public string GetAnswerText() { return answerText; }

        public Question GetQuestion() { return question; }

        public void SetID(int id) { this.id = id; }
    }
}
