using Grupo10MDP.Application.Factories.Services;
using Grupo10MDP.Application.Messages;
using Grupo10MDP.Application.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Grupo10MDP.Test
{
    [TestClass]
    public class MarkingDatesTest
    {
        [TestMethod]
        public void MarkingOneDateTest()
        {
            string provincia = "ALBACETE";

            IBORMEService BORMEService = BORMEServiceFactory.CreateMock();
            MarkAsReadResponse response = BORMEService.MarkAsRead(new MarkAsReadRequest()
            {
                Date = new DateTime(2021, 7, 26),
                SelectedProvincia = provincia
            });

            GetMarkedDatesResponse datesResponse = BORMEService.GetMarkedDates(new GetMarkedDatesRequest()
            {
                SelectedProvincia = provincia
            });

            Assert.IsTrue(datesResponse.Success);
            Assert.IsNotNull(datesResponse.Dates);
            Assert.AreEqual(datesResponse.Dates.Length, 1);
        }

        [TestMethod]
        public void MarkingSeveralDatesTest()
        {
            string provincia = "ALBACETE";

            IBORMEService BORMEService = BORMEServiceFactory.CreateMock();

            #region Marking dates
            MarkAsReadResponse response = BORMEService.MarkAsRead(new MarkAsReadRequest()
            {
                Date = new DateTime(2021, 7, 19),
                SelectedProvincia = provincia
            });

            response = BORMEService.MarkAsRead(new MarkAsReadRequest()
            {
                Date = new DateTime(2021, 7, 20),
                SelectedProvincia = provincia
            });

            response = BORMEService.MarkAsRead(new MarkAsReadRequest()
            {
                Date = new DateTime(2021, 7, 21),
                SelectedProvincia = provincia
            });

            response = BORMEService.MarkAsRead(new MarkAsReadRequest()
            {
                Date = new DateTime(2021, 7, 22),
                SelectedProvincia = provincia
            });

            response = BORMEService.MarkAsRead(new MarkAsReadRequest()
            {
                Date = new DateTime(2021, 7, 23),
                SelectedProvincia = provincia
            });
            #endregion Marking dates

            GetMarkedDatesResponse datesResponse = BORMEService.GetMarkedDates(new GetMarkedDatesRequest()
            {
                SelectedProvincia = provincia
            });

            Assert.IsTrue(datesResponse.Success);
            Assert.IsNotNull(datesResponse.Dates);
            Assert.AreEqual(datesResponse.Dates.Length, 5);
        }

        [TestMethod]
        public void MarkingDateWithoutDateTest()
        {
            string provincia = "ALBACETE";

            IBORMEService BORMEService = BORMEServiceFactory.CreateMock();

            #region Marking dates
            MarkAsReadResponse response = BORMEService.MarkAsRead(new MarkAsReadRequest()
            {
                SelectedProvincia = provincia
            });

            #endregion Marking dates

            GetMarkedDatesResponse datesResponse = BORMEService.GetMarkedDates(new GetMarkedDatesRequest()
            {
                SelectedProvincia = provincia
            });

            Assert.IsFalse(response.Success);
            Assert.IsNotNull(datesResponse.Dates);
            Assert.AreEqual(datesResponse.Dates.Length, 0);
        }

        [TestMethod]
        public void MarkingDateWithoutProvinciaTest()
        {
            string provincia = string.Empty;

            IBORMEService BORMEService = BORMEServiceFactory.CreateMock();

            #region Marking dates
            MarkAsReadResponse response = BORMEService.MarkAsRead(new MarkAsReadRequest()
            {
                Date = new DateTime(2021, 7, 23),
                SelectedProvincia = provincia
            });

            #endregion Marking dates

            GetMarkedDatesResponse datesResponse = BORMEService.GetMarkedDates(new GetMarkedDatesRequest()
            {
                SelectedProvincia = provincia
            });

            Assert.IsFalse(response.Success);
            Assert.IsNotNull(datesResponse.Dates);
            Assert.AreEqual(datesResponse.Dates.Length, 0);
        }
    }
}
