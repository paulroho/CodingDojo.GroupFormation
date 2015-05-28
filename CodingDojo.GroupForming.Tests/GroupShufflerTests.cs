using System.Diagnostics;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace CodingDojo.GroupForming.Tests
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

        // Bugfix
        [TestMethod]
        public void Shuffle_WithMembersForJustOneGroupWhenCalledA2ndTime_StillGivesASingleGroup()
        {
            var members = _fixture.CreateMany("Member_", 2).ToList();
            _shuffler.AddMembers(members);

            // 1st shuffle
            _shuffler.Shuffle();
            Debug.Assert(_shuffler.Groups.Count == 1, "there should be just a single group");

            // Act
            _shuffler.Shuffle();

            _shuffler.Groups.Should().HaveCount(1, "there should be just a single group");
        }

        [TestMethod]
        public void Shuffle_WhenCalledA2ndTime_GivesADifferentResultsThanThe1stTime()
        {
            var members = _fixture.CreateMany("Member_", 2).ToList();
            _shuffler.AddMembers(members);

            // Reference shuffle
            _shuffler.Shuffle();

            var groups1stTime = _shuffler.Groups;
            Debug.Assert(groups1stTime.Count == 1, "there should be just a single group");
            var group1 = groups1stTime[0];
            var membersGroup1 = group1.Members;
            Debug.Assert(membersGroup1.Count == 2, "there should be two members");

            // Act
            _shuffler.Shuffle();

            var groups2ndTime = _shuffler.Groups;
            Debug.Assert(groups2ndTime.Count == 1, "there should be just a single group");
            var group2 = groups2ndTime[0];
            var membersGroup2 = group2.Members;
            Debug.Assert(membersGroup2.Count == 2, "there should be two members");

            membersGroup1[0].Should().NotBe(membersGroup2[0], "the first members should be different");
            membersGroup1[1].Should().NotBe(membersGroup2[1], "the second members should be different");
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