using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Text;

namespace KWKY.Common
{
    public class AppConfiguration
    {
        public static string GetConnectionString (string connectionName)
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }
        public static string GetStrAppSetting (string appKey)
        {
            return ConfigurationManager.AppSettings[appKey];
        }
        public static int GetIntAppSetting (string appKey)
        {
            return int.Parse(GetStrAppSetting(appKey));
        }
        public static IPAddress GetIpAppSetting (string appKey)
        {
            return IPAddress.Parse(GetStrAppSetting(appKey));
        }

    }
}
