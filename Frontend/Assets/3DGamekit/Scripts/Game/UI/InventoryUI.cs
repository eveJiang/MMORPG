using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Gamekit3D;
using Gamekit3D.Network;
using Assets._3DGamekit.Scripts.Game;
using System;
using Common;

public class InventoryUI : MonoBehaviour
{

    public GameObject InventoryCell;
    public GameObject InventoryGridContent;
    public GameObject ItemInfoUI;

    // Use this for initialization

    private void Awake()
    {
        InventoryCell.SetActive(false);
    }

    private void OnEnable()
    {
        PlayerMyController.Instance.EnabledWindowCount++;
        int capacity = World.Instance.inventoryCapacity;
        int count = World.Instance.inventoryCount;
        foreach (var kv in World.Instance.myinventory)
        {
            GameObject cloned = GameObject.Instantiate(InventoryCell);
            Button button = cloned.GetComponent<Button>();
            // TODO ... specify icon by item types
            Sprite icon = GetAllIcons.icons[kv];
            button.image.sprite = icon;

            button.onClick.AddListener(delegate ()
            {
                CTreasureMessage m = new CTreasureMessage();
                m.dbid = World.Instance.selfDbid;
                m.treasureName = kv;
                Client.Instance.Send(m);
                GameObject.Find("ItemImage").GetComponent<Image>().sprite = icon;
                GameObject.Find("ItemText").GetComponent<Text>().text = kv;
                World.Instance.view.name = kv;
                //while(true)
                //{
                //    if (World.Instance.messageLock == 1)
                //        break;
                //}
                Debug.Log(string.Format("treausre: {0}, dbid : {1}", m.treasureName, m.dbid));
                GameObject.Find("ItemValue").GetComponent<Text>().text = String.Format("Value : {0}",Convert.ToString(World.Instance.view.value));
                // effect: 防御(0)：a, i, g 生命(1)：e, 智力(2)：h, k 速度(3)：b 攻击(4)：c, d, f, j, l
                if (World.Instance.view.effect == '0') GameObject.Find("ItemEffect").GetComponent<Text>().text = Convert.ToString("Funciton:Defence");
                else if (World.Instance.view.effect == '1') GameObject.Find("ItemEffect").GetComponent<Text>().text = Convert.ToString("Funciton:Medicine");
                else if (World.Instance.view.effect == '2') GameObject.Find("ItemEffect").GetComponent<Text>().text = Convert.ToString("Function:Intelligence");
                else if (World.Instance.view.effect == '3') GameObject.Find("ItemEffect").GetComponent<Text>().text = Convert.ToString("Function:Speed");
                else GameObject.Find("ItemEffect").GetComponent<Text>().text = Convert.ToString("Function:Attack");
                World.Instance.messageLock = 0;
                //GameObject.Find("Helmet").GetComponent<Image>().sprite = icon;
            });
            //Sprite icon = GetAllIcons.icons["Sword_2"];
            //button.image.sprite = icon;
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
