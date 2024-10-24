using Exotisch_Nederland_Intratuin.DAL;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Exotisch_Nederland_Intratuin.Model {
    internal class Observation {
        private static SQLDAL SqlDal = SQLDAL.Instance;

        private int id;
        private string name;
        private string location;
        private string description;
        private byte[] picture;
        private Specie specie;
        private Area area;
        private User user;


        //Constructor for creating an Observation from database
        public Observation(int id, string name, string location, string description, byte[] picture, Specie specie, Area area, User user) {
            this.id = id;
            this.name = name;
            this.location = location;
            this.description = description;
            this.picture = picture;
            this.specie = specie;
            this.area = area;
            this.user = user;

            if(this.name == "") {
                this.name = specie.GetName();
            }

            //Tell area this observation was added to it
            area.AddObservation(this);

            //Tell user this observation was made by them
            user.AddObservation(this);

            //Tell specie this observation was made of them
            specie.AddObservation(this);
        }

        //Constructor for creating an Observation from scratch (automatically adds it to the database)
        public Observation(string name, string location, string description, byte[] picture, Specie specie, Area area, User user) {
            this.name = name;
            this.location = location;
            this.description = description;
            this.picture = picture;
            this.specie = specie;
            this.area = area;
            this.user = user;

            if (this.name == "") {
                this.name = specie.GetName();
            }

            //Tell area this observation was added to it
            area.AddObservation(this);

            //Tell user this observation was made by them
            user.AddObservation(this);

            //Tell specie this observation was made of them
            specie.AddObservation(this);
            SqlDal.AddObservation(this);
        }


        //Methods

        public static List<Observation> GetAllObservations() {
            return SqlDal.GetAllObservations();
        }

        public static Observation GetObservationByID(int id) {
            return SqlDal.GetObservationByID(id);
        }

        public void EditObservation(string name, string location, string description, byte[] picture, Specie specie, Area area) {
            this.name = name;
            this.location = location;
            this.description = description;
            this.picture = picture;
            this.specie = specie;
            this.area = area;
            SqlDal.EditObservation(this);
        }

        public void DeleteObservation() {
            SqlDal.DeleteObservation(this);
        }

        public override string ToString() {
            return $"Observation {id}: {specie.GetName()}, {location}, Area {area.GetID()}, User {user.GetID()}";
        }


        //Getters and Setters

        public int GetID() { return id; }

        public string GetName() { return name; }

        public string GetLocation() { return location; }

        public string GetDescription() { return description; }

        public byte[] GetPicture() { return picture; }

        public Specie GetSpecie() { return specie; }

        public Area GetArea() { return area; }

        public User GetUser() { return user; }

        public void SetID(int id) { this.id = id; }
    }
}