using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamekit3D;

public class FriendUI : MonoBehaviour
{
    public GameObject FriendInfo;

    private void Awake()
    {
        FriendInfo.SetActive(false);
    }
    // Use this for initialization
    void Start()
    {
        Test();
    }

    private void OnEnable()
    {
        //Test();
        PlayerMyController.Instance.EnabledWindowCount++;
    }

    private void OnDisable()
    {
        PlayerMyController.Instance.EnabledWindowCount--;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Test()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject cloned = GameObject.Instantiate(FriendInfo);
            cloned.transform.SetParent(transform, false);
            cloned.SetActive(true);
            var test = cloned.GetComponentInChildren<UnityEngine.UI.Text>();
            test.text = i.ToString();
        }
    }
}
