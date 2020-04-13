using System.Linq;

namespace TodoList.Application.Utils
{
    public static class HttpRequestUtils
    {
        public static string FormatUrl(string baseUrl, params (string key, object value)[] queryParamsPairs)
        {
            string url = baseUrl;
            if (queryParamsPairs.Length > 0)
            {
                var queryParamsStr = queryParamsPairs
                .Select(pair => $"{pair.key}={pair.value.ToString()}");

                url += "?" + string.Join("&", queryParamsStr);
            }
            return url;
        }
    }
}
