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
    }
}