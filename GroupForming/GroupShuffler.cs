using System.Collections.Generic;
using System.Diagnostics;

namespace GroupForming
{
    public class GroupShuffler
    {
        private readonly List<string> _members = new List<string>();
        private readonly IList<Group> _groups = new List<Group>();
        private int _offset = 0;

        public void AddMember(string member)
        {
            _members.Add(member);
        }

        public void AddMembers(IEnumerable<string> members)
        {
            _members.AddRange(members);
        }

        public void Shuffle()
        {
            _groups.Clear();
            Group group = null;
            for (var i = 0; i < _members.Count; i++)
            {
                var member = _members[(i + _offset) % _members.Count];
                if (i%2 == 0)
                {
                    group = new Group();
                    _groups.Add(group);
                }
                Debug.Assert(group != null, "The group has not be initialized.");
                group.AddMember(member);
            }
            _offset++;
        }

        public IList<Group> Groups { get { return _groups; } }
    }
}
