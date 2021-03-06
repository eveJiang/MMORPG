﻿using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Gamekit3D.Network
{
    public partial class Incoming
    {
        private Dictionary<string, GameObject> networkObjects = new Dictionary<string, GameObject>();
        private Dictionary<int, GameObject> gameObjects = new Dictionary<int, GameObject>();
        public Dictionary<int, NetworkEntity> networkEntities = new Dictionary<int, NetworkEntity>();
        public PlayerController thisPlayer;
        IRegister register;
        public Incoming(IRegister register)
        {
            this.register = register;
            RegisterAll();
        }

        public void RegisterAll()
        {
            register.Register(Command.S_PLAYER_ENTER, OnRecvPlayerEnter);
            register.Register(Command.S_SPAWN, OnRecvSpawn);
            register.Register(Command.S_ATTACK, OnRecvAttack);
            register.Register(Command.S_JUMP, OnRecvJump);
            register.Register(Command.S_PLAYER_MOVE, OnRecvPlayerMove);
            register.Register(Command.S_SPRITE_MOVE, OnRecvSpriteMove);
            register.Register(Command.S_ENTITY_DESTROY, OnRecvEntityDestory);
            register.Register(Command.S_EQUIP_WEAPON, OnRecvEquipWeapon);
            register.Register(Command.S_TAKE_ITEM, OnRecvTakeItem);
            register.Register(Command.S_HIT, OnRecvHit);
            register.Register(Command.S_SPRITE_DIE, OnRecvDie);
            register.Register(Command.S_TIP_INFO, OnRecvTipInfo);
            register.Register(Command.S_PLAYER_DIE, OnRecvPlayerDie);
            register.Register(Command.S_PLAYER_RESPAWN, OnRecvPlayerReSpawn);
            register.Register(Command.S_CHAT_MESSAGE, OnRecvChat);
            register.Register(Command.S_COMMUNITY, OnRecvCommunity);
            register.Register(Command.S_BUYMESSAGE, OnRecvBuy);
            register.Register(Command.S_TREASUREMESSAGE, OnRecvTreausre);
            register.Register(Command.S_MARKETMESSAGE, OnRecvMarket);
            register.Register(Command.S_MYMARKET, OnRecvMyMarket);
            register.Register(Command.S_COINMESSAGE, OnRecvCoin);
			register.Register(Command.S_PLAYER_FLASH, OnRecvPlayerFlash);//
			register.Register(Command.S_FLASH, OnRecvFlash);//
            register.Register(Command.S_ADDFRIEND, OnRecvAdd);																 // DEBUG ...
			register.Register(Command.S_FIND_PATH, OnRecvFindPath);
            register.Register(Command.S_GETMESSAGE, OnRecvGetMessage);
			register.Register(Command.S_NAME, OnRecvName);
			register.Register(Command.S_TEMP, OnRecvName);
		}

        public void InitNetworkEntity()
        {
            NetworkEntity[] all = GameObject.FindObjectsOfType<NetworkEntity>();
            foreach (NetworkEntity entity in all)
            {
                GameObject go = entity.gameObject;
                go.SetActive(false);
                if (networkObjects.ContainsKey(go.name))
                {
                    GameObject.Destroy(go);
                }
                else
                {
                    networkObjects[go.name] = go;
                }
            }
        }

        public GameObject CloneGameObject(string name, int entityID)
        {
            GameObject go = networkObjects[name];
            return CloneGameObject(go, entityID);
        }

        public GameObject CloneGameObject(GameObject gameObject, int entityID)
        {
            GameObject go = GameObject.Instantiate(gameObject);
            NetworkEntity entity = go.GetComponent<NetworkEntity>();
            if (entity == null)
            {
                GameObject.Destroy(go);
                return null;
            }
            entity.entityId = entityID;
            if (!networkEntities.ContainsKey(entity.entityId))
            {
                networkEntities.Add(entity.entityId, entity);
            }
            return go;
        }
    }
}
