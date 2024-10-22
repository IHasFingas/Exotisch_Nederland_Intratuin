using Exotisch_Nederland_Intratuin.DAL;
using System.Collections.Generic;

namespace Exotisch_Nederland_Intratuin.Model {
    internal class Observation {
        private static SQLDAL SqlDal = SQLDAL.Instance;

        private int id;
        private string name; // verwijderen? specie heeft al een naam
        private string location;
        private string description;
        //private Image picture; To fix
        private Specie specie;
        private Area area;
        private User user;


        //Constructors

        /// <summary>
        /// Constructor for creating a <see cref="Observation"/> from database
        /// </summary>
        /// <param name="id">ID of the observation</param>
        /// <param name="name">Name of the observation</param>
        /// <param name="location">Location of the observation</param>
        /// <param name="description">Description of the observation</param>
        /// <param name="specie"><see cref="Specie"/> the observation is of</param>
        /// <param name="area"><see cref="Area"/> the observation is made in</param>
        /// <param name="user"><see cref="User"/> the observation is made by</param>
        public Observation(int id, string name, string location, string description, /*Image picture,*/ Specie specie, Area area, User user) {
            this.id = id;
            this.name = name;
            this.location = location;
            this.description = description;
            //this.picture = picture;
            this.specie = specie;
            this.area = area;
            this.user = user;

            //Tell area this observation was added to it
            area.AddObservation(this);

            //Tell user this observation was made by them
            user.AddObservation(this);

            //Tell specie this observation was made of them
            specie.AddObservation(this);
        }

        /// <summary>
        /// Constructor for creating a <see cref="Observation"/> from scratch<para/>
        /// Automatically adds it to the database
        /// </summary>
        /// <param name="name">Name of the observation</param>
        /// <param name="location">Location of the observation</param>
        /// <param name="description">Description of the observation</param>
        /// <param name="specie"><see cref="Specie"/> the observation is of</param>
        /// <param name="area"><see cref="Area"/> the observation is made in</param>
        /// <param name="user"><see cref="User"/> the observation is made by</param>
        public Observation(string name, string location, string description, /*Image picture,*/ Specie specie, Area area, User user) {
            this.name = name;
            this.location = location;
            this.description = description;
            //this.picture = picture;
            this.specie = specie;
            this.area = area;
            this.user = user;

            //Tell area this observation was added to it
            area.AddObservation(this);

            //Tell user this observation was made by them
            user.AddObservation(this);

            //Tell specie this observation was made of them
            specie.AddObservation(this);
            SqlDal.AddObservation(this);
        }


        //Methods

        /// <returns><see langword="List"/> of all <see cref="Observation"/>s currently in the database</returns>
        public static List<Observation> GetAllObservations() {
            return SqlDal.GetAllObservations();
        }

        public override string ToString() {
            return $"Observation {id}: {specie.GetName()}, {location}, Area {area.GetID()}, User {user.GetID()}";
        }


        //Getters and Setters

        public int GetID() { return id ; }

        public string GetName() { return name; }

        public string GetLocation() { return location; }

        public string GetDescription() { return description; }

        public Specie GetSpecie() { return specie; }

        public Area GetArea() { return area; }

        public User GetUser() { return user; }

        public void SetID(int id) { this.id = id; }
    }
}

