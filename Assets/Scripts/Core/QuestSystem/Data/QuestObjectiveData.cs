using Atypiki.Core.Core.QuestSystem.Runtime.QuestObjectives;
using UnityEngine;

namespace Atypiki.Core.Core.QuestSystem.Data
{
    [System.Serializable]
    public abstract class QuestObjectiveData : ScriptableObject
    {
        [field : SerializeField] public string objectiveName;
        [field : SerializeField] public string description;

        public abstract QuestObjective CreateObjective();
    }
}