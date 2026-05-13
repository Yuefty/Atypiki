using Atypiki.Core.Core.DialogSystem.Enum;
using UnityEngine;

namespace Atypiki.Core.Core.DialogSystem.Helpers
{
    public struct Sentence
    {
        [TextArea] public string Text;
        public CharacterData CharacterData;
        public Expression expression;
    }
}