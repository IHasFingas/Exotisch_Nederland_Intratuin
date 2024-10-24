using Exotisch_Nederland_Intratuin.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Exotisch_Nederland_Intratuin {
    internal class Program {
        private static List<Area> areas;
        private static List<Role> roles;
        private static List<Specie> species;
        private static List<RoutePoint> routePoints;
        private static List<Route> routes;
        private static List<POI> POIs;
        private static List<User> users;
        private static List<Game> games;
        private static List<Observation> observations;
        private static List<Question> questions;
        private static List<Answer> answers;

        static void Main(string[] args) {
            GetAllData();

            Console.WriteLine("Press enter to write all data");
            Console.ReadKey();

            WriteAllData();

            //Demonstrate GetObjectByID()
            //Demonstrate AddObject()
            //Demonstrate EditObject()
            //Demonstrate DeleteObject()

            Console.ReadKey();
        }

        public static void GetAllData() {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.WriteLine("Reading all data from database...");

            //Filling lists with all pre-entered data from database
            areas = Area.GetAllAreas();
            roles = Role.GetALlRoles();
            species = Specie.GetAllSpecies();
            routePoints = RoutePoint.GetAllRoutePoints();
            routes = Route.GetAllRoutes();
            POIs = POI.GetAllPOIs();
            users = User.GetAllUsers();
            games = Game.GetAllGames();
            observations = Observation.GetAllObservations();
            questions = Question.GetAllQuestions();
            answers = Answer.GetAllAnswers();
            Console.WriteLine("Done!");

            stopwatch.Stop();
            Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms\n");
        }

        public static void WriteAllData() {
            // Create a tuple of lists
            var allData = (areas, roles, species, routePoints, routes, POIs, users, games, observations, questions, answers);

            // Use IEnumerable to iterate over each item
            foreach (IEnumerable list in new IEnumerable[] { allData.areas, allData.roles, allData.species, allData.routePoints, allData.routes, allData.POIs, allData.users, allData.games, allData.observations, allData.questions, allData.answers }) {
                foreach (var item in list) {
                    Console.WriteLine(item);
                }

                Console.WriteLine();
            }
        }
    }
}