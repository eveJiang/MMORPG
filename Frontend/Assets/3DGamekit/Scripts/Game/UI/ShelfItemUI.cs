using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;
using Gamekit3D.Network;

public class ShelfItemUI : MonoBehaviour
{
    public string itemName;
    public GameObject cartContent;

    public Button button;
    public Text textName;
    public Text textCost;
    public MarketTreasure item;

    private static CartGridUI handler;

    private void Awake()
    {
        if (cartContent != null)
        {
            handler = cartContent.GetComponent<CartGridUI>();
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init(MarketTreasure treasure)
    {
        itemName = treasure.name;
        item = treasure;
        Sprite sprite;
        Image coinType;
        if (button == null || textName == null || textCost == null)
        {
            return;
        }
        if (!GetAllIcons.icons.TryGetValue(item.name, out sprite))
        {
            return;
        }
        button.image.sprite = sprite;
        textName.text = item.name;
        textCost.text = "$" + treasure.price.ToString();
        coinType = GetComponentInChildren<Image>();
        // coinType setimage treasure.coinType
    }

    public void Init(string name)
    {
        itemName = name;
        Sprite sprite;
        if (button == null || textName == null || textCost == null)
        {
            return;
        }
        if (!GetAllIcons.icons.TryGetValue(name, out sprite))
        {
            return;
        }
        button.image.sprite = sprite;
        textName.text = name;
        textCost.text = "$5";
    }

    public void AddToCart()
    {
        if (handler != null)
            handler.AddToCart(itemName);
    }

    public void AddToMarketCart()
    {
        if (handler != null)
            handler.AddToMarketCart(item);
    }

    public void OnBuyButtonClicked()
    {
        if (handler != null)
        {
            CBuyMessage message = handler.getBuyMessage();
            Client.Instance.Send(message);
            Debug.Log("FrontEnd: Receive BuyButtonClicked");
        }
    }

    public void OnMarketBuyButtonClicked()
    {
        if (handler != null)
        {
            var message = handler.GetMarketBuyMessage();
            message.option = "buy";
            Client.Instance.Send(message);
            Debug.Log("FrontEnd: Receive BuyButtonClicked");
        }
    }
}
