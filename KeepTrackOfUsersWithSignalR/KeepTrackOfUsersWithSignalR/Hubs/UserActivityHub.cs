namespace KeepTrackOfUsersWithSignalR.Hubs
{
    using System.Collections.Generic;
    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;
    
    [HubName("userActivityHub")]
    public class UserActivityHub : Hub
    {
        private static List<string> users = new List<string>();

        public void Send(int count)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<UserActivityHub>();
            context.Clients.All.updateUsersOnlineCount(count);
        }

        public override System.Threading.Tasks.Task OnConnected()
        {
            string clientId = this.GetClientId();
            if (users.IndexOf(clientId) == -1)
            {
                users.Add(clientId);
            }

            Send(users.Count);

            return base.OnConnected();
        }

        public override System.Threading.Tasks.Task OnReconnected()
        {
            string clientId = this.GetClientId();
            if (users.IndexOf(clientId) == -1)
            {
                users.Add(clientId);
            }

            // Send the current count of users
            Send(users.Count);

            return base.OnReconnected();
        }

        public override System.Threading.Tasks.Task OnDisconnected()
        {
            string clientId = this.GetClientId();

            if (users.IndexOf(clientId) > -1)
            {
                users.Remove(clientId);
            }

            // Send the current count of users
            Send(users.Count);

            return base.OnDisconnected();
        }

        private string GetClientId()
        {
            string clientId = string.Empty;
           
            if (Context.QueryString["clientId"] != null)
            {
                clientId = this.Context.QueryString["clientId"];
            }

            if (string.IsNullOrEmpty(clientId.Trim()))
            {
                clientId = Context.ConnectionId;
            }

            return clientId;
        }
    }
}