using Exotisch_Nederland_Intratuin.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exotisch_Nederland_Intratuin.Model {
    internal class Route {
        private SQLDAL SqlDal = SQLDAL.Instance;

        private int id;
        private string name;
        private double length;
        private Area area;
        private List<RoutePoint> routePoints;
        private List<Game> games;
        private List<User> users;

        public Route(string name, double length, Area area, List<RoutePoint> routePoints, List<Game> games) {
            this.name = name;
            this.length = length;
            this.area = area;
            foreach (RoutePoint routePoint in routePoints) { AddRoutePoint(routePoint); }
            foreach (Game game in games) { this.games.Add(game); }
            this.users = new List<User>();

            area.AddRoute(this);
            SqlDal.AddRoute(this);
        }

        public void AddRoutePoint(RoutePoint routePoint) {
            if (!routePoints.Contains(routePoint)) {
                routePoints.Add(routePoint);
                routePoint.AddRoute(this);
            }
        }

        public void AddGame(Game game) {
            if (!games.Contains(game)) {
                games.Add(game);
            }
        }

        public void AddUser(User user) {
            if (!users.Contains(user)) {
                users.Add(user);
            }
        }

        //Getters and Setters (veranderen we private attributen naar public incl { get; set; }?
        public int GetID() { return id; }

        public string GetName() { return name; }

        public double GetLength() { return length; }

        public Area GetArea() { return area; }

        public void SetID(int id) { this.id = id; }
    }
}
