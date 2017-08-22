using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Synnduit.TestHelper;

namespace Synnduit.Deduplication
{
    [TestClass]
    public class DuplicateTest
    {
        [TestMethod]
        public void Constructor_Throws_ArgumentNullException_When_DestinationSystemEntityId_Null()
        {
            ArgumentTester.ThrowsArgumentNullException(
                () => new Duplicate(null, MatchWeight.Inconsistent),
                "destinationSystemEntityId");
        }

        [TestMethod]
        public void Constructor_Throws_Argument_Exception_When_Weight_Invalid()
        {
            ArgumentTester.ThrowsArgumentException(
                () => new Duplicate(1, (MatchWeight) 57),
                "weight");
        }

        [TestMethod]
        public void DestinationSystemEntityId_Returns_DestinationSystemEntityId()
        {
            new Duplicate("abc", MatchWeight.Candidate)
                .DestinationSystemEntityId
                .Should()
                .Be((EntityIdentifier) "abc");
        }

        [TestMethod]
        public void Weight_Returns_Weight()
        {
            new Duplicate(5757L, MatchWeight.Candidate)
                .Weight
                .Should()
                .Be(MatchWeight.Candidate);
            new Duplicate("24ab", MatchWeight.Inconsistent)
                .Weight
                .Should()
                .Be(MatchWeight.Inconsistent);
            new Duplicate(24, MatchWeight.Positive)
                .Weight
                .Should()
                .Be(MatchWeight.Positive);
        }
    }
}
