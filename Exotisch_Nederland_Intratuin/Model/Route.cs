using Exotisch_Nederland_Intratuin.DAL;
using System.Collections.Generic;

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
        public Route(string name, double length, Area area, List<RoutePoint> routePoints, List<Game> games) {
            this.name = name;
            this.length = length;
            this.area = area;
            this.users = new List<User>();

            this.routePoints = new List<RoutePoint>();
            foreach (RoutePoint routePoint in routePoints) { AddRoutePoint(routePoint, false); }

            this.games = new List<Game>();
            foreach (Game game in games) { this.games.Add(game); }

            //Tell area this route was added to it
            this.area.AddRoute(this);

            this.id = SqlDal.AddRoute(this);
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

        public override string ToString() {
            string routePointIDs = string.Empty;
            foreach (RoutePoint routePoint in routePoints) {
                routePointIDs += " " + routePoint.GetID();
            }
            return $"Route {id}: {name}, {length}km, Area {area.GetID()}, RoutePoints ={routePointIDs}";

        }


        //Getters and Setters

        public int GetID() { return id; }

        public string GetName() { return name; }

        public double GetLength() { return length; }

        public Area GetArea() { return area; }

        public List<RoutePoint> GetRoutePoints() { return routePoints; }
    }
}