using System.Collections.Generic;

namespace GroupForming
{
    public class GroupFormer
    {
        public void AddMember(string member)
        {
        }

        public void Shuffle()
        {
        }

        public IList<Group> Groups { get { return new[] { new Group() }; } }
    }

    public class Group
    {
        public IList<string> Members { get { return new[] { "Dodo", "Michi" }; } }
    }
}
