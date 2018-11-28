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
        readonly IProduceContentService _produceContent;

        public SearchController(IProduceContentService produceContentService)
        {
            _produceContent = produceContentService;
        }

        [HttpGet]
        [Route("{term}")]
        public ActionResult<List<ResultDto>> GetResult(string term)
        {
            return _produceContent.CreateContent(term).Result;
        }
    }
}
