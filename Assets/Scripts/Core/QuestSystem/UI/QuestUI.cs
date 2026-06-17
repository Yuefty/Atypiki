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
    }
}