using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Gamekit3D;
using Common;
using Assets._3DGamekit.Scripts.Game;
using Gamekit3D.Network;


namespace Assets._3DGamekit.Scripts.Game.UI
{
    class propertyUI : MonoBehaviour
    {
        public void onClickSale()
        {
            string price = this.GetComponentInChildren<InputField>().text;
            Debug.Log(string.Format("frontEnd: propertyUI:{0}", price));
        }
        public void onClickOff()
        {
            CChangeStatus m = new CChangeStatus();
            if (World.Instance.off())
            {
                MessageBox.Show(":)");
                Debug.Log(string.Format("{0}", World.Instance.view.id));
                m.treasureId = World.Instance.view.id;
                m.userDbid = World.Instance.selfDbid;
                m.on = false;
                Client.Instance.Send(m);
                Sprite icon = GetAllIcons.icons["Grid"];
                GameObject.Find("Defence1").GetComponent<Image>().sprite = icon;
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
                m.treasureId = World.Instance.view.id;
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
