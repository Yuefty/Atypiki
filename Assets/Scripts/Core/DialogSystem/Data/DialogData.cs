using System.Collections.Generic;
using UnityEngine;

namespace Atypiki.Core.Core.DialogSystem.Data
{
    [CreateAssetMenu(fileName = "Dialog_", menuName = "Atypiki/Dialog/DialogNode", order = 0)]
    public class DialogData : ScriptableObject
    {
        public string DialogName;
        public List<NodeData> NodesList = new();

        public NodeData GetFirstNode()
        {
            return NodesList.Count > 0 ? NodesList[0] : null;
        }
    }
}