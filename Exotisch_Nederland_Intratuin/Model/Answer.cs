using Exotisch_Nederland_Intratuin.DAL;
using System.Collections.Generic;

namespace Exotisch_Nederland_Intratuin.Model {
    internal class Answer {
        private static SQLDAL SqlDal = SQLDAL.Instance;

        private int id;
        private string answerText;
        private Question question;


        //Constructors

        /// <summary>
        /// Constructor for creating an <see cref="Answer"/> from database
        /// </summary>
        /// <param name="id">ID of the answer</param>
        /// <param name="answerText">Text of the answer</param>
        /// <param name="question"><see cref="Question"/> the answer belongs to</param>
        public Answer(int id, string answerText, Question question) {
            this.id = id;
            this.answerText = answerText;
            this.question = question;

            //Tell question this answer belongs to it
            this.question.AddAnswer(this);
        }

        /// <summary>
        /// Constructor for creating a <see cref="RoutePoint"/> from scratch<para/>
        /// Automatically adds it to the database
        /// </summary>
        /// <param name="answerText">Text of the answer</param>
        /// <param name="question"><see cref="Question"/> the answer belongs to</param>
        public Answer(string answerText, Question question) {
            this.answerText = answerText;
            this.question = question;

            //Tell question this answer belongs to it
            this.question.AddAnswer(this);
            SqlDal.AddAnswer(this);
        }


        //Methods

        /// <returns><see langword="List"/> of all <see cref="Answer"/>s currently in the database</returns>
        public static List<Answer> GetAllAnswers() {
            return SqlDal.GetAllAnswers();
        }


        //Getters and Setters

        public string GetAnswerText() { return answerText; }

        public Question GetQuestion() { return question; }

        public void SetID(int id) { this.id = id; }
    }
}
