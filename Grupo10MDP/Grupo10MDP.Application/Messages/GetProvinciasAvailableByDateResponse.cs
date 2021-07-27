using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupo10MDP.Application.Messages
{
    public class GetProvinciasAvailableByDateResponse
    {
        public GetProvinciasAvailableByDateResponse()
        {
            URLList = new Dictionary<string, string>();
        }

        public Dictionary<string, string> URLList { get; set; }

        public string SelectedProvincia { get; set; }
    }
}
