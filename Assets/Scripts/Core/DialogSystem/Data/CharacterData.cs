using UnityEngine;

namespace Atypiki.Core.Core.DialogSystem
{
    [CreateAssetMenu(fileName = "Character_", menuName = "Atypiki/Dialog/CharacterData", order = 0)]
    public class CharacterData : ScriptableObject
    {
        [field: SerializeField] public string id { get; private set; }
        [field: SerializeField] public string displayName { get; private set; }
    }
}