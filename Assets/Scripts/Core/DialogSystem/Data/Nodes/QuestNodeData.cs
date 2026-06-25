using Atypiki.Core.Core.BusSystem;
using Atypiki.Core.Core.BusSystem.Event;
using Atypiki.Core.Core.QuestSystem.Data;
using UnityEngine;

namespace Atypiki.Core.Core.DialogSystem.Data.Nodes
{
    [CreateAssetMenu(fileName = "Quest", menuName = "Atypiki/Dialog/Node/QuestNodeData", order = 0)]
    public class QuestNodeData : NodeData
    {
        [field : SerializeField] private QuestData _questData;
        [Space(10)] 
        [field : SerializeField] public NodeData ChildNode;
        
        public override NodeData GetNextNode()
        {
            return ChildNode;
        }

        public override void ProcessNode(DialogBehaviour dialogBehaviour)
        {
            EventBus.Publish(new NewQuestEvent(_questData));
            dialogBehaviour.ProcessNextNode();
        }
    }
}