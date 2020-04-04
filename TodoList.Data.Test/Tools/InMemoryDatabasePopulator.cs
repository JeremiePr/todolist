using System;
using TodoList.Data.Context;
using TodoList.Data.Entities;

namespace TodoList.Data.Test.Tools
{
    public class InMemoryDatabasePopulator
    {
        private readonly TodoListContext _context;

        public InMemoryDatabasePopulator(TodoListContext context)
        {
            _context = context;
        }

        public void PopulateData()
        {
            // Status types
            var statusTypeTodo = CreateStatusType(1, "Todo");
            var statusTypePostponed = CreateStatusType(2, "Postponed");
            var statusTypeCancelled = CreateStatusType(3, "Cancelled");
            var statusTypeDone = CreateStatusType(4, "Done");
            // Objectives
            var objectiveSommeil = CreateObjective("Dormir", "Faire dodo c'est important", 10, statusTypeTodo, new DateTime(2020, 05, 01, 23, 30, 00), "C'est l'heure du coucher", new DateTime(2020, 04, 10));
            var objectiveManger = CreateObjective("Manger", "Manger c'est nécessaire", 12, statusTypeTodo, null, "Y'a pas d'heurue pour manger", new DateTime(2020, 04, 10));
            var objectiveAdministratif = CreateObjective("Administratif", "Tâches administratives", 5, statusTypePostponed, null, "La procrastination tu connais", new DateTime(2020, 04, 10));
            var objectiveAllerEnVacances = CreateObjective("Vacances", "Aller je ne sais ou, en montagne", 3, statusTypeCancelled, null, "Avec cette histoire de virus c'est rapé...", new DateTime(2020, 04, 10));
            var objectiveCordesDeGuitare = CreateObjective("Cordes de guitare", "Remettre les cordes à la guitare", 1, statusTypeDone, null, string.Empty, new DateTime(2020, 04, 10));
            // Tasks
            CreateTask(objectiveSommeil, "Lire Tolkien", "Lire 'Contes et légendes inachevés' de J.R.R. Tolkien", 3, statusTypeTodo, new DateTime(2020, 04, 10));
            CreateTask(objectiveSommeil, "Charger le téléphone", "Sinon il sera déchargé demain", 8, statusTypeTodo, new DateTime(2020, 04, 11));
            CreateTask(objectiveSommeil, "Un café", "Un café avant de se coucher ?? WTF", 2, statusTypeCancelled, new DateTime(2020, 04, 12));
            CreateTask(objectiveManger, "Entrée", "Manger l'entrée", 4, statusTypeDone, new DateTime(2020, 04, 10));
            CreateTask(objectiveManger, "Plat", "Manger le plat principal", 10, statusTypeTodo, new DateTime(2020, 04, 11));
            CreateTask(objectiveManger, "Dessert", "Manger le dessert", 4, statusTypeCancelled, new DateTime(2020, 04, 12));
            CreateTask(objectiveAdministratif, "Adresse assurances", "Changer mon adresse pour les assurances", 10, statusTypeDone, new DateTime(2020, 04, 10));
            CreateTask(objectiveAdministratif, "Avertir les communes", "Avertir les 2 communes de ma sortie et de mon arrivée", 10, statusTypeTodo, new DateTime(2020, 04, 11));
            CreateTask(objectiveAdministratif, "Internet", "Ouvrir ligne Internet", 12, statusTypePostponed, new DateTime(2020, 04, 12));
            // Histories
            CreateObjectiveHistory(objectiveSommeil, true, statusTypeTodo, null, new DateTime(2020, 04, 04));
            CreateObjectiveHistory(objectiveManger, true, statusTypeTodo, null, new DateTime(2020, 04, 04));
            CreateObjectiveHistory(objectiveAdministratif, true, statusTypeTodo, null, new DateTime(2020, 04, 04));
            CreateObjectiveHistory(objectiveAdministratif, false, statusTypePostponed, statusTypeTodo, new DateTime(2020, 04, 05));
            CreateObjectiveHistory(objectiveAllerEnVacances, true, statusTypeTodo, null, new DateTime(2020, 04, 04));
            CreateObjectiveHistory(objectiveAllerEnVacances, false, statusTypeCancelled, statusTypeTodo, new DateTime(2020, 04, 05));
            CreateObjectiveHistory(objectiveCordesDeGuitare, true, statusTypeTodo, null, new DateTime(2020, 04, 04));
            CreateObjectiveHistory(objectiveCordesDeGuitare, false, statusTypeDone, statusTypeTodo, new DateTime(2020, 04, 05));
        }

        private StatusTypeDB CreateStatusType(int key, string name)
        {
            var statusType = new StatusTypeDB
            {
                Key = key,
                Name = name
            };
            _context.StatusTypes.Add(statusType);
            _context.SaveChanges();
            return statusType;
        }

        private ObjectiveDB CreateObjective(string title, string details, int priority, StatusTypeDB statusType, DateTime? statusDate, string statusDetails, DateTime lastUpdateDate)
        {
            var objective = new ObjectiveDB
            {
                Details = details,
                Priority = priority,
                StatusDate = statusDate,
                StatusDetails = statusDetails,
                StatusType = statusType,
                StatusTypeKey = statusType.Key,
                Title = title,
                LastUpdateDate = lastUpdateDate
            };
            _context.Objectives.Add(objective);
            _context.SaveChanges();
            return objective;
        }

        private TaskDB CreateTask(ObjectiveDB objective, string title, string details, int priority, StatusTypeDB statusType, DateTime lastUpdateDate)
        {
            var task = new TaskDB
            {
                Details = details,
                Objective = objective,
                ObjectiveId = objective.Id,
                Priority = priority,
                StatusType = statusType,
                StatusTypeKey = statusType.Key,
                Title = title,
                LastUpdateDate = lastUpdateDate
            };
            _context.Tasks.Add(task);
            _context.SaveChanges();
            return task;
        }

        private ObjectiveHistoryDB CreateObjectiveHistory(ObjectiveDB objective, bool isNew, StatusTypeDB currentStatusType, StatusTypeDB previousStatusType, DateTime updateDate)
        {
            var history = new ObjectiveHistoryDB
            {
                CurrentStatusType = currentStatusType,
                CurrentStatusTypeKey = currentStatusType.Key,
                IsNew = isNew,
                Objective = objective,
                ObjectiveId = objective.Id,
                PreviousStatusType = previousStatusType,
                PreviousStatusTypeKey = previousStatusType?.Key,
                UpdateDate = updateDate
            };
            _context.ObjectiveHistories.Add(history);
            _context.SaveChanges();
            return history;
        }
    }
}
