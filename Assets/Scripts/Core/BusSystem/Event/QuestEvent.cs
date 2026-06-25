using Atypiki.Core.Core.QuestSystem.Data;
using Atypiki.Core.Core.QuestSystem.Runtime;
using Atypiki.Core.Core.QuestSystem.Runtime.QuestObjectives;
using UnityEngine;

namespace Atypiki.Core.Core.BusSystem.Event
{
    /*
     * Struct for new quest event
     */
    public struct NewQuestEvent
    {
        public QuestData quest;
        
        public NewQuestEvent(QuestData q)
        {
            quest = q;
        }
    }
    
    /*
     * Struct for completion of a quest event
     */
    public struct CompleteQuestEvent
    {
        public Quest quest;
        
        public CompleteQuestEvent(Quest q)
        {
            quest = q;
        }
    }
    
    /*
     * Struct for completion of an objective of a quest event
     */
    public struct CompleteObjectiveQuestEvent
    {
        public QuestObjective questObjective;
        
        public CompleteObjectiveQuestEvent(QuestObjective q)
        {
            questObjective = q;
        }
    }
}