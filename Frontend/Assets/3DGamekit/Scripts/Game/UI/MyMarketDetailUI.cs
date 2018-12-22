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

public class MyMarketDetailUI : MonoBehaviour
{

    public TextMeshProUGUI HPValue;
    public TextMeshProUGUI InteligenceValue;
    public TextMeshProUGUI SpeedValue;
    public TextMeshProUGUI LevelValue;
    public TextMeshProUGUI AttackValue;
    public TextMeshProUGUI DefenseValue;
    public Sprite init;
    private Damageable m_damageable;
    private PlayerController m_controller;
    public bool coinType = false; // false - silver, true - gold
    public void onClickSale()
    {
        int price = int.Parse(GetComponentInChildren<InputField>().text);
        MarketTreasure item = new MarketTreasure
        {
            owner_id = World.Instance.selfDbid,
            id = World.Instance.view.id,
            name = World.Instance.view.name,
            type = World.Instance.view.type,
            effect = World.Instance.view.effect,
            value = World.Instance.view.value,
            price = price,
            status = '3',
            coinType = coinType
        };
        CMarketMessage msgSell = new CMarketMessage
        {
            option = "change",
            items = new List<MarketTreasure>()
        };
        World.Instance.mview.price = price;
        msgSell.items.Add(World.Instance.mview);
        if (World.Instance.view.status != '2')
        {
            Client.Instance.Send(msgSell);
            World.Instance.myinventory.Remove(World.Instance.view);
            World.Instance.view.status = '3';
            MarketTreasure insert = new MarketTreasure();
            insert.id = World.Instance.view.id;
            insert.owner_id = World.Instance.selfId;
            insert.name = World.Instance.view.name;
            insert.value = World.Instance.view.value;
            insert.price = price;
            insert.type = World.Instance.view.type;
            insert.effect = World.Instance.view.effect;
            insert.status = World.Instance.view.status;
            insert.coinType = coinType;
            World.Instance.mymarket.Add(insert);
        }
        else
            MessageBox.Show("Please unload before selling");
    }

    public void onClickCoinType()
    {
        coinType = !coinType;
        var image = GetComponentInChildren<Image>();
        // TODO: corresponding image = Resources.Load(coin picture) as Sprite
        // image.sprite = corresponding image;
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
        string hp = string.Format("{0}/{1}", m_damageable.currentHitPoints, m_damageable.maxHitPoints);
        HPValue.SetText(hp, true);
        SpeedValue.SetText(Convert.ToString(World.Instance.count_speed), true);
        AttackValue.SetText(Convert.ToString(World.Instance.count_attack), true);
        GameObject.Find("MarketItemImage").SetActive(true);
        GameObject.Find("MarketItemText").SetActive(true);
        GameObject.Find("MarketItemImage").GetComponent<Image>().sprite = init;
        GameObject.Find("MarketItemValue").GetComponent<Text>().text = "Value";
        GameObject.Find("MarketItemEffect").GetComponent<Text>().text = "Function";
        GameObject.Find("MarketItemPrice").GetComponent<Text>().text = "Price";
        GameObject.Find("MarketItemText").GetComponent<Text>().text = World.Instance.selfName;
        foreach(var i in World.Instance.occupied)
        {
            if(i.Value == 1)
            {
                foreach(var j in World.Instance.position)
                {
                    if(j.Value == i.Key)
                    {
                        Sprite icon = GetAllIcons.icons[getname(j.Key)];
                        GameObject.Find(string.Format("Market{0}",i.Key)).GetComponent<Image>().sprite = icon;
                        Debug.Log(i.Key);
                    }
                }
            }
        }

    }

    private string getname(int dbid)
    {
        foreach(var temp in World.Instance.myinventory)
        {
            if(temp.id == dbid)
            {
                return temp.name;
            }
        }
        return "unknown";
    }

    private void OnDisable()
    {
        PlayerMyController.Instance.EnabledWindowCount--;
        World.Instance.view.effect = '5';

    }

    // Update is called once per frame
    void Update()
    {
        HPValue.SetText(Convert.ToString(World.Instance.count_HP), true);
        InteligenceValue.SetText(Convert.ToString(World.Instance.count_intelligence), true);
        DefenseValue.SetText(Convert.ToString(World.Instance.count_defence), true);
        SpeedValue.SetText(Convert.ToString(World.Instance.count_speed), true);
        AttackValue.SetText(Convert.ToString(World.Instance.count_attack), true);
        DefenseValue.SetText(Convert.ToString(World.Instance.count_defence), true);
        //GameObject.Find("MarketGold").GetComponent<Text>().text = String.Format("{0}", Convert.ToString(World.Instance.gold));
        //GameObject.Find("MarketSilver").GetComponent<Text>().text = String.Format("{0}", Convert.ToString(World.Instance.silver));
        //if (World.Instance.view.effect == '0') GameObject.Find("ItemEffect").GetComponent<Text>().text = Convert.ToString("Funciton:Defence");
        //else if (World.Instance.view.effect == '1') GameObject.Find("ItemEffect").GetComponent<Text>().text = Convert.ToString("Funciton:Medicine");
        //else if (World.Instance.view.effect == '2') GameObject.Find("ItemEffect").GetComponent<Text>().text = Convert.ToString("Function:Intelligence");
        //else if (World.Instance.view.effect == '3') GameObject.Find("ItemEffect").GetComponent<Text>().text = Convert.ToString("Function:Speed");
        //else if (World.Instance.view.effect == '4') GameObject.Find("ItemEffect").GetComponent<Text>().text = Convert.ToString("Function:Attack");
        //else GameObject.Find("ItemEffect").GetComponent<Text>().text = Convert.ToString("Funtion");
    }

    void Test()
    {
        HPValue.text = "100";
        InteligenceValue.text = "100";
    }

    public void withdraw()
    {
        CMarketMessage m = new CMarketMessage();
        m.option = "buy";
        m.items.Add(World.Instance.mview);
        m.dbid = World.Instance.selfDbid;
        Client.Instance.Send(m);
    }

    public void change()
    {

    }

    public void InitUI(PlayerController controller)
    {
        m_damageable = controller.GetComponent<Damageable>();
        m_controller = controller;
    }
    
}
