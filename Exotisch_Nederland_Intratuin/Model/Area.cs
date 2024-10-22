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


        //Constructors

        /// <summary>
        /// Constructor for creating an <see cref="Area"/> from database
        /// </summary>
        /// <param name="id">ID of the area</param>
        /// <param name="name">Name of the area</param>
        /// <param name="size">Size of the area</param>
        public Area(int id, string name, double size) {
            this.id = id;
            this.name = name;
            this.size = size;
            this.routes = new List<Route>();
            this.observations = new List<Observation>();
        }

        /// <summary>
        /// Constructor for creating an <see cref="Area"/> from scratch<para/>
        /// Automatically adds it to the database
        /// </summary>
        /// <param name="name">Name of the area</param>
        /// <param name="size">Size of the area</param>
        public Area(string name, double size) {
            this.name = name;
            this.size = size;
            this.routes = new List<Route>();
            this.observations = new List<Observation>();

            SqlDal.AddArea(this);
        }


        //Methods

        /// <returns><see langword="List"/> of all <see cref="Area"/>'s currently in the database</returns>
        public static List<Area> GetAllAreas() {
            return SqlDal.GetAllAreas();
        }

        /// <summary>Adds a <see cref="Route"/> to <see cref="Area"/>'s list of routes</summary>
        /// <param name="route"><see cref="Route"/> to be added to <see langword="this"/> <see cref="Area"/></param>
        public void AddRoute(Route route) {
            if (!routes.Contains(route)) {
                routes.Add(route);
            }
        }

        /// <summary>Adds a <see cref="Observation"/> to <see cref="Area"/>'s list of observations</summary>
        /// <param name="observation"><see cref="Observation"/> to be added to <see langword="this"/> <see cref="Area"/></param>
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

        public void EditArea(string name, double size)
        {
            this.name = name;
            this.size = size;
            SqlDal.EditArea(this);
        }

        public void DeleteArea()
        {
            SqlDal.DeleteArea(this);
        }
    }
}


