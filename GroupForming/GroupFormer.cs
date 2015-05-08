using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GroupForming
{
    public class GroupFormer
    {
        private readonly IList<string> _members = new List<string>();

        public void AddMember(string member)
        {
            _members.Add(member);
        }

        public void Shuffle()
        {
        }

        public IList<Group> Groups { get { return new[] { new Group(_members) }; } }
    }

    public class Group
    {
        private readonly List<string> _members = new List<string>();

        public Group(IEnumerable<string> members)
        {
            _members.AddRange(members);
        }

        public IList<string> Members { get { return _members; } }
    }
}
