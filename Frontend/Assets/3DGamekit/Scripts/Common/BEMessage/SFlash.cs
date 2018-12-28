using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
	[Serializable]
	public class SFlash : Message
	{
		public SFlash() : base(Command.S_FLASH) { }
		public List<Treasure> items = new List<Treasure>();
		public bool success;
		public int gold;
		
	}
}
