using MediatR;
using SampleASPDotNetCore.Data;

namespace SampleASPDotNetCore._MQueries
{
    public record GetMProductQuery:IRequest<IEnumerable<MProduct>>;
}
