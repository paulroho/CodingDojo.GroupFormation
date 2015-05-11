using System.Collections.Generic;
using FluentAssertions;
using Machine.Specifications;
using Ploeh.AutoFixture;

namespace GroupForming.MSpecs
{
    [Subject("GroupShuffling")]
    public class Shuffling_with_members_for_just_one_group
    {
        Establish context = () =>
        {
            var fixture = new Fixture();
            Members = fixture.CreateMany("Member_", 2);

            Shuffler = new GroupShuffler();
            Shuffler.AddMembers(Members);
        };

        Because of = () => Shuffler.Shuffle();

        It should_give_a_single_group = () => Shuffler.Groups.Should().HaveCount(1);

        It should_put_all_members_into_that_group = () => Shuffler.Groups[0].Members.ShouldAllBeEquivalentTo(Members);

        private static IEnumerable<string> Members { get; set; }
        private static GroupShuffler Shuffler { get; set; }
    }
}