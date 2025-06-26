using MediatR;
using SampleASPDotNetCore._MCaching;
using SampleASPDotNetCore.Data;

namespace SampleASPDotNetCore._MQueries
{
    public record GetMProductbyIDQuery(int ID) : IRequest<MProduct>,ICacheable
    {
        public string CacheKey => $"GetProductByID_{ID}";
    }
   
}
