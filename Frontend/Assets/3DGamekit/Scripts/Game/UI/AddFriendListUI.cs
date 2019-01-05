using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Gamekit3D;
using Gamekit3D.Network;
using Assets._3DGamekit.Scripts.Game;
using System;
using Common;

public class AddFriendListUI : MonoBehaviour
{

    public GameObject InventoryCell;
    public GameObject InventoryGridContent;
    public GameObject ItemInfoUI;
    public Sprite init;

    // Use this for initialization

    private void Awake()
    {
        InventoryCell.SetActive(false);
    }

    private IEnumerator ShowItems()
    {
        while (World.Instance.myAddFriend == null)
        {
            yield return new WaitForSecondsRealtime(0.1f);
        }
        int count = World.Instance.myAddFriend.Capacity;
        int capacity = 40;
        foreach (var kv in World.Instance.myAddFriend)
        {
            GameObject cloned = GameObject.Instantiate(InventoryCell);
            Button button = cloned.GetComponent<Button>();
            // TODO ... specify icon by item types
            Debug.Log(kv.name);
            button.image.sprite = init;
            button.onClick.AddListener(delegate ()
            {
                GameObject.Find("AddFriendImage").GetComponent<Image>().sprite = init;
                GameObject.Find("AddFriendText").GetComponent<Text>().text = kv.name;
                GameObject.Find("AddFriendInfo").GetComponent<Text>().text = kv.info;
                World.Instance.friendview.dbID = kv.dbID;
                World.Instance.friendview.info = kv.info;
                World.Instance.friendview.name = kv.name;
            });
            cloned.SetActive(true);
            cloned.transform.SetParent(InventoryGridContent.transform, false);
        }
        for (int i = 0; i < capacity - count; i++)
        {
            GameObject cloned = GameObject.Instantiate(InventoryCell);
            cloned.SetActive(true);
            cloned.transform.SetParent(InventoryGridContent.transform, false);
        }
    }

    private void OnEnable()
    {
        PlayerMyController.Instance.EnabledWindowCount++;
        CAddFriend m = new CAddFriend();
        m.selfdbid = World.Instance.selfDbid;
        m.option = "get";
        World.Instance.myAddFriend = null;
        Client.Instance.Send(m);
        StartCoroutine(ShowItems());
    }

    private void OnDisable()
    {
        int cellCount = InventoryGridContent.transform.childCount;
        foreach (Transform transform in InventoryGridContent.transform)
        {
            Destroy(transform.gameObject);
        }
        PlayerMyController.Instance.EnabledWindowCount--;
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void ExtendBagCapacity(int n)
    {
        int cellCount = InventoryGridContent.transform.childCount;
        for (int i = 0; i < n - cellCount; i++)
        {
            GameObject cloned = GameObject.Instantiate(InventoryCell);
            cloned.SetActive(true);
            cloned.transform.SetParent(InventoryGridContent.transform, false);
        }
    }

    public void OnClickButton()
    {

    }
}
