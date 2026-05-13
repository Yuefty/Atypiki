using Atypiki.Core.Core.DialogSystem.Enum;
using UnityEngine;

namespace Atypiki.Core.Core.DialogSystem.Helpers
{
    [System.Serializable]
    public struct SentenceStruct
    {
        [TextArea] public string Text;
        public CharacterData CharacterData;
        public Expression expression;
    }
}