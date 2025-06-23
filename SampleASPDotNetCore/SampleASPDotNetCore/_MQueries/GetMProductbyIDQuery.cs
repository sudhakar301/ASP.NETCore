using MediatR;
using SampleASPDotNetCore.Data;

namespace SampleASPDotNetCore._MQueries
{
    public record GetMProductbyIDQuery(int ID):IRequest<MProduct>;
   
}
