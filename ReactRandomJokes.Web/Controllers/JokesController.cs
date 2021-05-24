using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ReactRandomJokes.Data;
using ReactRandomJokes.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ReactRandomJokes.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JokesController : ControllerBase
    {
        private readonly string _connectionString;
        public JokesController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }
        [HttpGet]
        [Route("getjoke")]
        public Joke GetJoke()
        {
            var client = new HttpClient();
            var json = client.GetStringAsync(
                    $" https://official-joke-api.appspot.com/jokes/programming/random")
                .Result;

            var result = JsonConvert.DeserializeObject<List<Joke>>(json);
            var joke = result.FirstOrDefault();
            var repo = new JokeRepository(_connectionString);
            joke.ApiId = joke.Id;
            joke.Id = 0;
            return repo.AddJoke(joke);

        }

        [HttpGet]
        [Route("viewall")]
        public List<Joke> GetJokes()
        {
            var repo = new JokeRepository(_connectionString);
            return repo.GetJokes();
        }

        [HttpGet]
        [Route("getjokebyid")]
        public Joke GetJokeById(int id)
        {
            var repo = new JokeRepository(_connectionString);
            return repo.GetJokeById(id);
        }


        [HttpPost]
        [Route("like")]
        public Joke Like(LikeViewModel vm)
        {
            var user = GetCurrentUser();
            UserLikedJoke likedJoke = new UserLikedJoke
            {
                JokeId = vm.JokeId,
                UserId = user.Id,
                DateLiked = DateTime.Now,
                Liked = vm.Liked
            };
            var repo = new JokeRepository(_connectionString);
            return repo.Like(likedJoke);
           
        }

        private User GetCurrentUser()
        {
            var userRepo = new UserRepository(_connectionString);
            var user = userRepo.GetByEmail(User.Identity.Name);
            return user;
        }
    }
}
