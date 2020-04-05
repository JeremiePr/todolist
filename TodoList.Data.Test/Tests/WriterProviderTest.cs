using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Data.Context;
using TodoList.Data.Providers;
using TodoList.Data.Test.Tools;
using TodoList.Models.Enums;
using TodoList.Models.Models;
using TodoList.Utils.Wrappers;

namespace TodoList.Data.Test.Tests
{
    [TestClass]
    public class WriterProviderTest
    {
        private static readonly DateTime DEFAULT_DATE_NOW = new DateTime(2020, 04, 06);

        private WriterProvider _provider;
        private TodoListContext _context;

        [TestInitialize]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<TodoListContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _context = new TodoListContext(options);
            new InMemoryDatabasePopulator(_context).PopulateData();

            var dateTimeWrapper = A.Fake<IDateTimeWrapper>();
            A.CallTo(() => dateTimeWrapper.Now).Returns(DEFAULT_DATE_NOW);

            _provider = new WriterProvider(_context, dateTimeWrapper);
        }

        [TestMethod]
        public async Task CreateObjective_Test()
        {
            var dto = new ObjectiveDTO
            {
                Id = 11,
                Details = "Det",
                LastUpdateDate = new DateTime(2005, 01, 01),
                Priority = 5,
                StatusDate = null,
                StatusDetails = "StaDet",
                StatusType = StatusTypes.Todo,
                Title = "Title"
            };
            dto = await _provider.CreateObjective(dto);
            Assert.IsTrue(dto.Id > 0);
            Assert.AreEqual(DEFAULT_DATE_NOW, dto.LastUpdateDate);
            Assert.AreEqual(6, _context.Objectives.Count());
            var allHistories = _context.ObjectiveHistories.ToList();
            Assert.AreEqual(9, allHistories.Count());
            var histories = _context.ObjectiveHistories.Where(x => x.ObjectiveId == dto.Id).ToList();
            Assert.AreEqual(1, histories.Count());
            var history = histories[0];
            Assert.AreEqual(null, history.PreviousStatusType);
            Assert.AreEqual((int)StatusTypes.Todo, history.CurrentStatusType.Key);
            Assert.AreEqual(true, history.IsNew);
        }

        [TestMethod]
        public async Task UpdateObjective_Test()
        {
            var dto = new ObjectiveDTO
            {
                Id = 4,
                Details = "change details",
                LastUpdateDate = new DateTime(2001, 01, 01),
                Priority = 7,
                StatusDate = new DateTime(2001, 01, 01),
                StatusDetails = "change status details",
                StatusType = StatusTypes.Postponed,
                Title = "change title"
            };
            dto = await _provider.UpdateObjective(dto);
            Assert.IsTrue(dto.Id > 0);
            Assert.AreEqual(DEFAULT_DATE_NOW, dto.LastUpdateDate);
            Assert.AreEqual(5, _context.Objectives.Count());
            var allHistories = _context.ObjectiveHistories.ToList();
            Assert.AreEqual(9, allHistories.Count());
            var histories = _context.ObjectiveHistories.Where(x => x.ObjectiveId == dto.Id).ToList();
            Assert.AreEqual(3, histories.Count());
            var history = histories.Single(x => x.CurrentStatusTypeKey == (int)StatusTypes.Postponed);
            Assert.AreEqual((int)StatusTypes.Cancelled, history.PreviousStatusTypeKey);
            Assert.AreEqual((int)StatusTypes.Postponed, history.CurrentStatusType.Key);
            Assert.AreEqual(false, history.IsNew);
        }

        [TestMethod]
        public async Task UpdateObjective_TestWithNotExistingId()
        {
            int id = 25;
            try
            {
                await _provider.UpdateObjective(new ObjectiveDTO { Id = id });
                Assert.Fail();
            }
            catch (Exception ex)
            {
                if (ex.Message != $"There is no objective with id '{id}'")
                    Assert.Fail();
            }
        }

        [TestMethod]
        public async Task CreateTask_Test()
        {
            var dto = new TaskDTO
            {
                Id = 11,
                Details = "Det",
                LastUpdateDate = new DateTime(2015, 01, 01),
                ObjectiveId = 5,
                Priority = 4,
                StatusType = StatusTypes.Todo,
                Title = "Title"
            };
            dto = await _provider.CreateTask(dto);
            Assert.IsTrue(dto.Id > 0);
            Assert.IsNotNull(dto.Objective);
            Assert.AreEqual(DEFAULT_DATE_NOW, dto.LastUpdateDate);
            Assert.AreEqual(10, _context.Tasks.Count());
            Assert.AreEqual(StatusTypes.Todo, dto.StatusType);
        }

        [TestMethod]
        public async Task UpdateTask_Test()
        {
            var dto = new TaskDTO
            {
                Details = "change details",
                Id = 5,
                LastUpdateDate = new DateTime(2022, 05, 14),
                ObjectiveId = 4,
                Priority = 15,
                StatusType = StatusTypes.Done,
                Title = "change title"
            };
            dto = await _provider.UpdateTask(dto);
            Assert.IsTrue(dto.Id > 0);
            Assert.IsNotNull(dto.Objective);
            Assert.AreEqual(2, dto.ObjectiveId);
            Assert.AreEqual(DEFAULT_DATE_NOW, dto.LastUpdateDate);
            Assert.AreEqual(9, _context.Tasks.Count());
            Assert.AreEqual(StatusTypes.Done, dto.StatusType);
        }

        [TestMethod]
        public async Task UpdateTask_TestWithNotExistingId()
        {
            int id = 35;
            try
            {
                await _provider.UpdateTask(new TaskDTO { Id = id });
                Assert.Fail();
            }
            catch (Exception ex)
            {
                if (ex.Message != $"There is no task with id '{id}'")
                    Assert.Fail();
            }
        }

        [TestMethod]
        public async Task DeleteTask_Test()
        {
            await _provider.DeleteTask(6);
            Assert.AreEqual(8, _context.Tasks.Count());
            Assert.AreEqual(2, _context.Tasks.Count(x => x.ObjectiveId == 2));
        }

        [TestMethod]
        public async Task DeleteTask_TestWithNotExistingId()
        {
            int id = 45;
            try
            {
                await _provider.DeleteTask(45);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                if (ex.Message != $"There is no task with id '{id}'")
                    Assert.Fail();
            }
        }
    }
}
