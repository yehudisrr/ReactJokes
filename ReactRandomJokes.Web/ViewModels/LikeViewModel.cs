using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactRandomJokes.Web.ViewModels
{
    public class LikeViewModel
    {
        public int JokeId { get; set; }
        public bool Liked { get; set; }
    }
}
