using Gamekit3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _BackToPlace : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void Back()
	{
		MessageBox.Show("你通过了试炼！");
		GameObject.Find("Ellen(Clone)").GetComponent<PlayerMyController>().SendFlash(56, 17, 29);
	}
}
	
