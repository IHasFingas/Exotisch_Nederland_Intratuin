using Exotisch_Nederland_Intratuin.DAL;
using System.Collections.Generic;

namespace Exotisch_Nederland_Intratuin.Model {
    internal class Route {
        private static SQLDAL SqlDal = SQLDAL.Instance;

        private int id;
        private string name;
        private double length;
        private Area area;
        private List<User> users;
        private List<RoutePoint> routePoints;
        private List<Game> games;


        //Constructors

        /// <summary>
        /// Constructor for creating a <see cref="Route"/> from database
        /// </summary>
        /// <param name="id">ID of the route</param>
        /// <param name="name">Name of the route</param>
        /// <param name="length">Length of the route</param>
        /// <param name="area"><see cref="Area"/> the route is in</param>
        /// <param name="routePoints"><see cref="RoutePoint"/>s in the route (use empty list if there are none)</param>
        /// <param name="games"><see cref="Game"/>s in the route (use empty list if there are none)</param>
        public Route(int id, string name, double length, Area area, List<RoutePoint> routePoints, List<Game> games) {
            this.id = id;
            this.name = name;
            this.length = length;
            this.area = area;
            this.users = new List<User>();

            this.routePoints = new List<RoutePoint>();
            foreach (RoutePoint routePoint in routePoints) { AddRoutePoint(routePoint); }

            this.games = new List<Game>();
            foreach (Game game in games) { this.games.Add(game); }

            //Tell area this route was added to it
            this.area.AddRoute(this);
        }

        /// <summary>
        /// Constructor for creating an <see cref="Route"/> from scratch<para/>
        /// Automatically adds it to the database
        /// </summary>
        /// <param name="name">Name of the route</param>
        /// <param name="length">Length of the route</param>
        /// <param name="area"><see cref="Area"/> the route is in</param>
        /// <param name="routePoints"><see cref="RoutePoint"/>s in the route (use empty list if there are none)</param>
        /// <param name="games"><see cref="Game"/>s in the route (use empty list if there are none)</param>
        public Route(string name, double length, Area area, List<RoutePoint> routePoints, List<Game> games) {
            this.name = name;
            this.length = length;
            this.area = area;
            this.users = new List<User>();

            this.routePoints = new List<RoutePoint>();
            foreach (RoutePoint routePoint in routePoints) { AddRoutePoint(routePoint); }

            this.games = new List<Game>();
            foreach (Game game in games) { this.games.Add(game); }

            //Tell area this route was added to it
            this.area.AddRoute(this);
            SqlDal.AddRoute(this);
        }


        //Methods

        /// <returns><see langword="List"/> of all <see cref="Route"/>s currently in the database</returns>
        public static List<Route> GetAllRoutes() {
            return SqlDal.GetAllRoutes();
        }

        /// <summary>Adds a <see cref="RoutePoint"/> to <see cref="Route"/>'s list of routepoints it uses</summary>
        /// <param name="routePoint"><see cref="RoutePoint"/> to be added to <see langword="this"/> <see cref="Route"/></param>
        public void AddRoutePoint(RoutePoint routePoint) {
            if (!routePoints.Contains(routePoint)) {
                routePoints.Add(routePoint);

                //Tell routepoint it is used in this route
                routePoint.AddRoute(this);

                //Add new entry to linking table
                SqlDal.AddRouteRoutePoint(this, routePoint);
            }
        }

        /// <summary>Adds a <see cref="Game"/> to <see cref="Route"/>'s list of games it contains</summary>
        /// <param name="game"><see cref="Game"/> to be added to <see langword="this"/> <see cref="Route"/></param>
        public void AddGame(Game game) {
            if (!games.Contains(game)) {
                games.Add(game);
            }
        }

        /// <summary>Adds a <see cref="User"/> to <see cref="Route"/>'s list of users currently on it</summary>
        /// <param name="user"><see cref="User"/> to be added to <see langword="this"/> <see cref="Route"/></param>
        public void AddUser(User user) {
            if (!users.Contains(user)) {
                users.Add(user);
            }
        }


        //Getters and Setters

        public int GetID() { return id; }

        public string GetName() { return name; }

        public double GetLength() { return length; }

        public Area GetArea() { return area; }

        public void SetID(int id) { this.id = id; }
    }
}
