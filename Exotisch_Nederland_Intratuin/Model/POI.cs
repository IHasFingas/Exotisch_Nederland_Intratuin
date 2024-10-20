using Exotisch_Nederland_Intratuin.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exotisch_Nederland_Intratuin.Model {
    internal class POI {
        private SQLDAL SqlDal = SQLDAL.Instance;

        private int id;
        private string name;
        private string location;
        private RoutePoint routePoint;

        public POI(string name, string location, RoutePoint routePoint) {
            this.name = name;
            this.location = location;
            this.routePoint = routePoint;

            SqlDal.AddPOI(this);
        }

        //Getters and Setters (veranderen we private attributen naar public incl { get; set; }?
        public string GetName() { return name; }

        public string GetLocation() { return location; }

        public RoutePoint GetRoutePoint() { return routePoint; }

        public void SetID(int id) { this.id = id; }

        public void SetRoutePoint(RoutePoint routePoint) { this.routePoint = routePoint; }
    }
}
