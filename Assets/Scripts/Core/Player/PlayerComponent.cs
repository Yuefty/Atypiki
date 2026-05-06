using UnityEngine;

namespace Atypiki.Core
{
    public class PlayerComponent : MonoBehaviour
    {
        
        [field: SerializeField]
        public PlayerManager Manager { get; private set; }

        private void OnValidate()
        {
            if (Manager == null)
                Manager = FindObjectOfType<PlayerManager>();
        }
    }
}
