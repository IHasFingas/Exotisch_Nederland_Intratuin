using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exotisch_Nederland_Intratuin.Model;

namespace Exotisch_Nederland_Intratuin.DAL {
    internal class SQLDAL {
        private static SQLDAL instance;
        private readonly string connectionString;
        private SqlConnection connection;
        private List<Area> areas;
        private List<Route> routes;
        private List<RoutePoint> routePoints;
        private List<POI> pointsOfInterest;
        private List<Game> games;
        private List<Question> questions;
        private List<Answer> answers;
        private List<User> users;
        private List<Role> roles;
        private List<Specie> species;
        private List<Observation> observations;

        private SQLDAL() {
            connectionString = "Data Source =.; Initial Catalog = Intratuin; Trusted_Connection = True";
            connection = new SqlConnection(connectionString);

            areas = GetAllAreas();
            roles = GetAllRoles();
            species = GetAllSpecies();
            routePoints = GetAllRoutePoints();

            routes = GetAllRoutes();
            pointsOfInterest = GetAllPOIs();

            users = GetAllUsers();
            games = GetAllGames();

            observations = GetALlObservations();
            questions = GetAllQuestions();

            answers = GetAllAnswers();
        }

        public static SQLDAL Instance {
            get {
                if (instance == null) {
                    instance = new SQLDAL();
                }
                return instance;
            }
        }

        public List<Area> GetAllAreas() {
            areas.Clear();
            connection.Open();

            string query = "SELECT * FROM Area";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                using (SqlDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        int id = (int)reader["ID"];
                        string name = (string)reader["Name"];
                        double size = (double)reader["Size"];

                        try {
                            areas.Add(new Area(id, name, size));
                        } catch (Exception e) {
                            Console.WriteLine($"Failed to create Area {id} from database");
                            Console.WriteLine(e.Message);
                        }
                    }
                }
            }

            connection.Close();

