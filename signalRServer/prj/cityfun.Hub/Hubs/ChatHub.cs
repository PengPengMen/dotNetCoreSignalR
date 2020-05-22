using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using cityfun.Hub.Hubs;
using cityfun.Redis.Redis;
using Cityfun.Core.Serilize;
using Microsoft.AspNetCore.SignalR;
using SignalR = Microsoft.AspNetCore.SignalR;
namespace cityfun.Hub
{
    public class ChatHub : SignalR.Hub
    {
        /// <summary>
        /// Redis 存储数据设置
        /// </summary>
        RedisServiceHelp redisService = new RedisServiceHelp();
        int dbNumber = 1;
        #region 客户端建立连接之后调用
        public async Task UserStartConnectedAsync(string userId)
        {
            string connectionId = this.Context.ConnectionId;
            // 此处Redis用来保存userId与connectionId关系
            redisService.SetRedisStringValueToSelectDb(userId, connectionId, dbNumber);
            await base.OnConnectedAsync();
        }
        #endregion


        #region 客户端断开或者关闭调用
        public void UserDisconnected()
        {
            string connectionId = this.Context.ConnectionId;
            redisService.DeleteRedisValueSelectDb(connectionId, dbNumber);
            Dispose();
        }
        #endregion

        [HubMethodName("SendMessageToUserId")]
        public void SendMessageToUserId(string userId,string messgae)
        {
            Message msg = new Message();
            string connectionId = redisService.GetRedisValueSelectDb(userId, dbNumber);
            try
            {
                msg.code = 200;
                msg.desc = "SIGNALR_SUCCESS";
                msg.content = messgae;
                string signalRContent = JsonHelper.AsJson(msg).ToString();
                Clients.Client(connectionId).SendAsync("ReceiveMessageToUserId", signalRContent);
            }
            catch (Exception ex)
            {
                msg.code = 500;
                msg.desc = "SIGNALR_ERROR";
                msg.content = ex.ToString();
                string signalRContent = JsonHelper.AsJson(msg).ToString();
                Clients.Client(connectionId).SendAsync("ReceiveMessageToUserId", signalRContent);
            }
        }

        /// <summary>
        /// 使用Clients.All将消息发送到所有连接的客户端
        /// </summary>
        /// <param name="user"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task SendMessageToAll(string user, string message)
        {
            return Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        /// <summary>
        /// SendMessageToCaller使用Clients.Caller将消息发回给调用方
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task SendMessageToCaller(string user, string message)
        {
            string connectionId = this.Context.ConnectionId;
            message = string.Format("user:{0},connectionId:{1},message:{2}", user, connectionId, message);
            return Clients.Caller.SendAsync("ReceiveMessageToCaller", message);
        }
        /// <summary>
        /// SendMessageToGroups向组中的SignalR Users所有客户端发送一条消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task SendMessageToGroup(string groupName,string message)
        {
            return Clients.Group(groupName).SendAsync("ReceiveMessage", message);
        }

        /// <summary>
        /// 加入组
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("ReceiveMessage", $"{Context.ConnectionId} joined {groupName}");
        }

        /// <summary>
        /// 离开组
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("ReceiveMessage", $"{Context.ConnectionId} left {groupName}");
        }

        #region ThrowHubException
        public Task ThrowException()
        {
            throw new HubException("This error will be sent to the client!");
        }
        #endregion

    }
}
