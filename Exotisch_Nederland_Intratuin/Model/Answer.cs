using Exotisch_Nederland_Intratuin.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exotisch_Nederland_Intratuin.Model
{
    internal class Answer
    {
        private int id;
        private string answerText;
        private Question question;

        public void EditAnswer(string answertext)
        {
            this.answertext = answertext;
            SqlDal.EditAnswer(this);
        }

        public void DeleteAnswer()
        {
            SqlDal.DeleteAnswer(this);
        }

    }
}
