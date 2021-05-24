using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReactRandomJokes.Data
{
    public class JokeRepository
    {
        private readonly string _connectionString;

        public JokeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void GetJoke(Joke joke)
        {
            using var ctx = new JokeSiteContext(_connectionString);
            ctx.Jokes.Add(joke);
            ctx.SaveChanges();
        }

        public Joke GetJokeById(int id)
        {
            using var ctx = new JokeSiteContext(_connectionString);
            return ctx.Jokes.Include(j => j.Likes).FirstOrDefault(j => j.Id == id);

        }

        //public List<UserLikedJoke> GetLikedJokes()
        //{
        //    using var ctx = new JokeSiteContext(_connectionString);
        //    return ctx.UserLikedJokes.ToList();
        //}
        public Joke AddJoke(Joke joke)
        {
            using var ctx = new JokeSiteContext(_connectionString);
            if (!ctx.Jokes.Any(j => j.ApiId == joke.ApiId))
            {
                ctx.Jokes.Add(joke);
                ctx.SaveChanges();
            }
            return ctx.Jokes.Include(j => j.Likes).FirstOrDefault(j => j.ApiId == joke.ApiId);
        }

        public Joke Like(UserLikedJoke userLikedJoke)
        {  
            using var ctx = new JokeSiteContext(_connectionString);
            Joke joke = ctx.Jokes.FirstOrDefault(j => j.Id == userLikedJoke.JokeId);
            if (!ctx.UserLikedJokes.Any(ul => ul.UserId == userLikedJoke.UserId && ul.JokeId == userLikedJoke.JokeId))
            {
                ctx.UserLikedJokes.Add(userLikedJoke);
                ctx.SaveChanges();
            }
            else
            {
                ctx.Database.ExecuteSqlInterpolated($"UPDATE UserLikedJokes SET Liked = ~Liked WHERE JokeId = {userLikedJoke.JokeId} AND userId = {userLikedJoke.UserId}");

            }
            return GetJokeById(userLikedJoke.JokeId);
        }
        public List<Joke> GetJokes()
        {
            using var ctx = new JokeSiteContext(_connectionString);
            return ctx.Jokes.Include(j => j.Likes).ToList();
        }
    }
}