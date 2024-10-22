using Exotisch_Nederland_Intratuin.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace Exotisch_Nederland_Intratuin.DAL {
    internal class SQLDAL {
        private static SQLDAL instance;
        private readonly string connectionString = "Data Source =.; Initial Catalog = Intratuin; Trusted_Connection = True; MultipleActiveResultSets = True";
        private readonly SqlConnection connection;

        private List<Area> areas = new List<Area>();
        private List<Role> roles = new List<Role>();
        private List<Specie> species = new List<Specie>();
        private List<RoutePoint> routePoints = new List<RoutePoint>();

        private List<Route> routes = new List<Route>();
        private List<POI> pointsOfInterest = new List<POI>();

        private List<User> users = new List<User>();
        private List<Game> games = new List<Game>();

        private List<Observation> observations = new List<Observation>();
        private List<Question> questions = new List<Question>();

        private List<Answer> answers = new List<Answer>();


        // Static Instance attribute to ensure singleton
        public static SQLDAL Instance {
            get {
                if (instance == null) {
                    instance = new SQLDAL();
                }
                return instance;
            }
        }

        private SQLDAL() {
            connection = new SqlConnection(connectionString);
        }


        //Getting methods
        //Gets all objects currently in database and returns as List. Also updates corresponding object list
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

            List<Tuple<int, string, double, Area>> baseData = new List<Tuple<int, string, double, Area>>();

            using (SqlCommand command = new SqlCommand(query, connection)) {
                using (SqlDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        int id = (int)reader["ID"];
                        string name = (string)reader["Name"];
                        double length = (double)reader["Length"];
                        int areaID = (int)reader["Area_ID"];

                        foreach (Area area in areas) {
                            if (areaID == area.GetID()) {
                                baseData.Add(new Tuple<int, string, double, Area>(id, name, length, area));
                            }
                        }
                    }
                }
            }

            foreach (Tuple<int, string, double, Area> data in baseData) {
                try {
                    int id = data.Item1;
                    string name = data.Item2;
                    double length = data.Item3;
                    Area area = data.Item4;

                    routes.Add(new Route(id, name, length, area, GetRoutePointsForRoute(id), new List<Game>()));
                } catch (Exception e) {
                    Console.WriteLine($"Failed to create Route {data.Item1} from database");
                    Console.WriteLine(e.Message);
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

            string query = "SELECT * FROM [User]";

            List<Tuple<int, string, string, string, Route>> baseData = new List<Tuple<int, string, string, string, Route>>();

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
                                baseData.Add(new Tuple<int, string, string, string, Route>(id, name, email, currentLocation, route));
                            }
                        }
                    }
                }
            }

            foreach (Tuple<int, string, string, string, Route> data in baseData) {
                try {
                    int id = data.Item1;
                    string name = data.Item2;
                    string email = data.Item3;
                    string currentLocation = data.Item4;
                    Route route = data.Item5;

                    users.Add(new User(id, name, email, currentLocation, route, GetRolesForUser(id)));
                } catch (Exception e) {
                    Console.WriteLine($"Failed to create User {data.Item1} from database");
                    Console.WriteLine(e.Message);
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

        public List<Observation> GetAllObservations() {
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
                        bool correctAnswer = reader.GetBoolean(3);

                        foreach (Question question in questions) {
                            if (questionID == question.GetID()) {
                                try {
                                    answers.Add(new Answer(id, answerText, question, correctAnswer));
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

        private List<RoutePoint> GetRoutePointsForRoute(int routeID) {
            List<RoutePoint> routePointsForRoute = new List<RoutePoint>();

            using (SqlConnection secondConnection = new SqlConnection(connection.ConnectionString)) {
                secondConnection.Open();

                string query = "SELECT RoutePoint_ID FROM RouteRoutePoint WHERE Route_ID = @Route_ID";

                using (SqlCommand command = new SqlCommand(query, connection)) {
                    command.Parameters.AddWithValue("@Route_ID", routeID);

                    using (SqlDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            int routePointID = (int)reader["RoutePoint_ID"];

                            foreach (RoutePoint routePoint in this.routePoints) {
                                if (routePointID == routePoint.GetID()) {
                                    routePointsForRoute.Add(routePoint);
                                }
                            }
                        }
                    }
                }

                secondConnection.Close();
            }

            return routePointsForRoute;
        }

        private List<Role> GetRolesForUser(int userID) {
            List<Role> rolesForUser = new List<Role>();

            using (SqlConnection secondConnection = new SqlConnection(connection.ConnectionString)) {
                secondConnection.Open();

                string query = "SELECT Role_ID FROM UserRole WHERE User_ID = @User_ID";

                using (SqlCommand command = new SqlCommand(query, connection)) {
                    command.Parameters.AddWithValue("@User_ID", userID);

                    using (SqlDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            int roleID = (int)reader["Role_ID"];

                            foreach (Role role in this.roles) {
                                if (roleID == role.GetID()) {
                                    rolesForUser.Add(role);
                                }
                            }
                        }
                    }
                }

                secondConnection.Close();
            }

            return rolesForUser;
        }

        /*public List<Tuple<int, int>> GetAllUserQuestions() { }*/

        //Gets single object from internal list
        public Area GetAreaByID(int id) {
            GetAllAreas();

            foreach (Area area in areas) {
                if (area.GetID() == id) { return area; }
            }

            return null;
        }

        public Role GetRoleByID(int id) {
            GetAllRoles();

            foreach (Role role in roles) {
                if (role.GetID() == id) { return role; }
            }

            return null;
        }

        public Specie GetSpecieByID(int id) {
            GetAllSpecies();

            foreach (Specie specie in species) {
                if (specie.GetID() == id) { return specie; }
            }

            return null;
        }

        public Route GetRouteByID(int id) {
            GetAllRoutes();

            foreach (Route route in routes) {
                if (route.GetID() == id) { return route; }
            }

            return null;
        }

        public POI GetPOIByID(int id) {
            GetAllPOIs();

            foreach (POI poi in pointsOfInterest) {
                if (poi.GetID() == id) { return poi; }
            }

            return null;
        }

        public User GetUserByID(int id) {
            GetAllUsers();

            foreach (User user in users) {
                if (user.GetID() == id) { return user; }
            }

            return null;
        }

        public Game GetGameByID(int id) {
            GetAllGames();

            foreach (Game game in games) {
                if (game.GetID() == id) { return game; }
            }

            return null;
        }

        public Observation GetObservationByID(int id) {
            GetAllObservations();

            foreach (Observation observation in observations) {
                if (observation.GetID() == id) { return observation; }
            }

            return null;
        }

        public Question GetQuestionByID(int id) {
            GetAllQuestions();

            foreach (Question question in questions) {
                if (question.GetID() == id) { return question; }
            }

            return null;
        }

        public Answer GetAnswerByID(int id) {
            GetAllAnswers();

            foreach (Answer answer in answers) {
                if (answer.GetID() == id) { return answer; }
            }

            return null;
        }


        //Adding (Setting) methods
        //Add Object to the database and to corresponding area list in SQLDAL
        public void AddArea(Area area) {
            areas.Add(area);
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
            roles.Add(role);
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
            species.Add(specie);
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
            routePoints.Add(routePoint);
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
            routes.Add(route);
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
            pointsOfInterest.Add(poi);
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
            users.Add(user);
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
            games.Add(game);
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
            observations.Add(observation);
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
            questions.Add(question);
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
            answers.Add(answer);
            connection.Open();

            string query = "INSERT INTO Answer(AnswerText, Question_ID, CorrectAnswer) VALUES (@AnswerText, @Question_ID, @CorrectAnswer)";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                command.Parameters.AddWithValue("@AnswerText", answer.GetAnswerText());
                command.Parameters.AddWithValue("@Question_ID", answer.GetQuestion().GetID());
                command.Parameters.AddWithValue("@CorrectAnswer", answer.GetCorrectAnswer());
                command.ExecuteNonQuery();

                command.CommandText = "SELECT CAST(@@Identity as INT);";
                answer.SetID((int)command.ExecuteScalar());
            }

            connection.Close();
        }

        //Add entry to linking table for object1 and object2
        public void AddRouteRoutePoint(Route route, RoutePoint routePoint) {
            //connection.Open();

            //string query = "INSERT INTO RouteRoutePoint(Route_ID, RoutePoint_ID) VALUES (@Route_ID, @RoutePoint_ID)";

            //using (SqlCommand command = new SqlCommand(query, connection)) {
            //    command.Parameters.AddWithValue("@Route_ID", route.GetID());
            //    command.Parameters.AddWithValue("@RoutePoint_ID", routePoint.GetID());
            //    command.ExecuteNonQuery();
            //}

            //connection.Close();
        }

        public void AddUserRole(User user, Role role) {
            //connection.Open();

            //string query = "INSERT INTO UserRole(User_ID, Role_ID) VALUES (@User_ID, @Role_ID)";

            //using (SqlCommand command = new SqlCommand(query, connection)) {
            //    command.Parameters.AddWithValue("@User_ID", user.GetID());
            //    command.Parameters.AddWithValue("@Role_ID", role.GetID());
            //    command.ExecuteNonQuery();
            //}

            //connection.Close();
        }


        // Editing methods
        // Edits certain fields in given object
        public void EditArea(Area area) {
            connection.Open();

            string query = "UPDATE Area SET Name = @Name, Size = @Size WHERE Area_ID = @Area_ID";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                command.Parameters.AddWithValue("@Name", area.GetName());
                command.Parameters.AddWithValue("@Size", area.GetSize());
                command.Parameters.AddWithValue("@Area_ID", area.GetID());
                command.ExecuteNonQuery();
            }

            connection.Close();
        }

        public void EditRole(Role role) {
            connection.Open();

            string query = "UPDATE Role SET Name = @Name, Key = @Key WHERE Role_ID = @Role_ID";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                command.Parameters.AddWithValue("@Name", role.GetName());
                command.Parameters.AddWithValue("@Size", role.GetKey());
                command.Parameters.AddWithValue("@Role_ID", role.GetID());
                command.ExecuteNonQuery();
            }

            connection.Close();
        }

        public void EditSpecie(Specie specie) {
            connection.Open();

            string query = "UPDATE Specie SET Name = @Name, Domain = @Domain, Regnum = @Regnum, Phylum = @Phylum, Classus = @Classus, Ordo = @Ordo, Familia = @Familia, Genus = @Genus WHERE Specie_ID = @Specie_ID";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                command.Parameters.AddWithValue("@Name", specie.GetName());
                command.Parameters.AddWithValue("@Domain", specie.GetDomain());
                command.Parameters.AddWithValue("@Regnum", specie.GetRegnum());
                command.Parameters.AddWithValue("@Phylum", specie.GetPhylum());
                command.Parameters.AddWithValue("@Classus", specie.GetClassus());
                command.Parameters.AddWithValue("@Ordo", specie.GetOrdo());
                command.Parameters.AddWithValue("@Familia", specie.GetFamilia());
                command.Parameters.AddWithValue("@Genus", specie.GetGenus());
                command.Parameters.AddWithValue("@Specie_ID", specie.GetID());
                command.ExecuteNonQuery();
            }

            connection.Close();
        }

        public void EditRoutePoint(RoutePoint routePoint) {
            connection.Open();

            string query = "UPDATE RoutePoint SET Name = @Name, Location = @Location WHERE RoutePoint_ID = @RoutePoint_ID";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                command.Parameters.AddWithValue("@Name", routePoint.GetName());
                command.Parameters.AddWithValue("@Location", routePoint.GetLocation());
                command.Parameters.AddWithValue("@RoutePoint_ID", routePoint.GetID());
                command.ExecuteNonQuery();
            }

            connection.Close();
        }

        public void EditRoute(Route route) {
            connection.Open();

            string query = "UPDATE Route SET Name = @Name, Length = @Length, Area_ID = @Area_ID WHERE Route_ID = @Route_ID";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                command.Parameters.AddWithValue("@Name", route.GetName());
                command.Parameters.AddWithValue("@Length", route.GetLength());
                command.Parameters.AddWithValue("@Area_ID", route.GetArea().GetID());
                command.Parameters.AddWithValue("@Route_ID", route.GetID());
                command.ExecuteNonQuery();
            }

            query = "UPDATE RouteRoutePoint SET RoutePoint_ID = @RoutePoint_ID WHERE Route_ID = @Route_ID";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                command.Parameters.AddWithValue("@Route_ID", route.GetID());

                foreach (RoutePoint routePoint in route.GetRoutePoints()) {
                    command.Parameters.AddWithValue("@RoutePoint_ID", routePoint.GetID());
                    command.ExecuteNonQuery();
                }
            }

            connection.Close();
        }

        public void EditPOI(POI poi) {
            connection.Open();

            string query = "UPDATE POI SET Name = @Name, Location = @Location, RoutePoint_ID = @RoutePoint_ID WHERE POI_ID = @POI_ID";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                command.Parameters.AddWithValue("@Name", poi.GetName());
                command.Parameters.AddWithValue("@Location", poi.GetLocation());
                command.Parameters.AddWithValue("@RoutePoint_ID", poi.GetRoutePoint().GetID());
                command.Parameters.AddWithValue("@POI_ID", poi.GetID());
                command.ExecuteNonQuery();
            }

            connection.Close();
        }

        public void EditUser(User user) {
            connection.Open();

            string query = "UPDATE User SET Name = @Name, Email = @Email, CurrentLocation = @CurrentLocation, Route_ID = @Route_ID WHERE User_ID = @User_ID";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                command.Parameters.AddWithValue("@Name", user.GetName());
                command.Parameters.AddWithValue("@Email", user.GetEmail());
                command.Parameters.AddWithValue("@CurrentLocation", user.GetCurrentLocation());
                command.Parameters.AddWithValue("@Route_ID", user.GetRoute().GetID());
                command.Parameters.AddWithValue("@User_ID", user.GetID());
                command.ExecuteNonQuery();
            }

            query = "UPDATE UserRole SET Role_ID = @Role_ID WHERE User_ID = @User_ID";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                command.Parameters.AddWithValue("User_ID", user.GetID());

                foreach (Role role in user.GetRoles()) {
                    command.Parameters.AddWithValue("@Role_ID", role.GetID());
                    command.ExecuteNonQuery();
                }
            }

            connection.Close();
        }

        public void EditGame(Game game) {
            connection.Open();

            string query = "UPDATE Game SET Name = @Name, Location = @Location, Description = @Description, Route_ID = @Route_ID WHERE Game_ID = @Game_ID";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                command.Parameters.AddWithValue("@Name", game.GetName());
                command.Parameters.AddWithValue("@Location", game.GetLocation());
                command.Parameters.AddWithValue("@Description", game.GetDescription());
                command.Parameters.AddWithValue("@Route_ID", game.GetRoute().GetID());
                command.Parameters.AddWithValue("@Game_ID", game.GetID());
                command.ExecuteNonQuery();
            }

            connection.Close();
        }

        public void EditObservation(Observation observation) {
            connection.Open();

            string query = "UPDATE Observation SET Name = @Name, Location = @Location, Description = @Description, Specie_ID = @Specie_ID, Area_ID = Area_ID WHERE Observation_ID = @Observation_ID";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                command.Parameters.AddWithValue("@Name", observation.GetName());
                command.Parameters.AddWithValue("@Location", observation.GetLocation());
                command.Parameters.AddWithValue("@Description", observation.GetDescription());
                command.Parameters.AddWithValue("@Specie_ID", observation.GetSpecie().GetID());
                command.Parameters.AddWithValue("@Area_ID", observation.GetArea().GetID());
                command.Parameters.AddWithValue("@Observation_ID", observation.GetID());
                command.ExecuteNonQuery();
            }

            connection.Close();
        }

        public void EditQuestion(Question question) {
            connection.Open();

            string query = "UPDATE Question SET QuestionText = @QuestionText, Game_ID = @Game_ID WHERE Question_ID = @Question_ID";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                command.Parameters.AddWithValue("@QuestionText", question.GetQuestionText());
                command.Parameters.AddWithValue("@Game_ID", question.GetGame().GetID());
                command.Parameters.AddWithValue("@Question_ID", question.GetID());
                command.ExecuteNonQuery();
            }

            connection.Close();
        }

        public void EditAnswer(Answer answer) {
            connection.Open();

            string query = "UPDATE Question SET AnswerText = @AnswerText, Question_ID = @Question_ID, CorrectAnswer = @CorrectAnswer WHERE Question_ID = @Question_ID";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                command.Parameters.AddWithValue("@AnswerText", answer.GetAnswerText());
                command.Parameters.AddWithValue("@Question_ID", answer.GetQuestion().GetID());
                command.Parameters.AddWithValue("@CorrectAnswer", answer.GetCorrectAnswer());
                command.Parameters.AddWithValue("@Answer_ID", answer.GetID());
                command.ExecuteNonQuery();
            }

            connection.Close();
        }


        // Removing methods
        // Remove certain objects from database
        public void DeleteArea(Area area) {
            connection.Open();

            string query = "DELETE FROM Area WHERE Area_ID = @Area_ID";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                command.Parameters.AddWithValue("@Area_ID", area.GetID());
                command.ExecuteNonQuery();
            }

            connection.Close();
        }

        public void DeleteRole(Role role) {
            connection.Open();

            string query = "DELETE FROM Role WHERE Role_ID = @Role_ID";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                command.Parameters.AddWithValue("@Role_ID", role.GetID());
                command.ExecuteNonQuery();
            }

            connection.Close();
        }

        public void DeleteSpecie(Specie specie) {
            connection.Open();

            string query = "DELETE FROM Specie WHERE Specie_ID = @Specie_ID";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                command.Parameters.AddWithValue("@Specie_ID", specie.GetID());
                command.ExecuteNonQuery();
            }

            connection.Close();
        }

        public void DeleteRoutePoint(RoutePoint routePoint) {
            connection.Open();

            string query = "DELETE FROM RoutePoint WHERE RoutePoint_ID = @RoutePoint_ID";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                command.Parameters.AddWithValue("@RoutePoint_ID", routePoint.GetID());
                command.ExecuteNonQuery();
            }

            connection.Close();
        }

        public void DeleteRoute(Route route) {
            connection.Open();

            string query = "DELETE FROM Route WHERE Route_ID = @Route_ID";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                command.Parameters.AddWithValue("@Route_ID", route.GetID());
                command.ExecuteNonQuery();
            }

            query = "DELETE FROM RouteRoutePoint WHERE Route_ID = @Route_ID";
            using (SqlCommand command = new SqlCommand(query, connection)) {
                command.Parameters.AddWithValue("@Route_ID", route.GetID());
                command.ExecuteNonQuery();
            }

            connection.Close();
        }

        public void DeletePOI(POI poi) {
            connection.Open();

            string query = "DELETE FROM POI WHERE POI_ID = @POI_ID";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                command.Parameters.AddWithValue("@POI_ID", poi.GetID());
                command.ExecuteNonQuery();
            }

            connection.Close();
        }

        public void DeleteUser(User user) {
            connection.Open();

            string query = "DELETE FROM User WHERE User_ID = @User_ID";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                command.Parameters.AddWithValue("@User_ID", user.GetID());
                command.ExecuteNonQuery();
            }

            query = "DELETE FROM UserRole WHERE User_ID = @User_ID";
            using (SqlCommand command = new SqlCommand(query, connection)) {
                command.Parameters.AddWithValue("User_ID", user.GetID());
                command.ExecuteNonQuery();
            }

            connection.Close();
        }

        public void DeleteGame(Game game) {
            connection.Open();

            string query = "DELETE FROM Game WHERE Game_ID = @Game_ID";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                command.Parameters.AddWithValue("@Game_ID", game.GetID());
                command.ExecuteNonQuery();
            }

            connection.Close();
        }

        public void DeleteObservation(Observation observation) {
            connection.Open();

            string query = "DELETE FROM Observation WHERE Observation_ID = @Observation_ID";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                command.Parameters.AddWithValue("@Observation_ID", observation.GetID());
                command.ExecuteNonQuery();
            }

            connection.Close();
        }

        public void DeleteQuestion(Question question) {
            connection.Open();

            string query = "DELETE FROM Question WHERE Question_ID = @Question_ID";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                command.Parameters.AddWithValue("@Question_ID", question.GetID());
                command.ExecuteNonQuery();
            }

            connection.Close();
        }

        public void DeleteAnswer(Answer answer) {
            connection.Open();

            string query = "DELETE FROM Answer WHERE Answer_ID = @Answer_ID";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                command.Parameters.AddWithValue("@Answer_ID", answer.GetID());
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }
}