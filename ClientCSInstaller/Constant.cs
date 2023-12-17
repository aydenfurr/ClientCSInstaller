using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientCSInstaller
{
    public class Constant
    {
        public static string getJarUrl(string ver)
        {
            return "https://raw.githubusercontent.com/and1558/ClientUpdates/main/" + ver + "/and1558.jar";
        }

        public static string getVanillaJarUrl()
        {
            return "https://launcher.mojang.com/v1/objects/3870888a6c3d349d3771a3e9d16c9bf5e076b908/client.jar";
        }

        public static string getNewJarUrl(string ver)
        {
            return "https://raw.githubusercontent.com/and1558/ClientUpdates/main/" + ver + "/and1558-1.0-all.jar";
        }

        public static string getOptifineUrl()
        {
            return "https://raw.githubusercontent.com/and1558/ClientUpdates/main/depends/OptiFine-LOCAL.jar";
        }

        public static string getLaunchwrapperUrl()
        {
            return "https://raw.githubusercontent.com/and1558/ClientUpdates/main/depends/launchwrapper-1.11.jar";
        }

        public static string getJsonUrl(string ver)
        {
            return "https://raw.githubusercontent.com/and1558/ClientUpdates/main/" + ver + "/and1558.json";
        }

        public static string BASE_URL = "https://raw.githubusercontent.com/and1558/ClientUpdates/main/";
    }
}
