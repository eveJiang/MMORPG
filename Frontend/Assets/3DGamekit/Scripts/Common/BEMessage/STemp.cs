using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Common
{
	[Serializable]
	public class STemp : Message
	{
		public STemp() : base(Command.S_TEMP) { }
		public string name;
		public int entityid;
		//public GameObject self;
		///////////////////////////
	}
}