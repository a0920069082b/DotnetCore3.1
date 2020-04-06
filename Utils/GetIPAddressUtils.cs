using System.Net;

namespace FubonMailApi.Utils
{
    class GetIPAddressUtils
    {
        public string GetIPAddress()
        {
            string IP = string.Empty;
            // 取得本機名稱
            string strHostName = Dns.GetHostName();

            // 取得本機的IpHostEntry類別實體，MSDN建議新的用法
            IPHostEntry iphostentry = Dns.GetHostEntry(strHostName);

            // 取得所有 IP 位址
            foreach (IPAddress ipaddress in iphostentry.AddressList)
            {
                // 只取得IP V4的Address
                if (ipaddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    IP = ipaddress.ToString();
                }
            }
            return IP;
        }
    }
}