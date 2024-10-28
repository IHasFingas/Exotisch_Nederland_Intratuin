using Exotisch_Nederland_Intratuin.DAL;
using System.Collections.Generic;

namespace Exotisch_Nederland_Intratuin.Model {
    internal class RoutePoint {
        private static SQLDAL SqlDal = SQLDAL.Instance;

        private int id;
        private string name;
        private string location;
        private List<Route> routes;
        private List<POI> pointsOfInterest;
        private Dictionary<RoutePoint, double> neighbours;


        //Constructor for creating a RoutePoint from database
        public RoutePoint(int id, string name, string location) {
            this.id = id;
            this.name = name;
            this.location = location;
            this.routes = new List<Route>();
            this.pointsOfInterest = new List<POI>();
            this.neighbours = new Dictionary<RoutePoint, double>();
        }

        //Constructor for creating a RoutePoint from scratch (automatically adds it to the database)
        public RoutePoint(string name, string location, Dictionary<RoutePoint, double> neighbours) {
            this.name = name;
            this.location = location;
            this.routes = new List<Route>();
            this.pointsOfInterest = new List<POI>();

            this.neighbours = new Dictionary<RoutePoint, double>();
            foreach (RoutePoint routePoint in neighbours.Keys) { AddNeighbour(routePoint, neighbours[routePoint], false); }

            SqlDal.AddRoutePoint(this);
        }


        //Methods

        public static List<RoutePoint> GetAllRoutePoints() {
            return SqlDal.GetAllRoutePoints();
        }

        public static RoutePoint GetRoutePointByID(int id) {
            return SqlDal.GetRoutePointByID(id);
        }

        public void EditRoutePoint(string name, string location, Dictionary<RoutePoint, double> neighbours) {
            this.name = name;
            this.location = location;
            this.neighbours = neighbours;
            SqlDal.EditRoutePoint(this);
        }

        public void DeleteRoutePoint() {
            SqlDal.DeleteRoutePoint(this);
        }

        public void AddRoute(Route route) {
            if (!routes.Contains(route)) {
                routes.Add(route);
            }
        }

        public void AddPointOfInterest(POI point) {
            if (!pointsOfInterest.Contains(point)) {
                pointsOfInterest.Add(point);
            }
        }

        public void AddNeighbour(RoutePoint routePoint, double distance, bool fromDatabase) {
            if (!neighbours.ContainsKey(routePoint)) {
                neighbours.Add(routePoint, distance);

                //Tell routepoint they are neighbours
                routePoint.AddNeighbour(this, distance, fromDatabase);

                //Add new entry to linking table
                //Only add if RoutePoint is from scratch, otherwise these entries are already in DB
                if (!fromDatabase) {
                    SqlDal.AddRoutePointRoutePoint(this, routePoint, distance);
                }
            }
        }

        public override string ToString() {
            string neighbourIDs = string.Empty;
            foreach (RoutePoint neighbour in neighbours.Keys) {
                neighbourIDs += " " + neighbour.GetID();
            }
            return $"RoutePoint {id}: {name}, {location}, neighbours ={neighbourIDs}";
        }


        //Getters and Setter

        public int GetID() { return id; }

        public string GetName() { return name; }

        public string GetLocation() { return location; }

        public List<Route> GetRoutes() { return routes; }

        public Dictionary<RoutePoint, double> GetNeighbours() { return neighbours; }

        public void SetID(int id) { this.id = id; }
    }
}