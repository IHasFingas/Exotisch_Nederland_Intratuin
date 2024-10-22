using Exotisch_Nederland_Intratuin.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exotisch_Nederland_Intratuin.Model
{
    internal class Area
    {
        private static SQLDAL SqlDal = SQLDAL.Instance;

        private int id;
        private string name;
        private double size;
        private List<Route> routes;
        private List<Observation> observations;

        public void EditArea(string name, double size)
        {
            this.name = name;
            this.size = size;
            SqlDal.EditArea(this);
        }

        public void DeleteArea()
        {
            SqlDal.DeleteArea(this);
        }
    }
}


