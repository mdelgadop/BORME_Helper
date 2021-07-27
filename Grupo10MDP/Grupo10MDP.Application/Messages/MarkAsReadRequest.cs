using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupo10MDP.Application.Messages
{
    public class MarkAsReadRequest
    {
        public DateTime Date { get; set; }

        public string SelectedProvincia { get; set; }
    }
}
