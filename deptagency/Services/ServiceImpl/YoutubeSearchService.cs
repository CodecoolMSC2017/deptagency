using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using deptagency.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace deptagency.Services.ServiceImpl
{
    public class YouTubeSearchService : ITrailerSearchService
    {
        private IConfiguration _configuration;
        private string youTubeApiUrl;

        public YouTubeSearchService(IConfiguration configuration)
        {
            _configuration = configuration;
            youTubeApiUrl = _configuration.GetSection("ApiUrls").GetSection("TrailerApiUrl").Value;
        }

        public async Task<List<Trailer>> FindTrailer(string term)
        {
            List<Trailer> trailerList = new List<Trailer>();

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(this.youTubeApiUrl + "&part=snippet&q=" + term);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                JObject youTubeSearch = JObject.Parse(data);
                IList<JToken> results = youTubeSearch["items"].Children().ToList();

                foreach (JToken result in results)
                {
                    Trailer trailer = new Trailer
                    {
                        VideoId = result["id"]["videoId"].ToString(),
                        Title = result["snippet"]["title"].ToString(),
                        ChannelId = result["snippet"]["channelId"].ToString(),
                        ChannelTitle = result["snippet"]["channelTitle"].ToString()

                    };
                    trailerList.Add(trailer);
                }
            }
            return trailerList;
        }
    }
}