            return areas;
        }

        public List<Role> GetAllRoles() {
            roles.Clear();
            connection.Open();

            string query = "SELECT * FROM Role";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                using (SqlDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        int id = (int)reader["ID"];
                        string name = (string)reader["Name"];
                        string key = (string)reader["Key"];

                        try {
                            roles.Add(new Role(id, name, key));
                        } catch (Exception e) {
                            Console.WriteLine($"Failed to create Role {id} from database");
                            Console.WriteLine(e.Message);
                        }
                    }
                }
            }

            connection.Close();

            return roles;
        }

        public List<Specie> GetAllSpecies() {
            species.Clear();
            connection.Open();

            string query = "SELECT * FROM Specie";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                using (SqlDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        int id = (int)reader["ID"];
                        string name = (string)reader["Name"];
                        string domain = (string)reader["Domain"];
                        string regnum = (string)reader["Regnum"];
                        string phylum = (string)reader["Phylum"];
                        string classus = (string)reader["Classus"];
                        string ordo = (string)reader["Ordo"];
                        string familia = (string)reader["Familia"];
                        string genus = (string)reader["Genus"];

                        try {
                            species.Add(new Specie(id, domain, regnum, phylum, classus, ordo, familia, genus, name));
                        } catch (Exception e) {
                            Console.WriteLine($"Failed to create Specie {id} from database");
                            Console.WriteLine(e.Message);
                        }
                    }
                }
            }

            connection.Close();

            return species;
        }

        public List<RoutePoint> GetAllRoutePoints() {
            routePoints.Clear();
            connection.Open();

            string query = "SELECT * FROM RoutePoint";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                using (SqlDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        int id = (int)reader["ID"];
                        string name = (string)reader["Name"];
                        string location = (string)reader["Location"];

                        try {
                            routePoints.Add(new RoutePoint(id, name, location));
                        } catch (Exception e) {
                            Console.WriteLine($"Failed to create RoutePoint {id} from database");
                            Console.WriteLine(e.Message);
                        }
                    }
                }
            }

            connection.Close();

            return routePoints;
        }

        public List<Route> GetAllRoutes() {
            routes.Clear();
            connection.Open();

            string query = "SELECT * FROM Route";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                using (SqlDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        int id = (int)reader["ID"];
                        string name = (string)reader["Name"];
                        double length = (double)reader["Length"];
                        int areaID = (int)reader["Area_ID"];

                        foreach (Area area in areas) {
                            if (areaID == area.GetID()) {
                                try {
                                    routes.Add(new Route(id, name, length, area, new List<RoutePoint>(), new List<Game>()));
                                } catch (Exception e) {
                                    Console.WriteLine($"Failed to create Route {id} from database");
                                    Console.WriteLine(e.Message);
                                }
                            }
                        }
                    }
                }
            }

            connection.Close();

            return routes;
        }

        public List<POI> GetAllPOIs() {
            pointsOfInterest.Clear();
            connection.Open();

            string query = "SELECT * FROM POI";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                using (SqlDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        int id = (int)reader["ID"];
                        string name = (string)reader["Name"];
                        string location = (string)reader["Location"];
                        int routePointID = (int)reader["RoutePoint_ID"];

                        foreach (RoutePoint routePoint in routePoints) {
                            if (routePointID == routePoint.GetID()) {
                                try {
                                    pointsOfInterest.Add(new POI(id, name, location, routePoint));
                                } catch (Exception e) {
                                    Console.WriteLine($"Failed to create POI {id} from database");
                                    Console.WriteLine(e.Message);
                                }
                            }
                        }
                    }
                }
            }

            connection.Close();

            return pointsOfInterest;
        }

        public List<User> GetAllUsers() {
            users.Clear();
            connection.Open();

            string query = "SELECT * FROM User";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                using (SqlDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        int id = (int)reader["ID"];
                        string name = (string)reader["Name"];
                        string email = (string)reader["Email"];
                        string currentLocation = (string)reader["CurrentLocation"];
                        int routeID = (int)reader["Route_ID"];

                        foreach (Route route in routes) {
                            if (routeID == route.GetID()) {
                                try {
                                    users.Add(new User(id, name, email, currentLocation, route, new List<Role>()));
                                } catch (Exception e) {
                                    Console.WriteLine($"Failed to create User {id} from database");
                                    Console.WriteLine(e.Message);
                                }
                            }
                        }
                    }
                }
            }

            connection.Close();

            return users;
        }

        public List<Game> GetAllGames() {
            games.Clear();
            connection.Open();

            string query = "SELECT * FROM Game";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                using (SqlDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        int id = (int)reader["ID"];
                        string name = (string)reader["Name"];
                        string location = (string)reader["Location"];
                        string description = (string)reader["Description"];
                        int routeID = (int)reader["Route_ID"];

                        foreach (Route route in routes) {
                            if (routeID == route.GetID()) {
                                try {
                                    games.Add(new Game(id, name, location, description, route, new List<Question>()));
                                } catch (Exception e) {
                                    Console.WriteLine($"Failed to create Game {id} from database");
                                    Console.WriteLine(e.Message);
                                }

                                break;
                            }
                        }
                    }
                }
            }

            connection.Close();

            return games;
        }

        public List<Observation> GetALlObservations() {
            observations.Clear();
            connection.Open();

            string query = "SELECT * FROM Observation";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                using (SqlDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        int id = (int)reader["ID"];
                        string name = (string)reader["Name"];
                        string location = (string)reader["Location"];
                        string description = (string)reader["Description"];
                        //Image picture = (Image)reader["Picture"];
                        int specieID = (int)reader["Specie_ID"];
                        int areaID = (int)reader["Area_ID"];
                        int userID = (int)reader["User_ID"];

                        foreach (Specie specie in species) {
                            if (specieID == specie.GetID()) {

                                foreach (Area area in areas) {
                                    if (areaID == area.GetID()) {

                                        foreach (User user in users) {
                                            if (userID == user.GetID()) {
                                                try {
                                                    observations.Add(new Observation(id, name, location, description, specie, area, user));
                                                } catch (Exception e) {
                                                    Console.WriteLine($"Failed to create Observation {id} from database");
                                                    Console.WriteLine(e.Message);
                                                }

                                                break;
                                            }
                                        }

                                        break;
                                    }
                                }

                                break;
                            }
                        }
                    }
                }
            }

            connection.Close();

            return observations;
        }

        public List<Question> GetAllQuestions() {
            questions.Clear();
            connection.Open();

            string query = "SELECT * FROM Question";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                using (SqlDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        int id = (int)reader["ID"];
                        string questionText = (string)reader["QuestionText"];
                        int gameID = (int)reader["Game_ID"];

                        foreach (Game game in games) {
                            if (gameID == game.GetID()) {
                                try {
                                    questions.Add(new Question(id, questionText, game, new List<Answer>()));
                                } catch (Exception e) {
                                    Console.WriteLine($"Failed to create Question {id} from database");
                                    Console.WriteLine(e.Message);
                                }

                                break;
                            }
                        }
                    }
                }
            }

            connection.Close();

            return questions;
        }

        public List<Answer> GetAllAnswers() {
            answers.Clear();
            connection.Open();

            string query = "SELECT * FROM Answer";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                using (SqlDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        int id = (int)reader["ID"];
                        string answerText = (string)reader["AnswerText"];
                        int questionID = (int)reader["Question_ID"];

                        foreach (Question question in questions) {
                            if (questionID == question.GetID()) {
                                try {
                                    answers.Add(new Answer(id, answerText, question));
                                } catch (Exception e) {
                                    Console.WriteLine($"Failed to create Answer {id} from database");
                                    Console.WriteLine(e.Message);
                                }

                                break;
                            }
                        }
                    }
                }
            }

            connection.Close();

            return answers;
        }

        //public List<Tuple<int, int>> GetAllUserRoles() { }

        //public List<Tuple<int, int>> GetAllRouteRoutePoints() { }

        //public List<Tuple<int, int>> GetAllUserQuestions() { }

        public void AddArea(Area area) {
            connection.Open();

            string query = "INSERT INTO Area(Name, Size) VALUES (@Name, @Size)";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                command.Parameters.AddWithValue("@Name", area.GetName());
                command.Parameters.AddWithValue("@Size", area.GetSize());

                command.ExecuteNonQuery();

                command.CommandText = "SELECT CAST(@@Identity as INT);";
                area.SetID((int)command.ExecuteScalar());
            }

            connection.Close();
        }

        public void AddRole(Role role) {
            connection.Open();

            string query = "INSERT INTO Role(Name, Key) VALUES (@Name, @Key)";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                command.Parameters.AddWithValue("@Name", role.GetName());
                command.Parameters.AddWithValue("@Email", role.GetKey());

                command.ExecuteNonQuery();

                command.CommandText = "SELECT CAST(@@Identity as INT);";
                role.SetID((int)command.ExecuteScalar());
            }

            connection.Close();
        }

        public void AddSpecie(Specie specie) {
            connection.Open();

            string query = "INSERT INTO Specie(Name, Domain, Regnum, Phylum, Classus, Ordo, Familia, Genus) VALUES (@Name, @Domain, @Regnum, @Phylum, @Classus, @Ordo, @Familia, @Genus)";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                command.Parameters.AddWithValue("@Name", specie.GetName());
                command.Parameters.AddWithValue("@Domain", specie.GetDomain());
                command.Parameters.AddWithValue("@Regnum", specie.GetRegnum());
                command.Parameters.AddWithValue("@Phylum", specie.GetPhylum());
                command.Parameters.AddWithValue("@Classus", specie.GetClassus());
                command.Parameters.AddWithValue("@Ordo", specie.GetOrdo());
                command.Parameters.AddWithValue("@Familia", specie.GetFamilia());
                command.Parameters.AddWithValue("@Genus", specie.GetGenus());
                command.ExecuteNonQuery();

                command.CommandText = "SELECT CAST(@@Identity as INT);";
                specie.SetID((int)command.ExecuteScalar());
            }

            connection.Close();
        }

        public void AddRoutePoint(RoutePoint routePoint) {
            connection.Open();

            string query = "INSERT INTO RoutePoint(Name, Location) VALUES (@Name, @Location)";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                command.Parameters.AddWithValue("@Name", routePoint.GetName());
                command.Parameters.AddWithValue("@Location", routePoint.GetLocation());
                command.ExecuteNonQuery();

                command.CommandText = "SELECT CAST(@@Identity as INT);";
                routePoint.SetID((int)command.ExecuteScalar());
            }

            connection.Close();
        }

        public void AddRoute(Route route) {
            connection.Open();

            string query = "INSERT INTO Route(Name, Length, Area_ID) VALUES (@Name, @Length, @Area_ID)";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                command.Parameters.AddWithValue("@Name", route.GetName());
                command.Parameters.AddWithValue("@Length", route.GetLength());
                command.Parameters.AddWithValue("@Area_ID", route.GetArea().GetID());
                command.ExecuteNonQuery();

                command.CommandText = "SELECT CAST(@@Identity as INT);";
                route.SetID((int)command.ExecuteScalar());
            }

            connection.Close();
        }

        public void AddPOI(POI poi) {
            connection.Open();

            string query = "INSERT INTO POI(Name, Location, RoutePoint_ID) VALUES (@Name, @Location, @RoutePoint_ID)";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                command.Parameters.AddWithValue("@Name", poi.GetName());
                command.Parameters.AddWithValue("@Location", poi.GetLocation());
                command.Parameters.AddWithValue("@RoutePoint_ID", poi.GetRoutePoint().GetID());
                command.ExecuteNonQuery();

                command.CommandText = "SELECT CAST(@@Identity as INT);";
                poi.SetID((int)command.ExecuteScalar());
            }

            connection.Close();
        }

        public void AddUser(User user) {
            connection.Open();

            string query = "INSERT INTO User(Name, Email, CurrentLocation, Route_ID) VALUES (@Name, @Email, @CurrentLocation, @Route_ID)";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                command.Parameters.AddWithValue("@Name", user.GetName());
                command.Parameters.AddWithValue("@Email", user.GetEmail());
                command.Parameters.AddWithValue("@CurrentLocation", user.GetCurrentLocation());
                command.Parameters.AddWithValue("@Route_ID", user.GetRoute().GetID());

                command.ExecuteNonQuery();

                command.CommandText = "SELECT CAST(@@Identity as INT);";
                user.SetID((int)command.ExecuteScalar());
            }

            connection.Close();
        }

        public void AddGame(Game game) {
            connection.Open();

            string query = "INSERT INTO Game(Name, Location, Description, Route_ID) VALUES (@Name, @Location, @Description, @Route_ID)";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                command.Parameters.AddWithValue("@Name", game.GetName());
                command.Parameters.AddWithValue("@Location", game.GetLocation());
                command.Parameters.AddWithValue("@Description", game.GetDescription());
                command.Parameters.AddWithValue("@Route_ID", game.GetRoute().GetID());
                command.ExecuteNonQuery();

                command.CommandText = "SELECT CAST(@@Identity as INT);";
                game.SetID((int)command.ExecuteScalar());
            }

            connection.Close();
        }

        public void AddObservation(Observation observation) {
            connection.Open();

            string query = "INSERT INTO Observation(Name, Location, Description, Picture, Specie_ID, Area_ID, User_ID) VALUES (@Name, @Location, @Description, @Picture, @Specie_ID, @Area_ID, @User_ID)";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                command.Parameters.AddWithValue("@Name", observation.GetName());
                command.Parameters.AddWithValue("@Domain", observation.GetLocation());
                command.Parameters.AddWithValue("@Description", observation.GetDescription());
                command.Parameters.AddWithValue("@Picture", null);
                command.Parameters.AddWithValue("@Specie_ID", observation.GetSpecie().GetID());
                command.Parameters.AddWithValue("@Area_ID", observation.GetArea().GetID());
                command.Parameters.AddWithValue("@User_ID", observation.GetUser().GetID());

                //if(observation.GetPicture() != null) {
                //    command.Parameters.AddWithValue("@Picture", observation.GetPicture());
                //}

                command.ExecuteNonQuery();

                command.CommandText = "SELECT CAST(@@Identity as INT);";
                observation.SetID((int)command.ExecuteScalar());
            }

            connection.Close();
        }

        public void AddQuestion(Question question) {
            connection.Open();

            string query = "INSERT INTO Question(QuestionText, Game_ID) VALUES (@QuestionText, @Game_ID)";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                command.Parameters.AddWithValue("@QuestionText", question.GetQuestionText());
                command.Parameters.AddWithValue("@Game_ID", question.GetGame().GetID());
                command.ExecuteNonQuery();

                command.CommandText = "SELECT CAST(@@Identity as INT);";
                question.SetID((int)command.ExecuteScalar());
            }

            connection.Close();
        }

        public void AddAnswer(Answer answer) {
            connection.Open();

            string query = "INSERT INTO Answer(AnswerText, Question_ID) VALUES (@AnswerText, @Question_ID)";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                command.Parameters.AddWithValue("@Name", answer.GetAnswerText());
                command.Parameters.AddWithValue("@Domain", answer.GetQuestion().GetID());
                command.ExecuteNonQuery();

                command.CommandText = "SELECT CAST(@@Identity as INT);";
                answer.SetID((int)command.ExecuteScalar());
            }

            connection.Close();
        }

        public void AddRouteRoutePoint(Route route, RoutePoint routePoint) {
            connection.Open();

            string query = "INSERT INTO RouteRoutePoint(Route_ID, RoutePoint_ID) VALUES ((SELECT ID FROM Route WHERE Name = @RouteName), (SELECT ID FROM RoutePoint WHERE Name = @RoutePointName))";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                command.Parameters.AddWithValue("@RouteName", route.GetName());
                command.Parameters.AddWithValue("@RoutePointName", routePoint.GetName());
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }
}
