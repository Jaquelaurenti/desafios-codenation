using System;
using Xunit;
using Codenation.Challenge.Models;

namespace Codenation.Challenge
{
    public sealed class CandidateModelTest : ModelBaseTest
    {
        public CandidateModelTest()
            : base(new CodenationContext())
        {
            Model = "Codenation.Challenge.Models.Candidate";
            Table = "candidate";
        }

        [Fact]
        public void Should_Has_Table()
        {
            AssertTable();
        }

    }
}