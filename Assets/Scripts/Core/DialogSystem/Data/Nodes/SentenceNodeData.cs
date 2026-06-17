using Atypiki.Core.Core.DialogSystem.Helpers;
using UnityEngine;

namespace Atypiki.Core.Core.DialogSystem.Data
{
    [CreateAssetMenu(fileName = "Sentence", menuName = "Atypiki/Dialog/Node/SentenceData", order = 0)]
    public class SentenceNodeData : NodeData
    {
        [field : SerializeField] private SentenceStruct sentenceStruct;
        [Space(10)] 
        [field : SerializeField] public NodeData ChildNode;
        
        public override NodeData GetNextNode()
        {
            return ChildNode;
        }

        public override void ProcessNode(DialogBehaviour dialogBehaviour)
        {
            dialogBehaviour.ShowText(sentenceStruct);
        }

        public SentenceStruct SentenceStruct => sentenceStruct;
    }
}