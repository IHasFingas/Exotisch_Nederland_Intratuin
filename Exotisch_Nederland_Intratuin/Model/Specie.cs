using Exotisch_Nederland_Intratuin.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exotisch_Nederland_Intratuin.Model
{
    internal class Specie
    {
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

        public void EditSpecie(string name, string regnum, string phylum, string classus, string ordo, string familia, string genus, string domain)
        {
            this.name = name;
            this.regnum = regnum;
            this.phylum = phylum;   
            this.classus = classus;
            this.ordo = ordo;   
            this.familia = familia;
            this.genus = genus;
            this.domain = domain;
            SqlDal.EditSpecie(this);
        }

        public void DeleteSpecie()
        {
            SqlDal.DeleteSpecie(this);
        }

    }
}
