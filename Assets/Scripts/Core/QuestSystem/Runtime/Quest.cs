using System.Collections.Generic;
using Atypiki.Core.Core.BusSystem;
using Atypiki.Core.Core.BusSystem.Event;
using Atypiki.Core.Core.QuestSystem.Data;
using Atypiki.Core.Core.QuestSystem.Runtime.QuestObjectives;
using UnityEngine;

namespace Atypiki.Core.Core.QuestSystem.Runtime
{
    public class Quest
    {
        public QuestData Definition { get; private set; }
        public List<QuestObjective> Objectives { get; private set; } = new List<QuestObjective>();
        public bool IsCompleted { get; private set; }

        public Quest(QuestData data)
        {
            Definition = data;
            
            foreach (QuestObjectiveData objective in data.objectives)
            {
                QuestObjective questObjective = objective.CreateObjective();
                questObjective.Initialize();
                Objectives.Add(questObjective);
            }
        }

        public void Initialize()
        {
            EventBus.Subscribe<CompleteObjectiveQuestEvent>(OnCompleted);
        }

        public void OnCompleted(CompleteObjectiveQuestEvent e)
        {
            foreach (QuestObjective objective in Objectives)
            {
                if (!objective.IsCompleted)
                    return;
            }
            
            IsCompleted = true;
            EventBus.Publish(new CompleteQuestEvent(this));
            EventBus.Unsubscribe<CompleteObjectiveQuestEvent>(OnCompleted);
        }
    }
}