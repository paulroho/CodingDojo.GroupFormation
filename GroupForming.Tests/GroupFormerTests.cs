using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GroupForming.Tests
{
    [TestClass]
    public class GroupFormerTests
    {
        [TestMethod]
        public void Shuffle_WithMembersForJustOneGroup_GivesASingleGroupContainingAllMembers()
        {
            var former = new GroupFormer();
            former.AddMember("Michi");
            former.AddMember("Dodo");

            // Act
            former.Shuffle();

            former.Groups.Should().HaveCount(1, "there should be just a single group");
            former.Groups.First().Members.ShouldAllBeEquivalentTo(new[] { "Michi", "Dodo" });
        }

        [TestMethod]
        public void Shuffle_WithMembersForTwoGroups_GivesTwoProperlySizedGroupsContainingAllMembers()
        {
            var former = new GroupFormer();
            former.AddMember("Sepp");
            former.AddMember("Otto");
            former.AddMember("Karl");
            former.AddMember("Susi");

            // Act
            former.Shuffle();

            former.Groups.Should().HaveCount(2, "there should be two groups");
            former.Groups.Should().OnlyContain(g => g.Members.Count() == 2, "every group should have 2 members");
            var allMembersOfAllGroups = former.Groups.First().Members.Union(former.Groups.Last().Members);
            allMembersOfAllGroups.ShouldAllBeEquivalentTo(new[] {"Sepp", "Otto", "Karl", "Susi"});
        }
    }
}