using Exotisch_Nederland_Intratuin.DAL;
using System.Collections.Generic;

namespace Exotisch_Nederland_Intratuin.Model {
    internal class Question {
        private static SQLDAL SqlDal = SQLDAL.Instance;

        private int id;
        private string questionText;
        private Game game;
        private List<Answer> answers;
        private List<User> answeredBy;


        //Constructor for creating a Question from database
        public Question(int id, string questionText, Game game) {
            this.id = id;
            this.questionText = questionText;
            this.game = game;

            this.answers = new List<Answer>();

            this.answeredBy = new List<User>();

            //Tell game this question belongs to it
            this.game.AddQuestion(this);
        }

        //Constructor for creating a Question from scratch (automatically adds it to the database)
        public Question(string questionText, Game game) {
            this.questionText = questionText;
            this.game = game;

            this.id = SqlDal.AddQuestion(this);

            this.answers = new List<Answer>();

            this.answeredBy = new List<User>();

            //Tell game this question belongs to it
            this.game.AddQuestion(this);
        }


        //Methods

        public static List<Question> GetAll() {
            return SqlDal.GetAllQuestions();
        }

        public static Question GetByID(int id) {
            return SqlDal.GetQuestionByID(id);
        }

        public void Edit(string questionText, Game game) {
            this.questionText = questionText;

            if (this.game != game) {
                this.game.RemoveQuestion(this);
                this.game = game;
                this.game.AddQuestion(this);
            }

            SqlDal.EditQuestion(this);
        }

        public void Delete() {
            SqlDal.DeleteQuestion(this);
        }

        public void AddAnswer(Answer answer) {
            if (!answers.Contains(answer)) {
                answers.Add(answer);
            }
        }

        public void RemoveAnswer(Answer answer) {
            if (answers.Contains(answer)) {
                answers.Remove(answer);
            }
        }

        public void AddAnsweredBy(User user) {
            if (!answeredBy.Contains(user)) {
                answeredBy.Add(user);
            }
        }

        public void RemoveAnsweredBy(User user) {
            if (answeredBy.Contains(user)) {
                answeredBy.Remove(user);
            }
        }

        public override string ToString() {
            return $"Question {id}: {questionText}, Game {game.GetID()}";
        }


        //Getters and Setters

        public int GetID() { return id; }

        public string GetQuestionText() { return questionText; }

        public Game GetGame() { return game; }

        public List<Answer> GetAnswers() { return answers; }
    }
}