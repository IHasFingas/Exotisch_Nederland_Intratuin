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

            this.id = SqlDal.AddRoutePoint(this); //Get an ID for RoutePoint so we can enter RoutePoints in RoutePointRoutePoint linking table

            this.neighbours = new Dictionary<RoutePoint, double>();
            foreach (KeyValuePair<RoutePoint, double> neighbour in neighbours) { AddNeighbour(neighbour.Key, neighbour.Value, true); }
        }


        //Methods

        public static List<RoutePoint> GetAll() {
            return SqlDal.GetAllRoutePoints();
        }

        public static RoutePoint GetByID(int id) {
            return SqlDal.GetRoutePointByID(id);
        }

        public void Edit(string name, string location, Dictionary<RoutePoint, double> newNeighbours) {
            this.name = name;
            this.location = location;

            foreach (RoutePoint currentNeighbour in neighbours.Keys) {
                if (!newNeighbours.ContainsKey(currentNeighbour)) {
                    currentNeighbour.RemoveNeighbour(this);
                }
            }
            this.neighbours = newNeighbours;

            SqlDal.EditRoutePoint(this);
        }

        public void Delete() {
            SqlDal.DeleteRoutePoint(this);
        }

        public void AddRoute(Route route) {
            if (!routes.Contains(route)) {
                routes.Add(route);
            }
        }

        public void RemoveRoute(Route route) {
            if (routes.Contains(route)) {
                routes.Remove(route);
            }
        }

        public void AddPointOfInterest(POI point) {
            if (!pointsOfInterest.Contains(point)) {
                pointsOfInterest.Add(point);
            }
        }

        public void RemovePointOfInterest(POI point) {
            if (pointsOfInterest.Contains(point)) {
                pointsOfInterest.Remove(point);
            }
        }

        public void AddNeighbour(RoutePoint neighbour, double distance, bool addToDB) {
            if (!neighbours.ContainsKey(neighbour)) {
                neighbours.Add(neighbour, distance);

                //Add new entry to linking table
                //Only add if RoutePoint is from scratch, otherwise these entries are already in DB
                if (addToDB) {
                    SqlDal.AddRoutePointRoutePoint(this, neighbour, distance);
                }

                //Tell routepoint they are neighbours
                neighbour.AddNeighbour(this, distance, addToDB);
            }
        }

        public void RemoveNeighbour(RoutePoint neighbour) {
            if (neighbours.ContainsKey(neighbour)) {
                neighbours.Remove(neighbour);
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

        public Dictionary<RoutePoint, double> GetNeighbours() { return neighbours; }
    }
}