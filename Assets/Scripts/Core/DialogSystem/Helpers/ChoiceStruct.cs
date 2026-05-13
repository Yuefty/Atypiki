using UnityEngine;

namespace Atypiki.Core.Core.DialogSystem.Helpers
{
    [System.Serializable]
    public struct ChoiceStruct
    {
        [TextArea] public string choiceText;
        public NodeData ChildNode;
    }
}