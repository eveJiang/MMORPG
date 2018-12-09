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
            
            var db = Backend.Database.Instance;

            if (db.LoginUser(request.user.ToString(), request.password.ToString()))
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
            player.dbid = db.GetID(request.user);
            int k = player.entityId;
            response.id = k;
            ClientTipInfo(channel, string.Format("Backend:('name: {0}', 'entity_id: {1}','sql_id: {2});", player.user, player.entityId, player.dbid));
            DEntity dentity = World.Instance.EntityData["Ellen"];
            player.FromDEntity(dentity);
            player.forClone = false;

            SCommunity bm = new SCommunity();
            bm.name = request.user;
            bm.id = k;
            bm.enter = true;
            response.inventory = db.GetInventory(player.dbid);
            channel.Send(response);
            Scene scenes = World.Instance.GetScene(player.scene);
            foreach (var kvp in scenes.Players)
            {
                SCommunity am = new SCommunity();
                am.name = kvp.Value.user;
                am.id = kvp.Value.entityId;
                am.enter = true;
                channel.Send(am);
            }
            World.Instance.Broundcast(bm);



            //ClientTipInfo(channel, "TODO: get player's attribute from database");
            // player will be added to scene when receive client's CEnterSceneDone message
        }
    }
}
