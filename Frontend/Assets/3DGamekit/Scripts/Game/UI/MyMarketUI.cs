using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Gamekit3D;
using Gamekit3D.Network;
using Assets._3DGamekit.Scripts.Game;
using System;
using Common;

public class MyMarketUI : MonoBehaviour
{

    public GameObject InventoryCell;
    public GameObject InventoryGridContent;
    public GameObject ItemInfoUI;

    // Use this for initialization

    private void Awake()
    {
        InventoryCell.SetActive(false);
    }

    private IEnumerator ShowItems()
    {
        while (World.Instance.mymarket == null)
        {
            yield return new WaitForSecondsRealtime(0.1f);
        }
        int capacity = World.Instance.marketCapacity;
        int count = World.Instance.marketCount;
        foreach (var kv in World.Instance.mymarket)
        {
            GameObject cloned = GameObject.Instantiate(InventoryCell);
            Button button = cloned.GetComponent<Button>();
            // TODO ... specify icon by item types
            Debug.Log(kv.name);
            Sprite icon = GetAllIcons.icons[kv.name];
            button.image.sprite = icon;

            button.onClick.AddListener(delegate ()
            {
                GameObject.Find("MarketItemImage").GetComponent<Image>().sprite = icon;
                GameObject.Find("MarketItemText").GetComponent<Text>().text = kv.name;
                World.Instance.setMView(kv);
                GameObject.Find("MarketItemValue").GetComponent<Text>().text = String.Format("Value : {0}", Convert.ToString(World.Instance.mview.value));
                // effect: 防御(0)：a, i, g 生命(1)：e, 智力(2)：h, k 速度(3)：b 攻击(4)：c, d, f, j, l
                if (World.Instance.mview.effect == '0') GameObject.Find("MarketItemEffect").GetComponent<Text>().text = Convert.ToString("Funciton:Defence");
                else if (World.Instance.mview.effect == '1') GameObject.Find("MarketItemEffect").GetComponent<Text>().text = Convert.ToString("Funciton:Medicine");
                else if (World.Instance.mview.effect == '2') GameObject.Find("MarketItemEffect").GetComponent<Text>().text = Convert.ToString("Function:Intelligence");
                else if (World.Instance.mview.effect == '3') GameObject.Find("MarketItemEffect").GetComponent<Text>().text = Convert.ToString("Function:Speed");
                else GameObject.Find("MarketItemEffect").GetComponent<Text>().text = Convert.ToString("Function:Attack");
                if(World.Instance.mview.coinType)
                    GameObject.Find("MarketItemPrice").GetComponent<Text>().text = String.Format("GoldCoin : {0}", Convert.ToString(World.Instance.mview.price));
                else
                    GameObject.Find("MarketItemPrice").GetComponent<Text>().text = String.Format("SilverCoin : {0}", Convert.ToString(World.Instance.mview.price));
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

        CMyMarket m = new CMyMarket();
        m.dbid = World.Instance.selfDbid;
        World.Instance.mymarket = null;
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
