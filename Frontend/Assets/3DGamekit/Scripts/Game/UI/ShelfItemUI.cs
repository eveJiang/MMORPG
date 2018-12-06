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

    private CartGridUI handler;

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

    public void OnBuyButtonClicked()
    {
        Debug.Log(handler.ToString());
        CBuyMessage message = new CBuyMessage();
        message.message = "test";
        var items = handler.getItems();
        foreach (var kv in items)
        {
            // TODO ...
            var key = kv.Key;
            var item = kv.Value;
            Debug.Log(key);
        }
        Client.Instance.Send(message);
    }
}
