using System;
using System.Collections.Generic;

namespace deptagency.Model
{
    public class ResultDto
    {
        public string Title { get; set; }
        public string ImdbRating { get; set; }
        public string RottenTomatoesRating { get; set; }
        public List<Trailer>[] TrailerList { get; set; }
     }
}
