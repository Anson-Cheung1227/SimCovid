using System;
using NUnit.Framework;
using SimCovid.Core.Infection;
using SimCovidAPI;
using SimCovidAPI.Infection;

namespace SimCovid.Tests.EditMode
{
    public class InfectionTests
    {
        [Test]
        public void InfectionFindExistingInstance()
        {
            InfectionManager infectionManager = new InfectionManager(100);
            ISpreadableDataHandler activeHandler = infectionManager.GetISpreadableDataHandler(InfectionStatus.Active.StatusTag);
            ISpreadable param = activeHandler.CreateISpreadable();
            param.AddToInfection(10);
            activeHandler.AddISpreadable(param);
            param = activeHandler.CreateISpreadable();
            bool found = activeHandler.FindExistingInstance(param) != null;
            Assert.AreEqual(true, found);
        }

        [Test]
        public void InfectionAreEquals()
        {
            InfectionManager infectionManager = new InfectionManager(100);
            ISpreadableDataHandler activeHandler = infectionManager.GetISpreadableDataHandler(InfectionStatus.Active.StatusTag);
            ISpreadable a = activeHandler.CreateISpreadable();
            ISpreadable b = activeHandler.CreateISpreadable();
            a.SetActive(new DateTime(2020, 12, 1));
            b.SetActive(new DateTime(2020, 12, 1));
            bool isSameValue = a.IsSameValue(b);
            Assert.AreEqual(true, isSameValue);
        }

        [Test]
        public void InfectionAreDifferent()
        {
            InfectionManager infectionManager = new InfectionManager(100);
            ISpreadableDataHandler activeHandler = infectionManager.GetISpreadableDataHandler(InfectionStatus.Active.StatusTag);
            ISpreadable a = activeHandler.CreateISpreadable();
            ISpreadable b = activeHandler.CreateISpreadable();
            a.SetActive(new DateTime(2020, 12, 1));
            b.SetActive(new DateTime(2020, 12, 1));
            b.SetInHospital(new DateTime(2020, 12, 1));
            bool isDifferentValue = !a.IsSameValue(b);
            Assert.AreEqual(true, isDifferentValue);
        }
    }
}