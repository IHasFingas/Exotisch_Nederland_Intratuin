using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exotisch_Nederland_Intratuin.Model
{
    internal class RoutePoint
    {
        private int id;
        private string name;
        private string location;
        private List<Route> routes;
        private List<POI> pointsOfInterest;
    }
}

