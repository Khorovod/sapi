using NUnit.Framework;
using SimpleAPI.Controllers;
using SimpleAPI.Extensions;
using SimpleAPI.Models;
using System.Threading.Tasks;

namespace SimpleAPI.UnitTests
{
    public class TimeTests
    {
        private TimeController controller;

        [SetUp]
        public void SetUp()
        {
            var factory = new Factory<ITime>(() => new Time());
            controller = new TimeController(factory);
        }

        [Test]
        public async Task TwoCurrentTimesAreNotEqual()
        {
            var res = await controller.GetCurrentTime();
            var res1 = await controller.GetCurrentTime();

            Assert.AreNotEqual(res, res1);
        }
    }
}
