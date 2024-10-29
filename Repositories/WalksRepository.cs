using DogGo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NuGet.Protocol.Plugins;
using System.Collections.Generic;

namespace DogGo.Repositories
{
    public class WalksRepository : IWalksRepository
    {
        private readonly IConfiguration _config;

        // The constructor accepts an IConfiguration object as a parameter. This class comes from the ASP.NET framework and is useful for retrieving things out of the appsettings.json file like connection strings.
        public WalksRepository(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public List<Walks> GetAllWalks()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT w.Id, w.[Date], w.Duration, w.WalkerId, wr.Name AS 'Walker_Name', w.DogId, d.Name AS 'Dog_Name'
                        FROM Walks w
                        JOIN Walker wr ON wr.WalkerId = wr.
                        JOIN Dog d ON d.DogId = d.Id
                    ";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Walks> walks = new List<Walks>();
                    while (reader.Read())
                    {
                        Walks walk = new Walks
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            Duration = reader.GetTimeSpan(reader.GetOrdinal("Duration")),
                            WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                            Walker = new Walker
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                                Name = reader.GetString(reader.GetOrdinal("Walker_Name"))
                            },
                            DogId = reader.GetInt32(reader.GetOrdinal("DogId")),
                            Dog = new Dog
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("DogId")),
                                Name = reader.GetString(reader.GetOrdinal("Dog_Name"))
                            }
                        };

                        walks.Add(walk);
                    }

                    reader.Close();

                    return walks;
                }
            }
        }

        public Walks GetWalksById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT w.Id, w.[Date], w.Duration, w.WalkerId, wr.Name AS 'Walker_Name', w.DogId, d.Name AS 'Dog_Name'
                        FROM Walks w
                        JOIN Walker wr ON wr.WalkerId = wr.
                        JOIN Dog d ON d.DogId = d.Id
                        WHERE w.Id = @id
                    ";

                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Walks walk = new Walks
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            Duration = reader.GetTimeSpan(reader.GetOrdinal("Duration")),
                            WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                            Walker = new Walker
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                                Name = reader.GetString(reader.GetOrdinal("Walker_Name"))
                            },
                            DogId = reader.GetInt32(reader.GetOrdinal("DogId")),
                            Dog = new Dog
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("DogId")),
                                Name = reader.GetString(reader.GetOrdinal("Dog_Name"))
                            }
                        };

                        reader.Close();
                        return walk;
                    }
                    else
                    {
                        reader.Close();
                        return null;
                    }
                }
            }
        }
    }
}
