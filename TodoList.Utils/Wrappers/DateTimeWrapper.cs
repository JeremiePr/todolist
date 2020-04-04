using System;

namespace TodoList.Utils.Wrappers
{
    public class DateTimeWrapper : IDateTimeWrapper
    {
        public DateTime Now { get => DateTime.Now; }
    }
}
