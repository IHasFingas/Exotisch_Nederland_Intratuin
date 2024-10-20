using Exotisch_Nederland_Intratuin.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exotisch_Nederland_Intratuin.Model {
    internal class Specie {
        private SQLDAL SqlDal = SQLDAL.Instance;

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
        }

        public Specie(string domain, string regnum, string phylum, string classus, string ordo, string familia, string genus, string name) {
            this.domain = domain;
            this.regnum = regnum;
            this.phylum = phylum;
            this.classus = classus;
            this.ordo = ordo;
            this.familia = familia;
            this.genus = genus;
            this.name = name;

            SqlDal.AddSpecie(this);
        }

        public void AddObservation(Observation observation) {
            observations.Add(observation);
        }

        //Getters and Setters (veranderen we private attributen naar public incl { get; set; }?
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
