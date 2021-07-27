using Grupo10MDP.Application.Services;
using Grupo10MDP.Application.Services.Interfaces;
using Grupo10MDP.Business.Factories.Repositories;

namespace Grupo10MDP.Application.Factories.Services
{
    public class BORMEServiceFactory
    {
        public static IBORMEService Create()
        {
            IBORMEService service = new BORMEService(DayMarkedBORMERepositoryFactory.Create());
            return service;
        }

        public static IBORMEService CreateMock()
        {
            IBORMEService service = new BORMEService(DayMarkedBORMERepositoryFactory.CreateMock());
            return service;
        }
    }
}
