using System;
using System.Collections.Generic;
using Atypiki.Core.Core.BusSystem;
using Atypiki.Core.Core.BusSystem.Event;
using Atypiki.Core.Core.QuestSystem.Runtime;
using UnityEngine;

namespace Atypiki.Core.Core.QuestSystem
{
    /*
     * This manager handle quests, it tracks whenever a new quest id accepted and when ongoing quests are completed.
     */
    public class QuestManager : MonoBehaviour
    {
        private List<Quest> onGoingQuests = new List<Quest>();
        private List<Quest> completedQuests = new List<Quest>();

        private void OnEnable()
        {
            EventBus.Subscribe<NewQuestEvent>(OnNewQuest);
            EventBus.Subscribe<CompleteQuestEvent>(OnCompletedQuest);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<NewQuestEvent>(OnNewQuest);
            EventBus.Unsubscribe<CompleteQuestEvent>(OnCompletedQuest);
        }

        /*
         * Create new quest based on the quest data, initialize it and add it to ongoing quests
         */
        public void OnNewQuest(NewQuestEvent e)
        {
            Quest quest = new Quest(e.quest);
            quest.Initialize();
            onGoingQuests.Add(quest);
        }

        public void OnCompletedQuest(CompleteQuestEvent e)
        {
            Debug.Log("Quest '" + e.quest.Definition.questName + "' has been completed");
            completedQuests.Add(e.quest);
            onGoingQuests.Remove(e.quest);
        }
    }
}