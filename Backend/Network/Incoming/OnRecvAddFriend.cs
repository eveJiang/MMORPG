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
            switch (request.option)
            {
                case "get":
                    reply.items = Database.Instance.GetAddFriend(request.selfdbid);
                    reply.option = "get";
                    break;
                case "feedback":
                    if (request.acc == true)
                    {
                        Database.Instance.AccFriend(request.request, request.response);
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
                        Database.Instance.RejFriend(request.request, request.response);
                        ClientTipInfo(channel, "Successfully reject :)");
                    }
                    break;
                case "send":
                    if (Database.Instance.findFriend(request.name))
                    {
                        int temp = Database.Instance.insertFriend(request.name, request.selfdbid, request.message);
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
