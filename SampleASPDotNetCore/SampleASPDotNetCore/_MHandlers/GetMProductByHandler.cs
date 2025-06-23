using MediatR;
using SampleASPDotNetCore._MQueries;
using SampleASPDotNetCore.Data;

namespace SampleASPDotNetCore._MHandlers
{
    public class GetMProductByHandler: IRequestHandler<GetMProductbyIDQuery, MProduct>
    {
        private readonly MFakeDataStore _dataStore;
        public GetMProductByHandler(MFakeDataStore dataStore)
        {
            _dataStore = dataStore;
        }
        public async Task<MProduct> Handle(GetMProductbyIDQuery request, CancellationToken cancellationToken)
            => await _dataStore.GetMProductbyID(request.ID);
    }
   
}
