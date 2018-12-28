using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Common;
using Assets._3DGamekit.Scripts.Game;
using Gamekit3D.Network;
using Gamekit3D;

public class _SendPlace : MonoBehaviour
{
	//protected PlayerMyController m_myController;
	// Use this for initialization
	//public int type = 2;
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

	public void Send_try()
	{
		//m_myController = GetComponent<PlayerMyController>();
		//m_myController.SendFlash();
		//if (type == 1)
		//GameObject.Find("Ellen(Clone)").GetComponent<PlayerMyController>().SendFlash(56,17,29);
		//else if(type ==2)

		///======================
		//扣钱 不成功
			MessageBox.Show("You don't have enough money");

		//已经传送过
			MessageBox.Show("You have to wait to another enter");

		///======================
		MessageBox.Show("Enjoy your trip!");
		GameObject.Find("Ellen(Clone)").GetComponent<PlayerMyController>().SendFlash(1, 8, 120);
		Debug.Log("start send");
	}

}

