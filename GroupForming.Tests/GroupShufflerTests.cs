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
        string _member1;
        string _member2;
        string _member3;
        string _member4;

        [TestInitialize]
        public void Setup()
        {
            _fixture = new Fixture();
            _shuffler = new GroupShuffler();
            _member1 = _fixture.Create("Member1_");
            _member2 = _fixture.Create("Member2_");
            _member3 = _fixture.Create("Member3_");
            _member4 = _fixture.Create("Member4_");
        }

        [TestMethod]
        public void Shuffle_WithMembersForJustOneGroup_GivesASingleGroupContainingAllMembers()
        {
            _shuffler.AddMember(_member1);
            _shuffler.AddMember(_member2);

            // Act
            _shuffler.Shuffle();

            _shuffler.Groups.ShouldAllBeEquivalentTo(new[]
            {
                new Group(_member1, _member2)
            });
        }

        [TestMethod]
        public void Shuffle_WithMembersForTwoGroups_GivesTwoProperlySizedGroupsContainingAllMembers()
        {
            _shuffler.AddMember(_member1);
            _shuffler.AddMember(_member2);
            _shuffler.AddMember(_member3);
            _shuffler.AddMember(_member4);

            // Act
            _shuffler.Shuffle();

            _shuffler.Groups.Should().HaveCount(2, "there should be two groups");
            _shuffler.Groups.Should().OnlyContain(g => g.Members.Count() == 2, "every group should have 2 members");

            _shuffler.Groups.AllMembersAggregated()
                .ShouldAllBeEquivalentTo(new[] {_member1, _member2, _member3, _member4});
        }
    }
}