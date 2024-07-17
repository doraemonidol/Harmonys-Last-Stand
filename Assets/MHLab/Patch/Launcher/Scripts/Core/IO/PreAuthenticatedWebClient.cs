using System;
using System.Net;

namespace MHLab.Patch.Core.Client.IO
{
    public class PreAuthenticatedWebClient : WebClient
    {
        private readonly Action<HttpWebRequest> _configureRequest;

        public PreAuthenticatedWebClient(Action<HttpWebRequest> configureRequest)
        {
            _configureRequest = configureRequest;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = base.GetWebRequest(address) as HttpWebRequest;
            if (request == null)
                throw new ArgumentException("The URL parameter is not an HTTP endpoint!", nameof(address));

            request.PreAuthenticate = true;

            _configureRequest.Invoke(request);

            return request;
        }

        public HttpStatusCode GetStatusCode(Uri address)
        {
            var request = GetWebRequest(address);

            if (request == null)
                throw new InvalidOperationException("Cannot create the request.");

            using (var response = GetWebResponse(request) as HttpWebResponse)
            {
                if (response == null)
                    throw new InvalidOperationException("Cannot retrieve the response.");

                return response.StatusCode;
            }
        }
    }
}