using Common;
using Backend.Game;
using System;
using System.Collections;
using System.Collections.Generic;
namespace Backend.Network
{
    public partial class Incoming
    {
        private void OnRecvLogin(IChannel channel, Message message)
        {
            CLogin request = message as CLogin;       
            SPlayerEnter response = new SPlayerEnter();
            string scene = "Level1";
            response.user = request.user;
            response.token = request.user;
            response.scene = scene;
            var conn = db.Instance.Connect();
            if (db.Instance.LoginUser(request.user.ToString(), request.password.ToString(), conn))
            {
                //response.id = db.GetID(request.user);
                ClientTipInfo(channel, "Successfully login :)");
                //channel.Send(response);

            }
            else
            {
                ClientTipInfo(channel, "Sorry :(");
                return;
            }
            Player player = new Player(channel);
            player.scene = scene;
            player.user = request.user;
            player.id = response.id;
            player.dbid = db.Instance.GetID(request.user, conn);
            int k = player.entityId;
            response.id = k;
            response.dbid = player.dbid;
            ClientTipInfo(channel, string.Format("Backend:('name: {0}', 'entity_id: {1}','sql_id: {2});", player.user, player.entityId, player.dbid));
            DEntity dentity = World.Instance.EntityData["Ellen"];
            player.FromDEntity(dentity);
            player.forClone = false;

            SCommunity bm = new SCommunity();
            bm.name = request.user;
            bm.id = k;
            bm.enter = true;
            response.inventory = db.Instance.GetInventory(player.dbid, conn);
            response.market = db.Instance.GetMyMarket(player.dbid, conn);
            response.silver = db.Instance.GetSilverCoins(player.dbid, conn);
            response.gold = db.Instance.GetGoldCoins(player.dbid, conn);
            response.teammate_id = db.Instance.getTeammate(player.dbid, conn);
            channel.Send(response);
            Scene scenes = World.Instance.GetScene(player.scene);
            List<int> myFriend = db.Instance.getMyFriend(player.dbid, conn);
            foreach (var kvp in scenes.Players)
            {
                if (myFriend.Contains(kvp.Value.dbid) == false)
                    continue;
                SCommunity am = new SCommunity();
                am.name = kvp.Value.user;
                am.id = kvp.Value.entityId;
                am.enter = true;
                channel.Send(am);
                kvp.Value.connection.Send(bm);
            }
        }
    }
}
