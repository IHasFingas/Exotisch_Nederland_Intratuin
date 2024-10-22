using Exotisch_Nederland_Intratuin.DAL;
using System.Collections.Generic;

namespace Exotisch_Nederland_Intratuin.Model {
    internal class Question {
        private static SQLDAL SqlDal = SQLDAL.Instance;

        private int id;
        private string questionText;
        private Game game;
        private List<Answer> answers;


        //Constructors

        /// <summary>
        /// Constructor for creating a <see cref="Question"/> from database
        /// </summary>
        /// <param name="id">ID of the question</param>
        /// <param name="questionText">Text of the question</param>
        /// <param name="game"><see cref="Game"/> the question is part of</param>
        /// <param name="answers"><see cref="Answer"/>s beloning to the question (use empty list if there are none)</param>
        public Question(int id, string questionText, Game game, List<Answer> answers) {
            this.id = id;
            this.questionText = questionText;
            this.game = game;

            this.answers = new List<Answer>();
            foreach (Answer answer in answers) { AddAnswer(answer); }

            //Tell game this question belongs to it
            this.game.AddQuestion(this);
        }

        /// <summary>
        /// Constructor for creating a <see cref="RoutePoint"/> from scratch<para/>
        /// Automatically adds it to the database
        /// </summary>
        /// <param name="questionText">Text of the question</param>
        /// <param name="game"><see cref="Game"/> the question is part of</param>
        /// <param name="answers"><see cref="Answer"/>s beloning to the question (use empty list if there are none)</param>
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

        /// <returns><see langword="List"/> of all <see cref="Question"/>s currently in the database</returns>
        public static List<Question> GetAllQuestions() {
            return SqlDal.GetAllQuestions();
        }

        /// <summary>Adds an <see cref="Answer"/> to <see cref="Question"/>'s list of possible answers</summary>
        /// <param name="answer"><see cref="Answer"/> to be added to <see langword="this"/> <see cref="Question"/></param>
        public void AddAnswer(Answer answer) {
            if (!answers.Contains(answer)) {
                answers.Add(answer);
            }
        }

        public override string ToString() {
            return $"Question {id}: {questionText}, Game {game.GetID()}";
        }


        //Getters and Setters

        public int GetID() { return id; }

        public string GetQuestionText() { return questionText; }

        public Game GetGame() { return game; }

        public void SetID(int id) { this.id = id; }
        public void EditQuestion(string questionText)
        {
            this.questionText = questionText;
            SqlDal.EditQuestion(this);
        }

        public void DeleteQuestion()
        {
            SqlDal.DeleteQuestion(this);
        }
    }
}

