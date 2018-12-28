using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
	[Serializable]
	public class CAddGolds : Message
	{
		public CAddGolds() : base(Command.C_ADDGOLDS) { }
		public List<Treasure> items = new List<Treasure>();
		public int dbid;
		public int gold_nums;
	}
}
