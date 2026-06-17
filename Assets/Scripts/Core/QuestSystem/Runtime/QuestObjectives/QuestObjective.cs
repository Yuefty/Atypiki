using Atypiki.Core.Core.BusSystem;
using Atypiki.Core.Core.BusSystem.Event;
using Atypiki.Core.Core.QuestSystem.Data;

namespace Atypiki.Core.Core.QuestSystem.Runtime.QuestObjectives
{
    public abstract class QuestObjective
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool IsCompleted { get; protected set; } 

        public QuestObjective(QuestObjectiveData data)
        {
            Name = data.objectiveName;
            Description = data.description;
        }
        
        public abstract void Initialize();

        protected void IsObjectiveCompleted()
        {
            IsCompleted = true;
            EventBus.Publish(new CompleteObjectiveQuestEvent(this));
        }
    }
}