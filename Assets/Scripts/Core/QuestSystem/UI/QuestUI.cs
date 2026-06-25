using System;
using System.Collections.Generic;
using Atypiki.Core.Core.BusSystem;
using Atypiki.Core.Core.BusSystem.Event;
using Atypiki.Core.Core.QuestSystem.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Atypiki.Core.Core.QuestSystem.UI
{
    public class QuestUI : MonoBehaviour
    {
        [Header("Text Slot")]
        [SerializeField] private TextMeshProUGUI questName;
        [SerializeField] private TextMeshProUGUI questDescription;
        [SerializeField] private Image questBackground;
        
        public QuestData QuestData { get; private set; }

        public void Initialize(QuestData questData)
        {
            QuestData = questData;
            questName.text = questData.questName;
            questDescription.text = GetObjectiveDescription(questData.objectives);
        }

        private void Awake()
        {
            EventBus.Subscribe<CompleteQuestEvent>(SetCompleted);
        }

        private void OnDestroy()
        {
            EventBus.Unsubscribe<CompleteQuestEvent>(SetCompleted);
        }
        
        public void SetText(string charName, string text)
        {
            questName.text = charName;
            questDescription.text = text;
        }

        public void SetCompleted(CompleteQuestEvent e)
        {
            if(!e.quest.Definition == QuestData)
                return;
            questBackground.color = Color.green;
        }

        private String GetObjectiveDescription(List<QuestObjectiveData> objectives)
        {
            int nbObjectives = objectives.Count;
            String description = "";
            
            if(nbObjectives == 0)
                return description;
            
            description += FirstCharToUpper(objectives[0].description);
            
            for (int i = 1; i < nbObjectives; i++)
            {
                description += ", ";
                description += objectives[i].description;
                if (i + 1 == nbObjectives)
                {
                    description += ".";
                }
            }
            return description;
        }
        
        /*
         * Function that allow the first letter to be a majuscule
         */
        private static String FirstCharToUpper(String input)
        {
            if (String.IsNullOrEmpty(input))
                return input;

            return char.ToUpper(input[0]) + input.Substring(1);
        }
    }
}