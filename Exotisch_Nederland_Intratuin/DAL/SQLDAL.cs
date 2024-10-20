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

            pointsOfInterest = new List<POI>();
            routePoints = new List<RoutePoint>();
            routes = new List<Route>();

            questions = new List<Question>();
            answers = new List<Answer>();

            games = new List<Game>();

            roles = new List<Role>();
            users = new List<User>();

            species = GetAllSpecies();
            observations = new List<Observation>();
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

            return areas;
        }

        public List<Game> GetAllGames() {
            games.Clear();
            connection.Open();

            string query = "SELECT * FROM Game";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                using (SqlDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        string location = ""; //Can be null, so have to init
                        int routeID = -1; //Can be null (for now)

                        int id = (int)reader["ID"];
                        string name = (string)reader["Name"];
                        location = (string)reader["Location"];
                        string description = (string)reader["Description"];
                        routeID = (int)reader["Route"];

                        //Get correct route
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

            return games;
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

            return species;
        }

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
        }
    }
}
