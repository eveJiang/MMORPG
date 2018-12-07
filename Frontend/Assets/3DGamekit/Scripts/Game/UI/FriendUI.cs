using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Gamekit3D;
using Assets._3DGamekit.Scripts.Game;

public class FriendUI : MonoBehaviour
{
    private List<GameObject> friendObjects = new List<GameObject>();
    public GameObject FriendInfo;
    /*
    public void OnClickButton()
    {
        string friendName = this.GetComponentInChildren<Text>().text;
        var chatWindow = GameObject.FindObjectOfType<ChatUI>();
        chatWindow.setFriendName(friendName);
    }*/

    private void Awake()
    {
        FriendInfo.SetActive(false);
    }
    // Use this for initialization
    void Start()
    {
        //Test();
    }

    private void OnEnable()
    {
        //Test();
        PlayerMyController.Instance.EnabledWindowCount++;
        foreach(KeyValuePair<string, int> kvp in World.Instance.get_players())
        {
            GameObject cloned = GameObject.Instantiate(FriendInfo);
            cloned.transform.SetParent(transform, false);
            cloned.SetActive(true);
            friendObjects.Add(cloned);
            var test = cloned.GetComponentInChildren<UnityEngine.UI.Text>();
            test.text = kvp.Key.ToString();
        }
    }

    private void OnDisable()
    {
        PlayerMyController.Instance.EnabledWindowCount--;
        foreach (var msgOb in friendObjects)
            msgOb.SetActive(false);
        friendObjects.Clear();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Test()
    {
        /*
        for (int i = 0; i < 5; i++)
        {
            GameObject cloned = GameObject.Instantiate(FriendInfo);
            cloned.transform.SetParent(transform, false);
            cloned.SetActive(true);
            var test = cloned.GetComponentInChildren<UnityEngine.UI.Text>();
            test.text = i.ToString();
        }
        */
        int count = 0;
        foreach (KeyValuePair<string, int> kvp in World.Instance.players)
        {
            GameObject cloned = GameObject.Instantiate(FriendInfo);
            cloned.transform.SetParent(transform, false);
            cloned.SetActive(true);
            var test = cloned.GetComponentInChildren<UnityEngine.UI.Text>();
            test.text = kvp.Key;
            Debug.Log(count);
            count++;
        }
    }
}
