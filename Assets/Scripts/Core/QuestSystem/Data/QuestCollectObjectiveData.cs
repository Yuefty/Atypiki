using System;
using Atypiki.Core.Core.Items;
using Atypiki.Core.Core.QuestSystem.Runtime.QuestObjectives;
using UnityEngine;

namespace Atypiki.Core.Core.QuestSystem.Data
{
    [CreateAssetMenu(fileName = "QuestCollectObjective", menuName = "Atypiki/Quest/QuestObjective/QuestCollectObjectiveData", order = 0)]
    public class QuestCollectObjectiveData : QuestObjectiveData
    {
        [field : SerializeField] public ItemData ItemData { get; private set; }
        [field : SerializeField] public int NbItemToCollect { get; private set; }
        
        public override QuestObjective CreateObjective()
        {
            return new QuestCollectObjective(this);
        }
    }
}