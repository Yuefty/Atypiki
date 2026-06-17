using Atypiki.Core.Core.DialogSystem.Data;
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