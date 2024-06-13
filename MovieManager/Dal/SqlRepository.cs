using MovieManager.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MovieManager.Utils;
using System.IO;
using System.Windows.Input;

namespace MovieManager.Dal
{
    internal class SqlRepository : IRepository
    {
        // cannot be const -> using System.Configuration -> manual
        private static readonly string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        public void AddActor(Actor actor)
        {
            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue(nameof(Actor.ActorName), actor.ActorName);
            SqlParameter idActor = new(nameof(Actor.IdActor), SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(idActor);
            cmd.ExecuteNonQuery();
            actor.IdActor = (int)idActor.Value;
        }

        public void AddDirector(Director director)
        {
            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue(nameof(Director.DirectorName), director.DirectorName);
            SqlParameter idDirector = new(nameof(Director.IdDirector), SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(idDirector);
            cmd.ExecuteNonQuery();
            director.IdDirector = (int)idDirector.Value;
        }

        public void AddMovie(Movie movie)
        {
            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue(nameof(Movie.Title), movie.Title);
            cmd.Parameters.AddWithValue(nameof(Movie.MovieDescription), movie.MovieDescription);
            cmd.Parameters.AddWithValue(nameof(Movie.Duration), movie.Duration);
            cmd.Parameters.AddWithValue(nameof(Movie.Link), movie.Link);
            cmd.Parameters.Add(new SqlParameter(nameof(Movie.Picture), SqlDbType.Binary, movie.Picture!.Length)
            {
                Value = movie.Picture
            });
            SqlParameter idMovie = new(nameof(Movie.IdMovie), SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(idMovie);
            cmd.ExecuteNonQuery();
            movie.IdMovie = (int)idMovie.Value;
        }

        public void AddMovieActor(Movie movie, Actor actor)
        {
            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MovieId", movie.IdMovie);
            cmd.Parameters.AddWithValue(nameof(Actor.ActorName), actor.ActorName);
            SqlParameter idActor = new(nameof(Actor.IdActor), SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(idActor);
            cmd.ExecuteNonQuery();
            actor.IdActor = (int)idActor.Value;
        }

        public void AddMovieDirector(Movie movie, Director director)
        {
            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MovieId", movie.IdMovie);
            cmd.Parameters.AddWithValue(nameof(Director.DirectorName), director.DirectorName);
            SqlParameter idDirector = new(nameof(Director.IdDirector), SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(idDirector);
            cmd.ExecuteNonQuery();
            director.IdDirector = (int)idDirector.Value;
        }

        public void DeleteActor(Actor actor)
        {
            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue(nameof(Actor.IdActor), actor.IdActor);
            cmd.ExecuteNonQuery();
        }

        public void DeleteDirector(Director director)
        {
            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue(nameof(Director.IdDirector), director.IdDirector);
            cmd.ExecuteNonQuery();
        }

        public void DeleteMovie(Movie movie)
        {
            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MovieId", movie.IdMovie);
            cmd.ExecuteNonQuery();
        }

        public void DeleteMovieActors(Movie movie)
        {
            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MovieId", movie.IdMovie);
            cmd.ExecuteNonQuery();
        }

        public void DeleteMovieDirectors(Movie movie)
        {
            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MovieId", movie.IdMovie);
            cmd.ExecuteNonQuery();
        }

        public Actor GetActor(int idActor)
        {
            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue(nameof(Actor.IdActor), idActor);
            using SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                return ReadActor(dr);
            }
            throw new Exception("Actor does not exist");
        }

        public IList<Actor> GetActors()
        {
            IList<Actor> actors = new List<Actor>();
            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = CommandType.StoredProcedure;
            using SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                actors.Add(ReadActor(dr));
            }
            return actors;
        }

        public Director GetDirector(int idDirector)
        {
            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue(nameof(Director.IdDirector), idDirector);
            using SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                return ReadDirector(dr);
            }
            throw new Exception("Director does not exist");
        }

        public IList<Director> GetDirectors()
        {
            IList<Director> directors = new List<Director>();
            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = CommandType.StoredProcedure;
            using SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                directors.Add(ReadDirector(dr));
            }
            return directors;
        }

        public Movie GetMovie(int idMovie)
        {
            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue(nameof(Movie.IdMovie), idMovie);
            using SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                return ReadMovie(dr);
            }
            throw new Exception("Movie does not exist");
        }

