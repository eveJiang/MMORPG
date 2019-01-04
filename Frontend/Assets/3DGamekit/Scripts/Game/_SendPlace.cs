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
	public int nums = 500;
	public GameObject close_UI_1;
	public GameObject close_UI_2;

	void Start()
	{
		
	}


	// Update is called once per frame
	void Update()
	{

	}



	public void Send_try()
	{
		MessageBox.Show(string.Format("Wait...."));
		Debug.Log("start send_try");
		nums = 500;
		CFlash message = new CFlash();
		message.dbid = World.Instance.selfDbid;
		message.gold_nums = nums;
		Client.Instance.Send(message);

		//Debug.Log("start send_try");
		close_UI_1.SetActive(false);
		close_UI_2.SetActive(false);
	}

}

