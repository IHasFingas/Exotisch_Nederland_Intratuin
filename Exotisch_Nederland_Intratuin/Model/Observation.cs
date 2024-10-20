using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Exotisch_Nederland_Intratuin.DAL;
using Microsoft.Win32;

namespace Exotisch_Nederland_Intratuin.Model {
    internal class Observation {
        private SQLDAL SqlDal = SQLDAL.Instance;

        private int id;
        private string name;
        private string location;
        private string description;
        //private Image picture; To fix
        private Specie specie;
        private Area area;
        private User user;

        public Observation(int id, string name, string location, string description, /*Image picture,*/ Specie specie, Area area, User user) {
            this.id = id;
            this.name = name;
            this.location = location;
            this.description = description;
            //this.picture = picture;
            this.specie = specie;
            this.area = area;
            this.user = user;

            area.AddObservation(this);
            user.AddObservation(this);
            specie.AddObservation(this);
        }

        public Observation(string name, string location, string description, /*Image picture,*/ Specie specie, Area area, User user) {
            this.name = name;
            this.location = location;
            this.description = description;
            //this.picture = picture;
            this.specie = specie;
            this.area = area;
            this.user = user;

            area.AddObservation(this);
            user.AddObservation(this);
            specie.AddObservation(this);
            SqlDal.AddObservation(this);
        }

        //Getters and Setters (veranderen we private attributen naar public incl { get; set; }?
        public string GetName() { return name; }

        public string GetLocation() { return location; }

        public string GetDescription() { return description; }

        public Specie GetSpecie() { return specie; }

        public Area GetArea() { return area; }

        public User GetUser() { return user; }

        public void SetID(int id) { this.id = id; }
    }
}

