using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupo10MDP.Business.Repositories.Interfaces
{
    public interface IDayMarkedBORMERepository
    {
        bool MarkAsRead(DateTime Date, string SelectedProvincia);

        DateTime[] GetMarkedDates(string SelectedProvincia);
    }
}
