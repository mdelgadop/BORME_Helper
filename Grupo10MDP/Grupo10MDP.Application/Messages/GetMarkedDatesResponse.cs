using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupo10MDP.Application.Messages
{
    public class GetMarkedDatesResponse
    {
        public GetMarkedDatesResponse()
        {
            Dates = new DateTime[0];
        }

        public DateTime[] Dates { get; set; }

        public bool Success { get; set; }
    }
}
