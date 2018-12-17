using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using Gamekit3D.Network;
using UnityEngine.UI;

public class MarketGridUI : MonoBehaviour
{
    public GameObject ShelfItem;
    public List<GameObject> market;

    private IEnumerator ShowItems()
    {
        while (Client.Instance.market == null)
        {
            yield return new WaitForSecondsRealtime(0.1f);
        }
        foreach (var item in Client.Instance.market)
        {
            string key = item.name;
            GameObject cloned = GameObject.Instantiate(ShelfItem);
            if (cloned == null)
            {
                continue;
            }
            cloned.SetActive(true);
            cloned.transform.SetParent(this.transform, false);
            ShelfItemUI handler = cloned.GetComponent<ShelfItemUI>();
            if (handler == null)
            {
                continue;
            }
            handler.Init(key);
            market.Add(cloned);
        }
    }

    private void GetMarket()
    {
        CMarketMessage msgPull = new CMarketMessage
        {
            option = "get"
        };
        Client.Instance.market = null;
        Client.Instance.Send(msgPull);
        StartCoroutine(ShowItems());
    }

    private void OnEnable()
    {
        GetMarket();
        Debug.Log("awake");
    }

    private void OnDisable()
    {
        foreach (var o in market)
        {
            Destroy(o);
        }
    }

    private void Awake()
    {
        
    }
    // Use this for initialization
    void Start()
    {
        ShelfItem.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
