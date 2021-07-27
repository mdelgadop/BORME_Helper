using Grupo10MDP.Business.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupo10MDP.Business.Repositories.Mocks
{
    public class DayMarkedBORMEMockRepository : IDayMarkedBORMERepository
    {
        private IList<MARKED_DATES> MARKED_DATES { get; set; }

        public DayMarkedBORMEMockRepository()
        {
            MARKED_DATES = new List<MARKED_DATES>();
        }

        public DateTime[] GetMarkedDates(string SelectedProvincia)
        {
            if (MARKED_DATES.Any(md => md.Provincia.Equals(SelectedProvincia)))
                return MARKED_DATES.Where(md => md.Provincia.Equals(SelectedProvincia)).Select(md => md.Dates).FirstOrDefault().ToArray();
            else
                return new DateTime[0];
        }

        public bool MarkAsRead(DateTime Date, string SelectedProvincia)
        {
            if (MARKED_DATES.Any(md => md.Dates.Contains(Date.Date) && md.Provincia.Equals(SelectedProvincia)))
            {
                return false;
            }
            else if (MARKED_DATES.Any(md => md.Provincia.Equals(SelectedProvincia)))
            {
                MARKED_DATES curr = MARKED_DATES.Where(md => md.Provincia.Equals(SelectedProvincia)).FirstOrDefault();
                curr.Dates.Add(Date.Date);
                return true;
            }
            else
            {
                MARKED_DATES curr = new MARKED_DATES()
                {
                    Provincia = SelectedProvincia,
                    Dates = new List<DateTime>()
                };
                curr.Dates.Add(Date.Date);

                MARKED_DATES.Add(curr);

                return true;
            }
        }
    }

    public class MARKED_DATES
    {
        public IList<DateTime> Dates { get; set; }

        public string Provincia { get; set; }
    }
}
