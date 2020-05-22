#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：cityfun.Hub.Hubs
* 项目描述 ：
* 类 名 称 ：Message
* 类 描 述 ：
* 所在的域 ：CF-415PZN2
* 命名空间 ：cityfun.Hub.Hubs
* 机器名称 ：CF-415PZN2 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：门鹏鹏
* 创建时间 ：2020/5/21 17:02:09
* 更新时间 ：2020/5/21 17:02:09
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ 门鹏鹏 2020. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Text;

namespace cityfun.Hub.Hubs
{
    public class Message
    {
        /// <summary>
        /// signalR 请求状态码
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// signalR 请求描述
        /// </summary>
        public string desc { get; set; }
        /// <summary>
        /// SignalR 请求内容
        /// </summary>
        public string content { get; set; }
    }
}
