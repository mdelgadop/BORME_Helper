using Grupo10MDP.Business.Repositories;
using Grupo10MDP.Business.Repositories.Interfaces;
using Grupo10MDP.Business.Repositories.Mocks;

namespace Grupo10MDP.Business.Factories.Repositories
{
    public class DayMarkedBORMERepositoryFactory
    {
        public static IDayMarkedBORMERepository Create()
        {
            IDayMarkedBORMERepository element = new DayMarkedBORMERepository();
            return element;
        }

        public static IDayMarkedBORMERepository CreateMock()
        {
            IDayMarkedBORMERepository element = new DayMarkedBORMEMockRepository();
            return element;
        }
    }
}
