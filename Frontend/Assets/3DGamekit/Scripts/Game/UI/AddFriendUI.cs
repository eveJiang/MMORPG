using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Gamekit3D;
using System;
using UnityEngine.UI;
using Common;
using Assets._3DGamekit.Scripts.Game;
using Gamekit3D.Network;

public class AddFriendUI : MonoBehaviour
{
    public Sprite init;
    private Damageable m_damageable;
    private PlayerController m_controller;

    public void onClickAcc()
    {
        CAddFriend m = new CAddFriend();
        m.acc = true;
        m.request = World.Instance.friendview.dbID;
        m.response = World.Instance.selfDbid;
        m.option = "feedback";
        m.selfdbid = World.Instance.selfDbid;
        m.selfname = World.Instance.selfName;
        m.selfentityid = World.Instance.selfId;
        Client.Instance.Send(m);
    }

    public void onClickRej()
    {
        CAddFriend m = new CAddFriend();
        m.acc = false;
        m.request = World.Instance.friendview.dbID;
        m.response = World.Instance.selfDbid;
        m.option = "feedback";
        Client.Instance.Send(m);
    }

    public void onClickSend()
    {
        string name = GetComponentInChildren<InputField>().text;
        CAddFriend m = new CAddFriend();
        m.option = "send";
        m.selfdbid = World.Instance.selfDbid;
        m.name = name;
        m.message = GameObject.Find("SendMessage").GetComponentInChildren<InputField>().text;
        Client.Instance.Send(m);
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
        PlayerMyController.Instance.EnabledWindowCount++;
        if (m_controller == null || m_damageable == null)
        {
            m_controller = PlayerController.Mine;
            m_damageable = PlayerController.Mine.GetComponent<Damageable>();
        }
        GameObject.Find("AddFriendImage").GetComponent<Image>().sprite = init;
        GameObject.Find("AddFriendText").GetComponent<Text>().text = "Name";
        GameObject.Find("AddFriendInfo").GetComponent<Text>().text = "Message";
    }

    private void OnDisable()
    {
        PlayerMyController.Instance.EnabledWindowCount--;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void InitUI(PlayerController controller)
    {
        m_damageable = controller.GetComponent<Damageable>();
        m_controller = controller;
    }

}
