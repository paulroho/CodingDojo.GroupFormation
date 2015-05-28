using System.Collections.Generic;

namespace GroupForming
{
    public class Group
    {
        private readonly List<string> _members = new List<string>();

        public Group(params string[] members)
        {
            _members.AddRange(members);
        }

        public IList<string> Members { get { return _members; } }

        public void AddMember(string member)
        {
            _members.Add(member);
        }
    }
}