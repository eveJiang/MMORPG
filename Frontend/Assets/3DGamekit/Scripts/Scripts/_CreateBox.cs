using Assets._3DGamekit.Scripts.Game;
using Common;
using Gamekit3D;
using Gamekit3D.Network;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _CreateBox : MonoBehaviour {

	public GameObject box;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void Delete_try()
	{
		Debug.Log("delete_try");
		
		int probability = new System.Random().Next(1, 11);//
		if (probability > 5)
		{//
			GetGolds();
		}//

		//DestroyImmediate(box, true);
		else
		{
			MessageBox.Show(string.Format("The box is empty :（"));
		}
		Destroy(box, 1f);


	}
	void GetGolds()
	{
		Debug.Log("getgolds!");
		int sum = 1;
		foreach (var temp in World.Instance.intelligence)
		{
			sum += temp.Value.status;/////
		}
		int golds = 0;
		int basic = 0;
		int probability = new System.Random().Next(1, 11);
		if (probability < 10)
			basic = new System.Random().Next(1, 200);
		else
			basic new System.Random().Next(200, 1000);
		golds = (int)(sum * basic * 0.1);
		//golds = 20;//
		//Debug.Log("sum = ");//
		//print(sum);//
		CAddGolds m = new CAddGolds();
		m.dbid = World.Instance.selfDbid;
		//print(m.dbid);//2
		m.gold_nums = golds;
		Debug.Log("golds = ");
		print(m.gold_nums);
		Client.Instance.Send(m);
		MessageBox.Show(string.Format("You recieve {0} golds!", golds));
	}
}
	
