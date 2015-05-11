﻿using FluentAssertions;
using Machine.Specifications;
using Ploeh.AutoFixture;

namespace GroupForming.MSpecs
{
    [Subject("Group Shuffling")]
    public class Shuffling_with_members_for_two_groups
    {
        Establish context = () =>
        {
            Fixture = new Fixture();
            Shuffler = new GroupShuffler();
            Member1 = Fixture.Create("Member1_");
            Member2 = Fixture.Create("Member2_");
            Member3 = Fixture.Create("Member2_");
            Member4 = Fixture.Create("Member2_");
            Shuffler.AddMember(Member1);
            Shuffler.AddMember(Member2);
            Shuffler.AddMember(Member3);
            Shuffler.AddMember(Member4);
        };

        Because of = () => Shuffler.Shuffle();

        It should_give_two_groups = () => Shuffler.Groups.Should().HaveCount(2);

        It should_use_all_members_in_these_groups =
            () =>
                Shuffler.Groups.AllMembersAggregated()
                    .ShouldAllBeEquivalentTo(new[] {Member1, Member2, Member3, Member4});

        private static Fixture Fixture { get; set; }
        private static GroupShuffler Shuffler { get; set; }
        private static string Member1 { get; set; }
        private static string Member2 { get; set; }
        private static string Member3 { get; set; }
        private static string Member4 { get; set; }
    }
}