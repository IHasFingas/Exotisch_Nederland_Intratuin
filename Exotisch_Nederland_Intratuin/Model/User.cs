using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exotisch_Nederland_Intratuin.Model
{
    internal class User
    {
            private int id;
            private string name;
            private string email;
            private string currentLocation;
            private Route currentRoute;
            private List<Role> roles;
            private List<Observation> observations;

        public void AddObservation() { }
        public void EditObservation() { }
        public void RemoveObservation() { }
    }
}
