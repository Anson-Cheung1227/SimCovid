using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using SimCovid.Core.Infection;
using SimCovidAPI;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.EditMode
{
    public class InfectionTests
    {
        [Test]
        public void InfectionFindExistingInstance()
        {
            InfectionManager infectionManager = new InfectionManager(100);
            ISpreadableDataHandler activeHandler = infectionManager.GetActive();
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
            ISpreadableDataHandler activeHandler = infectionManager.GetActive();
            ISpreadable a = activeHandler.CreateISpreadable();
            ISpreadable b = activeHandler.CreateISpreadable();
            a.SetActive(new DateTime(2020, 12, 1));
            b.SetActive(new DateTime(2020, 12, 1));
            bool isSameValue = a.IsSameValue(b);
            Assert.AreEqual(true, isSameValue);
        }
    }
}