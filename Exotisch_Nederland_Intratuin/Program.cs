using Exotisch_Nederland_Intratuin.DAL;
using Exotisch_Nederland_Intratuin.Model;
using System;
using System.Collections.Generic;

namespace Exotisch_Nederland_Intratuin {
    internal class Program {
        static void Main(string[] args) {
            //Filling lists with all pre-entered data from database
            List<Area> areas = Area.GetAllAreas();
            List<Role> roles = Role.GetALlRoles();
            List<Specie> species = Specie.GetAllSpecies();
            List<RoutePoint> routePoints = RoutePoint.GetAllRoutePoints();
            List<Route> routes = Route.GetAllRoutes();
            List<POI> POIs = POI.GetAllPOIs();
            List<User> users = User.GetAllUsers();
            List<Game> games = Game.GetAllGames();
            List<Observation> observations = Observation.GetAllObservations();
            List<Question> questions = Question.GetAllQuestions();
            List<Answer> answers = Answer.GetAllAnswers();

            Console.WriteLine("Done!");
            Console.ReadKey();
        }
    }
}
