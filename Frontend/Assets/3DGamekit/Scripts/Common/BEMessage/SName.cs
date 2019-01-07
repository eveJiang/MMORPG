using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Common
{
	[Serializable]
	public class SName : Message
	{
		public SName() : base(Command.S_NAME) { }
		//public string name;
		//public int entityid;
		//public GameObject self;

	}
}