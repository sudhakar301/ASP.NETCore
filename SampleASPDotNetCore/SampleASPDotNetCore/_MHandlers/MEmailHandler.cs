using MediatR;
using SampleASPDotNetCore._MNotifications;
using SampleASPDotNetCore.Data;

namespace SampleASPDotNetCore._MHandlers
{
    public class MEmailHandler : INotificationHandler<MProductAddedNotification>
    {

        private readonly MFakeDataStore _dataStore;
        public MEmailHandler(MFakeDataStore dataStore)
        {
            _dataStore = dataStore;
        }
        public async Task Handle(MProductAddedNotification notification, CancellationToken cancellationToken)
        {
            await _dataStore.EventOccured(notification.product, "Email sent !!");
            await Task.CompletedTask;
        }
    }
}
