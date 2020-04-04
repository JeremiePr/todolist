using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Data.Context;
using TodoList.Data.Providers;
using TodoList.Data.Test.Tools;
using TodoList.Models.Enums;

namespace TodoList.Data.Test.Tests
{
    [TestClass]
    public class ReaderProviderTest
    {
        private ReaderProvider _provider;

        [TestInitialize]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<TodoListContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var context = new TodoListContext(options);
            new InMemoryDatabasePopulator(context).PopulateData();
            _provider = new ReaderProvider(context);
        }

        [DataTestMethod]
        [DataRow(2, StatusTypes.Todo)]
        [DataRow(1, StatusTypes.Postponed)]
        [DataRow(1, StatusTypes.Cancelled)]
        [DataRow(1, StatusTypes.Done)]
        [DataRow(5, null)]
        public async Task GetObjectives_TestCount(int expectedCount, StatusTypes? statusType)
        {
            var objectives = await _provider.GetObjectives(statusType);
            Assert.AreEqual(expectedCount, objectives.Count());
        }

        [DataTestMethod]
        [DataRow("Manger", 12, 0, StatusTypes.Todo)]
        [DataRow("Dormir", 10, 1, StatusTypes.Todo)]
        [DataRow("Vacances", 3, 3, null)]
        [DataRow("Cordes de guitare", 1, 4, null)]
        public async Task GetObjectives_TestPriorityOrder(string expectedTitle, int expectedPriority, int itemIndex, StatusTypes? statusType)
        {
            var objectives = await _provider.GetObjectives(statusType);
            Assert.IsTrue(objectives.Count() > itemIndex);
            var objective = objectives.ToList()[itemIndex];
            Assert.AreEqual(expectedTitle, objective.Title);
            Assert.AreEqual(expectedPriority, objective.Priority);
        }

        [DataTestMethod]
        [DataRow("Lire Tolkien", 3, 1, 1, StatusTypes.Todo)]
        [DataRow("Un café", 2, 1, 2, StatusTypes.Todo)]
        [DataRow("Avertir les communes", 10, 2, 2, null)]
        public async Task GetObjectives_TestTasksPriorityOrder(string expectedTaskTitle, int expectedTaskPriority, int itemIndex, int itemTasksIndex, StatusTypes? statusType)
        {
            var objectives = await _provider.GetObjectives(statusType);
            Assert.IsTrue(objectives.Count() > itemIndex);
            var objective = objectives.ToList()[itemIndex];
            Assert.IsTrue(objective.Tasks.Count() > itemTasksIndex);
            var task = objective.Tasks.ToList()[itemTasksIndex];
            Assert.AreEqual(expectedTaskTitle, task.Title);
            Assert.AreEqual(expectedTaskPriority, task.Priority);
        }

        [DataTestMethod] //TODO
        public async Task GetTasksByObjectiveId_TestCount(int expectedCount, int objectiveId, StatusTypes? statusType)
        {
            var tasks = await _provider.GetTasksByObjectiveId(objectiveId, statusType);
            Assert.AreEqual(expectedCount, tasks.Count());
        }
    }
}
