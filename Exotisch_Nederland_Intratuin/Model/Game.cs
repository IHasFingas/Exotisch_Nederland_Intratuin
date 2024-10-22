using Exotisch_Nederland_Intratuin.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exotisch_Nederland_Intratuin.Model
{
    internal class Game
    {
        private int id;
        private string name;
        private string location;
        private string description;
        private Route route;
        private List<Question> questions;

        public void EditGame(string name, string description, string location)
        {
            this.name = name;
            this.description = description;
            this.location = location;
            SqlDal.EditGame (this);
        }

        public void DeleteGame()
        {
            SqlDal.DeleteGame(this);
        }
    }
}
