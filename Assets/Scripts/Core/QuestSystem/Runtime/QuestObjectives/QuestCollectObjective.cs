using System;
using Atypiki.Core.Core.BusSystem;
using Atypiki.Core.Core.BusSystem.Event;
using Atypiki.Core.Core.QuestSystem.Data;

namespace Atypiki.Core.Core.QuestSystem.Runtime.QuestObjectives
{
    public class QuestCollectObjective : QuestObjective
    {
        private String itemID;
        private int nbItemToCollect;

        private int currentCollectNumber;
        
        public QuestCollectObjective(QuestCollectObjectiveData data) : base(data)
        {
            itemID = data.ItemData.ItemID;
            nbItemToCollect = data.NbItemToCollect;
        }

        public override void Initialize()
        {
            EventBus.Subscribe<ItemEvent.CollectItemEvent>(OnCollect);
        }

        public void OnCollect(ItemEvent.CollectItemEvent e)
        {
            if (e.itemID != itemID)
                return;
            
            currentCollectNumber++;
            if (currentCollectNumber == nbItemToCollect)
            {
                IsObjectiveCompleted();
                EventBus.Unsubscribe<ItemEvent.CollectItemEvent>(OnCollect);
            }
        }
    }
}