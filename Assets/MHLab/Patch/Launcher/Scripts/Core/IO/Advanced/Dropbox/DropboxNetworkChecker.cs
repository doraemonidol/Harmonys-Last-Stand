using System;
using System.Net;
using Dropbox.Api;
using MHLab.Patch.Core.Client.IO;
using MHLab.Patch.Core.Utilities.Asserts;

namespace MHLab.Patch.Core.Client.Advanced.IO.Dropbox
{
    public class DropboxNetworkChecker : NetworkChecker
    {
        public override bool IsRemoteServiceAvailable(string url, out Exception exception)
        {
            if (Credentials != null)
            {
                Assert.Check(Credentials is NetworkCredential);
            }
            var credentials = (NetworkCredential)Credentials;
            
            using (var client = new DropboxClient(credentials.Password))
            {
                if (!url.StartsWith("/"))
                    url = "/" + url;

                try
                {
                    using (var response = client.Files.DownloadAsync(url).Result)
                    {
                        exception = null;
                        return response.Response.IsFile;
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
}
