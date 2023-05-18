using StackExchange.Redis;
using System;
namespace NewsFeedApp.Services.Helpers
{
    public class ConnectionHelper
    {
        private static Lazy<ConnectionMultiplexer> lazyConnection;
        static ConnectionHelper()
        {
            ConnectionHelper.lazyConnection = new Lazy<ConnectionMultiplexer>(() => {
                return ConnectionMultiplexer.Connect(ConfigurationManager.AppSetting["RedisURL"]);
            });
        }        
        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
    }
}