using UnityEngine;

namespace Atypiki.Core.Core.DialogSystem.Data.Nodes
{
    
    public abstract class NodeData : ScriptableObject
    {
        [field : SerializeField] public DialogData DialogData;
        [field : SerializeField] public bool automaticSkip  = true;
        
        public abstract NodeData GetNextNode();
        public abstract void ProcessNode(DialogBehaviour dialogBehaviour);
    }
}