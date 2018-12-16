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

public class RoleUI : MonoBehaviour
{

    public TextMeshProUGUI HPValue;
    public TextMeshProUGUI InteligenceValue;
    public TextMeshProUGUI SpeedValue;
    public TextMeshProUGUI LevelValue;
    public TextMeshProUGUI AttackValue;
    public TextMeshProUGUI DefenseValue;

    private Damageable m_damageable;
    private PlayerController m_controller;

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
        GameObject.Find("ItemImage").SetActive(true);
        GameObject.Find("ItemText").SetActive(true);
    }

    private void OnDisable()
    {
        PlayerMyController.Instance.EnabledWindowCount--;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("ItemValue").GetComponent<Text>().text = String.Format("Value : {0}", Convert.ToString(World.Instance.view.value));
        GameObject.Find("ItemValue").GetComponent<Text>().text = String.Format("Value : {0}", Convert.ToString(World.Instance.view.value));
        if (World.Instance.view.effect == '0') GameObject.Find("ItemEffect").GetComponent<Text>().text = Convert.ToString("Funciton:Defence");
        else if (World.Instance.view.effect == '1') GameObject.Find("ItemEffect").GetComponent<Text>().text = Convert.ToString("Funciton:Medicine");
        else if (World.Instance.view.effect == '2') GameObject.Find("ItemEffect").GetComponent<Text>().text = Convert.ToString("Function:Intelligence");
        else if (World.Instance.view.effect == '3') GameObject.Find("ItemEffect").GetComponent<Text>().text = Convert.ToString("Function:Speed");
        else GameObject.Find("ItemEffect").GetComponent<Text>().text = Convert.ToString("Function:Attack");
        GameObject.Find("HPValue").GetComponent<Text>().text = Convert.ToString(World.Instance.view.value);
        GameObject.Find("IntelligenceValue").GetComponent<Text>().text = Convert.ToString(World.Instance.view.value);
        GameObject.Find("SpeedValue").GetComponent<Text>().text = Convert.ToString(World.Instance.view.value);
        GameObject.Find("AttackValue").GetComponent<Text>().text = Convert.ToString(World.Instance.view.value);
        GameObject.Find("DefenseValue").GetComponent<Text>().text = Convert.ToString(World.Instance.view.value);

    }

    void Test()
    {
        HPValue.text = "100";
        InteligenceValue.text = "100";
    }

    public void InitUI(PlayerController controller)
    {
        m_damageable = controller.GetComponent<Damageable>();
        m_controller = controller;
    }
}
