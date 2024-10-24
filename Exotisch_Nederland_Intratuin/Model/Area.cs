using Exotisch_Nederland_Intratuin.DAL;
using System.Collections.Generic;

namespace Exotisch_Nederland_Intratuin.Model {
    internal class Area {
        private static SQLDAL SqlDal = SQLDAL.Instance;

        private int id;
        private string name;
        private double size;
        private List<Route> routes;
        private List<Observation> observations;


        //Constructor for creating an Area from database
        public Area(int id, string name, double size) {
            this.id = id;
            this.name = name;
            this.size = size;
            this.routes = new List<Route>();
            this.observations = new List<Observation>();
        }

        //Constructor for creating an Area from scratch (automatically adds it to the database)
        public Area(string name, double size) {
            this.name = name;
            this.size = size;
            this.routes = new List<Route>();
            this.observations = new List<Observation>();

            SqlDal.AddArea(this);
        }


        //Methods

        public static List<Area> GetAllAreas() {
            return SqlDal.GetAllAreas();
        }

        public static Area GetAreaByID(int id) {
            return SqlDal.GetAreaByID(id);
        }

        public void EditArea(string name, double size) {
            this.name = name;
            this.size = size;
            SqlDal.EditArea(this);
        }

        public void DeleteArea() {
            SqlDal.DeleteArea(this);
        }

        public void AddRoute(Route route) {
            if (!routes.Contains(route)) {
                routes.Add(route);
            }
        }

        public void AddObservation(Observation observation) {
            if (!observations.Contains(observation)) {
                observations.Add(observation);
            }
        }

        public override string ToString() {
            return $"Area {id}: {name}, {size}ha";
        }

        //Getters and Setters

        public int GetID() { return id; }

        public string GetName() { return name; }

        public double GetSize() { return size; }

        public void SetID(int id) { this.id = id; }
    }
}