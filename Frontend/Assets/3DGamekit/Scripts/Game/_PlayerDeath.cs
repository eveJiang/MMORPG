/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets._3DGamekit.Scripts.Game
{
	class _PlayerDeath
	{
	}
}
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Common;
using Assets._3DGamekit.Scripts.Game;
using Gamekit3D.Network;
using Gamekit3D;

public class _PlayerDeath : MonoBehaviour
{
	void Start()
	{

	}


	// Update is called once per frame
	void Update()
	{

	}



	public void Deal_Death()
	{
		MessageBox.Show("You dead");
		//1.获得打人的id
		int Aid = World.Instance._deathId;
		//2.是人杀死的id就不是-1
		if(Aid != -1)
		{
			World.Instance._deathId = -1;
			int golds;
			//3.计算战斗金币结算
			int sum = 1;
			foreach (var temp in World.Instance.intelligence)
			{
				sum += temp.Value.status;
			}
			int basic = 0;
			int probability = new System.Random().Next(1, 11);
			if(probability < 8)
				basic = new System.Random().Next(10, 200);
			else
				basic = new System.Random().Next(200, 1000);
			golds = (int) (sum * 0.2 * basic);

			//4.死亡玩家扣除金币
			CMinesGolds smsg = new CMinesGolds();
			smsg.Sdbid = World.Instance.selfDbid;
			smsg.gold_nums = golds;
			smsg.AentityId = Aid;
			Client.Instance.Send(smsg);
			//5.最后一击玩家获得金币，同时获得dbid
			/*
			CAddGolds amsg = new CAddGolds();
			amsg.dbid = Aid;
			amsg.gold_nums = golds;
			Client.Instance.Send(amsg);
			*/
		}
	}

}

