using MediatR;
using SampleASPDotNetCore.Data;

namespace SampleASPDotNetCore._MNotifications
{
    public record MProductAddedNotification(MProduct product):INotification;
   
}
