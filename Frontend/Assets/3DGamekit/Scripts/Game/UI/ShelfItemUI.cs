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
        if (handler != null)
        {
            CBuyMessage message = handler.getBuyMessage();
            Client.Instance.Send(message);
        }
    }
}
