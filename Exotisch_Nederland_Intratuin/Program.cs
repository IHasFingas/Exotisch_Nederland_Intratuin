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

            Console.WriteLine("Press enter to close");
            Console.ReadKey();
        }

        public static void GetAllData() {
            Console.WriteLine("Press enter to get all data");
            Console.ReadKey();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.WriteLine("Reading all data from database...");

            //Filling lists with all pre-entered data from database
            areas = Area.GetAll();
            roles = Role.GetAll();
            species = Specie.GetAll();
            routePoints = RoutePoint.GetAll();
            routes = Route.GetAll();
            POIs = POI.GetAll();
            users = User.GetAll();
            games = Game.GetAll();
            observations = Observation.GetAll();
            questions = Question.GetAll();
            answers = Answer.GetAll();
            Console.WriteLine("Done!");

            stopwatch.Stop();
            Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms\n");
        }

        public static void WriteAllData() {
            Console.WriteLine("Press enter to write all data");
            Console.ReadKey();

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

        public static void TestAll() {
            Console.WriteLine("Press enter to add new instance of each object to databse");
            Console.ReadKey();

            Area area = new Area("Test name", 1000);
            Role role = new Role("Test name", "0123456789");
            Specie specie = new Specie("Test name", "Test domain", "Test regnum", "Test phylum", "Test classus", "Test ordo", "Test familia", "Test genus");
            RoutePoint routePoint = new RoutePoint("Test name", "Test location", new Dictionary<RoutePoint, double>() { { routePoints[0], 10 }, { routePoints[1], 20 }, { routePoints[2], 30 } });
            Route route = new Route("Test name", areas[0], routePoints[0], routePoints[5]);
            POI poi = new POI("Test name", "Test description", "Test location", routePoints[0]);
            User user = new User("Test name", "Test email", "Test password", "Test location", routes[0]);
            Game game = new Game("Test name", "Test location", "Test description", routes[0]);
            Observation observation = new Observation("Test name", "Test location", "Test description", new byte[] { 0x01, 0x02, 0x03 }, species[0], areas[0], users[0]);
            Question question = new Question("Test text", games[0]);
            Answer answer = new Answer("Text text", questions[0], true);

            Console.WriteLine("Succesfully created new instance of each object and added to database");
            Console.WriteLine("\nPress enter to edit each object and update in database");
            Console.ReadKey();

            area.Edit("Updated test name", 1500);
            role.Edit("Updated test name", "9876543210");
            specie.Edit("Updated test name", "Updated test domain", "Updated test regnum", "Updated test phylum", "Updated test classus", "Updated test ordo", "Updated test familia", "Updated test genus");
            routePoint.Edit("Updated test name", "Updated test location", new Dictionary<RoutePoint, double>() { { routePoints[3], 40 }, { routePoints[4], 50 }, { routePoints[5], 60 } });
            route.Edit("Updated test name", areas[1], routePoints[1], routePoints[6]);
            poi.Edit("Updated test name", "Updated test description", "Updated test location", routePoints[1]);
            user.Edit("Updated test name", "Updated test email", "Updated test password", "Updated test location", routes[1], new List<Role>() { roles[1] });
            game.Edit("Updated test name", "Updated test location", "Updated test description", routes[1]);
            observation.Edit("Updated test name", "Updated test location", "Updated test description", new byte[] { 0x04, 0x05, 0x06 }, species[1], areas[1], users[1], true, true);
            question.Edit("Updated test text", games[1]);
            answer.Edit("Updated test text", questions[1], false);

            Console.WriteLine("Succesfully edited each object and updated in database");
            Console.WriteLine("\nPress enter to delete each object from database");
            Console.ReadKey();

            area.Delete();
            role.Delete();
            specie.Delete();
            routePoint.Delete();
            route.Delete();
            poi.Delete();
            user.Delete();
            game.Delete();
            observation.Delete();
            question.Delete();
            answer.Delete();

            Console.WriteLine("Succesfully deleted each object from database");
        }

        public static void GUI() {

        }
    }
}