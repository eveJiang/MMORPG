using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;
using Gamekit3D.Network;

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
        foreach (var kv in m_items)
        {
            var name = kv.Key;
            var item = kv.Value;
            int count = m_count[name];
            for (int i = 0; i < count; ++i)
            {
                Treasure treasure = new Treasure
                {
                    name = name,
                    price = 5,
                    type = '0',
                    effect = '0'
                };
                message.items.Add(treasure);
            }
        }
        return message;
    }

}
