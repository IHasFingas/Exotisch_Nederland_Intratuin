using Exotisch_Nederland_Intratuin.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exotisch_Nederland_Intratuin.Model {
    internal class RoutePoint {
        private SQLDAL SqlDal = SQLDAL.Instance;

        private int id;
        private string name;
        private string location;
        private List<Route> routes;
        private List<POI> pointsOfInterest;

        public RoutePoint(string name, string location, List<POI> pointsOfInterest) {
            this.name = name;
            this.location = location;
            foreach (POI point in pointsOfInterest) { AddPointOfInterest(point); }

            SqlDal.AddRoutePoint(this);
        }

        public void AddPointOfInterest(POI point) {
            pointsOfInterest.Add(point);
            point.SetRoutePoint(this);
        }

        public void AddRoute(Route route) {
            routes.Add(route);
        }

        //Getters and Setters (veranderen we private attributen naar public incl { get; set; }?
        public int GetID() { return id; }

        public string GetName() { return name; }

        public string GetLocation() { return location; }

        public void SetID(int id) { this.id = id; }
    }
}

