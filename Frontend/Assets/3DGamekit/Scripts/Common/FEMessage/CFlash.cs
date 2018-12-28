using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
	[Serializable]
	public class CFlash : Message
	{
		public CFlash() : base(Command.C_FLASH) { }
		public List<Treasure> items = new List<Treasure>();
		public int dbid;
		public int gold_nums;
	}
}
