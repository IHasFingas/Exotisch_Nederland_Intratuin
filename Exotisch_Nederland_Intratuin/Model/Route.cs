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

            //Update route now that length is known
            SqlDal.EditRoute(this);

            this.games = new List<Game>();
            foreach (Game game in games) { this.games.Add(game); }

            //Tell area this route was added to it
            this.area.AddRoute(this);
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
            //Create necessary dictionaries
            Dictionary<RoutePoint, (double distance, int previousNodeID)> data = new Dictionary<RoutePoint, (double distance, int previousNodeID)> { { startNode, (0, -1) } };
            Dictionary<RoutePoint, double> unvisited = new Dictionary<RoutePoint, double>() { { startNode, 0 } };

            //Calculate fastest path to all other nodes using Dijkstra's
            data = Step(data, startNode, unvisited);

            //Check if end node is in the same area as start node
            try {
                if (!data.ContainsKey(endNode)) {
                    //If no:
                    //Throw an exception (do not fill routepoints list)
                    throw new InvalidOperationException($"Failed to create Route {id}: Start node and end node are not in the same area!");
                }
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return;
            }

            //Fill routepoints list and reverse it, so it goes start to end
            List<RoutePoint> routePoints = StepBack(data, new List<RoutePoint>(), startNode, endNode).AsEnumerable()
                                                                                                     .Reverse()
                                                                                                     .ToList();

            //Add each routepoint using method, so they get added to linking table as well
            foreach (RoutePoint routePoint in routePoints) {
                AddRoutePoint(routePoint, false);
            }

            //Set length to distance to end node
            length = data[endNode].distance;
        }

        private Dictionary<RoutePoint, (double distance, int previousNodeID)> Step(Dictionary<RoutePoint, (double distance, int previousNodeID)> data, RoutePoint currentNode, Dictionary<RoutePoint, double> unvisited) {
            
            //Check if current node hasn't already been visited
            if (!unvisited.TryGetValue(currentNode, out double d)) {
                Console.WriteLine("Accidentally visited an already visited node, returning...");
                return data;
            }

            //Mark current node as visited
            unvisited.Remove(currentNode);

            //Get all its neighbours
            Dictionary<RoutePoint, double> allNeighbours = currentNode.GetNeighbours();

            //For each neighbour:
            foreach (KeyValuePair<RoutePoint, double> neighbourData in allNeighbours) {
                //Calculate distance to it
                double distanceToNeighbour = data[currentNode].distance + neighbourData.Value;

                //Does neighbour exist in dataset?
                if (!data.ContainsKey(neighbourData.Key)) {
                    //If no:
                    //Add it to dataset
                    data.Add(neighbourData.Key, (distanceToNeighbour, currentNode.GetID()));

                    //Add it to unvisited nodes
                    unvisited.Add(neighbourData.Key, distanceToNeighbour);

                //If yes, is the route via current node shorter than currently shortest path to it?
                } else if (distanceToNeighbour < data[neighbourData.Key].distance) { 
                    //If yes:
                    //Overwrite value with new Tuple, setting distance to go via current node and setting its previous node ID to current node ID
                    data[neighbourData.Key] = (distanceToNeighbour, currentNode.GetID());
                }
            }

            //Check if all nodes have been visited
            if (unvisited.Count == 0) {
                return data;
            }

            //Determine closest unvisited node
            RoutePoint nextNode = unvisited.First().Key;
            double distanceToNextNode = Double.MaxValue;

            foreach (KeyValuePair<RoutePoint, double> unvisitedNode in unvisited) {
                if (unvisitedNode.Value < distanceToNextNode) {
                    nextNode = unvisitedNode.Key;
                    distanceToNextNode = unvisitedNode.Value;
                }
            }

            //Recur on next node
            return Step(data, nextNode, unvisited);
        }

        private List<RoutePoint> StepBack(Dictionary<RoutePoint, (double distance, int previousNodeID)> data, List<RoutePoint> routePoints, RoutePoint startNode, RoutePoint currentNode) {
            //Add current node to list of routepoints
            routePoints.Add(currentNode);

            //Check if current node is start node
            if (currentNode == startNode) {
                return routePoints;
            }

            //Initialize previous node to evaluate, and get current node's data
            RoutePoint previousNode = null;
            data.TryGetValue(currentNode, out (double distance, int previousNodeID) nodeData);

            //Determine previous node
            foreach (KeyValuePair<RoutePoint, (double distance, int previousNodeID)> candidate in data) {
                if (candidate.Key.GetID() == nodeData.previousNodeID) {
                    previousNode = candidate.Key;
                    break;
                }
            }

            //Recur on previous node
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
    }
}