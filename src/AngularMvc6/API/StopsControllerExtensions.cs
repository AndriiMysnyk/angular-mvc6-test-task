using System;
using System.Globalization;
using Microsoft.AspNet.Http;

namespace AngularMvc6.API
{
    internal static class StopsControllerExtensions
    {
        public static bool IsClientDataUpToDate(this HttpRequest request, DateTime lastModified)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var headerValue = request.Headers["If-Modified-Since"];
            if (headerValue == null)
                return false;

            var ifModifiedSince = DateTime.Parse(headerValue).ToLocalTime();
            return (ifModifiedSince.TrimMilliseconds() >= lastModified.TrimMilliseconds());
        }

        public static void SetLatModified(this HttpResponse response, DateTime lastModified)
        {
            if (response == null)
                throw new ArgumentNullException(nameof(response));

            response.Headers.Add("Last-Modified", new[] { lastModified.ToString(CultureInfo.InvariantCulture) });
        }

        public static DateTime TrimMilliseconds(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, 0);
        }
    }
}