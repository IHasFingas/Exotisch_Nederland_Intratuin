using Exotisch_Nederland_Intratuin.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;

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

            UI();

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
            Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms");
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

        public static void UI() {
            Console.WriteLine("\nPress enter to start UI");
            Console.ReadKey();

            bool running = true;
            bool loggedIn = false;

            while (running) {
                Console.Clear();
                Console.WriteLine("===Login===");

                Console.Write("Email: ");
                string email = Console.ReadLine();

                User user = users.FirstOrDefault(u => u.GetEmail() == email);

                //Logging in
                if (user != null) {
                    //Email exists, prompt for password until logged in
                    while (!loggedIn) {
                        Console.Write("Password: ");

                        if (user.GetPassword() == Console.ReadLine()) {
                            loggedIn = true;
                        } else {
                            Console.WriteLine("Incorrect password");
                        }
                    }
                } else {
                    //Email doesnt exist, offer to register a new account
                    Console.Write("No user with that email was found. Would you like to register a new account? (Y/N): ");

                    if (Console.ReadLine().ToUpper() == "Y") {
                        while (!loggedIn) {
                            Console.Clear();
                            Console.WriteLine("===Registering new account===");

                            Console.Write("Enter a username: ");
                            string name = Console.ReadLine();

                            Console.Write("Enter a password: ");
                            string password = Console.ReadLine();

                            Console.Write("Enter current location: ");
                            string location = Console.ReadLine();

                            Route route = null;

                            while (route == null) {
                                Console.Write("Enter route ID (placeholder = -1): ");
                                int routeID = int.Parse(Console.ReadLine());

                                route = routes.FirstOrDefault(r => r.GetID() == routeID);
                            }

                            try {
                                user = new User(name, email, password, location, route);
                                user.AddRole(roles[1], "0000", true);
                                loggedIn = true;
                            } catch (Exception) {
                                Console.WriteLine("\nFailed to create a new user. Please try again.");
                            }
                        }
                    }
                }

                if (loggedIn) { //Once user has logged in
                    Console.Clear();
                    Console.WriteLine($"===Welcome {user.GetName()}==="
                                      + "\nChoose a role:"
                                      + $"\n\t{string.Join("\n\t", user.GetRoles().Select(r => r.GetName()))}");

                    string input = null;
                    while (!user.GetRoles().Any(r => r.GetName() == input)) {
                        input = Console.ReadLine();
                    }

                    user.SetActiveRole(user.GetRoles().FirstOrDefault(r => r.GetName() == input));

                    while (loggedIn) {
                        Console.Clear();

                        if (user.GetActiveRole().GetName() == "Hiker" || user.GetActiveRole().GetName() == "Volunteer") {
                            Console.Write("===Homepage==="
                                          + $"\nOptions:            Active role: {user.GetActiveRole().GetName()}"
                                          + "\n\tRole options"
                                          + "\n\tAdd observation"
                                          + "\n\tView observations"
                                          + "\n\tStart route"
                                          + "\n\tLogout"
                                          + "\n\tStop\n");
                        } else {
                            Console.Write("===Homepage==="
                                          + $"\nOptions:            Active role: {user.GetActiveRole().GetName()}"
                                          + "\n\tRole options"
                                          + "\n\tValidate observations"
                                          + "\n\tLogout"
                                          + "\n\tStop\n");
                        }

                        switch (Console.ReadLine().ToLower()) {
                            case "role options":
                                RoleOptions(user);
                                break;
                            case "add observation":
                                AddObservation(user);
                                break;
                            case "view observations":
                                ViewObservations(user);
                                break;
                            case "start route":
                                StartRoute(user);
                                break;
                            case "logout":
                                loggedIn = false;
                                break;
                            case "stop":
                                loggedIn = false;
                                running = false;
                                break;
                            case "validate observations":
                                ValidateObservations(user);
                                break;
                            default:
                                Console.WriteLine("Invalid input");
                                break;
                        }
                    }
                }
            }

            Console.Clear();
        }

        private static void RoleOptions(User user) {
            Console.Clear();
            Console.WriteLine("===Role options==="
                              + "\nOptions:"
                              + "\n\tChange active role"
                              + "\n\tAdd role");

            switch (Console.ReadLine().ToLower()) {
                case "change active role":
                    ChangeActiveRole(user);
                    break;
                case "add role":
                    AddRole(user);
                    break;
                default:
                    break;
            }
        }

        private static void ChangeActiveRole(User user) {
            Console.Clear();
            Console.WriteLine("===Changing active role==="
                              + "\nChoose a role:"
                              + $"\n\t{string.Join("\n\t", user.GetRoles().Select(r => r.GetName()))}");

            string input = null;
            while (!user.GetRoles().Any(r => r.GetName() == input)) {
                input = Console.ReadLine();
            }

            user.SetActiveRole(roles.First(r => r.GetName() == input));

            Console.WriteLine("Successfully changed active role"
                              + "\nPress enter to return to menu");
            Console.ReadKey();
        }

        private static void AddRole(User user) {
            Console.Clear();
            Console.WriteLine("===Adding role==="
                              + "\nWhich role do you want to add:"
                              + $"\n\t{string.Join("\n\t", roles.Skip(1).Select(r => r.GetName()))}");

            string input = null;
            while (!roles.Any(r => r.GetName() == input)) {
                input = Console.ReadLine();
            }

            Role role = roles.First(r => r.GetName() == input);

            Console.Write("Key: ");
            string key = Console.ReadLine();

            if (role.GetKey() == key) {
                user.AddRole(roles.First(r => r.GetName() == input), key, true);

                Console.WriteLine("Successfully added role"
                                  + "\nPress enter to return to menu");
            } else {
                Console.WriteLine("Incorrect key");
            }

            Console.ReadKey();
        }

        private static void AddObservation(User user) {
            Console.Clear();
            Console.Write("===Add observation==="
                              + "\nName (leave empty to use specie name): ");

            string name = Console.ReadLine();

            Console.Write("Description: ");
            string description = Console.ReadLine();

            Console.Write("Picture: ");
            byte[] picture = Encoding.UTF8.GetBytes(Console.ReadLine());

            Console.Write("Specie name: ");
            string specieName = null;
            while (!species.Any(s => s.GetName() == specieName)) {
                specieName = Console.ReadLine();
            }

            Specie specie = species.First(s => s.GetName() == specieName);

            Console.Write("Area name: ");
            string areaName = null;
            while (!areas.Any(s => s.GetName() == areaName)) {
                areaName = Console.ReadLine();
            }

            Area area = areas.First(s => s.GetName() == areaName);

            try {
                Observation observation = new Observation(name, user.GetCurrentLocation(), description, picture, specie, area, user);
                Console.WriteLine("Successfully created observation"
                                  + "\nPress enter to return to menu");
            } catch (Exception e) {
                Console.WriteLine("Failed to create Observation");
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
        }

        private static void ViewObservations(User user) {
            Console.Clear();
            Console.Write("===View observations==="
                              + $"\n{string.Join("\n", user.GetObservations().Select(o => o.GetName() + "\n\tValidated: " + o.GetValidated()))}");

            if (user.GetObservations().Count > 0) {
                Console.WriteLine("\nDo you want to edit an observation? (Y/N)");

                if (Console.ReadLine().ToUpper() != "Y") {
                    Console.WriteLine("\nPress enter to return to menu");
                }

                List<Observation> unvalidatedObservations = user.GetObservations().Where(o => !o.GetValidated()).ToList();

                Console.Clear();
                Console.Write("===Edit observations==="
                              + $"\n{string.Join("\n", unvalidatedObservations.Select((o, i) => $"{i + 1}: {o.GetName()}\n\tValidated: {o.GetValidated()}"))}"
                              + $"\nEnter the number of the observation you wish to edit: ");

                Observation observation = unvalidatedObservations[int.Parse(Console.ReadLine()) - 1];

                EditObservation(user, observation, false);
            }
        }

        private static void EditObservation(User user, Observation observation, bool calledByValidator) {
            if (observation.GetValidated()) {
                return;
            }

            Console.Clear();
            if (calledByValidator) {
                Console.WriteLine("===Edit observation==="
                                  + $"\nEditing {observation.GetName()}:"
                                  + $"\n\t{observation.GetLocation()}"
                                  + $"\n\t{observation.GetDescription()}"
                                  + $"\n\t{observation.GetPicture()}"
                                  + $"\n\t{observation.GetSpecie().GetName()}"
                                  + $"\n\t{observation.GetArea().GetName()}"
                                  + $"\n\t{observation.GetUser().GetName()} ({observation.GetUser().GetID()})"
                                  + $"\n\tSubmitted by volunteer: {observation.GetSubmittedByVolunteer()}");
            } else {
                Console.WriteLine("===Edit observation==="
                                  + $"\nEditing {observation.GetName()}:"
                                  + $"\n\t{observation.GetDescription()}"
                                  + $"\n\t{observation.GetPicture()}"
                                  + $"\n\t{observation.GetSpecie().GetName()}"
                                  + $"\n\t{observation.GetArea().GetName()}");
            }

            Console.Write("New name: ");
            string name = Console.ReadLine();

            Console.Write("New description: ");
            string description = Console.ReadLine();

            Console.Write("New picture: ");
            byte[] picture = Encoding.UTF8.GetBytes(Console.ReadLine());

            Console.Write("New specie name: ");
            string specieName = null;
            while (!species.Any(s => s.GetName() == specieName)) {
                specieName = Console.ReadLine();
            }

            Specie specie = species.First(s => s.GetName() == specieName);

            Console.Write("New area name: ");
            string areaName = null;
            while (!areas.Any(s => s.GetName() == areaName)) {
                areaName = Console.ReadLine();
            }

            Area area = areas.First(s => s.GetName() == areaName);

            user.EditObservation(observation, name, observation.GetLocation(), description, picture, specie, area);
            Console.WriteLine("Successfully edited observation"
                              + "Press enter to return to menu ");
            Console.ReadKey();
        }

        private static void StartRoute(User user) {
            foreach (RoutePoint routePoint in user.GetRoute().GetRoutePoints()) {
                Console.Clear();
                Console.WriteLine("===Walking route==="
                              + $"\nNext route point: {routePoint.GetName()}"
                              + $"\n\t{routePoint.GetLocation()}"
                              + $"\n\nEnter\t\tNext route point\n'Stop'\t\tStop route\n'Play game'\tPlay a game");

                if (Console.ReadLine().ToLower() == "play game") {
                    user.PlayGame(user.GetRoute().GetGames()[0]);
                }
            }
        }

        private static void ValidateObservations(User user) {
            List<Observation> unvalidatedObservations = Observation.GetAll().Where(o => !o.GetValidated()).ToList();

            Console.Clear();
            Console.Write("===Validate observations==="
                              + $"\n{string.Join("\n", unvalidatedObservations.Select((o, i) => $"{i + 1}: {o.GetName()}"))}");

            Console.WriteLine("\nType the number of the observation you want to view, or type -1 to stop");
            int viewInput = int.Parse(Console.ReadLine());

            if (viewInput == -1) {
                return;
            }

            Observation observation = unvalidatedObservations[viewInput - 1];

            Console.Clear();
            Console.WriteLine("===Validate observation==="
                              + $"\nEditing {observation.GetName()}:"
                              + $"\n\t{observation.GetLocation()}"
                              + $"\n\t{observation.GetDescription()}"
                              + $"\n\t{observation.GetPicture()}"
                              + $"\n\t{observation.GetSpecie().GetName()}"
                              + $"\n\t{observation.GetArea().GetName()}"
                              + $"\n\t{observation.GetUser().GetName()} ({observation.GetUser().GetID()})"
                              + $"\n\tSubmitted by volunteer: {observation.GetSubmittedByVolunteer()}");

            Console.WriteLine("Type 'edit' if you want to edit this observation\nDo you want to validate this observation? (Y/N):");

            string validateInput = Console.ReadLine();

            if (validateInput.ToUpper() == "Y") {
                user.ValidateObservation(observation);
                Console.WriteLine("Successfully validated observation"
                                  + "\nPress enter to return to menu");
                Console.ReadKey();
            } else if (validateInput.ToLower() == "edit") {
                EditObservation(user, observation, true);
            }
        }
    }
}