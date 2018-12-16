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
        SpeedValue.SetText(Convert.ToString(World.Instance.count_speed), true);
        AttackValue.SetText(Convert.ToString(World.Instance.count_attack), true);
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
        HPValue.SetText(Convert.ToString(World.Instance.count_HP), true);
        InteligenceValue.SetText(Convert.ToString(World.Instance.count_intelligence), true);
        DefenseValue.SetText(Convert.ToString(World.Instance.count_defence), true);
        SpeedValue.SetText(Convert.ToString(World.Instance.count_speed), true);
        AttackValue.SetText(Convert.ToString(World.Instance.count_attack), true);
        DefenseValue.SetText(Convert.ToString(World.Instance.count_defence), true);
        GameObject.Find("Gold").GetComponent<Text>().text = String.Format("{0}", Convert.ToString(World.Instance.gold));
        GameObject.Find("Silver").GetComponent<Text>().text = String.Format("{0}", Convert.ToString(World.Instance.silver));
        if (World.Instance.view.effect == '0') GameObject.Find("ItemEffect").GetComponent<Text>().text = Convert.ToString("Funciton:Defence");
        else if (World.Instance.view.effect == '1') GameObject.Find("ItemEffect").GetComponent<Text>().text = Convert.ToString("Funciton:Medicine");
        else if (World.Instance.view.effect == '2') GameObject.Find("ItemEffect").GetComponent<Text>().text = Convert.ToString("Function:Intelligence");
        else if (World.Instance.view.effect == '3') GameObject.Find("ItemEffect").GetComponent<Text>().text = Convert.ToString("Function:Speed");
        else if (World.Instance.view.effect == '4') GameObject.Find("ItemEffect").GetComponent<Text>().text = Convert.ToString("Function:Attack");
        else GameObject.Find("ItemEffect").GetComponent<Text>().text = Convert.ToString("Function:");
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
