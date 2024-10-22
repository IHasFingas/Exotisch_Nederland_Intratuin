using Exotisch_Nederland_Intratuin.DAL;
using System.Collections.Generic;

namespace Exotisch_Nederland_Intratuin.Model {
    internal class Specie {
        private static SQLDAL SqlDal = SQLDAL.Instance;

        private int id;
        private string domain;
        private string regnum;
        private string phylum;
        private string classus;
        private string ordo;
        private string familia;
        private string genus;
        private string name;
        private List<Observation> observations;


        //Constructors

        /// <summary>
        /// Constructor for creating a <see cref="Specie"/> from database
        /// </summary>
        /// <param name="id">ID of the specie</param>
        /// <param name="domain">Domain of the specie</param>
        /// <param name="regnum">Regnum of the specie</param>
        /// <param name="phylum">Phylum of the specie</param>
        /// <param name="classus">Classus of the specie</param>
        /// <param name="ordo">Ordo of the specie</param>
        /// <param name="familia">Familia of the specie</param>
        /// <param name="genus">Genus of the specie</param>
        /// <param name="name">Name of the specie</param>
        public Specie(int id, string domain, string regnum, string phylum, string classus, string ordo, string familia, string genus, string name) {
            this.id = id;
            this.domain = domain;
            this.regnum = regnum;
            this.phylum = phylum;
            this.classus = classus;
            this.ordo = ordo;
            this.familia = familia;
            this.genus = genus;
            this.name = name;
            this.observations = new List<Observation>();
        }

        /// <summary>
        /// Constructor for creating a <see cref="Specie"/> from scratch<para/>
        /// Automatically adds it to the database
        /// </summary>
        /// <param name="id">ID of the specie</param>
        /// <param name="domain">Domain of the specie</param>
        /// <param name="regnum">Regnum of the specie</param>
        /// <param name="phylum">Phylum of the specie</param>
        /// <param name="classus">Classus of the specie</param>
        /// <param name="ordo">Ordo of the specie</param>
        /// <param name="familia">Familia of the specie</param>
        /// <param name="genus">Genus of the specie</param>
        /// <param name="name">Name of the specie</param>
        public Specie(string domain, string regnum, string phylum, string classus, string ordo, string familia, string genus, string name) {
            this.domain = domain;
            this.regnum = regnum;
            this.phylum = phylum;
            this.classus = classus;
            this.ordo = ordo;
            this.familia = familia;
            this.genus = genus;
            this.name = name;
            this.observations = new List<Observation>();

            SqlDal.AddSpecie(this);
        }


        //Methods

        /// <returns><see langword="List"/> of all <see cref="Specie"/>s currently in the database</returns>
        public static List<Specie> GetAllSpecies() {
            return SqlDal.GetAllSpecies();
        }

        /// <summary>Adds an <see cref="Observation"/> to <see cref="Specie"/>'s list of observations</summary>
        /// <param name="observation"><see cref="Observation"/> to be added to <see langword="this"/> <see cref="Specie"/></param>
        public void AddObservation(Observation observation) {
            if (!observations.Contains(observation)) {
                observations.Add(observation);
            }
        }

        public override string ToString() {
            return $"Specie {id}: {name}, {domain}, {regnum}, {phylum}, {classus}, {ordo}, {familia}, {genus}";
        }


        //Getters and Setters

        public int GetID() { return id; }

        public string GetDomain() { return domain; }

        public string GetRegnum() { return regnum; }

        public string GetPhylum() { return phylum; }

        public string GetClassus() { return classus; }

        public string GetOrdo() { return ordo; }

        public string GetFamilia() { return familia; }

        public string GetGenus() { return genus; }

        public string GetName() { return name; }

        public void SetID(int id) { this.id = id; }
    }
}
