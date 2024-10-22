using Exotisch_Nederland_Intratuin.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exotisch_Nederland_Intratuin.Model
{
    internal class Route
    {
        private int id;
        private string name;
        private double length;
        private Area area;
        private List<RoutePoint> routePoints;
        private List<Game> games;

        public void EditRoute(string name, double length)
        {
            this.name = name;
            this.length = length;
            SqlDal.EditRoute(this);
        }

        public void DeleteRoute()
        {
            SqlDal.DeleteRoute(this);
        }
    }
}
