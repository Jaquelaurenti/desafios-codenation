using System;
using Xunit;
using Codenation.Challenge.Models;

namespace Codenation.Challenge
{
    public sealed class AccelerationModelTest : ModelBaseTest
    {
        public AccelerationModelTest()
            : base(new CodenationContext())
        {
            Model = "Codenation.Challenge.Models.Acceleration";
            Table = "acceleration";
        }

        [Fact]
        public void Should_Has_Table()
        {
            AssertTable();
        }

    }
}