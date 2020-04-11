using TodoList.Application.Models.Enums;

namespace TodoList.Application.Tools
{
    public static class AppUtils
    {
        public static string GetStatusTypeText(StatusTypes? statusType) =>
            statusType switch
            {
                StatusTypes.Todo        => "Todo",
                StatusTypes.Postponed   => "Postponed",
                StatusTypes.Cancelled   => "Cancelled",
                StatusTypes.Done        => "Done",
                _                       => string.Empty
            };
    }
}
