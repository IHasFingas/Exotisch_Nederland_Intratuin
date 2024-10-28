using Exotisch_Nederland_Intratuin.DAL;
using System;
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
        public RoutePoint(int id, string name, string location, Dictionary<RoutePoint, double> neighbours) {
            this.id = id;
            this.name = name;
            this.location = location;
            this.routes = new List<Route>();
            this.pointsOfInterest = new List<POI>();
            this.neighbours = neighbours;
        }

        //Constructor for creating a RoutePoint from scratch (automatically adds it to the database)
        public RoutePoint(string name, string location) {
            this.name = name;
            this.location = location;
            this.routes = new List<Route>();
            this.pointsOfInterest = new List<POI>();
            this.neighbours = new Dictionary<RoutePoint, double>();

            SqlDal.AddRoutePoint(this);
        }


        //Methods

        public static List<RoutePoint> GetAllRoutePoints() {
            return SqlDal.GetAllRoutePoints();
        }

        public static RoutePoint GetRoutePointByID(int id) {
            return SqlDal.GetRoutePointByID(id);
        }

        public void EditRoutePoint(string name, string location) {
            this.name = name;
            this.location = location;
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

        public void AddNeighbour(RoutePoint routePoint, double distance) {
            if (!neighbours.ContainsKey(routePoint)) {
                neighbours.Add(routePoint, distance);
            }
        }

        public override string ToString() {
            return $"RoutePoint {id}: {name}, {location}";
        }


        //Getters and Setter

        public int GetID() { return id; }

        public string GetName() { return name; }

        public string GetLocation() { return location; }

        public void SetID(int id) { this.id = id; }
    }
}