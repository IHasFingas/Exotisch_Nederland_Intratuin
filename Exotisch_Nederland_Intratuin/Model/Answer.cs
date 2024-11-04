using Exotisch_Nederland_Intratuin.DAL;
using System.Collections.Generic;

namespace Exotisch_Nederland_Intratuin.Model {
    internal class Answer {
        private static SQLDAL SqlDal = SQLDAL.Instance;

        private int id;
        private string answerText;
        private Question question;
        private bool isCorrect;


        //Constructor for creating an Answer from database
        public Answer(int id, string answerText, Question question, bool correctAnswer) {
            this.id = id;
            this.answerText = answerText;
            this.question = question;
            this.isCorrect = correctAnswer;

            //Tell question this answer belongs to it
            this.question.AddAnswer(this);
        }

        //Constructor for creating an Answer from scratch (automatically adds it to the database)
        public Answer(string answerText, Question question, bool correctAnswer) {
            this.answerText = answerText;
            this.question = question;
            this.isCorrect = correctAnswer;

            this.id = SqlDal.AddAnswer(this);

            //Tell question this answer belongs to it
            this.question.AddAnswer(this);
        }


        //Methods

        public static List<Answer> GetAll() {
            return SqlDal.GetAllAnswers();
        }

        public static Answer GetByID(int id) {
            return SqlDal.GetAnswerByID(id);
        }

        public void Edit(string answerText, Question question, bool correctAnswer) {
            this.answerText = answerText;

            if (this.question != question) {
                this.question.RemoveAnswer(this);
                this.question = question;
                this.question.AddAnswer(this);
            }

            this.isCorrect = correctAnswer;

            SqlDal.EditAnswer(this);
        }

        public void Delete() {
            SqlDal.DeleteAnswer(this);
        }

        public override string ToString() {
            return $"Answer {id}: {answerText}, Question {question.GetID()}, Correct: {isCorrect}";
        }


        //Getters and Setters

        public int GetID() { return id; }

        public string GetAnswerText() { return answerText; }

        public Question GetQuestion() { return question; }

        public bool GetCorrectness() { return isCorrect; }
    }
}