using System.Collections.Generic;
using FluentAssertions;
using Machine.Specifications;
using Ploeh.AutoFixture;

namespace GroupForming.MSpecs
{
    [Subject("Group Shuffling")]
    public class Shuffling_with_members_for_two_groups
    {
        Establish context = () =>
        {
            var fixture = new Fixture();
            Members = fixture.CreateMany("Member_", 4);

            Shuffler = new GroupShuffler();
            Shuffler.AddMembers(Members);
        };

        Because of = () => Shuffler.Shuffle();

        It should_give_two_groups = () => Shuffler.Groups.Should().HaveCount(2);
        It should_balance_the_members_between_the_groups = () =>
        {
            Shuffler.Groups[0].Members.Should().HaveCount(2);
            Shuffler.Groups[1].Members.Should().HaveCount(2);
        };
        It should_use_all_members_in_these_groups = () => Shuffler.Groups.AllMembersAggregated().ShouldAllBeEquivalentTo(Members);

        private static IEnumerable<string> Members { get; set; }
        private static GroupShuffler Shuffler { get; set; }
    }
}