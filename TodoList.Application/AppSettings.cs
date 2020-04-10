namespace TodoList.Application
{
    public class AppSettings
    {
        public AppSettings(string baseApiUrl)
        {
            BaseApiUrl = baseApiUrl;
        }

        public string BaseApiUrl { get; private set; }
    }
}
