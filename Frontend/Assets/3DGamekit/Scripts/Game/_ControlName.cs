using Assets._3DGamekit.Scripts.Game;
using Common;
using Gamekit3D.Network;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _ControlName : MonoBehaviour
{
	public GameObject self;
	// Use this for initialization
	void Start()
	{
		//this.gameObject.GetComponent<Text>().text = World.Instance.selfName;
		CName msg = new CName();
		//msg.entityid = World.Instance.selfId;
		//msg.self = self;
		Client.Instance.Send(msg);
		/*************/
	}

	// Update is called once per frame
	void Update()
	{
		transform.rotation = Camera.main.transform.rotation;
	}
}