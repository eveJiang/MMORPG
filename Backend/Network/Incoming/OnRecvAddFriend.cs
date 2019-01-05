using Common;
using Backend.Game;
using System;
using System.Collections;
using System.Collections.Generic;
namespace Backend.Network
{
    public partial class Incoming
    {
        private void OnRecvAddFriend(IChannel channel, Message message)
        {
            CAddFriend request = message as CAddFriend;
            SAddFriend reply = new SAddFriend();
            string scene = "Level1";
            Scene scenes = World.Instance.GetScene(scene);
            var conn = db.Instance.Connect();
            switch (request.option)
            {
                case "get":
                    reply.items = db.Instance.GetAddFriend(request.selfdbid, conn);
                    reply.option = "get";
                    break;
                case "feedback":
                    if (request.acc == true)
                    {
                        db.Instance.AccFriend(request.request, request.response, conn);
                        foreach (var kvp in scenes.Players)
                        {
                            if (kvp.Value.dbid == request.request)
                            {
                                SCommunity am = new SCommunity();
                                am.name = kvp.Value.user;
                                am.id = kvp.Value.entityId;
                                am.enter = true;
                                channel.Send(am);
                                SCommunity bm = new SCommunity();
                                bm.name = request.selfname;
                                bm.id = request.selfentityid;
                                bm.enter = true;
                                kvp.Value.connection.Send(bm);
                                break;
                            }
                        }
                        ClientTipInfo(channel, "Successfully accept :)");
                    }
                    else
                    {
                        db.Instance.RejFriend(request.request, request.response, conn);
                        ClientTipInfo(channel, "Successfully reject :)");
                    }
                    break;
                case "send":
                    if (db.Instance.findFriend(request.name, conn))
                    {
                        int temp = db.Instance.insertFriend(request.name, request.selfdbid, request.message, conn);
                        if(temp == 0)
                            ClientTipInfo(channel, "Successfully send the message :)");
                        else
                            ClientTipInfo(channel, "Message already exists :(");
                    }
                    else
                        ClientTipInfo(channel, string.Format("Sorry, cannot find {0}", request.name));
                    break;
            }
            channel.Send(reply);
        }
    }
}
