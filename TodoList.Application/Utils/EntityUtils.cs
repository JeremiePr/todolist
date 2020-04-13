using TodoList.Application.Models;
using TodoList.Application.Models.Enums;

namespace TodoList.Application.Utils
{
    public static class EntityUtils
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

        public static bool IsActive(this ObjectiveDTO objective) => objective.StatusType == StatusTypes.Todo || objective.StatusType == StatusTypes.Postponed;

        public static bool IsActive(this TaskDTO task) => task.StatusType == StatusTypes.Todo || task.StatusType == StatusTypes.Postponed;
    }
}
