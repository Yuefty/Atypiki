using System.Collections.Generic;
using Atypiki.Core.Core.BusSystem;
using Atypiki.Core.Core.BusSystem.Event;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Atypiki.Core.Core.QuestSystem.UI
{
    public class QuestManagerUI : MonoBehaviour
    {
        
        [SerializeField] private QuestUI _questUIPrefab;
        [SerializeField] private Transform _questUIparentTransform;
        
        private List<QuestUI> _questUIs = new List<QuestUI>();

        private void OnEnable()
        {
            EventBus.Subscribe<NewQuestEvent>(AddQuestToUI);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<NewQuestEvent>(AddQuestToUI);
        }

        public void AddQuestToUI(NewQuestEvent e)
        {
            Debug.Log("Adding quest to UI");
            QuestUI dialogHistoricUI = Instantiate(_questUIPrefab, _questUIparentTransform);
            dialogHistoricUI.Initialize(e.quest);
            _questUIs.Add(dialogHistoricUI);
            
            Debug.Log("Adding quest to UI");
        }
    }
}