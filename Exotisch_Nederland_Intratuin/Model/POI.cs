using Exotisch_Nederland_Intratuin.DAL;
using System.Collections.Generic;

namespace Exotisch_Nederland_Intratuin.Model {
    internal class POI {
        private static SQLDAL SqlDal = SQLDAL.Instance;

        private int id;
        private string name;
        private string location;
        private RoutePoint routePoint;


        //Constructors

        /// <summary>
        /// Constructor for creating a <see cref="POI"/> from database
        /// </summary>
        /// <param name="id">ID of the POI</param>
        /// <param name="name">Name of the POI</param>
        /// <param name="location">Location of the POI</param>
        /// <param name="routePoint"><see cref="RoutePoint"/> associated with this POI</param>
        public POI(int id, string name, string location, RoutePoint routePoint) {
            this.id = id;
            this.name = name;
            this.location = location;
            this.routePoint = routePoint;

            //Tell routepoint this POI is near them
            this.routePoint.AddPointOfInterest(this);
        }

        /// <summary>
        /// Constructor for creating a <see cref="POI"/> from scratch<para/>
        /// Automatically adds it to the database
        /// <param name="name">Name of the POI</param>
        /// <param name="location">Location of the POI</param>
        /// <param name="routePoint"><see cref="RoutePoint"/> associated with this POI</param>
        public POI(string name, string location, RoutePoint routePoint) {
            this.name = name;
            this.location = location;
            this.routePoint = routePoint;

            //Tell routepoint this POI is near them
            this.routePoint.AddPointOfInterest(this);
            SqlDal.AddPOI(this);
        }


        //Methods

        /// <returns><see langword="List"/> of all <see cref="POI"/>'s currently in the database</returns>
        public static List<POI> GetAllPOIs() {
            return SqlDal.GetAllPOIs();
        }

        public override string ToString() {
            return $"POI {id}: {name}, {location}, RoutePoint {routePoint.GetID()}";
        }


        //Getters and Setters

        public string GetName() { return name; }

        public string GetLocation() { return location; }

        public RoutePoint GetRoutePoint() { return routePoint; }

        public void SetID(int id) { this.id = id; }

        public void SetRoutePoint(RoutePoint routePoint) { this.routePoint = routePoint; }
    }
}
