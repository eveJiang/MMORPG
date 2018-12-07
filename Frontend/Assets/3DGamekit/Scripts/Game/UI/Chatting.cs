using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Gamekit3D;
using Assets._3DGamekit.Scripts.Game;

public class Chatting : MonoBehaviour
{
    public GameObject FriendInfo;
    public int partner;

    public void OnClickButton()
    {
        PlayerMyController.Instance.EnabledWindowCount++;
        string friendName = this.GetComponentInChildren<Text>().text;
        //var chatWindow = GameObject.FindObjectOfType<ChatUI>();
        //chatWindow.setFriendName(friendName);
        int id =  World.Instance.players[friendName];
        World.Instance.partner = id;
        this.partner = id;
        Debug.Log(string.Format("hey this is my partner. name {0} id {1}", friendName, World.Instance.partner));
    }

    private void Awake()
    {
    }
    // Use this for initialization
    void Start()
    {

    }

    private void OnEnable()
    {
        //Test();
        //initialize
        
    }

    private void OnDisable()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }
}
