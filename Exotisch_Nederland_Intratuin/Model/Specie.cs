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


        //Constructor for creating a Specie from database
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

        //Constructor for creating a Specie from scratch (automatically adds it to the database)
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

            this.id = SqlDal.AddSpecie(this);
        }


        //Methods

        public static List<Specie> GetAllSpecies() {
            return SqlDal.GetAllSpecies();
        }

        public static Specie GetSpecieByID(int id) {
            return SqlDal.GetSpecieByID(id);
        }

        public void EditSpecie(string name, string domain, string regnum, string phylum, string classus, string ordo, string familia, string genus) {
            this.name = name;
            this.domain = domain;
            this.regnum = regnum;
            this.phylum = phylum;
            this.classus = classus;
            this.ordo = ordo;
            this.familia = familia;
            this.genus = genus;
            SqlDal.EditSpecie(this);
        }

        public void DeleteSpecie() {
            SqlDal.DeleteSpecie(this);
        }

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