using BerlinClock.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TechTalk.SpecFlow;
using Unity;

namespace BerlinClock
{
    [Binding]
    public class TheBerlinClockSteps
    {
        private readonly IUnityContainer container = new UnityContainer();
        private ITimeConverter berlinClock;
        private String theTime;

        public TheBerlinClockSteps()
        {
            Initialize();

            berlinClock = container.Resolve<ITimeConverter>();
        }

        private void Initialize()
        {
            container.RegisterType<IClock, BerlinClock.Classes.BerlinClock>();
            container.RegisterType<ITimeConverter, TimeConverter>();
        }

        [When(@"the time is ""(.*)""")]
        public void WhenTheTimeIs(string time)
        {
            theTime = time;
        }

        [Then(@"the clock should look like")]
        public void ThenTheClockShouldLookLike(string theExpectedBerlinClockOutput)
        {
            Assert.AreEqual(berlinClock.ConvertTime(theTime), theExpectedBerlinClockOutput);
        }

    }
}
