using Exotisch_Nederland_Intratuin.DAL;
using System.Collections.Generic;

namespace Exotisch_Nederland_Intratuin.Model {
    internal class POI {
        private static SQLDAL SqlDal = SQLDAL.Instance;

        private int id;
        private string name;
        private string description;
        private string location;
        private RoutePoint routePoint;


        //Constructor for creating a POI from database
        public POI(int id, string name, string description, string location, RoutePoint routePoint) {
            this.id = id;
            this.name = name;
            this.description = description;
            this.location = location;
            this.routePoint = routePoint;

            //Tell routepoint this POI is near them
            this.routePoint.AddPointOfInterest(this);
        }

        //Constructor for creating a POI from scratch (automatically adds it to the database)
        public POI(string name, string description, string location, RoutePoint routePoint) {
            this.name = name;
            this.description = description;
            this.location = location;
            this.routePoint = routePoint;

            this.id = SqlDal.AddPOI(this);

            //Tell routepoint this POI is near them
            this.routePoint.AddPointOfInterest(this);
        }


        //Methods

        public static List<POI> GetAll() {
            return SqlDal.GetAllPOIs();
        }

        public static POI GetByID(int id) {
            return SqlDal.GetPOIByID(id);
        }

        public void Edit(string name, string description, string location, RoutePoint routePoint) {
            this.name = name;
            this.description = description;
            this.location = location;

            if (this.routePoint != routePoint) {
                this.routePoint.RemovePointOfInterest(this);
                this.routePoint = routePoint;
                this.routePoint.AddPointOfInterest(this);
            }

            SqlDal.EditPOI(this);
        }

        public void Delete() {
            SqlDal.DeletePOI(this);
        }

        public override string ToString() {
            return $"POI {id}: {name}, {location}, RoutePoint {routePoint.GetID()}";
        }


        //Getters and Setters

        public int GetID() { return id; }

        public string GetName() { return name; }

        public string GetDescription() { return description; }

        public string GetLocation() { return location; }

        public RoutePoint GetRoutePoint() { return routePoint; }
    }
}