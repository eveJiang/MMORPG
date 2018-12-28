using System;

namespace Common
{

	[Serializable]
	public class SPlayerFlash : Message
	{
		public SPlayerFlash() : base(Command.S_PLAYER_FLASH) { }
		public int ID;
		public V3 pos;
		public V4 rot;
	}
}
