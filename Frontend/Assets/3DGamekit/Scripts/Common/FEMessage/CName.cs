using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Common
{
	[Serializable]
	public class CName: Message
	{
		public CName() : base(Command.C_NAME) { }
		//public List<Treasure> items = new List<Treasure>();
		//public int entityid;
		//public GameObject self;
	}
}