using Grupo10MDP.Application.Factories.Services;
using Grupo10MDP.Application.Messages;
using Grupo10MDP.Application.Services.Interfaces;
using Grupo10MDP.Resources;
using Grupo10MDP.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Grupo10MDP.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Properties

        #region Combo Properties

        public IList<string> Provincias
        {
            get { return URLS.Keys.ToList(); }
        }

        private string _SelectedProvincia;
        public string SelectedProvincia
        {
            get { return _SelectedProvincia; }
            set
            {
                _SelectedProvincia = value;
                OnPropertyChanged("SelectedProvincia");
                OnPropertyChanged("CalendarStyle");
            }
        }

        private Dictionary<string, string> _urls;
        public Dictionary<string, string> URLS
        {
            get
            {
                if (_urls == null)
                    _urls = new Dictionary<string, string>();
                return _urls;
            }
            set
            {
                _urls = value;
                OnPropertyChanged("Provincias");
                OnPropertyChanged("URLS");
            }
        }

        #endregion Combo Properties

        #region Calendar Properties

        private DateTime _SelectedDate;
        public DateTime SelectedDate
        {
            get { return _SelectedDate; }
            set
            {
                _SelectedDate = value;
                UpdateDocumentsList();
                OnPropertyChanged("SelectedDate");
            }
        }

        private Dictionary<string, Color> _DatesRevised;
        private Dictionary<string, Color> DatesRevised
        {
            get
            {
                _DatesRevised = new Dictionary<string, Color>();
                
                GetMarkedDatesResponse response = BORMEService.GetMarkedDates(new GetMarkedDatesRequest()
                {
                    SelectedProvincia = SelectedProvincia
                });

                foreach(DateTime date in response.Dates)
                {
                    _DatesRevised.Add(date.ToString("MM/dd/yyyy"), Colors.Green);
                }

                return _DatesRevised;
            }
            set
            {
                _DatesRevised = value;
                OnPropertyChanged("DatesRevised");
            }
        }

        public Style CalendarStyle
        {
            get
            {
                Style _CalendarStyle = new Style(typeof(CalendarDayButton));

                if (!string.IsNullOrEmpty(SelectedProvincia))
                {
                    foreach (KeyValuePair<string, Color> item in DatesRevised)
                    {
                        DataTrigger trigger = new DataTrigger()
                        {
                            Value = item.Key,
                            Binding = new Binding("Date")
                        };
                        trigger.Setters.Add(new Setter(Control.BackgroundProperty, new SolidColorBrush(item.Value)));
                        _CalendarStyle.Triggers.Add(trigger);
                    }
                }
                
                return _CalendarStyle;
            }
            set
            {
                OnPropertyChanged("CalendarStyle");
            }
        }

        #endregion Calendar Properties

        private IBORMEService BORMEService { get; set; }

        public ICommand DownloadDocument { get; set; }

        #endregion Properties

        public MainWindowViewModel()
        {
            DownloadDocument = new RelayCommand(o => DownloadDocumentClick(null));
            BORMEService = BORMEServiceFactory.Create();
            SelectedDate = DateTime.Now.Date;
        }

        #region Methods

        private void DownloadDocumentClick(object sender)
        {
            if (!string.IsNullOrWhiteSpace(SelectedProvincia) && _SelectedDate > DateTime.MinValue)
            {
                System.Diagnostics.Process.Start(URLS[SelectedProvincia]);

                if (!DatesRevised.Keys.Contains(SelectedDate.ToString("MM/dd/yyyy")))
                {
                    BORMEService.MarkAsRead(new MarkAsReadRequest()
                    {
                        Date = SelectedDate,
                        SelectedProvincia = SelectedProvincia
                    });
                }

                OnPropertyChanged("CalendarStyle");
            }
            else
            {
                if(string.IsNullOrWhiteSpace(SelectedProvincia))
                {
                    MessageBox_Show(null, "Provincia es null", "Message", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                }
                else
                {
                    MessageBox_Show(null, "Fecha es null", "Message", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                }
            }
        }

        private void UpdateDocumentsList()
        {
            if (_SelectedDate > DateTime.MinValue)
            {
                GetProvinciasAvailableByDateRequest request = new GetProvinciasAvailableByDateRequest()
                {
                    Date = SelectedDate,
                    SelectedProvincia = SelectedProvincia
                };

                GetProvinciasAvailableByDateResponse response = BORMEService.GetProvinciasAvailableByDate(request);

                if (response.URLList.Count == 0)
                    MessageBox_Show(null, ScreenResources.DayOutOfRange, "Message", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);

                URLS = response.URLList;
                SelectedProvincia = response.SelectedProvincia;
            }
        }

        #endregion Methods
    }
}
