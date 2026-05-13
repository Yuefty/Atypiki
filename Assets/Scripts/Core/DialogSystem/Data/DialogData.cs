using System.Collections.Generic;
using UnityEngine;

namespace Atypiki.Core.Core.DialogSystem
{
    [CreateAssetMenu(fileName = "Dialog_", menuName = "Atypiki/Dialog/DialogNode", order = 0)]
    public class DialogData : ScriptableObject
    {
        public List<NodeData> NodesList = new();
    }
}