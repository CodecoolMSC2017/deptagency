using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using deptagency.Model;

namespace deptagency.Services
{
    public interface IMovieSearchService
    {
        Task<List<Movie>> FindMovie(string movieTitle);
    }
}
