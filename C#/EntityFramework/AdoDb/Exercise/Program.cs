using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Exercise
{
    class Program
    {
        const string SqlConnectionString = "Server=.;Database=MinionsDB;Integrated Security=true";

        static void Main(string[] args)
        {
            using (var connection = new SqlConnection(SqlConnectionString))
            {
                connection.Open();

                
            }
        }   
    
        private static void GetVilianAbdHisMinionsById(SqlConnection connection)
        {
            string vilianNameQuery = "SELECT Name FROM Villains WHERE Id = @Id";
            var id = int.Parse(Console.ReadLine());

            using var command = new SqlCommand(vilianNameQuery, connection);

            command.Parameters.AddWithValue("@Id", id);

            var vilianName = command.ExecuteScalar();

            string minionsQuery = @"SELECT ROW_NUMBER() OVER (ORDER BY m.Name) as RowNum,
                                         m.Name, 
                                         m.Age
                                   FROM MinionsVillains AS mv
                                   JOIN Minions As m ON mv.MinionId = m.Id
                                   WHERE mv.VillainId = @Id
                                ORDER BY m.Name";


            if (vilianName == null)
            {
                Console.WriteLine($"No vilian with ID {id} exists in the database.");
            }
            else
            {
                Console.WriteLine($"Vilian: {vilianName}");

                using (var minionsCommand = new SqlCommand(minionsQuery, connection))
                {
                    minionsCommand.Parameters.AddWithValue("@Id", id);

                    using (var reader = minionsCommand.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            Console.WriteLine("(no minions)");
                            return;
                        }

                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["RowNum"]}. {reader["Name"]} {reader["Age"]}");
                        }
                    }
                }
            }
        }

        private static void GetVilians(SqlConnection connection)
        {
            var statement = @"  SELECT v.Name, COUNT(mv.VillainId) AS MinionsCount  
                                    FROM Villains AS v 
                                    JOIN MinionsVillains AS mv ON v.Id = mv.VillainId 
                                    GROUP BY v.Id, v.Name 
                                    HAVING COUNT(mv.VillainId) > 3 
                                    ORDER BY COUNT(mv.VillainId)";

            using (var command = new SqlCommand(statement, connection))
            {
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var name = reader["Name"];
                    var minionsCount = reader["MinionsCount"];
                    Console.WriteLine($"{name} - {minionsCount}");
                }
            }
        }

        private static void InitialiseDatabase(SqlConnection connection)
        {
            var createTableStatements = GetCreateTableStatements();

            ExecuteNonQuery(createTableStatements, connection);

            var insertStatements = InsertDataStatements();

            ExecuteNonQuery(insertStatements, connection);
        }

        private static void ExecuteNonQuery(string[] statements, SqlConnection connection)
        {
            foreach (var statemet in statements)
            {
                using (var command = new SqlCommand(statemet, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        private static object ExecuteScalar(string query, SqlConnection connection, params KeyValuePair<string, string>[] keyValuePairs)
        {
            using var command = new SqlCommand(query, connection);

            foreach (var kvp in keyValuePairs)
            {
                command.Parameters.AddWithValue(kvp.Key, kvp.Value);
            }

            return command.ExecuteScalar();
        }


        private static string[] GetCreateTableStatements()
        {
            var result = new string[]
            {
                "CREATE TABLE Countries (Id INT PRIMARY KEY IDENTITY,Name VARCHAR(50))",

                "CREATE TABLE Towns(Id INT PRIMARY KEY IDENTITY,Name VARCHAR(50), CountryCode INT FOREIGN KEY REFERENCES Countries(Id))",

                "CREATE TABLE Minions(Id INT PRIMARY KEY IDENTITY,Name VARCHAR(30), Age INT, TownId INT FOREIGN KEY REFERENCES Towns(Id))",
    
                "CREATE TABLE EvilnessFactors(Id INT PRIMARY KEY IDENTITY, Name VARCHAR(50))",

                "CREATE TABLE Villains (Id INT PRIMARY KEY IDENTITY, Name VARCHAR(50), EvilnessFactorId INT FOREIGN KEY REFERENCES EvilnessFactors(Id))",

                "CREATE TABLE MinionsVillains (MinionId INT FOREIGN KEY REFERENCES Minions(Id),VillainId INT FOREIGN KEY REFERENCES Villains(Id),CONSTRAINT PK_MinionsVillains PRIMARY KEY (MinionId, VillainId))",
            };

            return result;
        }

        private static string[] InsertDataStatements()
        {
            var result = new string[]
            {
                "INSERT INTO Countries ([Name]) VALUES('Bulgaria'),('England'),('Cyprus'),('Germany'),('Norway')",

                "INSERT INTO Towns([Name], CountryCode) VALUES('Plovdiv', 1),('Varna', 1),('Burgas', 1),('Sofia', 1),('London', 2),('Southampton', 2),('Bath', 2),('Liverpool', 2),('Berlin', 3),('Frankfurt', 3),('Oslo', 4)",

                "INSERT INTO Minions(Name, Age, TownId) VALUES('Bob', 42, 3),('Kevin', 1, 1),('Bob ', 32, 6),('Simon', 45, 3),('Cathleen', 11, 2),('Carry ', 50, 10),('Becky', 125, 5),('Mars', 21, 1),('Misho', 5, 10),('Zoe', 125, 5),('Json', 21, 1)",

                "INSERT INTO EvilnessFactors(Name) VALUES('Super good'),('Good'),('Bad'), ('Evil'),('Super evil')",

                "INSERT INTO Villains(Name, EvilnessFactorId) VALUES('Gru', 2),('Victor', 1),('Jilly', 3),('Miro', 4),('Rosen', 5),('Dimityr', 1),('Dobromir', 2)",

                "INSERT INTO MinionsVillains(MinionId, VillainId) VALUES(4, 2),(1, 1),(5, 7),(3, 5),(2, 6),(11, 5),(8, 4),(9, 7),(7, 1),(1, 3),(7, 3),(5, 3),(4, 3),(1, 2),(2, 1),(2, 7)"
            };

            return result;
        }
    }
}
