using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Common;
using Assets._3DGamekit.Scripts.Game;
using Gamekit3D.Network;
using Gamekit3D;

public class _ExitControl : MonoBehaviour
{
	public GameObject box;
	public GameObject itself;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	/*
	
	public void Destroy_try()
	{
		Debug.Log("Destory the box and get golds");
		
		int probability = new System.Random().Next(1, 11);
		if (probability > 7)
		{
			GetGolds();
		}

		Destroy(box, 1.0f);



	}
	void GetGolds()
	{
		int sum = 0;
		foreach (var temp in World.Instance.intelligence)
		{
			sum += temp.Value.status;/////
		}
		int golds = 0;
		int basic = 0;

		basic = new System.Random().Next(0, 1000);
		golds = (int)(sum * basic * 0.2);

		CAddGolds m = new CAddGolds();
		m.dbid = World.Instance.selfDbid;
		m.gold_nums = golds;
		Client.Instance.Send(m);
	}

	*/

	public void Destroy_try()
	{
		Debug.Log("Destory the sprite and get golds");
		Vector3 position = gameObject.transform.position;
		//Vector3 position_ = new Vector3(9, 2, 73);
		Instantiate(box, position, Quaternion.identity);
		Destroy(itself, 1f);
		/*
		int probability = new System.Random().Next(1, 11);//
		if (probability > 5){//
			GetGolds();
		}//

		//DestroyImmediate(box, true);
		else{
			MessageBox.Show(string.Format("The box is empty :（"));
		}
		Destroy(itself, 1f);


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

		basic = new System.Random().Next(10, 1000);
		golds = (int)(sum * basic * 0.2);
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
	*/
	}
}
