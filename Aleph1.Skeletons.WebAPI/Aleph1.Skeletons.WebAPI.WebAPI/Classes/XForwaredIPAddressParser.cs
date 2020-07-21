using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

using WebApiThrottle.Net;

namespace Aleph1.Skeletons.WebAPI.WebAPI.Classes
{
    internal class XForwaredIPAddressParser : DefaultIpAddressParser
    {
        private const string X_FORWARD_FOR_HEADER = "X-FORWARD-FOR";
        private const char X_FORWARD_FOR_DELIMITER = ',';

        public override IPAddress GetClientIp(HttpRequestMessage request)
        {
            if (request.Headers.TryGetValues(X_FORWARD_FOR_HEADER, out IEnumerable<string> headerValues))
            {
                return ParseIp(headerValues.FirstOrDefault()?.Split(X_FORWARD_FOR_DELIMITER).FirstOrDefault()?.Trim());
            }

            return base.GetClientIp(request);
        }
    }
}