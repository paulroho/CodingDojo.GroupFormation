using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace GroupForming.Tests
{
    [TestClass]
    public class GroupFormerTests
    {
        Fixture _fixture;
        GroupFormer _former;
        string _member1;
        string _member2;
        string _member3;
        string _member4;

        [TestInitialize]
        public void Setup()
        {
            _fixture = new Fixture();
            _former = new GroupFormer();
            _member1 = _fixture.Create("Member");
            _member2 = _fixture.Create("Member");
            _member3 = _fixture.Create("Member");
            _member4 = _fixture.Create("Member");
        }

        [TestMethod]
        public void Shuffle_WithMembersForJustOneGroup_GivesASingleGroupContainingAllMembers()
        {
            _former.AddMember(_member1);
            _former.AddMember(_member2);

            // Act
            _former.Shuffle();

            _former.Groups.Should().HaveCount(1, "there should be just a single group");
            _former.Groups.First().Members.ShouldAllBeEquivalentTo(new[] { _member1, _member2 });
        }

        [TestMethod]
        public void Shuffle_WithMembersForTwoGroups_GivesTwoProperlySizedGroupsContainingAllMembers()
        {
            _former.AddMember(_member1);
            _former.AddMember(_member2);
            _former.AddMember(_member3);
            _former.AddMember(_member4);

            // Act
            _former.Shuffle();

            _former.Groups.Should().HaveCount(2, "there should be two groups");
            _former.Groups.Should().OnlyContain(g => g.Members.Count() == 2, "every group should have 2 members");
            var allMembersOfAllGroups = _former.Groups.First().Members.Union(_former.Groups.Last().Members);
            allMembersOfAllGroups.ShouldAllBeEquivalentTo(new[] { _member1, _member2, _member3, _member4 });
        }
    }
}