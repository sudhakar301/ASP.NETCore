using MediatR;
using SampleASPDotNetCore._MQueries;
using SampleASPDotNetCore.Data;

namespace SampleASPDotNetCore._MHandlers
{
    public class GetMProductsHandler:IRequestHandler<GetMProductQuery, IEnumerable<MProduct>>
    {
        private readonly MFakeDataStore _dataStore;
        public GetMProductsHandler(MFakeDataStore dataStore)
        {
            _dataStore = dataStore;
        }
        public async Task<IEnumerable<MProduct>> Handle(GetMProductQuery request, CancellationToken cancellationToken)
            => await _dataStore.GetAllMProducts();
    }
   
}
