using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace cityfun.Hub.Hubs
{
    #region snippet_IChatClient
    public interface IChatClient
    {
        Task ReceiveMessage(string user, string message);
        Task ReceiveMessage(string message);
    }
    #endregion
}
