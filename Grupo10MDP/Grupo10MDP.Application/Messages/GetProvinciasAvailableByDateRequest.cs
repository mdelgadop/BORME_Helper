using System;

namespace Grupo10MDP.Application.Messages
{
    public class GetProvinciasAvailableByDateRequest
    {
        public DateTime Date { get; set; }

        public string SelectedProvincia { get; set; }
    }
}
