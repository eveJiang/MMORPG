using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
	[Serializable]
	public class CMinesGolds : Message
	{
		public CMinesGolds() : base(Command.C_MINESGOLDS) { }
		public List<Treasure> items = new List<Treasure>();
		public int dbid;
		public int gold_nums;
	}
}

