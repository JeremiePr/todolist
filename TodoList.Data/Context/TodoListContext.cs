using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TodoList.Data.Entities;

namespace TodoList.Data.Context
{
    public class TodoListContext : DbContext
    {
        public TodoListContext(DbContextOptions<TodoListContext> options) : base(options) { }

        public virtual DbSet<StatusTypeDB> StatusTypes { get; set; }
        public virtual DbSet<ObjectiveDB> Objectives { get; set; }
        public virtual DbSet<TaskDB> Tasks { get; set; }
        public virtual DbSet<ObjectiveHistoryDB> ObjectiveHistories { get; set; }
    }
}
