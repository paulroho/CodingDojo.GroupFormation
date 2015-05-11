using System.Collections.Generic;
using System.Linq;

namespace GroupForming.MSpecs
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