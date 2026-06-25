using Atypiki.Core.Core.DialogSystem.Data;
using Atypiki.Core.Core.NPC;
using UnityEngine;

namespace Atypiki.Core.Core.BusSystem.Event
{
    public struct StartDialogEvent
    {
        public CharacterData characterData;
        
        public StartDialogEvent(CharacterData cD)
        {
            characterData = cD;
        }
    }
    
    public struct EndDialogEvent
    {
        public CharacterData characterData;

        public EndDialogEvent(CharacterData cD)
        {
            characterData = cD;
        }
    }
}