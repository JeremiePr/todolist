using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TodoList.Services
{
    public static class Dependencies
    {
        public static ICollection<(Type contract, Type implementation)> Types => Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => type.Name.EndsWith("Service"))
            .Select(type => new
            {
                name = type.IsInterface && type.Name.StartsWith("I") ? type.Name.Substring(1) : type.Name,
                type
            })
            .GroupBy(t => t.name)
            .Where(g => g.Count(t => t.type.IsClass) == 1 && g.Count(t => t.type.IsInterface) == 1)
            .Select(g => (g.Single(t => t.type.IsInterface).type, g.Single(t => t.type.IsClass).type))
            .ToList();
    }
}
