using Exotisch_Nederland_Intratuin.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Exotisch_Nederland_Intratuin.Model {
    internal class Route {
        private static SQLDAL SqlDal = SQLDAL.Instance;

        private int id;
        private string name;
        private double length;
        private Area area;
        private List<RoutePoint> routePoints;
        private List<User> users;
        private List<Game> games;


        //Constructor for creating a Route from database
        public Route(int id, string name, double length, Area area, List<RoutePoint> routePoints, List<Game> games) {
            this.id = id;
            this.name = name;
            this.length = length;
            this.area = area;
            this.users = new List<User>();

            this.routePoints = new List<RoutePoint>();
            foreach (RoutePoint routePoint in routePoints) { AddRoutePoint(routePoint, true); }

            this.games = new List<Game>();
            foreach (Game game in games) { this.games.Add(game); }

            //Tell area this route was added to it
            this.area.AddRoute(this);
        }

        //Constructor for creating a Route from scratch (automatically adds it to the database)
        public Route(string name, Area area, RoutePoint startPoint, RoutePoint endPoint, List<Game> games) {
            this.name = name;
            this.area = area;

            this.id = SqlDal.AddRoute(this);

            this.routePoints = new List<RoutePoint>();
            FindRoute(startPoint, endPoint);

            this.games = new List<Game>();
            foreach (Game game in games) { this.games.Add(game); }

            //Tell area this route was added to it
            this.area.AddRoute(this);

            SqlDal.EditRoute(this);
        }


        //Methods

        public static List<Route> GetAllRoutes() {
            return SqlDal.GetAllRoutes();
        }

        public static Route GetRouteByID(int id) {
            return SqlDal.GetRouteByID(id);
        }

        public void EditRoute(string name, double length, Area area, List<RoutePoint> routePoints) {
            this.name = name;
            this.length = length;
            this.area = area;
            this.routePoints = routePoints;
            SqlDal.EditRoute(this);
        }

        public void DeleteRoute() {
            SqlDal.DeleteRoute(this);
        }

        public void AddRoutePoint(RoutePoint routePoint, bool fromDatabase) {
            if (!routePoints.Contains(routePoint)) {
                routePoints.Add(routePoint);

                //Tell routepoint it is used in this route
                routePoint.AddRoute(this);

                //Add new entry to linking table
                //Only add if route is from scratch, otherwise these entries are already in DB
                if (!fromDatabase) {
                    SqlDal.AddRouteRoutePoint(this, routePoint);
                }
            }
        }

        public void AddGame(Game game) {
            if (!games.Contains(game)) {
                games.Add(game);
            }
        }

        public void AddUser(User user) {
            if (!users.Contains(user)) {
                users.Add(user);
            }
        }

        private void FindRoute(RoutePoint startNode, RoutePoint endNode) {
            Dictionary<RoutePoint, Tuple<double, int>> data = new Dictionary<RoutePoint, Tuple<double, int>> {
                { startNode, new Tuple<double, int>(0, -1) }
            };

            Dictionary<RoutePoint, double> unvisited = new Dictionary<RoutePoint, double>() {
                { startNode, 0 }
            };

            data = Step(data, startNode, unvisited);


            try {
                if (!data.ContainsKey(endNode)) {
                    throw new InvalidOperationException($"Failed to create Route {id}: Start node and end node are not in the same area!");
                }
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return;
            }

            List<RoutePoint> routePoints = StepBack(data, new List<RoutePoint>(), startNode, endNode).AsEnumerable().Reverse().ToList();

            foreach (RoutePoint routePoint in routePoints) {
                AddRoutePoint(routePoint, false);
            }

            length = data[endNode].Item1;
        }

        private Dictionary<RoutePoint, Tuple<double, int>> Step(Dictionary<RoutePoint, Tuple<double, int>> data, RoutePoint currentNode, Dictionary<RoutePoint, double> unvisited) {
            if (!unvisited.TryGetValue(currentNode, out double d)) {
                Console.WriteLine("Accidentally visited an already visited node, returning...");
                return data;
            }

            unvisited.Remove(currentNode);

            Dictionary<RoutePoint, double> allNeighbours = currentNode.GetNeighbours(); //Get all its neighbours

            foreach (KeyValuePair<RoutePoint, double> neighbourData in allNeighbours) { //For each neighbour:

                if (!data.ContainsKey(neighbourData.Key)) { //Does neighbour exist in data?

                    data.Add(neighbourData.Key, new Tuple<double, int>(data[currentNode].Item1 + neighbourData.Value, currentNode.GetID())); //If no, add it
                    unvisited.Add(neighbourData.Key, data[currentNode].Item1 + neighbourData.Value); //Add neighbour to unvisited nodes

                } else if (data[currentNode].Item1 + neighbourData.Value < data[neighbourData.Key].Item1) { //If yes, is the route via this huidigeKnoop shorter than current route to neighbour?

                    data.TryGetValue(neighbourData.Key, out Tuple<double, int> oldData); //If yes, get old data
                    data[neighbourData.Key] = new Tuple<double, int>(data[currentNode].Item1 + neighbourData.Value, currentNode.GetID()); //Insert new Tuple as new value

                }
            }

            if (unvisited.Count == 0) {
                return data;
            }

            RoutePoint nextNode = unvisited.First().Key;
            double distance = Double.MaxValue;

            foreach (KeyValuePair<RoutePoint, double> unvisitedNode in unvisited) {
                if (unvisitedNode.Value < distance) {
                    nextNode = unvisitedNode.Key;
                    distance = unvisitedNode.Value;
                }
            }

            return Step(data, nextNode, unvisited);
        }

        private List<RoutePoint> StepBack(Dictionary<RoutePoint, Tuple<double, int>> data, List<RoutePoint> routePoints, RoutePoint startNode, RoutePoint currentNode) {
            routePoints.Add(currentNode);

            if (currentNode == startNode) {
                return routePoints;
            }

            RoutePoint previousNode = null;

            data.TryGetValue(currentNode, out Tuple<double, int> nodeData);

            foreach (KeyValuePair<RoutePoint, Tuple<double, int>> candidate in data) {
                if (candidate.Key.GetID() == nodeData.Item2) {
                    previousNode = candidate.Key;
                    break;
                }
            }

            return StepBack(data, routePoints, startNode, previousNode); ;
        }

        public override string ToString() {
            string routePointIDs = string.Empty;
            foreach (RoutePoint routePoint in routePoints) {
                routePointIDs += " " + routePoint.GetID();
            }
            return $"Route {id}: {name}, {length} km, Area {area.GetID()}, RoutePoints ={routePointIDs}";

        }


        //Getters and Setters

        public int GetID() { return id; }

        public string GetName() { return name; }

        public double GetLength() { return length; }

        public Area GetArea() { return area; }

        public List<RoutePoint> GetRoutePoints() { return routePoints; }

        public void SetID(int id) { this.id = id; }
    }
}