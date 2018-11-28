using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using deptagency.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace deptagency.Services.ServiceImpl
{
    public class OmdbSearchService : IMovieSearchService
    {
        private IConfiguration _configuration;
        private string omdbApiUrl;
        private string youTubeApiUrl;

        public OmdbSearchService(IConfiguration configuration)
        {
            _configuration = configuration;
            omdbApiUrl = _configuration.GetSection("ApiUrls").GetSection("MovieApiUrl").Value;
            youTubeApiUrl = _configuration.GetSection("ApiUrls").GetSection("TrailerApiUrl").Value;
        }

        public async Task<List<Movie>> FindMovie(string term)
        {
            List<Movie> movieList = new List<Movie>();

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(this.omdbApiUrl + "&s=" + term);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                JObject omdbSearch = JObject.Parse(data);
                IList<JToken> results = omdbSearch["Search"].ToList();
                
                foreach (JToken result in results)
                {
                    Movie movie = result.ToObject<Movie>();
                    movieList.Add(movie);
                }
            }
            return movieList;
        }
    }
}
