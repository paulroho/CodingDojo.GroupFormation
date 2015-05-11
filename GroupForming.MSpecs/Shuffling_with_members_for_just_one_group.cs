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
            Member1 = fixture.Create("Member1_");
            Member2 = fixture.Create("Member2_");

            Shuffler = new GroupShuffler();
            Shuffler.AddMember(Member1);
            Shuffler.AddMember(Member2);
        };

        Because of = () => Shuffler.Shuffle();

        It should_give_a_single_group = () => Shuffler.Groups.Should().HaveCount(1);

        It should_put_all_members_into_that_group = () =>
        {
            var members = Shuffler.Groups[0].Members;
            members.Should().Contain(Member1);
            members.Should().Contain(Member2);
        };

        private static string Member1 { get; set; }
        private static string Member2 { get; set; }

        private static GroupShuffler Shuffler { get; set; }
    }
}