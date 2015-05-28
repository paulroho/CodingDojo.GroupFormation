using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace GroupForming.Tests
{
    [TestClass]
    public class GroupShufflerTests
    {
        Fixture _fixture;
        GroupShuffler _shuffler;

        [TestInitialize]
        public void Setup()
        {
            _fixture = new Fixture();
            _shuffler = new GroupShuffler();
        }

        [TestMethod]
        public void Shuffle_WithMembersForJustOneGroup_GivesASingleGroupContainingAllMembers()
        {
            var members = _fixture.CreateMany("Member_", 2).ToList();
            _shuffler.AddMembers(members);

            // Act
            _shuffler.Shuffle();

            _shuffler.Groups.Should().HaveCount(1, "there should be a single group");
            _shuffler.Groups[0].Members.ShouldAllBeEquivalentTo(members);
        }

        [TestMethod]
        public void Shuffle_WithMembersForTwoGroups_GivesTwoProperlySizedGroupsContainingAllMembers()
        {
            var members = _fixture.CreateMany("Member_", 4).ToList();
            _shuffler.AddMembers(members);

            // Act
            _shuffler.Shuffle();

            _shuffler.Groups.Should().HaveCount(2, "there should be two groups");
            _shuffler.Groups.Should().OnlyContain(g => g.Members.Count() == 2, "every group should have 2 members");

            _shuffler.Groups.AllMembersAggregated().ShouldAllBeEquivalentTo(members);
        }
    }
}