using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ReactRandomJokes.Data
{
    public class UserLikedJoke
    {
        public int UserId { get; set; }
        public int JokeId { get; set; }

        [JsonIgnore]
        public User User { get; set; }
        [JsonIgnore]
        public Joke Joke { get; set; }

        public DateTime DateLiked { get; set; }
        public bool Liked { get; set; }
    }
}
