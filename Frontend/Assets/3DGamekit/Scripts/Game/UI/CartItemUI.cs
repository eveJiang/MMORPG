﻿using System;
using UnityEngine;
using UnityEngine.UI;

public class CartItemUI : MonoBehaviour
{
    public Button button;
    public Text textCost;
    public InputField inputCount;
    public int count = 0;
    string itemName;
    int cost;

    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init(string name, int cost, bool displayCount=true)
    {
        Sprite sprite;
        if (button == null || textCost == null || textCost == null)
        {
            return;
        }
        if (!GetAllIcons.icons.TryGetValue(name, out sprite))
        {
            return;
        }
        //textCost = GetComponentInChildren<Text>();
        itemName = name;
        count++;
        button.image.sprite = sprite;
        if (displayCount)
            inputCount.text = System.Convert.ToString(count);
        textCost.text = "$" + cost.ToString();
        this.cost = cost;
    }

    public void Increase()
    {
        CartGridUI gridHandler = transform.parent.GetComponent<CartGridUI>();
        count++;
        inputCount.text = System.Convert.ToString(count);
        textCost.text = "$" + cost.ToString();
        gridHandler.Increase(itemName);
    }

    public void Decrease()
    {
        count--;
        CartGridUI gridHandler = transform.parent.GetComponent<CartGridUI>();
        gridHandler.Decrease(itemName);
        if (count == 0)
        {
            if (transform.parent == null)
            {
                return;
            }
            if (gridHandler != null)
            {
                gridHandler.RemoveFromCart(itemName);
            }
        }
        else
        {
            inputCount.text = System.Convert.ToString(count);
            textCost.text = "$" + cost.ToString();
        }
    }

}
