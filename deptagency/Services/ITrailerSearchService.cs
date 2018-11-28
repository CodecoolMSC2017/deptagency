using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using deptagency.Model;

namespace deptagency.Services
{
    public interface ITrailerSearchService
    {
        Task<List<Trailer>> FindTrailer(string movie);
    }
}
