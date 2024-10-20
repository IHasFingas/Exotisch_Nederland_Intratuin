using Exotisch_Nederland_Intratuin.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exotisch_Nederland_Intratuin.Model {
    internal class Area {
        private SQLDAL SqlDal = SQLDAL.Instance;

        private int id;
        private string name;
        private double size;
        private List<Route> routes;
        private List<Observation> observations;

        public Area(int id, string name, double size) {
            this.id = id;
            this.name = name;
            this.size = size;
            this.routes = new List<Route>();
            this.observations = new List<Observation>();
        }

        public Area(string name, double size) {
            this.name = name;
            this.size = size;
            this.routes = new List<Route>();
            this.observations = new List<Observation>();

            SqlDal.AddArea(this);
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

        //Getters and Setters (veranderen we private attributen naar public incl { get; set; }?
        public int GetID() { return id; }

        public string GetName() { return name; }

        public double GetSize() { return size; }

        public void SetID(int id) { this.id = id; }
    }
}
