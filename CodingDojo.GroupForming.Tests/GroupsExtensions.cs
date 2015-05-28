using System.Collections.Generic;
using System.Linq;

namespace CodingDojo.GroupForming.Tests
{
    public static class GroupsExtensions
    {
        public static IList<string> AllMembersAggregated(this IEnumerable<Group> groups)
        {
            return groups.Aggregate(new List<string>(), (ms, g) =>
            {
                ms.AddRange(g.Members);
                return ms;
            });
        }
    }
}