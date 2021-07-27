using Grupo10MDP.Application.Factories.Services;
using Grupo10MDP.Application.Messages;
using Grupo10MDP.Application.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Grupo10MDP.Test
{
    [TestClass]
    public class BORMEServerTest
    {
        [TestMethod]
        public void DateCorrect()
        {
            IBORMEService BORMEService = BORMEServiceFactory.CreateMock();
            GetProvinciasAvailableByDateResponse response = BORMEService.GetProvinciasAvailableByDate(new GetProvinciasAvailableByDateRequest()
            {
                Date = new DateTime(2021, 7, 26)
            });

            Assert.IsNotNull(response.URLList);
            Assert.AreEqual(response.URLList.Count, 29);
            Assert.IsFalse(string.IsNullOrEmpty(response.SelectedProvincia));
        }

        [TestMethod]
        public void Weekend()
        {
            IBORMEService BORMEService = BORMEServiceFactory.CreateMock();
            GetProvinciasAvailableByDateResponse response = BORMEService.GetProvinciasAvailableByDate(new GetProvinciasAvailableByDateRequest()
            {
                Date = new DateTime(2021, 7, 24)
            });

            Assert.IsTrue(string.IsNullOrEmpty(response.SelectedProvincia));
            Assert.AreEqual(response.URLList.Count, 0);
        }

        [TestMethod]
        public void FutureDate()
        {
            //get next monday
            int daysToAdd = ((int)DayOfWeek.Monday - (int)DateTime.Now.DayOfWeek + 7) % 7;
            DateTime nextMonday = DateTime.Now.Date.AddDays(daysToAdd);

            IBORMEService BORMEService = BORMEServiceFactory.CreateMock();
            GetProvinciasAvailableByDateResponse response = BORMEService.GetProvinciasAvailableByDate(new GetProvinciasAvailableByDateRequest()
            {
                Date = nextMonday
            });

            Assert.IsTrue(string.IsNullOrEmpty(response.SelectedProvincia));
            Assert.AreEqual(response.URLList.Count, 0);
        }
    }
}
