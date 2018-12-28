using System;

namespace Common
{

	[Serializable]
	public class CPlayerFlash : Message
	{
		public CPlayerFlash() : base(Command.C_PLAYER_FLASH) { }
		public int player;
		public V3 pos;
		public V4 rot;
	}
}
