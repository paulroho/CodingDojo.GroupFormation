using System.Collections.Generic;
using System.Diagnostics;

namespace GroupForming
{
    public class GroupShuffler
    {
        private readonly IList<string> _members = new List<string>();
        private readonly IList<Group> _groups = new List<Group>();

        public void AddMember(string member)
        {
            _members.Add(member);
        }

        public void Shuffle()
        {
            int i = 0;
            Group group = null;
            foreach (var member in _members)
            {
                if (i % 2 == 0)
                {
                    group = new Group();
                    _groups.Add(group);
                }
                Debug.Assert(group != null, "The group has not be initialized.");
                group.AddMember(member);
                i++;
            }
        }

        public IList<Group> Groups { get { return _groups; } }
    }
}
