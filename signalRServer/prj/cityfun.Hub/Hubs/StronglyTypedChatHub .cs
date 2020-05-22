#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：cityfun.Hub.Hubs
* 项目描述 ：
* 类 名 称 ：StronglyTypedChatHub
* 类 描 述 ：
* 所在的域 ：CF-415PZN2
* 命名空间 ：cityfun.Hub.Hubs
* 机器名称 ：CF-415PZN2 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：门鹏鹏
* 创建时间 ：2020/5/20 9:41:18
* 更新时间 ：2020/5/20 9:41:18
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ 门鹏鹏 2020. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace cityfun.Hub.Hubs
{
    public class StronglyTypedChatHub : Hub<IChatClient>
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.ReceiveMessage(user, message);
        }

        public Task SendMessageToCaller(string message)
        {
            return Clients.Caller.ReceiveMessage(message);
        }

        public Task SendMessageToGroups(string message)
        {
            return Clients.Group("SignalR Users").ReceiveMessage(message);
        }

        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
