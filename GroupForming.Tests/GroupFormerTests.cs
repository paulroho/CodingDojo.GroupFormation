using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace GroupForming.Tests
{
    [TestClass]
    public class GroupFormerTests
    {
        [TestMethod]
        public void Shuffle_WithMembersForJustOneGroup_GivesASingleGroupContainingAllMembers()
        {
            var fixture = new Fixture();
            var member1 = fixture.Create<string>("Member");
            var member2 = fixture.Create<string>("Member");
            var former = new GroupFormer();
            former.AddMember(member1);
            former.AddMember(member2);

            // Act
            former.Shuffle();

            former.Groups.Should().HaveCount(1, "there should be just a single group");
            former.Groups.First().Members.ShouldAllBeEquivalentTo(new[] { member1, member2 });
        }
    }
}