using System;
using Xunit;
using Codenation.Challenge.Models;

namespace Codenation.Challenge
{
    public sealed class SubmissionModelTest : ModelBaseTest
    {
        public SubmissionModelTest()
            : base(new CodenationContext())
        {
            Model = "Codenation.Challenge.Models.Submission";
            Table = "submission";
        }

        [Fact]
        public void Should_Has_Table()
        {
            AssertTable();
        }

    }
}