using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using deptagency.Model;

namespace deptagency.Services
{
    public interface IProduceContentService
    {
        Task<List<ResultDto>> CreateContent(string term);
    }
}
