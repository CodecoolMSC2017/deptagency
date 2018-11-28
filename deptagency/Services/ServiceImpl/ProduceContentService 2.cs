using System.Collections.Generic;
using System.Threading.Tasks;
using deptagency.Model;

namespace deptagency.Services.ServiceImpl
{
    public class ProduceContentService : IProduceContentService
    {
        readonly IMovieSearchService _movieSearch;
        readonly ITrailerSearchService _trailerSearch;

        public ProduceContentService(IMovieSearchService movieSearch, ITrailerSearchService trailerSearch)
        {
            _movieSearch = movieSearch;
            _trailerSearch = trailerSearch;
        }

        public async Task<List<ResultDto>> CreateContent(string term)
        {
            List<ResultDto> results = new List<ResultDto>();
            List<Movie> movies = _movieSearch.FindMovie(term).Result;


            foreach (var movie in movies)
            {

                ResultDto resultDto = new ResultDto();
                resultDto.Title = movie.Title;
                resultDto.ImdbRating = movie.imdbID;

                resultDto.TrailerList = await Task.WhenAll(_trailerSearch.FindTrailer(movie.Title));

                results.Add(resultDto);
            }
            return results;
        }
    }
}
