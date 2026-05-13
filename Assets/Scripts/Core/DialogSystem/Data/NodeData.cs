using UnityEngine;

namespace Atypiki.Core.Core.DialogSystem
{
    [CreateAssetMenu(fileName = "Node", menuName = "Atypiki/Dialog/NodeData", order = 0)]
    public abstract class NodeData : ScriptableObject
    {
        //public DialogNodeGraph NodeGraph;
        [SerializeField] public bool automaticSkip  = true;
        public abstract NodeData GetNextNode();
    }
}