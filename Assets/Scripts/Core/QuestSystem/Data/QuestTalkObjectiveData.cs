using Atypiki.Core.Core.DialogSystem.Data;
using Atypiki.Core.Core.QuestSystem.Runtime.QuestObjectives;
using UnityEngine;

namespace Atypiki.Core.Core.QuestSystem.Data
{
    [CreateAssetMenu(fileName = "QuestTalkObjective", menuName = "Atypiki/Quest/QuestTalkObjectiveData", order = 0)]
    public class QuestTalkObjectiveData : QuestObjectiveData
    {
        [field : SerializeField] public CharacterData NpcId;
        
        public override QuestObjective CreateObjective()
        {
            return new QuestTalkObjective(this);
        }
    }
}