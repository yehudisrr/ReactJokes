using System;
using System.Collections.Generic;
using System.Text;

namespace ReactRandomJokes.Data
{
    public class Joke
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public string Setup { get; set; }
        public string Punchline { get; set; }

        public int ApiId { get; set; }

        public List<UserLikedJoke> Likes { get; set; }

    }
}