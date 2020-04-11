using TodoList.Application.Models.Enums;

namespace TodoList.Application.Tools
{
    public static class AppUtils
    {
        public static string GetStatusTypeText(StatusTypes? statusType)
        {
            switch (statusType)
            {
                case StatusTypes.Todo:
                    return "To do";
                case StatusTypes.Postponed:
                    return "Postponed";
                case StatusTypes.Cancelled:
                    return "Cancelled";
                case StatusTypes.Done:
                    return "Done";
                default:
                    return string.Empty;
            }
        }
    }
}
