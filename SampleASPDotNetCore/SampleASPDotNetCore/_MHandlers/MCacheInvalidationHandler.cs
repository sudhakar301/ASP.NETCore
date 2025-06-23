using MediatR;
using SampleASPDotNetCore._MNotifications;
using SampleASPDotNetCore.Data;

namespace SampleASPDotNetCore._MHandlers
{
    public class MCacheInvalidationHandler : INotificationHandler<MProductAddedNotification>
    {

        private readonly MFakeDataStore _dataStore;
        public MCacheInvalidationHandler(MFakeDataStore dataStore)
        {
            _dataStore = dataStore;
        }
        public async Task Handle(MProductAddedNotification notification, CancellationToken cancellationToken)
        {
            await _dataStore.EventOccured(notification.product, "Cache Invalidatied !!");
            await Task.CompletedTask;
        }
    }
}
