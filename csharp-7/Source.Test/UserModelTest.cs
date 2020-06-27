using System;
using Xunit;
using Codenation.Challenge.Models;

namespace Codenation.Challenge
{
    public sealed class UserModelTest : ModelBaseTest
    {
        public UserModelTest()
            : base(new CodenationContext())
        {
            Model = "Codenation.Challenge.Models.User";
            Table = "user";
        }

        [Fact]
        public void Should_Has_Table()
        {
            AssertTable();
        }

    }
}