using System;
using System.Collections.Generic;
using deptagency.Model;
using deptagency.Services;
using deptagency.Services.ServiceImpl;
using Microsoft.AspNetCore.Mvc;

namespace deptagency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        readonly ISearchService _searchService;
        readonly IProduceContentService _produceContent;

        public SearchController(IProduceContentService produceContentService, ISearchService searchService)
        {
            _searchService = searchService;
            _produceContent = produceContentService;
        }

        [HttpGet]
        [Route("movie/{term}")]
        public ActionResult<List<Movie>> GetMovie(String term)
        {
            List<Movie> movieList = _searchService.FindMovie(term).Result;

            if (movieList == null)
            {
                return NotFound("Movie not found");
            }
            return movieList;
        }

        [HttpGet]
        [Route("trailer/{term}")]
        public ActionResult<List<Trailer>> GetTrailer(String term)
        {
            List<Trailer> trailerList = _searchService.FindTrailer(term).Result;

            if (trailerList == null)
            {
                return NotFound("Trailer not found");
            }
            return trailerList;
        }

        [HttpGet]
        [Route("{term}")]
        public ActionResult<List<ResultDto>> GetResult(string term)
        {
            return _produceContent.CreateContent(term).Result;
        }
    }
}
