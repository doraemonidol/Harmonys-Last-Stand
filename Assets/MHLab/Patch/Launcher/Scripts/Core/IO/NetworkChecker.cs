using System;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;

namespace MHLab.Patch.Core.Client.IO
{
    public class NetworkChecker : INetworkChecker
    {
        public ICredentials Credentials { get; set; }
        public IWebProxy    Proxy       { get; set; }

        /// <summary>
        /// Indicates whether any network connection is available
        /// Filter connections below a specified speed, as well as virtual network cards.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if a network connection is available; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool IsNetworkAvailable()
        {
            return IsNetworkAvailable(1000000);
        }

        /// <summary>
        /// Indicates whether any network connection is available.
        /// Filter connections below a specified speed, as well as virtual network cards.
        /// </summary>
        /// <param name="minimumSpeed">The minimum speed required. Passing 0 will not filter connection using speed.</param>
        /// <returns>
        ///     <c>true</c> if a network connection is available; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool IsNetworkAvailable(long minimumSpeed)
        {
            if (NetworkInterface.GetIsNetworkAvailable() == false)
                return false;

            foreach (var ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                // Discards because of standard reasons
                if ((ni.OperationalStatus != OperationalStatus.Up) ||
                    (ni.NetworkInterfaceType == NetworkInterfaceType.Loopback) ||
                    (ni.NetworkInterfaceType == NetworkInterfaceType.Tunnel))
                    continue;

                // This allows us to filter modems, serial, etc.
                // I use 10000000 as a minimum speed for most cases
                if (ni.Speed < minimumSpeed)
                    continue;

                // Discards virtual cards (virtual box, virtual pc, etc.)
                if (IsNetworkInterfaceVirtual(ni))
                    continue;

                // Discards "Microsoft Loopback Adapter", it will not show as NetworkInterfaceType.Loopback but as Ethernet Card.
                if (ni.Description.Equals("Microsoft Loopback Adapter", StringComparison.OrdinalIgnoreCase))
                    continue;

                return true;
            }

            return false;
        }

        private static bool IsNetworkInterfaceVirtual(NetworkInterface networkInterface)
        {
            var description = networkInterface.Description.ToLower();
            var name        = networkInterface.Name.ToLower();

            var isVirtual = description.IndexOf("virtual", StringComparison.OrdinalIgnoreCase) >= 0 ||
                             name.IndexOf("virtual", StringComparison.OrdinalIgnoreCase) >= 0;

            // This is specific for Hyper-V virtual network cards
            if (description.IndexOf("hyper-v", StringComparison.OrdinalIgnoreCase) >= 0 &&
                name.IndexOf("mainswitch", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                isVirtual = false;
            }

            return isVirtual;
        }

        protected virtual void ConfigureHttpRequest(WebRequest request)
        {
        }

        public virtual bool IsRemoteServiceAvailable(string url, out Exception exception)
        {
            try
            {
                using (var client = new PreAuthenticatedWebClient(ConfigureHttpRequest))
                {
                    client.Credentials = Credentials;

                    var response = client.GetStatusCode(new Uri(url));
                    exception = null;
                    return response == HttpStatusCode.OK;
                }
            }
            catch (Exception e)
            {
                exception = e;
                return false;
            }
        }
    }
}