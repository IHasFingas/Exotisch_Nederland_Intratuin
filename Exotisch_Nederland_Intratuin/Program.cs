using Exotisch_Nederland_Intratuin.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Exotisch_Nederland_Intratuin {
    internal class Program {
        static void Main(string[] args) {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            //Filling lists with all pre-entered data from database
            Console.WriteLine("Reading all data from database...");
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

            stopwatch.Stop();
            Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms\n");

            foreach (Area area in areas) { Console.WriteLine(area); }
            Console.WriteLine();
            foreach (Role role in roles) { Console.WriteLine(role); }
            Console.WriteLine();
            foreach (Specie specie in species) { Console.WriteLine(specie); }
            Console.WriteLine();
            foreach (RoutePoint routePoint in routePoints) { Console.WriteLine(routePoint); }
            Console.WriteLine();
            foreach (Route route in routes) { Console.WriteLine(route); }
            Console.WriteLine();
            foreach (POI poi in POIs) { Console.WriteLine(poi); }
            Console.WriteLine();
            foreach (User user in users) { Console.WriteLine(user); }
            Console.WriteLine();
            foreach (Game game in games) { Console.WriteLine(game); }
            Console.WriteLine();
            foreach (Observation observation in observations) { Console.WriteLine(observation); }
            Console.WriteLine();
            foreach (Question question in questions) { Console.WriteLine(question); }
            Console.WriteLine();
            foreach (Answer answer in answers) { Console.WriteLine(answer); }

            Console.ReadKey();
        }
    }
}