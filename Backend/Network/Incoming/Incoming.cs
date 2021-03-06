﻿using Common;

namespace Backend.Network
{
    public partial class Incoming
    {
        IRegister register;
        public Incoming(IRegister register)
        {
            this.register = register;
            RegisterAll();
        }

        private void RegisterAll()
        {
            register.Register(Command.C_LOGIN, OnRecvLogin);
            register.Register(Command.C_REGISTER, OnRecvRegister);
            register.Register(Command.C_PLAYER_ENTER, OnRecvPlayerEnter);
            register.Register(Command.C_PLAYER_MOVE, OnRecvPlayerMove);
            register.Register(Command.C_PLAYER_JUMP, OnRecvPlayerJump);
            register.Register(Command.C_PLAYER_ATTACK, OnRecvPlayerAttack);
            register.Register(Command.C_PLAYER_TAKE, OnRecvPlayerTake);
            register.Register(Command.C_POSITION_REVISE, OnRecvPositionRevise);
            register.Register(Command.C_ENEMY_CLOSING, OnRecvEnemyClosing);
            register.Register(Command.C_DAMAGE, OnRecvDamage);
            register.Register(Command.C_CHAT_MESSAGE, OnRecvChat);
            register.Register(Command.C_BUYMESSAGE, OnRecvBuy);
            register.Register(Command.C_TREASUREMESSAGE, OnRecvTreasure);
            register.Register(Command.C_STATUSMESSAGE, OnRecvChangeStatus);
            register.Register(Command.C_DELETETREASURE, OnRecvDelete);
            register.Register(Command.C_MARKETMESSAGE, OnRecvMarket);
            register.Register(Command.C_MYMARKET, OnRecvMyMarket);
            register.Register(Command.C_COINMESSAGE, OnRecvCoin);
			register.Register(Command.C_ADDGOLDS, OnRecvAddGolds);
			register.Register(Command.C_PLAYER_FLASH, OnRecvPlayerFlash);//
			register.Register(Command.C_FLASH, OnRecvFlash);//
            register.Register(Command.C_ADDFRIEND, OnRecvAddFriend);
			register.Register(Command.C_FIND_PATH, OnRecvFindPath);
            register.Register(Command.C_GETMESSAGE, OnRecvGetMessage);
            register.Register(Command.C_GETMESSAGE, OnRecvGetMessage);
            register.Register(Command.C_MINESGOLDS, OnRecvMinesGolds); 
			register.Register(Command.C_NAME, OnRecvName);

		}


        static void ClientTipInfo(IChannel channel, string info)
        {
            STipInfo tipInfo = new STipInfo();
            tipInfo.info = info;
            channel.Send(tipInfo);
        }

    }
}
