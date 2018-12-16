using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;
using Gamekit3D.Network;
using Assets._3DGamekit.Scripts.Game;

public class CartGridUI : MonoBehaviour
{
    public GameObject CartItem;

    private Dictionary<string, GameObject> m_items = new Dictionary<string, GameObject>();
    private Dictionary<string, int> m_count = new Dictionary<string, int>();

    private void Awake()
    {
        CartItem.SetActive(false);
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Increase(string name)
    {
        m_count[name] += 1;
    }

    public void Decrease(string name)
    {
        m_count[name] -= 1;
    }

    public void AddToCart(string name)
    {
        Sprite sprite;
        GameObject item;
        if (!GetAllIcons.icons.TryGetValue(name, out sprite))
        {
            return;
        }
        bool exists = m_items.TryGetValue(name, out item);
        if (!exists)
        {
            item = GameObject.Instantiate(CartItem);
            if (item == null)
            {
                return;
            }
            item.transform.SetParent(transform, false);
            item.SetActive(true);
            m_items.Add(name, item);
            m_count.Add(name, 1);
        }
        CartItemUI handler = item.GetComponent<CartItemUI>();
        if (handler == null)
        {
            return;
        }

        if (exists)
        {
            handler.Increase();
        }
        else
        {
            handler.Init(name);
        }
    }

    public void RemoveFromCart(string name)
    {
        GameObject item;
        if (m_items.TryGetValue(name, out item))
        {
            m_items.Remove(name);
            m_count.Remove(name);
            Destroy(item);
        }
    }

    public CBuyMessage getBuyMessage()
    {
        CBuyMessage message = new CBuyMessage();
        message.dbid = World.Instance.selfDbid;
        foreach (var kv in m_items)
        {
            var name = kv.Key;
            var item = kv.Value;
            int count = m_count[name];
            // type: amulet = a, armor = b, ax = c, bow = d, elixir = e, flail = f, 
            // helmet = g, ring = h, shield = i, shurikens = j, stone = k,  sword = l
            // effect: 防御(0)：a, i, g 生命(1)：e, 智力(2)：h, k 速度(3)：b 攻击(4)：c, d, f, j, l
            char type_ = 'a', effect_ = '0';
            int value_ = (int)kv.Key[kv.Key.Length - 1] - '0';
            if (value_ >9 || value_ < 0) value_ = 1;
            if (kv.Key.Contains("Amulet") || kv.Key.Contains("Shield") || kv.Key.Contains("Helmet"))
            {
                effect_ = '0';
                if (kv.Key.Contains("Amulet")) type_ = 'a';
                if (kv.Key.Contains("Shield")) type_ = 'i';
                if (kv.Key.Contains("Helmet")) type_ = 'g';
            }
            else if( kv.Key.Contains("Elixir"))
            {
                effect_ = '1';
                type_ = 'e';
            }
            else if(kv.Key.Contains("Ring") || kv.Key.Contains("Stone"))
            {
                effect_ = '2';
                if(kv.Key.Contains("Ring")) type_ = 'h';
                else type_ = 'k';
            }
            else if(kv.Key.Contains("Armor"))
            {
                effect_ = '3';
                type_ = 'b';
            }
            else
            {
                effect_ = '4';
                if (kv.Key.Contains("Ax")) type_ = 'c';
                else if (kv.Key.Contains("Bow")) type_ = 'd';
                else if (kv.Key.Contains("Flail")) type_ = 'f';
                else if (kv.Key.Contains("Shurikens")) type_ = 'j';
                else  type_ = 'l';
            }
            // 1 拥有 2穿着
            Treasure treasure = new Treasure
            {
                name = name,
                price = 5,
                type = type_,
                effect = effect_,
                value = value_,
                status = '1'
            };
            for(int i = 0; i <count; ++i)
                message.items.Add(treasure);
        }
        return message;

    }
}
