namespace BettingManager.Tests.Models
{
    using Logic.Models;
    using Logic.Common.Constants;
    using NUnit.Framework;

    [TestFixture]
    public class TipsterTests
    {
        private string name = "Angel";
        private TipsterCompany company = TipsterCompany.Myself;

        [Test]
        public void Constructor_ShouldPass_WithValidParams()
        {
            var tipster = new Tipster(this.name, this.company);

            Assert.IsInstanceOf<Tipster>(tipster);
        }
    }
}
