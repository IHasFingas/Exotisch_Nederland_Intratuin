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


        //Constructors

        /// <summary>
        /// Constructor for creating a <see cref="RoutePoint"/> from database
        /// </summary>
        /// <param name="id">ID of the routepoint</param>
        /// <param name="name">Name of the routepoint</param>
        /// <param name="location">Location of the routepoint</param>
        public RoutePoint(int id, string name, string location) {
            this.id = id;
            this.name = name;
            this.location = location;
            this.routes = new List<Route>();
            this.pointsOfInterest = new List<POI>();
        }

        /// <summary>
        /// Constructor for creating a <see cref="RoutePoint"/> from scratch<para/>
        /// Automatically adds it to the database
        /// </summary>
        /// <param name="name">Name of the routepoint</param>
        /// <param name="location">Location of the routepoint</param>
        public RoutePoint(string name, string location) {
            this.name = name;
            this.location = location;
            this.routes = new List<Route>();
            this.pointsOfInterest = new List<POI>();

            SqlDal.AddRoutePoint(this);
        }


        //Methods

        /// <returns><see langword="List"/> of all <see cref="RoutePoint"/>s currently in the database</returns>
        public static List<RoutePoint> GetAllRoutePoints() {
            return SqlDal.GetAllRoutePoints();
        }

        /// <summary>Adds a <see cref="Route"/> to <see cref="RoutePoint"/>'s list of routes it is used in</summary>
        /// <param name="route"><see cref="Route"/> to be added to <see langword="this"/> <see cref="RoutePoint"/></param>
        public void AddRoute(Route route) {
            if (!routes.Contains(route)) {
                routes.Add(route);
            }
        }

        /// <summary>Adds a <see cref="POI"/> to <see cref="RoutePoint"/>'s list of POI's it is near</summary>
        /// <param name="point"><see cref="POI"/> to be added to <see langword="this"/> <see cref="RoutePoint"/></param>
        public void AddPointOfInterest(POI point) {
            if (!pointsOfInterest.Contains(point)) {
                pointsOfInterest.Add(point);
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