        public IList<Movie> GetMovies()
        {
            IList<Movie> movies = new List<Movie>();
            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = CommandType.StoredProcedure;
            using SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                movies.Add(ReadMovie(dr));
            }
            return movies;
        }

        public void UpdateActor(Actor actor)
        {
            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue(nameof(Actor.ActorName), actor.ActorName);
            cmd.Parameters.AddWithValue(nameof(Actor.IdActor), actor.IdActor);
            cmd.ExecuteNonQuery();
        }

        public void UpdateDirector(Director director)
        {
            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue(nameof(Director.DirectorName), director.DirectorName);
            cmd.Parameters.AddWithValue(nameof(Director.IdDirector), director.IdDirector);
            cmd.ExecuteNonQuery();
        }

        public void UpdateMovie(Movie movie)
        {
            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue(nameof(Movie.Title), movie.Title);
            cmd.Parameters.AddWithValue(nameof(Movie.MovieDescription), movie.MovieDescription);
            cmd.Parameters.AddWithValue(nameof(Movie.Duration), movie.Duration);
            cmd.Parameters.AddWithValue(nameof(Movie.Link), movie.Link);
            cmd.Parameters.Add(new SqlParameter(nameof(Movie.Picture), SqlDbType.Binary, movie.Picture!.Length)
            {
                Value = movie.Picture
            });
            cmd.Parameters.AddWithValue(nameof(Movie.IdMovie), movie.IdMovie);
            cmd.ExecuteNonQuery();
        }

        public IList<Actor> GetMovieActors(int idMovie)
        {
            IList<Actor> actors = new List<Actor>();
            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue(nameof(Movie.IdMovie), idMovie);
            using SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                actors.Add(ReadActor(dr));
            }
            return actors;
        }

        public IList<Director> GetMovieDirectors(int idMovie)
        {
            IList<Director> directors = new List<Director>();
            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue(nameof(Movie.IdMovie), idMovie);
            using SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                directors.Add(ReadDirector(dr));
            }
            return directors;
        }

        private static Actor ReadActor(SqlDataReader dr) => new()
        {
            IdActor = (int)dr[nameof(Actor.IdActor)],
            ActorName = dr[nameof(Actor.ActorName)].ToString(),
        };

        private static Director ReadDirector(SqlDataReader dr) => new()
        {
            IdDirector = (int)dr[nameof(Director.IdDirector)],
            DirectorName = dr[nameof(Director.DirectorName)].ToString(),
        };

        private static Movie ReadMovie(SqlDataReader dr) => new()
        {
            IdMovie = (int)dr[nameof(Movie.IdMovie)],
            Title = dr[nameof(Movie.Title)].ToString(),
            MovieDescription = dr[nameof(Movie.MovieDescription)].ToString(),
            Duration = dr[nameof(Movie.Duration)].ToString(),
            Link = dr[nameof(Movie.Link)].ToString(),
            Picture = ImageUtils.ByteArrayFromSqlDataReader(dr, nameof(Movie.Picture))
        };

        public int ActorMovieRelated(int idActor)
        {
            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue(nameof(Actor.IdActor), idActor);
            SqlParameter returnCode = new("@returnCode", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(returnCode);
            cmd.ExecuteNonQuery();
            return (int)returnCode.Value;
        }

        public int DirectorMovieRelated(int idDirector)
        {
            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue(nameof(Director.IdDirector), idDirector);
            SqlParameter returnCode = new("@returnCode", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(returnCode);
            cmd.ExecuteNonQuery();
            return (int)returnCode.Value;
        }

        public void SetMovieActors(int movieId, int IdActor)
        {
            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@movieId", movieId);
            cmd.Parameters.AddWithValue(nameof(Actor.IdActor), IdActor);
            cmd.ExecuteNonQuery();
        }

        public void SetMovieDirectors(int movieId, int IdDirector)
        {
            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@movieId", movieId);
            cmd.Parameters.AddWithValue(nameof(Director.IdDirector), IdDirector);
            cmd.ExecuteNonQuery();
        }
    }
}
