using Grupo10MDP.Application.Messages;

namespace Grupo10MDP.Application.Services.Interfaces
{
    public interface IBORMEService
    {
        GetProvinciasAvailableByDateResponse GetProvinciasAvailableByDate(GetProvinciasAvailableByDateRequest request);

        MarkAsReadResponse MarkAsRead(MarkAsReadRequest request);

        GetMarkedDatesResponse GetMarkedDates(GetMarkedDatesRequest request);
    }
}
