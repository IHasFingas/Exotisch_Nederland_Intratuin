using Exotisch_Nederland_Intratuin.DAL;
using System.Collections.Generic;

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
        private bool isValidated;


        //Constructor for creating an Observation from database
        public Observation(int id, string name, string location, string description, byte[] picture, Specie specie, Area area, User user, bool isValidated) {
            this.id = id;
            this.name = name;
            this.location = location;
            this.description = description;
            this.picture = picture;
            this.specie = specie;
            this.area = area;
            this.user = user;
            this.isValidated = isValidated;

            if (this.name == "") {
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
        public Observation(string name, string location, string description, byte[] picture, Specie specie, Area area, User user, bool isValidated) {
            this.name = name;
            this.location = location;
            this.description = description;
            this.picture = picture;
            this.specie = specie;
            this.area = area;
            this.user = user;
            this.isValidated = false;

            if (this.name == "") {
                this.name = specie.GetName();
            }

            this.id = SqlDal.AddObservation(this);

            //Tell area this observation was added to it
            area.AddObservation(this);

            //Tell user this observation was made by them
            user.AddObservation(this);

            //Tell specie this observation was made of them
            specie.AddObservation(this);
        }


        //Methods

        public static List<Observation> GetAll() {
            return SqlDal.GetAllObservations();
        }

        public static Observation GetByID(int id) {
            return SqlDal.GetObservationByID(id);
        }

        public void Edit(string name, string location, string description, byte[] picture, Specie specie, Area area, User user, bool isValidated) {
            this.name = name;

            if (this.name == "") {
                this.name = specie.GetName();
            }

            this.location = location;
            this.description = description;
            this.picture = picture;

            if (this.specie != specie) {
                this.specie.RemoveObservation(this);
                this.specie = specie;
                this.specie.AddObservation(this);
            }

            if (this.area != area) {
                this.area.RemoveObservation(this);
                this.area = area;
                this.area.AddObservation(this);
            }

            this.user = user;
            this.isValidated = isValidated;

            SqlDal.EditObservation(this);
        }

        public void Delete() {
            SqlDal.DeleteObservation(this);
        }

        public override string ToString() {
            return $"Observation {id}: {specie.GetName()}, {location}, Area {area.GetID()}, User {user.GetID()}, Validated {isValidated}";
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

        public bool GetValidated() { return isValidated; }
    }
}