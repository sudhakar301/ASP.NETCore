using MediatR;
using SampleASPDotNetCore._MCommands;
using SampleASPDotNetCore.Data;

namespace SampleASPDotNetCore._MHandlers
{
    public class AddMProductHandler : IRequestHandler<AddMProductCommand,MProduct>
    {
        private readonly MFakeDataStore _dataStore;

        public AddMProductHandler(MFakeDataStore dataStore)
        {
            _dataStore = dataStore;
        }
        public async Task<MProduct> Handle(AddMProductCommand request, CancellationToken cancellationToken)
        {

            await _dataStore.AddMProduct(request.mProduct);
            return request.mProduct;
        }
    }
}
