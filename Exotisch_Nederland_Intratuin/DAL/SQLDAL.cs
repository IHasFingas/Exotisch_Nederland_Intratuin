using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exotisch_Nederland_Intratuin.Model;

namespace Exotisch_Nederland_Intratuin.DAL
{
    internal class SQLDAL
    {
        private static SQLDAL instance;
        private readonly string connectionString;
        private SqlConnection connection;
        private List<Area> areas;
        private List<Route> routes;
        private List<RoutePoint> routePoints;
        private List<POI> pois;
        private List<Game> games;
        private List<User> users;
        private List<Role> roles;
        private List<Observation> observations;
        private List<Specie> species;
        private List<Question> questions;
        private List<Answer> answers;
    }
}
