using Grupo10MDP.Application.Messages;
using Grupo10MDP.Application.Services.Interfaces;
using Grupo10MDP.Business.Repositories.Interfaces;
using System;
using System.Linq;
using System.Xml;

namespace Grupo10MDP.Application.Services
{
    public class BORMEService : IBORMEService
    {
        private IDayMarkedBORMERepository _dayMarkedBORMERepository { get; set; }

        public BORMEService(IDayMarkedBORMERepository dayMarkedBORMERepository)
        {
            _dayMarkedBORMERepository = dayMarkedBORMERepository;
        }

        public GetProvinciasAvailableByDateResponse GetProvinciasAvailableByDate(GetProvinciasAvailableByDateRequest request)
        {
            GetProvinciasAvailableByDateResponse response = new GetProvinciasAvailableByDateResponse();

            string prevSelectedProvincia = request.SelectedProvincia;
            try
            {
                String URLString = Resources.ApplicationResources.BORMEUrl + request.Date.ToString("yyyyMMdd");
                XmlTextReader reader = new XmlTextReader(URLString);
                string prev = string.Empty;
                string titulo = string.Empty;
                string url = string.Empty;
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element: // The node is an element.
                            prev = reader.Name;
                            break;

                        case XmlNodeType.Text: //Display the text in each element.
                            if (prev.Equals("titulo"))
                            {
                                titulo = reader.Value;
                            }
                            else if (prev.Equals("urlPdf"))
                            {
                                url = Resources.ApplicationResources.BOEUrl + reader.Value;

                                if (!string.IsNullOrWhiteSpace(titulo))
                                    response.URLList.Add(titulo, url);

                                titulo = string.Empty;
                                url = string.Empty;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception)
            {
                //
            }

            if(response.URLList != null && response.URLList.Keys.Count > 0)
            {
                if (!string.IsNullOrEmpty(prevSelectedProvincia) && response.URLList.Keys.Contains(prevSelectedProvincia))
                    response.SelectedProvincia = prevSelectedProvincia;
                else
                    response.SelectedProvincia = response.URLList.Keys.FirstOrDefault();
            }

            return response;
        }

        public MarkAsReadResponse MarkAsRead(MarkAsReadRequest request)
        {
            MarkAsReadResponse response = new MarkAsReadResponse();

            if(!string.IsNullOrEmpty(request.SelectedProvincia) && request.Date > DateTime.MinValue)
                response.Success = _dayMarkedBORMERepository.MarkAsRead(request.Date, request.SelectedProvincia);

            return response;
        }

        public GetMarkedDatesResponse GetMarkedDates(GetMarkedDatesRequest request)
        {
            GetMarkedDatesResponse response = new GetMarkedDatesResponse();

            DateTime[] dates = _dayMarkedBORMERepository.GetMarkedDates(request.SelectedProvincia);

            if (response.Dates != null)
            {
                response.Dates = dates;
                response.Success = true;
            }

            return response;
        }

    }
}
