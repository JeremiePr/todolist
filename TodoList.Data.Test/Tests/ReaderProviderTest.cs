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
        [DataRow(1, 3, StatusTypes.Postponed)]
        [DataRow(3, 2, null)]
        [DataRow(2, 1, StatusTypes.Todo)]
        [DataRow(0, 6, null)]
        public async Task GetTasksByObjectiveId_TestCount(int expectedCount, int objectiveId, StatusTypes? statusType)
        {
            var tasks = await _provider.GetTasksByObjectiveId(objectiveId, statusType);
            Assert.AreEqual(expectedCount, tasks.Count());
        }

        [DataTestMethod]
        [DataRow("Charger le téléphone", 8, 0, 1, StatusTypes.Todo)]
        [DataRow("Dessert", 4, 1, 2, null)]
        public async Task GetTasksByObjectiveId_TestPriorityOrder(string expectedTitle, int expectedPriority, int itemIndex, int objectiveId, StatusTypes? statusType)
        {
            var tasks = await _provider.GetTasksByObjectiveId(objectiveId, statusType);
            Assert.IsTrue(tasks.Count() > itemIndex);
            var task = tasks.ToList()[itemIndex];
            Assert.AreEqual(expectedTitle, task.Title);
            Assert.AreEqual(expectedPriority, task.Priority);
        }

        [DataTestMethod]
        [DataRow(1, 2)]
        [DataRow(2, 4)]
        [DataRow(0, 6)]
        public async Task GetObjectiveHistoriesByObjectiveId_TestCount(int expectedCount, int objectiveId)
        {
            var histories = await _provider.GetObjectiveHistoriesByObjectiveId(objectiveId);
            Assert.AreEqual(expectedCount, histories.Count());
        }

        [DataTestMethod]
        [DataRow(05, 0, 3)]
        [DataRow(04, 1, 3)]
        public async Task GetObjectiveHistoriesByObjectiveId_TestUpdateDateOrder(int expectedUpdateDateDay, int itemIndex, int objectiveId)
        {
            var histories = await _provider.GetObjectiveHistoriesByObjectiveId(objectiveId);
            Assert.IsTrue(histories.Count() > itemIndex);
            var history = histories.ToList()[itemIndex];
            Assert.AreEqual(expectedUpdateDateDay, history.UpdateDate.Day);
        }

        [DataTestMethod]
        [DataRow("Vacances", 3, 4)]
        [DataRow("Cordes de guitare", 1, 5)]
        [DataRow("Dormir", 10, 1)]
        public async Task GetOneObjective_TestValues(string expectedTitle, int expectedPriority, int objectiveId)
        {
            var objective = await _provider.GetOneObjective(objectiveId);
            Assert.AreEqual(expectedTitle, objective.Title);
            Assert.AreEqual(expectedPriority, objective.Priority);
        }

        [DataTestMethod]
        [DataRow("Entrée", 4, "Manger", 4)]
        [DataRow("Charger le téléphone", 8, "Dormir", 2)]
        [DataRow("Avertir les communes", 10, "Administratif", 8)]
        public async Task GetOneTask_TestValues(string expectedTitle, int expectedPriority, string expectedObjectiveTitle, int taskId)
        {
            var task = await _provider.GetOneTask(taskId);
            Assert.AreEqual(expectedTitle, task.Title);
            Assert.AreEqual(expectedPriority, task.Priority);
            Assert.AreEqual(expectedObjectiveTitle, task.Objective.Title);
        }
    }
}
