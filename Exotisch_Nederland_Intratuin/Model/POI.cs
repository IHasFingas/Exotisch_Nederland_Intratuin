using Exotisch_Nederland_Intratuin.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exotisch_Nederland_Intratuin.Model
{
    internal class POI
    {
        private int id;
        private string name;
        private string location;
        private RoutePoint routePoint;

        public void EditArea(string name, string location)
        {
            this.name = name;
            this.location = location;
            SqlDal.EditPOI(this);
        }

        public void DeletePOI()
        {
            SqlDal.DeletePOI(this);
        }
    }
}
