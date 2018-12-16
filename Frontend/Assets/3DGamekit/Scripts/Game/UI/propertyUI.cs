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

        }
        public void onClickOn()
        {
            CChangeStatus m = new CChangeStatus();
            if (World.Instance.check())
            {
                MessageBox.Show("Successfully put on the clothes");
                m.treasureName = World.Instance.view.name;
                m.userDbid = World.Instance.selfDbid;
                Client.Instance.Send(m);
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
