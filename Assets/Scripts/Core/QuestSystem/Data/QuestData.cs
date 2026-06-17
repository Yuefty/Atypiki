using System.Collections.Generic;
using UnityEngine;

namespace Atypiki.Core.Core.QuestSystem.Data
{
    [CreateAssetMenu(fileName = "Quest", menuName = "Atypiki/Quest/QuestData", order = 0)]
    public class QuestData : ScriptableObject
    {
        [field : SerializeField] public string questID;
        [field : SerializeField] public string questName;
        [field : SerializeField] public List<QuestObjectiveData> objectives = new List<QuestObjectiveData>();
    }
}