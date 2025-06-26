using MediatR;
using SampleASPDotNetCore.Data;

namespace SampleASPDotNetCore._MCommands
{
    public record AddMProductCommand(MProduct mProduct) : IRequest<MProduct>; 
    
}
