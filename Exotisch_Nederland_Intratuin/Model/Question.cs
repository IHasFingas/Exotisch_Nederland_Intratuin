using Exotisch_Nederland_Intratuin.DAL;
using System.Collections.Generic;

namespace Exotisch_Nederland_Intratuin.Model {
    internal class Question {
        private static SQLDAL SqlDal = SQLDAL.Instance;

        private int id;
        private string questionText;
        private Game game;
        private List<Answer> answers;


        //Constructor for creating a Question from database
        public Question(int id, string questionText, Game game, List<Answer> answers) {
            this.id = id;
            this.questionText = questionText;
            this.game = game;

            this.answers = new List<Answer>();
            foreach (Answer answer in answers) { AddAnswer(answer); }

            //Tell game this question belongs to it
            this.game.AddQuestion(this);
        }

        //Constructor for creating a Question from scratch (automatically adds it to the database)
        public Question(string questionText, Game game, List<Answer> answers) {
            this.questionText = questionText;
            this.game = game;

            this.answers = new List<Answer>();
            foreach (Answer answer in answers) { AddAnswer(answer); }

            //Tell game this question belongs to it
            this.game.AddQuestion(this);
            SqlDal.AddQuestion(this);
        }


        //Methods

        public static List<Question> GetAllQuestions() {
            return SqlDal.GetAllQuestions();
        }

        public void AddAnswer(Answer answer) {
            if (!answers.Contains(answer)) {
                answers.Add(answer);
            }
        }

        public void EditQuestion(string questionText, Game game) {
            this.questionText = questionText;
            this.game = game;
            SqlDal.EditQuestion(this);
        }

        public void DeleteQuestion() {
            SqlDal.DeleteQuestion(this);
        }

        public override string ToString() {
            return $"Question {id}: {questionText}, Game {game.GetID()}";
        }


        //Getters and Setters

        public int GetID() { return id; }

        public string GetQuestionText() { return questionText; }

        public Game GetGame() { return game; }

        public void SetID(int id) { this.id = id; }
    }
}