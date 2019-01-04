using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class CAddFriend : Message
    {
        public CAddFriend() : base(Command.C_ADDFRIEND) { }
        public string option;
        public int response;
        public int request;
        public int selfdbid;  //自己的dbid
        public int selfentityid;  //自己的entityID
        public string selfname; //自己的名字
        public bool acc;    //是否同意对方的请求
        public string name; //对方的名字
        public string message; //给对方的消息
    }
}