using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class SAddFriend : Message
    {
        public SAddFriend() : base(Command.S_ADDFRIEND) {items = new List<AddFriend>(); }
        public List<AddFriend> items;
        public string option;
    }
}
