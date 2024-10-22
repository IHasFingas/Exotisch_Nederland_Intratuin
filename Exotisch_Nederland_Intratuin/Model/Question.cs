using Exotisch_Nederland_Intratuin.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exotisch_Nederland_Intratuin.Model
{
    internal class Question
    {
        private int id;
        private string questionText;
        private List<Answer> answers;
        private Game game;

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

