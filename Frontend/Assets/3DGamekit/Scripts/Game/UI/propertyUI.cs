﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Gamekit3D;
using Common;
using Assets._3DGamekit.Scripts.Game;
using Gamekit3D.Network;


namespace Gamekit3D
{
    class propertyUI : MonoBehaviour
    {
        public Sprite grid;
        public void onClickSale()
        {
            int price = int.Parse(GetComponentInChildren<InputField>().text);
            MarketTreasure item = new MarketTreasure
            {
                owner_id = World.Instance.selfDbid,
                id = World.Instance.view.id,
                name = World.Instance.view.name,
                type = World.Instance.view.type,
                effect = World.Instance.view.effect,
                value = World.Instance.view.value,
                price = price,
                status = '3'
            };
            CMarketMessage msgSell = new CMarketMessage
            {
                option = "sell",
                items = new List<MarketTreasure>()
            };
            msgSell.items.Add(item);
            if (World.Instance.view.status != '2')
            {
                Client.Instance.Send(msgSell);
                World.Instance.myinventory.Remove(World.Instance.view);
                World.Instance.view.status = '3';
                World.Instance.mymarket.Add(World.Instance.view);
            }
            else
                MessageBox.Show("Please unload before selling");
        }

        public void onClickOff()
        {
            CChangeStatus m = new CChangeStatus();
            if (World.Instance.off())
            {
                MessageBox.Show(":)");
                Debug.Log(string.Format("{0}", World.Instance.view.id));
                m.treasure = World.Instance.view;
                m.userDbid = World.Instance.selfDbid;
                m.on = false;
                Client.Instance.Send(m);
                //Sprite icon = GetAllIcons.icons["Grid"];
                GameObject.Find(World.Instance.position[World.Instance.view.id]).GetComponent<Image>().sprite = grid;
                World.Instance.position.Remove(World.Instance.view.id);

            }
            else
                MessageBox.Show(":(");
        }
        public void onClickOn()
        {
            CChangeStatus m = new CChangeStatus();
            if (World.Instance.check())
            {
                MessageBox.Show("Successfully put on the clothes");
                m.treasure = World.Instance.view;
                m.userDbid = World.Instance.selfDbid;
                m.on = true;
                Client.Instance.Send(m);
                Sprite icon = GetAllIcons.icons[World.Instance.view.name];
                GameObject.Find(World.Instance.position[World.Instance.view.id]).GetComponent<Image>().sprite = icon;
            }
            else
                MessageBox.Show("Cannot put on the clothes");

        }
        private void Awake()
        {
        }
        // Use this for initialization
        void Start()
        {

        }

        private void OnEnable()
        {
            //Test();
            //initialize

        }

        private void OnDisable()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}